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

using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace Casasoft.Commodore.WindowsUI
{
    /// <summary>
    /// Base class for all forms
    /// </summary>
    public partial class BaseForm : Form
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public BaseForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets CBM.ttf from application directory
        /// </summary>
        /// <returns></returns>
        protected Font GetCommodoreFont()
        {
            PrivateFontCollection privateFonts = new PrivateFontCollection();
            privateFonts.AddFontFile("CBM.ttf");
            return new Font(privateFonts.Families[0], 9);
        }
    }
}
