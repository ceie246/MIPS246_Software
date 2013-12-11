using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AssemblerApp : System.Web.UI.Page
{
    public string DownloadUrl;
    public string Version;
    public string UpdateTime;
    public string ManualUrl;
    public string update;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["LoginStatus"] == null)
            {
                Response.Redirect("Signin.aspx");
            }

            if (Session["LoginId"].ToString() == "246246" || Session["LoginId"].ToString() == "91225")
            {
                update = "<a class=\"btn btn-info\" href=\"updateAssemberApp.aspx\">更新</a>";
            }
        }

        this.DownloadUrl = "./file/Assembler/" + System.Configuration.ConfigurationManager.AppSettings["AssemblerFileName"];
        this.ManualUrl = "./file/Assembler/" + System.Configuration.ConfigurationManager.AppSettings["AssemblerManualName"];
        this.Version = ConfigurationManager.AppSettings["AssemblerVersion"];

        DirectoryInfo directoryInfo = new DirectoryInfo(System.Configuration.ConfigurationManager.AppSettings["AssemblerLocalPath"]);

        foreach (FileInfo fileinfo in directoryInfo.GetFiles())
        {
            if (fileinfo.Name == ConfigurationManager.AppSettings["AssemblerFileName"])
            {
                this.UpdateTime = fileinfo.LastWriteTime.ToString();
            }
        }
    }
}