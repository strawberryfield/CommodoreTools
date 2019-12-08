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

using System.Windows.Forms;

namespace Casasoft.Commodore
{
    public partial class TextViewerForm : Form
    {
        public TextViewerForm()
        {
            InitializeComponent();
            FileName = string.Empty;
            Content = string.Empty;
        }

        public TextViewerForm(string filename, string content) : this()
        {
            FileName = filename;
            Content = content;
        }

        public TextViewerForm(string filename, byte[] content) :
            this(filename, System.Text.Encoding.ASCII.GetString(content))
        {
        }

        public string FileName
        {
            get => Text;
            set => Text = value;
        }

        public string Content
        {
            get => richTextBox.Text;
            set => richTextBox.Text = value;
        }
    }
}
