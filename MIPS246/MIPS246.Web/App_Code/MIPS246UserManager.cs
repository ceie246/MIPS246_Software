using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// MIPS246UserManager 的摘要说明
/// </summary>
public static class MIPS246UserManager
{
    private static string connectionString;

    private static string dbString;

    private static string collectionString;

	static MIPS246UserManager()
	{
      connectionString = System.Configuration.ConfigurationManager.AppSettings["connectionString"];
      dbString = System.Configuration.ConfigurationManager.AppSettings["dbString"];
      collectionString = "user";
	}

    public static User CheckUser(string id, string password)
    {
        MongoServer server = MongoServer.Create(connectionString);
        MongoDatabase db = server.GetDatabase(dbString);
        MongoCollection collection = db.GetCollection(collectionString);

        var query = new QueryDocument("StudentID", id);

        if(collection.Count(query)==0)
        {
            return null;
        }
        else
        {
             User result = collection.FindOneAs<User>(query);
             if(result.Password==HashMD5(password))
             {
                 return result;
             }
             else
             {
                 return null;
             }
        }
       
    }

    public static User QueryUser(string id)
    {
        MongoServer server = MongoServer.Create(connectionString);
        MongoDatabase db = server.GetDatabase(dbString);
        MongoCollection collection = db.GetCollection(collectionString);

        var query = new QueryDocument("StudentID", id);

        return collection.FindOneAs<User>(query);
    }
    public static List<User> QueryAllUser()
    {
        MongoServer server = MongoServer.Create(connectionString);
        MongoDatabase db = server.GetDatabase(dbString);
        MongoCollection collection = db.GetCollection(collectionString);

        List<User> userList = new List<User>();
        foreach(User user in collection.FindAllAs<User>())
        {
            userList.Add(user);
        }

        return userList;
    }

    public static void AddUser(User user)
    {
        MongoServer server = MongoServer.Create(connectionString);
        MongoDatabase db = server.GetDatabase(dbString);
        MongoCollection collection = db.GetCollection(collectionString); 
        collection.Insert<User>(user);
        MIPS246ScoreManager.AddScore(user.StudentID);
    }

    public static void AddUser(string num, string password)
    {
        MongoServer server = MongoServer.Create(connectionString);
        MongoDatabase db = server.GetDatabase(dbString);
        MongoCollection collection = db.GetCollection(collectionString);

        User user = new User(num, "NA", HashMD5(password));
        collection.Insert<User>(user);
        MIPS246ScoreManager.AddScore(user.StudentID);
    }

    public static void UpdateLoginTime(string id)
    {
        MongoServer server = MongoServer.Create(connectionString);
        MongoDatabase db = server.GetDatabase(dbString);
        MongoCollection collection = db.GetCollection(collectionString);

        var query = new QueryDocument("StudentID", id);

        var update = new UpdateDocument { { "$set", new QueryDocument { { "LastLoginTime", BsonDateTime.Create(DateTime.Now + TimeSpan.FromHours(8)) } } } };
        collection.Update(query, update);
    }

    public static void UpdateLoginNum(string id)
    {
        MongoServer server = MongoServer.Create(connectionString);
        MongoDatabase db = server.GetDatabase(dbString);
        MongoCollection collection = db.GetCollection(collectionString);

        User user = QueryUser(id);

        var query = new QueryDocument("StudentID", id);

        var update = new UpdateDocument { { "$set", new QueryDocument { { "LoginNum", BsonInt32.Create(user.LoginNum + 1) } } } };
        collection.Update(query, update);
    }

    public static void DeleteUser(string StudentID)
    {
        MongoServer server = MongoServer.Create(connectionString);
        MongoDatabase db = server.GetDatabase(dbString);
        MongoCollection collection = db.GetCollection(collectionString);

         var query = new QueryDocument("StudentID", StudentID);

         collection.Remove(query);
         MIPS246ScoreManager.DeleteScore(StudentID);
    }

    public static void ChangePassword(string id,string password)
    {
        MongoServer server = MongoServer.Create(connectionString);
        MongoDatabase db = server.GetDatabase(dbString);
        MongoCollection collection = db.GetCollection(collectionString);

        var query = new QueryDocument("StudentID", id);

        var update = new UpdateDocument { { "$set", new QueryDocument { { "Password", HashMD5(password) } } } };
        collection.Update(query, update);
    }

    public static void UpdateUser(string StudentID, string Name, string Sex, string Major, string BoardID , string Password)
    {
        MongoServer server = MongoServer.Create(connectionString);
        MongoDatabase db = server.GetDatabase(dbString);
        MongoCollection collection = db.GetCollection(collectionString);

        var query = new QueryDocument("StudentID", StudentID);

        var update = new UpdateDocument();

        if (Password == string.Empty)
        {
            update = new UpdateDocument { { "$set", new QueryDocument { { "StudentID", StudentID }, { "Name", Name }, { "Major", Major }, { "Sex", Sex }, { "BoardID", BoardID } } } };
        }
        else
        {
            update = new UpdateDocument { { "$set", new QueryDocument { { "StudentID", StudentID }, { "Name", Name }, { "Major", Major }, { "Sex", Sex }, { "BoardID", BoardID }, { "Password", HashMD5(Password) } } } };

        }
        
        collection.Update(query, update);
    }

    public static string HashMD5(string s)
    {
        MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();

        byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(s + "mips246");
        byteArray = md5CryptoServiceProvider.ComputeHash(byteArray);
        StringBuilder sb = new StringBuilder();

        foreach (byte b in byteArray)
        {
            sb.Append(b.ToString("x2").ToLower());
        }

        return sb.ToString();
    }
}