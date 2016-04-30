using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace LBP
{
    public partial class Form1 : Form
    {
        private LBP lbp;

        public Form1()
        {
            InitializeComponent();
            this.pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            this.pictureBoxLBP.SizeMode = PictureBoxSizeMode.Zoom;
            menu();
        }

        private void menu()
        {
            Image img = Properties.Resources.girl_;
            loadPicture(img);
        }


        private void loadPicture(Image img)  
        {
            unsafe
            {
                lbp = new LBP(img);
                pictureBox.Image = lbp.getPicture();
                pictureBoxLBP.Image = lbp.getChengedPicture();
                lbp.setUnlock();
            }
        }

        private void LoadFotoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream stream;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Bitmaps|*.bmp;*.JPG; *.png";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((stream = openFileDialog.OpenFile()) != null)
                    {
                        using (stream)
                        {
                            Image img = Image.FromFile(openFileDialog.FileName);
                            loadPicture(img);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
