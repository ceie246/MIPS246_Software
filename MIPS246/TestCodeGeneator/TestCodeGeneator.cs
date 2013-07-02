using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MIPS246.Core.DataStructure;

namespace MIPS246.Core.TestCodeGeneator
{
    public class TestCodeGeneator
    {
        #region Fields
        private int count;
        private int seed;
        private List<Instruction> codeList; 
        #endregion

        #region Constructors
        static TestCodeGeneator()
        {
        }
        #endregion

        #region Properties
        public int Count
        {
            set
            {
                this.count = value;
            }

            get
            {
                return this.count;
            }
        }

        public int Seed
        {
            set
            {
                this.seed = value;
            }
            get
            {
                return this.seed;
            }
        }

        public List<Instruction> CodeList
        {
            set
            {
                this.codeList = value;
            }
            get
            {
                return this.codeList;
            }
        }
        #endregion

        #region Public Methods
        #endregion

        #region Internal Methods
        #endregion
    }
}
