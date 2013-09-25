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
    class mWBStage
    {
        //能否运行
        static public int enableRun = 0;

        static public bool isEnd = false;

        static public Code code;

        static public object[] args = null;

        static public void Initialize()
        {
            mWBStage.enableRun = -1;
            isEnd = false;
        }

        static public void Start()
        {
            isEnd = false;
            ThreadFun();
        }

        static private void ThreadFun()
        {
            if (enableRun < 0)
            {
                isEnd = true;
                return;
            }

            mWBStage.enableRun--;


            //  string str1 = null;

            switch (code.codeType)
            {
                case CodeType.ADD:
                
                case CodeType.SUB:
                
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
                    {
                        string rd = code.machineCode.Substring(16, 5);
                        rd = "$" + CommonTool.StrToNum(TypeCode.Int32, rd, 2);
                        Int32 value = (Int32)mWBStage.args[0];
                        if (!Register.SetRegisterValue(rd, value))
                        {
                            if (MipsSimulator.Program.mode == 1)
                            {
                                Form1.Message("address 0x" + code.address.ToString("X8") + code.codeStr + " error\r\n");
                            }
                        }
                        break;
                    }
                case CodeType.ADDU:
                case CodeType.SUBU:
                    {
                        string rd = code.machineCode.Substring(16, 5);
                        rd = "$" + CommonTool.StrToNum(TypeCode.Int32, rd, 2);
                        UInt32 value = (UInt32)mWBStage.args[0];
                        if (!Register.SetRegisterValue(rd, value))
                        {
                            if (MipsSimulator.Program.mode == 1)
                            {
                                Form1.Message("address 0x" + code.address.ToString("X8") + code.codeStr + " error\r\n");
                            }
                        }
                        break;
                    }
                case CodeType.ADDI:
                case CodeType.ADDIU:
                case CodeType.ANDI:
                case CodeType.ORI:
                case CodeType.XORI://以上是2个寄存器类型

                case CodeType.LUI:

                case CodeType.SLTI:
                case CodeType.SLTIU:
                    {
                        string rt = code.machineCode.Substring(11, 5);
                        rt = "$" + CommonTool.StrToNum(TypeCode.Int32, rt, 2);
                        Int32 value = (Int32)mWBStage.args[0];
                        if (!Register.SetRegisterValue(rt, value))
                        {
                            if (MipsSimulator.Program.mode == 1)
                            {
                                Form1.Message("address 0x" + code.address.ToString("X8") + code.codeStr + " error\r\n");
                            }
                        }
                        break;
                    }

                case CodeType.JAL:
                    {
                        Int32 value = (Int32)mWBStage.args[0];
                        if (!Register.SetRegisterValue("$31", value))
                        {
                            if (MipsSimulator.Program.mode == 1)
                            {
                                Form1.Message("address 0x" + code.address.ToString("X8") + code.codeStr + " error\r\n");
                            }
                        }
                        break;
                    }
                case CodeType.LW:
                    {
                        string rt = code.machineCode.Substring(11, 5);
                        rt = "$" + CommonTool.StrToNum(TypeCode.Int32, rt, 2);
                        Int32 value = (Int32)mWBStage.args[0];
                        if (!Register.SetRegisterValue(rt, value))
                        {
                            if (MipsSimulator.Program.mode == 1)
                            {
                                Form1.Message(code.codeStr + " error\r\n");
                            }
                            if (MipsSimulator.Program.mode == 0)
                            {
                                throw new Exception(code.codeStr + " error\r\n");
                            }
                        }
                        break;
                    }
            }
            if (MipsSimulator.Program.mode == 1)
            {
                int colorIndex = (int)cmdMode.lineTable[mWBStage.code.Index];
                Form1.codeColor(colorIndex, 5);
            }
           
            isEnd = true;
            return;
        }
    }
}
