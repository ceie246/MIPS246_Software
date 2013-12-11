using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddFile : System.Web.UI.Page
{
    public static string referencePath = "C:\\MIPS246_Software\\MIPS246\\MIPS246.Web\\file\\reference\\";
    public static string pptPath="C:\\MIPS246_Software\\MIPS246\\MIPS246.Web\\file\\ppT\\";
    public static string idePath="C:\\MIPS246_Software\\MIPS246\\MIPS246.Web\\file\\ide\\";
    public static string mips246bookPath="C:\\MIPS246_Software\\MIPS246\\MIPS246.Web\\file\\mips246book\\";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["LoginStatus"] == null)
            {
                Response.Redirect("Signin.aspx");
            }

            if (Session["LoginId"].ToString() != "246246" && Session["LoginId"].ToString() != "91225")
            {
                Response.Redirect("default.aspx");
            }
        }
    }

    protected void UploadButton_Click(object sender, EventArgs e)
    {
        string uploadPath = string.Empty;


        switch (Request["type"].ToString())
        {
            case "reference":
                uploadPath = referencePath;
                break;
            case "ide":
                uploadPath = idePath;
                break;
            case "ppt":
                uploadPath = pptPath;
                break;
            case "mips246book":
                uploadPath = mips246bookPath;
                break;
            default:
                break;
        }

        if (FileUploader.HasFile)
        {
            FileUploader.PostedFile.SaveAs(uploadPath + FileUploader.FileName);
            Response.Write("<script language=javascript>alert(\"修改成功\");</script>");
            switch (Request["type"].ToString())
            {
                case "reference":
                    Response.Redirect("reference.aspx");
                    break;
                case "ide":
                    Response.Redirect("ide.aspx");
                    break;
                case "ppt":
                    Response.Redirect("ppt.aspx");
                    break;
                case "mips246book":
                    Response.Redirect("mips246book.aspx");
                    break;
                default:
                    break;
            }
        }


    }
}