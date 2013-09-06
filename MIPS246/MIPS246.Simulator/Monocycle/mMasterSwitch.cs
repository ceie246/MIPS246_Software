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
    class mMasterSwitch
    {
        static private bool isTorun = false;
        static private int point = 0;

        static private void ThreadFun()
        {
            //初始化
            Initialize();
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
                    mIFStage.Start();
                    mDEStage.Start();
                    mEXEStage.Start();
                    mMEMStage.Start();
                    mWBStage.Start();
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
