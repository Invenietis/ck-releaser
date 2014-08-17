#region LGPL License
/*----------------------------------------------------------------------------
* This file (CK.Releaser\Info\InfoReleaseDatabase.Creation.cs) is part of CiviKey. 
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
using CK.Core;

namespace CK.Releaser.Info
{
    public partial class InfoReleaseDatabase
    {
        /// <summary>
        /// Initializes a new InfoReleaseDatabase on a directory.
        /// The directory must exist otherwise an Exception is thrown.
        /// If the directory do not contain a file "CK-InfoReleaseKey.txt" with a GUID inside it is automatically created
        /// with a new identifier if <paramref name="autoInitialize"/> is true, otherwise a <see cref="CKException"/> is thrown.
        /// </summary>
        /// <param name="path">Path of the database.</param>
        /// <param name="autoInitialize">True to create the marker file.</param>
        public InfoReleaseDatabase( string path, bool autoInitialize = true )
            : this( path )
        {
            if( !Directory.Exists( DatabasePath ) ) throw new ArgumentException( "Directory must exist.", "path" );
            string keyFile = Path.Combine( DatabasePath, _fileNameKey );
            if( File.Exists( keyFile ) )
            {
                string txt = File.ReadAllText( keyFile );
                DatabaseId = Guid.Parse( txt );
            }
            else
            {
                if( autoInitialize )
                {
                    DatabaseId = Guid.NewGuid();
                    File.WriteAllText( keyFile, DatabaseId.ToString() );
                }
                else throw new CKException( "The directory '{0}' must contain a {1} file with its unique identifier.", DatabasePath, _fileNameKey );
            }
        }

        InfoReleaseDatabase( string path, Guid id )
            : this( path )
        {
            DatabaseId = id;
        }

        InfoReleaseDatabase( string path )
        {
            DatabasePath = FileUtil.NormalizePathSeparator( Path.GetFullPath( path ), true );
            Name = Path.GetFileName( DatabasePath.Substring( 0, DatabasePath.Length - 1 ) );
            _instances = new Dictionary<string, DBObject>();
            _keyBuilder = new StringBuilder();
        }

        /// <summary>
        /// Tries to load a <see cref="InfoReleaseDatabase"/> from a directory with a specific identifier.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="databaseId">The identifier that must match the key file.</param>
        /// <returns>Null or the database object.</returns>
        public static InfoReleaseDatabase TryCreateFrom( string path, Guid databaseId )
        {
            string keyFile = Path.Combine( path, _fileNameKey );
            if( File.Exists( keyFile ) )
            {
                string txt = File.ReadAllText( keyFile );
                Guid id;
                if( Guid.TryParse( File.ReadAllText( keyFile ), out id ) && id == databaseId )
                {
                    return new InfoReleaseDatabase( path, id );
                }
            }
            return null;
        }

        /// <summary>
        /// Tries to load a <see cref="InfoReleaseDatabase"/> from a directory (if the key file with the identifier exists).
        /// </summary>
        /// <param name="path">Path of the database.</param>
        /// <returns>Null or the database object.</returns>
        public static InfoReleaseDatabase TryCreateFrom( string path )
        {
            string keyFile = Path.Combine( path, _fileNameKey );
            if( File.Exists( keyFile ) )
            {
                string txt = File.ReadAllText( keyFile );
                Guid id;
                if( Guid.TryParse( File.ReadAllText( keyFile ), out id ) )
                {
                    return new InfoReleaseDatabase( path, id );
                }
            }
            return null;
        }

        /// <summary>
        /// Tries to find a database based on its identifier.
        /// </summary>
        /// <param name="startingPath">The path to start.</param>
        /// <param name="databaseId">Identifier of the database.</param>
        /// <returns>Null or the database object.</returns>
        public static InfoReleaseDatabase FindSiblingOrAbove( string startingPath, Guid databaseId )
        {
            if( String.IsNullOrEmpty( startingPath ) ) throw new ArgumentException( "startingPath" );
            string p = startingPath[startingPath.Length - 1] == Path.DirectorySeparatorChar ? startingPath.Substring( 0, startingPath.Length - 1 ) : startingPath;
            while( !String.IsNullOrEmpty( p = Path.GetDirectoryName( p ) ) )
            {
                foreach( var c in Directory.EnumerateDirectories( p ) )
                {
                    InfoReleaseDatabase db = TryCreateFrom( c, databaseId );
                    if( db != null ) return db;
                }
            }
            return null;
        }

        /// <summary>
        /// Tries to find a database when only the name is known.
        /// </summary>
        /// <param name="startingPath">The path to start.</param>
        /// <param name="databaseName">Name of the database.</param>
        /// <returns>Null or the database object.</returns>
        public static InfoReleaseDatabase FindSiblingOrAbove( string startingPath, string databaseName )
        {
            if( String.IsNullOrEmpty( startingPath ) ) throw new ArgumentException( "startingPath" );
            ///TODO: FileUtil.GetFileDirectoryName( p ) and FileUtil.GetFileDirectoryNameWithoutExtension( p ).
            string p = startingPath[startingPath.Length - 1] == Path.DirectorySeparatorChar ? startingPath.Substring( 0, startingPath.Length - 1 ) : startingPath;
            while( !String.IsNullOrEmpty( p = Path.GetDirectoryName( p ) ) )
            {
                string dbPath = Path.Combine( p, databaseName );
                if( Directory.Exists( dbPath ) )
                {
                    InfoReleaseDatabase db = TryCreateFrom( dbPath );
                    if( db != null ) return db;
                }
            }
            return null;
        }

        /// <summary>
        /// Finds all <see cref="InfoReleaseDatabase"/> in a context.
        /// </summary>
        /// <param name="startingPath">The path to start.</param>
        /// <returns>All available database.</returns>
        public static IEnumerable<InfoReleaseDatabase> FindAll( string startingPath )
        {
            if( String.IsNullOrEmpty( startingPath ) ) throw new ArgumentException( "startingPath" );
            string p = startingPath[startingPath.Length - 1] == Path.DirectorySeparatorChar ? startingPath.Substring( 0, startingPath.Length - 1 ) : startingPath;
            while( !String.IsNullOrEmpty( p = Path.GetDirectoryName( p ) ) )
            {
                foreach( var sub in Directory.EnumerateDirectories( p ) )
                {
                    InfoReleaseDatabase db = TryCreateFrom( sub );
                    if( db != null ) yield return db;
                }
            }
        }


    }
}
