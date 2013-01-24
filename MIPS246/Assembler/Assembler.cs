using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MIPS246.Core.DataStructure;

namespace MIPS246.Core.Assembler
{
    public class Assembler
    {
        #region Fields
        private List<Instruction> codelist;
        private List<AssemblerError> errorlist;
        private string sourcepath;
        private bool Display;
        private bool outputFile;
        #endregion

        #region Constructors
        Assembler(string sourcepath, bool Display, bool outputFile)
        {
            this.sourcepath = sourcepath;
            this.Display = Display;
            this.outputFile = outputFile;
        }
        #endregion

        #region Properties
        #endregion

        #region Public Methods
        public void doAssemble()
        {

        }
        #endregion

        #region Internal Methods
        #endregion


    }

    public class AssemblerError
    {

    }
}
