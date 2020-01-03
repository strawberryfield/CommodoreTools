/// @file
/// PRG as a collection of rows.
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
    /// Structured form of tokenized lines
    /// </summary>
    public class BasicPRG
    {
        /// <summary>
        /// Code lines
        /// </summary>
        public List<BasicRow> Lines;

        /// <summary>
        /// Pointer of loading area
        /// </summary>
        public UInt16 StartPtr;

        /// <summary>
        /// Constructor for empty program
        /// </summary>
        public BasicPRG()
        {
            Lines = new List<BasicRow>();
            StartPtr = 8 * 256 + 1;
        }

        /// <summary>
        /// Loads the program from file image
        /// </summary>
        /// <param name="file"></param>
        public BasicPRG(byte[] file) : this()
        {
            StartPtr = (UInt16)(file[0] + 256 * file[1]);
            for (int j = 1; j < file.Length;)
            {
                // gets pointer to next line: if 0 exit 
                UInt16 ptr = (UInt16)(file[++j] + 256 * file[++j]);
                if (0 == ptr) break;

                // gets line number
                UInt16 linenum = (UInt16)(file[++j] + 256 * file[++j]);

                // scan for EOL
                int start = j+1;
                while (file[++j] != 0)
                {
                }
                byte[] data = new byte[j - start];
                Array.Copy(file, start, data, 0, j - start);
                BasicRow row = new BasicRow(ptr, linenum, data);
                Lines.Add(row);
            }
        }

        /// <summary>
        /// List in plain text
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach(BasicRow row in Lines)
            {
                sb.Append(row.ToString());
                sb.Append("\n");
            }
            return sb.ToString();
        }
    }
}
