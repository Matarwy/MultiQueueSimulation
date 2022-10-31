using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace MultiQueueSimulation
{
    public partial class LoadDataFileForm : Form
    {
        public LoadDataFileForm()
        {
            InitializeComponent();

            if (FrontForm.FilePath != null)
            {
                FilePathTextBox.Text = FrontForm.FilePath;
            }
        }

        private void LoadFilebtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    FrontForm.FilePath = FilePathTextBox.Text = ofd.FileName;
                }
                catch {
                    FrontForm.FilePath = null;
                    FilePathTextBox.Text = "";
                }
            }
            else
            {
                FrontForm.FilePath = null;
                FilePathTextBox.Text = "";
            }
        }

        private void FilePathTextBox_TextChanged(object sender, EventArgs e)
        {
            FrontForm.FilePath = FilePathTextBox.Text;
        }

        private void LoadDataFileForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void DoneLoadingbtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
