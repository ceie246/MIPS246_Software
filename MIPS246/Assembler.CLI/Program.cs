using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MIPS246.Core.DataStructure;
using MIPS246.Core.Assembler;

namespace CEIE246.Core.Assembler.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            string outputpath = args[args.Length - 1] + ".bin";
            bool isDisplay = false;
            Console.WriteLine(args.Length);
            Console.WriteLine(outputpath);
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-h":
                    case "--help":
                        ShowHelp();
                        break;
                    case "-o":
                        outputpath = args[i + 1];
                        break;
                    case "-d":
                        isDisplay = true;
                        break;
                }
            }
        }

        static void ShowHelp()
        {
            Console.WriteLine("246-MIPS-Assembler:");
            Console.WriteLine("Options and arguments (and corresponding environment variables):");
            Console.WriteLine("-h\t:\tprint this help message and exit (also --help)");
            Console.WriteLine("-o\t:\tcustom output file name");
            Console.WriteLine("-d\t:\tdisplay in console without output a bin file");
        }

        
    }
}
