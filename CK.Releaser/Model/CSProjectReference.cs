#region LGPL License
/*----------------------------------------------------------------------------
* This file (CK.Releaser\Model\CSProjectReference.cs) is part of CiviKey. 
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

namespace CK.Releaser
{
    public abstract class CSProjectReference
    {
        public readonly XElement Element;
        public readonly string Include;
        public readonly bool IsProjectReference;

        protected CSProjectReference( XElement e )
        {
            Element = e;
            Include = (string)e.Attribute( VSMP.IncludeAttribute );
            IsProjectReference = e.Name == VSMP.ProjectReference;
        }

    }

    public class CSProjectProjectReference : CSProjectReference
    {
        public readonly Guid Project;
        public readonly string Name;
        
        public CSProjectProjectReference( XElement e )
            : base( e )
        {
            var p = e.Elements( VSMP.Project ).Select( c => c.Value ).FirstOrDefault();
            if( p == null ) Project = Guid.Empty;
            else Guid.TryParse( p, out Project );
            Name = e.Elements( VSMP.Name ).Select( c => c.Value ).FirstOrDefault();
        }

    }

    public class CSProjectAssemblyReference : CSProjectReference
    {
        readonly CSProjectObject _project;

        public readonly bool? SpecificVersion;

        /// <summary>
        /// Null when in the GAC or if it is empty in the csproj.
        /// </summary>
        public readonly string HintPath;

        FileInfo _resolved;
        bool _resolvedDone;

        /// <summary>
        /// Null when HintPath is invalid of if the file does not exist.
        /// </summary>
        public FileInfo GetHintPathResolved( IActivityMonitor m )
        {
            if( !_resolvedDone )
            {
                _resolvedDone = true;
                if( !String.IsNullOrEmpty( HintPath ) )
                {
                    try
                    {
                        var p = Path.GetFullPath( Path.Combine( _project.ThisDirectoryPath, HintPath ) );
                        FileInfo f = new FileInfo( p );
                        if( f.Exists ) _resolved = f;
                    }
                    catch( Exception ex )
                    {
                        m.Warn().Send( ex, "When resolving HintPath='{0}'.", HintPath );
                    }
                }
            }
            return _resolved;
        }

        public CSProjectAssemblyReference( CSProjectObject project, XElement e )
            : base( e )
        {
            _project = project;
            var p = e.Elements( VSMP.SpecificVersion ).Select( c => c.Value ).FirstOrDefault();
            if( p != null ) SpecificVersion = StringComparer.OrdinalIgnoreCase.Equals( p, "true" );
            HintPath = e.Elements( VSMP.HintPath ).Select( c => c.Value ).FirstOrDefault();
            if( String.IsNullOrWhiteSpace( HintPath ) ) HintPath = null;
        }
    }
}
