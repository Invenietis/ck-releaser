#region LGPL License
/*----------------------------------------------------------------------------
* This file (CK.Releaser\Model\ValidationContext.cs) is part of CiviKey. 
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
using CK.Core;
using Mono.Cecil;

namespace CK.Releaser
{
    public class ValidationContext
    {
        public static readonly CKTrait ContextProcessingTag = ActivityMonitor.Tags.Context.FindOrCreate( "ContextProcessing" );

        readonly IActivityMonitor _monitor;
        readonly List<FixBase> _fixes;
        HashSet<string> _disabledFixes;
        Dictionary<string,AssemblyDefinition> _cachedAssemblies;

        public ValidationContext( IActivityMonitor m )
        {
            _monitor = m;
            _fixes = new List<FixBase>();
            _cachedAssemblies = new Dictionary<string, AssemblyDefinition>();
        }

        public IActivityMonitor Monitor
        {
            get { return _monitor; }
        }

        public void SetDisabledFixes( HashSet<string> disabledFixes )
        {
            _disabledFixes = disabledFixes;
        }

        public void Add( FixBase fix )
        {
            fix.IsDisabled = _disabledFixes.Contains( fix.MemoryKey );
            if( fix.IsDisabled )
            {
                _monitor.Info().Send( "A fix \"{0}\" is available but it is disabled.", fix.Title );
            }
            else
            {
                _monitor.Info().Send( "A fix \"{0}\" is available!", fix.Title );
            }
            _fixes.Add( fix );
        }

        [CLSCompliant(false)]
        public AssemblyDefinition GetAssembly( IActivityMonitor m, string path )
        {
            AssemblyDefinition d;
            if( !_cachedAssemblies.TryGetValue( path, out d ) )
            {
                try
                {
                    d = AssemblyDefinition.ReadAssembly( path );
                    _cachedAssemblies[path] = d;
                }
                catch( Exception ex )
                {
                    m.Error().Send( ex, "Unable to read Assembly file: '{0}'.", path );
                }
            }
            return d;
        }

        public IReadOnlyList<FixBase> Fixes
        {
            get { return _fixes; }
        }

        public bool ApplyFixes( IDevContext ctx, out int enabledFixCount, out int disabledFixCount, IActivityMonitor m = null )
        {
            enabledFixCount = disabledFixCount = 0;
            if( m == null ) m = _monitor;
            bool isValid = true;
            using( m.CatchCounter( _ => isValid = false ) )
            {
                FixContext fC = new FixContext( this, m, ctx );
                using( m.OpenInfo().Send( "Applying fixes." ) )
                {
                    foreach( var f in _fixes )
                    {
                        if( !f.IsDisabled )
                        {
                            ++enabledFixCount;
                            using( m.OpenInfo().Send( ValidationContext.ContextProcessingTag, f.FixScopeName ) )
                            {
                                m.Info().Send( "Attempt to apply fix '{0}'.", f.Title );
                                try
                                {
                                    f.Run( fC );
                                }
                                catch( Exception ex )
                                {
                                    m.Error().Send( ex );
                                }
                            }
                        }
                        else ++disabledFixCount;
                    }
                }
                ctx.Workspace.SolutionCKFile.SetDisabledFixes( _fixes.Where( f => f.IsDisabled ).Select( f => f.MemoryKey ) );
                ctx.Workspace.SaveDirtyFiles( m );
                if( fC.RenamingCount > 0 )
                {
                    using( m.OpenInfo().Send( ValidationContext.ContextProcessingTag, "Renaming {0} files and folder.", fC.RenamingCount ) )
                    {
                        fC.ApplyRenaming();
                    }
                }
            }
            return isValid;
        }

    }
}
