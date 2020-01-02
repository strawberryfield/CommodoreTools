/// @file
/// Commodore Disk Manager main form.
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

using Casasoft.Commodore.Disk;
using Casasoft.Commodore.WindowsUI;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Casasoft.Commodore
{
    public partial class MainForm : BaseForm
    {
        public MainForm()
        {
            InitializeComponent();
            openDiskDialog.Filter = CommodoreDisk.DialogFilter;
            saveFileDialog.Filter = DirectoryEntry.OpenSaveDialogsFilter;
        }

        private string fileName = string.Empty;
        private BaseDisk disk;

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openDiskDialog.ShowDialog() == DialogResult.OK)
            {
                fileName = openDiskDialog.FileName;
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

        private dsDisk.DirectoryRow currentDirectoryRow;
        private byte[] GetFileByGridRow(int rowIndex)
        {
            DataRowView datarow = (DataRowView)dataGridViewDirectory.Rows[rowIndex].DataBoundItem;
            currentDirectoryRow = (dsDisk.DirectoryRow)datarow.Row;
            return disk.GetFile(currentDirectoryRow.Index);
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            byte[] file = GetFileByGridRow(currentMouseOverRow);
            TextViewerForm viewer = new TextViewerForm(currentDirectoryRow.Filename, file);
            viewer.ShowDialog();
        }

        private void saveToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            byte[] file = GetFileByGridRow(currentMouseOverRow);
            saveFileDialog.DefaultExt = currentDirectoryRow.FileType;
            saveFileDialog.FileName = string.Format("{0}.{1}", 
                currentDirectoryRow.Filename.Trim(), saveFileDialog.DefaultExt);
            if( saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllBytes(saveFileDialog.FileName, file);
            }
        }
    }
}
