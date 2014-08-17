#region LGPL License
/*----------------------------------------------------------------------------
* This file (CK.Releaser\Info\InfoRelease.Data.cs) is part of CiviKey. 
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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using CK.Core;

namespace CK.Releaser.Info
{
    public partial class InfoRelease
    {
        static readonly XName _nReleaseInfo = XNamespace.None + "ReleaseInfo";
        static readonly XName _nNotes = XNamespace.None + "Notes";
        static readonly XName _nCommitSha = XNamespace.None + "CommitSha";
        static readonly XName _nVersion = XNamespace.None + "Version";
        static readonly XName _nSource = XNamespace.None + "Source";
        static readonly XName _nPreRelease = XNamespace.None + "PreRelease";
        static readonly XName _nBuildMetadata = XNamespace.None + "BuildMetadata";
        static readonly XName _nReadyForRelease = XNamespace.None + "ReadyForRelease";
        static readonly XName _nName = XNamespace.None + "Name";
        static readonly XName _nUtcTime = XNamespace.None + "UtcTime";
        static readonly XName _nReleasers = XNamespace.None + "Releasers";
        static readonly XName _nReleaser = XNamespace.None + "Releaser";

        public enum Status
        {
            Current,
            ReadyToRealease,
            Released,
            Invalid
        }

        public class Releaser
        {
            DateTime _utcTime;

            public string Name { get; set; }

            public DateTime UtcTime
            {
                get { return _utcTime; }
                set { _utcTime = value; }
            }

            public Releaser( string name, DateTime t )
            {
                Name = name;
                UtcTime = t;
            }

            public bool IsValid
            {
                get
                {
                    return !String.IsNullOrWhiteSpace( Name ) && UtcTime.Kind == DateTimeKind.Utc && UtcTime.Year > 2000;
                }
            }

            internal Releaser( XElement e )
            {
                Name = (string)e.Attribute( _nName );
                string unusedRemaider;
                InfoReleaseDatabase.TryParseFolderName( (string)e.Attribute( _nUtcTime ), out _utcTime, out unusedRemaider );
            }

            internal XElement ToXml()
            {
                return new XElement( _nReleaser, new XAttribute( _nName, Name ), new XAttribute( _nUtcTime, InfoReleaseDatabase.FormatFolderName( UtcTime ) ) );
            }
        }

        public class DataInfo
        {
            readonly List<Releaser> _releasers;

            public string Notes { get; set; }

            public string CommitSha { get; set; }

            public string SourceVersion { get; set; }

            public string PreReleaseVersion { get; set; }

            public string BuildMetadataVersion { get; set; }

            public Releaser ReadyForRelease { get; set; }

            public List<Releaser> Releasers { get { return _releasers; } }

            public Status Status
            {
                get
                {
                    if( _releasers.Count == 0 )
                    {
                        if( ReadyForRelease != null )
                        {
                            if( ReadyForRelease.IsValid ) return InfoRelease.Status.ReadyToRealease;
                            return InfoRelease.Status.Invalid;
                        }
                        return InfoRelease.Status.Current;
                    }
                    if( ReadyForRelease == null
                        || _releasers.Any( r => r == null || !r.IsValid ) )
                    {
                        return InfoRelease.Status.Invalid;
                    }
                    return InfoRelease.Status.Released;
                }
            }

            public DataInfo()
            {
                _releasers = new List<Releaser>();
            }

            internal DataInfo( XElement e )
            {
                Notes = String.Join( Environment.NewLine, e.Elements( _nNotes ).Select( n => n.Value ) );
                CommitSha = e.Elements( _nCommitSha ).Select( c => c.Value ).FirstOrDefault();
                
                SourceVersion = e.Elements( _nVersion ).Elements( _nSource ).Select( c => c.Value ).FirstOrDefault();
                PreReleaseVersion = e.Elements( _nVersion ).Elements( _nPreRelease ).Select( c => c.Value ).FirstOrDefault();
                BuildMetadataVersion = e.Elements( _nVersion ).Elements( _nBuildMetadata ).Select( c => c.Value ).FirstOrDefault();
                ReadyForRelease = e.Elements( _nReadyForRelease ).Elements( _nReleaser ).Select( r => new Releaser( r ) ).FirstOrDefault();
                _releasers = e.Elements( _nReleasers ).Elements( _nReleaser ).Select( r => new Releaser( r ) ).ToList();
            }

            internal XElement ToXml()
            {
                return new XElement( _nReleaseInfo,
                                        new XElement( _nVersion,
                                            new XElement( _nSource, SourceVersion ),
                                            new XElement( _nPreRelease, PreReleaseVersion ),
                                            new XElement( _nBuildMetadata, BuildMetadataVersion ) ),
                                        new XElement( _nNotes, Notes ),
                                        new XElement( _nCommitSha, CommitSha ),
                                        ReadyForRelease != null ? new XElement( _nReadyForRelease, ReadyForRelease.ToXml() ) : null,
                                        new XElement( _nReleasers, _releasers.Select( r => r.ToXml() ) ) );
            }
        }

        /// <summary>
        /// Ensures that data exists by loading it or creating a new one if needed.
        /// If an error occured, logs the error, and returns null.
        /// </summary>
        /// <param name="m"></param>
        /// <returns>Null or the data associated to this info.</returns>
        public DataInfo GetData( IActivityMonitor m )
        {
            if( _data == null )
            {
                string p = System.IO.Path.Combine( Path, ReleaseInfoFileName );
                try
                {
                    if( File.Exists( p ) )
                    {
                        _data = new DataInfo( XDocument.Load( p ).Root );
                    }
                    else _data = new DataInfo();
                }
                catch( Exception ex )
                {
                    m.Error().Send( ex, "While loading '{0}'.", p );
                }
            }
            return _data;
        }

        /// <summary>
        /// Attempts to save the current <see cref="P:Data"/> that must not be null.
        /// </summary>
        /// <param name="m"></param>
        /// <returns>True on success, false on error.</returns>
        public bool SaveData( IActivityMonitor m )
        {
            if( _data == null ) throw new InvalidOperationException( "Data is null." );
            string p = System.IO.Path.Combine( Path, ReleaseInfoFileName );
            try
            {
                var attr = File.Exists( p ) ? File.GetAttributes( p ) : FileUtil.InexistingFile;
                if( (attr & FileAttributes.Hidden) != 0 ) File.SetAttributes( p, attr & ~FileAttributes.Hidden );
                new XDocument( _data.ToXml() ).Save( p );
                if( attr == FileUtil.InexistingFile ) attr = File.GetAttributes( p ) | FileAttributes.Hidden;
                if( (attr & FileAttributes.Hidden) != 0 ) File.SetAttributes( p, attr | FileAttributes.Hidden );
            }
            catch( Exception ex )
            {
                m.Error().Send( ex, "While saving '{0}'.", p );
                return false;
            }
            return true;
        }

    }
}
