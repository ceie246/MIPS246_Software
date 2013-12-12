using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UploadHomework : System.Web.UI.Page
{
    public static string  homework="C:\\MIPS246_Software\\MIPS246\\MIPS246.Web\\file\\homework\\";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["LoginStatus"] == null)
            {
                Response.Redirect("Signin.aspx");
            }
        }
    }


    protected void UploadButton_Click(object sender, EventArgs e)
    {
        User user = MIPS246UserManager.QueryUser(Session["LoginId"].ToString());

        HomeworkRecord homeworkRecord = MIPS246HomerworkManager.QueryUserHomeworkRecord(user.StudentID);

        DirectoryInfo di = new DirectoryInfo(homework);
        DirectoryInfo targetdi =null;

        bool hasUserFolder = false;
        foreach (DirectoryInfo userdi in di.GetDirectories())
        {
            if (userdi.Name == user.StudentID)
            {
                hasUserFolder = true;
                targetdi=userdi;
                break;
            }
        }

        if (hasUserFolder == false)
        {
            targetdi=di.CreateSubdirectory(user.StudentID);
        }

        targetdi=targetdi.CreateSubdirectory(Request["index"].ToString());

        if (FileUploader.HasFile)
        {
            FileUploader.PostedFile.SaveAs(targetdi.FullName + "\\" + FileUploader.FileName);
            MIPS246HomerworkManager.UpdateHomeworkRecord(user.StudentID, int.Parse(Request["index"].ToString()));
            Response.Redirect("submitHomework.aspx");
        }
    }
}