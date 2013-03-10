using MIPS246.Core.DataStructure;
using System.Collections;
using System.Collections.Generic;

namespace Compiler
{
    class Compiler
    {
        #region Fields
        private List<Instruction> codelist;
        private List<string[]> sourceList;
        //error manager
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
            return true;
        }
        #endregion
    }
}
