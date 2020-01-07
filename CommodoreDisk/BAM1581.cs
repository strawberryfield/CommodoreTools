/// @file
/// BAM for 1581 disks class.
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
    /// BAM for 1581 disks (double side, 80 tracks, 40 sectors/track)
    /// </summary>
    public class BAM1581 : BAMbase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public BAM1581() : base()
        {
            for (int j = 1; j <= 80; j++) addTrackStructure(40);
            EntrySize = 6;
            DirectoryTrack = 40;
            DirectorySector = 3;
            DOSversion = 'D';
            DOStype[0] = '3';
            DOStype[1] = 'D';
            SectorsMap[DirectoryTrack - 1].ResetFlag(0); // Header sector on 40/0
            SectorsMap[DirectoryTrack - 1].ResetFlag(1); // BAM sector on 40/1
            SectorsMap[DirectoryTrack - 1].ResetFlag(2); // BAM sector on 40/2
        }

        /// <summary>
        /// Load BAM data from disk image
        /// </summary>
        /// <param name="disk">Disk image to read</param>
        public override void Load(BaseDisk disk)
        {
            LoadHeader(disk, 40, 0);

            const int tracks = 40;

            byte[] table = new byte[tracks * 6];
            byte[] data = disk.GetSector(40, 1);
            Array.Copy(data, 0x10, table, 0, tracks * 6);
            loadMap(table, 6);
            data = disk.GetSector(40, 2);
            Array.Copy(data, 0x10, table, 0, tracks * 6);
            loadMap(table, 6);
        }

        /// <summary>
        /// Read disk header
        /// </summary>
        /// <param name="disk">disk to analyze</param>
        /// <param name="track">header track</param>
        /// <param name="sector">header sector</param>
        protected override void LoadHeader(BaseDisk disk, int track, int sector)
        {
            base.LoadHeader(disk, track, sector);
            byte[] data = disk.GetSector(track, sector);
            DOStype[0] = (char)data[0xA5];
            DOStype[1] = (char)data[0xA6];
        }

        /// <summary>
        /// Saves BAM data to disk image
        /// </summary>
        /// <param name="disk">Disk image to write</param>
        public override void Save(BaseDisk disk)
        {
            byte[] data = BaseDisk.EmptySector();
            data[0] = DirectoryTrack;
            data[1] = DirectorySector;
            data[2] = (byte)DOSversion;
            data[0x14] = 0xA0;
            data[0x15] = 0xA0;
            data[0x16] = (byte)DiskId[0];
            data[0x17] = (byte)DiskId[1];
            data[0x18] = 0xA0;
            data[0x19] = (byte)DOStype[0];
            data[0x1A] = (byte)DOStype[1];
            data[0x1B] = 0xA0;
            data[0x1C] = 0xA0;
            Array.Copy(DiskLabel(), 0, data, 4, 16);
            disk.PutSector(DirectoryTrack, 0, data);

            data = BAMsectorHeader();
            data[0] = DirectoryTrack;
            data[1] = 2;
            Array.Copy(RawMap(41, 80), 0, data, 16, 40 * EntrySize);
            disk.PutSector(DirectoryTrack, 1, data);

            data = BAMsectorHeader();
            data[0] = 0;
            data[1] = 0xFF;
            Array.Copy(RawMap(1, 40), 0, data, 16, 40 * EntrySize);
            disk.PutSector(DirectoryTrack, 2, data);
        }

        private byte[] BAMsectorHeader()
        {
            byte[] data = BaseDisk.EmptySector();
            data[2] = (byte)DOSversion;
            data[3] = (byte)~DOSversion;
            data[4] = (byte)DiskId[0];
            data[5] = (byte)DiskId[1];
            data[6] = 0; // No checks
            data[7] = 0; // No autorun

            return data;
        }

    }
}
