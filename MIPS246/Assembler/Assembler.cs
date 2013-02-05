using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using MIPS246.Core.DataStructure;


namespace MIPS246.Core.Assembler
{
    public class Assembler
    {
        #region Fields
        private List<Instruction> codelist;
        private List<AssemblerErrorInfo> errorlist;
        private string sourcepath;
        private uint address;
        private uint line;
        private Hashtable addresstable;
        private bool foundadd0;
        #endregion

        #region Constructors
        public Assembler(string sourcepath)
        {
            this.sourcepath = sourcepath;
            codelist = new List<Instruction>();
            errorlist = new List<AssemblerErrorInfo>();
            addresstable = new Hashtable();

            address = 0;
            line = 0;
            foundadd0 = false;
        }
        #endregion

        #region Properties
        public List<Instruction> CodeList
        {
            get
            {
                return this.codelist;
            }
        }
        #endregion

        #region Public Methods
        public bool doAssemble()
        {
            if (!File.Exists(sourcepath))
            {
                this.errorlist.Add(new AssemblerErrorInfo(0, AssemblerError.NOFILE, sourcepath));
                return false;
            }
            StreamReader sr = new StreamReader(sourcepath);
            string linetext;
            while ((linetext = sr.ReadLine()) != null) 
            {
                string[] split = RemoveComment(linetext).Split(new Char[] { ' ', '\t', ',' });

                Console.WriteLine(RemoveComment(linetext));

                CheckWord(split);
            }
            return true;
        }
        #endregion

        #region Internal Methods
        

        private void addAddresstable(string addressname, uint address)
        {
            addresstable.Add(addressname, address);
        }

        private void CheckWord(string [] split)
        {
            switch (split.Length)
            {
                case 1:
                    CheckOneWord(split);
                    break;
                case 2:
                    CheckTwoWord(split);
                    break;
                case 3:
                    CheckThreeWord(split);
                    break;
                default:
                    this.errorlist.Add(new AssemblerErrorInfo(line, AssemblerError.UNKNOWNCMD, "Line " + line + ": " + "Unknown command."));
                    line++;
                    break;
            }
        }

        private void CheckOneWord(string[] split)
        {
            switch (split[0])
            {
                case ".text":
                case ".data":
                case ".memory":
                    line++;
                    break;
                default:
                    this.errorlist.Add(new AssemblerErrorInfo(line, AssemblerError.UNKNOWNCMD, "Line " + line + ": " + "Unknown command."));
                    line++;
                    break;

            }
        }

        private void CheckTwoWord(string [] split)
        {
            switch (split[0])
            {
                case ".globl":
                    if (foundadd0 == false)
                    {
                        address = 0;
                        addAddresstable(split[1], address);
                        line++;
                        foundadd0 = true;
                        break;
                    }
                    else
                    {
                        this.errorlist.Add(new AssemblerErrorInfo(line, AssemblerError.TWOADD0, null));
                        line++;
                        break;
                    }
                case "JR":
                case "JALR":                
                    if (CheckRegister(split[1]) == true)
                    {
                        Instruction instruction = new Instruction(split[0], split[1], string.Empty, string.Empty, string.Empty);
                        instruction.Validate();
                        codelist.Add(instruction);
                        address += 4;
                        line++;
                        break;
                    }
                    else
                    {
                        this.errorlist.Add(new AssemblerErrorInfo(line, AssemblerError.WRONGREGNAME, "Line " + line + ": " + "Wrong register name:"));
                        line++;
                        break;
                    }
                case "J":
                case "JAL":
                    if (CheckAddress(split[1]))
                    {
                        Instruction instruction = new Instruction(split[0], this.ConvertAddress(split[1]), string.Empty, string.Empty, string.Empty);
                        instruction.Validate();
                        codelist.Add(instruction);
                        address += 4;
                        line++;
                        break;
                    }
                    else
                    {
                        this.errorlist.Add(new AssemblerErrorInfo(line, AssemblerError.UNKNOWNADDLABEL, "Line " + line + ": " + "The address label is not define: " + split[1]));
                        line++;
                        break;
                    }
                default:
                    this.errorlist.Add(new AssemblerErrorInfo(line, AssemblerError.UNKNOWNCMD, split[0] + " " + split[1]));
                    line++;
                    break;
            }
        }

        private void CheckThreeWord(string[] split)
        {

        }

        private bool CheckRegister(string reg)
        {
            switch (reg)
            {
                case "$0":
                case "$zero":
                case "$1":
                case "$at":
                case "$2":
                case "$v0":
                case "$3":
                case "$v1":
                case "$4":
                case "$a0":
                case "$5":
                case "$a1":
                case "$6":
                case "$a2":
                case "$7":
                case "$a3":
                case "$8":
                case "$t0":
                case "$9":
                case "$t1":
                case "$10":
                case "$t2":
                case "$11":
                case "$t3":
                case "$12":
                case "$t4":
                case "$13":
                case "$t5":
                case "$14":
                case "$t6":
                case "$15":
                case "$t7":
                case "$16":
                case "$s0":
                case "$17":
                case "$s1":
                case "$18":
                case "$s2":
                case "$19":
                case "$s3":
                case "$20":
                case "$s4":
                case "$21":
                case "$s5":
                case "$22":
                case "$s6":
                case "$23":
                case "$s7":
                case "$24":
                case "$t8":
                case "$25":
                case "$t9":
                case "$26":
                case "$k0":
                case "$27":
                case "$k1":
                case "$28":
                case "$gp":
                case "$29":
                case "$sp":
                case "$30":
                case "$fp":
                case "$31":
                case "$ra":
                    return true;
                default:
                    return false;
            }
        }

        private bool CheckAddress(string addressname)
        {
            return this.addresstable.Contains(addressname);
        }

        private string ConvertAddress(string addressname)
        {
            return this.addresstable[addressname].ToString();
        }

        private string RemoveComment(string text)
        {
            if (text.Contains('#'))
            {
                if (text.IndexOf('#') != 0)
                {
                    text = text.Substring(0, text.IndexOf("#") - 1);
                }
                else
                {
                    text = string.Empty;
                }
            }
            return text;
        }
        #endregion

        
    }
}
