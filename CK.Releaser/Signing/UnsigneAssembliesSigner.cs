#region LGPL License
/*----------------------------------------------------------------------------
* This file (CK.Releaser\Signing\UnsigneAssembliesSigner.cs) is part of CiviKey. 
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
using Mono.Cecil;

namespace CK.Releaser.Signing
{
    static class UnsigneAssembliesSigner
    {
        public static void SignUnsignedAssemblies( IActivityMonitor m, IEnumerable<Tuple<string, AssemblyDefinition>> toSign )
        {
            foreach( var s in toSign )
            {
                AssemblyDefinition assembly = s.Item2;
                if( assembly.Name.PublicKeyToken == null || assembly.Name.PublicKeyToken.Length == 0 )
                {
                    StrongNameSigner.StrongName( assembly, KnownStrongNames.SharedKey );
                    assembly.Write( s.Item1, new WriterParameters { StrongNameKeyPair = KnownStrongNames.SharedKey } );
                }
            }
        }

    }
}
