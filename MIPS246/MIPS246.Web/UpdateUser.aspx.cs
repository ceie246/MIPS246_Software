using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UpdateUser : System.Web.UI.Page
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

            User user = MIPS246UserManager.QueryUser(Request["StudentID"].ToString());

            nameBox.Text = user.Name;
            studentIDBox.Text = user.StudentID;
            boardBox.Text = user.BoardID;
            majorBox.Text = user.Major;

            if (user.Sex == "男")
            {
                MaleRadioButton.Checked = true;
            }
            else
            {
                FemaleRadioButton.Checked = true;
            }
        }

       

        
    }
    protected void UpdateButton_Click(object sender, EventArgs e)
    {
        if(PasswordBox.Text==string.Empty)
        {
            MIPS246UserManager.UpdateUser(studentIDBox.Text, nameBox.Text, MaleRadioButton.Checked==true?"男":"女" ,majorBox.Text, boardBox.Text, string.Empty);
        }
        else
        {
            MIPS246UserManager.UpdateUser(studentIDBox.Text, nameBox.Text, MaleRadioButton.Checked == true ? "男" : "女", majorBox.Text, boardBox.Text, PasswordBox.Text);
        }

        Response.Redirect("StudentList.aspx");
    }
}