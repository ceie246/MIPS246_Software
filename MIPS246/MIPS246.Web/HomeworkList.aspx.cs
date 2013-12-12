using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HomeworkList : System.Web.UI.Page
{
    public string homeworkinfo;

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

        List<HomeworkRecord> homeworkList = MIPS246HomerworkManager.QueryAllHomeworkRecord();

        StringBuilder sb = new StringBuilder();
        foreach (HomeworkRecord homeworkRecord in homeworkList)
        {
            if (homeworkRecord.StudentID == "91225" || homeworkRecord.StudentID == "246246")
            {
                continue;
            }

            User user = MIPS246UserManager.QueryUser(homeworkRecord.StudentID);
            sb.Append("<tr>");
            sb.Append("<td>" + homeworkRecord.StudentID + "</td>");
            sb.Append("<td>" + user.Name + "</td>");
            sb.Append("<td>" + FormatHomework(homeworkRecord.Homework1, user.StudentID, 1) + "</td>");
            sb.Append("<td>" + FormatHomework(homeworkRecord.Homework2, user.StudentID, 2) + "</td>");
            sb.Append("<td>" + FormatHomework(homeworkRecord.Homework3, user.StudentID, 3) + "</td>");
            sb.Append("<td>" + FormatHomework(homeworkRecord.Homework4, user.StudentID, 4) + "</td>");
            sb.Append("<td>" + FormatHomework(homeworkRecord.Homework5, user.StudentID, 5) + "</td>");
            sb.Append("<td>" + FormatHomework(homeworkRecord.Homework6, user.StudentID, 6) + "</td>");
            sb.Append("<td>" + FormatHomework(homeworkRecord.Homework7, user.StudentID, 7) + "</td>");
            sb.Append("<td>" + FormatHomework(homeworkRecord.Homework8, user.StudentID, 8) + "</td>");
            sb.Append("<td>" + FormatHomework(homeworkRecord.Homework9, user.StudentID, 9) + "</td>");
            sb.Append("<td>" + FormatHomework(homeworkRecord.Homework10, user.StudentID, 10) + "</td>");
            sb.Append("</tr>");
        }

        this.homeworkinfo = sb.ToString();
    }

    private string FormatHomework(string score, string studentID, int index)
    {
        if (score == "0")
        {
            return "<a class=\"btn btn-danger btn-small\" href=\"#\">未提交</a>";
        }
        else
        {
            return "<a class=\"btn btn-success  btn-small\" href=\"#\">已提交</a>";
        }
    }
}