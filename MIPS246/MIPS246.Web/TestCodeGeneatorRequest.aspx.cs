using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TestCodeGeneatorRequest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Write(Request.Form.Get("num"));
        string i = Request.QueryString["num"];
        Response.Write(Request.Form.Get("num"));
    }//
}