/// @file
/// Windows UI Base Form.
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

using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Linq;
using System;

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

        private Point getWindowInfo(string setting)
        {
            Point point = new Point();
            if (ConfigurationManager.AppSettings.AllKeys.Contains(this.Name + setting))
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
                string[] p = config.AppSettings.Settings[this.Name + setting].Value.Split(',');
                point = new Point(Convert.ToInt16(p[0]), Convert.ToInt16(p[1]));
                
            }
            return point;
        }

        private void BaseForm_Load(object sender, System.EventArgs e)
        {
            Point p = getWindowInfo("Position");
            if(!p.IsEmpty)
            {
                this.Location = p;
            }

            p = getWindowInfo("Size");
            if(!p.IsEmpty)
            {
                this.Size = new Size(p);
            }
        }

        private void BaseForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
