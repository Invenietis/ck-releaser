#region LGPL License
/*----------------------------------------------------------------------------
* This file (CKReleaser\Pages\InfoRelease\BaseReleaseDataEditor.cs) is part of CiviKey. 
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
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CK.Releaser.Info
{
    partial class BaseReleaseDataEditor : UserControl
    {
        InfoReleasePage Page;
        InfoRelease Info;
        readonly Font _normalSaveFont;

        public BaseReleaseDataEditor()
        {
            InitializeComponent();
            _normalSaveFont = _saveData.Font;
        }

        public void Initialize( InfoReleasePage page, InfoRelease info )
        {
            Page = page;
            Info = info;
        }

        public void RefreshDBView()
        {
            DevContextReleaseStatus status = Page.DevContext.ReleaseHead.Status;

            _versionFromSource.Text = status.SimpleModeVersion.ToString( 3 );
            var data = Info.GetData( Page.DevContext.MainMonitor );
            _preRelease.Text = data != null ? data.PreReleaseVersion : null;
            _buildMetaData.Text = data != null ? data.BuildMetadataVersion : null;

            _infoNotes.Text = data != null ? data.Notes : "Error: unable to load data.";
            _saveData.Font = _normalSaveFont;
        }

        void _saveData_Click( object sender, EventArgs e )
        {
            var data = Info.GetData( Page.DevContext.MainMonitor );
            if( data != null )
            {
                data.PreReleaseVersion = _preRelease.Text;
                data.BuildMetadataVersion = _buildMetaData.Text;
                data.Notes = _infoNotes.Text;
                if( Info.SaveData( Page.DevContext.MainMonitor ) )
                {
                    _saveData.Font = _normalSaveFont;
                }
            }
        }

        void _any_TextChanged( object sender, EventArgs e )
        {
            _saveData.Font = _versionFromSource.Font;
        }
    }
}
