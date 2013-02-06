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
            string sourcepath, outputpath;
            bool isDisplay = false;

            Console.WriteLine(args.Length);

            if (args.Length == 0)
            {
                ShowHelp();
                return;
            }
            else
            {
                if (args[args.Length - 1].Contains(".asm"))
                {
                    sourcepath = args[args.Length - 1].Substring(0, args[args.Length - 1].Length - 4);
                }
                else
                    sourcepath = args[args.Length - 1] + ".bin";
                outputpath = sourcepath + ".bin";
                
                Console.WriteLine(outputpath);
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
                    }
                }
                MIPS246.Core.Assembler.Assembler assembler = new MIPS246.Core.Assembler.Assembler(args[args.Length - 1]);
                if (assembler.doAssemble() == true)
                {
                    if (isDisplay == true)
                    {
                        Display(assembler);
                    }
                }
                else
                {

                }
            }

            Console.ReadLine();
            
        }

        static void ShowHelp()
        {
            Console.WriteLine("usage:\t246-MIPS-Assembler [-o obj objbinname | -d display | -h help ] target asmfile");
            Console.WriteLine("Options and arguments (and corresponding environment variables):");
            Console.WriteLine("-h : print this help message and exit (also --help)");
            Console.WriteLine("-o : custom output file name (also --obj)");
            Console.WriteLine("-d : display in console without output a bin file (also --display)");
        }

        static void Display(MIPS246.Core.Assembler.Assembler assembler)
        {
            foreach (Instruction i in assembler.CodeList)
            {
                Console.WriteLine(assembler.Display(i));
            }
        }
    }
}
