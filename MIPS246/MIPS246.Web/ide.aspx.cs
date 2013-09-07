using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ide : System.Web.UI.Page

{
    public string ideTable;
    private const string bookpath = "C:\\MIPS246_Software\\MIPS246\\MIPS246.Web\\file\\ide\\";

    protected void Page_Load(object sender, EventArgs e)
    {
        

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("<table class=\"table table-hover\">");

        sb.AppendLine("<tr><th width=\"50%\">文件名</th><th width=\"20%\">大小</th><th width=\"20%\">修改日期</th><th></th></tr>");

        DirectoryInfo directoryInfo = new DirectoryInfo(bookpath);

        foreach (FileInfo fileinfo in directoryInfo.GetFiles())
        {
            sb.AppendLine("<tr><td><a href=\"./file/ide/" + fileinfo.Name + "\">" + fileinfo.Name + "</a></td><td>" + fileinfo.Length / 1024 + " KB</td><td>" + fileinfo.LastAccessTime + "</td><td><a class=\"btn btn-primary\" href=\"./file/ide/" + fileinfo.Name + "\">下载</td></tr>");
        }

        sb.AppendLine("</table>");
        this.ideTable = sb.ToString();
    }
}