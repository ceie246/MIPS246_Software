using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler
{
    public class RegContent
    {
        #region Fields
        private Dictionary<string, List<string>> regDic;
        #endregion

        #region Fields
        public RegContent()
        { }
        #endregion

        #region Public Method
        public void Add(string regName, string varName)
        {
            regDic[regName].Add(varName);
        }

        public List<string> GetContent(string regName)
        {
            return regDic[regName];
        }
        #endregion
    }
}
