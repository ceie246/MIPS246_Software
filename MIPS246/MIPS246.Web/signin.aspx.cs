using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class signin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void signinButton_Click(object sender, EventArgs e)
    {
        string studentid = this.studentIDBox.Text;
        string password = this.passwordBox.Text;
        User user=MIPS246UserManager.CheckUser(studentid, password);
        if (user!=null)
        {
            Session["LoginStatus"] = "true";
            Session["LoginId"] = user.StudentID;
            Response.Redirect("Default.aspx");
        }
        else
        {
            this.warningLabel.Visible = true;
        }
    }
    protected void passwordBox_TextChanged(object sender, EventArgs e)
    {
        this.warningLabel.Visible = false;
    }
    protected void studentIDBox_TextChanged(object sender, EventArgs e)
    {
        this.warningLabel.Visible = false;
    }
}