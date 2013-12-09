using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class studentList : System.Web.UI.Page
{
    public string studentInfo;

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

        List<User> userList = MIPS246UserManager.QueryAllUser();

        StringBuilder sb = new StringBuilder();
        foreach(User user in userList)
        {
            if(user.StudentID=="91225"||user.StudentID=="246246")
            {
                continue;
            }
            sb.Append("<tr>");
            sb.Append("<td>" + user.StudentID + "</td>");
            sb.Append("<td>" + user.Name + "</td>");
            sb.Append("<td>" + user.Sex + "</td>");
            sb.Append("<td>" + user.Major + "</td>");
            sb.Append("<td>" + user.BoardID + "</td>");
            sb.Append("<td><a class=\"btn btn-info\" href=\"UpdateUser.aspx?studentID=" + user.StudentID + "\">更新</a> <a class=\"btn btn-danger\" href=\"DeleteUserRequest.aspx?studentID=" + user.StudentID + "\">删除</a></td>");
            sb.Append("</tr>");
        }

        this.studentInfo = sb.ToString();
    }
}