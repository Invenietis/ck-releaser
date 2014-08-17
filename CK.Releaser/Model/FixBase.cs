#region LGPL License
/*----------------------------------------------------------------------------
* This file (CK.Releaser\Model\FixBase.cs) is part of CiviKey. 
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

namespace CK.Releaser
{
    public abstract class FixBase
    {
        bool _disabled;

        public bool IsDisabled
        {
            get { return _disabled; }
            set { _disabled = value; }
        }

        public virtual bool IsSolutionFix
        {
            get { return true; }
        }

        /// <summary>
        /// Gets the scope name. Defaults to "Solution fixes" for solution.
        /// </summary>
        public virtual string FixScopeName
        {
            get { return "Solution fix"; }
        }

        public abstract string Title { get; }

        public abstract string MemoryKey { get; }

        public abstract void Run( FixContext ctx );

    }
}
