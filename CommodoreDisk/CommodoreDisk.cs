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

namespace Casasoft.Commodore.Disk
{
    /// <summary>
    /// CommodoreDisk Library Tools
    /// </summary>
    public static class CommodoreDisk
    {
        /// <summary>
        /// Creates disk instance by model or extension
        /// </summary>
        /// <param name="diskModel">disk model (1541, 1571...) or extension (D64, D71...)</param>
        /// <returns>Specialized BaseDisk class</returns>
        public static BaseDisk Factory(string diskModel)
        {
            switch (diskModel.ToUpper())
            {
                case ".D64":
                case "1541":
                    return new Disk1541();
                case ".D71":
                case "1571":
                    return new Disk1571();
                case ".D81":
                case "1581":
                    return new Disk1581();
                case ".D80":
                case "8050":
                    return new Disk8050();
                case ".D82":
                case "8250":
                    return new Disk8250();
                default:
                    return null;
            }
        }

        /// <summary>
        /// Filter for Open/Save dialogs
        /// </summary>
        /// <returns></returns>
        public static string DialogFilter()
        {
            string template = "{0} files|*{1}|";
            string ret = string.Format(template, Disk1541.Model, Disk1541.Extension);
            ret += string.Format(template, Disk1571.Model, Disk1571.Extension);
            ret += string.Format(template, Disk1581.Model, Disk1581.Extension);
            ret += string.Format(template, Disk8050.Model, Disk8050.Extension);
            ret += string.Format(template, Disk8250.Model, Disk8250.Extension);
            return ret + "All files|*.*";
        }
    }
}
