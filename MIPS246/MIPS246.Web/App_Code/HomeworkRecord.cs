using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;

/// <summary>
/// HomeworkRecord 的摘要说明
/// </summary>
public class HomeworkRecord
{
    public ObjectId _id;

    public string StudentID { get; set; }
    public string Homework1 { get; set; }
    public string Homework2 { get; set; }
    public string Homework3 { get; set; }
    public string Homework4 { get; set; }
    public string Homework5 { get; set; }
    public string Homework6 { get; set; }
    public string Homework7 { get; set; }
    public string Homework8 { get; set; }
    public string Homework9 { get; set; }
    public string Homework10 { get; set; }

    public HomeworkRecord(string studentID)
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//

        this.StudentID = studentID;
        this.Homework1 = "0";
        this.Homework2 = "0";
        this.Homework3 = "0";
        this.Homework4 = "0";
        this.Homework5 = "0";
        this.Homework6 = "0";
        this.Homework7 = "0";
        this.Homework8 = "0";
        this.Homework9 = "0";
        this.Homework10 = "0";
        this.Homework1 = "0";

	}
}