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
        private int lableNo;
        #endregion

        #region Constructor
        public CodeGenerator(List<FourExp> fourExpList, List<Instruction> insList)
        {
            this.fourExpList = fourExpList;
            this.insList = insList;
            this.lableDic = new Dictionary<int, string>();
            this.lableNo = 0;
        }
        #endregion

        #region Public Method
        public void Generate()
        {
            foreach (FourExp f in fourExpList)
            {
                genLabel(f);
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

        private void genLabel(FourExp f)
        {
            int fourExpNo = f.NextFourExp;
            if (fourExpNo != -1)
            {
                lableDic.Add(fourExpNo, "L" + lableNo.ToString("D3"));
                lableNo++;
            }
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
