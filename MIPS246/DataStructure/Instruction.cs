using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace MIPS246.Core.DataStructure
{
    enum Mnemonic
    {
        ADD,ADDU,SUB,SUBU,AND,OR,XOR,NOR,SLT,SLTU,SLL,SRL,SRA,SLLV,SRLV,SRAV,JR,JALR,
        ADDI,ADDIU,ANDI,ORI,XORI,LUI,SLTI,SLTIU,LW,SW,LB,LBU,LH,LHU,SB,SH,BEQ,BNE,BGEZ,
        BGEZAL,BGTZ,BLEZ,BLTZ,BLTZAL,J,JAL,SUBI,MOVE,NOP,LI,LA,SYSCALL
    }

    public class Instruction
    {
        #region Fields
        private Mnemonic mnemonic;
        private bool[] machine_code;
        private string arg1, arg2, arg3;
        private static Hashtable AssemblerTable;
        private static Hashtable DisassemblerTable;
        #endregion

        #region Constructors
        static Instruction()
        {
            InitAssemblerTable();
        }

        Instruction(Mnemonic mnemonic, string arg1, string arg2, string arg3)
        {
            this.mnemonic = mnemonic;
            this.arg1 = arg1;
            this.arg2 = arg2;
            this.arg3 = arg3;
        }

        Instruction(bool[] machine_code)
        {
            this.machine_code = machine_code;
        }
        #endregion

        #region Properties
        #endregion

        #region Public Methods
        public bool Validate()
        {
            if (this.mnemonic == null)
            {
                return true;
            }
            else
            {
                
                return true;
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
            AssemblerTable.Add(Mnemonic.ADD, InitBoolArray("00000000000000000000000000100000"));
            AssemblerTable.Add(Mnemonic.ADDU, InitBoolArray("00000000000000000000000000100001"));
            AssemblerTable.Add(Mnemonic.SUB, InitBoolArray("00000000000000000000000000100010"));
            AssemblerTable.Add(Mnemonic.SUBU, InitBoolArray("00000000000000000000000000100011"));
            AssemblerTable.Add(Mnemonic.AND, InitBoolArray("00000000000000000000000000100100"));
            AssemblerTable.Add(Mnemonic.OR, InitBoolArray("00000000000000000000000000100101"));
            AssemblerTable.Add(Mnemonic.XOR, InitBoolArray("00000000000000000000000000100110"));
            AssemblerTable.Add(Mnemonic.NOR, InitBoolArray("00000000000000000000000000100111"));
            AssemblerTable.Add(Mnemonic.SLT, InitBoolArray("00000000000000000000000000101010"));
            AssemblerTable.Add(Mnemonic.SLTU, InitBoolArray("00000000000000000000000000101011"));
            AssemblerTable.Add(Mnemonic.SLL, InitBoolArray("00000000000000000000000000000000"));
            AssemblerTable.Add(Mnemonic.SRL, InitBoolArray("00000000000000000000000000000010"));
            AssemblerTable.Add(Mnemonic.SRA, InitBoolArray("00000000000000000000000000000011"));
            AssemblerTable.Add(Mnemonic.SLLV, InitBoolArray("00000000000000000000000000000100"));
            AssemblerTable.Add(Mnemonic.SRLV, InitBoolArray("00000000000000000000000000000110"));
            AssemblerTable.Add(Mnemonic.SRAV, InitBoolArray("00000000000000000000000000000111"));
            AssemblerTable.Add(Mnemonic.JR, InitBoolArray("00000000000000000000000000001000"));
            AssemblerTable.Add(Mnemonic.JALR, InitBoolArray("00000000000000001111100000001001"));

            AssemblerTable.Add(Mnemonic.ADDI, InitBoolArray("00100000000000000000000000000000"));
            AssemblerTable.Add(Mnemonic.ADDIU, InitBoolArray("00100100000000000000000000000000"));
            AssemblerTable.Add(Mnemonic.ANDI, InitBoolArray("00110000000000000000000000000000"));
            AssemblerTable.Add(Mnemonic.ORI, InitBoolArray("00110100000000000000000000000000"));
            AssemblerTable.Add(Mnemonic.XORI, InitBoolArray("00111000000000000000000000000000"));
            AssemblerTable.Add(Mnemonic.LUI, InitBoolArray("00111100000000000000000000000000"));
            AssemblerTable.Add(Mnemonic.SLTI, InitBoolArray("00101000000000000000000000000000"));
            AssemblerTable.Add(Mnemonic.SLTIU, InitBoolArray("00101100000000000000000000000000"));
            AssemblerTable.Add(Mnemonic.LW, InitBoolArray("10001100000000000000000000000000"));
            AssemblerTable.Add(Mnemonic.SW, InitBoolArray("10101100000000000000000000000000"));
            AssemblerTable.Add(Mnemonic.LB, InitBoolArray("10000000000000000000000000000000"));
            AssemblerTable.Add(Mnemonic.LBU, InitBoolArray("10010000000000000000000000000000"));
            AssemblerTable.Add(Mnemonic.LH, InitBoolArray("10000100000000000000000000000000"));
            AssemblerTable.Add(Mnemonic.LHU, InitBoolArray("10010100000000000000000000000000"));
            AssemblerTable.Add(Mnemonic.SB, InitBoolArray("10100000000000000000000000000000"));
            AssemblerTable.Add(Mnemonic.SH, InitBoolArray("10100100000000000000000000000000"));
            AssemblerTable.Add(Mnemonic.BEQ, InitBoolArray("00010000000000000000000000000000"));
            AssemblerTable.Add(Mnemonic.BNE, InitBoolArray("00010100000000000000000000000000"));
            AssemblerTable.Add(Mnemonic.BGEZ, InitBoolArray("0000010000000001"));
            AssemblerTable.Add(Mnemonic.BGEZAL, InitBoolArray("0000010000010001"));
            AssemblerTable.Add(Mnemonic.BGTZ, InitBoolArray("000111000000000"));
            AssemblerTable.Add(Mnemonic.BLEZ, InitBoolArray("0001100000000000"));
            AssemblerTable.Add(Mnemonic.BLTZ, InitBoolArray("0000010000000000"));
            AssemblerTable.Add(Mnemonic.BLTZAL, InitBoolArray("0000010000010000"));

            AssemblerTable.Add(Mnemonic.J, InitBoolArray("00001000000000000000000000000000"));
            AssemblerTable.Add(Mnemonic.JAL, InitBoolArray("00001100000000000000000000000000"));

            //AssemblerTable.Add(Mnemonic.SUBI, InitBoolArray(""));
            //AssemblerTable.Add(Mnemonic.MOVE, InitBoolArray(""));
            //AssemblerTable.Add(Mnemonic.NOP, InitBoolArray(""));
            //AssemblerTable.Add(Mnemonic.LI, InitBoolArray(""));
            //AssemblerTable.Add(Mnemonic.LA, InitBoolArray(""));
            //AssemblerTable.Add(Mnemonic.SYSCALL, InitBoolArray(""));
        }

        private void ToMachineCode()
        {
            switch (this.mnemonic)
            {
                case Mnemonic.ADD:
                    break;
                case Mnemonic.ADDU:
                    break;
                case Mnemonic.SUB:
                    break;
                case Mnemonic.SUBU:
                    break;
                case Mnemonic.AND:
                    break;
                case Mnemonic.OR:
                    break;
                case Mnemonic.XOR:
                    break;
                case Mnemonic.NOR:
                    break;
                case Mnemonic.SLT:
                    break;
                case Mnemonic.SLTU:
                    break;
                case Mnemonic.SLL:
                    break;
                case Mnemonic.SRL:
                    break;
                case Mnemonic.SRA:
                    break;
                case Mnemonic.SLLV:
                    break;
                case Mnemonic.SRLV:
                    break;
                case Mnemonic.SRAV:
                    break;
                case Mnemonic.JR:
                    break;
                case Mnemonic.JALR:
                    break;
                case Mnemonic.ADDI:
                    break;
                case Mnemonic.ADDIU:
                    break;
                case Mnemonic.ANDI:
                    break;
                case Mnemonic.ORI:
                    break;
                case Mnemonic.XORI:
                    break;
                case Mnemonic.LUI:
                    break;
                case Mnemonic.SLTI:
                    break;
                case Mnemonic.SLTIU:
                    break;
                case Mnemonic.LW:
                    break;
                case Mnemonic.SW:
                    break;
                case Mnemonic.LB:
                    break;
                case Mnemonic.LBU:
                    break;
                case Mnemonic.LH:
                    break;
                case Mnemonic.LHU:
                    break;
                case Mnemonic.SB:
                    break;
                case Mnemonic.SH:
                    break;
                case Mnemonic.BEQ:
                    break;
                case Mnemonic.BNE:
                    break;
                case Mnemonic.BGEZ:
                    break;
                case Mnemonic.BGEZAL:
                    break;
                case Mnemonic.BGTZ:
                    break;
                case Mnemonic.BLEZ:
                    break;
                case Mnemonic.BLTZAL:
                    break;
                case Mnemonic.J:
                    break;
                case Mnemonic.JAL:
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

        #endregion
    }
}
