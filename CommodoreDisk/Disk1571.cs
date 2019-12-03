﻿// copyright (c) 2019 Roberto Ceccarelli - Casasoft
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

namespace Casasoft.Commodore.Disk
{
    /// <summary>
    /// class for 1571 disks (double side, 70 tracks)
    /// </summary>
    public class Disk1571 : BaseDisk
    {
        #region static members
        /// <summary>
        /// Standard extension for disk image files
        /// </summary>
        /// <returns></returns>
        public static string Extension => ".D71";

        /// <summary>
        /// Original Commodore disk model
        /// </summary>
        /// <returns></returns>
        public static string Model => "1571";
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public Disk1571() : base()
        {
            Header = new BAM1571();

            // side one
            for (int j = 1; j <= 17; j++) addTrackStructure(21);
            for (int j = 18; j <= 24; j++) addTrackStructure(19);
            for (int j = 25; j <= 30; j++) addTrackStructure(18);
            for (int j = 31; j <= 35; j++) addTrackStructure(17);
            // side two
            for (int j = 36; j <= 52; j++) addTrackStructure(21);
            for (int j = 53; j <= 59; j++) addTrackStructure(19);
            for (int j = 60; j <= 65; j++) addTrackStructure(18);
            for (int j = 66; j <= 70; j++) addTrackStructure(17);

            initDiskData();
        }

        /// <summary>
        /// Load root directory starting at 18/1
        /// </summary>
        protected override void LoadRoot()
        {
            RootDir.Load(this, 18, 1);
        }

        /// <summary>
        /// Load BAM 
        /// </summary>
        protected override void LoadBAM()
        {
            Header.Load(this);
        }

    }
}
