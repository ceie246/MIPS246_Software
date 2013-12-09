using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;

/// <summary>
/// StudentScore 的摘要说明
/// </summary>
public sealed class StudentScore
{
    public ObjectId _id;

    public string StudentID { get; set; }
    public string Score1 { get; set; }
    public string Score2 { get; set; }
    public string Score3 { get; set; }
    public string Score4 { get; set; }
    public string Score5 { get; set; }
    public string Score6 { get; set; }
    public string Score7 { get; set; }
    public string Score8 { get; set; }
    public string Score9 { get; set; }
    public string Score10 { get; set; }

	public StudentScore(string studentID)
	{
        this.StudentID = studentID;
        this.Score1 = "null";
        this.Score2 = "null";
        this.Score3 = "null";
        this.Score4 = "null";
        this.Score5 = "null";
        this.Score6 = "null";
        this.Score7 = "null";
        this.Score8 = "null";
        this.Score9 = "null";
        this.Score10 = "null";
	}
}