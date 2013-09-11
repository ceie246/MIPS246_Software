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
    private const string connectionString = "mongodb://localhost";

    private const string dbString = "MIPS246";

    private const string collectionString = "user";

	static MIPS246UserManager()
	{
        
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    public static void Test()
    {
        /*MongoServer server = MongoServer.Create(connectionString);
        MongoDatabase db = server.GetDatabase(dbString);
        MongoCollection collection = db.GetCollection(collectionString);

        User user=new User();
        user.Name="mips246";
        user.BoardID = "000000";
        user.StudentID = 246246;
        user.Password = HashMD5("lavielavie");

        collection.Insert(user);*/
        bool a = checkUser("mips246", "lavielavie2");
    }

    public static bool checkUser(string name, string password)
    {
        MongoServer server = MongoServer.Create(connectionString);
        MongoDatabase db = server.GetDatabase(dbString);
        MongoCollection collection = db.GetCollection(collectionString);

        var query = new QueryDocument("Name", name);

        if(collection.Count(query)==0)
        {
            return false;
        }
        else
        {
             var result = collection.FindOneAs<User>(query);
             if(result.Password==HashMD5(password))
             {
                 return true;
             }
             else
             {
                 return false;
             }
        }
       
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