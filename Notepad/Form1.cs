using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notepad
{
    public partial class MyForm : Form
    {
        bool isAnyFileOpened = true;
        bool isFilenew = true;
        bool isFileSaved = false;
        string currentOpenedFilePath = null;
        public MyForm()
        {
            InitializeComponent();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isFilenew && !isFileSaved) 
            {
                saveToolStripMenuItem_Click(sender, e);
            }
            NotepadTextBox.Text = "";
            this.Text = "Untitled - Notepad";
            isAnyFileOpened = false;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(!isAnyFileOpened || (isAnyFileOpened && !isFileSaved))
            {
                saveAsToolStripMenuItem_Click(sender, e);
            }
            else if(isAnyFileOpened && isFileSaved)
            {
                System.IO.File.WriteAllText(currentOpenedFilePath, NotepadTextBox.Text);

            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                System.IO.File.WriteAllText(sfd.FileName, NotepadTextBox.Text);
                currentOpenedFilePath = sfd.FileName;
                this.Text = System.IO.Path.GetFileName(sfd.FileName);
                isFileSaved = true;
                isAnyFileOpened = true;
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string content = System.IO.File.ReadAllText(ofd.FileName);
                this.Text = System.IO.Path.GetFileName(ofd.FileName);
                currentOpenedFilePath = ofd.FileName;
                NotepadTextBox.Text = content;
                isAnyFileOpened = true;
            }
        }
    }

}
