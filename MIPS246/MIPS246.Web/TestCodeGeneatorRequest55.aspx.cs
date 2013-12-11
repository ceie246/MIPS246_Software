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
            if (mnemonic == Mnemonic.ADD || mnemonic == Mnemonic.ADDU || mnemonic == Mnemonic.SUB || mnemonic == Mnemonic.SUBU || mnemonic == Mnemonic.AND || mnemonic == Mnemonic.OR || mnemonic == Mnemonic.XOR || mnemonic == Mnemonic.NOR || mnemonic == Mnemonic.SLT || mnemonic == Mnemonic.SLTU || mnemonic == Mnemonic.SLL || mnemonic == Mnemonic.SRL || mnemonic == Mnemonic.SRA || mnemonic == Mnemonic.SLLV || mnemonic == Mnemonic.SRLV || mnemonic == Mnemonic.SRAV || mnemonic == Mnemonic.ADDI || mnemonic == Mnemonic.ADDIU || mnemonic == Mnemonic.ANDI || mnemonic == Mnemonic.ORI || mnemonic == Mnemonic.XORI || mnemonic == Mnemonic.LUI ||  mnemonic == Mnemonic.SLTI || mnemonic == Mnemonic.SLTIU)
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