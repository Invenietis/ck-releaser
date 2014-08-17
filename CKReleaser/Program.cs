#region LGPL License
/*----------------------------------------------------------------------------
* This file (CKReleaser\Program.cs) is part of CiviKey. 
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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using CK.Core;
using CK.Monitoring;

namespace CK.Releaser
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main( string[] arguments )
        {
            SystemActivityMonitor.RootLogPath = @"..\Logs";
            GrandOutput.EnsureActiveDefaultWithDefaultSettings();
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault( false );
                string root = Workspace.FindSolutionFolderFrom();
                if( arguments.Length > 0 )
                {
                    if( root == null ) root = AppDomain.CurrentDomain.BaseDirectory;
                    root = Path.GetFullPath( Path.Combine( root, arguments[0] ) );
                    if( !Directory.Exists( root ) )
                    {
                        MessageBox.Show( "Unable to find a root at '{0}'.", root );
                        root = null;
                    }
                }
                else
                {
                    if( root == null ) MessageBox.Show( "No folder with a .sln found above '{0}'.", AppDomain.CurrentDomain.BaseDirectory );
                }

                if( root != null )
                {
                    var c = new GUIDevContext( new ActivityMonitor( "CK.Releaser.Main" ), root );
                    Application.Run( new MainWindow( c ) );
                }
            }
            catch( Exception ex )
            {
                ActivityMonitor.MonitoringError.Add( ex, "Unhandled error." );
            }
            ActivityMonitor.MonitoringError.WaitOnErrorFromBackgroundThreadsPending();
        }
    }
}
