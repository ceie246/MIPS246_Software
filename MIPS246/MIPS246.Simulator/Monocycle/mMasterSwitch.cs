using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MipsSimulator.Assembler;
using MipsSimulator.Devices;
using MipsSimulator.Tools;
using MipsSimulator.Cmd;
using System.IO;

namespace MipsSimulator.Monocycle
{
    class mMasterSwitch
    {
        static private bool isTorun = false;
        static private int point = 0;

        static private void ThreadFun()
        {
            //初始化
            Initialize();
            string outputPath = Form1.outputName;
           
            if (File.Exists(outputPath))
            {
                File.Delete(outputPath);
            }
            //判断是否要继续
            while (isTorun)
            {
                if (!IFRun())
                {
                    point = 0;
                    Form1.isBreak = false;
                    Form1.isStep2 = false;
                    break;
                }
                else
                {
                    string strArg1 = MipsSimulator.Devices.Register.GetRegisterValue("pc");
                    string pcstr = "pc = " + strArg1.Substring(2) + "\r\n";
                    try
                    {
                        mIFStage.Start();
                        mDEStage.Start();
                        mEXEStage.Start();
                        mMEMStage.Start();
                        mWBStage.Start();
                    }
                    catch (Exception e)
                    {
                        MipsSimulator.Tools.FileControl.WriteFile(outputPath, e.Message);
                        return;
                    }

                    int PC = (Int32)CommonTool.StrToNum(TypeCode.Int32, strArg1, 16);
                    //获取指令
                    Code code = RunTimeCode.GetCode(PC);
                    string codeStr = code.machineCode;
                    Int32 tmp = (Int32)CommonTool.StrToNum(TypeCode.Int32, codeStr, 2);
                    codeStr = tmp.ToString("X8");
                    for (int i = 0; i <= 31; i++)
                    {
                        string registerName = "$" + i;
                        string value = MipsSimulator.Devices.Register.GetRegisterValue(registerName);
                        value = "regfiles" + i + " = " + value.Substring(2) + "\r\n";
                        value = value.ToLower();
                        MipsSimulator.Tools.FileControl.WriteFile(outputPath, value);
                    }
                    string instr = "instr = " + codeStr + "\r\n";
                    instr = instr.ToLower();
                    pcstr = pcstr.ToLower();
                    MipsSimulator.Tools.FileControl.WriteFile(outputPath, instr);
                    MipsSimulator.Tools.FileControl.WriteFile(outputPath, pcstr);
                }
            }
        }

        static public void StepInto()
        {
            if (!IFRun())
            {
                point = 0;
                Form1.isBreak = false;
                Form1.isStep2 = false;
                return;
            }
            else
            {
                mIFStage.Start();
                mDEStage.Start();
                mEXEStage.Start();
                mMEMStage.Start();
                mWBStage.Start();
            }
        }

        static public void BreakPoint()
        {
            while (true)
            {
                if (!IFRun())
                {
                    point = 0;
                    Form1.isBreak = false;
                    Form1.isStep2 = false;
                    return;
                }
                else
                {
                    string strArg1 = Register.GetRegisterValue("pc");
                    int PC = (Int32)CommonTool.StrToNum(TypeCode.Int32, strArg1, 16);
                    int index = (PC - RunTimeCode.CodeStartAddress) / 4;
                    if (Form1.breakpoints.Count > 0 && point < Form1.breakpoints.Count)
                    {
                        if (Form1.breakpoints.Contains(index))
                        {
                            mIFStage.Start();
                            mDEStage.Start();
                            mEXEStage.Start();
                            mMEMStage.Start();
                            mWBStage.Start();
                           
                            Form1.codeColor((int)cmdMode.lineTable[index], 5);
                            return;
                        }
                    }
                    mIFStage.Start();
                    mDEStage.Start();
                    mEXEStage.Start();
                    mMEMStage.Start();
                    mWBStage.Start();
                }
            }
        }

        // 开启主开关
        static public void Start()
        {
            isTorun = true;
            ThreadFun();
        }

        static public void Initialize()
        {
            //设置开始能否运行
            mIFStage.Initialize();
            mDEStage.Initialize();
            mEXEStage.Initialize();
            mMEMStage.Initialize();
            mWBStage.Initialize();
        }

        // 判断流水线是否运行
        static public bool IFRun()
        {
            return (mIFStage.enableRun >= 0 || mDEStage.enableRun >= 0 || mEXEStage.enableRun >= 0 || mMEMStage.enableRun >= 0 || mWBStage.enableRun >= 0);
        }

        static private bool IsEnd()
        {
            return (mIFStage.isEnd && mDEStage.isEnd && mEXEStage.isEnd && mMEMStage.isEnd && mWBStage.isEnd);
        }

        // 关闭主开关
        static public void Close()
        {
            isTorun = false;
        }
    }
}
