using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using MIPS246.Core.DataStructure;


namespace MIPS246.Core.Assembler
{
    public class Assembler
    {
        #region Fields
        private List<Instruction> codelist;
        private List<AssemblerErrorInfo> errorlist;
        private string sourcepath;
        private uint address;
        private uint line;
        private Hashtable addresstable;        
        #endregion

        #region Constructors
        public Assembler(string sourcepath)
        {
            this.sourcepath = sourcepath;
            codelist = new List<Instruction>();
            errorlist = new List<AssemblerErrorInfo>();

            address = 0;
            line = 0;
        }
        #endregion

        #region Properties
        #endregion

        #region Public Methods
        public bool doAssemble()
        {
            if (!File.Exists(sourcepath))
            {
                this.errorlist.Add(new AssemblerErrorInfo(0, AssemblerError.NOFILE, sourcepath));
                return false;
            }
            StreamReader sr = new StreamReader(sourcepath);
            string linetext;
            while ((linetext = sr.ReadLine()) != null) 
            {
                string[] split = linetext.Split(new Char[] { ' ', '\t', ',' });
                switch (split.Length)
                {
                    case 1: 
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                }
                /*if (linetext.StartsWith("."))
                {
                    if (linetext.StartsWith(".text")) 
                    {
                    }
                    else if(linetext.StartsWith(".globl"))
                    {
                        address = 0;
                        
                        continue;
                    }
                }
                if (linetext.EndsWith(":"))
                {
                    addAddresstable(linetext.Substring(0, linetext.Length - 1), address);
                }
                Console.WriteLine(linetext);
                line++;*/
            }
            return true;
        }
        #endregion

        #region Internal Methods
        private bool checktext()
        {
            return true;
        }

        private void addAddresstable(string addressname, uint address)
        {
            addresstable.Add(addressname, address);
        }

        private void CheckOneWord()
        { 
        }

        private void CheckTwoWord()
        {
        }

        private void CheckThreeWord()
        {
        }
        #endregion

    }
}
