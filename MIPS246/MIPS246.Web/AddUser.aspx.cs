using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddUser : System.Web.UI.Page
{
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
    protected void AddButton_Click(object sender, EventArgs e)
    {
        User user = new User(studentIDBox.Text, boardBox.Text, nameBox.Text, majorBox.Text, MaleRadioButton.Checked==true?"男":"女");
        MIPS246UserManager.AddUser(user);

        Response.Redirect("StudentList.aspx");
    }
}