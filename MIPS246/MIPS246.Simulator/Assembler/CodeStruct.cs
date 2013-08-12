using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MipsSimulator.Assembler
{
    // 结构体类型，指令结构
    public struct Code
    {
        public Code(CodeType codeType, object[] args,string codeStr,string machineCode)
        {
            this.codeType = codeType;
            this.args = args;
            this.codeStr = codeStr;
            this.machineCode = machineCode;
            this.index =0;
            this.address = 0;
        }
        public CodeType codeType;
        public object[] args;
        public string codeStr;
        public string machineCode;
        public Int32 index;
        public Int32 address;

        public Int32 Index
        {
            get { return index; }
            set { index = value; }
        }
    }

    // 枚举类型，支持的指令类型以及错误&不支持
    public enum CodeType
    {
       /**************RType************************/
        ADD,ADDU,SUB,SUBU,
        AND,OR,XOR,NOR,
        SLT,SLTU, 
        SLL,SRL,SRA,
        SLLV,SRLV,SRAV,
        JR,

        /**************IType************************/
        ADDI,ADDIU,
        ANDI, ORI,XORI,
        LUI,
        LW,SW,
        BEQ,BNE,
        SLTI,SLTIU,

        /**************JType************************/
        J,JAL,
        NOP,OVER,
        ERR, UNSUPPOTED
    };
}
