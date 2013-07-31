using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.IO;
using MIPS246.Core.Assembler;


public partial class AssemblerRequest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        Response.Write(Request.Form["sourcecode"]);
    }

     private static string FormatHex(bool[] boolArray)
     {
         string Hexstr="0x";
         for (int i = 0; i < 8; i++)
         {
             int tempi = 0;
             tempi += 8 * Convert.ToInt32(boolArray[i * 4]) + 4 * Convert.ToInt32(boolArray[i * 4 + 1]) + 2 * Convert.ToInt32(boolArray[i * 4 + 2]) + Convert.ToInt32(boolArray[i * 4 + 3]);
             if (tempi >= 0 && tempi < 10)
             {
                 Hexstr += tempi.ToString();
             }
             else
             {
                 if (tempi == 10)
                 {
                     Hexstr += "a";
                 }
                 else if (tempi == 11)
                 {
                     Hexstr += "b";
                 }
                 else if (tempi == 12)
                 {
                     Hexstr += "c";
                 }
                 else if (tempi == 13)
                 {
                     Hexstr += "d";
                 }
                 else if (tempi == 14)
                 {
                     Hexstr += "e";
                 }
                 else if (tempi == 15)
                 {
                     Hexstr += "f";
                 }
             }
         }
         return Hexstr;
     }

    [WebMethod]
    public static string Assemble(string sourceCode, string displayFormat,string hasAddress)
    {
        string output="";
        MIPS246.Core.Assembler.Assembler assembler = new MIPS246.Core.Assembler.Assembler(sourceCode);
        if (assembler.DoAssemble() == true)
        {
            bool[] boolArray = new bool[32];
            for (int i = 0; i < assembler.CodeList.Count; i++)
            {
                assembler.CodeList[i].Machine_Code.CopyTo(boolArray,0);
                if (displayFormat == "BIN")
                {
                    if (hasAddress == "checked")
                    {
                        output+="0x" + String.Format("{0:X8}", assembler.CodeList[i].Address) + ":\t";
                    }
                    for (int j = 0; j < 32; j++)
                    {
                        if (boolArray[j] == true)
                        {
                            output += "1";
                        }
                        else
                        {
                            output += "0";
                        }
                    }
                    output += "\n";
                }
                else
                {
                    if (hasAddress == "checked")
                    {
                        output+="0x" + String.Format("{0:X8}", assembler.CodeList[i].Address) + ":\t";
                    }
                    output+=FormatHex(boolArray);
                    output += "\n";
                }
            }      
        }
        else
        {
            output= assembler.Error.ToString();
        }
        return output;
    }

    [WebMethod]
    public static string SaveTargetCode(string sourceCode, string displayFormat, string outputFormat)
    {
        string output = "";
        string fileName = Guid.NewGuid().ToString();
        bool isHEX;
        MIPS246.Core.Assembler.Assembler assembler = new MIPS246.Core.Assembler.Assembler(sourceCode);
        if(outputFormat=="BIN")
        {
            isHEX=false;
        }
        else
        {
            isHEX=true;
        }

        if (assembler.DoAssemble() == true)
        {
            
            if (outputFormat == "TXT")
            {
                fileName += ".txt";
                assembler.Output(false, HttpContext.Current.Server.MapPath("~") + "/AssemblerOutput/" + fileName, isHEX);
            }
            else
            {
                fileName += ".coe";
                assembler.Output(true, HttpContext.Current.Server.MapPath("~") + "/AssemblerOutput/" + fileName, isHEX);
            }
        }
        else
        {
            output = assembler.Error.ToString();
        }

        return fileName;        
    }

    [WebMethod]
    public static string SaveSourceCode(string sourceCode)
    {
        string fileName = Guid.NewGuid().ToString() + ".txt";
        StreamWriter sr = new StreamWriter(HttpContext.Current.Server.MapPath("~") + "/AssemblerOutput/" + fileName);
        sr.Write(sourceCode);
        sr.Close();
        return fileName;
    }
}