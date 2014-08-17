#region LGPL License
/*----------------------------------------------------------------------------
* This file (CK.Releaser\Signing\AuthentiCodeSigner.cs) is part of CiviKey. 
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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CK.Core;

namespace CK.Releaser.Signing
{
    public class AuthentiCodeSigner
    {
        const string _signToolFileName = "signtool.exe";
        static string _defaultSignToolPath;

        string _signToolPath;
        readonly string _pfxFilePath;
        readonly string _password;

        public AuthentiCodeSigner( string pfxFilePath, string password )
        {
            _pfxFilePath = pfxFilePath;
            _password = password ?? String.Empty;
        }

        public int TotalProcessedCount { get; private set; }

        public int SuccessCount { get; private set; }

        public int FailureCount { get; private set; }

        public int WarningCount { get; private set; }

        public void ClearStats()
        {
            TotalProcessedCount = 0;
            SuccessCount = 0;
            FailureCount = 0;
            WarningCount = 0;
        }

        public string SignToolPath
        {
            get { return _signToolPath; }
            set { _signToolPath = value; }
        }

        public string EnsureSignToolPath( IActivityMonitor m )
        {
            return _signToolPath ?? (_signToolPath = FindDefaultSignToolPath( m, true ));
        }

        public bool ProcessFile( IActivityMonitor m, string fileToSign )
        {
            using( m.OpenInfo().Send( "Digitally signing '{0}'.", fileToSign ) )
            {
                try
                {
                    EnsureSignToolPath( m );
                    if( _signToolPath == null ) return false;
                    ++TotalProcessedCount;
                    ProcessStartInfo info = new ProcessStartInfo( _signToolPath );
                    info.Arguments = String.Format( @"sign /f ""{0}"" /p ""{1}"" /t http://timestamp.verisign.com/scripts/timstamp.dll /v ""{2}""", _pfxFilePath, _password, fileToSign );
                    info.CreateNoWindow = true;
                    info.UseShellExecute = false;
                    info.WindowStyle = ProcessWindowStyle.Hidden;
                    using( Process p = Process.Start( info ) )
                    {
                        p.WaitForExit();
                        if( p.ExitCode == 1 )
                        {
                            ++FailureCount;
                            m.Error().Send( "Signtool.exe exited with code = 1." );
                            m.CloseGroup( "Failed." );
                            return false;
                        }
                        if( p.ExitCode == 0 )
                        {
                            ++SuccessCount;
                            m.CloseGroup( "Success." );
                        }
                        else if( p.ExitCode == 2 )
                        {
                            ++WarningCount;
                            m.CloseGroup( "Success but with warnings." );
                        }
                    }
                    return true;
                }
                catch( Exception ex )
                {
                    m.Error().Send( ex );
                    ++FailureCount;
                    return false;
                }
            }
        }

        public static string FindDefaultSignToolPath( IActivityMonitor m, bool logErrorIfNotFound )
        {
            return Tools.ToolPathFinder.FindDefaultPath( m, ref _defaultSignToolPath, _signToolFileName, logErrorIfNotFound );
        }

    }
}
