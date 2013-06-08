using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIPS246.Core.DataStructure
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
        private FourExpOperation op;
        private string arg1;
        private string arg2;
        private string arg3;
        //private int addr;
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

        public string TargetLabel //跳转的目标标签
        {
            get { return arg3; }
            set { arg3 = value; }
        }

        public string Result //数学和逻辑运算的结果
        {
            get { return arg3; }
            set { arg3 = value; }
        }

        public string LabelName //标签的标签名
        {
            get { return arg3; }
            set { arg3 = value; }
        }

        #endregion

        #region Constructor
        public FourExp(FourExpOperation op, string arg1, string arg2, string arg3)
        {
            this.op = op;
            this.arg1 = arg1;
            this.arg2 = arg2;
            this.arg3 = arg3;
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
                    .Append(", ")
                    .Append(this.arg3)
                    .Append(" )");
            }
            return strTemp.ToString();
        }
        #endregion
    }
}
