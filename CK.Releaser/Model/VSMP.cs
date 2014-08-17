#region LGPL License
/*----------------------------------------------------------------------------
* This file (CK.Releaser\Model\VSMP.cs) is part of CiviKey. 
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
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CK.Releaser
{
    public class VSMP
    {
        public static readonly XNamespace MSBuild = XNamespace.Get( "http://schemas.microsoft.com/developer/msbuild/2003" );
        public static readonly XName Project = MSBuild + "Project";
        public static readonly XName PropertyGroup = MSBuild + "PropertyGroup";
        public static readonly XName ProjectGuid = MSBuild + "ProjectGuid";
        public static readonly XName OutputType = MSBuild + "OutputType";
        public static readonly XName TargetFrameworkVersion = MSBuild + "TargetFrameworkVersion";
        public static readonly XName AppDesignerFolder = MSBuild + "AppDesignerFolder";

        public static readonly XName PublishUrl = MSBuild + "PublishUrl";
        public static readonly XName ApplicationVersion = MSBuild + "ApplicationVersion";

        public static readonly XName SignAssembly = MSBuild + "SignAssembly";
        public static readonly XName AssemblyOriginatorKeyFile = MSBuild + "AssemblyOriginatorKeyFile";
        public static readonly XName AssemblyName = MSBuild + "AssemblyName";

        public static readonly XName Import = MSBuild + "Import";
        public static readonly XName ItemGroup = MSBuild + "ItemGroup";
        public static readonly XName Compile = MSBuild + "Compile";
        public static readonly XName Link = MSBuild + "Link";

        public static readonly XName ProjectAttribute = XNamespace.None + "Project";
        public static readonly XName IncludeAttribute = XNamespace.None + "Include";
        
        public static readonly XName Reference = MSBuild + "Reference";
        public static readonly XName ProjectReference = MSBuild + "ProjectReference";
        public static readonly XName Name = MSBuild + "Name";
        public static readonly XName SpecificVersion = MSBuild + "SpecificVersion";
        public static readonly XName HintPath = MSBuild + "HintPath";

    }
}
