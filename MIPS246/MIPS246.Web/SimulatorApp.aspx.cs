using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Simulator : System.Web.UI.Page
{
    public string DownloadUrl;
    public string Version;
    public string UpdateTime;
    public string ManualUrl;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["LoginStatus"] == null)
            {
                Response.Redirect("Signin.aspx");
            }

        }
        this.DownloadUrl = "./file/Simulator/" + System.Configuration.ConfigurationManager.AppSettings["SimulatorFileName"];
        this.ManualUrl = "./file/Simulator/" + System.Configuration.ConfigurationManager.AppSettings["SimulatorManualName"];
        this.Version = System.Configuration.ConfigurationManager.AppSettings["SimulatorVersion"];

        DirectoryInfo directoryInfo = new DirectoryInfo(System.Configuration.ConfigurationManager.AppSettings["SimulatorLocalPath"]);

        foreach (FileInfo fileinfo in directoryInfo.GetFiles())
        {
            if (fileinfo.Name == ConfigurationManager.AppSettings["SimulatorFileName"])
            {
                this.UpdateTime = fileinfo.LastWriteTime.ToString();
            }
        }

    }
}