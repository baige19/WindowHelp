using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WindowHelp.WindowHelper;

namespace WindowHelp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            var windows = WindowHelper.FindAllWindows().ToList().OrderBy(x => x.Title).ToList();
            foreach ( WindowInfo window  in windows )
            {
                comboBox1.Items.Add(new ListItem(window.Title,window));
            }

            if(comboBox1.Items.Count > 0 ) comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex < 0) return;
            ListItem li = (ListItem)comboBox1.SelectedItem;
            WindowInfo wi = (WindowInfo)li.WindowInfos;
            richTextBox1.Text = wi.ToString();
            //激活最小化窗体
            WindowHelper.ActivateWindow(wi.Handle);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = ImageHelper.GetWindowImage(((ListItem)comboBox1.SelectedItem).WindowInfos);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.Image.Save("D:\\1.png");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox1.AppendText("点击了");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Bitmap dbmp = new Bitmap(75, 23);
            Graphics g = Graphics.FromImage(dbmp);
            g.DrawImage(pictureBox1.Image, new Rectangle(0, 0, 68, 24), 460, 90, 68, 24, GraphicsUnit.Pixel);
            pictureBox1.Image = dbmp;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Bitmap bmpa = new Bitmap("D:\\1.png");
            pictureBox2.Image = bmpa;
            Bitmap bmpb = new Bitmap(68, 24);
            Graphics g = Graphics.FromImage(bmpb);
            g.DrawImage(ImageHelper.GetWindowImage(((ListItem)comboBox1.SelectedItem).WindowInfos), new Rectangle(0,0,68,24),460,90,68,24, GraphicsUnit.Pixel);
            pictureBox1.Image = bmpb;
            SimilarImageHelper.SourceImg = bmpa;
            string sa = SimilarImageHelper.GetHash();
            SimilarImageHelper.SourceImg = bmpb;
            string sb = SimilarImageHelper.GetHash();
            label2.Text = "比对结果："+(SimilarImageHelper.CalcSimilarDegree(sa,sb)<5);
        }
    }
}
