#region LGPL License
/*----------------------------------------------------------------------------
* This file (CK.Releaser\Tools\VersionUtil.cs) is part of CiviKey. 
*  
* CiviKey is free software: you can redistribute it and/or modify 
* it under the terms of the GNU Lesser General Public License as published 
* by the Free Software Foundation, either version 3 of the License, or 
* (at your option) any later version. 
*  
* CiviKey is distributed in the hope that it will be useful, 
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the 
* GNU Lesser General Public License for more details. 
* You should have received a copy of the GNU Lesser General Public License 
* along with CiviKey.  If not, see <http://www.gnu.org/licenses/>. 
*  
* Copyright © 2007-2014, 
*     Invenietis <http://www.invenietis.com>,
*     In’Tech INFO <http://www.intechinfo.fr>,
* All rights reserved. 
*-----------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CK.Core;

namespace CK.Releaser
{
    public static class VersionUtil
    {
        public static readonly string StandardInformationalVersionContent = "%semver% - %githash%";

        // [assembly: AssemblyVersion( "4.0.0" )]
        // [assembly: AssemblyInformationalVersion( "%semver% - %githash%" )]
        // [assembly: AssemblyFileVersion( "4.0.0" )]
        static readonly Regex _versions = new Regex( @"\[\s*assembly\s*:\s*Assembly(?<1>(Informational|File)?)?Version(Attribute)?\s*\(\s*\""(?<2>.*?)\""\s*\)\s*\]", RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture );

        [Flags]
        public enum CheckStatus
        {
            Valid = 0,
            MissingVersion = 1,
            BadOrMissingInformationalVersion = 2,
            UselessFileVersion = 4
        }


        /// <summary>
        /// Tests if this <see cref="Version"/> is http://semver.org compliant: it must be only Major.Minor.Patch.
        /// (<see cref="Version.Build"/> exists but not <see cref="Version.Revision"/>).
        /// </summary>
        /// <param name="this">This version.</param>
        /// <returns>True if it is "semantic versionning" compliant.</returns>
        public static bool IsSemVerCompliant( this Version @this )
        {
            return @this.Build >= 0 && @this.Revision == -1;
        }

        /// <summary>
        /// Computes the <see cref="CheckStatus"/> from a source.
        /// </summary>
        /// <param name="source">CSharp Source text.</param>
        /// <returns>The</returns>
        public static CheckStatus CheckVersionAttributes( string source )
        {
            return ExtractVersionAttributes( source ).Item4;
        }

        /// <summary>
        /// Reads the AssemblyVersionAttribute string content. Null if not found.
        /// </summary>
        /// <param name="source">CSharp Source text.</param>
        /// <returns>The version string.</returns>
        public static string ReadVersionAttribute( string source )
        {
            Match v = ExtractVersionAttributes( source ).Item1;
            if( v == null ) return null;
            return v.Groups[2].Value;
        }

        /// <summary>
        /// Sets the AssemblyVersionAttribute in a source file that MUST contain the attribute definition. 
        /// The version MUST be Major.Minor.Patch.
        /// </summary>
        /// <param name="source">CSharp Source text that contains the attribute.</param>
        /// <param name="version">Version Major.Minor.Patch.</param>
        /// <returns>The version string.</returns>
        public static string SetVersionAttribute( string source, Version version )
        {
            Match v = ExtractVersionAttributes( source ).Item1;
            if( v == null ) throw new ArgumentException( "Source file must contain AssemblyAttribute.", "source" );
            if( !version.IsSemVerCompliant() ) throw new ArgumentException( "Must be a Major.Minor.Patch version.", "version" );
            var g =  v.Groups[2];
            return source.Remove( g.Index, g.Length ).Insert( g.Index, version.ToString( 3 ) );
        }

        public static string CorrectMajorMinorPatchVersion( IActivityMonitor m, string source )
        {
            Tuple<Match, Match, Match, CheckStatus> attr = ExtractVersionAttributes( source );
            Match mV = attr.Item1;
            Version v;
            if( attr.Item4 == CheckStatus.Valid
                || mV == null
                || !Version.TryParse( mV.Groups[2].Value, out v ) )
            {
                throw new ArgumentException( "Source file must contain a valid Version attribute before calling this.", "source" );
            }
            int patch = v.Build == -1 ? 0 : v.Build;
            v = new Version( v.Major, v.Minor, patch );
            string vCorrect = v.ToString( 3 );
            var g = mV.Groups[2];
            if( vCorrect != g.Value )
            {
                m.Trace().Send( "Correcting version '{0}' to be {1}.", g.Value, vCorrect );
                source = source.Remove( g.Index, g.Length ).Insert( g.Index, vCorrect );
            }
            return source;
        }

        public static string RemoveFileVersionAttribute( IActivityMonitor m, string source )
        {
            Tuple<Match, Match, Match, CheckStatus> attr = ExtractVersionAttributes( source );
            Match file = attr.Item3;
            if( file != null )
            {
                m.Trace().Send( "Removing AssemblyFileVersionAttribute." );
                source = source.Remove( file.Index, file.Length );
            }
            return source;
        }


        internal static string EnsureVersionAttributes( IActivityMonitor m, string source )
        {
            Tuple<Match, Match, Match, CheckStatus> attr = ExtractVersionAttributes( source );
            StringBuilder b = new StringBuilder( source );
            Match version = attr.Item1, info = attr.Item2;
            CheckStatus status = attr.Item4;
            Debug.Assert( (status & CheckStatus.UselessFileVersion) == 0, "This must have been done before!" );

            bool commentAdded = false;
            if( (status & CheckStatus.BadOrMissingInformationalVersion) != 0 )
            {
                if( info != null )
                {
                    m.Trace().Send( "Removing bad AssemblyInformationalVersionAttribute." );
                    b.Remove( info.Index, info.Length );
                }
                m.Trace().Send( "Adding AssemblyInformationalVersion( \"{0}\" ).", StandardInformationalVersionContent );
                commentAdded = true;
                b.AppendLine().AppendLine( "// Added by CKReleaser." );
                b.AppendFormat( @"[assembly: AssemblyInformationalVersion( """ + StandardInformationalVersionContent + @""" )]" );
                b.AppendLine();
            }
            if( (status & CheckStatus.MissingVersion) != 0 )
            {
                m.Trace().Send( "Adding AssemblyVersion( \"0.1.0\" )." );
                if( !commentAdded ) b.AppendLine().AppendLine( "// Added by CKReleaser." );
                b.AppendFormat( @"[assembly: AssemblyVersion( ""0.1.0"" )]" );
                b.AppendLine();
            }
            source = b.ToString();
            Debug.Assert( CheckVersionAttributes( source ) == CheckStatus.Valid );
            return source;
        }

        static Tuple<Match, Match, Match, CheckStatus> ExtractVersionAttributes( string source )
        {
            if( source == null ) throw new ArgumentNullException( "source" );
            Match version = null, info = null, file = null;
            CheckStatus s = CheckStatus.MissingVersion|CheckStatus.BadOrMissingInformationalVersion;
            Match m = _versions.Match( source );
            while( m.Success )
            {
                if( m.Groups[1].Value == "File" )
                {
                    file = m;
                    s |= CheckStatus.UselessFileVersion;
                }
                else if( m.Groups[1].Value == "Informational" )
                {
                    info = m;
                    if( m.Groups[2].Value == StandardInformationalVersionContent ) s &= ~CheckStatus.BadOrMissingInformationalVersion;
                }
                else
                {
                    version = m;
                    s &= ~CheckStatus.MissingVersion;
                }
                m = m.NextMatch();
            }
            return Tuple.Create( version, info, file, s );
        }


    }
}
