#region LGPL License
/*----------------------------------------------------------------------------
* This file (CK.Releaser\Model\VSProjectBase.cs) is part of CiviKey. 
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
using CK.Core;

namespace CK.Releaser
{
    public abstract class VSProjectBase : IFile
    {
        public readonly Workspace Workspace;

        /// <summary>
        /// This folder path.
        /// </summary>
        public readonly string ThisDirectoryPath;

        /// <summary>
        /// Full path of the ProjectFileName.csproj.
        /// </summary>
        public readonly string ProjectFilePath;

        /// <summary>
        /// Name of the .csproj without the extension.
        /// This is the "main" name, the one that drives the AssemblyName and the name of the folder.
        /// </summary>
        public readonly string ProjectFileName;

        /// <summary>
        /// Intermediate directories from the root solution folder to this project.
        /// </summary>
        public readonly IReadOnlyList<string> SubDirectories;

        protected VSProjectBase( Workspace w, string filePath )
	    {
            Workspace = w;
            ProjectFilePath = FileUtil.NormalizePathSeparator( filePath, false );
            ProjectFileName = Path.GetFileNameWithoutExtension( ProjectFilePath );
            ThisDirectoryPath = ProjectFilePath.Substring( 0, ProjectFilePath.LastIndexOf( Path.DirectorySeparatorChar ) + 1 );
            SubDirectories = ThisDirectoryPath.Substring( w.WorkspacePath.Length ).Split( '\\' ).Where( n => n.Length > 0 ).ToArray();
        }

        public string WorkspaceBasedPath
        {
            get { return ProjectFilePath.Substring( Workspace.WorkspacePath.Length ); }
        }

        public abstract bool IsDirty { get; }

        public abstract void Save( IActivityMonitor m );

    }
}
