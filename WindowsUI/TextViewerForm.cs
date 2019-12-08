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

namespace Casasoft.Commodore.WindowsUI
{
    public partial class TextViewerForm : BaseForm
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public TextViewerForm() : base()
        {
            InitializeComponent();
            FileName = string.Empty;
            Content = string.Empty;

            richTextBox.Font = GetCommodoreFont();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="filename">Name of the file (shown in form caption)</param>
        /// <param name="content">Content to display</param>
        public TextViewerForm(string filename, string content) : this()
        {
            FileName = filename;
            Content = content;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="filename">Name of the file (shown in form caption)</param>
        /// <param name="content">Content to display</param>
        public TextViewerForm(string filename, byte[] content) :
            this(filename, System.Text.Encoding.ASCII.GetString(content))
        {
        }

/// <summary>
/// get/set filename
/// </summary>
        public string FileName
        {
            get => Text;
            set => Text = value;
        }

        /// <summary>
        /// get/set content to display
        /// </summary>
        public string Content
        {
            get => richTextBox.Text;
            set => richTextBox.Text = value;
        }
    }
}
