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

    public static User checkUser(string id, string password)
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

    public static void AddUser(string num)
    {
        MongoServer server = MongoServer.Create(connectionString);
        MongoDatabase db = server.GetDatabase(dbString);
        MongoCollection collection = db.GetCollection(collectionString);

        User user = new User(num, "NA", num);
        collection.Insert<User>(user);
    }

    private static string HashMD5(string s)
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