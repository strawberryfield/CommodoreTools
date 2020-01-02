/// @file
/// 8050 disk class.
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

namespace Casasoft.Commodore.Disk
{
    /// <summary>
    /// class for 8050 disks (sigle side, 77 tracks)
    /// </summary>
    public class Disk8050 : BaseDisk
    {
        #region static members
        /// <summary>
        /// Standard extension for disk image files
        /// </summary>
        /// <returns></returns>
        public static string Extension => ".D80";

        /// <summary>
        /// Original Commodore disk model
        /// </summary>
        /// <returns></returns>
        public static string Model => "8050";
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public Disk8050() : base()
        {
            Header = new BAM8050();

            for (int j = 1; j <= 39; j++) addTrackStructure(29);
            for (int j = 40; j <= 53; j++) addTrackStructure(27);
            for (int j = 54; j <= 64; j++) addTrackStructure(25);
            for (int j = 65; j <= 77; j++) addTrackStructure(23);
            initDiskData();
        }

        /// <summary>
        /// Load root directory starting at 39/1
        /// </summary>
        protected override void LoadRoot()
        {
            RootDir.Load(this, 39, 1);
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
