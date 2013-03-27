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
        private List<string> sourceList;
        private CompilerErrorInfo error;
        private string sourcepath;
        private string outputpath;
        private Hashtable addresstable;
        private Hashtable labeltable;
        private List<Token> tokenList;

        //config
        private static int startAddress = 0;    //add 0 line, for future use
        #endregion

        #region Constructors
        public Compiler(string sourcepath, string outputpath)
        {
            this.sourcepath = sourcepath;
            this.outputpath = outputpath;
            sourceList = new List<string>();
            codelist = new List<Instruction>();
            addresstable = new Hashtable();
            labeltable = new Hashtable();
            tokenList = new List<Token>();
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
            if (Scanner.DoScan(sourceList, out tokenList) == false)
            {
                int i = 5;
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
                    linetext = linetext.TrimStart(new char[] { '\t', ' ' });
                    sourceList.Add(linetext);
                }
                sr.Close();
                return true;
            }
        }

        private bool DoScan()
        {
            

            return true;
        }
        #endregion
    }
}
