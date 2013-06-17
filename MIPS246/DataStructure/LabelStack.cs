using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIPS246.Core.DataStructure
{
    public class LabelStack
    {
        #region Private Field
        private int labelIndex = 0;
        #endregion

        #region Public Field
        #endregion

        #region Public Constructor
        #endregion

        #region Public Method
        public string newLabel()
        {
            string label = "L" + this.labelIndex.ToString("000");
            this.labelIndex++;
            return label;
        }
        #endregion
    }
}
