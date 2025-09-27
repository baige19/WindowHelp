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
            g.DrawImage(pictureBox1.Image, new Rectangle(0, 0, 68, 24), 0, 0, 384, 216, GraphicsUnit.Pixel);
            pictureBox1.Image = dbmp;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Bitmap bmpa = new Bitmap("D:\\1.png");
            pictureBox2.Image = bmpa;
            Bitmap bmpb = new Bitmap(75, 23);
            Graphics g = Graphics.FromImage(bmpb);
            g.DrawImage(ImageHelper.GetWindowImage(((ListItem)comboBox1.SelectedItem).WindowInfos), new Rectangle(0,0,68,24), 0, 0, 384, 216, GraphicsUnit.Pixel);
            pictureBox1.Image = bmpb;
            SimilarImageHelper.SourceImg = bmpa;
            string sa = SimilarImageHelper.GetHash();
            SimilarImageHelper.SourceImg = bmpb;
            string sb = SimilarImageHelper.GetHash();
            label2.Text = "比对结果："+(SimilarImageHelper.CalcSimilarDegree(sa,sb)<5);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            WindowInfo wi = ((ListItem)comboBox1.SelectedItem).WindowInfos;
            int x = 200;
            int y = 200;
            InputHelper.MouseLeftDown(wi.Handle, x, y);
            InputHelper.MouseLeftUp(wi.Handle, x, y);
            InputHelper.KeyPress(wi.Handle, (byte)Keys.A);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            richTextBox1.AppendText($"\r\n键盘按下{e.KeyCode}");
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            richTextBox1.AppendText($"\r\n鼠标按下位置{e.Location}");
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            richTextBox1.AppendText($"\r\n键盘抬起{e.KeyCode}");
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            richTextBox1.AppendText($"\r\n鼠标抬起位置{e.Location}");
        }
    }
}
