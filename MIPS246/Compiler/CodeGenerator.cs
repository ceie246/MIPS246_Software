using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MIPS246.Core.DataStructure;

namespace Compiler
{
    public class CodeGenerator
    {
        #region Fields
        #endregion

        #region Constructor
        public CodeGenerator(List<FourExp> fourExpList, List<Instruction> insList)
        {

        }
        #endregion
        #region Public Method
        public static void Generate(ref List<FourExp> fourExpList, ref List<Instruction> insList, ref Dictionary<int, String> labelDic)
        {
            int labelNo = 0;
            foreach (FourExp f in fourExpList)
            {
                genLabel(f, ref labelNo, ref labelDic);
                convert(f);
                optimize();
            }
        }

        private static string getRegOrImmi() 
        {
            return null;
        }

        private static void genLabel(FourExp f, ref int labelNo, ref Dictionary<int, String> labelDic)
        {
            int fourExpNo = f.NextFourExp;
            if (fourExpNo != -1)
            {
                labelDic.Add(fourExpNo, "L" + labelNo.ToString("D3"));
                labelNo++;
            }
        }
     
        private static void convert(FourExp f)
        {
            switch (f.Op)
            {
                case FourExpOperation.jmp:  //无条件跳转
                    
                    break;
                case FourExpOperation.je:   //条件跳转：=

                    break;
                case FourExpOperation.jne:  //条件跳转：！=

                    break;
                case FourExpOperation.jg:   //条件跳转：>

                    break;
                case FourExpOperation.jge:  //条件跳转：>=

                    break;
                case FourExpOperation.jl:   //条件跳转：<

                    break;
                case FourExpOperation.jle:  //条件跳转：<=


                    break;
                case FourExpOperation.mov:  //赋值

                    break;
                case FourExpOperation.add:  //加

                    break;
                case FourExpOperation.sub:  //减

                    break;
                case FourExpOperation.mul:  //乘

                    break;
                case FourExpOperation.div:  //除

                    break;
                case FourExpOperation.neg:  //取反

                    break;
                case FourExpOperation.and:  //与

                    break;
                case FourExpOperation.or:   //或

                    break;
                case FourExpOperation.not:  //非

                    break;
                default:
                    //错误处理
                    break;
            }
        }

        private static void optimize()
        { 
        
        }
        #endregion
    }
}
