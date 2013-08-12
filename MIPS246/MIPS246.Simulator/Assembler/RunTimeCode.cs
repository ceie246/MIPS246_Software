using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace MipsSimulator.Assembler
{
    class RunTimeCode
    {
        static public List<Code> codeList = new List<Code>();//指令序列

        static public DataTable CodeT;//指令表

        static private Int32 codeStartAddress = 0;//起始地址
        static private Int32 codeIndex = 0;

        public static Int32 CodeIndex
        {
            get { return RunTimeCode.codeIndex; }
            set { RunTimeCode.codeIndex = value; }
        }
        public static Int32 CodeStartAddress
        {
            get { return RunTimeCode.codeStartAddress; }
            set { RunTimeCode.codeStartAddress = value; }
        }

        static public void CodeTInitial()
        {
            codeIndex = 0;
            CodeT = new DataTable();
            CodeT.Columns.Add("Address");
            CodeT.Columns.Add("Code");
            CodeT.Columns.Add("Source");
        }

        static public void Add(Code code)//添加单条指令
        {
            //code.Index = codeIndex;
            //codeIndex++;
            //codeList.Add(code);

            DataRow dr = CodeT.NewRow();
            string address = "0x" + code.address.ToString("X8");
            dr["Address"] =address;
            dr["Code"] = code.machineCode;
            dr["Source"] = code.codeStr;
            CodeT.Rows.Add(dr);

        }

        static public void Add(Code[] codes)//添加指令段
        {

            for (int i = 0; i < codes.Length; i++)
            {
                DataRow dr = CodeT.NewRow();
                string address = "0x" + codes[i].address.ToString("X8");
                dr["Address"] = address;
                dr["Code"] = codes[i].machineCode;
                dr["Source"] = codes[i].codeStr;
                CodeT.Rows.Add(dr);
                codes[i].Index = codeIndex;
                codeIndex++;
            }
            // codeList.AddRange(codes);
        }
        static public void Clear()//清除指令序列
        {
            RunTimeCode.CodeIndex = 0;
            RunTimeCode.CodeStartAddress = 0;
            codeList.Clear();
            CodeT.Rows.Clear();
        }

        static public Code GetCode(Int32 codeCurrentAddress)//获取指令
        {
            if ((codeCurrentAddress - CodeStartAddress) % 4 != 0)
            {
                Code code = new Code();
                code.codeType = CodeType.ERR;
                code.codeStr = "地址未对齐";
                // code.Index = -1;
                return code;
            }
            int index = Convert.ToInt32((codeCurrentAddress - CodeStartAddress) / 4);
            if (index < 0 || index >= codeList.Count)
            {
                Code code = new Code();
                code.codeType = CodeType.OVER;
                // code.Index = -1;
                return code;
            }
            return codeList[index];
        }

    }
}
