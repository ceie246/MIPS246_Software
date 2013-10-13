using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserInfo : System.Web.UI.Page
{
    public string StudentId;

    public string BoardId;

    public string LastLoginTime;

    public string Name;

    public string Sex;

    public string Major;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["LoginStatus"] == null)
            {
                Response.Redirect("Signin.aspx");
            }
        }

        this.StudentId = Session["LoginId"].ToString();

        User user = MIPS246UserManager.QueryUser(this.StudentId);

        this.BoardId = user.BoardID;

        this.LastLoginTime = user.LastLoginTime.ToString();

        this.Major = user.Major;
        
        this.Name = user.Name;

        this.Sex = user.Sex;



    }
}