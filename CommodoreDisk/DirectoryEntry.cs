/// @file
/// Directory entry class.
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
using System.Text;

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
        public byte track = 0;
        public byte sector = 0;
        public byte entry = 0;
    }

    /// <summary>
    /// Single directory entry
    /// </summary>
    public class DirectoryEntry
    {
        public FileType Type;
        public bool Locked;
        public bool Closed;
        public byte FirstTrack;
        public byte FirstSector;
        public string Filename;
        public byte RelFirstTrack;
        public byte RelFirstSector;
        public byte RelRecordLength;
        public UInt16 FileSize;
        public DirectoryIndex Reference;
        public Int32 Index { get { return Reference.entry + Reference.sector * 10 + Reference.track * 1000; } }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        /// <summary>
        /// Size of an entry in bytes
        /// </summary>
        public const int EntrySize = 32;

        /// <summary>
        /// Filter for open/save Dialogs
        /// </summary>
        public const string OpenSaveDialogsFilter = 
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
            FileSize = Convert.ToUInt16(data[0x1E] + data[0x1F] * 256);
            StringBuilder sb = new StringBuilder(16);
            for (int j = 0x05; j <= 0x14; j++)
            {
                sb.Append((char)data[j]);
            }
            Filename = sb.ToString();
        }

        /// <summary>
        /// Returns binary data to write on disk
        /// </summary>
        /// <returns></returns>
        public byte[] ToRaw()
        {
            byte[] ret = new byte[32];
            ret[0] = 0;
            ret[1] = 0;
            ret[2] = Convert.ToByte(Type + 0x80);
            ret[3] = FirstTrack;
            ret[4] = RelFirstSector;
            ret[0x15] = RelFirstTrack;
            ret[0x16] = RelFirstSector;
            ret[0x17] = RelRecordLength;
            ret[0x1E] = Convert.ToByte(FileSize % 256);
            ret[0x1F] = Convert.ToByte(FileSize / 256);
            for(int j = 0; j<16 || j<Filename.Length; j++)
            {
                ret[5 + j] = Convert.ToByte(Filename[j]);
            }
            return ret;
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
