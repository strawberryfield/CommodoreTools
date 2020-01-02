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
        /// Constructor
        /// </summary>
        public BAM8050() : base()
        {
        }

        /// <summary>
        /// Load BAM data from disk image
        /// </summary>
        /// <param name="disk">Disk image to read</param>
        public override void Load(BaseDisk disk)
        {
            LoadHeader(disk, 39, 0);
            loadPartialBAM(disk, 38, 0);
            loadPartialBAM(disk, 38, 3);
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

    }
}
