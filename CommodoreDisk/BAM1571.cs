﻿/// @file
/// BAM for 1571 disks class.
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
    /// BAM for 1571 disks (double side, 70 tracks)
    /// </summary>
    public class BAM1571 : BAM1541
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public BAM1571() : base()
        {
            DoubleSide = true;
            SingleSideStructure();
            SectorsMap[53 - 1].ResetFlag(0); //  BAM sector on 53/0
        }

        /// <summary>
        /// Load BAM data from disk image
        /// </summary>
        /// <param name="disk">Disk image to read</param>
        public override void Load(BaseDisk disk)
        {
            LoadHeader(disk, 18, 0);

            const int tracks = 35;
            byte[] data = disk.GetSector(18, 0);
            DoubleSide = (data[3] == 0x80);

            byte[] table = new byte[tracks * 4];
            Array.Copy(data, 4, table, 0, tracks * 4);
            SectorsMap.Clear();
            loadMap(table, 4);

            for (int j = 0; j < tracks; j++)
            {
                table[j * 4] = data[0xDD + j];
            }
            data = disk.GetSector(53, 0);
            for (int j = 0; j < tracks; j++)
            {
                Array.Copy(data, j * 3, table, j * 4, 3);
            }
            loadMap(table, 4);
        }

        /// <summary>
        /// Saves BAM data to disk image
        /// </summary>
        /// <param name="disk">Disk image to write</param>
        public override void Save(BaseDisk disk)
        {
            byte[] data = BaseHeader();
            Array.Copy(RawMap(), 0, data, 4, 35 * EntrySize);
            for(int j = 0; j<35; ++j)
            {
                data[0xDD + j] = SectorsMap[35 + j].FreeSectors;
            }
            disk.PutSector(18, 0, data);

            data = BaseDisk.EmptySector();
            for (int j = 0; j < 35; ++j)
            {
                Array.Copy(SectorsMap[35 + j].flags, 0, data, j * 3, 3);
            }
            disk.PutSector(53, 0, data);

        }

    }
}
