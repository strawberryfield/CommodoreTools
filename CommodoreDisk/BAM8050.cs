/// @file
/// BAM for 8050 disks class.
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
    /// BAM for 8050 disks (single side, 77 tracks)
    /// </summary>
    public class BAM8050 : BAMbase
    {
        /// <summary>
        /// Track for BAM
        /// </summary>
        public const byte BAMtrack = 38;

        /// <summary>
        /// Constructor
        /// </summary>
        public BAM8050() : base()
        {
            EntrySize = 5;
            SingleSideStructure();
            DirectoryTrack = 39;
            DirectorySector = 1;
            SectorsMap[DirectoryTrack - 1].ResetFlag(0); // Header sector on 39/0
            SectorsMap[BAMtrack].ResetFlag(0); // BAM sector on 38/0
            SectorsMap[BAMtrack].ResetFlag(3); // BAM sector on 38/3
            DOSversion = 'C';
            DOStype[0] = '2';
            DOStype[1] = 'C';
        }

        /// <summary>
        /// Structure for single side 
        /// </summary>
        protected void SingleSideStructure()
        {
            for (int j = 1; j <=  39; j++) addTrackStructure(29);
            for (int j = 40; j <= 53; j++) addTrackStructure(27);
            for (int j = 54; j <= 64; j++) addTrackStructure(25);
            for (int j = 65; j <= 77; j++) addTrackStructure(23);
        }

        /// <summary>
        /// Load BAM data from disk image
        /// </summary>
        /// <param name="disk">Disk image to read</param>
        public override void Load(BaseDisk disk)
        {
            LoadHeader(disk, 39, 0);
            loadPartialBAM(disk, BAMtrack, 0);
            loadPartialBAM(disk, BAMtrack, 3);
        }

        /// <summary>
        /// Load partial BAM
        /// </summary>
        /// <param name="disk">Disk to read</param>
        /// <param name="track">BAM track</param>
        /// <param name="sector">BAM sector</param>
        protected void loadPartialBAM(BaseDisk disk, int track, int sector)
        {
            byte[] data = disk.GetSector(track, sector);
            int firstTrack = data[4];
            int lastTrack = data[5];
            int tracks = lastTrack - firstTrack;

            byte[] table = new byte[tracks * 5];
            Array.Copy(data, 6, table, 0, tracks * 5);
            loadMap(table, 5);
        }

        /// <summary>
        /// Read disk header
        /// </summary>
        /// <param name="disk">disk to analyze</param>
        /// <param name="track">header track</param>
        /// <param name="sector">header sector</param>
        protected override void LoadHeader(BaseDisk disk, int track, int sector)
        {
            byte[] data = disk.GetSector(track, sector);

            BAMTrack = data[0];
            BAMSector = data[1];
            DirectoryTrack = 39;
            DirectorySector = 1;
            DOSversion = (char)data[2];
            DiskName = string.Empty;
            for (int j = 0x06; j <= 0x16; j++)
            {
                DiskName += (char)data[j];
            }
            DiskId[0] = (char)data[0x18];
            DiskId[1] = (char)data[0x19];
            DOStype[0] = (char)data[0x1B];
            DOStype[1] = (char)data[0x1C];
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

            data = BAMsector(51,77);
            data[0] = DirectoryTrack;
            data[1] = DirectorySector;
            disk.PutSector(BAMtrack, 3, data);
        }

        /// <summary>
        /// Writes disk header
        /// </summary>
        /// <param name="disk"></param>
        protected void SaveHeader(BaseDisk disk)
        {
            byte[] data = BaseDisk.EmptySector();
            data[0] = BAMtrack;
            data[1] = 0;
            data[2] = (byte)DOSversion;
            data[0x17] = 0xA0;
            data[0x18] = (byte)DiskId[0];
            data[0x19] = (byte)DiskId[1];
            data[0x1A] = 0xA0;
            data[0x1B] = (byte)DOStype[0];
            data[0x1C] = (byte)DOStype[1];
            data[0x1D] = 0xA0;
            data[0x1E] = 0xA0;
            data[0x1F] = 0xA0;
            data[0x20] = 0xA0;
            Array.Copy(DiskLabel(), 0, data, 6, 16);
            disk.PutSector(DirectoryTrack, 0, data);
        }

        /// <summary>
        /// Single bam sector
        /// </summary>
        /// <param name="firstTrack"></param>
        /// <param name="lastTrack"></param>
        /// <returns></returns>
        protected byte[] BAMsector(byte firstTrack, byte lastTrack)
        {
            byte[] data = BaseDisk.EmptySector();
            data[2] = (byte)DOSversion;
            data[4] = firstTrack;
            data[5] = (byte)(lastTrack + 1);
            Array.Copy(RawMap(firstTrack, lastTrack), 0, data, 6, (lastTrack - firstTrack + 1) * EntrySize);
            return data;
        }


    }
}
