using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MipsSimulator.Tools;

namespace MipsSimulator
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();

            string path = System.Environment.CurrentDirectory;
            path = path + "\\about.txt";

            this.textBox1.Text = FileControl.ReadFile(path);
            this.textBox1.Font = new Font(textBox1.Font.FontFamily, 15, textBox1.Font.Style);
        }
    }
}
