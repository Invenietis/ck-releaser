#region LGPL License
/*----------------------------------------------------------------------------
* This file (CK.Releaser\Tools\CommentReader.cs) is part of CiviKey. 
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
using System.Linq;
using System.Text;
using System.IO;

namespace CK.Releaser.Tools
{
    class CommentReader
    {
        [Flags]
        enum Position
        {
            BeginOfFile = 0,
            StarComment = 1,
            SlashComment = 3,
            NormalLine = 8,
            EndOfFile = 16
        }
        TextReader _r;
        Position _p;
        string _line;

        public CommentReader( TextReader r )
        {
            _r = r;
            _p = Position.BeginOfFile;
        }

        void ForwardHead()
        {
            _line = _r.ReadLine();
            if( _line == null )
            {
                _p = Position.EndOfFile;
            }
            else
            {
                _line = _line.Trim();
                if( _p == Position.StarComment )
                {
                    if( HandleEndOfStarComment( ref _line, 0 ) ) _p = Position.NormalLine;
                }
                else
                {
                    if( _line.StartsWith( "//" ) )
                    {
                        _p = Position.SlashComment;
                        _line = _line.Substring( 2 ).TrimStart();
                    }
                    else if( _line.StartsWith( "/*" ) )
                    {
                        _p = HandleEndOfStarComment( ref _line, 2 ) ? Position.NormalLine : Position.StarComment;
                    }
                }
            }
        }

        bool HandleEndOfStarComment( ref string line, int index )
        {
            int iEnd = line.IndexOf( "*/", index );
            if( iEnd >= 0 )
            {
                if( iEnd > 0 ) line = line.Substring( index, iEnd - index ).TrimStart( ' ', '*' ).TrimEnd();
                return true;
            }
            if( index > 0 ) line = line.Substring( index );
            line = line.TrimStart( ' ', '*' );
            return false;
        }

        public string NextCommentLine()
        {
            ForwardHead();
            return ((_p & Position.StarComment) != 0) ? _line : null;
        }
    }
}
