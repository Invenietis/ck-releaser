#region LGPL License
/*----------------------------------------------------------------------------
* This file (CK.Releaser\Model\ModelExtension.cs) is part of CiviKey. 
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

namespace CK.Releaser
{
    public static class AllExtension
    {
        /// <summary>
        /// Gets whether the working folder is writable.
        /// It is writable if there is no Git repository (<paramref name="this"/> is null) or 
        /// the <see cref="GitManager.CurrentBranchName"/> is null (unitialized repo), if is not the special name "(no branch)"
        /// or if the branch is not a master: a branch that starts with "master" (like "master-product" for example is not writable.
        /// Caution: When <see cref="GitManager.IsOpen"/> is false, since we have no information, we consider that the working folder is writable.
        /// </summary>
        public static bool IsWorkingFolderWritable( this GitManager @this )
        {
            if( @this == null ) return false;
            if( @this.CurrentBranchName == null ) return false;
            if( @this.CurrentBranchName == "(no branch)" ) return false;
            if( @this.CurrentBranchName.StartsWith( "master" ) ) return @this.IsDirty;
            return true;
        }

        /// <summary>
        /// Gets a description that explains why the working folder is writable or not.
        /// </summary>
        /// <param name="this">This <see cref="GitManager"/>.</param>
        /// <returns>A description.</returns>
        public static string IsWorkingFolderWritableDescription( this GitManager @this )
        {
            if( @this == null ) return "This is not .git repository: the working folder is writable.";
            if( @this.CurrentBranchName == null ) return "This .git repository is not initialized: the working folder is writable.";
            if( @this.CurrentBranchName == "(no branch)" ) return "The repository is not on a branch (a detached head): the working folder is not writable.";
            if( @this.CurrentBranchName.StartsWith( "master" ) )
            {
                if( !@this.IsDirty )
                {
                    return "The current branch is a 'master*' branch:\r\nThe working folder is not writable (there should be no direct modification in a master branch).";
                }
                else
                {
                    return "The current branch is a 'master*' branch but files are already modified:\r\nthe working folder is writable but this is bad!\r\n(There should be no direct modification in a master branch.)";
                }
            }
            if( @this.IsDirty )
            {
                return "The working folder is writable (and is already modified).";
            }
            else
            {
                return "The working folder is writable (and is already modified).";
            }
        }

        /// <summary>
        /// Gets the a string that can be <see cref="(No repository)"/> (if <paramref name="this"/> is null), "(unable to open .git repo)" if it is 
        /// closed, "(unitialized repo)" if the repository is not initialized and "branchName (++)" if it is dirty.
        /// </summary>
        /// <param name="this">This <see cref="GitManager"/>. Can be null.</param>
        /// <returns>A displayable string.</returns>
        public static string DisplayBranchName( this GitManager @this )
        {
            if( @this == null ) return "(No repository)";
            if( !@this.IsOpen ) return "(unable to open .git repo)";
            string s = @this.CurrentBranchName;
            if( s == null ) return "(unitialized repo)";
            if( @this.IsDirty )
            {
                return s + " (++)";
            }
            return s;
        }

    }
}
