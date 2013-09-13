using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;

/// <summary>
/// TypicalQuestion 的摘要说明
/// </summary>
public class TypicalQuestion
{
    public ObjectId _id;

    public string Question { get; set; }
    public string Answer { get; set; }
    public int Year { get; set; }
    
	public TypicalQuestion(string question, string answer, int year)
	{
        this.Question = question;
        this.Answer = answer;
        this.Year = year;
	}
}