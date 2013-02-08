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
        private List<FourExp> fourExpList;
        private List<Instruction> insList;
        private Dictionary<int, string> lableDic;
        #endregion

        #region Constructor
        public CodeGenerator(List<FourExp> fourExpList, List<Instruction> insList)
        {
            this.fourExpList = fourExpList;
            this.insList = insList;
            lableDic = new Dictionary<int, string>();
        }
        #endregion

        #region Public Method
        public void Generate()
        {
            foreach (FourExp f in fourExpList)
            {
                genLabel();
                convert(f);
                optimize();
            }
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

        private void convert(FourExp f)
        { 
        
        }

        private void optimize()
        { 
        
        }
        #endregion
    }
}
