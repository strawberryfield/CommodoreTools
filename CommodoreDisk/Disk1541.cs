/// @file
/// 1541 disk class.
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
    /// class for 1541 disks (sigle side, 35 tracks)
    /// </summary>
    public class Disk1541 : BaseDisk
    {
        #region static members
        /// <summary>
        /// Standard extension for disk image files
        /// </summary>
        /// <returns></returns>
        public static string Extension => ".D64";

        /// <summary>
        /// Original Commodore disk model
        /// </summary>
        /// <returns></returns>
        public static string Model => "1541";
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public Disk1541() : base()
        {
            Header = new BAM1541();

            for (int j = 1; j <= 17; j++) addTrackStructure(21);
            for (int j = 18; j <= 24; j++) addTrackStructure(19);
            for (int j = 25; j <= 30; j++) addTrackStructure(18);
            for (int j = 31; j <= 35; j++) addTrackStructure(17);
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
