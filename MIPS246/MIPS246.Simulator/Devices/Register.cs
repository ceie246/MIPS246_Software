using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MipsSimulator.Tools;

namespace MipsSimulator.Devices
{
     public class Register
    {
        static public  DataTable Res;

        static public void ResInitialize()
        {
            Res = new DataTable();
            Res.Columns.Add("Name");
            Res.Columns.Add("Value");
            for (int i = 0; i < 32; i++)
            {
                DataRow dr = Res.NewRow();
                string[] s = new string[2];
                s[0] = "$" + i.ToString(); 
                s[1] = "0x00000000";
                dr["Name"] = s[0];
                dr["Value"] = s[1];
                Res.Rows.Add(dr);
            }

            DataRow drPC = Res.NewRow();
            string[] pcStr = new string[2];
            pcStr[0] = "pc";
            pcStr[1] = "0x00000000";
            drPC["Name"] = pcStr[0];
            drPC["Value"] = pcStr[1];
            Res.Rows.Add(drPC);

            DataRow drHI = Res.NewRow();
            string[] hiStr = new string[2];
            hiStr[0] = "hi";
            hiStr[1] = "0x00000000";
            drHI["Name"] = hiStr[0];
            drHI["Value"] = hiStr[1];
            Res.Rows.Add(drHI);

            DataRow drLO = Res.NewRow();
            string[] loStr = new string[2];
            loStr[0] = "lo";
            loStr[1] = "0x00000000";
            drLO["Name"] = loStr[0];
            drLO["Value"] = loStr[1];
            Res.Rows.Add(drLO);
            
        }

        static public void Clear()
        {
            for (int i = 0; i < 35; i++)
            {
                Register.Res.Rows[i]["Value"] = "0x00000000";
            }
        }

        // 判断寄存器是否存在
        static public  bool IfExsit(String registerName)
        {
            DataRow[] register = Res.Select("Name='" + registerName + "'");
            if (register.Length<=0)
                return false;
            else return true;
        }

        // 寄存器to机器码
        static public string toMachineCode(String registerName)
        {
            if (IfExsit(registerName))
            {
                string numStr=registerName.Trim('$');
                int numInt = Convert.ToInt32(numStr, 10);
                string regMachineCode = CommonTool.decToBin(numInt,5);
                return regMachineCode;
            }
            else
                return null;
        }

        public static void setPC(Int32 addressPC)
        {
            string pcValue = "0x"+addressPC.ToString("X8");
            Res.Rows[32]["Value"] = pcValue;  
        }

        public static string GetRegisterValue(string registerName)
        {
            DataRow[] register = Res.Select("Name='" + registerName + "'");
            if (register.Length < 0)
                return null;
            else
                return (string)register[0]["Value"];
        }
        public static bool SetRegisterValue(string registerName,Int32 value)
        {
            DataRow[] register = Res.Select("Name='" + registerName + "'");
            if (register.Length < 0)
                return false;
            else
            {
                string numStr=registerName.Trim('$');
                int numInt = Convert.ToInt32(numStr, 10);
                Res.Rows[numInt]["Value"] = "0x" + value.ToString("X8");
                return true;
            }
        }
        public static bool SetRegisterValue(string registerName, UInt32 value)
        {
            DataRow[] register = Res.Select("Name='" + registerName + "'");
            if (register.Length < 0)
                return false;
            else
            {
                string numStr = registerName.Trim('$');
                int numInt = Convert.ToInt32(numStr, 10);
                Res.Rows[numInt]["Value"] = "0x" + value.ToString("X8");
                return true;
            }
        }
    }
}
