using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private Mnemonic mnemonic;
        private bool[] machine_code;
        private string arg1, arg2, arg3;

        Instruction(Mnemonic mnemonic, bool[] machine_code, string arg1, string arg2, string arg3)
        {
            this.mnemonic = mnemonic;
            this.machine_code = machine_code;
            this.arg1 = arg1;
            this.arg2 = arg2;
            this.arg3 = arg3;
        }
    }
}
