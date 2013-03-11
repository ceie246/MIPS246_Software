using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MIPS246.Core.DataStructure;


namespace CEIE246.Core.Compile.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            string sourcepath = null, outputpath = null;
            bool isDisplay = false;
            bool isDisplayBinary = false;
            bool isOutputCOE = false;

            if (args.Length == 0)
            {
                ShowHelp();
                return;
            }
            else
            {
                sourcepath = args[args.Length - 1];
                if (args[args.Length - 1].EndsWith(".c"))
                {
                    outputpath = args[args.Length - 1].Substring(0, args[args.Length - 1].Length - 2);
                }
            }
            outputpath += ".txt";

            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-h":
                    case "--help":
                        ShowHelp();
                        return;
                    case "-o":
                    case "--obj":
                        outputpath = args[i + 1];
                        break;
                    case "-d":
                    case "--display":
                        isDisplay = true;
                        break;
                    case "-b":
                        isDisplay = true;
                        isDisplayBinary = true;
                        break;
                    case "-t":
                        break;
                    case "-c":
                        isOutputCOE = true;
                        outputpath = outputpath.Substring(0, outputpath.Length - 4) + ".coe";
                        break;
                }
            }

            MIPS246.Core.Compiler.Compiler compiler = new MIPS246.Core.Compiler.Compiler(sourcepath, outputpath);
            if (compiler.DoCompile() == true)
            {
                if (isDisplay == true)
                {
                    compiler.Display(isDisplayBinary);
                }
                compiler.Output(isOutputCOE, outputpath);
            }
            else
            {
                compiler.DisplayError();
                return;
            }

            Console.ReadLine();
        }

        static void ShowHelp()
        {
            Console.WriteLine("usage:\t246-MIPS-C-Compiler [-o obj objbinname | -d display | -h help ] target asmfile");
            Console.WriteLine("Options and arguments (and corresponding environment variables):");
            Console.WriteLine("-h : print this help message and exit (also --help)");
            Console.WriteLine("-o : custom output file name (also --obj)");
            Console.WriteLine("-d : display in console in HEX machine code (also --display)");
            Console.WriteLine("-b : display in console in binary machine code");
            Console.WriteLine("-t : Output .txt file. (default)");
            Console.WriteLine("-c : Output .coe file.");
        }
    }
}
