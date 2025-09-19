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
    }
}
