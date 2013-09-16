using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Assembler.GUI
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        /// 


        //assembler windows interface main function
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AssemblerMainWindow());
        }
    }
}
