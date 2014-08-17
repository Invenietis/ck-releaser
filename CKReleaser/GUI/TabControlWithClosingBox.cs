#region LGPL License
/*----------------------------------------------------------------------------
* This file (CKReleaser\GUI\TabControlWithClosingBox.cs) is part of CiviKey. 
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
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CK.Releaser.GUI
{
    public class TabControlWithClosingBox : TabControl
    {
        protected override void OnDrawItem( DrawItemEventArgs e )
        {
            base.OnDrawItem( e );
            if( ShowClose( e.Index ) )
            {
                e.Graphics.DrawString( "x", e.Font, Brushes.Black, e.Bounds.Right - 15, e.Bounds.Top + 4 );
                e.Graphics.DrawString( TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left + 12, e.Bounds.Top + 4 );
                e.Graphics.DrawRectangle( Pens.Gray, CloseButtonRect( e.Bounds ) );
                e.DrawFocusRectangle();
            }
        }

        protected override void OnMouseDown( MouseEventArgs e )
        {
            base.OnMouseDown( e );
            for( int i = 0; i < TabPages.Count; i++ )
            {
                if( ShowClose( i ) )
                {
                    Rectangle closeButton = CloseButtonRect( GetTabRect( i ) );
                    if( closeButton.Contains( e.Location ) )
                    {
                        TabPages.RemoveAt( i );
                        break;
                    }
                }
            }
        }

        private static Rectangle CloseButtonRect( Rectangle r )
        {
            return new Rectangle( r.Right - 16, r.Top + 5, 10, 12 );
        }

        protected virtual bool ShowClose( int index )
        {
            return true;
        }
    }
}
