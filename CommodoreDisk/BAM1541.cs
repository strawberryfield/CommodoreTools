/// @file
/// BAM for 1541 disks class.
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
    /// BAM for 1541 disks (sigle side, 35 tracks)
    /// </summary>
    public class BAM1541 : BAMbase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public BAM1541() : base()
        {
            DoubleSide = false;
            SingleSideStructure();
            EntrySize = 4;
            DirectoryTrack = 18;
            DirectorySector = 1;
            SectorsMap[DirectoryTrack - 1].ResetFlag(0); // Header and BAM sector on 18/0
        }

        /// <summary>
        /// Structure for single side 
        /// </summary>
        protected void SingleSideStructure()
        {
            for (int j = 1; j <=  17; j++) addTrackStructure(21);
            for (int j = 18; j <= 24; j++) addTrackStructure(19);
            for (int j = 25; j <= 30; j++) addTrackStructure(18);
            for (int j = 31; j <= 35; j++) addTrackStructure(17);
        }

        /// <summary>
        /// Loads BAM data from disk image
        /// </summary>
        /// <param name="disk">Disk image to read</param>
        public override void Load(BaseDisk disk)
        {
            LoadHeader(disk, 18, 0);

            byte[] data = disk.GetSector(18, 0);

            byte[] table = new byte[totalTracks * 4];
            Array.Copy(data, 4, table, 0, totalTracks * 4);
            loadMap(table, 4);
        }

        /// <summary>
        /// Saves BAM data to disk image
        /// </summary>
        /// <param name="disk">Disk image to write</param>
        public override void Save(BaseDisk disk)
        {
            byte[] data = BaseHeader();
            Array.Copy(RawMap(), 0, data, 4, totalTracks * EntrySize);

            disk.PutSector(18, 0, data);
        }

        /// <summary>
        /// Disk Header
        /// </summary>
        /// <returns></returns>
        protected virtual byte[] BaseHeader()
        {
            byte[] data = BaseDisk.EmptySector();
            data[0] = DirectoryTrack;
            data[1] = DirectorySector;
            data[2] = (byte)DOSversion;
            data[3] = (byte)(DoubleSide ? 0x80 : 0);
            data[0xA0] = 0xA0;
            data[0xA1] = 0xA0;
            data[0xA2] = (byte)DiskId[0];
            data[0xA3] = (byte)DiskId[1];
            data[0xA4] = 0xA0;
            data[0xA5] = (byte)DOStype[0];
            data[0xA6] = (byte)DOStype[1];
            data[0xA7] = 0xA0;
            data[0xA8] = 0xA0;
            data[0xA9] = 0xA0;
            data[0xAA] = 0xA0;
            Array.Copy(DiskLabel(), 0, data, 0x90, 16);
            return data;
        }
    }
}
