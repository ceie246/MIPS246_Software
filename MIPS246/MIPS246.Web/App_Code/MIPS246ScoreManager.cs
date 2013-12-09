using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Text;

/// <summary>
/// MIPS246ScoreManager 的摘要说明
/// </summary>
public static class MIPS246ScoreManager
{
    private static string connectionString;

    private static string dbString;

    private static string collectionString;

    static MIPS246ScoreManager()
	{
      connectionString = System.Configuration.ConfigurationManager.AppSettings["connectionString"];
      dbString = System.Configuration.ConfigurationManager.AppSettings["dbString"];
      collectionString = "score";
	}

    public static StudentScore QueryUserScore(string id)
    {
        MongoServer server = MongoServer.Create(connectionString);
        MongoDatabase db = server.GetDatabase(dbString);
        MongoCollection collection = db.GetCollection(collectionString);

        var query = new QueryDocument("StudentID", id);

        return collection.FindOneAs<StudentScore>(query);
    }

    public static List<StudentScore> QueryAllUser()
    {
        MongoServer server = MongoServer.Create(connectionString);
        MongoDatabase db = server.GetDatabase(dbString);
        MongoCollection collection = db.GetCollection(collectionString);

        List<StudentScore> studentList = new List<StudentScore>();
        foreach (StudentScore score in collection.FindAllAs<StudentScore>())
        {
            studentList.Add(score);
        }

        return studentList;
    }

    public static void AddScore(string studentID)
    {
        MongoServer server = MongoServer.Create(connectionString);
        MongoDatabase db = server.GetDatabase(dbString);
        MongoCollection collection = db.GetCollection(collectionString);

        StudentScore studentScore = new StudentScore(studentID);

        collection.Insert<StudentScore>(studentScore);
    }

    public static void UpdateScore(string studentID, int index, string score)
    {
        MongoServer server = MongoServer.Create(connectionString);
        MongoDatabase db = server.GetDatabase(dbString);
        MongoCollection collection = db.GetCollection(collectionString);

        var query = new QueryDocument("StudentID", studentID);

        var update = new UpdateDocument { { "$set", new QueryDocument { { "Score" + index.ToString(), score } } } };

        collection.Update(query, update);
    }

    public static void DeleteScore(string StudentID)
    {
        MongoServer server = MongoServer.Create(connectionString);
        MongoDatabase db = server.GetDatabase(dbString);
        MongoCollection collection = db.GetCollection(collectionString);

        var query = new QueryDocument("StudentID", StudentID);

        collection.Remove(query);
    }
}