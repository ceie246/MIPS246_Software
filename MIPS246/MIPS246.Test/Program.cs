using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MIPS246.Core.DataStructure;
using MIPS246.Core.TestCodeGeneator;
using System.Configuration;
using MIPS246.Core.ResultComparer;

namespace MIPS246.Test
{
    class Program
    {
        static void Main(string[] args)
        {
<<<<<<< HEAD
            List<Mnemonic> cmdList = new List<Mnemonic>();
            foreach (Mnemonic m in Enum.GetValues(typeof(Mnemonic)))
            {
                if (ConfigurationManager.AppSettings[m.ToString()] == "true")
                {
                    cmdList.Add(m);
                    Console.WriteLine(m.ToString());
                }
            }

            TestCodeGeneator.ConfigGeneator(100,/* int.Parse(ConfigurationManager.AppSettings["maximm"]), int.Parse(ConfigurationManager.AppSettings["minimm"]),*/ cmdList);
            TestCodeGeneator.Generate();
=======
            string path1 = @"C:\Users\Alfred\Desktop\file_test.out";
            string path2 = @"C:\Users\Alfred\Desktop\file_test2.out";
>>>>>>> MIPS246_Software/master

            Console.WriteLine(ResultComparer.Compare(path1, path2));
            Console.ReadLine();
           // BitArray instr = new BitArray();
           // Instruction fortest = new Instruction(instr);

        }
    }
}
