/// @file
/// Disk directory class.
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
    /// List of files in the disk image
    /// </summary>
    public class Directory
    {
        private List<DirectoryEntry> dir;

        /// <summary>
        /// First track/sector for dir
        /// </summary>
        public SectorId First { get; protected set; }

        /// <summary>
        /// Entries per sector
        /// </summary>
        public readonly int EntriesPerSector;

        /// <summary>
        /// Constructor
        /// </summary>
        public Directory()
        {
            dir = new List<DirectoryEntry>();
            EntriesPerSector = BaseDisk.sectorSize / DirectoryEntry.EntrySize;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="firstTrack"></param>
        /// <param name="firstSector"></param>
        public Directory(byte firstTrack, byte firstSector) : this(new SectorId(firstTrack, firstSector))
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="first">first track/sector</param>
        public Directory(SectorId first) : this()
        {
            First = first;
        }

        /// <summary>
        /// Loads directory from disk image database
        /// </summary>
        /// <param name="disk">disk image to read</param>
        /// <param name="id">directory start track/sector</param>
        public void Load(BaseDisk disk, SectorId id)
        {
            byte[] sectorData = disk.GetSector(id);

            for (byte j = 0; j < EntriesPerSector; j++)
            {
                byte[] entryData = new byte[DirectoryEntry.EntrySize];
                Array.Copy(sectorData, j * DirectoryEntry.EntrySize, entryData, 0, DirectoryEntry.EntrySize);
                addEntry(entryData, id, j);
            }

            if (sectorData[0] == 0) return;
            Load(disk, new SectorId(sectorData[0], sectorData[1]));
        }

        /// <summary>
        /// Loads directory from disk image database using standard parameters
        /// </summary>
        /// <param name="disk">disk image to read</param>
        public void Load(BaseDisk disk)
        {
            Load(disk, First);
        }

        private void addEntry(byte[] data, SectorId id, byte record)
        {
            if (data[2] != 0)
            {
                DirectoryEntry entry = new DirectoryEntry(data);
                entry.Reference.track = id.Track;
                entry.Reference.sector = id.Sector;
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

        /// <summary>
        /// Directory as a list of raw sector data
        /// </summary>
        /// <returns></returns>
        public List<byte[]> ToRaw()
        {
            List<byte[]> ret = new List<byte[]>();
            int sectors = (dir.Count + EntriesPerSector - 1) / EntriesPerSector;
            // see https://stackoverflow.com/questions/17944/how-to-round-up-the-result-of-integer-division
            for (int j = 0; j < sectors; j++)
            {
                byte[] s = BaseDisk.EmptySector();
                for (int e = 0; e < EntriesPerSector && j * EntriesPerSector + e < dir.Count; ++e)
                {
                    Array.Copy(dir[j * EntriesPerSector + e].ToRaw(), 0,
                        s, e * DirectoryEntry.EntrySize, DirectoryEntry.EntrySize);
                }
            }
            return ret;
        }

        /// <summary>
        /// Puts directory in disk sectors
        /// </summary>
        /// <param name="disk"></param>
        public void Save(BaseDisk disk)
        {
            disk.Header.FreeSectorsOnDirectoryTrack();
            List<byte[]> rawData = ToRaw();
            byte prevSect = disk.Header.Directory.Sector;
            if (rawData.Count > 0)
            {
                disk.PutSector(disk.Header.Directory.Track, prevSect, rawData[0]);
                for (int j = 1; j < rawData.Count; ++j)
                {
                    int sect = disk.Header.GetAFreeSector(disk.Header.Directory.Track);
                    if(sect > 0)
                    {
                        disk.GetSector(disk.Header.Directory.Track, prevSect)[0] = disk.Header.Directory.Track;
                        disk.GetSector(disk.Header.Directory.Track, prevSect)[0] = (byte)sect;
                        prevSect = (byte)sect;
                        disk.PutSector(disk.Header.Directory.Track, sect, rawData[j]);
                    }
                }
            }
        }

    }
}
