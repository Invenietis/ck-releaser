#region LGPL License
/*----------------------------------------------------------------------------
* This file (CKReleaser\Pages\Home\HomePage.cs) is part of CiviKey. 
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
using CK.Core;

namespace CK.Releaser.Home
{
    [DisplayName("Home")]
    partial class HomePage : ReleaserControl
    {
        readonly string _openFixesFormat;
        ValidationContext _currentValidationContext;
        ActivityMonitorListItemClient _currentLoggedItems;

        public HomePage()
        {
            SuspendLayout();
            InitializeComponent();
            _openFixesFormat = _openFixes.Text;
            _openFixes.Visible = false;
            ClearLogs( true );
            ResumeLayout();
        }

        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad( e );
            RunValidation();
        }

        protected override void OnDevContextRefreshed()
        {
            RunValidation();
        }
        
        public void AddSimpleLog( LogLevel level, string text )
        {
            var g = _logs.Groups["Logs"];
            if( g == null )
            {
                g = new ListViewGroup( "Logs", "" );
                _logs.Groups.Insert( 0, g );
            }
            ActivityMonitorListItemClient.AddSimpleLogEntry( _logs.Items, level, text );
        }

        public void ClearLogs( bool withGroups = false )
        {
            if( withGroups )
            {
                _logs.Groups.Clear();
                _logs.Items.Clear();
            }
            else
            {
                var g = _logs.Groups["Logs"];
                if( g != null )
                {
                    foreach( ListViewItem item in g.Items )
                    {
                        _logs.Items.Remove( item );
                    }
                }
            }
        }

        void _refresh_Click( object sender, EventArgs e )
        {
            DevContext.Refresh( DevContext.MainMonitor );
        }

        void RunValidation()
        {
            var m = new ActivityMonitor();
            var loggedItems = new ActivityMonitorListItemClient();
            m.Output.RegisterClient( loggedItems );
                        
            ValidationContext v = new ValidationContext( m );
            bool isValid = DevContext.Initialize( v );
            ClearLogs( true );
            var otherLogs = loggedItems.AddAndReset( _logs );
            string validText = String.Empty;
            if( v.Fixes.Count > 0 )
            {
                _openFixes.Visible = true;
                int enabledCount = v.Fixes.Count( f => !f.IsDisabled );
                if( enabledCount > 0 )
                {
                    validText = String.Format( "Some checks failed. {0} automatic fix(es) are available", enabledCount );
                    if( DevContext.GitManager.IsWorkingFolderWritable() ) validText += '.';
                    else validText += " but working folder is not writable (not on a valid branch).";

                }
                else
                {
                    validText = "Some checks failed, automatic fixes are available but they are all disabled.";
                    if( !DevContext.GitManager.IsWorkingFolderWritable() ) validText += " (And the working folder is not on a valid branch.)";
                }
                _currentValidationContext = v;
                _currentLoggedItems = loggedItems;
                _openFixes.Text = String.Format( _openFixesFormat, v.Fixes.Count );
            }
            else
            {
                _openFixes.Visible = false;
                _currentValidationContext = null;
                _currentLoggedItems = null;
                if( isValid )
                {
                    validText = "All checks passed.";
                }
                else 
                {
                    validText = "No automatic fixes are available. Please read the errors below and correct manually";
                    if( DevContext.GitManager.IsWorkingFolderWritable() ) validText += '.';
                    else validText += " after branching to a valid branch.";
                }
            }
            _validationSummary.Text = validText; 
        }

        private void _openFixes_Click( object sender, EventArgs e )
        {
            using( var fw = new FixWindow( DevContext, _currentValidationContext ) )
            {
                if( fw.ShowDialog( this ) == DialogResult.OK )
                {
                    int enabledFixCount, disabledFixCount;
                    bool success = _currentValidationContext.ApplyFixes( DevContext, fw.MemorizeDisabledFixes, out enabledFixCount, out disabledFixCount );
                    if( success )
                    {
                        if( enabledFixCount > 0 )
                        {
                            _validationSummary.Text = String.Format( "Successfully applied {0} fix(es).", enabledFixCount );
                        }
                        else 
                        {
                            if( disabledFixCount > 0 )
                            {
                                _validationSummary.Text = String.Format( "All {0} fix(es) have been disabled.", disabledFixCount );
                            }
                            else
                            {
                                _validationSummary.Text = String.Format( "Successfully applied {0} fix(es) ({1} have been disabled).", enabledFixCount, disabledFixCount );
                            }
                        }
                    }
                    else
                    {
                        _validationSummary.Text = String.Format( "Failed to apply one of the {0} fix(es).", enabledFixCount );
                    }
                    DevContext.IsDirty = true;
                    ClearLogs( true );
                    var otherLogs = _currentLoggedItems.AddAndReset( _logs );
                }
            }
        }

    }
}
