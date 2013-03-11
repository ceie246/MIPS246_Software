using MIPS246.Core.DataStructure;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace MIPS246.Core.Compiler
{
    public class Compiler
    {
        #region Fields
        private List<Instruction> codelist;
        private List<string[]> sourceList;
        private CompilerErrorInfo error;
        private string sourcepath;
        private string outputpath;
        private Hashtable addresstable;
        private Hashtable labeltable;

        //config
        private static int startAddress = 0;    //add 0 line, for future use
        #endregion

        #region Constructors
        public Compiler(string sourcepath, string outputpath)
        {
            this.sourcepath = sourcepath;
            this.outputpath = outputpath;
            sourceList = new List<string[]>();
            codelist = new List<Instruction>();
            addresstable = new Hashtable();
            labeltable = new Hashtable();
        }
        #endregion

        #region Properties
        public List<Instruction> CodeList
        {
            get
            {
                return this.codelist;
            }
        }
        #endregion

        #region Public Methods
        public bool DoCompile()
        {
            if (this.LoadFile() == false)
            {
                this.error = new CompilerErrorInfo(0, CompilerError.NOFILE);
                return false;
            }
            return true;
        }

        public void Display(bool isBinary)
        {
        }

        public void Output(bool isoutputCOE, string outputPath)
        {
        }

        public void DisplayError()
        {
        }
        #endregion

        #region Internal Methods      
        private bool LoadFile()
        {
            if (File.Exists(sourcepath) == false)
            {
                this.error = new CompilerErrorInfo(0, CompilerError.NOFILE);
                return false;
            }
            else
            {
                StreamReader sr = new StreamReader(sourcepath);
                string linetext;
                while ((linetext = sr.ReadLine()) != null)
                {
                    linetext = RemoveComment(linetext);
                    sourceList.Add(linetext.Split(new char[] { ' ', '\t', ',' }));
                }
                sr.Close();
                return true;
            }
            return true;
        }

        private string RemoveComment(string str)
        {
            if (str.Contains("//"))
            {
                if (str.IndexOf('#') != 0)
                {
                    str = str.Substring(0, str.IndexOf("#") - 1);
                }
                else
                {
                    str = string.Empty;
                }
            }
            return str;
        }
        #endregion
    }
}
