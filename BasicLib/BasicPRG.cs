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
using System.Linq;
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
        protected List<BasicRow> Lines;

        /// <summary>
        /// Pointer of loading area
        /// </summary>
        public UInt16 StartPtr;

        #region constructors
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
        #endregion

        #region lines management
        /// <summary>
        /// Gets line by number
        /// </summary>
        /// <param name="LineNum"></param>
        /// <returns>null if not found</returns>
        public BasicRow GetLine(UInt16 LineNum)
        {
            return Lines.SingleOrDefault(x => x.LineNumber == LineNum);
        }

        /// <summary>
        /// Deletes line by number
        /// </summary>
        /// <param name="LineNum"></param>
        /// <returns>true if line deleted, false if not found</returns>
        public bool DeleteLine(UInt16 LineNum)
        {
            BasicRow r = GetLine(LineNum);
            if (r != null)
            {
                Lines.Remove(r);
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Adds a new row of code (or replaces existing)
        /// </summary>
        /// <param name="LineNum"></param>
        /// <param name="rawdata"></param>
        public void AddLine(UInt16 LineNum, byte[] rawdata)
        {
            BasicRow r = GetLine(LineNum);
            if (r != null) Lines.Remove(r);

            r = new BasicRow(0, LineNum, rawdata);
            Lines.Add(r);
        }
        #endregion

        #region lines arrangements
        /// <summary>
        /// Adjusts rows pointers
        /// </summary>
        protected void AdjustPointers()
        {
            UInt16 ptr = StartPtr;
            foreach(BasicRow r in Lines)
            {
                ptr += Convert.ToUInt16(r.Code.Count() + 5);
                r.NextRowAddress = ptr;
            }
        }

        /// <summary>
        /// Sorts lines by number
        /// </summary>
        protected void Sort()
        {
            Lines.Sort((x, y) => x.LineNumber.CompareTo(y.LineNumber));
        }
        #endregion

        /// <summary>
        /// List in plain text
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            Sort();
            StringBuilder sb = new StringBuilder();
            foreach(BasicRow row in Lines)
            {
                sb.Append(row.ToString());
                sb.Append("\n");
            }
            return sb.ToString();
        }

        /// <summary>
        /// Total length of the program in Comodore format
        /// </summary>
        public int RawLength
        {
            get
            {
                int ret = 4;
                foreach (BasicRow row in Lines) ret += row.RawLength;
                return ret;
            }
        }

        /// <summary>
        /// Exports the whole program in raw Commodore format
        /// </summary>
        /// <returns></returns>
        public byte[] ToRaw()
        {
            Sort();
            AdjustPointers();
            byte[] ret = new byte[RawLength];
            ret[0] = Convert.ToByte(StartPtr % 256);
            ret[1] = Convert.ToByte(StartPtr / 256);

            // Appends all lines
            int ptr = 2;
            foreach (BasicRow row in Lines)
            {
                Array.Copy(row.ToRaw(), 0, ret, ptr, row.RawLength);
                ptr += row.RawLength;
            }

            ret[ptr] = 0;
            ret[ptr + 1] = 0;
            return ret;
        }
    }
}
