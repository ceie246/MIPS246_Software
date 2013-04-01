using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIPS246.Core.Compiler
{
    public class RegContent
    {
        #region Private Fields
        private Dictionary<string, List<string>> regDic;
        #endregion

        #region Constructor
        public RegContent()
        {
            regDic = new Dictionary<string, List<string>>();
        }

        public RegContent(List<string> regs)
        { 
            foreach(string regName in regs)
            {
                regDic.Add(regName, new List<string>());
            }
        }
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

        public void Clear(string regName)
        {
            regDic[regName].Clear();
        }

        public void RemoveVar(string regName, string varName)
        {
            regDic[regName].Remove(varName);
        }
        #endregion
    }
}
