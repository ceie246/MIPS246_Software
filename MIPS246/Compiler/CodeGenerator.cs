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
        //$T0专门用来保存分支指令的结果
        private static string[] registers = { "$T1", "$T2", "$T3", "$T4", "$T5", "$T6", "$T7", "$T7", "$T9" };
        private RegContent regUseTable;
        #endregion

        #region Constructor
        public CodeGenerator()
        {
            regUseTable = new RegContent(registers.ToList());
        }
        #endregion

        #region Public Method
        public void Generate(List<FourExp> fourExpList, VarTable varTable, List<AssemblerIns> cmdList)
        {
            //为变量分配内存,并对符号的后续引用信息域和活跃信息域进行初始化
            List<string> varNameList = varTable.GetNames();
            initVarTable(varTable, fourExpList, varNameList);
            
            //生成数据段
            genDataIns(varNameList, varTable, cmdList);

            //遍历四元式表，生成代码段
            int count = 0;
            int index = 0;
            //填入四元式的index字段
            foreach (FourExp f in fourExpList)
            {
                f.Index = index++;
            }
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
                f.Addr = cmdList.Count() * 4;   //填入四元式对应的汇编指令首地址
                convert(f, varTable, cmdList);
                optimize();
            }
        }
        #endregion

        #region Private Method
        //初始化变量表
        private void initVarTable(VarTable varTable, List<FourExp> fourExpList, List<string> varNameList)
        {
            
            short address = 0x0000;
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

        //获取寄存器，isResult为true，则说明需要返回的是存放结果的寄存器
        private string getReg(FourExp f, VarTable varTable, bool isResult, List<AssemblerIns> cmdList)
        {
            //返回B或者C所在的寄存器
            if (isResult)
            {
                if ((f.Arg1 != "") && (varTable.GetAddrInfo(f.Arg1) != ""))
                {
                    string regB = varTable.GetAddrInfo(f.Arg1);
                    if ((varTable.GetPeekActInfo(f.Arg1) == false) || f.Arg1 == f.Result || regUseTable.GetContent(regB).Count == 1)
                    {
                        return regB;
                    }
                }
                if ((f.Arg2 != "") && (varTable.GetAddrInfo(f.Arg2) != ""))
                {
                    string regC = varTable.GetAddrInfo(f.Arg1);
                    if ((varTable.GetPeekActInfo(f.Arg2) == false) || f.Arg2 == f.Result || regUseTable.GetContent(regC).Count == 1)
                    {
                        return regC;
                    }
                }
            }
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
        private void doAdjust(string regName, VarTable varTable, List<AssemblerIns> cmdList)
        {
            foreach (string varName in regUseTable.GetContent(regName))
            {
                cmdList.Add(AssemblerFac.GenSW(regName, varTable.GetAddr(varName).ToString(), "$ZERO"));
                varTable.SetAddrInfo(varName, "");
            }
            regUseTable.Clear(regName);
        }

        private void convert(FourExp f, VarTable varTable, List<AssemblerIns> cmdList)
        {
            #region Jump Operation
            if (f.Op <= FourExpOperation.jle)
            {
                string operation = "";
                switch (f.Op)
                { 
                    case FourExpOperation.jmp:
                        operation = Mnemonic.J.ToString();
                        cmdList.Add(AssemblerFac.GenJ(f.NextFourExp.ToString()));
                        break;
                    case FourExpOperation.je:
                        operation = Mnemonic.BEQ.ToString();
                        doJump(f, operation, varTable, cmdList);
                        break;
                    case FourExpOperation.jne:
                        operation = Mnemonic.BNE.ToString();
                        doJump(f, operation, varTable, cmdList);
                        break;
                    case FourExpOperation.jg:
                        operation = Mnemonic.BGTZ.ToString();
                        doJump(f, operation, varTable, cmdList);
                        break;
                    case FourExpOperation.jge:
                        operation = Mnemonic.BGEZ.ToString();
                        doJump(f, operation, varTable, cmdList);
                        break;
                    case FourExpOperation.jl:
                        operation = Mnemonic.BLTZ.ToString();
                        doJump(f, operation, varTable, cmdList);
                        break;
                    case FourExpOperation.jle:
                        operation = Mnemonic.BLEZ.ToString();
                        doJump(f, operation, varTable, cmdList);
                        break;
                    default:
                        //错误处理
                        break;
                }

            }
            #endregion

            #region Move Operation
            else if (f.Op == FourExpOperation.mov)
            {
                string regB = "";
                if (varTable.GetAddrInfo(f.Arg1) == "")
                {
                    regB = getReg(f, varTable, false, cmdList);
                    cmdList.Add(AssemblerFac.GenLW(regB, varTable.GetAddr(f.Arg1).ToString(), "$ZERO"));
                    regUseTable.GetContent(regB).Add(f.Arg1);
                    varTable.SetAddrInfo(f.Arg1, regB);
                }
                else
                {
                    regB = varTable.GetAddrInfo(f.Arg1);
                }
                regUseTable.GetContent(regB).Add(f.Result);
                varTable.SetAddrInfo(f.Result, regB);

                if(varTable.GetPeekActInfo(f.Arg1) == false)
                {
                    cmdList.Add(AssemblerFac.GenSW(regB, varTable.GetAddr(f.Arg1).ToString(), "$ZERO"));
                    varTable.SetAddrInfo(f.Arg1, "");
                    regUseTable.GetContent(regB).Remove(f.Arg1);
                }

                if(varTable.GetPeekActInfo(f.Result) == false)
                {
                    cmdList.Add(AssemblerFac.GenSW(regB, varTable.GetAddr(f.Result).ToString(), "$ZERO"));
                    varTable.SetAddrInfo(f.Result, "");
                    regUseTable.GetContent(regB).Remove(f.Result);
                }
            }
            #endregion

            #region Arithmetical or Logical Operation
            else if (f.Op <= FourExpOperation.or)//数学或逻辑运算
            {
                //获取第一个参数的寄存器
                string regA, regB, regC;
                if (varTable.GetAddrInfo(f.Arg1) == "")
                {
                    regB = getReg(f, varTable, false, cmdList);
                    varTable.SetAddrInfo(f.Arg1, regB);
                    regUseTable.Add(regB, f.Arg1);
                    cmdList.Add(AssemblerFac.GenLW(regB, varTable.GetAddr(f.Arg1).ToString(), "$ZERO"));
                }
                else
                {
                    regB = varTable.GetAddrInfo(f.Arg1);
                }
                //获取第二个参数的寄存器
                if (varTable.GetAddrInfo(f.Arg2) == "")
                {
                    regC = getReg(f, varTable, false, cmdList);
                    varTable.SetAddrInfo(f.Arg2, regC);
                    regUseTable.Add(regC, f.Arg2);
                    cmdList.Add(AssemblerFac.GenLW(regC, varTable.GetAddr(f.Arg2).ToString(), "$ZERO"));
                }
                else
                {
                    regC = varTable.GetAddrInfo(f.Arg2);
                }
                regA = getReg(f, varTable, true, cmdList);
                varTable.SetAddrInfo(f.Result, regA);
                regUseTable.Add(regA, f.Result);
                
                if (regA == regB)
                {
                    foreach (string var in regUseTable.GetContent(regB))
                    {
                        cmdList.Add(AssemblerFac.GenSW(regB, varTable.GetAddr(var).ToString(), "$ZERO"));
                        varTable.SetAddrInfo(var, "");
                        regUseTable.GetContent(regB).Remove(var);
                    }
                }
                if (regA == regC)
                {
                    foreach (string var in regUseTable.GetContent(regC))
                    {
                        cmdList.Add(AssemblerFac.GenSW(regC, varTable.GetAddr(var).ToString(), "$ZERO"));
                        varTable.SetAddrInfo(var, "");
                        regUseTable.GetContent(regC).Remove(var);
                    }
                }
                string operation = "";
                switch (f.Op)
                {
                    case FourExpOperation.add:
                        operation = Mnemonic.ADD.ToString();
                        break;
                    case FourExpOperation.sub:
                        operation = Mnemonic.SUB.ToString();
                        break;
                    //case FourExpOperation.mul:

                    //    break;
                    //case FourExpOperation.div:

                    //    break;
                    case FourExpOperation.and:
                        operation = Mnemonic.AND.ToString();
                        break;
                    case FourExpOperation.or:
                        operation = Mnemonic.OR.ToString();
                        break;
                    case FourExpOperation.xor:
                        operation = Mnemonic.XOR.ToString();
                        break;
                    case FourExpOperation.nor:
                        operation = Mnemonic.NOR.ToString();
                        break;
                    default:
                        //错误处理
                        break;
                }
                cmdList.Add(AssemblerFac.GenMathOrLog(operation, regA, regB, regC));
                if ((varTable.GetAddrInfo(f.Arg1) != null) && (varTable.GetPeekActInfo(f.Arg1) == false))
                {
                    cmdList.Add(AssemblerFac.GenSW(regB, varTable.GetAddr(f.Arg1).ToString(), "$ZERO"));
                    varTable.SetAddrInfo(f.Arg1, "");
                    regUseTable.GetContent(regB).Remove(f.Arg1);
                }
                if ((varTable.GetAddrInfo(f.Arg2) != null) && (varTable.GetPeekActInfo(f.Arg2) == false))
                {
                    cmdList.Add(AssemblerFac.GenSW(regC, varTable.GetAddr(f.Arg2).ToString(), "$ZERO"));
                    varTable.SetAddrInfo(f.Arg2, "");
                    regUseTable.GetContent(regC).Remove(f.Arg2);
                }
                if (varTable.GetPeekActInfo(f.Result) == false)
                {
                    cmdList.Add(AssemblerFac.GenSW(regA, varTable.GetAddr(f.Result).ToString(), "$ZERO"));
                    varTable.SetAddrInfo(f.Result, "");
                    regUseTable.GetContent(regA).Remove(f.Result);
                }
            }
            #endregion

            #region Not or Neg Operation
            else if (f.Op == FourExpOperation.neg)
            {
                string regB = varTable.GetAddrInfo(f.Arg1);
                string regA = varTable.GetAddrInfo(f.Result);
                if (regB == "")
                {
                    regB = getReg(f, varTable, false, cmdList);
                    cmdList.Add(AssemblerFac.GenLW(regB, varTable.GetAddr(f.Arg1).ToString(), "$ZERO"));
                    varTable.SetAddrInfo(f.Arg1, regB);
                    regUseTable.GetContent(regB).Add(f.Arg1);
                }
                if (regA == "")
                {
                    regA = getReg(f, varTable, true, cmdList);
                    varTable.SetAddrInfo(f.Result, regA);
                    regUseTable.GetContent(regA).Add(f.Result);
                }
                cmdList.Add(AssemblerFac.GenSUB(regA, "$ZERO", regB));
                if (varTable.GetPeekActInfo(f.Arg1) == false)
                {
                    cmdList.Add(AssemblerFac.GenSW(regB, varTable.GetAddr(f.Arg1).ToString(), "$ZERO"));
                    varTable.SetAddrInfo(f.Arg1, "");
                    regUseTable.GetContent(regB).Remove(f.Arg1);
                }
                if (varTable.GetPeekActInfo(f.Result) == false)
                {
                    cmdList.Add(AssemblerFac.GenSW(regA, varTable.GetAddr(f.Result).ToString(), "$ZERO"));
                    varTable.SetAddrInfo(f.Result, "");
                    regUseTable.GetContent(regA).Remove(f.Result);
                }
            }
            else if (f.Op == FourExpOperation.not)
            {
                string regB = varTable.GetAddrInfo(f.Arg1);
                string regA = varTable.GetAddrInfo(f.Result);
                if (regB == "")
                {
                    regB = getReg(f, varTable, false, cmdList);
                    cmdList.Add(AssemblerFac.GenLW(regB, varTable.GetAddr(f.Arg1).ToString(), "$ZERO"));
                    varTable.SetAddrInfo(f.Arg1, regB);
                    regUseTable.GetContent(regB).Add(f.Arg1);
                }
                if (regA == "")
                {
                    regA = getReg(f, varTable, true, cmdList);
                    varTable.SetAddrInfo(f.Result, regA);
                    regUseTable.GetContent(regA).Add(f.Result);
                }
                cmdList.Add(AssemblerFac.GenXORI(regA, regB, Convert.ToString(1)));//a = NOT b => a = b xor 1
                if (varTable.GetPeekActInfo(f.Arg1) == false)
                {
                    cmdList.Add(AssemblerFac.GenSW(regB, varTable.GetAddr(f.Arg1).ToString(), "$ZERO"));
                    varTable.SetAddrInfo(f.Arg1, "");
                    regUseTable.GetContent(regB).Remove(f.Arg1);
                }
                if (varTable.GetPeekActInfo(f.Result) == false)
                {
                    cmdList.Add(AssemblerFac.GenSW(regA, varTable.GetAddr(f.Result).ToString(), "$ZERO"));
                    varTable.SetAddrInfo(f.Result, "");
                    regUseTable.GetContent(regA).Remove(f.Result);
                }
            }
            #endregion
            
            else
            {
                //错误处理
            }
        }

        private void doJump(FourExp f, string operation, VarTable varTable, List<AssemblerIns> cmdList)
        {
            string reg1 = "", reg2 = "";
            reg1 = varTable.GetAddrInfo(f.Arg1);
            reg2 = varTable.GetAddrInfo(f.Arg2);
            if (reg1 == "")
            {
                reg1 = getReg(f, varTable, false, cmdList);
                cmdList.Add(AssemblerFac.GenLW(reg1, varTable.GetAddr(f.Arg1).ToString(), "$ZERO"));
                regUseTable.GetContent(reg1).Add(f.Arg1);
                varTable.SetAddrInfo(f.Arg1, reg1);
            }
            if (reg2 == "")
            {
                reg2 = getReg(f, varTable, false, cmdList);
                cmdList.Add(AssemblerFac.GenLW(reg2, varTable.GetAddr(f.Arg2).ToString(), "$ZERO"));
                regUseTable.GetContent(reg2).Add(f.Arg2);
                varTable.SetAddrInfo(f.Arg2, reg2);
            }
            cmdList.Add(AssemblerFac.GenSLT("$T0", reg1, reg2));
            cmdList.Add(AssemblerFac.GenJUMP(operation, "$T0", f.NextFourExp.ToString()));
            adjustAfterJump(f, reg1, reg2, varTable, cmdList);
        }

        //跳转之后的调整
        private void adjustAfterJump(FourExp f, string reg1, string reg2, VarTable varTable, List<AssemblerIns> cmdList)
        {
            if (varTable.GetPeekActInfo(f.Arg1) == false)
            {
                cmdList.Add(AssemblerFac.GenSW(reg1, varTable.GetAddr(f.Arg1).ToString(), "$ZERO"));
                varTable.SetAddrInfo(f.Arg1, "");
                regUseTable.GetContent(reg1).Remove(f.Arg1);
            }
            if (varTable.GetPeekActInfo(f.Arg2) == false)
            {
                cmdList.Add(AssemblerFac.GenSW(reg2, varTable.GetAddr(f.Arg2).ToString(), "$ZERO"));
                varTable.SetAddrInfo(f.Arg2, "");
                regUseTable.GetContent(reg2).Remove(f.Arg2);
            }
        }

        //生成数据段
        private void genDataIns(List<string> varNameList, VarTable varTable, List<AssemblerIns> cmdList)
        {
            foreach (string varName in varNameList)
            {
                if (varTable.GetType(varName) == VariableType.INT || varTable.GetType(varName) == VariableType.CHAR)
                {
                    short varValue = (short)varTable.GetValue(varName);
                    short varAddr = varTable.GetAddr(varName);
                    cmdList.Add(AssemblerFac.GenLUI("$T0", varValue.ToString()));
                    cmdList.Add(AssemblerFac.GenSRL("$T0", "$T0", Convert.ToString(16)));
                    cmdList.Add(AssemblerFac.GenSW("$T0", varAddr.ToString(), "$ZERO"));
                }
                else
                {
                    int value = varTable.GetValue(varName);
                    short high = (short)(value>>16);
                    short varAddr = varTable.GetAddr(varName);
                    cmdList.Add(AssemblerFac.GenLUI("$T0", high.ToString()));
                    short low = (short)(value & 0xffff);
                    cmdList.Add(AssemblerFac.GenORI("$T0", "$T0", low.ToString()));
                    cmdList.Add(AssemblerFac.GenSW("$T0", varAddr.ToString(), "$ZERO"));
                }
            }
        }

        //优化
        private void optimize()
        { 
        
        }
        #endregion
    }
}