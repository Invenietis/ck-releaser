#region LGPL License
/*----------------------------------------------------------------------------
* This file (CK.Releaser\Info\InfoRelease.XContent.cs) is part of CiviKey. 
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
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CK.Core;

namespace CK.Releaser.Info
{
    public partial class InfoRelease
    {
        static public readonly XName XContentFolder = XNamespace.None + "Folder";
        static public readonly XName XContentFile = XNamespace.None + "File";
        static public readonly XName XContentName = XNamespace.None + "Name";
        static public readonly XName XContentIsHidden = XNamespace.None + "IsHidden";

        public XElement GetXContent( IActivityMonitor m )
        {
            if( _content == null )
            {
                try
                {
                    _content = ReadDirectory( Path );
                }
                catch( Exception ex )
                {
                    m.Error().Send( ex, "While reading content of '{0}'.", Path );
                }
            }
            return _content;
        }

        static XElement ReadDirectory( string path )
        {
            return new XElement( XContentFolder, new XAttribute( XContentName, System.IO.Path.GetFileName( path ) ),
                                    Directory.EnumerateDirectories( path )
                                        .Where( p => !p.EndsWith( BranchRelease.HistoricFolderNameSuffix ) )
                                        .Select( p => ReadDirectory( p ) ),
                                    Directory.EnumerateFiles( path )
                                        .Where( p => !p.EndsWith( InfoRelease.ReleaseInfoFileNameSuffix ) )
                                        .Select( p => new XElement( XContentFile, 
                                                                        new XAttribute( XContentName, System.IO.Path.GetFileName( p ) ),
                                                                        new XAttribute( XContentIsHidden, (File.GetAttributes( p ) & FileAttributes.Hidden) != 0 ) ) ) );
        }

    }
}
