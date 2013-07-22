using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MipsSimulator.Devices;
using MipsSimulator.Assembler;
using MipsSimulator.Tools;
using MipsSimulator.Monocycle;
using MipsSimulator.Cmd;

namespace MipsSimulator
{
    public partial class Form1 : Form
    {
        static public bool isStep = false;
        static public bool isStep2 = false;
        static public List<Int32> breakpoints = new List<int>();
        static public bool isBreak = false;
        //private ChildFrm myChildFrm;

        public Form1()
        {
            InitializeComponent();
            this.toolStripButton2.Enabled = false;
            this.toolStripButton3.Enabled = false;
            this.textBox1.Font = new Font(textBox1.Font.FontFamily, 15, textBox1.Font.Style);
            textBox2.Font = new Font(textBox2.Font.FontFamily, 15, textBox2.Font.Style);
            
            //寄存器表格
            Register.ResInitialize();
            this.dataGridView2.DataSource =Register.Res;
            this.dataGridView2.Columns[0].FillWeight = 30;
            this.dataGridView2.Columns[1].FillWeight = 70;

            //内存表格
            Memory.MemInitialize();
            this.dataGridView3.DataSource = Memory.Mem;
            //this.dataGridView3.Columns[0].FillWeight = 30;
            //this.dataGridView3.Columns[1].FillWeight = 70;

            //Execute表格
            RunTimeCode.CodeTInitial();
            dataGridView1.DataSource = RunTimeCode.CodeT;
            dataGridView1.Columns[0].FillWeight = 20;
            dataGridView1.Columns[1].FillWeight = 20;
            dataGridView1.Columns[2].FillWeight = 30;
            dataGridView1.Columns[3].FillWeight = 30;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[3].ReadOnly = true;

        }

        //File    Open
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string stream = "";
            FileControl.Open(ref stream);
            this.textBox1.Text = stream;
        }

        //File    Save
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton1_Click(sender, e);
        }

        //Help   About
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About aboutFrm=new About();
            aboutFrm.Visible = true;
        }

        //save
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            string stream = this.textBox1.Text;
            if (stream.Length <= 0)
            {
                MessageBox.Show("请输入代码！");
                return;
            }
            FileControl.Save(stream);
        }

        //分析代码
        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            textBox2.Text="";
            if (this.textBox1.Text == null || this.textBox1.Text == "")
            {
                MessageBox.Show("代码不可以为空！");
                return;
            }
            string[] codes = this.textBox1.Text.Split(new string[1] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (codes == null || codes.Length <= 0)
            {
                MessageBox.Show("代码不可以为空！");
                return;
            }

            Reset();
            RunTimeCode.Clear();
            string inputPath = System.Environment.CurrentDirectory;
            inputPath = inputPath + "\\source.txt";
            string outputPath = System.Environment.CurrentDirectory;
            outputPath = outputPath + "\\report.txt";
            if (File.Exists(inputPath))
            {
                File.Delete(inputPath);
            }
            if (File.Exists(outputPath))
            {
                File.Delete(outputPath);
            }
            MipsSimulator.Tools.FileControl.WriteFile(inputPath, this.textBox1.Text);

            MipsSimulator.Cmd.cmdMode cmdMode = new Cmd.cmdMode();
            if (!cmdMode.doAssembler(inputPath, outputPath))
            {
                string error=MipsSimulator.Tools.FileControl.ReadFile(outputPath);
                RunTimeCode.Clear();
                textBox2.Text += error;
                return;
            }

            int rows=RunTimeCode.CodeT.Rows.Count;

            dataGridView1.Refresh();
            int rows2 = dataGridView1.RowCount;

            this.tabControl1.SelectedTab = this.tabPage2;
            
        }

        static public void codeColor(int index, int color)
        {
            switch (color)
            {
                case 1:
                    {
                        dataGridView1.Rows[index].DefaultCellStyle.ForeColor = Color.Red;
                        break;
                    }
                case 2:
                    {
                        dataGridView1.Rows[index].DefaultCellStyle.ForeColor = Color.Orange;
                        break;
                    }
                case 3:
                    {
                        dataGridView1.Rows[index].DefaultCellStyle.ForeColor = Color.Green;
                        break;
                    }
                case 4:
                    {
                        dataGridView1.Rows[index].DefaultCellStyle.ForeColor = Color.Purple;
                        break;
                    }
                case 5:
                    {
                        dataGridView1.Rows[index].DefaultCellStyle.ForeColor = Color.Blue;
                        break;
                    }
            }
        }

        static public void Message(string message)
        {
            textBox2.Text += message + "\r\n";
        }

        //复位
        static public void Reset()
        {
            isStep = false;
            isStep2 = false;
            isBreak = false;
            breakpoints.Clear();

            Register.Clear();
            RunTimeCode.Clear();
           
            //MasterSwitch.Initialize();
            //MasterSwitch.Close();

           // IFStage.Close();
            //DEStage.Close();
            //EXEStage.Close();
           // MEMStage.Close();
           // WBStage.Close();

            mMasterSwitch.Initialize();
            mMasterSwitch.Close();
        }

        //执行
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            //MasterSwitch.Start();
        }

        
        //step
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            this.toolStripButton3.Enabled = false;
            if (!isStep)
            {
              // MasterSwitch.Initialize();
            }
            isStep = true;
            //MasterSwitch.StepInto();
            this.toolStripButton3.Enabled = true;
        }

        //单周期running
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            mMasterSwitch.Start();
        }

        //单周期step
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (!isStep2)
            {
                mMasterSwitch.Initialize();
            }
            isStep2 = true;
            mMasterSwitch.StepInto();
        }

        //
        static private void breaksCompute()
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                try
                {
                    //第0列是checkbox 
                    DataGridViewCheckBoxCell check = dataGridView1.Rows[i].Cells[0] as DataGridViewCheckBoxCell;
                                    
                    if (check.Value != null)//先验证为null                    
                    {
                        int id = Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value);                       
                        if ((bool)check.Value)
                        {
                            breakpoints.Add(id);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Message(ex.Message.ToString());
                }
            }
        }

        //单周期断点
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (!isBreak)
            {
                breaksCompute();
                mMasterSwitch.Initialize();
            }
            isBreak = true;
            mMasterSwitch.BreakPoint();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {

        }

        private void tabControl3_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.dataGridView3.Refresh();
        }
        
    }
}
