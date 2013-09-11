using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;

/// <summary>
/// User 的摘要说明
/// </summary>
public sealed class User
{
    public ObjectId _id;

    public long StudentID { get; set; }
    public string Name { get; set; }
    public string BoardID { get; set; }
    public string Password { get; set; }
    public DateTime LastLoginTime { get; set; }

    public User()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
        

	}
}