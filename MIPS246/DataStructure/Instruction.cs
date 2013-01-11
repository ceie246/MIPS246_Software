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
        private static Hashtable Assemblertable;
        private static Hashtable Disassembler;
        #endregion

        #region Constructors
        static Instruction()
        {
            Assemblertable = new Hashtable();
            Assemblertable.Add(Mnemonic.ADD,InitBoolArray("hh"));
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
        #endregion
    }
}
