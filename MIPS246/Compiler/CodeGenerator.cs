using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MIPS246.Core.DataStructure;

namespace Compiler
{
    public class CodeGenerator
    {
        #region Fields
        private FourExpTable fourExpTable;
        private List<Instruction> insList;
        #endregion

        #region Constructor
        public CodeGenerator(FourExpTable fourExpTable, List<Instruction> insList)
        {
            this.fourExpTable = fourExpTable;
            this.insList = insList;
        }
        #endregion

        #region Public Method
        public void Generate() 
        {
            genLabel();
            convert();
            optimize();
        }
        #endregion

        #region Public Method
        private string getRegOrImmi() 
        {
            return null;
        }

        private void genLabel()
        { 
        
        }

        private void convert()
        { 
        
        }

        private void optimize()
        { 
        
        }
        #endregion
    }
}
