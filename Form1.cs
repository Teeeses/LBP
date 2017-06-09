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
using ZedGraph;

namespace LBP
{
    public partial class Form1 : Form
    {
        private LBP lbp;
        private Histogram histogram;
        private Image img;

        private string str;
        private int transitions;

        public Form1()
        {
            InitializeComponent();
            this.pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            this.pictureBoxLBP.SizeMode = PictureBoxSizeMode.Zoom;
            histogram = new Histogram();
            str = "none";
            transitions = 0;
            menu();
        }

        private void menu()
        {
            img = Properties.Resources._5;
            loadPicture();
            DrawGraph();
        }


        private void loadPicture()  
        {
            //unsafe
            //{
            lbp = new LBP(img, str, checkBox1.Checked);
            pictureBox.Image = lbp.getPicture();
            pictureBoxLBP.Image = lbp.getChengedPicture();
            DrawGraph();
            //lbp.setUnlock();
            //}
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
                            img = Image.FromFile(openFileDialog.FileName);
                            loadPicture();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void DrawGraph()
        {
            histogram.drawGraph(lbp.getHistogram());
            histogram.Show();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            str = "R";
            loadPicture();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            str = "G";
            loadPicture();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            str = "B";
            loadPicture();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            str = "none";
            loadPicture();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) 
        {
            loadPicture();
        }

        private void saveStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = "bmp";
            sfd.Filter = "Image files (*.bmp)|*.bmp|All files (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)

                lbp.getChengedPicture().Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
        }

    }
}
