using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MIPS246.Core.DataStructure;
using MIPS246.Core.Compiler.AstStructure;

namespace Compiler
{
    public static class AstToFourExp
    {
        /// <summary>
        /// 遍历Ast树，生成四元式列表
        /// </summary>
        /// <param name="ast">Ast树</param>
        /// <param name="varTable">变量表</param>
        /// <returns></returns>
        public static List<FourExp> Translate(Ast ast, VarTable varTable)
        {
            List<FourExp> fourExpList = new List<FourExp>();
            LabelStack labelStack = new LabelStack();
            foreach (Statement s in ast.Statements)
            {
                s.Translate(varTable, labelStack, fourExpList);
            }
            return fourExpList;
        }
    }
}
