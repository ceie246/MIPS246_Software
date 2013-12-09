using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ScoreList : System.Web.UI.Page
{
    public string scoreInfo;

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
        foreach (User user in userList)
        {
            if (user.StudentID == "91225" || user.StudentID == "246246")
            {
                continue;
            }

            StudentScore score = MIPS246ScoreManager.QueryUserScore(user.StudentID);
            sb.Append("<tr>");
            sb.Append("<td>" + user.StudentID + "</td>");
            sb.Append("<td>" + user.Name + "</td>");
            sb.Append("<td>" + FormatScore(score.Score1) + "</td>");
            sb.Append("<td>" + FormatScore(score.Score2) + "</td>");
            sb.Append("<td>" + FormatScore(score.Score3) + "</td>");
            sb.Append("<td>" + FormatScore(score.Score4) + "</td>");
            sb.Append("<td>" + FormatScore(score.Score5) + "</td>");
            sb.Append("<td>" + FormatScore(score.Score6) + "</td>");
            sb.Append("<td>" + FormatScore(score.Score7) + "</td>");
            sb.Append("<td>" + FormatScore(score.Score8) + "</td>");
            sb.Append("<td>" + FormatScore(score.Score9) + "</td>");
            sb.Append("<td>" + FormatScore(score.Score10) + "</td>");
            sb.Append("<td><a class=\"btn btn-info\" href=\"UpdateScore.aspx?studentID=" + user.StudentID + "\">更新</a>");
            sb.Append("</tr>");
        }

        this.scoreInfo = sb.ToString();
    }

    private string FormatScore(string score)
    {
        if (score == "未提交")
        {
            return "<button class=\"btn-mini btn-info\">" + score + "</button>";
        }
        else if (score == "未评分")
        {
            return "<button class=\"btn-mini btn-warning\">" + score + "</button>";
        }
        else
        {
            return "<button class=\"btn-mini btn-success\">" + score + "</button>";
        }
    }
}