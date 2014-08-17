#region LGPL License
/*----------------------------------------------------------------------------
* This file (CK.Releaser\Info\InfoRelease.cs) is part of CiviKey. 
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
using System.Xml;
using System.Xml.Linq;
using CK.Core;

namespace CK.Releaser.Info
{
    /// <summary>
    /// An InfoRelease belongs to a branch.
    /// </summary>
    public partial class InfoRelease : InfoReleaseDatabase.DBObject
    {
        internal static readonly string ReleaseInfoFileName = ".infoRelease.ck";
        internal static readonly string ReleaseInfoFileNameSuffix = System.IO.Path.DirectorySeparatorChar + ReleaseInfoFileName;

        public readonly BranchRelease Branch;
        /// <summary>
        /// The time associated to this release info.
        /// <see cref="Util.UtcMinValue"/> for the current information.
        /// </summary>
        public readonly DateTime UtcTime;

        /// <summary>
        /// The <see cref="UtcTime"/> with the standard <see cref="InfoReleaseDatabase.TimeFormat"/>.
        /// It is the empty string for the current information.
        /// </summary>
        public readonly string StandardTimeDisplay;
        
        public readonly string Path;

        DataInfo _data;
        XElement _content;
        WorkingFolder _workingFolder;

        internal InfoRelease( string keyId, BranchRelease branch, DateTime time, string standardTimeDisplay, string path )
            : base( keyId )
        {
            Debug.Assert( (standardTimeDisplay.Length == 0) == (time == Util.UtcMinValue) );
            Branch = branch;
            UtcTime = time;
            StandardTimeDisplay = standardTimeDisplay;
            Path = path;
        }

        /// <summary>
        /// Gets whether this is the current information (the one at the branch level).
        /// </summary>
        public bool IsCurrent
        {
            get { return StandardTimeDisplay.Length == 0; }
        }


        /// <summary>
        /// Copies the content to the targetPath. It is created if it does not exist.
        /// </summary>
        /// <param name="monitor">Required monitor.</param>
        /// <param name="targetPath">Path of the target directory.</param>
        /// <returns>True on success, false on error.</returns>
        public bool CopyContentTo( IActivityMonitor monitor, string targetPath )
        {
            if( monitor == null ) throw new ArgumentNullException( "monitor" );
            if( targetPath == null ) throw new ArgumentNullException( "targetPath" );
            return InfoReleaseDatabase.CopyContentTo( monitor, new DirectoryInfo( Path ), new DirectoryInfo( targetPath ), withInfoReleaseFile: false ) != null;
        }

        /// <summary>
        /// Gets the opened working folder for this <see cref="InfoRelease"/>.
        /// Null if it does not exist or is closed.
        /// </summary>
        /// <returns>Null if the working folder is closed.</returns>
        public WorkingFolder GetOpenedWorkingFolder()
        {
            if( _workingFolder == null )
            {
                _workingFolder = Branch.Database.FindWorkingFolder( Branch, StandardTimeDisplay );
                if( _workingFolder != null ) _workingFolder.InfoRelease = this;
            }
            return _workingFolder != null && _workingFolder.IsOpen ? _workingFolder : null;
        }

        /// <summary>
        /// Obtains an opened working folder for this <see cref="InfoRelease"/>.
        /// </summary>
        /// <param name="m"></param>
        /// <returns>Null if an error occured.</returns>
        public WorkingFolder EnsureWorkingFolder( IActivityMonitor m )
        {
            DirectoryInfo baseDir = new DirectoryInfo( Path );
            return _workingFolder = Branch.Database.FindOrCreateWorkingFolder( m, Branch, baseDir, UtcTime );
        }

        /// <summary>
        /// Returns a DirectoryInfo object for this info release or null on error.
        /// </summary>
        internal DirectoryInfo CopyContentTo( IActivityMonitor m, DirectoryInfo target )
        {
            return InfoReleaseDatabase.CopyContentTo( m, new DirectoryInfo( Path ), target );
        }

        internal void OnWorkingFolderWritten( WorkingFolder f )
        {
            Debug.Assert( _workingFolder == null || _workingFolder == f );
            _workingFolder = f;
            _content = null;
        }

    }
}
