using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using MIPS246.Core.Assembler;
using System.Windows.Forms;
using System.IO;

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
    public static void SaveTargetCode(string sourceCode, string displayFormat,string hasAddress, string outputFormat)
    {
        string output = "";
        MIPS246.Core.Assembler.Assembler assembler = new MIPS246.Core.Assembler.Assembler(sourceCode);

        if (assembler.DoAssemble() == true)
        {
            bool[] boolArray = new bool[32];
            for (int i = 0; i < assembler.CodeList.Count; i++)
            {
                assembler.CodeList[i].Machine_Code.CopyTo(boolArray, 0);
                if (displayFormat == "BIN")
                {
                    if (hasAddress == "checked")
                    {
                        output += "0x" + String.Format("{0:X8}", assembler.CodeList[i].Address) + ":\t";
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
                        output += "0x" + String.Format("{0:X8}", assembler.CodeList[i].Address) + ":\t";
                    }
                    output += FormatHex(boolArray);
                    output += "\n";
                }
            }
        }
        else
        {
            output = assembler.Error.ToString();
        }

        SaveFileDialog saveFileDialog = new SaveFileDialog();
        if(outputFormat=="TXT")
        {
            saveFileDialog.Filter = "Text file (*.txt)|*.txt";
            saveFileDialog.FileName = "";
        }
        else
        {
            saveFileDialog.Filter = "Coe File (*.coe)|*.coe";
            saveFileDialog.FileName = "";
        }
        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            string fileName = saveFileDialog.FileName;
        }
        
    }
}