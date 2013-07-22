using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MipsSimulator.Assembler;
using MipsSimulator.Devices;
using MipsSimulator.Tools;

namespace MipsSimulator.Monocycle
{
    class mMasterSwitch
    {
        static private bool isTorun = false;
        static private bool isAdd = false;
        static private int indexBreak = 0;

        static private void ThreadFun()
        {
            //初始化
            Initialize();
            //判断是否要继续
            while (isTorun)
            {
                if (!IFRun())
                {
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
            mIFStage.Start();
            mDEStage.Start();
            mEXEStage.Start();
            mMEMStage.Start();
            mWBStage.Start();
        }

        static public void BreakPoint()
        {
            while (true)
            {
                if (!IFRun())
                {

                    break;
                }
                else
                {
                    if (isAdd)
                    {
                        Form1.breakpoints.Add(indexBreak);
                        isAdd = false;
                    }
                    string strArg1 = Register.GetRegisterValue("pc");
                    int PC = (Int32)CommonTool.StrToNum(TypeCode.Int32, strArg1, 16);
                    int index = (PC - RunTimeCode.CodeStartAddress) / 4;
                    if (Form1.breakpoints.Count > 0)
                    {
                        if (Form1.breakpoints.ElementAt(0) == index)
                        {
                            Form1.breakpoints.Remove(index);
                            isAdd = true;
                            indexBreak = index;
                            Form1.codeColor(index, 1);
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
        static private bool IFRun()
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
