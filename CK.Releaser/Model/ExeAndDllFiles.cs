#region LGPL License
/*----------------------------------------------------------------------------
* This file (CK.Releaser\Model\ExeAndDllFiles.cs) is part of CiviKey. 
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
    public class ExeAndDllFiles
    {
        public readonly Workspace Solution;

        public class File
        {
            public File( ExeAndDllFiles holder, string p, bool isOutput )
            {
                IsOutput = isOutput;
                SolutionFilePath = FileUtil.NormalizePathSeparator( p.Substring( holder.Solution.WorkspacePath.Length ), false );
            }

            public readonly bool IsOutput;
            public readonly string SolutionFilePath; 
        }

        IReadOnlyList<File> _files;
        IReadOnlyList<File> _outputs;

        internal ExeAndDllFiles( Workspace s )
        {
            Solution = s;
        }

        internal void SetFiles( IReadOnlyList<string> filePaths, IReadOnlyList<CSProjectObject> projects )
        {
            var assemblyNames = new HashSet<string>( projects.Select( p => p.AssemblyName ) );
            _files = filePaths.Select( p => new File( this, p, assemblyNames.Contains( Path.GetFileNameWithoutExtension( p ) ) ) ).ToArray();
            _outputs = _files.Where( f => f.IsOutput ).ToArray();
        }

        /// <summary>
        /// Gets all .exe and .dll files in the solution folder. Among them, <see cref="Outputs"/> are the one whose name
        /// is one assembly name of the projects.
        /// </summary>
        public IReadOnlyList<File> Files
        {
            get { return _files; }
        }

        /// <summary>
        /// Gets .exe and .dll files that <see cref="File.Origin"/> is not null.
        /// </summary>
        public IReadOnlyList<File> Outputs
        {
            get { return _outputs; }
        }

    }
}
