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

    public string StudentID { get; set; }
    public string BoardID { get; set; }
    public string Password { get; set; }
    public DateTime LastLoginTime { get; set; }

    public User(string studentID, string boardId, string password)
	{
        this.StudentID = studentID;
        this.BoardID=boardId;
        this.Password = password;       

	}
}