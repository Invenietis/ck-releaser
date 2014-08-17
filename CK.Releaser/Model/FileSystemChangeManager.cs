#region LGPL License
/*----------------------------------------------------------------------------
* This file (CK.Releaser\Model\FileSystemChangeManager.cs) is part of CiviKey. 
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

namespace CK.Releaser
{
    class FileSystemChangeManager
    {
        readonly List<Change> _changes;

        struct Change
        {
            public readonly bool IsDirectory;
            public readonly string Original;
            public string From;
            public string To;

            public Change( bool isDirectory, string from, string to )
            {
                IsDirectory = isDirectory;
                Original = From = from;
                To = to;
            }

            public void ApplyPreviousChange( Change c )
            {
                Debug.Assert( c.IsDirectory );
                Debug.Assert( From[From.Length-1] == Path.DirectorySeparatorChar );
                Debug.Assert( To[To.Length] == Path.DirectorySeparatorChar );
                From = AppyChange( From, c );
                To = AppyChange( To, c );
            }

            private string AppyChange( string s, Change c )
            {
                return s.Replace( c.From, c.To );
            }
        }

        public FileSystemChangeManager()
        {
            _changes = new List<Change>();
        }

        public void Rename( string from, string to )
        {
            bool isDir = from[from.Length-1] == Path.DirectorySeparatorChar;
            Debug.Assert( !isDir || to[to.Length-1] == Path.DirectorySeparatorChar );
            _changes.Add( new Change( isDir, from, to ) );
        }

        public int ChangeCount
        {
            get { return _changes.Count; }
        }

        public bool ApplyChanges( IActivityMonitor m, Workspace s )
        {
            for( int i = 0; i < _changes.Count; ++i )
            {
                Change c = _changes[i];
                using( m.OpenInfo().Send( "Renaming '{0}' to '{1}'.", c.From, c.To ) )
                {
                    try
                    {
                        if( c.IsDirectory )
                        {
                            Directory.Move( c.From, c.To );
                            for( int j = i + 1; i < _changes.Count; ++j )
                            {
                                _changes[j].ApplyPreviousChange( c );
                            }
                        }
                        else
                        {
                            File.Move( c.From, c.To );
                        }
                    }
                    catch( Exception ex )
                    {
                        string fromLocal = s.MakeRootRelativePath( c.From );
                        string toLocal = s.MakeRootRelativePath( c.To );
                        m.Error().Send( ex );
                        m.Info().Send( "Please manually rename '{0}' to '{1}'", fromLocal, toLocal );
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
