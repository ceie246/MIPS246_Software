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
    public string Name { get; set; }
    public string Major { get; set; }
    public string Sex { get; set; }
    public DateTime LastLoginTime { get; set; }
    public Int32 LoginNum { get; set; }

    public User(string studentID, string boardId, string password)
	{
        this.StudentID = studentID;
        this.BoardID=boardId;
        this.Password = password;       
	}

    public User(string studentID, string boardID, string name, string major, string sex)
    {
        this.StudentID = studentID;
        this.BoardID = boardID;
        this.Name = name;
        this.Major = major;
        this.Sex = sex;
    }
}