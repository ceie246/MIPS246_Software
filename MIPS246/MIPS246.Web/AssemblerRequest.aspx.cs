using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;   

public partial class AssemblerRequest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        Response.Write(Request.Form["sourcecode"]);
    }

    [WebMethod]
    public static string SayHello()
    {
        return "Hello Ajax!";
    }  
}