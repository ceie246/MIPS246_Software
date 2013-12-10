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
        this.Score1 = "0";
        this.Score2 = "0";
        this.Score3 = "0";
        this.Score4 = "0";
        this.Score5 = "0";
        this.Score6 = "0";
        this.Score7 = "0";
        this.Score8 = "0";
        this.Score9 = "0";
        this.Score10 = "0";
	}
}