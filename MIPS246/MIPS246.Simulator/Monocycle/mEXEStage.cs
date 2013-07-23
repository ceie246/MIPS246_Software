using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MipsSimulator.Assembler;
using MipsSimulator.Devices;
using MipsSimulator.Tools;
using MipsSimulator.Cmd;

namespace MipsSimulator.Monocycle
{
    class mEXEStage
    {
        //能否运行
        static public int enableRun = 0;

        static public bool isEnd = false;

        static public int bzAddress;

        static public bool bzSuccess;

        static public Code code;

        static public object[] args = null;

        static public void Initialize()
        {
            mEXEStage.enableRun = -1;
            mEXEStage.isEnd = false;
            mEXEStage.bzSuccess = false;
            // code = new Code(CodeType.NOP, null, null, null);
        }

        static public void Start()
        {
            isEnd = false;
            bzSuccess = false;
            //开始
            ThreadFun();
            
            
        }

        static private void ThreadFun()
        {
            if (enableRun < 0)
            {
                isEnd = true;
                return;
            }

            mEXEStage.enableRun--;

            object obj1 = null;
            object obj2 = null;

            switch (code.codeType)
            {
                case CodeType.ADD:
                    {
                        obj1 = (Int32)mEXEStage.args[0] + (Int32)mEXEStage.args[1];
                        break;
                    }
                case CodeType.ADDU:
                    {
                        obj1 = (Int32)mEXEStage.args[0] + (Int32)mEXEStage.args[1];
                        break;
                    }
                case CodeType.SUB:
                    {
                        obj1 = (Int32)mEXEStage.args[0] - (Int32)mEXEStage.args[1];
                        break;
                    }
                case CodeType.SUBU:
                    {
                        obj1 = (Int32)mEXEStage.args[0] - (Int32)mEXEStage.args[1];
                        break;
                    }
                case CodeType.AND:
                    {
                        obj1 = (Int32)mEXEStage.args[0] & (Int32)mEXEStage.args[1];
                        break;
                    }
                case CodeType.OR:
                    {
                        obj1 = (Int32)mEXEStage.args[0] | (Int32)mEXEStage.args[1];
                        break;
                    }
                case CodeType.XOR:
                    {
                        obj1 = (Int32)mEXEStage.args[0] ^ (Int32)mEXEStage.args[1];
                        break;
                    }
                case CodeType.NOR:
                    {
                        obj1 = ~((Int32)mEXEStage.args[0] | (Int32)mEXEStage.args[1]);
                        break;
                    }
                case CodeType.SLT:
                    {
                        if ((Int32)mEXEStage.args[0] < (Int32)mEXEStage.args[1])
                            obj1 = (Int32)1;
                        else
                            obj1 = (Int32)0;
                        break;
                    }
                case CodeType.SLTU:
                    {
                        if ((UInt32)mEXEStage.args[0] < (UInt32)mEXEStage.args[1])
                            obj1 = (Int32)1;
                        else
                            obj1 = (Int32)0;
                        break;
                    }
                case CodeType.SLL:
                    {
                        obj1 = (Int32)mEXEStage.args[0] << (Int32)mEXEStage.args[1];
                        break;
                    }
                case CodeType.SRL:
                    {
                        int rt = (Int32)mEXEStage.args[0];
                        obj1 = unchecked((int)((uint)rt >> (Int32)mEXEStage.args[1]));
                        break;
                    }
                case CodeType.SRA:
                    {
                        int rt = (Int32)mEXEStage.args[0];
                        obj1 = rt >> (Int32)mEXEStage.args[1];
                        break;
                    }
                case CodeType.SLLV:
                    {
                        int rs = (Int32)mEXEStage.args[0];
                        int rt = (Int32)mEXEStage.args[1];
                        obj1 = rt << rs;
                        break;
                    }
                case CodeType.SRLV:
                    {
                        int rs = (Int32)mEXEStage.args[0];
                        int rt = (Int32)mEXEStage.args[1];
                        obj1 = unchecked((int)((uint)rt >> rs));
                        break;
                    }
                case CodeType.SRAV:
                    {
                        int rs = (Int32)mEXEStage.args[0];
                        int rt = (Int32)mEXEStage.args[1];
                        obj1 = rt >> rs;
                        break;
                    }
                case CodeType.JR:
                    {
                        mEXEStage.bzAddress = (Int32)mEXEStage.args[0];
                        
                        break;
                    }
                case CodeType.ADDI:
                    {
                        obj1 = (Int32)mEXEStage.args[0] + (Int32)mEXEStage.args[1];
                        break;
                    }
                case CodeType.ADDIU:
                    {
                        obj1 = (Int32)mEXEStage.args[0] + (Int32)mEXEStage.args[1];
                        break;
                    }
                case CodeType.ANDI:
                    {
                        obj1 = (UInt32)mEXEStage.args[0] & (UInt32)mEXEStage.args[1];
                        break;
                    }
                case CodeType.ORI:
                    {
                        obj1 = (Int32)mEXEStage.args[0] | (Int32)mEXEStage.args[1];
                        break;
                    }
                case CodeType.XORI:
                    {
                        obj1 = (UInt32)mEXEStage.args[0] ^ (UInt32)mEXEStage.args[1];
                        break;
                    }
                case CodeType.LUI:
                    {
                        string immediate = Convert.ToString(mEXEStage.args[0]);
                        immediate = immediate + "0000000000000000";
                        obj1 = (Int32)CommonTool.StrToNum(TypeCode.Int32, immediate, 2);

                        break;
                    }
                case CodeType.LW:
                    {
                        obj1 = (Int32)mEXEStage.args[0] + (Int32)mEXEStage.args[1];
                        break;
                    }
                case CodeType.SW:
                    {
                        obj1 = (Int32)mEXEStage.args[0] + (Int32)mEXEStage.args[1];//address
                        obj2 = (Int32)mEXEStage.args[2];//rt
                        break;
                    }
                case CodeType.BEQ:
                    {
                        int rs = (Int32)mEXEStage.args[0];
                        int rt = (Int32)mEXEStage.args[2];
                        if (rs == rt)
                        {
                            mEXEStage.bzSuccess = true;
                            mEXEStage.bzAddress = (Int32)mEXEStage.args[1] + (Int32)mEXEStage.args[3];
                           
                        }
                        else
                        {
                            mEXEStage.bzSuccess = false;
                            
                        }
                        break;
                    }
                case CodeType.BNE:
                    {
                        int rs = (Int32)mEXEStage.args[0];
                        int rt = (Int32)mEXEStage.args[2];
                        if (rs != rt)
                        {
                            mEXEStage.bzSuccess = true;
                            mEXEStage.bzAddress = (Int32)mEXEStage.args[1] + (Int32)mEXEStage.args[3];
                            
                        }
                        else
                        {
                            mEXEStage.bzSuccess = false;
                            
                        }
                        break;
                    }
                case CodeType.SLTI:
                    {
                        int rs = (Int32)mEXEStage.args[0];
                        int immediate = (Int32)mEXEStage.args[1];
                        if (rs < immediate)
                            obj1 = 1;
                        else
                            obj1 = 0;
                        break;
                    }
                case CodeType.SLTIU:
                    {
                        int rs = (Int32)mEXEStage.args[0];
                        int immediate = (Int32)mEXEStage.args[1];
                        if (rs < immediate)
                            obj1 = 1;
                        else
                            obj1 = 0;
                        break;
                    }
                case CodeType.J:
                    {
                        string target = Convert.ToString(mEXEStage.args[0]);
                        target = target + "00";
                        target = CommonTool.zero_extend(target, 32);
                        int target1 = (Int32)CommonTool.StrToNum(TypeCode.Int32, target, 2);
                        int PC1 = ((Int32)mEXEStage.args[1] - 4) & (Int32)CommonTool.StrToNum(TypeCode.Int32, "F0000000", 16);
                        // obj1 = PC1 | target1;
                        mEXEStage.bzAddress = PC1 | target1;
                        
                        break;
                    }
                case CodeType.JAL:
                    {
                        int PC1 = (Int32)mEXEStage.args[1] + 4;//PC+8
                        obj1 = PC1;
                        // int PC2 = (Int32)EXEStage.args[1] - 4;//PC
                        string target = Convert.ToString(mEXEStage.args[0]);
                        target = target + "00";
                        target = CommonTool.zero_extend(target, 32);
                        int target1 = (Int32)CommonTool.StrToNum(TypeCode.Int32, target, 2);
                        int PC2 = ((Int32)mEXEStage.args[1] - 4) & (Int32)CommonTool.StrToNum(TypeCode.Int32, "F0000000", 16);

                        mEXEStage.bzAddress = PC1 | target1;
                        
                        break;
                    }
            }
            switch (code.codeType)
            {
                case CodeType.ADD:
                case CodeType.ADDU:
                case CodeType.SUB:
                case CodeType.SUBU:
                case CodeType.AND:
                case CodeType.OR:
                case CodeType.XOR:
                case CodeType.NOR:
                case CodeType.SLT:
                case CodeType.SLTU:
                case CodeType.SLL:
                case CodeType.SRL:
                case CodeType.SRA:
                case CodeType.SLLV:
                case CodeType.SRLV:
                case CodeType.SRAV:
                case CodeType.ADDI:
                case CodeType.ADDIU:
                case CodeType.ANDI:
                case CodeType.ORI:
                case CodeType.XORI:
                case CodeType.LUI:
                case CodeType.LW:
                case CodeType.SLTI:
                case CodeType.SLTIU:
                case CodeType.JAL:
                    {
                        mMEMStage.code = mEXEStage.code;
                        mMEMStage.args = new object[1] { obj1 };
                        mMEMStage.enableRun++;
                        break;
                    }

                case CodeType.SW:
                    {
                        mMEMStage.code = mEXEStage.code;
                        mMEMStage.args = new object[2] { obj1, obj2 };
                        mMEMStage.enableRun++;
                        break;
                    }
                case CodeType.JR:
                case CodeType.BEQ:
                case CodeType.BNE:
                case CodeType.J:
                    {
                        mMEMStage.code = mEXEStage.code;
                        mMEMStage.args = null;
                        mMEMStage.enableRun ++;
                        break;
                    }
            }
            if (MipsSimulator.Program.mode == 1)
            {
                int colorIndex = (int)cmdMode.lineTable[mEXEStage.code.Index];
                Form1.codeColor(colorIndex, 3);
            }
            
            isEnd = true;
            return;
        }
    }
}
