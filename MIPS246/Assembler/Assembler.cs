using MIPS246.Core.DataStructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;


namespace MIPS246.Core.Assembler
{
    public class Assembler
    {
        #region Fields
        private List<Instruction> codelist;
        private List<string[]> sourceList;
        private AssemblerErrorInfo error;
        private string sourcepath;
        private string outputpath;
        private Hashtable linetable;
        private Hashtable addresstable;
        private Hashtable labeltable;

        //config
        private static int startAddress = 0;    //add 0 line, for future use
        #endregion

        #region Constructors
        public Assembler()
        {
        }

        public Assembler(string sourcepath, string outputpath)
        {
            this.sourcepath = sourcepath;
            this.outputpath = outputpath;
            sourceList = new List<string[]>();
            codelist = new List<Instruction>();
            linetable = new Hashtable();
            addresstable = new Hashtable();
            labeltable = new Hashtable();
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

        public AssemblerErrorInfo Error
        {
            get
            {
                return this.error;
            }
        }
        #endregion

        #region Public Methods
        public bool DoAssemble()
        {
            if (this.LoadFile() == false)
            {
                this.error = new AssemblerErrorInfo(0, AssemblerError.NOFILE);
                return false;
            }

            if (LoadAddress() == false)
            {
                return false;
            }

            if (CheckWord() == false)
            {
                return false;
            }

            WriteBackAddress();

            CalculOffset();

            InitInstructionAddress();

            AssemblerInstructions();
            return true;
        }

        public void Display(bool isBinary)
        {
            bool[] boolArray = new bool[32];
            
            for (int i = 0; i < codelist.Count; i++)
            {
                codelist[i].Machine_Code.CopyTo(boolArray, 0);
             
                if(isBinary)
                {
                    Console.Write("0x" + String.Format("{0:X8}", this.codelist[i].Address) + ":\t");
                    for (int j = 0; j < 32; j++)
                    {
                        if (boolArray[j] == true)
                        {
                            Console.Write("1");
                        }
                        else
                        {
                            Console.Write("0");
                        }
                    }
                    Console.WriteLine();
                }
                else
                {
                    Console.Write("0x" + String.Format("{0:X8}", this.codelist[i].Address) + ":\t");
                    Console.WriteLine(FormatHex(boolArray));
                }                
            }
        }

        public void Output(bool isoutputCOE, string outputpath)
        {
            bool[] boolArray = new bool[32];

            StreamWriter sr = new StreamWriter(outputpath);

            if (isoutputCOE == true)
            {
                sr.WriteLine("memory_initialization_radix=16;");
                sr.WriteLine("memory_initialization_vector=");
                for (int i = 0; i < codelist.Count; i++)
                {
                    this.codelist[i].Machine_Code.CopyTo(boolArray, 0);
                    for (int j = 0; j < 32; j++)
                    {
                        if (boolArray[j] == true)
                        {
                            sr.Write("1");
                        }
                        else
                        {
                            sr.Write("0");
                        }
                    }
                    if (i != codelist.Count - 1)
                    {
                        sr.WriteLine(",");
                    }
                    else
                    {
                        sr.WriteLine(";");
                    }
                    
                }
            }
            else
            {
                for (int i = 0; i < codelist.Count; i++)
                {
                    this.codelist[i].Machine_Code.CopyTo(boolArray, 0);
                    for (int j = 0; j < 32; j++)
                    {
                        if (boolArray[j] == true)
                        {
                            sr.Write("1");
                        }
                        else
                        {
                            sr.Write("0");
                        }
                    }
                    sr.WriteLine();
                }
            }
            
            

            sr.Close();
        }
        
        public void DisplayError()
        {
            Console.WriteLine("Compile failed:");
            this.error.ConsoleDisplay();
        }       
        #endregion

        #region Internal Methods
        private bool LoadFile()
        {
            if (File.Exists(sourcepath) == false)
            {
                this.error = new AssemblerErrorInfo(0, AssemblerError.NOFILE);
                return false;
            }
            else
            {
                StreamReader sr = new StreamReader(sourcepath);
                string linetext;
                for (int i = 0; ; i++)
                {
                    if ((linetext = sr.ReadLine()) == null) 
                    {
                        break;
                    }
                    else
                    {
                        linetext = RemoveComment(linetext);
                        if (linetext != "")
                        {
                            if (linetext.Split(new char[] { ' ', '\t', ',' }, StringSplitOptions.RemoveEmptyEntries).Length != 0)
                            {
                                linetable[sourceList.Count] = i;
                            sourceList.Add(linetext.Split(new char[] { ' ', '\t', ',' }, StringSplitOptions.RemoveEmptyEntries));
                            }
                            
                        }
                    }
                }

                sr.Close();
                return true;
            }
        }

        private bool LoadAddress()
        {
            for (int i = 0; i < sourceList.Count; i++)
            {
                if(sourceList[i][0].EndsWith(":"))
                {
                    string label = sourceList[i][0].Substring(0, sourceList[i][0].Length - 1);
                    if (CheckVariableName(label) == false)
                    {
                        this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.INVALIDLABEL, label);
                        return false;
                    }
                    addAddresstable(sourceList[i][0].Substring(0, sourceList[i][0].Length - 1), i);
                }
            }
            return true;
        }

        private bool CheckWord()
        {
            for (int i = 0; i < sourceList.Count; i++)
            {
                
                if (addresstable.ContainsValue(i - 1))
                {
                    addresstable[labeltable[i - 1].ToString()] = codelist.Count;
                }

                if (addresstable.ContainsValue(i)) continue;
                switch (sourceList[i][0].ToUpper())
                {
                    case ".GLOBL":
                        if (sourceList[i].Length != 2)
                        {
                            this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGARGUNUM, "2");
                            return false;
                        }
                        else if (CheckVariableName(sourceList[i][1]) == false)
                        {
                            this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.INVALIDLABEL, sourceList[i][1]);
                            return false;
                        }
                        else if (addresstable.ContainsKey(sourceList[i][1].ToString()) == false)
                        {
                            this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.ADDNOTFOUND, sourceList[i][1]);
                            return false;
                        }
                        else
                        {
                            SetAddress0(sourceList[i][1]);
                            break;
                        }
                        
                    case ".TEXT":
                    case ".DATA":
                    case ".WORD":
                        break;
                    case "ADD":
                        if (OP_ADD(i) == false) return false;
                        break;
                    case "ADDU":
                        if (OP_ADDU(i) == false) return false;
                        break;
                    case "SUB":
                        if (OP_SUB(i) == false) return false;
                        break;
                    case "SUBU":
                        if (OP_SUBU(i) == false) return false;
                        break;
                    case "AND":
                        if (OP_AND(i) == false) return false;
                        break;
                    case "OR":
                        if (OP_OR(i) == false) return false;
                        break;
                    case "XOR":
                        if (OP_XOR(i) == false) return false;
                        break;
                    case "NOR":
                        if (OP_NOR(i) == false) return false;
                        break;
                    case "SLT":
                        if (OP_SLT(i) == false) return false;
                        break;
                    case "SLTU":
                        if (OP_SLTU(i) == false) return false;
                        break;
                    case "SLL":
                        if (OP_SLL(i) == false) return false;
                        break;
                    case "SRL":
                        if (OP_SRL(i) == false) return false;
                        break;
                    case "SRA":
                        if (OP_SRA(i) == false) return false;
                        break;
                    case "SLLV":
                        if (OP_SLLV(i) == false) return false;
                        break;
                    case "SRLV":
                        if (OP_SRLV(i) == false) return false;
                        break;
                    case "SRAV":
                        if (OP_SRAV(i) == false) return false;
                        break;
                    case "JR":
                        if (OP_JR(i) == false) return false;
                        break;
                    case "JALR":
                        if (OP_JALR(i) == false) return false;
                        break;

                    case "ADDI":
                        if (OP_ADDI(i) == false) return false;
                        break;
                    case "ADDIU":
                        if (OP_ADDIU(i) == false) return false;
                        break;
                    case "ANDI":
                        if (OP_ANDI(i) == false) return false;
                        break;
                    case "ORI":
                        if (OP_ORI(i) == false) return false;
                        break;
                    case "XORI":
                        if (OP_XORI(i) == false) return false;
                        break;
                    case "LUI":
                        if (OP_LUI(i) == false) return false;
                        break;
                    case "SLTI":
                        if (OP_SLTI(i) == false) return false;
                        break;
                    case "SLTIU":
                        if (OP_SLTIU(i) == false) return false;
                        break;
                    case "LW":
                        if (OP_LW(i) == false) return false;
                        break;
                    case "SW":
                        if (OP_SW(i) == false) return false;
                        break;
                    case "LB":
                        if (OP_LB(i) == false) return false;
                        break;
                    case "LBU":
                        if ( OP_LBU(i) == false) return false;
                        break;
                    case "LH":
                        if (OP_LH(i) == false) return false;
                        break;
                    case "LHU":
                        if (OP_LHU(i) == false) return false;
                        break;
                    case "SB":
                        if (OP_SB(i) == false) return false;
                        break;
                    case "SH":
                        if (OP_SH(i) == false) return false;
                        break;
                    case "BEQ":
                        if (OP_BEQ(i) == false) return false;
                        break;
                    case "BNE":
                        if (OP_BNE(i) == false) return false;
                        break;

                    case "BGEZ":
                        if (OP_BGEZ(i) == false) return false;
                        break;
                    case "BGEZAL":
                        if (OP_BGEZAL(i) == false) return false;
                        break;
                    case "BGTZ":
                        if (OP_BGTZ(i) == false) return false;
                        break;
                    case "BLEZ":
                        if (OP_BLEZ(i) == false) return false;
                        break;
                    case "BLTZ":
                        if (OP_BLTZ(i) == false) return false;
                        break;
                    case "BLTZAL":
                        if (OP_BLTZAL(i) == false) return false;
                        break;
                    case "J":
                        if (OP_J(i) == false) return false;
                        break;
                    case "JAL":
                        if (OP_JAL(i) == false) return false;
                        break;

                    case "SUBI":
                        if (OP_SUBI(i) == false) return false;
                        break;
                    case "MOVE":
                        if (OP_MOVE(i) == false) return false;
                        break;
                    case "NOP":
                        if (OP_NOP(i) == false) return false;
                        break;
                    case "LI":
                        if (OP_LI(i) == false) return false;
                        break;
                    case "LA":
                        if (OP_LA(i) == false) return false;
                        break;
                    case "SYSCALL":
                        if (OP_SYSCALL(i) == false) return false;
                        break;

                    default:
                        if (sourceList[i].Length == 1 && sourceList[i][0] == string.Empty)
                            break;
                        else
                        {
                            this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.UNKNOWNCMD, sourceList[i][0]);
                            return false;
                        }                        
                }
                
            }
            return true;
        }

        private void WriteBackAddress()
        {
            for (int i = 0; i < codelist.Count; i++)
            {
                if (codelist[i].Mnemonic == Mnemonic.J || codelist[i].Mnemonic == Mnemonic.JAL)
                {
                    codelist[i].Arg1 = ((int)addresstable[codelist[i].Arg1]*4).ToString();
                }
            }
        }

        private void CalculOffset()
        {
            for (int i = 0; i < codelist.Count; i++)
            {
                if (codelist[i].Mnemonic == Mnemonic.BEQ || codelist[i].Mnemonic == Mnemonic.BNE ||
                        codelist[i].Mnemonic == Mnemonic.BGEZ || codelist[i].Mnemonic == Mnemonic.BGEZAL ||
                        codelist[i].Mnemonic == Mnemonic.BGTZ || codelist[i].Mnemonic == Mnemonic.BLTZ ||
                        codelist[i].Mnemonic == Mnemonic.BLTZAL)
                {
                    string addtemp=codelist[i].Arg3;
                    codelist[i].Arg3 = ((int)addresstable[addtemp]-i).ToString();
                }
            }

        }

        private void InitInstructionAddress()
        {
            for (int i = 0; i < this.codelist.Count; i++)
            {
                codelist[i].Address = i * 4;
            }            
        }

        private void AssemblerInstructions()
        {
            for (int i = 0; i < this.codelist.Count; i++)
            {
                codelist[i].Validate();
            }
        }

        private bool SetAddress0(string label)
        {
            startAddress = int.Parse(addresstable[label].ToString());
            return true;
        }

        private bool CheckVariableName(string name)
        {
            return Regex.IsMatch(name, "^[a-zA-Z_][a-zA-Z0-9_]*");
        }

        private void addAddresstable(string addressname, int address)
        {
            addresstable.Add(addressname, address);
            labeltable.Add(address, addressname);
        }

        private bool CheckRegister(string reg)
        {
            string regname = reg.ToLower();
            switch (regname)
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

        private string RemoveComment(string str)
        {
           if (str.Contains('#'))
           {
               if (str.IndexOf('#') != 0)
               {
                   str = str.Substring(0, str.IndexOf("#") - 1);
               }
               else
               {
                   str = string.Empty;
               }
           }
           return str;
        }

        private bool CheckShamt(string i)
        {
            int n;
            try
            {
                n = int.Parse(i);
            }
            catch
            {
                return false;
            }
            if (n >= 0 && n < 32) return true;
            else return false;
        }

        private bool ConvertImmediate(int i, string str, out int intvalue)
        {
            if (str.ToUpper().StartsWith("0X") == false)
            {
                try
                {
                    intvalue = int.Parse(str);
                }
                catch
                {
                    this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.INVALIDIMMEDIATE, str);
                    intvalue = 0;
                    return false;
                }                
            }
            else
            {
                try
                {
                    intvalue = Int32.Parse(str.Substring(2), System.Globalization.NumberStyles.HexNumber);
                }
                catch
                {
                    this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.INVALIDIMMEDIATE, str);
                    intvalue = 0;
                    return false;
                }
            }

            return true;
        }

        private string FormatHex(bool[] boolArray)
        {
            string Hexstr="0x";
            for (int i = 0; i < 8; i++)
            {
                int tempi = 0;
                tempi += 8 * Convert.ToInt32(boolArray[i * 4]) + 4 * Convert.ToInt32(boolArray[i * 4 + 1]) + 2 * Convert.ToInt32(boolArray[i * 4 + 2]) + Convert.ToInt32(boolArray[i * 4 + 3]);
                if (tempi >= 0 && tempi < 10)
                {
                    Hexstr += tempi.ToString();
                }
                else
                {
                    if (tempi == 10)
                    {
                        Hexstr += "a";
                    }
                    else if (tempi == 11)
                    {
                        Hexstr += "b";
                    }
                    else if (tempi == 12)
                    {
                        Hexstr += "c";
                    }
                    else if (tempi == 13)
                    {
                        Hexstr += "d";
                    }
                    else if (tempi == 14)
                    {
                        Hexstr += "e";
                    }
                    else if (tempi == 15)
                    {
                        Hexstr += "f";
                    }
                }
            }


            return Hexstr;
        }
        #endregion

        #region OPs
        private bool OP_ADD(int i)
        {
            if (sourceList[i].Length != 4)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGARGUNUM, "4");
                return false;
            }
            if (!CheckRegister(sourceList[i][1]) || !CheckRegister(sourceList[i][2]) || !CheckRegister(sourceList[i][3]))
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGREGNAME);
                return false;
            }
            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
            return true;
        }

        private bool OP_ADDU(int i)
        {
            if (sourceList[i].Length != 4)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGARGUNUM, "4");
                return false;
            }
            if (!CheckRegister(sourceList[i][1]) || !CheckRegister(sourceList[i][2]) || !CheckRegister(sourceList[i][3]))
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGREGNAME);
                return false;
            }
            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
            return true;
        }

        private bool OP_SUB(int i)
        {
            if (sourceList[i].Length != 4)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGARGUNUM, "4");
                return false;
            }
            if (!CheckRegister(sourceList[i][1]) || !CheckRegister(sourceList[i][2]) || !CheckRegister(sourceList[i][3]))
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGREGNAME);
                return false;
            }
            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
            return true;
        }

        private bool OP_SUBU(int i)
        {
            if (sourceList[i].Length != 4)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGARGUNUM, "4");
                return false;
            }
            if (!CheckRegister(sourceList[i][1]) || !CheckRegister(sourceList[i][2]) || !CheckRegister(sourceList[i][3]))
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGREGNAME);
                return false;
            }
            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
            return true;
        }

        private bool OP_AND(int i)
        {
            if (sourceList[i].Length != 4)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGARGUNUM, "4");
                return false;
            }
            if (!CheckRegister(sourceList[i][1]) || !CheckRegister(sourceList[i][2]) || !CheckRegister(sourceList[i][3]))
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGREGNAME);
                return false;
            }
            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
            return true;
        }

        private bool OP_OR(int i)
        {
            if (sourceList[i].Length != 4)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGARGUNUM, "4");
                return false;
            }
            if (!CheckRegister(sourceList[i][1]) || !CheckRegister(sourceList[i][2]) || !CheckRegister(sourceList[i][3]))
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGREGNAME);
                return false;
            }
            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
            return true;
        }

        private bool OP_XOR(int i)
        {
            if (sourceList[i].Length != 4)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGARGUNUM, "4");
                return false;
            }
            if (!CheckRegister(sourceList[i][1]) || !CheckRegister(sourceList[i][2]) || !CheckRegister(sourceList[i][3]))
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGREGNAME);
                return false;
            }
            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
            return true;
        }

        private bool OP_NOR(int i)
        {
            if (sourceList[i].Length != 4)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGARGUNUM, "4");
                return false;
            }
            if (!CheckRegister(sourceList[i][1]) || !CheckRegister(sourceList[i][2]) || !CheckRegister(sourceList[i][3]))
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGREGNAME);
                return false;
            }
            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
            return true;
        }

        private bool OP_SLT(int i)
        {
            if (sourceList[i].Length != 4)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGARGUNUM, "4");
                return false;
            }
            if (!CheckRegister(sourceList[i][1]) || !CheckRegister(sourceList[i][2]) || !CheckRegister(sourceList[i][3]))
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGREGNAME);
                return false;
            }
            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
            return true;
        }

        private bool OP_SLTU(int i)
        {
            if (sourceList[i].Length != 4)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGARGUNUM, "4");
                return false;
            }
            if (!CheckRegister(sourceList[i][1]) || !CheckRegister(sourceList[i][2]) || !CheckRegister(sourceList[i][3]))
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGREGNAME);
                return false;
            }
            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
            return true;
        }

        private bool OP_SLL(int i)
        {
            if (sourceList[i].Length != 4)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGARGUNUM, "4");
                return false;
            }
            if (!CheckRegister(sourceList[i][1]) || !CheckRegister(sourceList[i][2]))
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGREGNAME);
                return false;
            }

            if (CheckShamt(sourceList[i][3]) == false)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGSHAMT, sourceList[i][3]);
                return false;
            }
            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
            return true;
        }

        private bool OP_SRL(int i)
        {
            if (sourceList[i].Length != 4)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGARGUNUM, "4");
                return false;
            }
            if (!CheckRegister(sourceList[i][1]) || !CheckRegister(sourceList[i][2]))
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGREGNAME);
                return false;
            }

            if (CheckShamt(sourceList[i][3]) == false)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGSHAMT, sourceList[i][3]);
                return false;
            }
            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
            return true;
        }

        private bool OP_SRA(int i)
        {
            if (sourceList[i].Length != 4)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGARGUNUM, "4");
                return false;
            }
            if (!CheckRegister(sourceList[i][1]) || !CheckRegister(sourceList[i][2]))
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGREGNAME);
                return false;
            }

            if (CheckShamt(sourceList[i][3]) == false)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGSHAMT, sourceList[i][3]);
                return false;
            }
            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
            return true;
        }

        private bool OP_SLLV(int i)
        {
            if (sourceList[i].Length != 4)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGARGUNUM, "4");
                return false;
            }
            if (!CheckRegister(sourceList[i][1]) || !CheckRegister(sourceList[i][2]) || !CheckRegister(sourceList[i][3]))
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGREGNAME);
                return false;
            }
            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
            return true;
        }

        private bool OP_SRLV(int i)
        {
            if (sourceList[i].Length != 4)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGARGUNUM, "4");
                return false;
            }
            if (!CheckRegister(sourceList[i][1]) || !CheckRegister(sourceList[i][2]) || !CheckRegister(sourceList[i][3]))
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGREGNAME);
                return false;
            }
            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
            return true;
        }

        private bool OP_SRAV(int i)
        {
            if (sourceList[i].Length != 4)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGARGUNUM, "4");
                return false;
            }
            if (!CheckRegister(sourceList[i][1]) || !CheckRegister(sourceList[i][2]) || !CheckRegister(sourceList[i][3]))
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGREGNAME);
                return false;
            }
            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
            return true;
        }

        private bool OP_JR(int i)
        {
            if (sourceList[i].Length != 2)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGARGUNUM, "2");
                return false;
            }
            if (!CheckRegister(sourceList[i][1]))
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGREGNAME, sourceList[i][1]);
                return false;
            }
            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], string.Empty, string.Empty));
            return true;
        }

        private bool OP_JALR(int i)
        {
            if (sourceList[i].Length != 2)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGARGUNUM, "2");
                return false;
            }
            if (!CheckRegister(sourceList[i][1]))
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGREGNAME, sourceList[i][1]);
                return false;
            }
            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], string.Empty, string.Empty));
            return true;
        }

        private bool OP_ADDI(int i)
        {
            if (sourceList[i].Length != 4)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGARGUNUM, "4");
                return false;
            }
            if (!CheckRegister(sourceList[i][1]) || !CheckRegister(sourceList[i][2]))
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGREGNAME);
                return false;
            }

            int imm = new int();
            if (ConvertImmediate(i, sourceList[i][3], out imm) == false)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.INVALIDIMMEDIATE);
                return false;
            }
            else
            {
                sourceList[i][3] = imm.ToString();
            }
            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
            return true;
        }

        private bool OP_ADDIU(int i)
        {
            if (sourceList[i].Length != 4)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGARGUNUM, "4");
                return false;
            }
            if (!CheckRegister(sourceList[i][1]) || !CheckRegister(sourceList[i][2]))
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGREGNAME);
                return false;
            }

            int imm = new int();
            if (ConvertImmediate(i, sourceList[i][3], out imm) == false)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.INVALIDIMMEDIATE);
                return false;
            }
            else
            {
                sourceList[i][3] = imm.ToString();
            }
            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
            return true;
        }

        private bool OP_ANDI(int i)
        {
            if (sourceList[i].Length != 4)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGARGUNUM, "4");
                return false;
            }
            if (!CheckRegister(sourceList[i][1]) || !CheckRegister(sourceList[i][2]))
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGREGNAME);
                return false;
            }

            int imm = new int();
            if (ConvertImmediate(i, sourceList[i][3], out imm) == false)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.INVALIDIMMEDIATE);
                return false;
            }
            else
            {
                sourceList[i][3] = imm.ToString();
            }
            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
            return true;
        }

        private bool OP_ORI(int i)
        {
            if (sourceList[i].Length != 4)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGARGUNUM, "4");
                return false;
            }
            if (!CheckRegister(sourceList[i][1]) || !CheckRegister(sourceList[i][2]))
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGREGNAME);
                return false;
            }

            int imm = new int();
            if (ConvertImmediate(i, sourceList[i][3], out imm) == false)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.INVALIDIMMEDIATE);
                return false;
            }
            else
            {
                sourceList[i][3] = imm.ToString();
            }
            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
            return true;
        }

        private bool OP_XORI(int i)
        {
            if (sourceList[i].Length != 4)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGARGUNUM, "4");
                return false;
            }
            if (!CheckRegister(sourceList[i][1]) || !CheckRegister(sourceList[i][2]))
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGREGNAME);
                return false;
            }

            int imm = new int();
            if (ConvertImmediate(i, sourceList[i][3], out imm) == false)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.INVALIDIMMEDIATE);
                return false;
            }
            else
            {
                sourceList[i][3] = imm.ToString();
            }
            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
            return true;
        }

        private bool OP_LUI(int i)
        {
            if (sourceList[i].Length != 3)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGARGUNUM, "3");
                return false;
            }
            if (!CheckRegister(sourceList[i][1]))
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGREGNAME);
                return false;
            }

            int imm = new int();
            if (ConvertImmediate(i, sourceList[i][2], out imm) == false)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGREGNAME);
                return false;
            }
            else
            {
                sourceList[i][2] = imm.ToString();
            }
            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], string.Empty));
            return true;
        }

        private bool OP_SLTI(int i)
        {
            if (sourceList[i].Length != 4)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGARGUNUM, "4");
                return false;
            }
            if (!CheckRegister(sourceList[i][1]) || !CheckRegister(sourceList[i][2]))
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGREGNAME);
                return false;
            }

            int imm = new int();
            if (ConvertImmediate(i, sourceList[i][3], out imm) == false)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.INVALIDIMMEDIATE);
                return false;
            }
            else
            {
                sourceList[i][3] = imm.ToString();
            }
            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
            return true;
        }

        private bool OP_SLTIU(int i)
        {
            if (sourceList[i].Length != 4)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.INVALIDIMMEDIATE, "4");
                return false;
            }
            if (!CheckRegister(sourceList[i][1]) || !CheckRegister(sourceList[i][2]))
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGREGNAME);
                return false;
            }

            int imm = new int();
            if (ConvertImmediate(i, sourceList[i][3], out imm) == false)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.INVALIDIMMEDIATE);
                return false;
            }
            else
            {
                sourceList[i][3] = imm.ToString();
            }
            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
            return true;
        }

        private bool OP_LW(int i)
        {
            if (sourceList[i].Length != 3)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGARGUNUM, "3");
                return false;
            }
            if (!CheckRegister(sourceList[i][1]))
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGREGNAME);
                return false;
            }

            string[] SubArg = sourceList[i][2].Split(new Char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
            if (SubArg.Length != 2)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGARG, this.sourceList[i][2]);
                return false;
            }
            else
            {
                sourceList[i] = new string[] { sourceList[i][0], sourceList[i][1], SubArg[1], SubArg[0] };
                if (!CheckRegister(sourceList[i][2]))
                {
                    this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.INVALIDLABEL, this.sourceList[i][2]);
                    return false;
                }

                int imm = new int();
                if (ConvertImmediate(i, sourceList[i][3], out imm) == false)
                {
                    this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGOFFSET, this.sourceList[i][3]);
                    return false;
                }
                this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
                return true;
            }
        }

        private bool OP_SW(int i)
        {
            if (sourceList[i].Length != 3)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGARGUNUM, "3");
                return false;
            }
            if (!CheckRegister(sourceList[i][1]))
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGREGNAME);
                return false;
            }

            string[] SubArg = sourceList[i][2].Split(new Char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
            if (SubArg.Length != 2)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGARG, this.sourceList[i][2]);
                return false;
            }
            else
            {
                sourceList[i] = new string[] { sourceList[i][0], sourceList[i][1], SubArg[1], SubArg[0] };
                if (!CheckRegister(sourceList[i][2]))
                {
                    this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.INVALIDLABEL, this.sourceList[i][2]);
                    return false;
                }

                int imm = new int();
                if (ConvertImmediate(i, sourceList[i][3], out imm) == false)
                {
                    this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGOFFSET, this.sourceList[i][3]);
                    return false;
                }
                this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], imm.ToString()));
                return true;
            }
        }

        private bool OP_LB(int i)
        {
            if (sourceList[i].Length != 3)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGARGUNUM, "3");
                return false;
            }
            if (!CheckRegister(sourceList[i][1]))
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGREGNAME);
                return false;
            }

            string[] SubArg = sourceList[i][2].Split(new Char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
            if (SubArg.Length != 2)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGARG, this.sourceList[i][2]);
                return false;
            }
            else
            {
                sourceList[i] = new string[] { sourceList[i][0], sourceList[i][1], SubArg[1], SubArg[0] };
                if (!CheckRegister(sourceList[i][2]))
                {
                    this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.INVALIDLABEL, this.sourceList[i][2]);
                    return false;
                }

                int imm = new int();
                if (ConvertImmediate(i, sourceList[i][3], out imm) == false)
                {
                    this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGOFFSET, this.sourceList[i][3]);
                    return false;
                }
                this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
                return true;
            }
        }

        private bool OP_LBU(int i)
        {
            if (sourceList[i].Length != 3)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGARGUNUM, "3");
                return false;
            }
            if (!CheckRegister(sourceList[i][1]))
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGREGNAME);
                return false;
            }

            string[] SubArg = sourceList[i][2].Split(new Char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
            if (SubArg.Length != 2)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGARG, this.sourceList[i][2]);
                return false;
            }
            else
            {
                sourceList[i] = new string[] { sourceList[i][0], sourceList[i][1], SubArg[1], SubArg[0] };
                if (!CheckRegister(sourceList[i][2]))
                {
                    this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.INVALIDLABEL, this.sourceList[i][2]);
                    return false;
                }

                int imm = new int();
                if (ConvertImmediate(i, sourceList[i][3], out imm) == false)
                {
                    this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGOFFSET, this.sourceList[i][3]);
                    return false;
                }
                this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
                return true;
            }
        }

        private bool OP_LH(int i)
        {
            if (sourceList[i].Length != 3)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGARGUNUM, "3");
                return false;
            }
            if (!CheckRegister(sourceList[i][1]))
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGREGNAME);
                return false;
            }

            string[] SubArg = sourceList[i][2].Split(new Char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
            if (SubArg.Length != 2)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGARG, this.sourceList[i][2]);
                return false;
            }
            else
            {
                sourceList[i] = new string[] { sourceList[i][0], sourceList[i][1], SubArg[1], SubArg[0] };
                if (!CheckRegister(sourceList[i][2]))
                {
                    this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.INVALIDLABEL, this.sourceList[i][2]);
                    return false;
                }

                int imm = new int();
                if (ConvertImmediate(i, sourceList[i][3], out imm) == false)
                {
                    this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGOFFSET, this.sourceList[i][3]);
                    return false;
                }
                this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
                return true;
            }
        }

        private bool OP_LHU(int i)
        {
            if (sourceList[i].Length != 3)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGARGUNUM, "3");
                return false;
            }
            if (!CheckRegister(sourceList[i][1]))
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGREGNAME);
                return false;
            }

            string[] SubArg = sourceList[i][2].Split(new Char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
            if (SubArg.Length != 2)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGARG, this.sourceList[i][2]);
                return false;
            }
            else
            {
                sourceList[i] = new string[] { sourceList[i][0], sourceList[i][1], SubArg[1], SubArg[0] };
                if (!CheckRegister(sourceList[i][2]))
                {
                    this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.INVALIDLABEL, this.sourceList[i][2]);
                    return false;
                }

                int imm = new int();
                if (ConvertImmediate(i, sourceList[i][3], out imm) == false)
                {
                    this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGOFFSET, this.sourceList[i][3]);
                    return false;
                }
                this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
                return true;
            }
        }

        private bool OP_SB(int i)
        {
            if (sourceList[i].Length != 3)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGARGUNUM, "3");
                return false;
            }
            if (!CheckRegister(sourceList[i][1]))
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGREGNAME);
                return false;
            }

            string[] SubArg = sourceList[i][2].Split(new Char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
            if (SubArg.Length != 2)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGARG, this.sourceList[i][2]);
                return false;
            }
            else
            {
                sourceList[i] = new string[] { sourceList[i][0], sourceList[i][1], SubArg[1], SubArg[0] };
                if (!CheckRegister(sourceList[i][2]))
                {
                    this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.INVALIDLABEL, this.sourceList[i][2]);
                    return false;
                }

                int imm = new int();
                if (ConvertImmediate(i, sourceList[i][3], out imm) == false)
                {
                    this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGOFFSET, this.sourceList[i][3]);
                    return false;
                }
                this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
                return true;
            }
        }

        private bool OP_SH(int i)
        {
            if (sourceList[i].Length != 3)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGARGUNUM, "3");
                return false;
            }
            if (!CheckRegister(sourceList[i][1]))
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGREGNAME);
                return false;
            }

            string[] SubArg = sourceList[i][2].Split(new Char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
            if (SubArg.Length != 2)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGARG, this.sourceList[i][2]);
                return false;
            }
            else
            {
                sourceList[i] = new string[] { sourceList[i][0], sourceList[i][1], SubArg[1], SubArg[0] };
                if (!CheckRegister(sourceList[i][2]))
                {
                    this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.INVALIDLABEL, this.sourceList[i][2]);
                    return false;
                }

                int imm = new int();
                if (ConvertImmediate(i, sourceList[i][3], out imm) == false)
                {
                    this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGOFFSET, this.sourceList[i][3]);
                    return false;
                }
                this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
                return true;
            }
        }

        private bool OP_BEQ(int i)
        {
            if (sourceList[i].Length != 4)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGARGUNUM, "4");
                return false;
            }
            if (!CheckRegister(sourceList[i][1]) || !CheckRegister(sourceList[i][2]))
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGREGNAME);
                return false;
            }

            if (CheckAddress(sourceList[i][3]) == false)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.ADDNOTFOUND);
                return false;
            }

            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
            return true;
        }

        private bool OP_BNE(int i)
        {
            if (sourceList[i].Length != 4)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGARGUNUM, "4");
                return false;
            }
            if (!CheckRegister(sourceList[i][1]) || !CheckRegister(sourceList[i][2]))
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGREGNAME);
                return false;
            }

            if (CheckAddress(sourceList[i][3]) == false)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.ADDNOTFOUND);
                return false;
            }

            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
            return true;
        }

        private bool OP_BGEZ(int i)
        {
            if (sourceList[i].Length != 3)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGARGUNUM, "3");
                return false;
            }
            if (!CheckRegister(sourceList[i][1]))
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGREGNAME);
                return false;
            }

            if (CheckAddress(sourceList[i][3]) == false)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.ADDNOTFOUND);
                return false;
            }

            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
            return true;
        }

        private bool OP_BGEZAL(int i)
        {
            if (sourceList[i].Length != 3)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGARGUNUM, "3");
                return false;
            }
            if (!CheckRegister(sourceList[i][1]))
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGREGNAME);
                return false;
            }

            if (CheckAddress(sourceList[i][3]) == false)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.ADDNOTFOUND);
                return false;
            }

            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
            return true;
        }

        private bool OP_BGTZ(int i)
        {
            if (sourceList[i].Length != 3)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGARGUNUM, "3");
                return false;
            }
            if (!CheckRegister(sourceList[i][1]))
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGREGNAME);
                return false;
            }

            if (CheckAddress(sourceList[i][3]) == false)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.ADDNOTFOUND);
                return false;
            }

            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
            return true;
        }

        private bool OP_BLEZ(int i)
        {
            if (sourceList[i].Length != 3)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGARGUNUM, "3");
                return false;
            }
            if (!CheckRegister(sourceList[i][1]))
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGREGNAME);
                return false;
            }

            if (CheckAddress(sourceList[i][3]) == false)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.ADDNOTFOUND);
                return false;
            }

            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
            return true;
        }

        private bool OP_BLTZ(int i)
        {
            if (sourceList[i].Length != 3)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGARGUNUM, "3");
                return false;
            }
            if (!CheckRegister(sourceList[i][1]))
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGREGNAME);
                return false;
            }

            if (CheckAddress(sourceList[i][3]) == false)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.ADDNOTFOUND);
                return false;
            }

            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
            return true;
        }

        private bool OP_BLTZAL(int i)
        {
            if (sourceList[i].Length != 3)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGARGUNUM, "3");
                return false;
            }
            if (!CheckRegister(sourceList[i][1]))
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGREGNAME);
                return false;
            }

            if (CheckAddress(sourceList[i][3]) == false)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.ADDNOTFOUND);
                return false;
            }
            
            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
            return true;
        }

        private bool OP_J(int i)
        {
            if (sourceList[i].Length != 2)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGARGUNUM, "2");
                return false;
            }
            if (CheckAddress(sourceList[i][1]) == false)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.ADDNOTFOUND, sourceList[i][1]);
                return false;
            }
            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], string.Empty, string.Empty));
            return true;
        }

        private bool OP_JAL(int i)
        {
            if (sourceList[i].Length != 2)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.WRONGARGUNUM, "2");
                return false;
            }
            if (CheckAddress(sourceList[i][1]) == false)
            {
                this.error = new AssemblerErrorInfo((int)linetable[i], AssemblerError.ADDNOTFOUND, sourceList[i][1]);
                return false;
            }
            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], string.Empty, string.Empty));
            return true;
        }

        private bool OP_SUBI(int i)
        {
            return true;
        }

        private bool OP_MOVE(int i)
        {
            return true;
        }

        private bool OP_NOP(int i)
        {
            return true;
        }

        private bool OP_LI(int i)
        {
            return true;
        }

        private bool OP_LA(int i)
        {
            return true;
        }

        private bool OP_SYSCALL(int i)
        {
            return true;
        }
        #endregion
    }
}
