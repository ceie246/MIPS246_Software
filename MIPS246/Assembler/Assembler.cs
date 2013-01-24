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
                this.errorlist.Add(new AssemblerErrorInfo(0, AssemblerError.NOFILE));
                return false;
            }
            return true;
        }
        #endregion

        #region Internal Methods
        #endregion

    }
}
