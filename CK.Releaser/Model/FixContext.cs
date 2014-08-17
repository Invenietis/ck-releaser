#region LGPL License
/*----------------------------------------------------------------------------
* This file (CK.Releaser\Model\FixContext.cs) is part of CiviKey. 
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
    public class FixContext
    {
        public readonly ValidationContext ValidationContext;
        readonly IActivityMonitor _monitor;
        readonly IDevContext _ctx;
        readonly FileSystemChangeManager _fileManager;


        internal FixContext( ValidationContext validationContext, IActivityMonitor m, IDevContext ctx )
        {
            ValidationContext = validationContext;
            _monitor = m;
            _ctx = ctx;
            _fileManager = new FileSystemChangeManager();
        }

        public IActivityMonitor Monitor
        {
            get { return _monitor; }
        }

        public IDevContext DevContext
        {
            get { return _ctx; }
        } 

        public void RenameFileOrFolder( string from, string to )
        {
            if( from == null ) throw new ArgumentNullException( "from" );
            if( from.Contains( Path.AltDirectorySeparatorChar ) ) throw new ArgumentException( String.Format( "From path '{0}' must be normalized.", from ), "from" );
            if( !Path.IsPathRooted( from ) ) throw new ArgumentException( String.Format( "From path '{0}' must be rooted.", from ), "from" );
            if( to == null ) throw new ArgumentNullException( "to" );
            if( to.Contains( Path.AltDirectorySeparatorChar ) ) throw new ArgumentException( String.Format( "To path '{0}' must be normalized.", to ), "to" );
            if( !Path.IsPathRooted( to ) ) throw new ArgumentException( String.Format( "To path '{0}' must be rooted.", to ), "to" );
            if( (from[from.Length - 1] == Path.DirectorySeparatorChar) != (to[to.Length - 1] == Path.DirectorySeparatorChar) )
            {
                throw new ArgumentException( String.Format( "Can not rename a file to/from a directory ('{0}' and '{1}').", from, to ), "to" );
            }
            _fileManager.Rename( from, to );
        }
    
        public int RenamingCount
        {
            get { return _fileManager.ChangeCount; }
        }

        public bool ApplyRenaming( IActivityMonitor m = null )
        {
            if( m == null ) m = _monitor;
            return _fileManager.ApplyChanges( m, _ctx.Workspace );
        }
    }
}
