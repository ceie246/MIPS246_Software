using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class question : System.Web.UI.Page
{
    public string questionList;
    private const string questionpath = "C:\\MIPS246_Software\\MIPS246\\MIPS246.Web\\file\\Questions\\";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["LoginStatus"] == null)
            {
                Response.Redirect("Signin.aspx");
            }
        }

        StringBuilder sb = new StringBuilder();

        DirectoryInfo directoryInfo = new DirectoryInfo(questionpath);

        foreach (DirectoryInfo di in directoryInfo.GetDirectories())
        {
            sb.AppendLine("<table class=\"table table-hover\">");

            sb.AppendLine("<tr><th>"+di.Name+"</th></tr>");
            for (int i = 0; i < di.GetFiles().Count(); i++)
            {
                sb.AppendLine("<tr class=\"success\"><td>Q:<a href=\"./file/questions/" + di.Name + "/" + di.GetFiles()[i].Name + "\">" + di.GetFiles()[i].Name.Substring(0, di.GetFiles()[i].Name.IndexOf(".")) + "</td></tr>");
            }
            sb.AppendLine("</table><br />");
        }
        this.questionList = sb.ToString();
    }
}