#region LGPL License
/*----------------------------------------------------------------------------
* This file (CK.Releaser\Info\BranchRelease.cs) is part of CiviKey. 
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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CK.Core;

namespace CK.Releaser.Info
{
    /// <summary>
    /// Holds current and history release information for a branch. 
    /// </summary>
    public class BranchRelease : InfoReleaseDatabase.DBObject
    {
        internal static readonly string HistoricFolderName = ".releases";
        internal static readonly string HistoricFolderNameSuffix = System.IO.Path.DirectorySeparatorChar + HistoricFolderName;

        public readonly string SolutionName;
        public readonly string BranchName;


        readonly InfoRelease _current;

        internal readonly InfoReleaseDatabase Database;
        public readonly string Path;
        internal readonly string HistoryPath;
        
        internal BranchRelease( string keyId, InfoReleaseDatabase db, string solutionName, string branchName, string path )
            : base( keyId )
        {
            Database = db;
            SolutionName = solutionName;
            BranchName = branchName;
            Path = path;
            HistoryPath = FileUtil.NormalizePathSeparator( System.IO.Path.Combine( Path, HistoricFolderName ), true );
            _current = db.FindOrCreateInfoRelease( this, Util.UtcMinValue, Path );
        }

        /// <summary>
        /// Gets the current <see cref="InfoRelease"/> for this branch.
        /// </summary>
        public InfoRelease Current
        {
            get { return _current; }
        }

        /// <summary>
        /// Gets all the historical <see cref="InfoRelease"/> this BranchRelease contains ordered
        /// from the most recent to the oldest one.
        /// </summary>
        public IReadOnlyList<InfoRelease> GetPastInfoReleases( IActivityMonitor m )
        {
            if( !Directory.Exists( HistoryPath ) ) return CKReadOnlyListEmpty<InfoRelease>.Empty;
            var infos = new List<InfoRelease>();
            foreach( var p in Directory.EnumerateDirectories( HistoryPath ) )
            {
                var parsed = new ParsedPath( p );
                if( parsed.IsTimedDir )
                {
                    infos.Add( Database.FindOrCreateInfoRelease( this, parsed.Time, p ) );
                }
                else m.Error().Send( "Invalid folder name: '{0}' in '{1}'.", parsed.FolderName, Path );
            }
            infos.Sort( ( r1, r2 ) => r2.UtcTime.CompareTo( r1.UtcTime ) );
            return infos;
        }

        /// <summary>
        /// Copies the content of the current folder to the <see cref="HistoricFolderName"/> sub folder at the specified time.
        /// </summary>
        /// <param name="m">Monitor to use.</param>
        /// <param name="sourceVersion">The source version (Major.Minor.Patch).</param>
        /// <param name="commitSha">The sha signature of the commit.</param>
        /// <param name="commitUtcTime">Time of the commit.</param>
        /// <param name="actorName">Name of the actor.</param>
        public bool ReadyToReleaseCurrent( IActivityMonitor m, Version sourceVersion, string commitSha, DateTime commitUtcTime, string actorName )
        {
            if( sourceVersion == null ) throw new ArgumentNullException( "sourceVersion" );
            if( !sourceVersion.IsSemVerCompliant() ) throw new ArgumentException( "Must be Majot.Minor.Patch.", "sourceVersion" );
            if( String.IsNullOrEmpty( commitSha ) ) throw new ArgumentException( "commitSha" );
            if( commitUtcTime.Kind != DateTimeKind.Utc && commitUtcTime != Util.UtcMinValue && commitUtcTime != DateTime.MinValue )
            {
                throw new ArgumentException( "Must be UTC time and not empty.", "releaseUtcTime" );
            }

            string newDirName = InfoReleaseDatabase.FormatFolderName( commitUtcTime );
            DirectoryInfo newDir = new DirectoryInfo( System.IO.Path.Combine( HistoryPath, newDirName ) );
            if( _current.CopyContentTo( m, newDir ) == null ) return false;
            InfoRelease r = Database.FindOrCreateInfoRelease( this, commitUtcTime, newDir.FullName );
            var data = r.GetData( m );
            if( data == null ) return false;
            data.SourceVersion = sourceVersion.ToString(3);
            data.CommitSha = commitSha;
            if( String.IsNullOrWhiteSpace( actorName ) ) actorName = Environment.MachineName;
            data.ReadyForRelease = new InfoRelease.Releaser( actorName, DateTime.UtcNow );
            return r.SaveData( m );
        }

        struct ParsedPath
        {
            public readonly string Path;
            public readonly string FolderName;
            public readonly DateTime Time;
            public readonly string FolderNameRemainder;
            public readonly bool IsTimedDir;

            internal ParsedPath( string p )
            {
                Path = p;
                FolderName = System.IO.Path.GetFileName( p );
                IsTimedDir = InfoReleaseDatabase.TryParseFolderName( FolderName, out Time, out FolderNameRemainder );
            }
        }


    }

}
