#region LGPL License
/*----------------------------------------------------------------------------
* This file (CK.Releaser\Model\VSSolutionObject.cs) is part of CiviKey. 
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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CK.Core;

namespace CK.Releaser
{
    public class VSSolutionObject : IFile
    {
        static Regex _rProject = new Regex( @"Project\(\s*""(?<1>.*?)""\)\s*=\s*""(?<2>.*?)""\s*,\s*""(?<3>.*?)"",\s*""(?<4>.*?)""\s+(?<5>ProjectSection\s*\(\s*(?<6>\w+)\s*\).*?EndProjectSection\s+)?EndProject",
                                                    RegexOptions.ExplicitCapture
                                                    | RegexOptions.CultureInvariant
                                                    | RegexOptions.Singleline );

        public readonly Workspace Workspace;

        /// <summary>
        /// Full path of the SolutionFileName.sln.
        /// </summary>
        public readonly string SolutionFilePath;

        /// <summary>
        /// This folder path.
        /// </summary>
        public readonly string ThisDirectoryPath;

        /// <summary>
        /// Name of the .sln without the extension.
        /// </summary>
        public readonly string SolutionFileName;

        /// <summary>
        /// Intermediate directories from the root solution folder to this .sln.
        /// </summary>
        public readonly IReadOnlyList<string> SubDirectories;

        public class OriginalProject
        {
            /// <summary>
            /// Project type identifier.
            /// </summary>
            public readonly Guid ProjectTypeId;
            /// <summary>
            /// Name of the project.
            /// </summary>
            public readonly string ProjectName;
            /// <summary>
            /// Path to the project file (.csproj) relative to this solution.sln
            /// (as it appears in the .sln).
            /// </summary>
            public readonly string ProjectFilePath;
            /// <summary>
            /// Guid of the project.
            /// </summary>
            public readonly Guid ProjectId;
            /// <summary>
            /// Full text "ProjectSection(ProjectSectionName)...EndProjectSection".
            /// Empty string if no ProjectSection exists.
            /// </summary>
            public readonly string FullProjectSection;
            /// <summary>
            /// Name of the ProjectSection.
            /// Empty string if no ProjectSection exists.
            /// </summary>
            public readonly string ProjectSectionName;

            public OriginalProject( Match m )
            {
                ProjectTypeId = Guid.Parse( m.Groups[1].Value );
                ProjectName = m.Groups[2].Value;
                ProjectFilePath = m.Groups[3].Value;
                ProjectId = Guid.Parse( m.Groups[4].Value );
                FullProjectSection = m.Groups[5].Value;
                ProjectSectionName = m.Groups[6].Value;
            }
        }

        string _text;
        int _firstProjectIndex;
        IReadOnlyList<OriginalProject> _originalProjects;
        string _textNoProjects;

        internal VSSolutionObject( Workspace w, string filePath )
        {
            Workspace = w;
            SolutionFilePath = FileUtil.NormalizePathSeparator( filePath, false );
            SolutionFileName = Path.GetFileNameWithoutExtension( SolutionFilePath );
            ThisDirectoryPath = SolutionFilePath.Substring( 0, SolutionFilePath.LastIndexOf( Path.DirectorySeparatorChar ) + 1 );
            SubDirectories = ThisDirectoryPath.Substring( w.WorkspacePath.Length ).Split( '\\' ).Where( n => n.Length > 0 ).ToArray();
        }

        public IReadOnlyList<OriginalProject> OriginalProjects
        {
            get
            {
                if( _originalProjects == null )
                {
                    _text = File.ReadAllText( SolutionFilePath );
                    StringBuilder clearedText = new StringBuilder( _text );
                    _firstProjectIndex = -1;
                    var projects = new List<OriginalProject>();
                    var m = _rProject.Match( _text );
                    int removedOffset = 0;
                    while( m.Success )
                    {
                        if( _firstProjectIndex == -1 ) _firstProjectIndex = m.Index;
                        projects.Add( new OriginalProject( m ) );
                        clearedText.Remove( m.Index - removedOffset, m.Length );
                        removedOffset += m.Length;
                        m = m.NextMatch();
                    }
                    _textNoProjects = clearedText.ToString();
                    _originalProjects = projects.ToArray();
                }
                return _originalProjects;
            }
        }

        class Project
        {
            VSSolutionObject _solution;
            Guid _projectTypeId;
            string _projectName;
            string _projectFileFullPath;
            Guid _projectId;
            string _fullProjectSection;
            string _projectSectionName;
            
            public Project( VSSolutionObject s, OriginalProject p )
            {
                _solution = s;
                _projectTypeId = p.ProjectTypeId;
                _projectName = p.ProjectName;
                _projectFileFullPath = FileUtil.NormalizePathSeparator( Path.Combine( s.ThisDirectoryPath, p.ProjectFilePath ), false );
                _projectId = p.ProjectId;
                _fullProjectSection = p.FullProjectSection;
                _projectSectionName = p.ProjectSectionName;
            }
        }
        List<Project> _projects;

        List<Project> EnsureProjects()
        {
            if( _projects == null )
            {
                _projects = OriginalProjects.Select( p => new Project( this, p ) ).ToList();
            }
            return _projects;
        }

        public bool IsDirty
        {
            get { return false; }
        }

        public string WorkspaceBasedPath
        {
            get { return SolutionFilePath.Substring( Workspace.WorkspacePath.Length ); }
        }

        public void Save( IActivityMonitor m )
        {
        }
    }
}
