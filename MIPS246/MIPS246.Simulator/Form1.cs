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
using System.Runtime.InteropServices;

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

            this.txtContent.MouseWheel += new MouseEventHandler(txtContect_MouseWheel);

            textBox2.Font = new Font(textBox2.Font.FontFamily, 15, textBox2.Font.Style);

            //寄存器表格
            Register.ResInitialize();
            this.dataGridView2.DataSource = Register.Res;
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

        private int pageLine = 0;
        private void txtContect_TextChanged(object sender, EventArgs e)
        {
            //调用顺序不可变
            SetScrollBar();
            ShowRow();
            ShowCursorLine();
        }

        //鼠标滚动
        void txtContect_MouseWheel(object sender, MouseEventArgs e)
        {
            _timer1.Enabled = true;
        }

        // 上、下键
        private void txtContent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == System.Windows.Forms.Keys.Up || e.KeyData == System.Windows.Forms.Keys.Down)
                SetScrollBar();

        }
        private void txtContent_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == System.Windows.Forms.Keys.Up || e.KeyData == System.Windows.Forms.Keys.Down)
                ShowCursorLine();
        }

        //点击滚动条
        private void vScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            int t = SetScrollPos(this.txtContent.Handle, 1, _vScrollBar1.Value, true);
            SendMessage(this.txtContent.Handle, WM_VSCROLL, SB_THUMBPOSITION + 0x10000 * _vScrollBar1.Value, 0);
            ShowRow();
        }

        //显示光标行
        private void txtContent_MouseDown(object sender, MouseEventArgs e)
        {
            ShowCursorLine();
        }

        //文本框大小改变
        private void txtContent_SizeChanged(object sender, EventArgs e)
        {
            SCROLLINFO si = new SCROLLINFO();
            si.cbSize = (uint)Marshal.SizeOf(si);
            si.fMask = SIF_ALL;
            int r = GetScrollInfo(this.txtContent.Handle, SB_VERT, ref si);
            pageLine = (int)si.nPage;
            _timer1.Enabled = true;
            ShowRow();
        }

        //行显示栏宽度自适应
        private void txtRow_TextChanged(object sender, EventArgs e)
        {
            if (this._txtRow.Lines.Length > 0)
            {
                System.Drawing.SizeF s = this._txtRow.CreateGraphics().MeasureString(this._txtRow.Lines[this._txtRow.Lines.Length - 1], this._txtRow.Font);
                this._txtRow.Width = (int)s.Width;
            }
        }

        private void txtRow_SizeChanged(object sender, EventArgs e)
        {
            this.txtContent.Location = new Point(this._txtRow.Width, this.txtContent.Location.Y);
            this.txtContent.Width = this.ClientSize.Width - this._txtRow.Width;
        }

        //private void menuItemAbout_Click(object sender, EventArgs e)
        //{
        //    About a = new About();
        //    a.ShowDialog();
        //}

        #region Method
        private void ShowCursorLine()
        {
            _toolStripStatusLabel1.Text = "line: " + (this.txtContent.GetLineFromCharIndex(this.txtContent.SelectionStart) + 1);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SetScrollBar();
            _timer1.Enabled = false;
        }

        private void SetScrollBar()
        {
            SCROLLINFO si = new SCROLLINFO();
            si.cbSize = (uint)Marshal.SizeOf(si);
            si.fMask = SIF_ALL;
            int r = GetScrollInfo(this.txtContent.Handle, SB_VERT, ref si);
            pageLine = (int)si.nPage;
            this._vScrollBar1.LargeChange = pageLine;

            if (si.nMax >= si.nPage)
            {
                this._vScrollBar1.Visible = true;
                this._vScrollBar1.Maximum = si.nMax;
                this._vScrollBar1.Value = si.nPos;
            }
            else
                this._vScrollBar1.Visible = false;
        }

        private void ShowRow()
        {
            int firstLine = txtContent.GetLineFromCharIndex(txtContent.GetCharIndexFromPosition(new Point(0, 2)));
            string[] lin = new string[pageLine];
            for (int i = 0; i < pageLine; i++)
            {
                lin[i] = (i + firstLine + 1).ToString();
            }
            _txtRow.Lines = lin;
        }

        #endregion

        #region API 调用

        public static uint SIF_RANGE = 0x0001;
        public static uint SIF_PAGE = 0x0002;
        public static uint SIF_POS = 0x0004;
        public static uint SIF_TRACKPOS = 0x0010;
        public static uint SIF_ALL = (SIF_RANGE | SIF_PAGE | SIF_POS | SIF_TRACKPOS);
        public int SB_THUMBPOSITION = 4;
        public int SB_VERT = 1;
        public int WM_VSCROLL = 0x0115;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct SCROLLINFO
        {
            public uint cbSize;
            public uint fMask;
            public int nMin;
            public int nMax;
            public uint nPage;
            public int nPos;
            public int nTrackPos;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetScrollInfo(IntPtr hwnd, int bar, ref SCROLLINFO si);

        [DllImport("user32.dll")]
        private static extern int GetScrollPos(IntPtr hwnd, int nbar);

        [DllImport("user32.dll")]
        public static extern int SetScrollPos(IntPtr hWnd, int nBar, int nPos, bool Rush);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        #endregion
        //File    Open
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string stream = "";
            FileControl.Open(ref stream);
            this.txtContent.Text = stream;
        }

        //File    Save
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton1_Click(sender, e);
        }

        //Help   About
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About aboutFrm = new About();
            aboutFrm.Visible = true;
        }

        //save
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            string stream = this.txtContent.Text;
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
            textBox2.Text = "";
            if (this.txtContent.Text == null || this.txtContent.Text == "")
            {
                MessageBox.Show("代码不可以为空！");
                return;
            }
            string[] codes = this.txtContent.Text.Split(new string[1] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
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
            MipsSimulator.Tools.FileControl.WriteFile(inputPath, this.txtContent.Text);

            MipsSimulator.Cmd.cmdMode cmdMode = new Cmd.cmdMode();
            if (!cmdMode.doAssembler(inputPath, outputPath))
            {
                string error = MipsSimulator.Tools.FileControl.ReadFile(outputPath);
                RunTimeCode.Clear();
                textBox2.Text += error;
                return;
            }

            int rows = RunTimeCode.CodeT.Rows.Count;

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
                        if (MipsSimulator.Program.mode == 1)
                        {
                            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                            {
                                if (i != index)
                                    dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                            }
                        }
                        break;
                    }
                case 2:
                    {
                        dataGridView1.Rows[index].DefaultCellStyle.ForeColor = Color.Orange;
                        if (MipsSimulator.Program.mode == 1)
                        {
                            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                            {
                                if (i != index)
                                    dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                            }
                        }
                        break;
                    }
                case 3:
                    {
                        dataGridView1.Rows[index].DefaultCellStyle.ForeColor = Color.Green;
                        if (MipsSimulator.Program.mode == 1)
                        {
                            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                            {
                                if (i != index)
                                    dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                            }
                        }
                        break;
                    }
                case 4:
                    {
                        dataGridView1.Rows[index].DefaultCellStyle.ForeColor = Color.Purple;
                        if (MipsSimulator.Program.mode == 1)
                        {
                            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                            {
                                if (i != index)
                                    dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                            }
                        }
                        break;
                    }
                case 5:
                    {
                        dataGridView1.Rows[index].DefaultCellStyle.ForeColor = Color.Blue;
                        if (MipsSimulator.Program.mode == 1)
                        {
                            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                            {
                                if (i != index)
                                    dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                            }
                        }
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
