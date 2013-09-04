using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MIPS246.Core.DataStructure;
using System.IO;

namespace MIPS246.Core.Disassembler
{
    public class Disassembler
    {
        #region Fields
        int counter;
        private List<string> sourceString;
        private List<Instruction> codelist;
       //private List<string[]> sourceList;
        private List<int> jumptag;
        
        //private AssemblerErrorInfo error;
        private string sourcepath;
        private string outputpath;
        //private Hashtable linetable;
        //private Hashtable codeindextable;
        //private Hashtable addresstable;
        //private Hashtable labeltable;


        //config
        private static int startAddress = 0;    //add 0 line, for future use
        #endregion

        #region Constructors
        public Disassembler()
        {
        }

        public Disassembler(string sorcepath, string outputpath)
        {
            this.sourcepath = sorcepath;
            this.outputpath = outputpath;
            this.sourceString = new List<string>();
            this.jumptag = new List<int>();
            this.codelist = new List<Instruction>();
        }

        public Disassembler(List<string> sourceString)
        {
            this.sourcepath = null;
            this.outputpath = null;
            this.sourceString = sourceString;
            this.jumptag = new List<int>();
            this.codelist = new List<Instruction>();
        }

        public Disassembler(string sourceCode)
        {
            this.sourcepath = null;
            this.outputpath = null;
            this.sourceString = new List<string>(sourceCode.Split(new char[] {'\n'}, StringSplitOptions.RemoveEmptyEntries));
            this.jumptag = new List<int>();
            this.codelist = new List<Instruction>();
        }

        #endregion

        #region Properties
        public List<string> SourceString
        {
            get
            {
                return sourceString;
            }
        }

        public List<Instruction> Codelist
        {
            get
            {
                return codelist;
            }
        }

        public List<int> Jumptag
        {
            get
            {
                return jumptag;
            }
        } 

        #endregion

        #region Public Methods
        public bool DoDisassemble
        {        


        }
        #endregion

        #region Internal Methods
        private bool LoadFile()
        {
            if (sourcepath != null)
            {
                if (File.Exists(sourcepath) == false)
                {
                    //error code
                    return false;
                }
                else
                {
                    StreamReader sr = new StreamReader(sourcepath);
                    string _line;
                    while ((_line = sr.ReadLine()) != null)
                    {
                        this.sourceString.Add(_line);
                    }
                }
            }
        }
        #endregion
    }
}
