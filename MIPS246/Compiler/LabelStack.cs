using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIPS246.Core.Compiler
{
    class LabelStack
    {
        #region Private Field
        private int labelIndex = 0;
        private Stack<string> labelStack = new Stack<string>();
        #endregion

        #region Public Field
        #endregion

        #region Public Constructor
        #endregion


        #region Public Method
        public void Push()
        { 
            this.labelStack.Push("L" + labelIndex.ToString("000"));
            this.labelIndex ++;
        }

        public string Pop()
        { 
            return labelStack.Pop();
        }

        public string Peek()
        {
            return labelStack.Peek();
        }
        #endregion
    }
}
