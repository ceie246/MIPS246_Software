using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using MIPS246.Core.ResultComparer;

namespace MipsSimulator
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static int mode = 0;
        public static Form1 form1;

        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length>=1)
            {
                if (args.Length == 4)
                {
                    MipsSimulator.Cmd.cmdMode cmdMode = new Cmd.cmdMode();
                    cmdMode.start(args[0], args[1]);
                    string source = args[1];
                    string output = args[2];
                    int count1 = File.ReadAllLines(source).Length;
                    int count2 = File.ReadAllLines(output).Length;
                    if (count2 > count1)
                    {
                        string[] output2 = File.ReadAllLines(output);

                        FileInfo fInfo = new FileInfo(output);
                        if (fInfo.Exists)
                        {
                            fInfo.Delete();
                        }
                        FileStream fs = fInfo.OpenWrite();
                        StreamWriter w = new StreamWriter(fs);
                        if (count1 > 0)
                        {
                            for (int i = 0; i < count1; i++)
                            {
                                w.WriteLine(output2[i]);
                            }
                        }

                        w.Flush();
                        w.Close();
                        w.Dispose();
                    }
                    if (count1 > count2)
                    {
                        string[] output1 = File.ReadAllLines(source);

                        FileInfo fInfo = new FileInfo(source);
                        if (fInfo.Exists)
                        {
                            fInfo.Delete();
                        }
                        FileStream fs = fInfo.OpenWrite();
                        StreamWriter w = new StreamWriter(fs);
                        if (count2 > 0)
                        {
                            for (int i = 0; i < count2; i++)
                            {
                                w.WriteLine(output1[i]);
                            }
                        }

                        w.Flush();
                        w.Close();
                        w.Dispose();
                    }
                    string result = ResultComparer.Compare(source, output);
                    string resultPath = args[3];
                    FileInfo fInfoR = new FileInfo(resultPath);
                    if (fInfoR.Exists)
                    {
                        fInfoR.Delete();
                    }
                    FileStream fsR = fInfoR.OpenWrite();
                    StreamWriter wR = new StreamWriter(fsR);
                    //w.BaseStream.Seek(0, SeekOrigin.Begin);
                    wR.Write(result);
                    wR.Flush();
                    wR.Close();
                    wR.Dispose();
                }
                else
                {
                    string message = "参数输入有误，只允许两个参数：outputPath，inputPath\r\n";
                    string path = System.Environment.CurrentDirectory;
                    path = path + "\\report.txt";
                    MipsSimulator.Tools.FileControl.WriteFile(path, message);
                }
                
            }
            else
            {
                mode = 1;
               
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                form1 = new Form1();
                Application.Run(form1);
            }
        }
    }
}
