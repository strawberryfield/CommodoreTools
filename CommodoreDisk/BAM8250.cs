﻿/// @file
/// BAM for 8250 disks class.
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
    /// BAM for 8250 disks (double side, 77+77 tracks)
    /// </summary>
    public class BAM8250 : BAM8050
    {       
        /// <summary>
        /// Constructor
        /// </summary>
        public BAM8250() : base()
        {
            SingleSideStructure();
            SectorsMap[BAMtrack].ResetFlag(6); // BAM sector on 38/0
            SectorsMap[BAMtrack].ResetFlag(9); // BAM sector on 38/3

        }

        /// <summary>
        /// Load BAM data from disk image
        /// </summary>
        /// <param name="disk">Disk image to read</param>
        public override void Load(BaseDisk disk)
        {
            LoadHeader(disk, 39, 0);
            SectorsMap.Clear();
            loadPartialBAM(disk, BAMtrack, 0);
            loadPartialBAM(disk, BAMtrack, 3);
            loadPartialBAM(disk, BAMtrack, 6);
            loadPartialBAM(disk, BAMtrack, 9);
        }

        /// <summary>
        /// Saves BAM data to disk image
        /// </summary>
        /// <param name="disk">Disk image to write</param>
        public override void Save(BaseDisk disk)
        {
            SaveHeader(disk);

            byte[] data = BAMsector(1, 50);
            data[0] = BAMtrack;
            data[1] = 3;
            disk.PutSector(BAMtrack, 0, data);

            data = BAMsector(51, 100);
            data[0] = BAMtrack;
            data[1] = 6;
            disk.PutSector(BAMtrack, 3, data);

            data = BAMsector(51, 150);
            data[0] = BAMtrack;
            data[1] = 9;
            disk.PutSector(BAMtrack, 6, data);

            data = BAMsector(151, 154);
            data[0] = Directory.Track;
            data[1] = Directory.Sector;
            disk.PutSector(BAMtrack, 9, data);

        }

    }
}
