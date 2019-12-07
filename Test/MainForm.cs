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

using Casasoft.Commodore.Disk;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Casasoft.Commodore
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            openFileDialog.Filter = CommodoreDisk.DialogFilter();
        }

        private string fileName = string.Empty;
        private BaseDisk disk;

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog.FileName;
                disk = CommodoreDisk.Factory(System.IO.Path.GetExtension(fileName));
                if (disk != null)
                {
                    disk.Load(fileName);
                    disk.RootDir.ToDataTable(dsDisk.Directory);
                    txtDiskName.Text = disk.Header.DiskName;
                    txtId.Text = new string(disk.Header.DiskId);
                    txtDos.Text = new string(disk.Header.DOStype);
                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form about = new AboutBox();
            about.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private int currentMouseOverRow;
        private void dataGridViewDirectory_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                currentMouseOverRow = dataGridViewDirectory.HitTest(e.X, e.Y).RowIndex;
                if (currentMouseOverRow >= 0)
                {
                    contextMenuStripGrid.Show(dataGridViewDirectory, new Point(e.X, e.Y));
                }
            }
        }

        private byte[] GetFileByGridRow(int rowIndex)
        {
            DataRowView datarow = (DataRowView)dataGridViewDirectory.Rows[rowIndex].DataBoundItem;
            dsDisk.DirectoryRow row = (dsDisk.DirectoryRow)datarow.Row;
            return disk.GetFile(row.Index);
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            byte[] file = GetFileByGridRow(currentMouseOverRow);
            TextViewerForm viewer = new TextViewerForm("File", file);
            viewer.ShowDialog();
        }
    }
}
