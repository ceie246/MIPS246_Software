using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class MIPS246Book : System.Web.UI.Page
{
    public string bookTable;
    private const string bookpath="C:\\MIPS246_Software\\MIPS246\\MIPS246.Web\\file\\MIPS246Book\\";
    protected void Page_Load(object sender, EventArgs e)
    {
        StringBuilder sb=new StringBuilder();
        sb.AppendLine("<table class=\"table table-hover\">");

        sb.AppendLine("<tr><th width=\"50%\">文件名</th><th width=\"20%\">大小</th><th width=\"20%\">修改日期</th><th></th></tr>");

        DirectoryInfo directoryInfo = new DirectoryInfo(bookpath);

        foreach (FileInfo fileinfo in directoryInfo.GetFiles())
        {
            sb.AppendLine("<tr><td><a href=\"./file/MIPS246Book/" + fileinfo.Name + "\">" + fileinfo.Name + "</a></td><td>" + fileinfo.Length / 1024 + " KB</td><td>" + fileinfo.LastAccessTime + "</td><td><a class=\"btn btn-primary\" href=\"./file/MIPS246Book/" + fileinfo.Name + "\">下载</td></tr>");
        }

        sb.AppendLine("</table>");
        this.bookTable = sb.ToString();
    }
}