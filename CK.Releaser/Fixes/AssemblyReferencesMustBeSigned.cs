#region LGPL License
/*----------------------------------------------------------------------------
* This file (CK.Releaser\Fixes\AssemblyReferencesMustBeSigned.cs) is part of CiviKey. 
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
using CK.Releaser.Signing;
using Mono.Cecil;

namespace CK.Releaser.Fixes
{
    public class AssemblyReferencesMustBeSigned : FixProjectBase
    {
        public AssemblyReferencesMustBeSigned( CSProjectObject project )
            : base( project )
        {
        }

        public override string Title
        {
            get { return String.Format( "Signs the referenced assemblies with the {1} in {0}.csproj.", Project.ProjectFileName, CKMP.SharedKeyFileName ); }
        }

        public override string MemoryKey
        {
            get { return String.Format( "Assemblies that '{0}' references must be signed.", Project.ProjectFileName ); }
        }

        public override void Run( FixContext ctx )
        {
            var u = Project.GetUnsignedReferencedAssembliesWithHintPathResolved( ctx.Monitor );
            List<Tuple<string,AssemblyDefinition>> toSign = new List<Tuple<string, AssemblyDefinition>>();
            foreach( var uref in u )
            {
                var path = uref.GetHintPathResolved( ctx.Monitor ).FullName;
                AssemblyDefinition d = ctx.ValidationContext.GetAssembly( ctx.Monitor, path );
                if( d != null && d.Name.PublicKeyToken == null || d.Name.PublicKeyToken.Length == 0 )
                {
                    toSign.Add( Tuple.Create( path, d ) );
                }
            }
            UnsigneAssembliesSigner.SignUnsignedAssemblies( ctx.Monitor, toSign );
        }

    }
}
