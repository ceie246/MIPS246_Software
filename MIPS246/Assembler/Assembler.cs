using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using MIPS246.Core.DataStructure;


namespace MIPS246.Core.Assembler
{
    public class Assembler
    {
        #region Fields
        private List<Instruction> codelist;
        private List<string[]> sourceList;
        private AssemblerErrorInfo error;
        private string sourcepath;
        private int address;
        private int line;
        private Hashtable addresstable;

        //config
        private static int startAddress = 0;
        #endregion

        #region Constructors
        public Assembler(string sourcepath)
        {
            this.sourcepath = sourcepath;
            sourceList = new List<string[]>();
            codelist = new List<Instruction>();
            addresstable = new Hashtable();

            address = 0;
            line = 1;
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


            return true;
        }

        public string Display(Instruction instruction)
        {
            return "0x"+String.Format("{0:X8}", instruction.Address)+":\t"+DisplayHexCMD(instruction.Machine_Code);
        }
        
        public void DisplayError()
        {
            Console.WriteLine("Compile failed:");
            this.error.Display();
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
                while ((linetext = sr.ReadLine()) != null)
                {
                    linetext = RemoveComment(linetext);
                    sourceList.Add(linetext.Split());
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
                        this.error = new AssemblerErrorInfo(i, AssemblerError.INVALIDLABEL, label);
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
                if (addresstable.ContainsValue(i)) continue;
                switch (sourceList[i][0].ToUpper())
                {
                    case ".GLOBL":
                        if (sourceList[i].Length != 2)
                        {
                            this.error = new AssemblerErrorInfo(i, AssemblerError.WRONGARGUNUM, "2");
                            return false;
                        }
                        else if (CheckVariableName(sourceList[i][1]) == false)
                        {
                            this.error = new AssemblerErrorInfo(i, AssemblerError.INVALIDLABEL, sourceList[i][1]);
                            return false;
                        }
                        else if (addresstable.ContainsKey(sourceList[i][1].ToString()) == false)
                        {
                            this.error = new AssemblerErrorInfo(i, AssemblerError.ADDNOTFOUND, sourceList[i][1]);
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
                        return OP_ADD(i);
                    case "ADDU":
                        return OP_ADDU(i);
                    case "SUB":
                        return OP_SUB(i);
                    case "SUBU":
                        return OP_SUBU(i);
                    case "AND":
                        return OP_AND(i);
                    case "OR":
                        return OP_OR(i);
                    case "XOR":
                        return OP_XOR(i);
                    case "NOR":
                        return OP_NOR(i);
                    case "SLT":
                        return OP_SLT(i);
                    case "SLTU":
                        return OP_SLTU(i);
                    case "SLL":
                        return OP_SLL(i);
                    case "SRL":
                        return OP_SRL(i);
                    case "SRA":
                        return OP_SRA(i);
                    case "SLLV":
                        return OP_SLLV(i);
                    case "SRLV":
                        return OP_SRLV(i);
                    case "SRAV":
                        return OP_SRAV(i);
                    case "JR":
                        return OP_JR(i);
                    case "JALR":
                        return OP_JALR(i);

                    case "ADDI":
                        return OP_ADDI(i);
                    case "ADDIU":
                        return OP_ADDIU(i);
                    case "ANDI":
                        return OP_ANDI(i);
                    case "ORI":
                        return OP_ORI(i);
                    case "XORI":
                        return OP_XORI(i);
                    case "LUI":
                        return OP_LUI(i);
                    case "SLTI":
                        return OP_SLTI(i);
                    case "SLTIU":
                        return OP_SLTIU(i);
                    case "LW":
                        return OP_LW(i);
                    case "SW":
                        return OP_SW(i);
                    case "LB":
                        return OP_LB(i);
                    case "LBU":
                        return OP_LBU(i);
                    case "LH":
                        return OP_LH(i);
                    case "LHU":
                        return OP_LHU(i);
                    case "SB":
                        return OP_SB(i);
                    case "SH":
                        return OP_SH(i);
                    case "BEQ":
                        return OP_BEQ(i);
                    case "BNE":
                        return OP_BNE(i);

                    case "BGEZ":
                        return OP_BGEZ(i);
                    case "BGEZAL":
                        return OP_BGEZAL(i);
                    case "BGTZ":
                        return OP_BGTZ(i);
                    case "BLEZ":
                        return OP_BLEZ(i);
                    case "BLTZ":
                        return OP_BLTZ(i);
                    case "BLTZAL":
                        return OP_BLTZAL(i);
                    case "J":
                        return OP_J(i);
                    case "JAL":
                        return OP_JAL(i);

                    case "SUBI":
                        return OP_SUBI(i);
                    case "MOVE":
                        return OP_MOVE(i);
                    case "NOP":
                        return OP_NOP(i);
                    case "LI":
                        return OP_LI(i);
                    case "LA":
                        return OP_LA(i);
                    case "SYSCALL":
                        return OP_SYSCALL(i);

                    default:
                        this.error = new AssemblerErrorInfo(i, AssemblerError.UNKNOWNCMD, sourceList[i][0]);
                        return false;
                }                
            }
            return true;
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
        }

        private string DisplayHexCMD(bool[] machine_code)
        {
            string machine_codeSTR = string.Empty;
            for (int i = 0; i < 8; i++)
            {
                machine_codeSTR = machine_codeSTR + InttoHex(8 * BoolToInt(machine_code[i * 4]) +
                    4 * BoolToInt(machine_code[i * 4 + 1]) +
                    2 * BoolToInt(machine_code[i * 4 + 2]) +
                    BoolToInt(machine_code[i * 4 + 3]));
            }

            return machine_codeSTR;
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
        
        private string InttoHex(int i)
        {
            switch (i)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                    return i.ToString();
                case 11:
                    return "A";
                case 12:
                    return "B";
                case 13:
                    return "C";
                case 14:
                    return "D";
                case 15:
                    return "E";
                case 16:
                    return "F";
                default:
                    return i.ToString();
            }
        }

        private int BoolToInt(bool bit)
        {
            switch (bit)
            {
                case true:
                    return 1;
                case false:
                    return 0;
                default:
                    return 0;
            }
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
        #endregion

        #region OPs
        private bool OP_ADD(int i)
        {
            if (sourceList[i].Length != 4)
            {
                this.error = new AssemblerErrorInfo(i, AssemblerError.WRONGARGUNUM, "4");
                return false;
            }
            if (CheckRegister(sourceList[i][1]) && CheckRegister(sourceList[i][2]) && CheckRegister(sourceList[i][3]) == false)
            {
                this.error = new AssemblerErrorInfo(i, AssemblerError.WRONGREGNAME);
                return false;
            }
            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
            return true;
        }

        private bool OP_ADDU(int i)
        {
            if (sourceList[i].Length != 4)
            {
                this.error = new AssemblerErrorInfo(i, AssemblerError.WRONGARGUNUM, "4");
                return false;
            }
            if (CheckRegister(sourceList[i][1]) && CheckRegister(sourceList[i][2]) && CheckRegister(sourceList[i][3]) == false)
            {
                this.error = new AssemblerErrorInfo(i, AssemblerError.WRONGREGNAME);
                return false;
            }
            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
            return true;
        }

        private bool OP_SUB(int i)
        {
            if (sourceList[i].Length != 4)
            {
                this.error = new AssemblerErrorInfo(i, AssemblerError.WRONGARGUNUM, "4");
                return false;
            }
            if (CheckRegister(sourceList[i][1]) && CheckRegister(sourceList[i][2]) && CheckRegister(sourceList[i][3]) == false)
            {
                this.error = new AssemblerErrorInfo(i, AssemblerError.WRONGREGNAME);
                return false;
            }
            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
            return true;
        }

        private bool OP_SUBU(int i)
        {
            if (sourceList[i].Length != 4)
            {
                this.error = new AssemblerErrorInfo(i, AssemblerError.WRONGARGUNUM, "4");
                return false;
            }
            if (CheckRegister(sourceList[i][1]) && CheckRegister(sourceList[i][2]) && CheckRegister(sourceList[i][3]) == false)
            {
                this.error = new AssemblerErrorInfo(i, AssemblerError.WRONGREGNAME);
                return false;
            }
            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
            return true;
        }

        private bool OP_AND(int i)
        {
            if (sourceList[i].Length != 4)
            {
                this.error = new AssemblerErrorInfo(i, AssemblerError.WRONGARGUNUM, "4");
                return false;
            }
            if (CheckRegister(sourceList[i][1]) && CheckRegister(sourceList[i][2]) && CheckRegister(sourceList[i][3]) == false)
            {
                this.error = new AssemblerErrorInfo(i, AssemblerError.WRONGREGNAME);
                return false;
            }
            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
            return true;
        }

        private bool OP_OR(int i)
        {
            if (sourceList[i].Length != 4)
            {
                this.error = new AssemblerErrorInfo(i, AssemblerError.WRONGARGUNUM, "4");
                return false;
            }
            if (CheckRegister(sourceList[i][1]) && CheckRegister(sourceList[i][2]) && CheckRegister(sourceList[i][3]) == false)
            {
                this.error = new AssemblerErrorInfo(i, AssemblerError.WRONGREGNAME);
                return false;
            }
            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
            return true;
        }

        private bool OP_XOR(int i)
        {
            if (sourceList[i].Length != 4)
            {
                this.error = new AssemblerErrorInfo(i, AssemblerError.WRONGARGUNUM, "4");
                return false;
            }
            if (CheckRegister(sourceList[i][1]) && CheckRegister(sourceList[i][2]) && CheckRegister(sourceList[i][3]) == false)
            {
                this.error = new AssemblerErrorInfo(i, AssemblerError.WRONGREGNAME);
                return false;
            }
            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
            return true;
        }

        private bool OP_NOR(int i)
        {
            if (sourceList[i].Length != 4)
            {
                this.error = new AssemblerErrorInfo(i, AssemblerError.WRONGARGUNUM, "4");
                return false;
            }
            if (CheckRegister(sourceList[i][1]) && CheckRegister(sourceList[i][2]) && CheckRegister(sourceList[i][3]) == false)
            {
                this.error = new AssemblerErrorInfo(i, AssemblerError.WRONGREGNAME);
                return false;
            }
            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
            return true;
        }

        private bool OP_SLT(int i)
        {
            if (sourceList[i].Length != 4)
            {
                this.error = new AssemblerErrorInfo(i, AssemblerError.WRONGARGUNUM, "4");
                return false;
            }
            if (CheckRegister(sourceList[i][1]) && CheckRegister(sourceList[i][2]) && CheckRegister(sourceList[i][3]) == false)
            {
                this.error = new AssemblerErrorInfo(i, AssemblerError.WRONGREGNAME);
                return false;
            }
            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
            return true;
        }

        private bool OP_SLTU(int i)
        {
            if (sourceList[i].Length != 4)
            {
                this.error = new AssemblerErrorInfo(i, AssemblerError.WRONGARGUNUM, "4");
                return false;
            }
            if (CheckRegister(sourceList[i][1]) && CheckRegister(sourceList[i][2]) && CheckRegister(sourceList[i][3]) == false)
            {
                this.error = new AssemblerErrorInfo(i, AssemblerError.WRONGREGNAME);
                return false;
            }
            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
            return true;
        }

        private bool OP_SLL(int i)
        {
            if (sourceList[i].Length != 4)
            {
                this.error = new AssemblerErrorInfo(i, AssemblerError.WRONGARGUNUM, "4");
                return false;
            }
            if (CheckRegister(sourceList[i][1]) && CheckRegister(sourceList[i][2]) == false)
            {
                this.error = new AssemblerErrorInfo(i, AssemblerError.WRONGREGNAME);
                return false;
            }

            if (CheckShamt(sourceList[i][3]) == false)
            {
                this.error = new AssemblerErrorInfo(i, AssemblerError.WRONGSHAMT, sourceList[i][3]);
                return false;
            }
            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
            return true;
        }

        private bool OP_SRL(int i)
        {
            if (sourceList[i].Length != 4)
            {
                this.error = new AssemblerErrorInfo(i, AssemblerError.WRONGARGUNUM, "4");
                return false;
            }
            if (CheckRegister(sourceList[i][1]) && CheckRegister(sourceList[i][2]) == false)
            {
                this.error = new AssemblerErrorInfo(i, AssemblerError.WRONGREGNAME);
                return false;
            }

            if (CheckShamt(sourceList[i][3]) == false)
            {
                this.error = new AssemblerErrorInfo(i, AssemblerError.WRONGSHAMT, sourceList[i][3]);
                return false;
            }
            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
            return true;
        }

        private bool OP_SRA(int i)
        {
            if (sourceList[i].Length != 4)
            {
                this.error = new AssemblerErrorInfo(i, AssemblerError.WRONGARGUNUM, "4");
                return false;
            }
            if (CheckRegister(sourceList[i][1]) && CheckRegister(sourceList[i][2]) == false)
            {
                this.error = new AssemblerErrorInfo(i, AssemblerError.WRONGREGNAME);
                return false;
            }

            if (CheckShamt(sourceList[i][3]) == false)
            {
                this.error = new AssemblerErrorInfo(i, AssemblerError.WRONGSHAMT, sourceList[i][3]);
                return false;
            }
            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
            return true;
        }

        private bool OP_SLLV(int i)
        {
            if (sourceList[i].Length != 4)
            {
                this.error = new AssemblerErrorInfo(i, AssemblerError.WRONGARGUNUM, "4");
                return false;
            }
            if (CheckRegister(sourceList[i][1]) && CheckRegister(sourceList[i][2]) && CheckRegister(sourceList[i][3]) == false)
            {
                this.error = new AssemblerErrorInfo(i, AssemblerError.WRONGREGNAME);
                return false;
            }
            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
            return true;
        }

        private bool OP_SRLV(int i)
        {
            if (sourceList[i].Length != 4)
            {
                this.error = new AssemblerErrorInfo(i, AssemblerError.WRONGARGUNUM, "4");
                return false;
            }
            if (CheckRegister(sourceList[i][1]) && CheckRegister(sourceList[i][2]) && CheckRegister(sourceList[i][3]) == false)
            {
                this.error = new AssemblerErrorInfo(i, AssemblerError.WRONGREGNAME);
                return false;
            }
            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
            return true;
        }

        private bool OP_SRAV(int i)
        {
            if (sourceList[i].Length != 4)
            {
                this.error = new AssemblerErrorInfo(i, AssemblerError.WRONGARGUNUM, "4");
                return false;
            }
            if (CheckRegister(sourceList[i][1]) && CheckRegister(sourceList[i][2]) && CheckRegister(sourceList[i][3]) == false)
            {
                this.error = new AssemblerErrorInfo(i, AssemblerError.WRONGREGNAME);
                return false;
            }
            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], sourceList[i][2], sourceList[i][3]));
            return true;
        }

        private bool OP_JR(int i)
        {
            if (CheckVariableName(sourceList[i][1]) == false)
            {
                this.error = new AssemblerErrorInfo(i, AssemblerError.INVALIDLABEL, sourceList[i][1]);
                return false;
            }
            if (CheckAddress(sourceList[i][1]) == false)
            {
                this.error = new AssemblerErrorInfo(i, AssemblerError.ADDNOTFOUND, sourceList[i][1]);
                return false;
            }
            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], string.Empty, string.Empty));
            return true;
        }

        private bool OP_JALR(int i)
        {
            if (CheckVariableName(sourceList[i][1]) == false)
            {
                this.error = new AssemblerErrorInfo(i, AssemblerError.INVALIDLABEL, sourceList[i][1]);
                return false;
            }
            if (CheckAddress(sourceList[i][1]) == false)
            {
                this.error = new AssemblerErrorInfo(i, AssemblerError.ADDNOTFOUND, sourceList[i][1]);
                return false;
            }
            this.codelist.Add(new Instruction(sourceList[i][0], sourceList[i][1], string.Empty, string.Empty));
            return true;
        }

        private bool OP_ADDI(int i)
        {
            return true;
        }

        private bool OP_ADDIU(int i)
        {
            return true;
        }

        private bool OP_ANDI(int i)
        {
            return true;
        }

        private bool OP_ORI(int i)
        {
            return true;
        }

        private bool OP_XORI(int i)
        {
            return true;
        }

        private bool OP_LUI(int i)
        {
            return true;
        }

        private bool OP_SLTI(int i)
        {
            return true;
        }

        private bool OP_SLTIU(int i)
        {
            return true;
        }

        private bool OP_LW(int i)
        {
            return true;
        }

        private bool OP_SW(int i)
        {
            return true;
        }

        private bool OP_LB(int i)
        {
            return true;
        }

        private bool OP_LBU(int i)
        {
            return true;
        }

        private bool OP_LH(int i)
        {
            return true;
        }

        private bool OP_LHU(int i)
        {
            return true;
        }

        private bool OP_SB(int i)
        {
            return true;
        }

        private bool OP_SH(int i)
        {
            return true;
        }

        private bool OP_BEQ(int i)
        {
            return true;
        }

        private bool OP_BNE(int i)
        {
            return true;
        }

        private bool OP_BGEZ(int i)
        {
            return true;
        }

        private bool OP_BGEZAL(int i)
        {
            return true;
        }

        private bool OP_BGTZ(int i)
        {
            return true;
        }

        private bool OP_BLEZ(int i)
        {
            return true;
        }

        private bool OP_BLTZ(int i)
        {
            return true;
        }

        private bool OP_BLTZAL(int i)
        {
            return true;
        }

        private bool OP_J(int i)
        {
            return true;
        }

        private bool OP_JAL(int i)
        {
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
