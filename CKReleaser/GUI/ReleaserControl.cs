#region LGPL License
/*----------------------------------------------------------------------------
* This file (CKReleaser\GUI\ReleaserControl.cs) is part of CiviKey. 
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
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CK.Releaser
{
    class ReleaserControl : UserControl
    {
        IInteractiveDevContext _ctx;

        public virtual void Initialize( IInteractiveDevContext ctx )
        {
            if( _ctx != null ) throw new InvalidOperationException();
            _ctx = ctx;
            _ctx.Refreshed += ctx_Refreshed;
            foreach( Control c in Controls )
            {
                ReleaserControl rc = c as ReleaserControl;
                if( rc != null ) rc.Initialize( ctx );
            }
        }

        void ctx_Refreshed( object sender, EventArgs e )
        {
            OnDevContextRefreshed();
        }

        public IInteractiveDevContext DevContext
        {
            get { return _ctx; }
        }

        protected virtual void OnDevContextRefreshed()
        {
        }

    }
}
