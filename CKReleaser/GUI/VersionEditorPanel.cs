#region LGPL License
/*----------------------------------------------------------------------------
* This file (CKReleaser\GUI\VersionEditorPanel.cs) is part of CiviKey. 
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

namespace CK.Releaser.GUI
{
    public partial class VersionEditorPanel : UserControl
    {
        public VersionEditorPanel()
        {
            InitializeComponent();
        }

        public bool FromSourceEnabled
        {
            get { return _major.Enabled; }
            set { _major.Enabled = _minor.Enabled = _patch.Enabled = value; }
        }

        public bool PreReleaseEnabled
        {
            get { return _preRelease.Enabled; }
            set { _preRelease.Enabled = value; }
        }

        public bool BuildMetaDataEnabled
        {
            get { return _buildMetaData.Enabled; }
            set { _buildMetaData.Enabled = value; }
        }

        public Version Version
        {
            get { return new Version( (int)_major.Value, (int)_minor.Value, (int)_patch.Value ); }
            set 
            { 
                _major.Value = value.Major;
                _minor.Value = value.Minor;
                _patch.Value = value.Build > 0 ? value.Build : 0; 
            }
        }

        public string PreRelease
        {
            get { return _preRelease.Text; }
            set { _preRelease.Text = value; }
        }
        
        public string BuildMetaData
        {
            get { return _buildMetaData.Text; }
            set { _buildMetaData.Text = value; }
        }
    }
}
