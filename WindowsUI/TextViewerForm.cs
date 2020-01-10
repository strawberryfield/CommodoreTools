/// @file
/// Windows UI Text viewer Form.
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

using Casasoft.Commodore.Video;
using System.Windows.Forms;

namespace Casasoft.Commodore.WindowsUI
{
    public partial class TextViewerForm : BaseForm
    {
        private C64Colors Colors;

        #region inits
        /// <summary>
        /// Constructor
        /// </summary>
        public TextViewerForm() : base()
        {
            InitializeComponent();
            Colors = new C64Colors();
            AddColorsMenu(textColorToolStripMenuItem, C64Color.LightBlue);
            AddColorsMenu(backgroundColorToolStripMenuItem, C64Color.Blue);
            AddColorsMenu(borderColorToolStripMenuItem, C64Color.LightBlue);

            FileName = string.Empty;
            Content = string.Empty;
            BackColor = Colors[C64Color.LightBlue].RGBColor;

            richTextBox.Font = GetCommodoreFont();
            richTextBox.BackColor = Colors[C64Color.Blue].RGBColor;
            richTextBox.ForeColor = Colors[C64Color.LightBlue].RGBColor;
        }

        private void AddColorsMenu(ToolStripMenuItem menu, C64Color sel)
        {
            foreach (ColorDefinition c in Colors.ToArray())
            {
                ToolStripRadioButtonMenuItem item = new ToolStripRadioButtonMenuItem(c.Name);
                item.Tag = c;
                if (c.ColorId == sel) item.Checked = true;
                menu.DropDownItems.Add(item);
            }
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
        #endregion

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

        #region menu handlers
        private void textColorToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ColorDefinition c = e.ClickedItem.Tag as ColorDefinition;
            richTextBox.ForeColor = c.RGBColor;
        }

        private void backgroundColorToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ColorDefinition c = e.ClickedItem.Tag as ColorDefinition;
            richTextBox.BackColor = c.RGBColor;
        }

        private void borderColorToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ColorDefinition c = e.ClickedItem.Tag as ColorDefinition;
            BackColor = c.RGBColor;
        }

        private void loadToolStripMenuItem_Click(object sender, System.EventArgs e)
        {

        }

        private void saveToolStripMenuItem_Click(object sender, System.EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        #endregion

    }
}
