using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

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
                if (args.Length == 2)
                {
                    MipsSimulator.Cmd.cmdMode cmdMode = new Cmd.cmdMode();
                    cmdMode.start(args[0], args[1]);
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
