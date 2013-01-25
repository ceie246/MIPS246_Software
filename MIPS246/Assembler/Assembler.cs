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
        private bool foundadd0;
        #endregion

        #region Constructors
        public Assembler(string sourcepath)
        {
            this.sourcepath = sourcepath;
            codelist = new List<Instruction>();
            errorlist = new List<AssemblerErrorInfo>();

            address = 0;
            line = 0;
            foundadd0 = false;
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
                CheckWord(split);
            }
            return true;
        }
        #endregion

        #region Internal Methods
        

        private void addAddresstable(string addressname, uint address)
        {
            addresstable.Add(addressname, address);
        }

        private void CheckWord(string [] split)
        {
            switch (split.Length)
            {
                case 1:
                    CheckOneWord(split);
                    break;
                case 2:
                    CheckTwoWord(split);
                    break;
                case 3:
                    CheckThreeWord(split);
                    break;
                default:
                    if (split[0].StartsWith("#") == true)
                    {
                    }
                    else
                    {
                        this.errorlist.Add(new AssemblerErrorInfo(line, AssemblerError.UNKNOWNCMD, split[0] + " " + split[1] + " " + split[2] + " " + split[3]));
                    }
                    line++;
                    break;
            }
        }

        private void CheckOneWord(string[] split)
        {
            switch (split[0])
            {
                case ".text":
                    line++;
                    break;
                default:
                    this.errorlist.Add(new AssemblerErrorInfo(line, AssemblerError.UNKNOWNCMD, split[0]));
                    line++;
                    break;

            }
        }

        private void CheckTwoWord(string [] split)
        {
            switch (split[0])
            {
                case ".globl":
                    if (foundadd0 == false)
                    {
                        address = 0;
                        addAddresstable(split[1], address);
                        line++;
                        foundadd0 = true;
                        break;
                    }
                    else
                    {
                        this.errorlist.Add(new AssemblerErrorInfo(line, AssemblerError.TWOADD0, null));
                        line++;
                        break;
                    }
                    
                default:
                    this.errorlist.Add(new AssemblerErrorInfo(line, AssemblerError.UNKNOWNCMD, split[0] + " " + split[1]));
                    line++;
                    break;
            }
        }

        private void CheckThreeWord(string[] split)
        {

        }
        #endregion

    }
}
