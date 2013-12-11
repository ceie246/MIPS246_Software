using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UpdateScore : System.Web.UI.Page
{
    public string scoreInfo;
    public string StudentID;
    public string StudentName;
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
            StudentID = user.StudentID;
            StudentName = user.Name;

            StudentScore score = MIPS246ScoreManager.QueryUserScore(user.StudentID);

            if (score.Score1 == "0")
            {
                this.ScoreBox1.Attributes["placeholder"] = "未评分";
            }
            else
            {
                this.ScoreBox1.Text = score.Score1;
            }

            if (score.Score2 == "0")
            {
                this.ScoreBox2.Attributes["placeholder"] = "未评分";
            }
            else
            {
                this.ScoreBox2.Text = score.Score2;
            }

            if (score.Score3 == "0")
            {
                this.ScoreBox3.Attributes["placeholder"] = "未评分";
            }
            else
            {
                this.ScoreBox3.Text = score.Score3;
            }

            if (score.Score4 == "0")
            {
                this.ScoreBox4.Attributes["placeholder"] = "未评分";
            }
            else
            {
                this.ScoreBox4.Text = score.Score4;
            }

            if (score.Score5 == "0")
            {
                this.ScoreBox5.Attributes["placeholder"] = "未评分";
            }
            else
            {
                this.ScoreBox5.Text = score.Score5;
            }

            if (score.Score6 == "0")
            {
                this.ScoreBox6.Attributes["placeholder"] = "未评分";
            }
            else
            {
                this.ScoreBox6.Text = score.Score6;
            }

            if (score.Score7 == "0")
            {
                this.ScoreBox7.Attributes["placeholder"] = "未评分";
            }
            else
            {
                this.ScoreBox7.Text = score.Score7;
            }

            if (score.Score8 == "0")
            {
                this.ScoreBox8.Attributes["placeholder"] = "未评分";
            }
            else
            {
                this.ScoreBox8.Text = score.Score8;
            }

            if (score.Score9 == "0")
            {
                this.ScoreBox9.Attributes["placeholder"] = "未评分";
            }
            else
            {
                this.ScoreBox9.Text = score.Score9;
            }

            if (score.Score10 == "0")
            {
                this.ScoreBox10.Attributes["placeholder"] = "未评分";
            }
            else
            {
                this.ScoreBox10.Text = score.Score10;
            }
        }
    }

    protected void UpdateButton_Click(object sender, EventArgs e)
    {

        User user = MIPS246UserManager.QueryUser(Request["StudentID"].ToString());
        StudentID = user.StudentID;
        StudentName = user.Name;

        if (this.ScoreBox1.Text != string.Empty)
        {
            MIPS246ScoreManager.UpdateScore(StudentID, 1, this.ScoreBox1.Text);
        }

        if (this.ScoreBox2.Text != string.Empty)
        {
            MIPS246ScoreManager.UpdateScore(StudentID, 2, this.ScoreBox2.Text);
        }

        if (this.ScoreBox3.Text != string.Empty)
        {
            MIPS246ScoreManager.UpdateScore(StudentID, 3, this.ScoreBox3.Text);
        }

        if (this.ScoreBox4.Text != string.Empty)
        {
            MIPS246ScoreManager.UpdateScore(StudentID, 4, this.ScoreBox4.Text);
        }

        if (this.ScoreBox5.Text != string.Empty)
        {
            MIPS246ScoreManager.UpdateScore(StudentID, 5, this.ScoreBox5.Text);
        }

        if (this.ScoreBox1.Text != string.Empty)
        {
            MIPS246ScoreManager.UpdateScore(StudentID, 1, this.ScoreBox1.Text);
        }

        if (this.ScoreBox6.Text != string.Empty)
        {
            MIPS246ScoreManager.UpdateScore(StudentID, 6, this.ScoreBox6.Text);
        }

        if (this.ScoreBox7.Text != string.Empty)
        {
            MIPS246ScoreManager.UpdateScore(StudentID, 7, this.ScoreBox7.Text);
        }

        if (this.ScoreBox8.Text != string.Empty)
        {
            MIPS246ScoreManager.UpdateScore(StudentID, 8, this.ScoreBox8.Text);
        }

        if (this.ScoreBox9.Text != string.Empty)
        {
            MIPS246ScoreManager.UpdateScore(StudentID, 9, this.ScoreBox9.Text);
        }

         if (this.ScoreBox10.Text != string.Empty)
        {
            MIPS246ScoreManager.UpdateScore(StudentID, 10, this.ScoreBox10.Text);
        }

        Response.Redirect("ScoreList.aspx");
    }
}