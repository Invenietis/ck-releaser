#region LGPL License
/*----------------------------------------------------------------------------
* This file (CKReleaser\GUI\Prompt.cs) is part of CiviKey. 
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
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace CK.Releaser
{
	public class PromptBox
	{
		/// <summary>
		/// Asks for a string. Returned value will be null only if the user choose Cancel, otherwise <see cref="String.Empty"/>
		/// is returned. The value must be changed by the user, otherwise Null will be returned (i.e. the user can only click on Cancel).
		/// </summary>
		/// <param name="title">Title of the dialog box.</param>
		/// <param name="description">Description comes above the text box.</param>
		/// <param name="originalValue">Original value. Null is equivalent to String.Empty.</param>
		/// <returns>Null only if the user choose Cancel.</returns>
		public static string Prompt( string title, string description, string originalValue )
		{
			return Prompt( title, description, originalValue, false );
		}

		/// <summary>
		/// Asks for a string. Returned value will be null only if the user choose Cancel, otherwise <see cref="String.Empty"/>
		/// is returned.
		/// </summary>
		/// <param name="title">Title of the dialog box.</param>
		/// <param name="description">Description comes above the text box.</param>
		/// <param name="originalValue">Original value. Null is equivalent to String.Empty.</param>
		/// <param name="acceptNoChange">When set to false, OK button is disabled whenever the value has not been changed by the user: 
		/// he/she can only click on Cancel.</param>
        /// <param name="maskedInput">True to hide the characters (replaced with '*').</param>
		/// <returns>Null only if the user choose Cancel.</returns>
		public static string Prompt( string title, string description, string originalValue, bool acceptNoChange, bool maskedInput = false )
		{
            PromptDlg d = new PromptDlg( originalValue, acceptNoChange, maskedInput );
			d.Text = title;
			d._desc.Text = description;
			string result = d.ShowDialog() == DialogResult.OK 
				? d._txt.Text
				: null;
			d.Dispose();
			return result;
		}

	}
}
