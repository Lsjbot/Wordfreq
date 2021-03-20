using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;

namespace WordFreq
{
    public partial class ChartForm : Form
    {
        public Chart chart1;

        public ChartForm(Chart chartpar)
        {
            InitializeComponent();
            chart1 = chartpar;
            chart1.Width = this.Width - 20;
            chart1.Height = this.Height - 60;
            chart1.Top = 0;
            chart1.Left = 0;

            this.Controls.Add(chart1);
        }

        private void Quitbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Copybutton_Click(object sender, EventArgs e)
        {

        }

        private void Copybutton_Click_1(object sender, EventArgs e)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                chart1.SaveImage(ms, ChartImageFormat.Bmp);
                Bitmap bm = new Bitmap(ms);
                Clipboard.SetImage(bm);
            }
        }
    }
}
