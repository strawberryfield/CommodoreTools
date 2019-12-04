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

namespace Casasoft.Commodore.Disk
{
    /// <summary>
    /// List of files in the disk image
    /// </summary>
    public class Directory
    {
        private List<DirectoryEntry> dir;

        /// <summary>
        /// Constructor
        /// </summary>
        public Directory()
        {
            dir = new List<DirectoryEntry>();
        }

        /// <summary>
        /// Loads directory from disk image database
        /// </summary>
        /// <param name="disk">disk image to read</param>
        /// <param name="track">directory start track</param>
        /// <param name="sector">directory start sector</param>
        public void Load(BaseDisk disk, int track, int sector)
        {
            byte[] sectorData = disk.GetSector(track, sector);

            for (int j = 0; j < 8; j++)
            {
                byte[] entryData = new byte[32];
                Array.Copy(sectorData, j * 32, entryData, 0, 32);
                addEntry(entryData, track, sector, j);
            }

            if (sectorData[0] == 0) return;
            Load(disk, sectorData[0], sectorData[1]);
        }

        private void addEntry(byte[] data, int track, int sector, int record)
        {
            if (data[2] != 0)
            {
                DirectoryEntry entry = new DirectoryEntry(data);
                entry.Reference.track = track;
                entry.Reference.sector = sector;
                entry.Reference.entry = record;
                dir.Add(entry);
            }
        }

        /// <summary>
        /// Gets Directory Entry by File Name
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public DirectoryEntry GetEntry(string filename)
        {
            return dir.Find(entry => entry.Filename == filename);
        }

        /// <summary>
        /// Gets Directory Entry by Index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public DirectoryEntry GetEntry(Int32 index)
        {
            return dir.Find(entry => entry.Index == index);
        }

        /// <summary>
        /// Prints directory list to string
        /// </summary>
        /// <returns>Formatted files list</returns>
        public override string ToString()
        {
            string ret = string.Empty;

            foreach (DirectoryEntry de in dir)
            {
                ret += de.ToString() + "\n";
            }
            return ret;
        }

        /// <summary>
        /// Inserts entries in a datatable
        /// </summary>
        /// <param name="dt">table to fill</param>
        public void ToDataTable(dsDisk.DirectoryDataTable dt)
        {
            dt.Rows.Clear();
            foreach (DirectoryEntry de in dir)
            {
                dsDisk.DirectoryRow row = de.GetDataRow(dt);
                dt.AddDirectoryRow(row);
            }

        }
    }
}
