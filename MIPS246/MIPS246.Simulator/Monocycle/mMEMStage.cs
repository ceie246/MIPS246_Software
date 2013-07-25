using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MipsSimulator.Assembler;
using MipsSimulator.Devices;
using MipsSimulator.Cmd;

namespace MipsSimulator.Monocycle
{
    class mMEMStage
    {
        //能否运行
        static public int enableRun = 0;

        static public bool isEnd = false;

        static public Code code;

        static public object[] args = null;

        static public void Initialize()
        {
            mMEMStage.enableRun = -1;
            isEnd = false;
        }

        static public void Start()
        {
            isEnd = false;
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

            mMEMStage.enableRun--;

            object obj1 = null;
            
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
                case CodeType.SLTU://以上是三个寄存器类型
                case CodeType.SLL:
                case CodeType.SRL:
                case CodeType.SRA://以上是2个寄存器类型
                case CodeType.SLLV:
                case CodeType.SRLV:
                case CodeType.SRAV://以上是三个寄存器类型
                case CodeType.ADDI:
                case CodeType.ADDIU:
                case CodeType.ANDI:
                case CodeType.ORI:
                case CodeType.XORI://以上是2个寄存器类型
                case CodeType.LUI:
                case CodeType.SLTI:
                case CodeType.SLTIU:
                case CodeType.JAL:
                    {
                        mWBStage.code = mMEMStage.code;
                        mWBStage.args = new object[1] { mMEMStage.args[0] };
                        mWBStage.enableRun++;
                        break;
                    }
                case CodeType.LW:
                    {
                        Int32 address = (Int32)mMEMStage.args[0];
                        Int32 value = 0;
                        if (Memory.getMemory(address, ref value))
                        {
                            obj1 = value;
                        }
                        else
                        {
                            if (MipsSimulator.Program.mode == 1)
                            {
                                Form1.Message(code.codeStr+" error\r\n");
                                RunTimeCode.codeList.Clear();
                                break;
                            }
                            if (MipsSimulator.Program.mode == 0)
                            {
                                throw new Exception(code.codeStr + " error\r\n");
                               // cmdMode.addMessage(code.codeStr + " error\r\n");
                                //RunTimeCode.codeList.Clear();
                               // break;
                            }
                        }
                       
                        mWBStage.code = mMEMStage.code;
                        mWBStage.args = new object[1] { obj1 };
                        mWBStage.enableRun++;
                        break;
                    }
                case CodeType.SW:
                    {
                        Int32 address = (Int32)mMEMStage.args[0];
                        Int32 value = (Int32)mMEMStage.args[1];
                        if (!Memory.setMemory(address, value))
                        {
                            if (MipsSimulator.Program.mode == 1)
                            {
                                Form1.Message(code.codeStr + "error\r\n");
                                 RunTimeCode.codeList.Clear();
                                 break;
                            }
                            if (MipsSimulator.Program.mode == 0)
                            {
                                throw new Exception(code.codeStr + " error\r\n");
                            }
                        }
                        
                        mWBStage.code = mMEMStage.code;
                        mWBStage.args = null;
                        mWBStage.enableRun++;
                        break;
                    }

                case CodeType.J:
                case CodeType.JR:
                case CodeType.BEQ:
                case CodeType.BNE:
                    {
                        mWBStage.code = mMEMStage.code;
                        mWBStage.args = null;
                        mWBStage.enableRun++;
                        break;
                    }
            }

            switch (code.codeType)
            {
                case CodeType.JR:
                case CodeType.JAL:
                case CodeType.J:
                    {
                        Register.setPC(mEXEStage.bzAddress);
                        break;
                    }
                case CodeType.BEQ:
                case CodeType.BNE:
                    {
                        if (mEXEStage.bzSuccess)
                        {
                            Register.setPC(mEXEStage.bzAddress);
                        }
                        else
                            Register.setPC(mIFStage.NPC);
                        break;
                    }
                default:
                    {
                        Register.setPC(mIFStage.NPC);
                        break;
                    }
            }

            if (MipsSimulator.Program.mode == 1)
            {
                int colorIndex = (int)cmdMode.lineTable[mMEMStage.code.Index];
                Form1.codeColor(colorIndex, 4);
            }
            
            isEnd = true;
            return;
        }
    }
}
