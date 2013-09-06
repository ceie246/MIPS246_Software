using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MipsSimulator
{
    public partial class Form2 : Form
    {
        public bool isOpen = false;
        public Form2()
        {
            isOpen = false;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                string source = getSource();
                string output = getOutput();
                Form1.react(ref source, ref output);
            }
            else
            {
                MessageBox.Show("请输入正确地址！");
            }
        }
        public string getSource()
        {
            return textBox1.Text;
        }
        public string getOutput()
        {
            return textBox2.Text;

        }

    }
}
