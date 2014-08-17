#region LGPL License
/*----------------------------------------------------------------------------
* This file (CK.Releaser\Signing\KnownStrongNames.cs) is part of CiviKey. 
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
using System.Text;
using System.Threading.Tasks;

namespace CK.Releaser.Signing
{
    public static class KnownStrongNames
    {
        static byte[] _sharedKeyBytes;
        static StrongNameKeyPair _sharedKey;

        static byte[] _officialPublicKey = new byte[]
        {
            0x00, 0x24, 0x00, 0x00, 0x04, 0x80, 0x00, 0x00, 0x94, 0x00, 0x00, 0x00, 0x06, 0x02, 0x00, 0x00, 0x00, 0x24, 0x00, 0x00, 
            0x52, 0x53, 0x41, 0x31, 0x00, 0x04, 0x00, 0x00, 0x01, 0x00, 0x01, 0x00, 0x85, 0x13, 0x23, 0x0f, 0xf3, 0x92, 0x39, 0x5f, 
            0x63, 0x67, 0x1e, 0x73, 0xe0, 0x33, 0xe8, 0x9e, 0xd6, 0xad, 0x75, 0x42, 0xc0, 0xc6, 0x3d, 0x2f, 0x9c, 0xd8, 0xad, 0x15, 
            0xf0, 0x57, 0xfa, 0xfd, 0xce, 0x68, 0xaa, 0x9c, 0xae, 0x81, 0x95, 0x9a, 0xa1, 0xc0, 0x06, 0x97, 0xd5, 0xff, 0x5c, 0xb6, 
            0xec, 0x2e, 0x41, 0x38, 0xb5, 0xc8, 0x9e, 0x9d, 0x62, 0xd8, 0x73, 0xd4, 0xaa, 0x4f, 0x46, 0x20, 0x31, 0x4c, 0x00, 0xd4, 
            0xfc, 0xb3, 0x86, 0x6f, 0x2f, 0xe5, 0xc0, 0x15, 0x06, 0xdf, 0xc2, 0x78, 0x54, 0x62, 0x56, 0xad, 0xa0, 0xfc, 0xd9, 0xc5, 
            0xb0, 0x43, 0xdb, 0x0e, 0xa1, 0x1e, 0xe4, 0x55, 0x09, 0xa7, 0x9f, 0xd9, 0x88, 0xf1, 0x77, 0x5a, 0x2b, 0x31, 0xf0, 0x55, 
            0x8a, 0x07, 0x9e, 0xf6, 0xfd, 0x87, 0xa8, 0xd9, 0x47, 0x56, 0x01, 0xd4, 0x07, 0x2b, 0x88, 0x0e, 0xf9, 0x0d, 0xf2, 0x9a
        };

        public static byte[] OfficialPublicKeyBytes
        {
            get { return (byte[])_officialPublicKey.Clone(); }
        }

        public static byte[] OfficialPublicKeyTokenBytes
        {
            get { return StrongNameSigner.GetPublicKeyToken( _officialPublicKey ); }
        }

        public static byte[] SharedPublicKeyBytes
        {
            get { return SharedKey.PublicKey; }
        }

        public static byte[] SharedPublicKeyTokenBytes
        {
            get { return StrongNameSigner.GetPublicKeyToken( _sharedKey.PublicKey ); }
        }

        public static StrongNameKeyPair SharedKey
        {
            get
            {
                if( _sharedKey == null )
                {
                    _sharedKey = new StrongNameKeyPair( SharedKeyBytes );
                }
                return _sharedKey;
            }
        }

        public static byte[] SharedKeyBytes
        {
            get { return (byte[])EnsureSharedKeyBytes().Clone(); }
        }

        static byte[] EnsureSharedKeyBytes()
        {
            if( _sharedKeyBytes == null )
            {
                using( Stream stream = Assembly.GetAssembly( typeof( KnownStrongNames ) ).GetManifestResourceStream( typeof( KnownStrongNames ).Namespace + ".SharedKey.snk" ) )
                {
                    _sharedKeyBytes = new byte[stream.Length];
                    stream.Read( _sharedKeyBytes, 0, (int)stream.Length );
                }
            }
            return _sharedKeyBytes;
        }

    }
}
