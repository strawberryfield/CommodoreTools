/// @file
/// BAM base class.
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

namespace Casasoft.Commodore.Disk
{
    /// <summary>
    /// BAM base class
    /// </summary>
    public class BAMbase
    {
        #region properties
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public byte DirectoryTrack;
        public byte DirectorySector;
        public byte BAMTrack;
        public byte BAMSector;
        public bool DoubleSide;
        public char DOSversion;
        public string DiskName;
        public char[] DiskId;
        public char[] DOStype;
        public List<BAMentry> SectorsMap;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        /// <summary>
        /// number of sectors in each track
        /// </summary>
        public List<byte> diskStructure { get; protected set; }

        /// <summary>
        /// total number of sectors in disk (reserved included)
        /// </summary>
        public UInt16 totalSectors { get; protected set; }

        /// <summary>
        /// total number of tracks in disk (reserved included)
        /// </summary>
        public byte totalTracks => (byte)diskStructure.Count;

        /// <summary>
        /// Size of BAM entries in bytes
        /// </summary>
        public byte EntrySize { get; protected set; }

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public BAMbase()
        {
            DOStype = new char[2] { '2', 'A' };
            DiskId = new char[2] { '0', '0' };
            DiskName = string.Empty;
            SectorsMap = new List<BAMentry>();
            diskStructure = new List<byte>();
            DOSversion = 'A';
        }

        /// <summary>
        /// Add track info to structure
        /// </summary>
        /// <param name="sectors">number of sectors in the track</param>
        protected void addTrackStructure(byte sectors)
        {
            diskStructure.Add(sectors);
            totalSectors += sectors;
        }

        #region Load
        /// <summary>
        /// Load BAM data from disk image
        /// </summary>
        /// <param name="disk">Disk image to read</param>
        /// <remarks>Empty virtual method</remarks>
        public virtual void Load(BaseDisk disk)
        {
        }

        /// <summary>
        /// Load header from disk image
        /// </summary>
        /// <param name="disk">Disk image to read</param>
        /// <param name="track">Track of disk header</param>
        /// <param name="sector">Secton inside track of disk header</param>
        protected virtual void LoadHeader(BaseDisk disk, int track, int sector)
        {
            byte[] data = disk.GetSector(track, sector);

            DirectoryTrack = data[0];
            DirectorySector = data[1];
            DOSversion = (char)data[2];
            DiskName = string.Empty;
            for (int j = 0x90; j <= 0x9F; j++)
            {
                DiskName += (char)data[j];
            }
            DiskId[0] = (char)data[0xA2];
            DiskId[1] = (char)data[0xA3];
            DOStype[0] = (char)data[0xA5];
            DOStype[1] = (char)data[0xA6];
        }

        /// <summary>
        /// Disk name in bytes array
        /// </summary>
        /// <returns></returns>
        protected byte[] DiskLabel()
        {
            byte[] ret = new byte[16];
            string s = DiskName.Trim().PadRight(16, (char)0xA0);
            for (int j = 0; j < 16; ++j) ret[j] = (byte)s[j];
            return ret;
        }

        /// <summary>
        /// Binary sectors map
        /// </summary>
        /// <returns></returns>
        protected byte[] RawMap()
        {
             return RawMap(1, totalTracks);
        }

        /// <summary>
        /// Binary sectors map
        /// </summary>
        /// <param name="firstTrack"></param>
        /// <param name="lastTrack"></param>
        /// <returns></returns>
        protected byte[] RawMap(byte firstTrack, byte lastTrack)
        {
            int nTracks = lastTrack - firstTrack + 1;
            byte[] ret = new byte[nTracks * EntrySize];
            for (int j = 0; j < nTracks; ++j)
            {
                int offset = j * EntrySize;
                BAMentry be = SectorsMap[j + firstTrack - 1];
                ret[offset] = be.FreeSectors;
                Array.Copy(be.flags, 0, ret, offset + 1, EntrySize - 1);
            }
            return ret;
        }

        /// <summary>
        /// Load sector map
        /// </summary>
        /// <param name="data">raw bytes of map</param>
        /// <param name="entrySize">size (in bytes) of a single track entry</param>
        protected void loadMap(byte[] data, int entrySize)
        {
            for (int j = 0; j < data.Length; j += entrySize)
            {
                byte[] entry = new byte[entrySize];
                Array.Copy(data, j, entry, 0, entrySize);
                SectorsMap.Add(new BAMentry(entry));
            }
        }
        #endregion

        /// <summary>
        /// Frees all sectors
        /// </summary>
        public void ClearBAM()
        {
            SectorsMap.Clear();
            foreach (byte b in diskStructure) SectorsMap.Add(new BAMentry(EntrySize - 1, b));
        }

        /// <summary>
        /// Saves BAM data to disk image
        /// </summary>
        /// <param name="disk">Disk image to write</param>
        /// <remarks>Empty virtual method</remarks>
        public virtual void Save(BaseDisk disk)
        {
        }

        /// <summary>
        /// Return total free sectors number
        /// </summary>
        /// <returns></returns>
        public int GetFreeSectorCount()
        {
            int ret = 0;
            for (int j = 1; j <= SectorsMap.Count; j++)
            {
                if (j != DirectoryTrack) ret += SectorsMap[j - 1].FreeSectors;
            }
            return ret;
        }

        /// <summary>
        /// Print directory header (disk name)
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0,-16}  {1}{2}  {3}{4}",
                new object[] { DiskName, DOStype[0], DOStype[1], DiskId[0], DiskId[1] });
        }

        /// <summary>
        /// Return formatted sectors free
        /// </summary>
        /// <returns></returns>
        public string PrintFreeSectors()
        {
            return string.Format("{0,4} sectors free", GetFreeSectorCount());
        }
    }
}
