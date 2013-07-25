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
    class mIFStage
    {
        static public int enableRun = 0;

        static public bool isEnd = false;

        static public Int32 PC = 0;
        static public Int32 NPC = 0;

        static private Code code;

        static public bool ifOverOrNop = false;

        static public void Initialize()
        {
            mIFStage.enableRun = 0;
            mIFStage.PC = RunTimeCode.CodeStartAddress;
        }

        static public void Start()
        {
            //开始标志
            isEnd = false;
          
            ifOverOrNop = false;

            ThreadFun();
        }

        static private void ThreadFun()
        {
            if (enableRun < 0)
            {
                isEnd = true;
                return;
            }
            string strArg1 = Register.GetRegisterValue("pc");
            PC = (Int32)CommonTool.StrToNum(TypeCode.Int32, strArg1, 16);
            //获取指令
            code = RunTimeCode.GetCode(PC);
            
            //设置NPC
            switch (code.codeType)
            {
                case CodeType.OVER:
                    {
                        enableRun = -1;
                        NPC = PC;
                        ifOverOrNop = true;
                        break;
                    }
                case CodeType.NOP:
                    {
                        PC = PC + 4;
                        NPC = PC;
                        ifOverOrNop = true;
                        break;
                    }
                case CodeType.ERR:
                    {
                        Form1.Message(code.codeStr);
                        RunTimeCode.codeList.Clear();
                        break;
                    }
                default:
                    {
                        PC = PC + 4;
                        NPC = PC;
                        mDEStage.code =mIFStage.code;
                        mDEStage.args = new object[1] { NPC };
                        mDEStage.enableRun++;
                        break;
                    }
            }
            if (MipsSimulator.Program.mode==1)
            {
                if (code.codeType != CodeType.OVER)
                {
                    int colorIndex = (int)cmdMode.lineTable[mIFStage.code.Index];
                    Form1.codeColor(colorIndex, 1);
                }  
            }
            
            isEnd = true;
            return;
        }

    }
}
