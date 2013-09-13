using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MongoDB.Driver;
using MongoDB.Bson;

public partial class AddQuestion : System.Web.UI.Page
{
    private string connectionString;

    private string dbString;

    private string collectionString;
    

    protected void Page_Load(object sender, EventArgs e)
    {
        this.connectionString = System.Configuration.ConfigurationManager.AppSettings["connectionString"];
        this.dbString = System.Configuration.ConfigurationManager.AppSettings["dbString"];
        this.collectionString = "question";
    }
    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        try
        {
            MongoServer server = MongoServer.Create(this.connectionString);
            MongoDatabase db = server.GetDatabase(this.dbString);
            MongoCollection collection = db.GetCollection(this.collectionString);

            TypicalQuestion typicalQuestion = new TypicalQuestion(this.QuestionBox.Text, this.AnswerBox.Text, Convert.ToInt32(this.YearBox.Text));

            collection.Insert<TypicalQuestion>(typicalQuestion);

            Response.Write("<script language=javascript>alert(\"添加成功\");</script>");
        }
        catch (Exception ex)
        {
            Response.Write("<script language=javascript>alert(" + ex.Message + ");</script>");
        }
       

    }
}