/// @file
/// Track and sector pointer.
/// This file is part of Casasoft Commodore Utilities
/// 
/// @author
/// copyright (c) 2020 Roberto Ceccarelli - Casasoft  
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

namespace Casasoft.Commodore.Disk
{
    /// <summary>
    /// Track/Sector couple container
    /// </summary>
    public class SectorId
    {
        /// <summary>
        /// Track number
        /// </summary>
        public byte Track;

        /// <summary>
        /// Sector number
        /// </summary>
        public byte Sector;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="track"></param>
        /// <param name="sector"></param>
        public SectorId(byte track, byte sector)
        {
            Track = track;
            Sector = sector;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="track"></param>
        /// <param name="sector"></param>
        public SectorId(int track, int sector) : this((byte)track, (byte)sector) { }
    }
}
