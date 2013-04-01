using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIPS246.Core.Compiler
{
    public enum FourExpOperation
    {
        jmp,  //无条件跳转
        je,   //条件跳转：=
        jne,  //条件跳转：！=
        jg,   //条件跳转：>
        jge,  //条件跳转：>=
        jl,   //条件跳转：<
        jle,  //条件跳转：<=
        mov,  //赋值
        add,  //加
        sub,  //减
        mul,  //乘
        div,  //除
        neg,   //取反
        and,  //与
        or,   //或
        not  //非
    }

    public class FourExp
    {
        #region Private Fields
        //private int index;
        private FourExpOperation op;
        private string arg1;
        private string arg2;
        private int nextFourExp;
        private string result;
        #endregion

        #region Public Fields
        public FourExpOperation Op
        {
            get { return op; }
            set { op = value; }
        }

        public string Arg1
        {
            get { return arg1; }
            set { arg1 = value; }
        }

        public string Arg2
        {
            get { return arg2; }
            set { arg2 = value; }
        }

        public int NextFourExp
        {
            get { return nextFourExp; }
            set { nextFourExp = value; }
        }

        public string Result
        {
            get { return result; }
            set { result = value; }
        }
        #endregion

        #region Constructor
        public FourExp(FourExpOperation op, string arg1, string arg2, int nextFourExp) //跳转
        {
            this.Op = op;
            this.Arg1 = arg1;
            this.Arg2 = arg2;
            this.NextFourExp = nextFourExp;
            this.Result = "";
        }

        public FourExp(FourExpOperation op, string arg1, string arg2, string result) //赋值、取反、四则元算、逻辑运算
        {
            if (op < FourExpOperation.mov)
            {
                //错误处理
            }
            else
            {
                this.Op = op;
                this.Arg1 = arg1;
                this.Arg2 = arg2;
                this.Result = result;
                this.NextFourExp = -1;
            }
        }
        #endregion

        #region Public Method
        public override string ToString()
        {
            StringBuilder strTemp = new StringBuilder();
            strTemp.Append("( ")
                    .Append(this.op)
                    .Append(", ")
                    .Append(this.arg1)
                    .Append(", ")
                    .Append(this.arg2)
                    .Append(", ");
            if (this.nextFourExp == -1)
            {
                strTemp.Append(this.result)
                    .Append(" )");
            }
            else
            {
                strTemp.Append(this.nextFourExp)
                    .Append(" )");
            }
            return strTemp.ToString();
        }
        #endregion
    }
}
