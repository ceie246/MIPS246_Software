using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MIPS246.Core.DataStructure;

namespace MIPS246.Core.Compiler
{
    public class CodeGenerator
    {
        #region Fields
        private static string[] registers = { "T0", "T1", "T2", "T3", "T4", "T5", "T6", "T7", "T7", "T9" };
        private RegContent regUseTable;
        #endregion

        #region Constructor
        public CodeGenerator()
        {
            regUseTable = new RegContent(registers.ToList());
        }
        #endregion

        #region Public Method
        public void Generate(List<FourExp> fourExpList, VarTable varTable, List<string> cmdList, Dictionary<int, String> labelDic)
        {
            //为变量分配内存,并对符号的后续引用信息域和活跃信息域进行初始化
            List<string> varNameList = varTable.GetNames();
            initVarTable(varTable, fourExpList, varNameList);
            
            //生成数据段
            genDataIns(varNameList, varTable, cmdList);

            //遍历四元式表，生成代码段
            int labelNo = 0;
            int count = 0;
            foreach (FourExp f in fourExpList)
            {
                foreach (string varName in varNameList)
                {
                    //从符号表的后续引用信息域和活跃域中去除无用信息
                    if (varTable.GetPeekRefeInfo(varName) == count)
                    {
                        varTable.PopRefeInfo(varName);
                        varTable.PopActInfo(varName);
                    }
                }
                genLabel(f, ref labelNo, ref labelDic);
                convert(f);
                optimize();
            }
        }

        //初始化变量表
        private void initVarTable(VarTable varTable, List<FourExp> fourExpList, List<string> varNameList)
        {
            
            int address = 0x0000;
            foreach (string varName in varNameList)
            {
                //初始化变量表中后续引用信息域和活跃信息域
                varTable.SetAddr(varName, address);
                address += 4;
                varTable.ClearRefeInfo(varName);
                varTable.ClearActInfo(varName);
                varTable.PushActInfo(varName, false);
                varTable.PushRefeInfo(varName, -1);
            }
            //扫描四元式表，在变量表中填入相关信息
            int count = fourExpList.Count;
            int length = count;
            for (int i = length; i != 0; i--)
            {
                string A = fourExpList[i].Result;
                string B = fourExpList[i].Arg1;
                string C = fourExpList[i].Arg2;
                if (A != "")
                {
                    varTable.PushRefeInfo(A, -1);
                    varTable.PushActInfo(A, false);
                }
                if (B != "")
                {
                    varTable.PushRefeInfo(B, count);
                    varTable.PushActInfo(B, true);
                }
                if (C != "")
                {
                    varTable.PushRefeInfo(C, count);
                    varTable.PushActInfo(C, true);
                }
                count--;
            }
        }

        private string getReg(FourExp f, VarTable varTable, List<string> cmdList)
        {
            //返回未占用寄存器
            foreach (string regName in registers)
            {
                if (regUseTable.GetContent(regName) == null)
                {
                    return regName;
                }
            }
            //随机返回一个已占用的寄存器
            Random r = new Random();
            while (true)
            {
                int i = r.Next(registers.Length);
                string reg = registers[i];
                List<string> varList = new List<string>() { f.Arg1, f.Arg2, f.Result };
                if (!regUseTable.Contains(reg, varList))
                {
                    //调整变量表和寄存器表中的相关域
                    doAdjust(reg, varTable, cmdList);
                    return reg;
                }
            }
        }

        //调整变量表和寄存器表中的相关域
        private void doAdjust(string regName, VarTable varTable, List<string> cmdList)
        {
            foreach (string varName in regUseTable.GetContent(regName))
            {
                cmdList.Add("sw " + regName + ", " + varName);
                varTable.SetAddrInfo(varName, "");
            }
            regUseTable.Clear(regName);
        }

        //生成标签
        private void genLabel(FourExp f, ref int labelNo, ref Dictionary<int, String> labelDic)
        {
            int fourExpNo = f.NextFourExp;
            if (fourExpNo != -1)
            {
                labelDic.Add(fourExpNo, "L" + labelNo.ToString("D3"));
                labelNo++;
            }
        }
     
        //生成指令段
        private void convert(FourExp f)
        {
            if (f.Op <= FourExpOperation.jle)
            { 
                
            }
            else if (f.Op == FourExpOperation.mov)
            { 
            
            }
            else if (f.Op <= FourExpOperation.or)
            { 
            
            }
            else if (f.Op == FourExpOperation.not)
            {

            }
            else 
            { 
                //错误处理
            }
        }

        //生成数据段
        private void genDataIns(List<string> varNameList, VarTable varTable, List<string> cmdList)
        {
            cmdList.Add(".data");
            foreach (string varName in varNameList)
            {
                cmdList.Add(varName + ": .word " + varTable.GetValue(varName));
            }
            cmdList.Add(".text");
        }

        //优化
        private void optimize()
        { 
        
        }
        #endregion
    }
}
