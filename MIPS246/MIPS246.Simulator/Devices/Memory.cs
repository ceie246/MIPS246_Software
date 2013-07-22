using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using MipsSimulator.Tools;

namespace MipsSimulator.Devices
{
    class Memory
    {
        static public DataTable Mem;
        static public Int32 size =1024 * 1024;
        static public void MemInitialize()
        {
            Mem = new DataTable();
            Mem.Columns.Add("Address");
            Mem.Columns.Add("Value(+00)");
            Mem.Columns.Add("Value(+04)");
            Mem.Columns.Add("Value(+08)");
            Mem.Columns.Add("Value(+0c)");
            Mem.Columns.Add("Value(+10)");
            Mem.Columns.Add("Value(+14)");
            Mem.Columns.Add("Value(+18)");
            Mem.Columns.Add("Value(+1c)");

            for (int i = 0; i < size; )
            {
                DataRow dr = Mem.NewRow();
                string[] s = new string[9];
                s[0] = "0x"+((Int32)i).ToString("X8");
                s[1] = "0x00000000";
                s[2] = "0x00000000";
                s[3] = "0x00000000";
                s[4] = "0x00000000";
                s[5] = "0x00000000";
                s[6] = "0x00000000";
                s[7] = "0x00000000";
                s[8] = "0x00000000";
                dr["Address"] = s[0];
                dr["Value(+00)"] = s[1];
                dr["Value(+04)"] = s[2];
                dr["Value(+08)"] = s[3];
                dr["Value(+0c)"] = s[4];
                dr["Value(+10)"] = s[5];
                dr["Value(+14)"] = s[6];
                dr["Value(+18)"] = s[7];
                dr["Value(+1c)"] = s[8];
                Mem.Rows.Add(dr);
                i = i + 32;
            }
        }

        static public bool getMemory(Int32 address,ref Int32 value)//address%4==0||address%2==0
        {
            Int32 row=address/32;//行数从0算起
           
            Int32 column=(address%32)/4;
           
            string valueStr32 = Mem.Rows[row][column+1].ToString();
             string valueStr16=valueStr32.Substring(2,4);
             if (address % 4 == 0)
             {
                 value= (Int32)CommonTool.StrToNum(TypeCode.Int32, valueStr32, 16);
                 return true;
             }
             else
                 if (address % 2 == 0)
                 {
                     value= (Int32)CommonTool.StrToNum(TypeCode.Int32, valueStr16, 16);
                     return true;
                 }
                 else
                     return false;
        }

        static public bool setMemory(Int32 address, Int32 value)//address%4==0||address%2==0
        {
            Int32 row = address / 32;//行数从0算起

            Int32 column = (address % 32) / 4;
            string ValueStr = "";
            try
            {
                if (address % 4 == 0)
                {
                    ValueStr = ((Int32)value).ToString("X8");
                    Mem.Rows[row][column + 1] = "0x"+ ValueStr;
                    return true;
                }
                else
                    if (address % 2 == 0)
                    {
                        ValueStr = ((Int16)value).ToString("X4");
                        string valueStr32 = Mem.Rows[row][column + 1].ToString();
                        Mem.Rows[row][column + 1] = valueStr32.Substring(0, 6) + ValueStr;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("内存地址未对齐");
                        return false;
                    }

            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.ToString());
               
                return false;
            }
        }

    }
}
