using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MIPS246.Core.DataStructure;
using MIPS246.Core.TestCodeGeneator;
using System.Configuration;

namespace MIPS246.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Mnemonic> cmdList = new List<Mnemonic>();
            foreach (Mnemonic m in Enum.GetValues(typeof(Mnemonic)))
            {
                if (ConfigurationManager.AppSettings[m.ToString()] == "true")
                {
                    cmdList.Add(m);
                    Console.WriteLine(m.ToString());
                }
            }

            TestCodeGeneator.ConfigGeneator(100, int.Parse(ConfigurationManager.AppSettings["maximm"]), int.Parse(ConfigurationManager.AppSettings["minimm"]), cmdList);
            TestCodeGeneator.Generate();

            Console.ReadLine();
        }
    }
}
