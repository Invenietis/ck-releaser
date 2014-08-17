#region LGPL License
/*----------------------------------------------------------------------------
* This file (CK.Releaser\Fixes\SharedAssemblyInfoMustBeReferenced.cs) is part of CiviKey. 
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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CK.Core;
using System.Xml.Linq;

namespace CK.Releaser.Fixes
{
    public class SharedAssemblyInfoMustBeReferenced : FixProjectBase
    {
        public SharedAssemblyInfoMustBeReferenced( CSProjectObject project )
            : base( project )
        {
        }

        public override string Title
        {
            get { return String.Format( "Adds a link (an alias) to {1} in {0}.csproj.", Project.ProjectFileName, CKMP.SharedAssemblyInfoFileName ); }
        }

        public override string MemoryKey
        {
            get { return String.Format( "Assembly '{0}' must reference Shared assembly info.", Project.ProjectFileName ); }
        }

        public override void Run( FixContext ctx )
        {
            Project.SharedAssemblyInfoCompileElements.Remove();
            var eCompile = new XElement( VSMP.Compile, new XAttribute( VSMP.IncludeAttribute, Project.ExpectedRelativePathToSolutionSharedAssemblyInfo ),
                                    new XElement(VSMP.Link, Project.DesignerFolderPrefix( CKMP.SharedAssemblyInfoFileName ) ) );

            Project.InsertAfterFirstItemGroup( new XElement( VSMP.ItemGroup, eCompile ) );
        }
    }
}
