#region LGPL License
/*----------------------------------------------------------------------------
* This file (CK.Releaser\Signing\StrongNameSigner.cs) is part of CiviKey. 
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
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CK.Core;
using Mono.Cecil;

namespace CK.Releaser.Signing
{
    public class StrongNameSigner
    {
        readonly StrongNameKeyPair _sharedKey;
        readonly StrongNameKeyPair _privateKey;
        readonly byte[] _sharedPublicKey;
        readonly byte[] _sharedPublicKeyToken;
        readonly byte[] _privatePublicKey;
        readonly string _sharedPublicKeyString;
        readonly string _privatePublicKeyString;

        /// <summary>
        /// Initializes a new <see cref="StrongNameSigner"/> with the shared key <see cref="KnownStrongNames.SharedKey"/>.
        /// </summary>
        /// <param name="privateKey"></param>
        public StrongNameSigner( StrongNameKeyPair privateKey )
            : this( KnownStrongNames.SharedKey, privateKey )
        {
        }

        public StrongNameSigner( StrongNameKeyPair sharedKey, StrongNameKeyPair privateKey )
        {
            _sharedKey = sharedKey;
            _sharedPublicKey = _sharedKey.PublicKey;
            _sharedPublicKeyToken = GetPublicKeyToken( _sharedKey.PublicKey );
            _privateKey = privateKey;
            _privatePublicKey = _privateKey.PublicKey;
            _sharedPublicKeyString = DoPublicKeyToString( _sharedPublicKey, false );
            _privatePublicKeyString = DoPublicKeyToString( _privatePublicKey, false );
        }

        public int TotalProcessedCount { get; private set; }

        public int ModifiedCount { get; private set; }

        public int FailureCount { get; private set; }
        
        public int AssemblySignedCount { get; private set; }

        public int ReferencesUpdatedCount { get; private set; }

        public int InternalsVisibleToCount { get; private set; }

        public void ClearStats()
        {
            TotalProcessedCount = 0;
            ModifiedCount = 0;
            FailureCount = 0;
            AssemblySignedCount = 0;
            ReferencesUpdatedCount = 0;
            InternalsVisibleToCount = 0;
        }

        public bool ProcessFile( IActivityMonitor m, string filePath )
        {
            if( m == null ) throw new ArgumentNullException( "m" );
            if( filePath == null ) throw new ArgumentNullException( "filePath" );
            return DoApply( m, filePath );
        }

        class Resolver : BaseAssemblyResolver
        {
            readonly IActivityMonitor _monitor;

            public Resolver( string path, IActivityMonitor m )
            {
                _monitor = m;
                AddSearchDirectory( Path.GetDirectoryName( path ) );
            }
           
            public override AssemblyDefinition Resolve( AssemblyNameReference name )
            {
                _monitor.Trace().Send( "Resolving {0}.", name.FullName );
                return base.Resolve( name );
            }

        }

        bool DoApply( IActivityMonitor m, string filePath )
        {
            using( m.OpenTrace().Send( "Strong Naming '{0}'.", filePath ) )
            {
                TotalProcessedCount++;
                try
                {
                    AssemblyDefinition assembly = AssemblyDefinition.ReadAssembly( filePath, new ReaderParameters() { AssemblyResolver = new Resolver( filePath, m ) } );
                    bool touched = false;
                    foreach( ModuleDefinition module in assembly.Modules )
                    {
                        foreach( AssemblyNameReference refA in module.AssemblyReferences )
                        {
                            if( refA.PublicKeyToken.SequenceEqual( _sharedPublicKeyToken ) )
                            {
                                ReferencesUpdatedCount++;
                                m.Info().Send( "Reference to {0} updated.", refA.Name );
                                touched = true;
                                refA.PublicKey = _privatePublicKey;
                            }
                        }
                    }
                    if( assembly.HasCustomAttributes )
                    {
                        List<Tuple<int,CustomAttribute>> toReplace = null;
                        int i = 0;
                        foreach( var attr in assembly.CustomAttributes )
                        {
                            string assemblyRefName;
                            if( attr.AttributeType.Name == "InternalsVisibleToAttribute"
                                && attr.ConstructorArguments.Count == 1
                                && (assemblyRefName = attr.ConstructorArguments[0].Value as string) != null
                                && assemblyRefName.EndsWith( _sharedPublicKeyString ) )
                            {
                                if( toReplace == null ) toReplace = new List<Tuple<int,CustomAttribute>>();
                                var newName = assemblyRefName.Remove( assemblyRefName.Length - _sharedPublicKeyString.Length );
                                m.Info().Send( "InternalsVisibleTo '{0}' updated.", newName );
                                newName += _privatePublicKeyString;
                                var newAttr = new CustomAttribute( attr.Constructor );
                                newAttr.ConstructorArguments.Add( new CustomAttributeArgument( attr.ConstructorArguments[0].Type, newName ) );
                                toReplace.Add( Tuple.Create( i, newAttr ) );
                            }
                            ++i;
                        }
                        if( toReplace != null )
                        {
                            touched = true;
                            InternalsVisibleToCount += toReplace.Count;
                            foreach( var rep in toReplace )
                            {
                                assembly.CustomAttributes[rep.Item1] = rep.Item2;
                            }
                        }
                    }
                    if( assembly.Name.HasPublicKey && assembly.Name.PublicKey.SequenceEqual( _sharedPublicKey ) )
                    {
                        AssemblySignedCount++;
                        m.Info().Send( "Setting private key on {0}.", assembly.Name.Name );
                        touched = true;
                        StrongName( assembly, _privateKey );
                    }
                    if( touched )
                    {
                        ModifiedCount++;
                        m.Info().Send( "Saving modified file." );
                        assembly.Write( filePath, new WriterParameters { StrongNameKeyPair = _privateKey } );
                    }
                    else m.CloseGroup( "No modification required." );
                    return true;
                }
                catch( Exception ex )
                {
                    FailureCount++;
                    m.Error().Send( ex );
                    return false;
                }
            }
        }

        public static byte[] GetPublicKeyToken( StrongNameKeyPair k )
        {
            return GetPublicKeyToken( k.PublicKey );
        }

        public static string PublicKeyTokenToString( StrongNameKeyPair k )
        {
            return PublicKeyTokenToString( GetPublicKeyToken( k.PublicKey ) );
        }

        public static string PublicKeyToString( StrongNameKeyPair k, bool binaryArray = true )
        {
            var publicKey = k.PublicKey;
            return DoPublicKeyToString( publicKey, binaryArray );
        }

        static string DoPublicKeyToString( byte[] publicKey, bool binaryArray )
        {
            StringBuilder b = new StringBuilder();
            if( binaryArray )
            {
                for( int i = 0; i < publicKey.Length; i++ )
                {
                    if( b.Length > 0 ) b.Append( ", " );
                    b.Append( "0x" ).Append( publicKey[i].ToString( "x2" ) );
                }
            }
            else
            {
                for( int i = 0; i < publicKey.Length; i++ )
                {
                    b.Append( publicKey[i].ToString( "x2" ) );
                }
            }
            return b.ToString();
        }

        internal static byte[] GetPublicKeyToken( byte[] publicKey )
        {
            byte[] publicKeyToken = new byte[8];
            using( SHA1 sha1 = new SHA1Managed() )
            {
                byte[] hash = sha1.ComputeHash( publicKey );
                Array.Copy( hash, hash.Length - publicKeyToken.Length, publicKeyToken, 0, publicKeyToken.Length );
            }
            Array.Reverse( publicKeyToken, 0, publicKeyToken.Length );
            return publicKeyToken;
        }

        static string PublicKeyTokenToString( byte[] publicKeyToken )
        {
            StringBuilder b = new StringBuilder( 16 );
            for( int i = 0; i < publicKeyToken.Length; i++ )
            {
                b.Append( publicKeyToken[i].ToString( "x2" ) );
            }
            return b.ToString();
        }

        internal static void StrongName( AssemblyDefinition assembly, StrongNameKeyPair key )
        {
            assembly.Name.HashAlgorithm = AssemblyHashAlgorithm.SHA1;
            assembly.Name.PublicKey = key.PublicKey;
            assembly.Name.HasPublicKey = true;
            assembly.Name.Attributes &= AssemblyAttributes.PublicKey;
            foreach( var m in assembly.Modules )
            {
                m.Attributes |= ModuleAttributes.StrongNameSigned;
            }
        }


    }
}
