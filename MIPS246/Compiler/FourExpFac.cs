using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIPS246.Core.Compiler
{
    class FourExpFac
    {
        public static FourExp GenLabel(string labelName)
        {
            FourExp f = new FourExp(FourExpOperation.label, "", "", labelName);
            return f;
        }

        public static FourExp GenJmp(string targetLabel)
        {
            FourExp f = new FourExp(FourExpOperation.jmp, "", "", targetLabel);
            return f;
        }

        public static FourExp GenJe(string arg1, string arg2, string targetLabel)
        {
            FourExp f = new FourExp(FourExpOperation.je, arg1, arg2, targetLabel);
            return f;
        }

        public static FourExp GenJne(string arg1, string arg2, string targetLabel)
        {
            FourExp f = new FourExp(FourExpOperation.jne, arg1, arg2, targetLabel);
            return f;
        }

        public static FourExp GenJg(string arg1, string arg2, string targetLabel)
        {
            FourExp f = new FourExp(FourExpOperation.jg, arg1, arg2, targetLabel);
            return f;
        }

        public static FourExp GenJge(string arg1, string arg2, string targetLabel)
        {
            FourExp f = new FourExp(FourExpOperation.jge, arg1, arg2, targetLabel);
            return f;
        }

        public static FourExp GenJl(string arg1, string arg2, string targetLabel)
        {
            FourExp f = new FourExp(FourExpOperation.jl, arg1, arg2, targetLabel);
            return f;
        }

        public static FourExp GenJle(string arg1, string arg2, string targetLabel)
        {
            FourExp f = new FourExp(FourExpOperation.jle, arg1, arg2, targetLabel);
            return f;
        }

        public static FourExp GenMov(string arg1, string Result)
        {
            FourExp f = new FourExp(FourExpOperation.mov, arg1, "", Result);
            return f;
        }

        public static FourExp GenAdd(string arg1, string arg2, string result)
        {
            FourExp f = new FourExp(FourExpOperation.add, arg1, arg2, result);
            return f;
        }

        public static FourExp GenSub(string arg1, string arg2, string result)
        {
            FourExp f = new FourExp(FourExpOperation.sub, arg1, arg2, result);
            return f;
        }

        public static FourExp GenMul(string arg1, string arg2, string result)
        {
            FourExp f = new FourExp(FourExpOperation.mul, arg1, arg2, result);
            return f;
        }

        public static FourExp GenDiv(string arg1, string arg2, string result)
        {
            FourExp f = new FourExp(FourExpOperation.div, arg1, arg2, result);
            return f;
        }

        public static FourExp GenAnd(string arg1, string arg2, string result)
        {
            FourExp f = new FourExp(FourExpOperation.and, arg1, arg2, result);
            return f;
        }

        public static FourExp GenOr(string arg1, string arg2, string result)
        {
            FourExp f = new FourExp(FourExpOperation.or, arg1, arg2, result);
            return f;
        }

        public static FourExp GenXor(string arg1, string arg2, string result)
        {
            FourExp f = new FourExp(FourExpOperation.xor, arg1, arg2, result);
            return f;
        }

        public static FourExp GenNor(string arg1, string arg2, string result)
        {
            FourExp f = new FourExp(FourExpOperation.nor, arg1, arg2, result);
            return f;
        }

        public static FourExp GenNeg(string arg1, string result)
        {
            FourExp f = new FourExp(FourExpOperation.neg, arg1, "", result);
            return f;
        }

        public static FourExp GenNot(string arg1, string result)
        {
            FourExp f = new FourExp(FourExpOperation.not, arg1, "", result);
            return f;
        }

    }
}
