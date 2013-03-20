using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MIPS246.Core.Compiler;

namespace MIPS246.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string aa = "	int a=5;//hello";
            MIPS246.Core.Compiler.Scanner.GetSymbol(aa);
            Console.ReadLine();
        }
    }
}
