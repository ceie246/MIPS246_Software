using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using MIPS246.Core.TestCodeGeneator;
using MIPS246.Core.DataStructure;
using System.Text;

public partial class TestCodeGeneatorRequest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    [WebMethod]
    public static string GeneatorCode(string num)
    {
        List<Mnemonic> MnemonicList = new List<Mnemonic>();
        foreach(Mnemonic mnemonic in Enum.GetValues(typeof(Mnemonic)))
        {
            if (mnemonic ==mnemonic.ADD ||mnemonic ==mnemonic.ADDU ||mnemonic ==mnemonic.SUB || mnemonic ==mnemonic.SUBU ||mnemonic==mnemonic.AND ||mnemonic ==mnemonic.OR ||mnemonic ==mnemonic.XOR ||mnemonic ==mnemonic.NOR ||mnemonic ==mnemonic.SLT ||mnemonic ==mnemonic.SLTU ||mnemonic ==mnemonic.SLL ||mnemonic ==mnemonic.SRL ||mnemonic ==mnemonic.SRA ||mnemonic ==mnemonic.SLLV ||mnemonic ==mnemonic.SRLV ||mnemonic ==mnemonic.SRAV ||mnemonic ==mnemonic.ADDI ||mnemonic ==mnemonic.ADDIU ||mnemonic ==mnemonic.ANDI ||mnemonic ==mnemonic.ORI ||mnemonic ==mnemonic.XORI ||mnemonic ==mnemonic.LUI ||mnemonic ==mnemonic.LW ||mnemonic ==mnemonic.SW ||mnemonic ==mnemonic.SLTI ||mnemonic ==mnemonic.SLTIU)
            {
                MnemonicList.Add(mnemonic);
            }
            
        }
        TestCodeGeneator.ConfigGeneator(int.Parse(num), MnemonicList);
        TestCodeGeneator.Generate();
        StringBuilder stringbuilder = new StringBuilder();
        foreach (Instruction instruction in TestCodeGeneator.CodeList)
        {
            stringbuilder.AppendLine(instruction.ToString());
        }

        return stringbuilder.ToString();
    }
}