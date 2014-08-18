#region LGPL License
/*----------------------------------------------------------------------------
* This file (CKReleaser\Pages\Signing\StrongNameAndSignPage.cs) is part of CiviKey. 
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
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.IO;
using System.Reflection;
using CK.Core;
using System.Diagnostics;

namespace CK.Releaser.Signing
{
    [DisplayName("Strong Naming & Signing")]
    partial class StrongNameAndSignPage : ReleaserControl
    {
        StrongNameKeyPair _privateKey;

        public StrongNameAndSignPage()
        {
            InitializeComponent();
            _privateKeyDesc2.Text = String.Empty;
        }

        public override void Initialize( IInteractiveDevContext ctx )
        {
            base.Initialize( ctx );
            ctx.Refreshed += ctx_Refreshed;
            _signToolPath.Text = AuthentiCodeSigner.FindDefaultSignToolPath( ctx.MainMonitor, false );
            ctx_Refreshed( this, EventArgs.Empty );
        }

        void ctx_Refreshed( object sender, EventArgs e )
        {
            var items = DevContext.Workspace.ExeAndDllFiles.Outputs.Select( f => new ListViewItem( f.SolutionFilePath ) { Checked = true, Tag = f } );
            _allOutputs.Items.Clear();
            _allOutputs.Items.AddRange( items.ToArray() );
        }

        #region Strong naming

        private void _privateKeyPathChoose_Click( object sender, EventArgs e )
        {
            using( OpenFileDialog d = new OpenFileDialog() )
            {
                d.CheckPathExists = true;
                d.FileName = _privateKeyPath.Text;
                d.Filter = "*.snk files|*.snk";
                if( d.ShowDialog( this ) == DialogResult.OK )
                {
                    string fileName = d.FileName;
                    SetPrivateKey( fileName );
                }
            }
        }

        void SetPrivateKey( string fileName )
        {
            try
            {
                _privateKey = new StrongNameKeyPair( File.ReadAllBytes( fileName ) );
                _privateKeyDesc.Text = "Private key successfully loaded.";
                _processStrongName.Enabled = true;
            }
            catch( Exception ex )
            {
                _privateKey = null;
                DevContext.MainMonitor.Error().Send( ex );
                _privateKeyDesc.Text = "Unable to read key: " + ex.Message;
                _processStrongName.Enabled = false;
            }
            _privateKeyPath.Text = fileName;
        }

        
        private void _strongNameLabel_Click( object sender, EventArgs e )
        {
            if( _privateKey != null )
            {
                using( var w = new DisplayStrongNameKey( _privateKey ) )
                {
                    w.ShowDialog();
                }
            }
        }

        private void _processStrongName_Click( object sender, EventArgs e )
        {
            StrongNameSigner signer = new StrongNameSigner( Signing.KnownStrongNames.SharedKey, _privateKey );
            try
            {
                UseWaitCursor = true;
                _privateKeyDesc.Text = String.Format( "Setting official strong names." );
                _privateKeyDesc2.Text = String.Empty;
                _privateKeyDesc.Refresh();
                _privateKeyDesc2.Refresh();
                foreach( var f in _allOutputs.Items.Cast<ListViewItem>().Where( item => item.Checked ).Select( item => item.Tag ).Cast<ExeAndDllFiles.File>() )
                {
                    string p = Path.Combine( DevContext.Workspace.WorkspacePath, f.SolutionFilePath );
                    signer.ProcessFile( DevContext.MainMonitor, p );
                    _privateKeyDesc.Text = String.Format( "{0} processed ({1} failed), {2} modified.", signer.TotalProcessedCount, signer.FailureCount, signer.ModifiedCount );
                    _privateKeyDesc2.Text = String.Format( "{0} signed, {1} reference(s) and {2} InternalsVisible updated.", signer.AssemblySignedCount, signer.ReferencesUpdatedCount, signer.InternalsVisibleToCount );
                    _privateKeyDesc.Refresh();
                    _privateKeyDesc2.Refresh();
                }
                _privateKeyDesc.Text = String.Format( "Finished: {0} processed ({1} failed), {2} modified.", signer.TotalProcessedCount, signer.FailureCount, signer.ModifiedCount );
            }
            catch( Exception ex )
            {
                ActivityMonitor.CriticalErrorCollector.Add( ex, "While signing." );
                _privateKeyDesc.Text = String.Format( "Unexpected error: {0}", ex.Message );
            }
            finally
            {
                UseWaitCursor = false;
            }
        }
    
        #endregion

        void _signToolFind_Click( object sender, EventArgs e )
        {
            using( OpenFileDialog d = new OpenFileDialog() )
            {
                d.CheckPathExists = true;
                d.FileName = _signToolPath.Text;
                d.Filter = "SignTool.exe|SignTool.exe";
                if( d.ShowDialog( this ) == DialogResult.OK )
                {
                    _signToolPath.Text = d.FileName;
                }
            }
            _processSignTool.Enabled = !(String.IsNullOrWhiteSpace( _signToolPath.Text ) && String.IsNullOrWhiteSpace( _pfxFilePath.Text ));
        }

        void _pfxFilePathFind_Click( object sender, EventArgs e )
        {
            using( OpenFileDialog d = new OpenFileDialog() )
            {
                d.CheckPathExists = true;
                d.FileName = _pfxFilePath.Text;
                d.Filter = "Authenticode Certificate (*.pfx)|*.pfx";
                if( d.ShowDialog( this ) == DialogResult.OK )
                {
                    _pfxFilePath.Text = d.FileName;
                }
            }
            _processSignTool.Enabled = !(String.IsNullOrWhiteSpace( _signToolPath.Text ) && String.IsNullOrWhiteSpace( _pfxFilePath.Text ));
        }

        private void _processSignTool_Click( object sender, EventArgs e )
        {
            string password = PromptBox.Prompt( "Certificate password", "Enter the password for the certificate.", null, acceptNoChange: true, maskedInput: true );
            if( password == null ) return;

            var m = DevContext.MainMonitor;
            var toSign = _allOutputs.Items
                            .Cast<ListViewItem>()
                            .Where( item => item.Checked )
                            .Select( item => item.Tag )
                            .Cast<ExeAndDllFiles.File>()
                            .Select( f => Path.Combine( DevContext.Workspace.WorkspacePath, f.SolutionFilePath ) );
            var signer = new AuthentiCodeSigner( _pfxFilePath.Text, password );
            try
            {
                UseWaitCursor = true;
                _pfxDescription.Text = "Running SignTool...";
                foreach( var f in toSign )
                {
                    signer.ProcessFile( m, f );
                    _pfxDescription.Text = String.Format( "{0} files processed: {1} success, {2} failed, {3} with warning.", signer.TotalProcessedCount, signer.SuccessCount, signer.FailureCount, signer.WarningCount );
                }
                _pfxDescription.Text = String.Format( "Finished: {0} files processed: {1} success, {2} failed, {3} with warning.", signer.TotalProcessedCount, signer.SuccessCount, signer.FailureCount, signer.WarningCount );
            }
            catch( Exception ex )
            {
                ActivityMonitor.CriticalErrorCollector.Add( ex, "While signing." );
                _pfxDescription.Text = String.Format( "Unexpected error: {0}", ex.Message );
            }
            finally
            {
                UseWaitCursor = false;
            }
        }



    }
}
