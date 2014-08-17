#region LGPL License
/*----------------------------------------------------------------------------
* This file (CK.Releaser\Model\CKMP.cs) is part of CiviKey. 
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
    public class CKMP
    {
        public static readonly XNamespace releaser = XNamespace.Get( "http://invenietis.net/ck-release" );
        public static readonly XName Solution = releaser + "Solution";
        public static readonly XName Fixes = releaser + "Fixes";
        public static readonly XName Version = releaser + "Version";
        public static readonly XName Disabled = releaser + "Disabled";
        public static readonly XName Versioning = releaser + "Versioning";
        public static readonly XName Mode = releaser + "Mode";

        public static readonly XName InfoReleaseDatabase = releaser + "InfoReleaseDatabase";
        public static readonly XName Name = releaser + "Name";
        public static readonly XName GUID = releaser + "GUID";
        
        public static readonly XName Branches = releaser + "Branches";
        public static readonly XName Branch = releaser + "Branch";
        
        public static readonly XName Master = releaser + "Master";
        public static readonly XName Develop = releaser + "Develop";
        public static readonly XName Major = releaser + "Major";
        public static readonly XName Minor = releaser + "Minor";
        public static readonly XName Patch = releaser + "Patch";
        public static readonly XName PreRelease = releaser + "PreRelease";
        
        /// <summary>
        /// "SharedKey.snk"
        /// </summary>
        public const string SharedKeyFileName = "SharedKey.snk";
        /// <summary>
        /// "SharedAssemblyInfo.cs"
        /// </summary>
        public const string SharedAssemblyInfoFileName = "SharedAssemblyInfo.cs";
    }
}
