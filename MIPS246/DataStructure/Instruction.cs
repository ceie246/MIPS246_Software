using System;
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
        private BitArray machine_code;
        private string arg1, arg2, arg3;
        private int address;
        private static Hashtable AssemblerTable;
        private static Hashtable DisassemblerTable;
        #endregion

        #region Constructors
        static Instruction()
        {
            InitAssemblerTable();
        }

        public Instruction(string mnemonic, string arg1, string arg2, string arg3, int address)
        {
            this.mnemonic = (Mnemonic)Enum.Parse(typeof(Mnemonic),mnemonic);
            this.arg1 = arg1;
            this.arg2 = arg2;
            this.arg3 = arg3;
            this.address = address;
        }

        public Instruction(string mnemonic, string arg1, string arg2, string arg3)
        {
            this.mnemonic = (Mnemonic)Enum.Parse(typeof(Mnemonic), mnemonic.ToUpper());
            this.arg1 = arg1;
            this.arg2 = arg2;
            this.arg3 = arg3;
        }

        public Instruction(Mnemonic mnemonic, Register arg1, Register arg2, Register arg3)
        {
            this.mnemonic = mnemonic;
            this.arg1 = arg1.ToString();
            this.arg2 = arg2.ToString();
            this.arg3 = arg3.ToString();
        }

        public Instruction(BitArray machine_code)
        {
            this.machine_code = machine_code;
        }
        #endregion

        #region Properties
        public int Address
        {
            get
            {
                return this.address;
            }
            set
            {
                this.address = value;
            }
        }

        public BitArray Machine_Code
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
        private static BitArray InitBoolArray(string codestring)
        {
            BitArray machine_code = new BitArray(32);
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
                    OP_ADD();
                    break;
                case Mnemonic.ADDU:
                    OP_ADDU();
                    break;
                case Mnemonic.SUB:
                    OP_SUB();
                    break;
                case Mnemonic.SUBU:
                    OP_SUBU();
                    break;
                case Mnemonic.AND:
                    OP_AND();
                    break;
                case Mnemonic.OR:
                    OP_OR();
                    break;
                case Mnemonic.XOR:
                    OP_XOR();
                    break;
                case Mnemonic.NOR:
                    OP_NOR();
                    break;
                case Mnemonic.SLT:
                    OP_SLT();
                    break;
                case Mnemonic.SLTU:
                    OP_SLTU();
                    break;
                case Mnemonic.SLL:
                    OP_SLL();
                    break;
                case Mnemonic.SRL:
                    OP_SRL();
                    break;
                case Mnemonic.SRA:
                    OP_SRA();
                    break;
                case Mnemonic.SLLV:
                    OP_SLLV();
                    break;
                case Mnemonic.SRLV:
                    OP_SRLV();
                    break;
                case Mnemonic.SRAV:
                    OP_SRAV();
                    break;
                case Mnemonic.JR:
                    OP_JR();
                    break;
                case Mnemonic.JALR:
                    OP_JALR();
                    break;
                case Mnemonic.ADDI:
                    OP_ADDI();
                    break;
                case Mnemonic.ADDIU:
                    OP_ADDIU();
                    break;
                case Mnemonic.ANDI:
                    OP_ANDI();
                    break;
                case Mnemonic.ORI:
                    OP_ORI();
                    break;
                case Mnemonic.XORI:
                    OP_XORI();
                    break;
                case Mnemonic.LUI:
                    OP_LUI();
                    break;
                case Mnemonic.SLTI:
                    OP_SLTI();
                    break;
                case Mnemonic.SLTIU:
                    OP_SLTIU();
                    break;
                case Mnemonic.LW:
                    OP_LW();
                    break;
                case Mnemonic.SW:
                    OP_SW();
                    break;
                case Mnemonic.LB:
                    OP_LB();
                    break;
                case Mnemonic.LBU:
                    OP_LBU();
                    break;
                case Mnemonic.LH:
                    OP_LH();
                    break;
                case Mnemonic.LHU:
                    OP_LHU();
                    break;
                case Mnemonic.SB:
                    OP_SB();
                    break;
                case Mnemonic.SH:
                    OP_SH();
                    break;
                case Mnemonic.BEQ:
                    OP_BEQ();
                    break;
                case Mnemonic.BNE:
                    OP_BNE();
                    break;
                case Mnemonic.BGEZ:
                    OP_BGEZ();
                    break;
                case Mnemonic.BGEZAL:
                    OP_BGEZAL();
                    break;
                case Mnemonic.BGTZ:
                    OP_BGTZ();
                    break;
                case Mnemonic.BLEZ:
                    OP_BLEZ();
                    break;
                case Mnemonic.BLTZAL:
                    OP_BLTZAL();
                    break;
                case Mnemonic.J:
                    OP_J();
                    break;
                case Mnemonic.JAL:
                    OP_JAL();
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

        private bool ConvertBit(string i)
        {
            if (i == "1")
                return true;
            else
                return false;
        }

        private void setRegMachineCode(int startPosition, string reg)
        {
            switch (reg)
            {
                case "$0":
                case "$zero":
                    this.machine_code[startPosition] = false;
                    this.machine_code[startPosition + 1] = false;
                    this.machine_code[startPosition + 2] = false;
                    this.machine_code[startPosition + 3] = false;
                    this.machine_code[startPosition + 4] = false;
                    break;
                case "$1":
                case "$at":
                    this.machine_code[startPosition] = false;
                    this.machine_code[startPosition + 1] = false;
                    this.machine_code[startPosition + 2] = false;
                    this.machine_code[startPosition + 3] = false;
                    this.machine_code[startPosition + 4] = true;
                    break;
                case "$2":
                case "$v0":
                    this.machine_code[startPosition] = false;
                    this.machine_code[startPosition + 1] = false;
                    this.machine_code[startPosition + 2] = false;
                    this.machine_code[startPosition + 3] = true;
                    this.machine_code[startPosition + 4] = false;
                    break;
                case "$3":
                case "$v1":
                    this.machine_code[startPosition] = false;
                    this.machine_code[startPosition + 1] = false;
                    this.machine_code[startPosition + 2] = false;
                    this.machine_code[startPosition + 3] = true;
                    this.machine_code[startPosition + 4] = true;
                    break;
                case "$4":
                case "$a0":
                    this.machine_code[startPosition] = false;
                    this.machine_code[startPosition + 1] = false;
                    this.machine_code[startPosition + 2] = true;
                    this.machine_code[startPosition + 3] = false;
                    this.machine_code[startPosition + 4] = false;
                    break;
                case "$5":
                case "$a1":
                    this.machine_code[startPosition] = false;
                    this.machine_code[startPosition + 1] = false;
                    this.machine_code[startPosition + 2] = true;
                    this.machine_code[startPosition + 3] = false;
                    this.machine_code[startPosition + 4] = true;
                    break;
                case "$6":
                case "$a2":
                    this.machine_code[startPosition] = false;
                    this.machine_code[startPosition + 1] = false;
                    this.machine_code[startPosition + 2] = true;
                    this.machine_code[startPosition + 3] = true;
                    this.machine_code[startPosition + 4] = false;
                    break;
                case "$7":
                case "$a3":
                    this.machine_code[startPosition] = false;
                    this.machine_code[startPosition + 1] = false;
                    this.machine_code[startPosition + 2] = true;
                    this.machine_code[startPosition + 3] = true;
                    this.machine_code[startPosition + 4] = true;
                    break;
                case "$8":
                case "$t0":
                    this.machine_code[startPosition] = false;
                    this.machine_code[startPosition + 1] = true;
                    this.machine_code[startPosition + 2] = false;
                    this.machine_code[startPosition + 3] = false;
                    this.machine_code[startPosition + 4] = false;
                    break;
                case "$9":
                case "$t1":
                    this.machine_code[startPosition] = false;
                    this.machine_code[startPosition + 1] = true;
                    this.machine_code[startPosition + 2] = false;
                    this.machine_code[startPosition + 3] = false;
                    this.machine_code[startPosition + 4] = true;
                    break;
                case "$10":
                case "$t2":
                    this.machine_code[startPosition] = false;
                    this.machine_code[startPosition + 1] = true;
                    this.machine_code[startPosition + 2] = false;
                    this.machine_code[startPosition + 3] = true;
                    this.machine_code[startPosition + 4] = false;
                    break;
                case "$11":
                case "$t3":
                    this.machine_code[startPosition] = false;
                    this.machine_code[startPosition + 1] = true;
                    this.machine_code[startPosition + 2] = false;
                    this.machine_code[startPosition + 3] = true;
                    this.machine_code[startPosition + 4] = true;
                    break;
                case "$12":
                case "$t4":
                    this.machine_code[startPosition] = false;
                    this.machine_code[startPosition + 1] = true;
                    this.machine_code[startPosition + 2] = true;
                    this.machine_code[startPosition + 3] = false;
                    this.machine_code[startPosition + 4] = false;
                    break;
                case "$13":
                case "$t5":
                    this.machine_code[startPosition] = false;
                    this.machine_code[startPosition + 1] = true;
                    this.machine_code[startPosition + 2] = true;
                    this.machine_code[startPosition + 3] = false;
                    this.machine_code[startPosition + 4] = true;
                    break;
                case "$14":
                case "$t6":
                    this.machine_code[startPosition] = false;
                    this.machine_code[startPosition + 1] = true;
                    this.machine_code[startPosition + 2] = true;
                    this.machine_code[startPosition + 3] = true;
                    this.machine_code[startPosition + 4] = false;
                    break;
                case "$15":
                case "$t7":                    
                    this.machine_code[startPosition] = false;
                    this.machine_code[startPosition + 1] = true;
                    this.machine_code[startPosition + 2] = true;
                    this.machine_code[startPosition + 3] = true;
                    this.machine_code[startPosition + 4] = true;
                    break;
                case "$16":
                case "$s0":
                    this.machine_code[startPosition] = true;
                    this.machine_code[startPosition + 1] = false;
                    this.machine_code[startPosition + 2] = false;
                    this.machine_code[startPosition + 3] = false;
                    this.machine_code[startPosition + 4] = false;
                    break;
                case "$17":
                case "$s1":                    
                    this.machine_code[startPosition] = true;
                    this.machine_code[startPosition + 1] = false;
                    this.machine_code[startPosition + 2] = false;
                    this.machine_code[startPosition + 3] = false;
                    this.machine_code[startPosition + 4] = true;
                    break;
                case "$18":
                case "$s2":
                    this.machine_code[startPosition] = true;
                    this.machine_code[startPosition + 1] = false;
                    this.machine_code[startPosition + 2] = false;
                    this.machine_code[startPosition + 3] = true;
                    this.machine_code[startPosition + 4] = false;
                    break;
                case "$19":
                case "$s3":
                    this.machine_code[startPosition] = true;
                    this.machine_code[startPosition + 1] = false;
                    this.machine_code[startPosition + 2] = false;
                    this.machine_code[startPosition + 3] = true;
                    this.machine_code[startPosition + 4] = true;
                    break;
                case "$20":
                case "$s4":
                    this.machine_code[startPosition] = true;
                    this.machine_code[startPosition + 1] = false;
                    this.machine_code[startPosition + 2] = true;
                    this.machine_code[startPosition + 3] = false;
                    this.machine_code[startPosition + 4] = false;
                    break;
                case "$21":
                case "$s5":
                    this.machine_code[startPosition] = true;
                    this.machine_code[startPosition + 1] = false;
                    this.machine_code[startPosition + 2] = true;
                    this.machine_code[startPosition + 3] = false;
                    this.machine_code[startPosition + 4] = true;
                    break;
                case "$22":
                case "$s6":
                    this.machine_code[startPosition] = true;
                    this.machine_code[startPosition + 1] = false;
                    this.machine_code[startPosition + 2] = true;
                    this.machine_code[startPosition + 3] = true;
                    this.machine_code[startPosition + 4] = false;
                    break;
                case "$23":
                case "$s7":
                    this.machine_code[startPosition] = true;
                    this.machine_code[startPosition + 1] = false;
                    this.machine_code[startPosition + 2] = true;
                    this.machine_code[startPosition + 3] = true;
                    this.machine_code[startPosition + 4] = true;
                    break;
                case "$24":
                case "$t8":
                    this.machine_code[startPosition] = true;
                    this.machine_code[startPosition + 1] = true;
                    this.machine_code[startPosition + 2] = false;
                    this.machine_code[startPosition + 3] = false;
                    this.machine_code[startPosition + 4] = false;
                    break;
                case "$25":
                case "$t9":
                    this.machine_code[startPosition] = true;
                    this.machine_code[startPosition + 1] = true;
                    this.machine_code[startPosition + 2] = false;
                    this.machine_code[startPosition + 3] = false;
                    this.machine_code[startPosition + 4] = true;
                    break;
                case "$26":
                case "$k0":
                    this.machine_code[startPosition] = true;
                    this.machine_code[startPosition + 1] = true;
                    this.machine_code[startPosition + 2] = false;
                    this.machine_code[startPosition + 3] = true;
                    this.machine_code[startPosition + 4] = false;
                    break;
                case "$27":
                case "$k1":
                    this.machine_code[startPosition] = true;
                    this.machine_code[startPosition + 1] = true;
                    this.machine_code[startPosition + 2] = false;
                    this.machine_code[startPosition + 3] = true;
                    this.machine_code[startPosition + 4] = true;
                    break;
                case "$28":
                case "$gp":
                    this.machine_code[startPosition] = true;
                    this.machine_code[startPosition + 1] = true;
                    this.machine_code[startPosition + 2] = true;
                    this.machine_code[startPosition + 3] = false;
                    this.machine_code[startPosition + 4] = false;
                    break;
                case "$29":
                case "$sp":
                    this.machine_code[startPosition] = true;
                    this.machine_code[startPosition + 1] = true;
                    this.machine_code[startPosition + 2] = true;
                    this.machine_code[startPosition + 3] = false;
                    this.machine_code[startPosition + 4] = true;
                    break;
                case "$30":
                case "$fp":
                    this.machine_code[startPosition] = true;
                    this.machine_code[startPosition + 1] = true;
                    this.machine_code[startPosition + 2] = true;
                    this.machine_code[startPosition + 3] = true;
                    this.machine_code[startPosition + 4] = false;
                    break;
                case "$31":
                case "$ra":
                    this.machine_code[startPosition] = true;
                    this.machine_code[startPosition + 1] = true;
                    this.machine_code[startPosition + 2] = true;
                    this.machine_code[startPosition + 3] = true;
                    this.machine_code[startPosition + 4] = true;
                    break;
                default:
                    break;
            }
        }

        private void setShamtMachineCode(int startPosition, string shamt)
        {
            string HEXstring = Convert.ToString(int.Parse(shamt), 2).PadLeft(5, '0');
            for (int i = 0; i < 5; i++)
            {
                if (HEXstring[i] == '1')
                {
                    machine_code[i + startPosition] = true;
                }
                else
                {
                    machine_code[i + startPosition] = false;
                }
            }
        }

        private void setImmediateMachineCode(int startPosition, string immediate)
        {
            string immediatestr = Convert.ToString(Int32.Parse(immediate), 2).PadLeft(16, '0');
            for (int i = 0; i < 16; i++)
            {
                if (immediatestr[i] == '0')
                {
                    machine_code[i + startPosition] = false;
                }
                else
                {
                    machine_code[i + startPosition] = true;
                }
            }
        }

        private void setOffsetMachineCode(int startPosition, string offset)
        {
            string offsetstr = Convert.ToString(Int32.Parse(offset), 2).PadLeft(16, '0');
            for (int i = 0; i < 16; i++)
            {
                if (offsetstr[i] == '0')
                {
                    machine_code[i + startPosition] = false;
                }
                else
                {
                    machine_code[i + startPosition] = true;
                }
            }
        }

        private void setAddressMachineCode(string address)
        {
            string addressstr = Convert.ToString(Int32.Parse(address), 2);
            addressstr = addressstr.PadLeft(32,'0');
            addressstr = addressstr.Substring(4, addressstr.Length - 6);
            for (int i = 0; i < 26; i++)
            {
                if (addressstr[i] == '0')
                {
                    machine_code[i + 6] = false;
                }
                else
                {
                    machine_code[i + 6] = true;
                }
            }
        }
        #endregion

        #region OPs
        private void OP_ADD()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.ADD].ToString());
            setRegMachineCode(16, arg1);
            setRegMachineCode(6, arg2);
            setRegMachineCode(11, arg3);
        }

        private void OP_ADDU()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.ADDU].ToString());
            setRegMachineCode(16, arg1);
            setRegMachineCode(6, arg2);
            setRegMachineCode(11, arg3);
        }

        private void OP_SUB()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.SUB].ToString());
            setRegMachineCode(16, arg1);
            setRegMachineCode(6, arg2);
            setRegMachineCode(11, arg3);
        }

        private void OP_SUBU()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.SUBU].ToString());
            setRegMachineCode(16, arg1);
            setRegMachineCode(6, arg2);
            setRegMachineCode(11, arg3);
        }

        private void OP_AND()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.AND].ToString());
            setRegMachineCode(16, arg1);
            setRegMachineCode(6, arg2);
            setRegMachineCode(11, arg3);
        }

        private void OP_OR()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.OR].ToString());
            setRegMachineCode(16, arg1);
            setRegMachineCode(6, arg2);
            setRegMachineCode(11, arg3);
        }

        private void OP_XOR()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.XOR].ToString());
            setRegMachineCode(16, arg1);
            setRegMachineCode(6, arg2);
            setRegMachineCode(11, arg3);
        }

        private void OP_NOR()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.NOR].ToString());
            setRegMachineCode(16, arg1);
            setRegMachineCode(6, arg2);
            setRegMachineCode(11, arg3);
        }

        private void OP_SLT()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.SLT].ToString());
            setRegMachineCode(16, arg1);
            setRegMachineCode(6, arg2);
            setRegMachineCode(11, arg3);
        }

        private void OP_SLTU()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.SLTU].ToString());
            setRegMachineCode(16, arg1);
            setRegMachineCode(6, arg2);
            setRegMachineCode(11, arg3);
        }

        private void OP_SLL()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.SLL].ToString());
            setRegMachineCode(16, arg1);
            setRegMachineCode(11, arg2);
            setShamtMachineCode(21, arg3);
        }

        private void OP_SRL()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.SRL].ToString());
            setRegMachineCode(16, arg1);
            setRegMachineCode(11, arg2);
            setShamtMachineCode(21, arg3);
        }

        private void OP_SRA()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.SRA].ToString());
            setRegMachineCode(16, arg1);
            setRegMachineCode(11, arg2);
            setShamtMachineCode(21, arg3);
        }

        private void OP_SLLV()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.SLLV].ToString());
            setRegMachineCode(16, arg1);
            setRegMachineCode(11, arg2);
            setRegMachineCode(6, arg3);
        }

        private void OP_SRLV()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.SRLV].ToString());
            setRegMachineCode(16, arg1);
            setRegMachineCode(11, arg2);
            setRegMachineCode(6, arg3);
        }

        private void OP_SRAV()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.SRAV].ToString());
            setRegMachineCode(16, arg1);
            setRegMachineCode(11, arg2);
            setRegMachineCode(6, arg3);
        }

        private void OP_JR()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.JR].ToString());
            setRegMachineCode(6, arg1);
        }

        private void OP_JALR()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.JR].ToString());
            setRegMachineCode(6, arg1);
        }

        private void OP_ADDI()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.ADDI].ToString());
            setRegMachineCode(11, arg1);
            setRegMachineCode(6, arg2);
            setImmediateMachineCode(16, arg3);
        }

        private void OP_ADDIU()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.ADDIU].ToString());
            setRegMachineCode(11, arg1);
            setRegMachineCode(6, arg2);
            setImmediateMachineCode(16, arg3);
        }

        private void OP_ANDI()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.ANDI].ToString());
            setRegMachineCode(11, arg1);
            setRegMachineCode(6, arg2);
            setImmediateMachineCode(16, arg3);
        }

        private void OP_ORI()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.ORI].ToString());
            setRegMachineCode(11, arg1);
            setRegMachineCode(6, arg2);
            setImmediateMachineCode(16, arg3);
        }

        private void OP_XORI()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.XORI].ToString());
            setRegMachineCode(11, arg1);
            setRegMachineCode(6, arg2);
            setImmediateMachineCode(16, arg3);
        }

        private void OP_LUI()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.LUI].ToString());
            setRegMachineCode(11, arg1);
            setImmediateMachineCode(16, arg2);
        }

        private void OP_SLTI()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.SLTI].ToString());
            setRegMachineCode(11, arg1);
            setRegMachineCode(6, arg2);
            setImmediateMachineCode(16, arg3);
        }

        private void OP_SLTIU()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.SLTIU].ToString());
            setRegMachineCode(11, arg1);
            setRegMachineCode(6, arg2);
            setImmediateMachineCode(16, arg3);
        }

        private void OP_LW()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.LW].ToString());
            setRegMachineCode(11, arg1);
            setRegMachineCode(6, arg2);
            setOffsetMachineCode(16, arg3);
        }

        private void OP_SW()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.SW].ToString());
            setRegMachineCode(11, arg1);
            setRegMachineCode(6, arg2);
            setOffsetMachineCode(16, arg3);
        }

        private void OP_LB()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.LB].ToString());
            setRegMachineCode(11, arg1);
            setRegMachineCode(6, arg2);
            setOffsetMachineCode(16, arg3);
        }

        private void OP_LBU()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.LBU].ToString());
            setRegMachineCode(11, arg1);
            setRegMachineCode(6, arg2);
            setOffsetMachineCode(16, arg3);
        }

        private void OP_LH()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.LH].ToString());
            setRegMachineCode(11, arg1);
            setRegMachineCode(6, arg2);
            setOffsetMachineCode(16, arg3);
        }

        private void OP_LHU()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.LHU].ToString());
            setRegMachineCode(11, arg1);
            setRegMachineCode(6, arg2);
            setOffsetMachineCode(16, arg3);
        }

        private void OP_SB()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.SB].ToString());
            setRegMachineCode(11, arg1);
            setRegMachineCode(6, arg2);
            setOffsetMachineCode(16, arg3);
        }

        private void OP_SH()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.SH].ToString());
            setRegMachineCode(11, arg1);
            setRegMachineCode(6, arg2);
            setOffsetMachineCode(16, arg3);
        }

        private void OP_BEQ()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.BEQ].ToString());
            setRegMachineCode(6, arg1);
            setRegMachineCode(11, arg2);
            setOffsetMachineCode(16, arg3);
        }

        private void OP_BNE()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.BNE].ToString());
            setRegMachineCode(6, arg1);
            setRegMachineCode(11, arg2);
            setOffsetMachineCode(16, arg3);
        }

        private void OP_BGEZ()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.BGEZ].ToString());
            setRegMachineCode(6, arg1);
            setRegMachineCode(11, arg2);
            setOffsetMachineCode(16, arg3);
        }

        private void OP_BGEZAL()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.BGEZAL].ToString());
            setRegMachineCode(6, arg1);
            setOffsetMachineCode(16, arg2);
        }

        private void OP_BGTZ()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.BGTZ].ToString());
            setRegMachineCode(6, arg1);
            setOffsetMachineCode(16, arg2);
        }

        private void OP_BLEZ()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.BLEZ].ToString());
            setRegMachineCode(6, arg1);
            setOffsetMachineCode(16, arg2);
        }

        private void OP_BLTZ()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.BLTZ].ToString());
            setRegMachineCode(6, arg1);
            setOffsetMachineCode(16, arg2);
        }

        private void OP_BLTZAL()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.BLTZAL].ToString());
            setRegMachineCode(6, arg1);
            setOffsetMachineCode(16, arg2);
        }

        private void OP_J()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.J].ToString());
            setAddressMachineCode(arg1);
        }

        private void OP_JAL()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.JAL].ToString());
            setAddressMachineCode(arg1);

        }

        private void OP_SUBI()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.SUBI].ToString());
            setAddressMachineCode(arg1);

        }

        private void OP_NOP()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.NOP].ToString());
            setAddressMachineCode(arg1);

        }

        private void OP_LI()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.LI].ToString());
            setAddressMachineCode(arg1);

        }

        private void OP_LA()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.LA].ToString());
            setAddressMachineCode(arg1);

        }

        private void OP_SYSCALL()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.SYSCALL].ToString());
            setAddressMachineCode(arg1);

        }
        #endregion
    }
}