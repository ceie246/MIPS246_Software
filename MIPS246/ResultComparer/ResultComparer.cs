using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MIPS246.ResultComparer
{
    public static class ResultComparer
    {
        public static string Compare(string path1, string path2)
        {
            StringBuilder resultBuilder = new StringBuilder();

            List<string> source1 = new List<string>();
            List<string> source2 = new List<string>();

            try
            {
                source1 = LoadFile(path1);
                source2 = LoadFile(path2);
            }
            catch
            {
                resultBuilder.AppendLine("Error: Cannot load the file.");
                return resultBuilder.ToString();
            }

            try
            {
                for (int i = 0; (i * 33 + j)<source1.Count; i++)
                {
                    resultBuilder.AppendLine("Command #" + i+1);
                    for (int j = 0; j < 33; j++)
                    {

                        if (j != 32 && (source1[i * 33 + j].StartsWith("regfiles" + j + " = ") == false || source2[i * 33 + j].StartsWith("regfiles" + j + " = ")))
                        {
                            resultBuilder.AppendLine("Error: .out file format error.");
                            return resultBuilder.ToString();
                        }

                        if (j == 33 && (source1[i * 33 + j].StartsWith("instr = ") == false || source1[i * 33 + j].StartsWith("instr = ") == false))
                        {
                            resultBuilder.AppendLine("Error: .out file format error.");
                            return resultBuilder.ToString();
                        }

                        if (source1[i * 33 + j].Substring(source1[i * 33 + j].Length - 8) == source2[i * 33 + j].Substring(source1[i * 33 + j].Length - 8))
                        {
                            resultBuilder.AppendLine("\tCorrect.");
                        }
                        else
                        {
                            resultBuilder.AppendLine("\tWrong.");
                            resultBuilder.AppendLine("\t\t" + source1[i * 32 + j].Substring(source1[i * 32 + j].Length - 8));
                            resultBuilder.AppendLine("\t\t" + source1[i * 32 + j].Substring(source1[i * 32 + j].Length - 8));
                        }
                    }

                
                }
            }
            catch
            {
                resultBuilder.AppendLine("Error: .out file format error.");
                return "Error: .out file format error.";
            }

            return resultBuilder.ToString();
        }

        private static List<string> LoadFile(string path)
        {
            StreamReader reader = new StreamReader(path);

            List<string> sourceList = new List<string>();
            
            StreamReader sr = new StreamReader(path);
            string linetext;
                
            for (int i = 0; ; i++)
            {
                if ((linetext = sr.ReadLine()) == null)
                {
                    break;
                }
                sourceList.Add(linetext);
            }

            sr.Close();

            return sourceList;
        }
    }

    public class MIPS246ResultComparerException : Exception
    {

    }
}
