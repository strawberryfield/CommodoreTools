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
    /// <summary>
    /// BAM for 8250 disks (double side, 77+77 tracks)
    /// </summary>
    public class BAM8250 : BAM8050
    {       
        /// <summary>
        /// Constructor
        /// </summary>
        public BAM8250() : base()
        {
        }

        /// <summary>
        /// Load BAM data from disk image
        /// </summary>
        /// <param name="disk">Disk image to read</param>
        public override void Load(BaseDisk disk)
        {
            LoadHeader(disk, 39, 0);
            loadPartialBAM(disk, 38, 0);
            loadPartialBAM(disk, 38, 3);
            loadPartialBAM(disk, 38, 6);
            loadPartialBAM(disk, 38, 9);
        }

    }
}
