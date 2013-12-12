using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class submitHomework : System.Web.UI.Page
{
    public string homeworkStatus;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["LoginStatus"] == null)
            {
                Response.Redirect("Signin.aspx");
            }
        }

        User user = MIPS246UserManager.QueryUser(Session["LoginId"].ToString());
        HomeworkRecord hr = MIPS246HomerworkManager.QueryUserHomeworkRecord(user.StudentID);



        StringBuilder sb = new StringBuilder();

        sb.AppendLine("<tr>");
        sb.Append("<td>" + hr.StudentID + "</td>");
        sb.Append("<td>" + user.Name + "</td>");
        sb.Append("<td>" + FormatHomework(hr.Homework1, user.StudentID, 1) + "</td>");
        sb.Append("<td>" + FormatHomework(hr.Homework2, user.StudentID, 2) + "</td>");
        sb.Append("<td>" + FormatHomework(hr.Homework3, user.StudentID, 3) + "</td>");
        sb.Append("<td>" + FormatHomework(hr.Homework4, user.StudentID, 4) + "</td>");
        sb.Append("<td>" + FormatHomework(hr.Homework5, user.StudentID, 5) + "</td>");
        sb.Append("<td>" + FormatHomework(hr.Homework6, user.StudentID, 6) + "</td>");
        sb.Append("<td>" + FormatHomework(hr.Homework7, user.StudentID, 7) + "</td>");
        sb.Append("<td>" + FormatHomework(hr.Homework8, user.StudentID, 8) + "</td>");
        sb.Append("<td>" + FormatHomework(hr.Homework9, user.StudentID, 9) + "</td>");
        sb.Append("<td>" + FormatHomework(hr.Homework10, user.StudentID, 10) + "</td>");
        sb.AppendLine("</tr>");

        this.homeworkStatus = sb.ToString();
    }

    private string FormatHomework(string score, string studentID, int index)
    {
        if (score == "0")
        {
            return "<a class=\"btn btn-danger btn-small\" href=\"uploadHomework.aspx?index=" + index + "\">未提交</a>";
        }
        else
        {
            return "<a class=\"btn btn-success  btn-small\" href=\"#\">已提交</a>";
        }
    }
}