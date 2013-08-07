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
            MnemonicList.Add(mnemonic);
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