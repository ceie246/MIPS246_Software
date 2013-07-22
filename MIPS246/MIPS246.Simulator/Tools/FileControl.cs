using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MipsSimulator.Tools
{
    class FileControl
    {
        public static string ReadFile(string filepath) 
        { 
            try 
            {
                StreamReader sr = new StreamReader(filepath, Encoding.Default); 
                string result = sr.ReadToEnd(); 
                sr.Close();
                return result;
            } 
            catch (Exception e) 
            { 
                return e.Message.ToString();
            }
        } 
        
        public static string WriteFile(string filepath,string str) 
        { 
            try 
            {
                File.AppendAllText(filepath, str);
                return null;
            } 
            catch (Exception e) 
            {
                return e.Message.ToString();
            } 
        } 
        public static string Save(string stream)
        {
            SaveFileDialog saveFD = new SaveFileDialog();
            saveFD.Filter = "文本文件(*.txt)|*.txt";
            saveFD.FilterIndex = 1;
            saveFD.AddExtension = true;
            saveFD.RestoreDirectory = true;

            if (saveFD.ShowDialog() == DialogResult.OK)
            {
                    string fName = saveFD.FileName;
                    try
                    {
                        FileInfo fInfo = new FileInfo(fName);
                        if (fInfo.Exists)
                        {
                            fInfo.Delete();
                        }
                        FileStream fs = fInfo.OpenWrite();
                        StreamWriter w = new StreamWriter(fs);
                        w.BaseStream.Seek(0, SeekOrigin.Begin);
                        w.Write(stream);
                        w.Flush();
                        w.Close();
                    }
                    catch (Exception e)
                    {
                        return e.ToString();
                    }
                    
            }
            return null;
        }

        public static void Open(ref string stream)
        {
            OpenFileDialog openFD = new OpenFileDialog();
            openFD.Filter = "文本文件(*.txt)|*.txt";
            openFD.FilterIndex = 1;
            openFD.AddExtension = true;
            openFD.RestoreDirectory = true;

            if (openFD.ShowDialog() == DialogResult.OK)
            {
                string fName = openFD.FileName;
                stream = ReadFile(fName);
            }
            else
            {
                stream = null;
            }
        }

    }
}
