using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIPS246.Core.Compiler
{
    public enum FourExpOperation
    {
        
        label, //标签类型
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
        and,  //与
        or,   //或
        xor,  //异或
        nor,  //非或
        neg,   //取反
        not  //非
    }

    public class FourExp
    {
        #region Private Fields
        private int index;
        private FourExpOperation op;
        private string arg1;
        private string arg2;
        private string targetLabel;
        private string result;
        private string labelName;
        //private int addr;
        #endregion

        #region Public Fields
        public int Index
        {
            get { return index; }
            set { index = value; }
        }

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

        public string TargetLabel
        {
            get { return targetLabel; }
            set { targetLabel = value; }
        }

        public string Result
        {
            get { return result; }
            set { result = value; }
        }

        public string LabelName
        {
            get { return labelName; }
            set { labelName = value; }
        }

        //public int Addr
        //{
        //    get { return addr; }
        //    set { addr = value; }
        //}
        #endregion

        #region Constructor
        public FourExp(FourExpOperation op, string labelName) //标签
        {
            this.Op = op;
            this.Arg1 = "";
            this.Arg2 = "";
            this.TargetLabel = "";
            this.Result = "";
            this.LabelName = labelName;
            //this.Addr = 0x0000;
        }
        
        public FourExp(FourExpOperation op, string arg1, string arg2, string targetLabel) //跳转
        {
            this.Op = op;
            this.Arg1 = arg1;
            this.Arg2 = arg2;
            this.TargetLabel = targetLabel;
            this.Result = "";
            this.LabelName = "";
            //this.Addr = 0x0000;
        }

        public FourExp(FourExpOperation op, string source, string target) //赋值、取反
        {
            this.Op = op;
            this.Arg1 = source;
            this.Arg2 = "";
            this.TargetLabel = "";
            this.Result = target;
            this.LabelName = "";
            //this.Addr = 0x0000;
        }

        public FourExp(FourExpOperation op, string arg1, string arg2, string result) //四则元算、逻辑运算
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
                this.TargetLabel = "";
                this.LabelName = "";
            }
        }
        #endregion

        #region Public Method
        public override string ToString()
        {
            StringBuilder strTemp = new StringBuilder();
            if (this.Op == FourExpOperation.label) //标签
            {
                strTemp.Append(this.LabelName)
                    .Append(":");
            }
            else
            {
                strTemp.Append("( ")
                        .Append(this.op)
                        .Append(", ")
                        .Append(this.arg1)
                        .Append(", ")
                        .Append(this.arg2)
                        .Append(", ");
                if (this.targetLabel == "")
                {
                    strTemp.Append(this.result)
                        .Append(" )");
                }
                else
                {
                    strTemp.Append(this.targetLabel)
                        .Append(" )");
                }
            }
            return strTemp.ToString();
        }
        #endregion
    }
}
