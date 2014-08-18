#region LGPL License
/*----------------------------------------------------------------------------
* This file (CK.Releaser\Info\InfoReleaseDatabase.cs) is part of CiviKey. 
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
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CK.Core;

namespace CK.Releaser.Info
{
    public partial class InfoReleaseDatabase
    {
        /// <summary>
        /// Full path of this database.
        /// </summary>
        public readonly string DatabasePath;

        /// <summary>
        /// Name of the database (last <see cref="DatabasePath"/> folder's name).
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Unique identifier for this database (stored in "CK-InfoReleaseKey.txt" file).
        /// </summary>
        public readonly Guid DatabaseId;

        /// <summary>
        /// Internal base class for <see cref="BranchRelease"/>, <see cref="InfoRelease"/> and <see cref="WorkingFolder"/>.
        /// </summary>
        public class DBObject
        {
            internal readonly string KeyId;

            internal DBObject( string keyId )
            {
                KeyId = keyId;
            }
        }

        readonly Dictionary<string,DBObject> _instances;
        readonly StringBuilder _keyBuilder;

        const string _fileNameKey = "CK-InfoReleaseKey.txt";

        /// <summary>
        /// The display format for <see cref="DateTime"/> is: @"yyyy-MM-dd HH\hmm.ss"
        /// </summary>
        public static readonly string TimeFormat = @"yyyy-MM-dd HH\hmm.ss";

        /// <summary>
        /// Lists all branches that exist in a solution as <see cref="BranchRelease"/>.
        /// </summary>
        /// <param name="solutionName">The solution name.</param>
        /// <returns>An empty list if none exist.</returns>
        public IReadOnlyList<BranchRelease> GetExistingBranches( string solutionName )
        {
            string solutionPath = Path.Combine( DatabasePath, "Solutions", solutionName );
            if( !Directory.Exists( solutionPath ) ) return CKReadOnlyListEmpty<BranchRelease>.Empty;
            return Directory.EnumerateDirectories( solutionPath )
                            .Select( d => FindOrCreateBranchRelease( solutionName, Path.GetFileName( d ), d ) )
                            .ToArray();
        }

        /// <summary>
        /// Ensures that a branch exists: if the directory does not exist, creates it and 
        /// sets the PreRelease version to the branch name.
        /// If an error occurs, returns null.
        /// </summary>
        /// <param name="solutionName">The solution name.</param>
        /// <param name="branchName">The branch name.</param>
        /// <returns>A <see cref="BranchRelease"/> or null if an error occurred.</returns>
        public BranchRelease EnsureBranch( IActivityMonitor monitor, string solutionName, string branchName )
        {
            string path = Path.Combine( DatabasePath, "Solutions", solutionName, branchName );
            try
            {
                DirectoryInfo d = new DirectoryInfo( path );
                bool exists = d.Exists;
                if( !exists ) d.Create();
                var b = FindOrCreateBranchRelease( solutionName, branchName, d.FullName );
                if( !exists )
                {
                    var data = b.Current.GetData( monitor );
                    if( data != null )
                    {
                        data.PreReleaseVersion = branchName;
                        if( !b.Current.SaveData( monitor ) ) return null;
                    }
                }
                return b;
            }
            catch( Exception ex )
            {
                monitor.Error().Send( ex, "While creating branch '{0}'.", path );
                return null;
            }
        }

        /// <summary>
        /// Finds an existing branch.
        /// </summary>
        /// <param name="solutionName">The solution name.</param>
        /// <param name="branchName">The branch name.</param>
        /// <returns>A <see cref="BranchRelease"/> or null if not found.</returns>
        public BranchRelease FindBranch( string solutionName, string branchName )
        {
            DirectoryInfo d = new DirectoryInfo( Path.Combine( DatabasePath, "Solutions", solutionName, branchName ) );
            if( !d.Exists ) return null;
            return FindOrCreateBranchRelease( solutionName, branchName, d.FullName );
        }

        /// <summary>
        /// Finds a <see cref="InfoRelease"/> from its commit sha1.
        /// </summary>
        /// <param name="monitor">ActivityMonitor to use.</param>
        /// <param name="commitSha">The sha1 of the commit.</param>
        /// <returns>Null if not found.</returns>
        public InfoRelease FindByCommit( IActivityMonitor monitor, string commitSha )
        {
            string solutions = Path.Combine( DatabasePath, "Solutions" );
            foreach( var s in Directory.EnumerateDirectories( solutions ) )
            {
                string name = Path.GetFileName( s );
                foreach( var b in GetExistingBranches( name ) )
                {
                    foreach( var r in b.GetPastInfoReleases( monitor ) )
                    {
                        var data = r.GetData( monitor );
                        if( data != null )
                        {
                            if( data.CommitSha == commitSha ) return r;
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Gets a snapshot (a copy) of the <see cref="WorkingFolder"/> that are opened.
        /// </summary>
        public IReadOnlyList<WorkingFolder> OpenedWorkingFolders
        {
            get { return _instances.Values.OfType<WorkingFolder>().Where( w => w.IsOpen ).ToList(); }
        }

        BranchRelease FindOrCreateBranchRelease( string solutionName, string branchName, string path )
        {
            _keyBuilder.Clear().Append( solutionName ).Append( ':' ).Append( branchName );
            string key = _keyBuilder.ToString();
            DBObject o;
            if( !_instances.TryGetValue( key, out o ) )
            {
                var b = new BranchRelease( key, this, solutionName, branchName, path );
                _instances.Add( key, b );
                return b;
            }
            return (BranchRelease)o;
        }

        internal InfoRelease FindInfoRelease( BranchRelease branch, DateTime time )
        {
            string standardTimeDisplay = time != Util.UtcMinValue ? time.ToString( TimeFormat ) : String.Empty;
            _keyBuilder.Clear().Append( branch.KeyId ).Append( '/' ).Append( standardTimeDisplay );
            return (InfoRelease)_instances.GetValueWithDefault( _keyBuilder.ToString(), null );
        }

        internal InfoRelease FindOrCreateInfoRelease( BranchRelease branch, DateTime time, string path )
        {
            string standardTimeDisplay = time != Util.UtcMinValue ? time.ToString( TimeFormat ) : String.Empty;
            _keyBuilder.Clear().Append( branch.KeyId ).Append( '/' ).Append( standardTimeDisplay );
            string key = _keyBuilder.ToString();
            DBObject o;
            if( !_instances.TryGetValue( key, out o ) )
            {
                var r = new InfoRelease( key, branch, time, standardTimeDisplay, path );
                _instances.Add( key, r );
                return r;
            }
            return (InfoRelease)o;
        }

        internal WorkingFolder FindWorkingFolder( BranchRelease branch, string standardTimeDisplay )
        {
            _keyBuilder.Clear().Append( branch.KeyId ).Append( '?' ).Append( standardTimeDisplay );
            return (WorkingFolder)_instances.GetValueWithDefault( _keyBuilder.ToString(), null );
        }

        internal WorkingFolder FindOrCreateWorkingFolder( IActivityMonitor m, BranchRelease branch, DirectoryInfo baseDir, DateTime time )
        {
            string standardTimeDisplay = time != Util.UtcMinValue ? time.ToString( TimeFormat ) : String.Empty;
            _keyBuilder.Clear().Append( branch.KeyId ).Append( '?' ).Append( standardTimeDisplay );
            string key = _keyBuilder.ToString();
            DBObject o;
            if( !_instances.TryGetValue( key, out o ) )
            {
                DirectoryInfo tmp = CreateAndFillTempDirectory( m, baseDir );
                if( tmp == null ) return null;
                var w = new WorkingFolder( key, branch, baseDir, tmp, time );
                _instances.Add( key, w );
                return w;
            }
            var existing = (WorkingFolder)o;
            if( !existing.IsOpen )
            {
                DirectoryInfo tmp = CreateAndFillTempDirectory( m, baseDir );
                if( tmp == null ) return null;
                existing.ReOpen( tmp );
            }
            return existing;
        }

        static DirectoryInfo CreateAndFillTempDirectory( IActivityMonitor m, DirectoryInfo baseDir )
        {
            DirectoryInfo tmp = Directory.CreateDirectory( System.IO.Path.Combine( System.IO.Path.GetTempPath(), Guid.NewGuid().ToString() ) );
            if( baseDir.Exists )
            {
                if( CopyContentTo( m, baseDir, tmp ) == null )
                {
                    try { tmp.Delete(); }
                    catch { }
                    return null;
                }
            }
            return tmp;
        }

        /// <summary>
        /// Returns sourceDir object on success, null on error.
        /// The target directory is created if it does not exist.
        /// </summary>
        internal static DirectoryInfo CopyContentTo( IActivityMonitor m, DirectoryInfo sourceDir, DirectoryInfo target, bool withInfoReleaseFile = true )
        {
            try
            {
                Func<FileInfo,bool> filter = null;
                if( !withInfoReleaseFile ) filter = IsNormalFile; 
                FileUtil.CopyDirectory( sourceDir, target, true, true, filter, IsContentFolder );
                return sourceDir;
            }
            catch( Exception ex )
            {
                m.Error().Send( ex, "While copying from: '{0}' to '{1}'.", sourceDir.FullName, target.FullName );
                return null;
            }
        }

        static bool IsContentFolder( DirectoryInfo d )
        {
            return d.Name != BranchRelease.HistoricFolderName;
        }

        static bool IsNormalFile( FileInfo f )
        {
            return f.Name != InfoRelease.ReleaseInfoFileName;
        }

        internal static bool TryParseFolderName( string s, out DateTime time, out string remainder )
        {
            remainder = null;
            time = Util.UtcMinValue;
            Debug.Assert( TimeFormat.Replace( "\\", "" ).Length == 19 );
            if( s.Length < 19 ) return false;
            if( s.Length > 19 )
            {
                remainder = s.Substring( 19 );
                s = s.Remove( 19 );
            }
            return DateTime.TryParseExact( s, TimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out time );
        }

        internal static string FormatFolderName( DateTime t )
        {
            return t.ToString( TimeFormat );
        }

    }
}
