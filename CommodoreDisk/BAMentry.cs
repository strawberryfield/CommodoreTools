/// @file
/// Generic BAM entry class.
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

namespace Casasoft.Commodore.Disk
{
    /// <summary>
    /// Single track sector map
    /// </summary>
    public class BAMentry
    {
        /// <summary>
        /// Free sectors in track
        /// </summary>
        public int FreeSectors { get; protected set; }

        /// <summary>
        /// free sectors flags
        /// </summary>
        public byte[] flags { get; protected set; }

        /// <summary>
        /// size of the entry in bytes
        /// </summary>
        public readonly int EntrySize;

        /// <summary>
        /// Build an empty entry
        /// </summary>
        /// <param name="size">size of track map in BAM table</param>
        public BAMentry(int size)
        {
            flags = new byte[size];
            EntrySize = size + 1;
        }

        /// <summary>
        /// Build an entry from raw data
        /// </summary>
        /// <param name="data">raw entry data</param>
        public BAMentry(byte[] data) : this(data.Length - 1)
        {
            FreeSectors = data[0];
            Array.Copy(data, 1, flags, 0, data.Length - 1);
        }

        /// <summary>
        /// Get status of sector
        /// </summary>
        /// <param name="sector">sector number in track</param>
        /// <returns>true if sector is free</returns>
        public bool GetFlag(int sector)
        {
            int idx = sector / 8;
            int bit = 7 - (sector % 8);  //reverse bit order
            return (flags[idx] & (1 << bit)) != 0;
        }

        /// <summary>
        /// marks a sector as used
        /// </summary>
        /// <param name="sector"></param>
        public void SetFlag(int sector)
        {
            int idx = sector / 8;
            int bit = 7 - (sector % 8);  //reverse bit order
            flags[idx] |= (byte)(1 << bit);
            FreeSectors--;
        }

        /// <summary>
        /// marks a sector as free
        /// </summary>
        /// <param name="sector"></param>
        public void ResetFlag(int sector)
        {
            int idx = sector / 8;
            int bit = 7 - (sector % 8);  //reverse bit order
            flags[idx] &= (byte)(~(1 << bit));
            FreeSectors++;
        }

    }
}
