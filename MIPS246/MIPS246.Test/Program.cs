using System;
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
            string path1 = @"C:\Users\Alfred\Desktop\file_test.out";
            string path2 = @"C:\Users\Alfred\Desktop\file_test2.out";

            Console.WriteLine(ResultComparer.Compare(path1, path2));
            Console.ReadLine();
        }
    }
}
