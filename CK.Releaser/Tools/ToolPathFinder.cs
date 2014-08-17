using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CK.Core;

namespace CK.Releaser.Tools
{
    static class ToolPathFinder
    {
        static string[] _vsPaths = new string[]
        {
            @"%ProgramFiles(x86)%\Microsoft SDKs\Windows\v7.0A\Bin\",
            @"%ProgramFiles(x86)%\Microsoft Visual Studio 11.0\Common7\IDE\",
            @"%ProgramFiles(x86)%\Microsoft Visual Studio 11.0\VC\BIN\x86_amd64",
            @"%ProgramFiles(x86)%\Microsoft Visual Studio 11.0\VC\BIN",
            @"%ProgramFiles(x86)%\Microsoft Visual Studio 11.0\Common7\Tools",
            @"%ProgramFiles(x86)%\Microsoft Visual Studio 11.0\VC\VCPackages",
            @"%ProgramFiles(x86)%\Windows Kits\8.0\bin\x86",
            @"%ProgramFiles(x86)%\Microsoft SDKs\Windows\v8.0A\bin\NETFX 4.0 Tools",
        };

        public static string FindDefaultPath( IActivityMonitor m, ref string cached, string exeName, bool logErrorIfNotFound )
        {
            if( cached == null )
            {
                using( m.OpenInfo().Send( "Finding {0}.", exeName ) )
                {
                    cached = FindToolPath( m, _vsPaths.Select( s => Environment.ExpandEnvironmentVariables( s ) ), exeName )
                                                    ?? FindToolPath( m, Environment.ExpandEnvironmentVariables( "%path%" ).Split( ';' ), exeName );
                    if( cached == null && logErrorIfNotFound )
                    {
                        m.Error().Send( "Unable to find it." );
                    }
                }
            }
            return cached;
        }

        static string FindToolPath( IActivityMonitor m, IEnumerable<string> paths, string exeName )
        {
            foreach( var p in paths )
            {
                m.Trace().Send( "Considering: {0}", p );
                var signToolPath = Path.Combine( p, "signtool.exe" );
                if( File.Exists( signToolPath ) )
                {
                    m.CloseGroup( "Found here: " + p );
                    return signToolPath;
                }
            }
            return null;
        }

    }
}
