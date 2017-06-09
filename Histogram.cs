using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace LBP
{
    public partial class Histogram : Form
    {
        GraphPane pane;

        public Histogram()
        {
            InitializeComponent();
        }

        public void drawGraph(double[] array)
        {
            pane = zedGraph.GraphPane;

            pane.CurveList.Clear();

            pane.XAxis.Scale.Min = -2;
            pane.XAxis.Scale.Max = 257;
            pane.YAxis.Scale.Min = 0;

            array = createLog(array);
            pane.YAxis.Scale.Max = 2;
            BarItem bar = pane.AddBar("Гистограмма", null, array, Color.Blue);

            pane.BarSettings.MinClusterGap = 0.0f;
            zedGraph.AxisChange();

            zedGraph.Invalidate();
        }

        public int max(double[] array)
        {
            double max = 0;
            for (int i = 0; i < 256; i++)
            {
                if (array[i] > max)
                    max = array[i];
            }
            return (int)max;
        }

        private double[] createLog(double[] array)
        {
            double[] arr = new double[256];
            for (int i = 0; i < 256; i++)
            {
                arr[i] = Math.Log(Math.E, array[i]);
            }
            return arr;
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = pane.GetImage();
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = "png";
            sfd.Filter = "Image files (*.bmp)|*.bmp|All files (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)

                bmp.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
        }

    }
}
