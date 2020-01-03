/// @file
/// Single line of Basic PRG class.
/// This file is part of Casasoft Commodore Utilities
/// 
/// @author
/// copyright (c) 2019-2020 Roberto Ceccarelli - Casasoft  
/// http://strawberryfield.altervista.org 
/// 
/// @copyright
/// Casasoft Commodore Utilities is free software: 
/// you can redistribute it and/or modify it
/// under the terms of the GNU General Public License as published by
/// the Free Software Foundation, either version 3 of the License, or
/// (at your option) any later version.
/// 
/// You should have received a copy of the GNU General Public License
/// along with Casasoft Commodore Utilities.  
/// If not, see http://www.gnu.org/licenses/
/// 
/// @remark
/// Casasoft Commodore Utilities is distributed in the hope that it will be useful,
/// but WITHOUT ANY WARRANTY; without even the implied warranty of
/// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  
/// See the GNU General Public License for more details.
/// 

using System;
using System.Collections.Generic;
using System.Text;

namespace Casasoft.Commodore.Basic
{
    /// <summary>
    /// Single line of Basic PRG class.
    /// </summary>
    public class BasicRow
    {
        /// <summary>
        /// Pointer to next row
        /// </summary>
        public UInt16 NextRowAddress;

        /// <summary>
        /// Row number
        /// </summary>
        public UInt16 LineNumber;

        /// <summary>
        /// Tokenized row
        /// </summary>
        public List<byte> Code;

        /// <summary>
        /// Constructor
        /// </summary>
        public BasicRow() 
        {
            NextRowAddress = 0;
            LineNumber = 0;
            Code = new List<byte>();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Line"></param>
        /// <param name="Source"></param>
        public BasicRow(UInt16 Line, string Source) : this()
        {
            for (int j = 0; j < Source.Length; j++)
            {
                Code.Add((byte)Source[j]);
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Ptr"></param>
        /// <param name="Line"></param>
        /// <param name="rawdata"></param>
        public BasicRow(UInt16 Ptr, UInt16 Line, byte[] rawdata) : this()
        {
            NextRowAddress = Ptr;
            LineNumber = Line;
            Code.AddRange(rawdata);
        }

        /// <summary>
        /// Formats row in plain text
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(LineNumber.ToString());
            for(int j=0; j<Code.Count; j++)
            {
                char c = (char)Code[j];
                if (c<128)
                {
                    sb.Append(c);
                } 
                else
                {
                    sb.Append(Tokens.List[c]);
                }
            }
            return sb.ToString();
        }
    }
}
