#region LGPL License
/*----------------------------------------------------------------------------
* This file (CKReleaser\Pages\Signing\DisplayStrongNameKey.cs) is part of CiviKey. 
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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CK.Releaser
{
    public partial class DisplayStrongNameKey : Form
    {
        public DisplayStrongNameKey( StrongNameKeyPair snk )
        {
            InitializeComponent();
            _publicKeyToken.Text = Signing.StrongNameSigner.PublicKeyTokenToString( snk );
            _publicKey.Text = Signing.StrongNameSigner.PublicKeyToString( snk );
            if( snk.PublicKey.SequenceEqual( Signing.KnownStrongNames.OfficialPublicKeyBytes ) )
            {
                _knownText.Text = "This is the Official key. It must ABSOLUTELY be kept secret.";
            }
            else if( snk.PublicKey.SequenceEqual( Signing.KnownStrongNames.SharedPublicKeyBytes ) )
            {
                _knownText.Text = "This is the Shared key. It is useless to use it again...";
            }
        }

        private void _closeButton_Click( object sender, EventArgs e )
        {
            Close();
        }
    }
}
