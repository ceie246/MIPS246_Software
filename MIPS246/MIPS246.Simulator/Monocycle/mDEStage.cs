using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MipsSimulator.Assembler;
using MipsSimulator.Devices;
using MipsSimulator.Tools;

namespace MipsSimulator.Monocycle
{
    class mDEStage
    {
        //能否运行
        static public int enableRun = 0;

        static public bool isEnd = false;

        static public Code code;

        static public object[] args = null;

        static public void Initialize()
        {
            mDEStage.enableRun = -1;
            isEnd = false;
        }

        static public void Start()
        {
            isEnd = false;
            //开始标志
            ThreadFun();
        }

        static private void ThreadFun()
        {
            //判断可不可以运行
            if (enableRun < 0)
            {
                isEnd = true;
                return;
            }
            mDEStage.enableRun--;

            object obj1 = null;
            object obj2 = null;
            object obj3 = null;

            switch (code.codeType)
            {
                case CodeType.ADD:
                case CodeType.SUB:
                case CodeType.AND:
                case CodeType.OR:
                case CodeType.XOR:
                case CodeType.NOR:
                case CodeType.SLT:
                case CodeType.ADDU:
                case CodeType.SUBU:
                    {
                        string rs = code.machineCode.Substring(6, 5);
                        string rt = code.machineCode.Substring(11, 5);
                        rs = "$" + CommonTool.StrToNum(TypeCode.Int32, rs, 2);
                        rt = "$" + CommonTool.StrToNum(TypeCode.Int32, rt, 2);
                        string strArg1 = Register.GetRegisterValue(rs);
                        string strArg2 = Register.GetRegisterValue(rt);
                        obj1 = (Int32)CommonTool.StrToNum(TypeCode.Int32, strArg1, 16);
                        obj2 = (Int32)CommonTool.StrToNum(TypeCode.Int32, strArg2, 16);
                        
                        mEXEStage.code = mDEStage.code;
                        mEXEStage.args = new object[2] { obj1, obj2};
                        mEXEStage.enableRun++;
                        break;
                    }

                case CodeType.SLTU:
                    {
                        string rs = code.machineCode.Substring(6, 5);
                        string rt = code.machineCode.Substring(11, 5);
                        rs = "$" + CommonTool.StrToNum(TypeCode.Int32, rs, 2);
                        rt = "$" + CommonTool.StrToNum(TypeCode.Int32, rt, 2);
                        string strArg1 = Register.GetRegisterValue(rs);
                        string strArg2 = Register.GetRegisterValue(rt);
                        obj1 = (UInt32)CommonTool.StrToNum(TypeCode.UInt32, strArg1, 16);
                        obj2 = (UInt32)CommonTool.StrToNum(TypeCode.UInt32, strArg2, 16);
                        
                        mEXEStage.code = mDEStage.code;
                        mEXEStage.args = new object[2] { obj1, obj2 };
                        mEXEStage.enableRun++;
                        break;
                    }
                case CodeType.SLL:
                case CodeType.SRL:
                case CodeType.SRA:
                    {
                        string rt = code.machineCode.Substring(11, 5);
                        string sa = code.machineCode.Substring(21, 5);
                        rt = "$" + CommonTool.StrToNum(TypeCode.Int32, rt, 2);
                        string strArg1 = Register.GetRegisterValue(rt);
                        obj1 = (Int32)CommonTool.StrToNum(TypeCode.Int32, strArg1, 16);
                        // obj2=code.machineCode.
                        obj2 = CommonTool.binToDec(sa);
                        
                        mEXEStage.code = mDEStage.code;
                        mEXEStage.args = new object[2] { obj1, obj2 };
                        mEXEStage.enableRun++;
                        break;
                    }
                case CodeType.SLLV:
                case CodeType.SRLV:
                case CodeType.SRAV:
                    {
                        string rs = code.machineCode.Substring(6, 5);
                        string rt = code.machineCode.Substring(11, 5);
                        rs = "$" + CommonTool.StrToNum(TypeCode.Int32, rs, 2);
                        rt = "$" + CommonTool.StrToNum(TypeCode.Int32, rt, 2);
                        string strArg1 = Register.GetRegisterValue(rs);
                        string strArg2 = Register.GetRegisterValue(rt);
                        obj1 = (Int32)CommonTool.StrToNum(TypeCode.Int32, strArg1, 16);
                        obj2 = (Int32)CommonTool.StrToNum(TypeCode.Int32, strArg2, 16);
                        
                        mEXEStage.code = mDEStage.code;
                        mEXEStage.args = new object[2] { obj1, obj2 };
                        mEXEStage.enableRun++;
                        break;
                    }
                case CodeType.JR:
                    {
                        string rs = code.machineCode.Substring(6, 5);
                        rs = "$" + CommonTool.StrToNum(TypeCode.Int32, rs, 2);
                        string strArg1 = Register.GetRegisterValue(rs);
                        obj1 = (Int32)CommonTool.StrToNum(TypeCode.Int32, strArg1, 16);
                        
                        mEXEStage.code = mDEStage.code;
                        mEXEStage.args = new object[1] { obj1 };
                        mEXEStage.enableRun++;
                        
                        break;
                    }
                case CodeType.ADDI:
                case CodeType.ADDIU:
                    {
                        string rs = code.machineCode.Substring(6, 5);
                        rs = "$" + CommonTool.StrToNum(TypeCode.Int32, rs, 2);
                        string immediate = code.machineCode.Substring(16, 16);
                        immediate = CommonTool.sign_extend(immediate, 32);
                        string strArg1 = Register.GetRegisterValue(rs);
                        obj1 = (Int32)CommonTool.StrToNum(TypeCode.Int32, strArg1, 16);
                        obj2 = (Int32)CommonTool.StrToNum(TypeCode.Int32, immediate, 2);
                        
                        mEXEStage.code = mDEStage.code;
                        mEXEStage.args = new object[2] { obj1, obj2 };
                        mEXEStage.enableRun++;
                        break;
                    }
                case CodeType.ANDI:
                case CodeType.ORI:
                case CodeType.XORI:
                    {
                        string rs = code.machineCode.Substring(6, 5);
                        rs = "$" + CommonTool.StrToNum(TypeCode.Int32, rs, 2);
                        string immediate = code.machineCode.Substring(16, 16);
                        string strArg1 = Register.GetRegisterValue(rs);
                        obj1 = (Int32)CommonTool.StrToNum(TypeCode.Int32, strArg1, 16);
                        obj2 = (Int32)CommonTool.StrToNum(TypeCode.Int32, CommonTool.zero_extend(immediate, 32), 2);
                        
                        mEXEStage.code = mDEStage.code;
                        mEXEStage.args = new object[2] { obj1, obj2 };
                        mEXEStage.enableRun++;
                        break;
                    }
                case CodeType.LUI:
                    {
                        string immediate = code.machineCode.Substring(16, 16);
                        obj1 = immediate;
                        
                        mEXEStage.code = mDEStage.code;
                        mEXEStage.args = new object[1] { obj1 };
                        mEXEStage.enableRun++;
                        break;
                    }
                case CodeType.LW:
                    {
                        string Rbase = code.machineCode.Substring(6, 5);
                        Rbase = "$" + CommonTool.StrToNum(TypeCode.Int32, Rbase, 2);
                        string offset = code.machineCode.Substring(16, 16);
                        string strArg1 = Register.GetRegisterValue(Rbase);
                        obj1 = (Int32)CommonTool.StrToNum(TypeCode.Int32, strArg1, 16);
                        obj2 = (Int32)CommonTool.StrToNum(TypeCode.Int32, CommonTool.sign_extend(offset, 32), 2);
                        
                        mEXEStage.code = mDEStage.code;
                        mEXEStage.args = new object[2] { obj1, obj2 };
                        mEXEStage.enableRun++;
                        break;
                    }
                case CodeType.SW:
                    {
                        string Rbase = code.machineCode.Substring(6, 5);
                        string rt = code.machineCode.Substring(11, 5);
                        Rbase = "$" + CommonTool.StrToNum(TypeCode.Int32, Rbase, 2);
                        rt = "$" + CommonTool.StrToNum(TypeCode.Int32, rt, 2);
                        string offset = code.machineCode.Substring(16, 16);
                        string strArg1 = Register.GetRegisterValue(Rbase);
                        string strArg3 = Register.GetRegisterValue(rt);
                        obj1 = (Int32)CommonTool.StrToNum(TypeCode.Int32, strArg1, 16);//Rbase
                        obj2 = (Int32)CommonTool.StrToNum(TypeCode.Int32, CommonTool.sign_extend(offset, 32), 2);
                        obj3 = (Int32)CommonTool.StrToNum(TypeCode.Int32, strArg3, 16);//rt
                        
                        mEXEStage.code = mDEStage.code;
                        mEXEStage.args = new object[3] { obj1, obj2, obj3 };
                        mEXEStage.enableRun++;
                        break;
                    }
                case CodeType.BEQ:
                case CodeType.BNE:
                    {
                        string rs = code.machineCode.Substring(6, 5);
                        string rt = code.machineCode.Substring(11, 5);
                        string offset = code.machineCode.Substring(16, 16);
                        offset = offset + "00";
                        offset = CommonTool.sign_extend(offset, 32);
                        rs = "$" + CommonTool.StrToNum(TypeCode.Int32, rs, 2);
                        rt = "$" + CommonTool.StrToNum(TypeCode.Int32, rt, 2);
                        string strArg1 = Register.GetRegisterValue(rs);
                        string strArg2 = Register.GetRegisterValue(rt);
                        obj1 = (Int32)CommonTool.StrToNum(TypeCode.Int32, strArg1, 16);//rs
                        obj2 = (Int32)CommonTool.StrToNum(TypeCode.Int32, offset, 2);
                        obj3 = (Int32)CommonTool.StrToNum(TypeCode.Int32, strArg2, 16);//rt

                        mEXEStage.code = mDEStage.code;
                        mEXEStage.args = new object[4] { obj1, obj2, obj3, mDEStage.args[0] };
                        mEXEStage.enableRun++;
                        //if (IFStage.ifOverOrNop && bzSuccess)
                        //{
                        //    IFStage.codeCurrentAddress = DEStage.bzAddress;
                        //    Register.setPC(IFStage.codeCurrentAddress);
                        //    DEStage.code = new Code(CodeType.NOP, null, null, null);
                        //}
                        break;
                    }
                case CodeType.SLTI:
                    {
                        string rs = code.machineCode.Substring(6, 5);
                        rs = "$" + CommonTool.StrToNum(TypeCode.Int32, rs, 2);
                        string strArg1 = Register.GetRegisterValue(rs);
                        obj1 = (Int32)CommonTool.StrToNum(TypeCode.Int32, strArg1, 16);

                        string immediate = code.machineCode.Substring(16, 16);
                        immediate = CommonTool.sign_extend(immediate, 32);
                        obj2 = (Int32)CommonTool.StrToNum(TypeCode.Int16, immediate, 2);
                        
                        mEXEStage.code = mDEStage.code;
                        mEXEStage.args = new object[2] { obj1, obj2 };
                        mEXEStage.enableRun++;
                        break;
                    }
                case CodeType.SLTIU:
                    {
                        string rs = code.machineCode.Substring(6, 5);
                        rs = "$" + CommonTool.StrToNum(TypeCode.Int32, rs, 2);
                        string strArg1 = Register.GetRegisterValue(rs);
                        obj1 = (Int32)CommonTool.StrToNum(TypeCode.Int32, strArg1, 16);

                        string immediate = code.machineCode.Substring(16, 16);
                        immediate = CommonTool.zero_extend(immediate, 32);
                        obj2 = (Int32)CommonTool.StrToNum(TypeCode.Int32, immediate, 2);

                        mEXEStage.code = mDEStage.code;
                        mEXEStage.args = new object[2] { obj1, obj2 };
                        mEXEStage.enableRun++;
                        break;
                    }
                case CodeType.J:
                case CodeType.JAL:
                    {
                        string instr_index = code.machineCode.Substring(6, 26);
                        obj1 = instr_index;
                       
                        mEXEStage.code = mDEStage.code;
                        
                        mEXEStage.args = new object[2] { obj1, mDEStage.args[0] };
                        mEXEStage.enableRun++;
                        //if (IFStage.ifOverOrNop && bzSuccess)
                        //{
                        //    IFStage.codeCurrentAddress = DEStage.bzAddress;
                        //    DEStage.code = new Code(CodeType.NOP, null, null, null);
                        //}
                        break;
                    }
            }
            if (MipsSimulator.Program.mode == 1)
            {
                Form1.codeColor(mDEStage.code.Index, 2);
            }
            isEnd = true;
            return;
        }

        static public void Wait()
        {
            
        }

        
    }
}
