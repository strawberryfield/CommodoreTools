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

namespace Casasoft.Commodore.Disk
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    /// <summary>
    /// Type of file
    /// </summary>
    public enum FileType { DEL, SEQ, PRG, USR, REL, CBM }

    /// <summary>
    /// Reference to raw data location
    /// </summary>
    public class DirectoryIndex
    {
        public int track = 0;
        public int sector = 0;
        public int entry = 0;
    }

    /// <summary>
    /// Single directory entry
    /// </summary>
    public class DirectoryEntry
    {
        public FileType Type;
        public bool Locked;
        public bool Closed;
        public int FirstTrack;
        public int FirstSector;
        public string Filename;
        public int RelFirstTrack;
        public int RelFirstSector;
        public int RelRecordLength;
        public int FileSize;
        public DirectoryIndex Reference;
        public Int32 Index { get { return Reference.entry + Reference.sector * 10 + Reference.track * 1000; } }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        /// <summary>
        /// Filter for open/save Dialogs
        /// </summary>
        public static string OpenSaveDialogsFilter => 
            "Program|*.PRG|Sequential|*.SEQ|User defined|*.USR|Random file|*.REL|Deleted|*.DEL|Directory|*.CBM|All files|*.*";

        /// <summary>
        /// Build an empty record
        /// </summary>
        public DirectoryEntry()
        {
            Reference = new DirectoryIndex();
        }

        /// <summary>
        /// Build record from raw data
        /// </summary>
        /// <param name="data">raw entry data</param>
        public DirectoryEntry(byte[] data) : this()
        {
            Closed = !((data[2] & 0x80) != 0);
            Locked = ((data[2] & 0x40) != 0);
            Type = (FileType)(data[2] & 0x07);
            FirstTrack = data[3];
            FirstSector = data[4];
            RelFirstTrack = data[0x15];
            RelFirstSector = data[0x16];
            RelRecordLength = data[0x17];
            FileSize = data[0x1E] + data[0x1F] * 256;
            Filename = string.Empty;
            for (int j = 0x05; j <= 0x14; j++)
            {
                Filename += (char)data[j];
            }
        }

        /// <summary>
        /// Format file info to string
        /// </summary>
        /// <returns>file info string</returns>
        public override string ToString()
        {
            return string.Format("{0,-16}.{1,-3} {2}{3} {4,4}",
                new object[] { Filename, Type.ToString(), Locked ? "<" : " ", Closed ? "*" : " ", FileSize });
        }

        /// <summary>
        /// Return entry as datarow
        /// </summary>
        /// <param name="dt">Datatable for row model</param>
        /// <returns></returns>
        public dsDisk.DirectoryRow GetDataRow(dsDisk.DirectoryDataTable dt)
        {
            dsDisk.DirectoryRow row = dt.NewDirectoryRow();
            row.Filename = Filename;
            row.FileType = Type.ToString();
            row.FileSize = (Int16)FileSize;
            row.Locked = Locked;
            row.Closed = Closed;
            row.Index = Index;
            return row;
        }
    }
}
