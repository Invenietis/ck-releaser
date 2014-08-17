#region LGPL License
/*----------------------------------------------------------------------------
* This file (CK.Releaser\Tools\FileHeaderProcessor.cs) is part of CiviKey. 
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
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;
using CK.Core;

namespace CK.Releaser.Tools
{
    public static class FileHeaderProcessor
    {
        static string _defaultLicense;

        public static string DefaultLicenceText
        {
            get
            {
                if( _defaultLicense == null )
                {
                    using( Stream stream = Assembly.GetAssembly( typeof( FileHeaderProcessor ) ).GetManifestResourceStream( typeof( FileHeaderProcessor ).Namespace + ".DefaultLicenseText.txt" ) )
                    using( StreamReader reader = new StreamReader( stream ) )
                    {
                        _defaultLicense = reader.ReadToEnd().Replace( "[CurrentYear]", DateTime.UtcNow.Year.ToString() );
                    }
                }
                return _defaultLicense;
            }
        }

        public static int Process( string rootPath, bool addNew, bool removeExisting, string newLicenceText = null )
        {
            if( newLicenceText == null ) newLicenceText = DefaultLicenceText;
            DirectoryInfo d = new DirectoryInfo( rootPath );
            if( d.Exists )
            {
                string root = FileUtil.NormalizePathSeparator( d.FullName, true );
                string text = null;
                if( addNew )
                {
                    text = newLicenceText;
                    if( text.Length == 0 ) text = null;
                    else
                    {
                        if( !text.EndsWith( Environment.NewLine ) ) text += Environment.NewLine;
                        // Inserts a new blank line.
                        text += Environment.NewLine;
                    }
                }
                if( text != null || removeExisting )
                {
                    return ProcessDirectory( root.Length, d, text, removeExisting );
                }
            }
            return 0;
        }

        static int ProcessDirectory( int rootLength, DirectoryInfo d, string text, bool removeExisting )
        {
            int count = 0;
            if( d.Name == ".svn" || d.Name == ".nuget" || d.Name == ".git" || (d.Attributes & FileAttributes.Hidden) != 0 ) return count;
            foreach( FileInfo f in d.GetFiles( "*.cs" ) )
            {
                if( ProcessFile( rootLength, f, text, removeExisting ) ) ++count;
            }
            foreach( DirectoryInfo c in d.GetDirectories() )
            {
                count += ProcessDirectory( rootLength, c, text, removeExisting );
            }
            return count;
        }

        static bool ProcessFile( int rootLength, FileInfo f, string text, bool removeExisting )
        {
            if( !f.Name.EndsWith( ".Designer.cs" ) )
            {
                AddHeader( rootLength, f, text, removeExisting );
                return true;
            }
            return false;
        }

        internal static Regex _existing = new Regex( "\\s*#region \\w+ License\\s.*?#endregion\\s+",
            RegexOptions.Singleline | RegexOptions.ExplicitCapture | RegexOptions.CultureInvariant | RegexOptions.Compiled );

        static void AddHeader( int rootLength, FileInfo f, string text, bool removeExisting )
        {
            if( text != null )
            {
                string fName = f.FullName.Substring( rootLength );
                text = text.Replace( "[FILE]", fName );
            }
            string content = File.ReadAllText( f.FullName );
            if( removeExisting )
            {
                content = _existing.Replace( content, String.Empty, 1 );
            }
            File.WriteAllText( f.FullName, text + content );
        }

    }
}
