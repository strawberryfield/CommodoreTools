// copyright (c) 2019 Roberto Ceccarelli - Casasoft
// http://strawberryfield.altervista.org 
// 
// This file is part of Casasoft Commodore Utilities
// 
// Casasoft Commodore Utilities is free software: 
// you can redistribute it and/or modify it
// under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Casasoft Commodore Utilities is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  
// See the GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Casasoft Commodore Utilities.  
// If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.IO;

namespace Casasoft.Commodore.Disk
{
    /// <summary>
    /// Base disk manager
    /// </summary>
    public class BaseDisk
    {
        #region fields
        /// <summary>
        /// sectors raw data
        /// </summary>
        protected List<List<byte[]>> diskData;

        /// <summary>
        /// number of sectors in each track
        /// </summary>
        protected List<int> diskStructure;

        /// <summary>
        /// bytes in each sector
        /// </summary>
        public int sectorSize => 256;

        /// <summary>
        /// total number of sectors in disk (reserved included)
        /// </summary>
        public int totalSectors { get; protected set; }

        /// <summary>
        /// total number of tracks in disk (reserved included)
        /// </summary>
        public int totalTracks => diskStructure.Count;

        /// <summary>
        /// Root directory
        /// </summary>
        public Directory RootDir { get; protected set; }

        /// <summary>
        /// Disk header and blocks table
        /// </summary>
        public BAMbase Header { get; protected set; }
        #endregion

        #region init
        /// <summary>
        /// Constructor
        /// </summary>
        public BaseDisk()
        {
            diskData = new List<List<byte[]>>();
            diskStructure = new List<int>();
            RootDir = new Directory();
            totalSectors = 0;
        }

        /// <summary>
        /// Create database container
        /// </summary>
        protected void initDiskData()
        {
            for (int j = 1; j <= diskStructure.Count; j++)
            {
                addEmptyTrack(diskStructure.ToArray()[j - 1]);
            }
        }

        /// <summary>
        /// Add track info to structure
        /// </summary>
        /// <param name="sectors">number of sectors in the track</param>
        protected void addTrackStructure(int sectors)
        {
            diskStructure.Add(sectors);
            totalSectors += sectors;
        }

        /// <summary>
        /// Add a blank track to database
        /// </summary>
        /// <param name="sectors">number of sectors in the track</param>
        protected void addEmptyTrack(int sectors)
        {
            List<byte[]> track = new List<byte[]>();
            for (int j = 0; j < sectors; j++)
            {
                track.Add(new byte[sectorSize]);
            }
            diskData.Add(track);
        }
        #endregion

        #region sectors management
        /// <summary>
        /// Get track by number
        /// </summary>
        /// <param name="track">track number</param>
        /// <returns>Track sectors list</returns>
        protected List<byte[]> getTrack(int track)
        {
            return diskData.ToArray()[track - 1];
        }

        /// <summary>
        /// Get sector raw data
        /// </summary>
        /// <param name="track">track number</param>
        /// <param name="sector">sector number in the track</param>
        /// <returns>raw sector content</returns>
        public byte[] GetSector(int track, int sector)
        {
            return getTrack(track).ToArray()[sector];
        }

        /// <summary>
        /// Put raw data in a sector
        /// </summary>
        /// <param name="track">track number</param>
        /// <param name="sector">sector number in the track</param>
        /// <param name="data">raw sector content</param>
        protected void putSector(int track, int sector, byte[] data)
        {
            data.CopyTo(getTrack(track).ToArray()[sector], 0);
        }
        #endregion

        #region Load
        /// <summary>
        /// Load disk image from file
        /// </summary>
        /// <param name="filename">file to load</param>
        public void Load(string filename)
        {
            using (BinaryReader reader = new BinaryReader(File.Open(filename, FileMode.Open)))
            {
                for (int track = 1; track <= diskStructure.Count; track++)
                {
                    for (int sector = 0; sector < diskStructure.ToArray()[track - 1]; sector++)
                    {
                        byte[] sectorData = reader.ReadBytes(sectorSize);
                        putSector(track, sector, sectorData);
                    }
                }
            }
            LoadBAM();
            LoadRoot();
        }

        /// <summary>
        /// Load root directory
        /// </summary>
        /// <remarks>empty virtual method</remarks>
        protected virtual void LoadRoot()
        {
        }

        /// <summary>
        /// Load disk header and free sectors map
        /// </summary>
        /// <remarks>empty virtual method</remarks>
        protected virtual void LoadBAM()
        {
        }
        #endregion

        #region Get File
        /// <summary>
        /// Gets a file by File Name
        /// </summary>
        /// <param name="filename"></param>
        /// <returns>file in byte array</returns>
        public byte[] GetFile(string filename)
        {
            return GetFile(RootDir.GetEntry(filename));
        }

        /// <summary>
        /// Gets a file by Index
        /// </summary>
        /// <param name="index"></param>
        /// <returns>file in byte array</returns>
        public byte[] GetFile(Int32 index)
        {
            return GetFile(RootDir.GetEntry(index));
        }

        /// <summary>
        /// Gets a file by Directory Entry
        /// </summary>
        /// <param name="file">Diretory entry of file to retrieve</param>
        /// <returns>file in byte array</returns>
        public byte[] GetFile(DirectoryEntry file)
        {
            List<byte> ret = new List<byte>();
            scanFile(ret, file.FirstTrack, file.FirstSector);
            return ret.ToArray();
        }

        private void scanFile(List<byte> prev, int track, int sector)
        {
            byte[] data = GetSector(track, sector);
            int nextTrack = data[0];
            int nextSector = data[1];
            byte[] payload;
            if (nextTrack == 0)
            {
                payload = new byte[nextSector];
                Array.Copy(data, 2, payload, 0, nextSector);
                prev.AddRange(payload);
            }
            else
            {
                payload = new byte[254];
                Array.Copy(data, 2, payload, 0, 254);
                prev.AddRange(payload);
                scanFile(prev, nextTrack, nextSector);
            }
        }
        #endregion

        /// <summary>
        /// Complete directory 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}\n{1}{2}", Header.ToString(), RootDir.ToString(), Header.PrintFreeSectors());
        }
    }
}
