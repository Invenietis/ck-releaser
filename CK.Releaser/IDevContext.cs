#region LGPL License
/*----------------------------------------------------------------------------
* This file (CK.Releaser\IDevContext.cs) is part of CiviKey. 
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
using CK.Releaser.Info;

namespace CK.Releaser
{
    public interface IDevContext
    {
        /// <summary>
        /// Gets the initial root (the first <see cref="Workspace"/>.<see cref="Workspace.SolutionPath"/>).
        /// </summary>
        string InitialRoot { get; }

        /// <summary>
        /// Initializes this context.
        /// This method should be called on new or refreshed context prior to working with it.
        /// </summary>
        /// <param name="ctx">A new instance of a <see cref="ValidationContext"/>.</param>
        /// <returns>True on succes. False if any error occurred.</returns>
        bool Initialize( ValidationContext ctx );

        /// <summary>
        /// Gets the <see cref="Workspace"/> object.
        /// </summary>
        Workspace Workspace { get; }

        /// <summary>
        /// Gets the Git manager for the current path.
        /// Null if no repository found, closed if an error occurred while opening it.
        /// </summary>
        GitManager GitManager { get; }

        /// <summary>
        /// Gets the InfoReleaseDatabase for the current path context.
        /// Null if no InfoReleaseDatabase found.
        /// </summary>
        InfoReleaseDatabase InfoReleaseDatabase { get; }

        /// <summary>
        /// Refreshes this context.
        /// </summary>
        /// <param name="monitor">Monitor to use.</param>
        /// <param name="newSolutionFolder">New solution folder. Defaults to <see cref="InitialRoot"/>.</param>
        /// <returns>True on succes. False if any error occurred (this object is left untouched).</returns>
        bool Refresh( IActivityMonitor monitor, string newSolutionFolder = null );


    }
}
