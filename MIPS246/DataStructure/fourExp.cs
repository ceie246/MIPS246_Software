using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIPS246.Core.DataStructure
{
    enum quaternion_action
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
        public static int id = 0;     //四元式的编号，静态变量
        private quaternion_action action = quaternion_action.jmp;
        private string left = "";
        private string right = "";
        private int next = -1;
        private string result = "";
        #endregion

        #region constructor
        /// <summary>
        /// generate a quaternion
        /// </summary>
        /// <param name="act">action</param>
        /// <param name="l">left</param>
        /// <param name="r">right</param>
        /// <param name="n">next</param>
        FourExp(quaternion_action act, string l, string r, int n) //跳转
        {
            this.action = act;
            this.left = l;
            this.right = r;
            this.next = n;
        }
        /// <summary>
        /// generate a quaternion
        /// </summary>
        /// <param name="act">action(should not be jump type)</param>
        /// <param name="l">left value</param>
        /// <param name="r">right value</param>
        /// <param name="res">result</param>
        FourExp(quaternion_action act, string l, string r, string res) //赋值、取反、四则元算、逻辑运算
        {
            if (act < quaternion_action.mov)
            {
                //错误处理
            }
            this.action = act;
            this.left = l;
            this.right = r;
            this.result = res;
        }
        #endregion

        #region public method
        public override string ToString()
        {
            StringBuilder sb_result = new StringBuilder(id.ToString("D3")); //三位十进制数
            //StringBuilder sb_result = new StringBuilder();
            sb_result.Append(": (");
            sb_result.Append(this.action.ToString("G"));
            sb_result.Append(", ");
            sb_result.Append(this.left);
            sb_result.Append(", ");
            sb_result.Append(this.right);
            sb_result.Append(", ");
            if (this.next != -1)
            {
                sb_result.Append(this.next);
            }
            else
            {
                sb_result.Append(this.result);
            }
            sb_result.Append(")");
            return sb_result.ToString();
        }
        #endregion
    }
}
