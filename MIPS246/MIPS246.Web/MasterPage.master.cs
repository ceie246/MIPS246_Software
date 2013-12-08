using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage   
{
    public string LoginStatus;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            StringBuilder sb = new StringBuilder();
            if (Session["LoginStatus"]!=null && Session["LoginStatus"].ToString() == "true")
            {
                if (Session["LoginId"].ToString() == "246246" || Session["LoginId"].ToString() == "91225")
                {
                    sb.AppendLine("<li class=\"pull-right\"><a href=\"./studentList.aspx\">学生管理</a></li>");
                }

                sb.AppendLine("<li class=\"pull-right\"><a href=\"UserInfo.aspx\">" + Session["LoginId"] + "</a></li>");
                sb.AppendLine("<li class=\"pull-right\"><a href=\"Signout.aspx\">登出</a></li>");
            }
            else
            {
                sb.AppendLine("<li class=\"pull-right\"><a href=\"./Signin.aspx\">登录</a></li>");
            }
          

            

            this.LoginStatus = sb.ToString();
        }
        else
        {
            this.LoginStatus = "<li class=\"pull-right\"><a href=\"./Signin.aspx\">登录</a></li>";
        }
    }
}
