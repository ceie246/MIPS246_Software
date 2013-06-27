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
        /// <summary>
        /// 返回一个形如“L001”的字符串，编号由自身维护
        /// </summary>
        /// <returns>标签的字符串形式</returns>
        public string NewLabel()
        {
            string label = "L" + this.labelIndex.ToString("000");
            this.labelIndex++;
            return label;
        }
        #endregion
    }
}
