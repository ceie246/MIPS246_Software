using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ChangePassword : System.Web.UI.Page
{
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
    protected void ChangeButton_Click(object sender, EventArgs e)
    {
        string studentId = Session["LoginId"].ToString();

        User user = MIPS246UserManager.CheckUser(studentId,this.currentPasswordBox.Text);

        if (user == null)
        {
            Response.Write("<script language=javascript>alert(\"密码错误\");</script>");
        }
        else
        {
            MIPS246UserManager.ChangePassword(user.StudentID, this.PasswordBox.Text);
            Session.Clear();
            Response.Write("<script language=javascript>alert(\"修改成功\");</script>");
            Response.Redirect("Default.aspx");
        }

    }
}