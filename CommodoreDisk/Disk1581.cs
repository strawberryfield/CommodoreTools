/// @file
/// 1581 disk class.
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
    /// class for 1581 disks (double side, 80 tracks, 40 sectors/track)
    /// </summary>
    public class Disk1581 : BaseDisk
    {
        #region static members
        /// <summary>
        /// Standard extension for disk image files
        /// </summary>
        /// <returns></returns>
        public static string Extension => ".D81";

        /// <summary>
        /// Original Commodore disk model
        /// </summary>
        /// <returns></returns>
        public static string Model => "1581";
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public Disk1581() : base()
        {
            Header = new BAM1581();
            RootDir = new Directory(40, 3);

            for (int j = 1; j <= 80; j++) addTrackStructure(40);
            initDiskData();
        }

    }
}
