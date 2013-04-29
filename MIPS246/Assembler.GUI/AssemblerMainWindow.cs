using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MIPS246.Core.Assembler;
using MIPS246.Core.DataStructure;

namespace Assembler.GUI
{
    public partial class AssemblerMainWindow : Form
    {
        string sourcepath = null, outputpath = null;
        string defaultFileName = null;
        bool isOutputFile = false;
        bool isOutputCOE = false;

        public AssemblerMainWindow()
        {
            InitializeComponent();
        }

        private void OutputFileCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (OutputFileCheckBox.Checked == true)
            {
                EnableFileOutput();
            }
            else
            {
                DisableFileOutput();
            }
        }

        private void SourcePathFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog.Filter = "Assemble file (*.asm)|*.asm";
            OpenFileDialog.FileName = "";
            SourceRichTextBox.Text = "";
            if (OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                SourceFilePathTextBox.Text = OpenFileDialog.FileName;
                if (File.Exists(SourceFilePathTextBox.Text))
                {
                    string linetext;
                    StreamReader sr = new StreamReader(SourceFilePathTextBox.Text);
                    while ((linetext = sr.ReadLine()) != null)
                    {
                        SourceRichTextBox.Text += linetext + "\r\n";
                    }
                    sr.Close();

                    defaultFileName = Path.GetFileNameWithoutExtension(SourceFilePathTextBox.Text);
                    
                    sourcepath = SourceFilePathTextBox.Text;
                    OutputRichTextBox.Text = string.Empty;
                    AssembleButton.Enabled = true;
                }
                else
                {
                    MessageBox.Show("File is not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                SourceFilePathTextBox.Text = string.Empty;
            }            
        }

        private void OutputFileButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog.Filter = "Text file (*.txt)|*.txt|Coe File (*.coe)|*.coe";
            SaveFileDialog.FileName = defaultFileName;
            if (SaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                OutputPathTextBox.Text = SaveFileDialog.FileName;
                if (Path.GetExtension(SaveFileDialog.FileName) == ".coe")
                {

                    isOutputCOE = true;
                }
                else
                {
                    isOutputCOE = false;
                }
                outputpath = SaveFileDialog.FileName;
            }
            else
            {
                OutputPathTextBox.Text = string.Empty;
            }
        }

        private void EnableFileOutput()
        {
            OutputFileButton.Enabled = true;
            OutputPathTextBox.Enabled = true;
            isOutputFile = true;
        }

        private void DisableFileOutput()
        {
            OutputFileButton.Enabled = false;
            OutputPathTextBox.Enabled = false;
            OutputPathTextBox.Text = string.Empty;
            isOutputFile = false;
        }

        private void AssembleButton_Click(object sender, EventArgs e)
        {
            if (SourceFilePathTextBox.Text == string.Empty)
            {
                SaveFileDialog.Filter = "asm file (*.asm)|*.asm";
                SaveFileDialog.FileName = "";
                if (SaveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    sourcepath = SaveFileDialog.FileName;
                    SourceFilePathTextBox.Text = sourcepath;
                }
            }

            StreamWriter sw = new StreamWriter(sourcepath, false);
            for (int i = 0; i < SourceRichTextBox.Lines.Length; i++)
            {
                sw.WriteLine(SourceRichTextBox.Lines[i]);
            }
            sw.Close();
            OutputRichTextBox.Text = "";

            bool[] boolArray = new bool[32];
            MIPS246.Core.Assembler.Assembler assembler = new MIPS246.Core.Assembler.Assembler(sourcepath, outputpath);
            if (assembler.DoAssemble() == true)
            {                
                for (int i = 0; i < assembler.CodeList.Count; i++)
                {
                    assembler.CodeList[i].Machine_Code.CopyTo(boolArray,0);
                    if (HEXRadioButton.Checked == false)
                    {
                        OutputRichTextBox.Text += "0x" + String.Format("{0:X8}", assembler.CodeList[i].Address) + ":\t";
                        for (int j = 0; j < 32; j++)
                        {
                            if (boolArray[j] == true)
                            {
                                OutputRichTextBox.Text += "1";
                            }
                            else
                            {
                                OutputRichTextBox.Text += "0";
                            }
                        }
                        OutputRichTextBox.Text += "\r\n";
                    }
                    else
                    {
                        OutputRichTextBox.Text += "0x" + String.Format("{0:X8}", assembler.CodeList[i].Address) + ":\t";
                        OutputRichTextBox.Text += FormatHex(boolArray);
                        OutputRichTextBox.Text += "\r\n";
                    }
                }

                if (isOutputFile == true)
                {
                    assembler.Output(isOutputCOE, outputpath);
                }                
            }
            else
            {
                OutputRichTextBox.Text = assembler.Error.ToString();
            }
        }

        private void HEXRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if(OutputRichTextBox.Text!=string.Empty)
            {
                AssembleButton_Click(null, null);
            }
            
        }

        private void BinaryRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (OutputRichTextBox.Text != string.Empty)
            {
                AssembleButton_Click(null, null);
            }
        }

        private string FormatHex(bool[] boolArray)
        {
            string Hexstr="0x";
            for (int i = 0; i < 8; i++)
            {
                int tempi = 0;
                tempi += 8 * Convert.ToInt32(boolArray[i * 4]) + 4 * Convert.ToInt32(boolArray[i * 4 + 1]) + 2 * Convert.ToInt32(boolArray[i * 4 + 2]) + Convert.ToInt32(boolArray[i * 4 + 3]);
                if (tempi >= 0 && tempi < 10)
                {
                    Hexstr += tempi.ToString();
                }
                else
                {
                    if (tempi == 10)
                    {
                        Hexstr += "a";
                    }
                    else if (tempi == 11)
                    {
                        Hexstr += "b";
                    }
                    else if (tempi == 12)
                    {
                        Hexstr += "c";
                    }
                    else if (tempi == 13)
                    {
                        Hexstr += "d";
                    }
                    else if (tempi == 14)
                    {
                        Hexstr += "e";
                    }
                    else if (tempi == 15)
                    {
                        Hexstr += "f";
                    }
                }
            }


            return Hexstr;
        }
    }
}
