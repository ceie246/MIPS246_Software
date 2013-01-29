﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIPS246.Core.DataStructure
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
        #region fields
        private FourExpOperation op = FourExpOperation.jmp;
        private string arg1 = "";
        private string arg2 = "";
        private int nextFourExp = -1;
        private string result = "";
        #endregion

        #region Constructor
        public FourExp(FourExpOperation op, string arg1, string arg2, int nextFourExp) //跳转
        {
            this.op = op;
            this.arg1 = arg1;
            this.arg2 = arg2;
            this.nextFourExp = nextFourExp;
        }

        public FourExp(FourExpOperation op, string arg1, string arg2, string result) //赋值、取反、四则元算、逻辑运算
        {
            if (op < FourExpOperation.mov)
            {
                //错误处理
            }
            else
            {
                this.op = op;
                this.arg1 = arg1;
                this.arg2 = arg2;
                this.result = result;
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
