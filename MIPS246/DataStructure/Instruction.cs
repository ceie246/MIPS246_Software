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

            AssemblerTable.Add(Mnemonic.ADDI, InitBoolArray("001000"));
            AssemblerTable.Add(Mnemonic.ADDIU, InitBoolArray("001001"));
            AssemblerTable.Add(Mnemonic.ANDI, InitBoolArray("001100"));
            AssemblerTable.Add(Mnemonic.ORI, InitBoolArray("001101"));
            AssemblerTable.Add(Mnemonic.XORI, InitBoolArray("001110"));
            AssemblerTable.Add(Mnemonic.LUI, InitBoolArray("00111100000"));
            AssemblerTable.Add(Mnemonic.SLTI, InitBoolArray("001010"));
            AssemblerTable.Add(Mnemonic.SLTIU, InitBoolArray("001011"));
            AssemblerTable.Add(Mnemonic.LW, InitBoolArray("100011"));
            AssemblerTable.Add(Mnemonic.SW, InitBoolArray("101011"));
            AssemblerTable.Add(Mnemonic.LB, InitBoolArray("100000"));
            AssemblerTable.Add(Mnemonic.LBU, InitBoolArray("100100"));
            AssemblerTable.Add(Mnemonic.LH, InitBoolArray("100001"));
            AssemblerTable.Add(Mnemonic.LHU, InitBoolArray("100101"));
            AssemblerTable.Add(Mnemonic.SB, InitBoolArray("101000"));
            AssemblerTable.Add(Mnemonic.SH, InitBoolArray("101001"));
            AssemblerTable.Add(Mnemonic.BEQ, InitBoolArray("000100"));
            AssemblerTable.Add(Mnemonic.BNE, InitBoolArray("000101"));
            AssemblerTable.Add(Mnemonic.BGEZ, InitBoolArray("0000010000000001"));
            AssemblerTable.Add(Mnemonic.BGEZAL, InitBoolArray("0000010000010001"));
            AssemblerTable.Add(Mnemonic.BGTZ, InitBoolArray("000111000000000"));
            AssemblerTable.Add(Mnemonic.BLEZ, InitBoolArray("0001100000000000"));
            AssemblerTable.Add(Mnemonic.BLTZ, InitBoolArray("0000010000000000"));
            AssemblerTable.Add(Mnemonic.BLTZAL, InitBoolArray("0000010000010000"));

            AssemblerTable.Add(Mnemonic.J, InitBoolArray("000010"));
            AssemblerTable.Add(Mnemonic.JAL, InitBoolArray("000011"));

            //AssemblerTable.Add(Mnemonic.SUBI, InitBoolArray(""));
            //AssemblerTable.Add(Mnemonic.MOVE, InitBoolArray(""));
            //AssemblerTable.Add(Mnemonic.NOP, InitBoolArray(""));
            //AssemblerTable.Add(Mnemonic.LI, InitBoolArray(""));
            //AssemblerTable.Add(Mnemonic.LA, InitBoolArray(""));
            //AssemblerTable.Add(Mnemonic.SYSCALL, InitBoolArray(""));
        }
        #endregion
    }
}
