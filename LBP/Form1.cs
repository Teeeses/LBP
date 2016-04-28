using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LBP
{
    public partial class Form1 : Form
    {
        private LBP lbp;

        public Form1()
        {
            InitializeComponent();
            menu();
        }

        private void menu()
        {
            
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Bitmaps|*.bmp;*.JPG; *.png";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                lbp = new LBP(Image.FromFile(openFileDialog.FileName));
                pictureBox.Image = lbp.getPicture();
                lbp.methodLBP(lbp.getPicture());
                pictureBoxLBP.Image = lbp.getChengedPicture();
                //Console.WriteLine(picture.GetPixel(5, 5).
            }
        }
    }
}
