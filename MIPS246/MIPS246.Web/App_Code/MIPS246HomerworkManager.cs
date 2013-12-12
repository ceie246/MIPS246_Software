using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Text;

/// <summary>
/// MIPS246HomerworkManager 的摘要说明
/// </summary>
public class MIPS246HomerworkManager
{
    private static string connectionString;

    private static string dbString;

    private static string collectionString;

    static MIPS246HomerworkManager()
	{
      connectionString = System.Configuration.ConfigurationManager.AppSettings["connectionString"];
      dbString = System.Configuration.ConfigurationManager.AppSettings["dbString"];
      collectionString = "homework";
	}

    public static HomeworkRecord QueryUserHomeworkRecord(string id)
    {
        MongoServer server = MongoServer.Create(connectionString);
        MongoDatabase db = server.GetDatabase(dbString);
        MongoCollection collection = db.GetCollection(collectionString);

        var query = new QueryDocument("StudentID", id);

        return collection.FindOneAs<HomeworkRecord>(query);
    }

    public static List<HomeworkRecord> QueryAllHomeworkRecord()
    {
        MongoServer server = MongoServer.Create(connectionString);
        MongoDatabase db = server.GetDatabase(dbString);
        MongoCollection collection = db.GetCollection(collectionString);

        List<HomeworkRecord> homeworkList = new List<HomeworkRecord>();
        foreach (HomeworkRecord homework in collection.FindAllAs<HomeworkRecord>())
        {
            homeworkList.Add(homework);
        }

        return homeworkList;
    }

    public static void AddHomeworkRecord(string studentID)
    {
        MongoServer server = MongoServer.Create(connectionString);
        MongoDatabase db = server.GetDatabase(dbString);
        MongoCollection collection = db.GetCollection(collectionString);

        HomeworkRecord homeworkRecord = new HomeworkRecord(studentID);

        collection.Insert<HomeworkRecord>(homeworkRecord);
    }

    public static void UpdateHomeworkRecord(string studentID, int index)
    {
        MongoServer server = MongoServer.Create(connectionString);
        MongoDatabase db = server.GetDatabase(dbString);
        MongoCollection collection = db.GetCollection(collectionString);

        var query = new QueryDocument("StudentID", studentID);

        var update = new UpdateDocument { { "$set", new QueryDocument { { "Homework" + index.ToString(), "1" } } } };

        collection.Update(query, update);
    }

    public static void DeleteHomeworkRecord(string StudentID)
    {
        MongoServer server = MongoServer.Create(connectionString);
        MongoDatabase db = server.GetDatabase(dbString);
        MongoCollection collection = db.GetCollection(collectionString);

        var query = new QueryDocument("StudentID", StudentID);

        collection.Remove(query);
    }
}