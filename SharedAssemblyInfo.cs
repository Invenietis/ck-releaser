#region LGPL License
/*----------------------------------------------------------------------------
* This file (SharedAssemblyInfo.cs) is part of CiviKey. 
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
using System.Reflection;

// Shared Assembly info: it must be added as a link to
// each project.
// It contains information that must be the same for all
// assemblies produced by this solution.


[assembly: AssemblyCompany( "Invenietis" )]
[assembly: AssemblyProduct( "CK-Releaser" )]
[assembly: AssemblyCopyright( "Copyright © Invenietis 2014" )]
[assembly: AssemblyTrademark( "" )]
[assembly: CLSCompliant( true )]

// In simple versioning mode, the version is centralized here.
[assembly: AssemblyVersion( "0.1.0" )]
//[assembly: AssemblyInformationalVersion( "%semver% - %githash%" )]
[assembly: AssemblyInformationalVersion( "%ck-standard%" )]

#if DEBUG
    [assembly: AssemblyConfiguration("Debug")]
#else
    [assembly: AssemblyConfiguration( "Release" )]
#endif
