using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace MIPS246.Core.DataStructure
{
    public enum Mnemonic
    {
        ADD,ADDU,SUB,SUBU,AND,OR,XOR,NOR,SLT,SLTU,SLL,SRL,SRA,SLLV,SRLV,SRAV,JR,JALR,
        ADDI,ADDIU,ANDI,ORI,XORI,LUI,SLTI,SLTIU,LW,SW,LB,LBU,LH,LHU,SB,SH,BEQ,BNE,BGEZ,
        BGEZAL,BGTZ,BLEZ,BLTZ,BLTZAL,J,JAL,SUBI,MOVE,NOP,LI,LA,SYSCALL,
        NULL
    }

    public enum Register
    {
        ZERO,AT,V0,v1,A0,A1,A2,A3,T0,T1,T2,T3,T4,T5,T6,T7,S0,S1,S2,S3,S4,S5,S6,S7,T8,T9,K0,K1,GP,SP,FP,RA
    }

    public class Instruction
    {
        #region Fields
        private Mnemonic mnemonic;
        private bool[] machine_code;
        private string arg1, arg2, arg3;
        private uint address;
        private static Hashtable AssemblerTable;
        private static Hashtable DisassemblerTable;
        #endregion

        #region Constructors
        static Instruction()
        {
            InitAssemblerTable();
        }

        public Instruction(string mnemonic, string arg1, string arg2, string arg3, uint address)
        {
            this.mnemonic = (Mnemonic)Enum.Parse(typeof(Mnemonic),mnemonic);
            this.arg1 = arg1;
            this.arg2 = arg2;
            this.arg3 = arg3;
            this.address = address;
        }

        public Instruction(string mnemonic, string arg1, string arg2, string arg3)
        {
            this.mnemonic = (Mnemonic)Enum.Parse(typeof(Mnemonic), mnemonic);
            this.arg1 = arg1;
            this.arg2 = arg2;
            this.arg3 = arg3;
        }

        public Instruction(bool[] machine_code)
        {
            this.machine_code = machine_code;
        }
        #endregion

        #region Properties
        public uint Address
        {
            get
            {
                return this.address;
            }
        }

        public bool[] Machine_Code
        {
            get
            {
                return this.machine_code;
            }
        }

        public string Arg1
        {
            get
            {
                return this.arg1;
            }
            set
            {
                this.arg1 = value;
            }
        }

        public string Arg2
        {
            get
            {
                return this.arg2;
            }
            set
            {
                this.arg2 = value;
            }
        }

        public string Arg3
        {
            get
            {
                return this.arg3;
            }
            set
            {
                this.arg3 = value;
            }
        }

        public Mnemonic Mnemonic
        {
            get
            {
                return this.mnemonic;
            }
            set
            {
                this.mnemonic = value;
            }
        }
        #endregion

        #region Public Methods
        public void Validate()
        {
            
            if (this.mnemonic != Mnemonic.NULL)
            {
                ToMachineCode();                
            }
            else
            {
                
                
            }
        }

        
        #endregion

        #region Internal Methods
        private static bool[] InitBoolArray(string codestring)
        {
            bool[] machine_code = new bool[32];
            for (int i = 0; i < 32; i++)
            {
                if (codestring[i] == '0') machine_code[i] = false;
                else machine_code[i] = true;
            }
            return machine_code;
        }

        private static void InitAssemblerTable()
        {
            AssemblerTable = new Hashtable();
            AssemblerTable.Add(Mnemonic.ADD, "00000000000000000000000000100000");
            AssemblerTable.Add(Mnemonic.ADDU, "00000000000000000000000000100001");
            AssemblerTable.Add(Mnemonic.SUB, "00000000000000000000000000100010");
            AssemblerTable.Add(Mnemonic.SUBU, "00000000000000000000000000100011");
            AssemblerTable.Add(Mnemonic.AND, "00000000000000000000000000100100");
            AssemblerTable.Add(Mnemonic.OR, "00000000000000000000000000100101");
            AssemblerTable.Add(Mnemonic.XOR, "00000000000000000000000000100110");
            AssemblerTable.Add(Mnemonic.NOR, "00000000000000000000000000100111");
            AssemblerTable.Add(Mnemonic.SLT, "00000000000000000000000000101010");
            AssemblerTable.Add(Mnemonic.SLTU, "00000000000000000000000000101011");
            AssemblerTable.Add(Mnemonic.SLL, "00000000000000000000000000000000");
            AssemblerTable.Add(Mnemonic.SRL, "00000000000000000000000000000010");
            AssemblerTable.Add(Mnemonic.SRA, "00000000000000000000000000000011");
            AssemblerTable.Add(Mnemonic.SLLV, "00000000000000000000000000000100");
            AssemblerTable.Add(Mnemonic.SRLV, "00000000000000000000000000000110");
            AssemblerTable.Add(Mnemonic.SRAV, "00000000000000000000000000000111");
            AssemblerTable.Add(Mnemonic.JR, "00000000000000000000000000001000");
            AssemblerTable.Add(Mnemonic.JALR,"00000000000000001111100000001001");

            AssemblerTable.Add(Mnemonic.ADDI, "00100000000000000000000000000000");
            AssemblerTable.Add(Mnemonic.ADDIU, "00100100000000000000000000000000");
            AssemblerTable.Add(Mnemonic.ANDI, "00110000000000000000000000000000");
            AssemblerTable.Add(Mnemonic.ORI, "00110100000000000000000000000000");
            AssemblerTable.Add(Mnemonic.XORI, "00111000000000000000000000000000");
            AssemblerTable.Add(Mnemonic.LUI, "00111100000000000000000000000000");
            AssemblerTable.Add(Mnemonic.SLTI, "00101000000000000000000000000000");
            AssemblerTable.Add(Mnemonic.SLTIU, "00101100000000000000000000000000");
            AssemblerTable.Add(Mnemonic.LW, "10001100000000000000000000000000");
            AssemblerTable.Add(Mnemonic.SW, "10101100000000000000000000000000");
            AssemblerTable.Add(Mnemonic.LB, "10000000000000000000000000000000");
            AssemblerTable.Add(Mnemonic.LBU, "10010000000000000000000000000000");
            AssemblerTable.Add(Mnemonic.LH, "10000100000000000000000000000000");
            AssemblerTable.Add(Mnemonic.LHU, "10010100000000000000000000000000");
            AssemblerTable.Add(Mnemonic.SB, "10100000000000000000000000000000");
            AssemblerTable.Add(Mnemonic.SH, "10100100000000000000000000000000");
            AssemblerTable.Add(Mnemonic.BEQ, "00010000000000000000000000000000");
            AssemblerTable.Add(Mnemonic.BNE, "00010100000000000000000000000000");
            AssemblerTable.Add(Mnemonic.BGEZ, "00000100000000010000000000000000");
            AssemblerTable.Add(Mnemonic.BGEZAL, "00000100000100010000000000000000");
            AssemblerTable.Add(Mnemonic.BGTZ,"00011100000000000000000000000000");
            AssemblerTable.Add(Mnemonic.BLEZ, "00011000000000000000000000000000");
            AssemblerTable.Add(Mnemonic.BLTZ, "00000000000000000000000000000000");
            AssemblerTable.Add(Mnemonic.BLTZAL, "00000100000100000000000000000000");

            AssemblerTable.Add(Mnemonic.J, "00001000000000000000000000000000");
            AssemblerTable.Add(Mnemonic.JAL, "00001100000000000000000000000000");

            //AssemblerTable.Add(Mnemonic.SUBI, InitBoolArray(""));
            //AssemblerTable.Add(Mnemonic.MOVE, InitBoolArray(""));
            //AssemblerTable.Add(Mnemonic.NOP, InitBoolArray(""));
            //AssemblerTable.Add(Mnemonic.LI, InitBoolArray(""));
            //AssemblerTable.Add(Mnemonic.LA, InitBoolArray(""));
            //AssemblerTable.Add(Mnemonic.SYSCALL, InitBoolArray(""));
        }

        private void ToMachineCode()
        {
            this.machine_code = InitBoolArray(AssemblerTable[this.mnemonic].ToString());
            switch (this.mnemonic)
            {
                case Mnemonic.ADD:
                case Mnemonic.ADDU:
                case Mnemonic.SUB:
                case Mnemonic.SUBU:
                case Mnemonic.AND:
                case Mnemonic.OR:
                case Mnemonic.XOR:
                case Mnemonic.NOR:
                case Mnemonic.SLT:
                case Mnemonic.SLTU:            
                    bool[] rs = new bool[5];
                    bool[] rt = RegtoBin(this.arg2);
                    bool[] rd = RegtoBin(this.arg3);
                    for (int i = 0; i < 5; i++)
                    {
                        this.machine_code[i + 6] = rs[i];
                    }
                    for (int i = 0; i < 5; i++)
                    {
                        this.machine_code[i + 11] = rt[i];
                    }
                    for (int i = 0; i < 5; i++)
                    {
                        this.machine_code[i + 16] = rd[i];
                    }
                    break;
                case Mnemonic.SLL:
                case Mnemonic.SRL:
                case Mnemonic.SRA:
                    rs = RegtoBin(this.arg1);
                    rt = RegtoBin(this.arg2);
                    for (int i = 0; i < 5; i++)
                    {
                        this.machine_code[i + 6] = rs[i];
                    }
                    for (int i = 0; i < 5; i++)
                    {
                        this.machine_code[i + 11] = rt[i];
                    }
                    break;
                case Mnemonic.SLLV:
                case Mnemonic.SRLV:
                case Mnemonic.SRAV:
                    rs = RegtoBin(this.arg1);
                    rt = RegtoBin(this.arg2);
                    rd = RegtoBin(this.arg3);
                    for (int i = 0; i < 5; i++)
                    {
                        this.machine_code[i + 6] = rs[i];
                    }
                    for (int i = 0; i < 5; i++)
                    {
                        this.machine_code[i + 11] = rt[i];
                    }
                    for (int i = 0; i < 5; i++)
                    {
                        this.machine_code[i + 16] = rd[i];
                    }
                    break;
                case Mnemonic.JR:
                case Mnemonic.JALR:
                    rs = RegtoBin(this.arg1);
                    for (int i = 0; i < 5; i++)
                    {
                        this.machine_code[i + 6] = rs[i];
                    }
                    break;
                case Mnemonic.ADDI:
                case Mnemonic.ADDIU:
                case Mnemonic.ANDI:
                case Mnemonic.ORI:
                case Mnemonic.XORI:
                    rs = RegtoBin(this.arg1);
                    rt = RegtoBin(this.arg2);
                    bool[] immediate = HEXtoBin16(this.arg3);
                    for (int i = 0; i < 5; i++)
                    {
                        this.machine_code[i + 6] = rs[i];
                    }
                    for (int i = 0; i < 5; i++)
                    {
                        this.machine_code[i + 11] = rt[i];
                    }
                    for (int i = 0; i < 16; i++)
                    {
                        this.machine_code[i + 16] = immediate[i];
                    }
                    break;
                case Mnemonic.LUI:
                    rt = RegtoBin(this.arg1);
                    immediate = HEXtoBin16(this.arg2);
                    for (int i = 0; i < 5; i++)
                    {
                        this.machine_code[i + 11] = rt[i];
                    }
                    for (int i = 0; i < 16; i++)
                    {
                        this.machine_code[i + 16] = immediate[i];
                    }
                    break;
                case Mnemonic.SLTI:
                case Mnemonic.SLTIU:
                case Mnemonic.LW:
                case Mnemonic.SW:
                case Mnemonic.LB:
                case Mnemonic.LBU:
                case Mnemonic.LH:
                case Mnemonic.LHU:
                case Mnemonic.SB:
                case Mnemonic.SH:
                case Mnemonic.BEQ:
                case Mnemonic.BNE:
                    rs = RegtoBin(this.arg1);
                    rt = RegtoBin(this.arg2);
                    bool[] offset = HEXtoBin16(this.arg3);
                    for (int i = 0; i < 5; i++)
                    {
                        this.machine_code[i + 6] = rs[i];
                    }
                    for (int i = 0; i < 5; i++)
                    {
                        this.machine_code[i + 11] = rt[i];
                    }
                    for (int i = 0; i < 16; i++)
                    {
                        this.machine_code[i + 16] = offset[i];
                    }
                    break;
                case Mnemonic.BGEZ:
                case Mnemonic.BGEZAL:
                case Mnemonic.BGTZ:
                case Mnemonic.BLEZ:
                case Mnemonic.BLTZAL:
                    rs = RegtoBin(this.arg1);
                    offset = HEXtoBin16(this.arg2);
                    for (int i = 0; i < 5; i++)
                    {
                        this.machine_code[i + 6] = rs[i];
                    }
                    for (int i = 0; i < 16; i++)
                    {
                        this.machine_code[i + 16] = offset[i];
                    }
                    break;
                case Mnemonic.J:
                case Mnemonic.JAL:
                    bool[] AddressArray = INTtoAddress(arg1);
                    for (int i = 0; i < 30; i++)
                    {
                        machine_code[i + 6] = AddressArray[i + 4];
                    }
                    break;
                /*
                case Mnemonic.SUBI:
                    break;
                case Mnemonic.MOVE:
                    break;
                 case Mnemonic.NOP:
                    break;
                 case Mnemonic.LI:
                    break;
                 case Mnemonic.LA:
                    break;
                 case Mnemonic.SYSCALL:
                    break;
                 */
                default:
                    return;

            }
        }

        private static bool[] INTtoAddress(string uintstring)
        {
            uint uintaddress = uint.Parse(uintstring);
            bool[] BinArray = new bool[32];
            string addressstring = Convert.ToString(uintaddress, 2);
            for (int i = 0; i < 32; i++)
            {
                if (addressstring[i] == '0')
                    BinArray[i] = false;
                else
                    BinArray[i] = true;
            }
            return BinArray;
        }

        private static bool[] HEXtoBin16(string HexString)
        {
            bool[] BinArray = new bool[16];
            for (int i = 0; i < 4; i++)
            {
                bool[] Bin4 = HextoBin(HexString[i]);
                for (int j = 0; j < 4; j++)
                {
                    BinArray[i * 4 + j] = Bin4[j];
                }
            }
            return BinArray;
        }

        private static bool[] HEXtoAddress(string HexString)
        {
            bool[] BinArray = new bool[32];
            for (int i = 0; i < 8; i++)
            {
                bool[] Bin4 = HextoBin(HexString[i]);
                for (int j = 0; j < 4; j++)
                {
                    BinArray[i * 4 + j] = Bin4[j];
                }
            }
            return BinArray;
        }

        private static bool[] HextoBin(char c)
        {
            bool[] BinArray;
             switch (c)
             {
                case '0':
                    BinArray = new bool[4] { false, false, false, false };
                    return BinArray;
                case '1':
                    BinArray = new bool[4] { false, false, false, true };
                    return BinArray;
                case '2':
                     BinArray = new bool[4] { false, false, true, false };
                     return BinArray;
                case '3':
                     BinArray = new bool[4] { false, false, true, true };
                     return BinArray;
                case '4':
                     BinArray = new bool[4] { false, true, false, false };
                     return BinArray;
                case '5':
                     BinArray = new bool[4] { false, true, false, true };
                     return BinArray;
                case '6':
                     BinArray = new bool[4] { false, true, true, false };
                     return BinArray;
                case '7':
                     BinArray = new bool[4] { false, true, true, true };
                     return BinArray;
                case '8':
                     BinArray = new bool[4] { true, false, false, false };
                     return BinArray;
                case '9':
                     BinArray = new bool[4] { true, false, false, true };
                     return BinArray;
                case 'A':
                case 'a':
                     BinArray = new bool[4] { true, false, true, false };
                     return BinArray;
                case 'B':
                case 'b':
                     BinArray = new bool[4] { true, false, true, true };
                     return BinArray;
                case 'C':
                case 'c':
                     BinArray = new bool[4] { true, true, false, false };
                     return BinArray;
                case 'D':
                case 'd':
                     BinArray = new bool[4] { true, true, false, true };
                     return BinArray;
                case 'E':
                case 'e':
                     BinArray = new bool[4] { true, true, true, false };
                     return BinArray;
                case 'F':
                case 'f':
                     BinArray = new bool[4] { true, true, true, true };
                     return BinArray;
                 default:
                     BinArray = new bool[4] { true, true, true, true };
                     return BinArray;
             }
        }

        private static bool[] RegtoBin(string reg)
        {
            switch (reg)
            {
                case "$0":
                case "$zero":
                    return new bool[5] { false, false, false, false, false };
                case "$1":
                case "$at":
                    return new bool[5] { false, false, false, false, true };
                case "$2":
                case "$v0":
                    return new bool[5] { false, false, false, true, false };
                case "$3":
                case "$v1":
                    return new bool[5] { false, false, false, true, true };
                case "$4":
                case "$a0":
                    return new bool[5] { false, false, true, false, false };
                case "$5":
                case "$a1":
                    return new bool[5] { false, false, true, false, true };
                case "$6":
                case "$a2":
                    return new bool[5] { false, false, true, true, false };
                case "$7":
                case "$a3":
                    return new bool[5] { false, false, true, true, true };
                case "$8":
                case "$t0":
                    return new bool[5] { false, true, false, false, false };
                case "$9":
                case "$t1":
                    return new bool[5] { false, true, false, false, true };
                case "$10":
                case "$t2":
                    return new bool[5] { false, true, false, true, false };
                case "$11":
                case "$t3":
                    return new bool[5] { false, true, false, true, true };
                case "$12":
                case "$t4":
                    return new bool[5] { false, true, true, false, false };
                case "$13":
                case "$t5":
                    return new bool[5] { false, true, true, false, true };
                case "$14":
                case "$t6":
                    return new bool[5] { false, true, true, true, false };
                case "$15":
                case "$t7":
                    return new bool[5] { false, true, true, true, true };
                case "$16":
                case "$s0":
                    return new bool[5] { true, false, false, false, false };
                case "$17":
                case "$s1":
                    return new bool[5] { true, false, false, false, true };
                case "$18":
                case "$s2":
                    return new bool[5] { true, false, false, true, false };
                case "$19":
                case "$s3":
                    return new bool[5] { true, false, false, true, true };
                case "$20":
                case "$s4":
                    return new bool[5] { true, false, true, false, false };
                case "$21":
                case "$s5":
                    return new bool[5] { true, false, true, false, true };
                case "$22":
                case "$s6":
                    return new bool[5] { true, false, true, true, false };
                case "$23":
                case "$s7":
                    return new bool[5] { true, false, true, true, true };
                case "$24":
                case "$t8":
                    return new bool[5] { true, true, false, false, false };
                case "$25":
                case "$t9":
                    return new bool[5] { true, true, false, false, true };
                case "$26":
                case "$k0":
                    return new bool[5] { true, true, false, true, false };
                case "$27":
                case "$k1":
                    return new bool[5] { true, true, false, true, true };
                case "$28":
                case "$gp":
                    return new bool[5] { true, true, true, false, false };
                case "$29":
                case "$sp":
                    return new bool[5] { true, true, true, false, true };
                case "$30":
                case "$fp":
                    return new bool[5] { true, true, true, true, false };
                case "$31":
                case "$ra":
                    return new bool[5] { true, true, true, true, true };
                default:
                    return new bool[5] { false, false, false, false, false };
            }
        }

        #endregion
    }
}
