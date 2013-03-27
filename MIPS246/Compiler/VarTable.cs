using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIPS246.Core.Compiler
{
    class VarTable
    {
        #region Fields
        private Dictionary<string, VarProp> varDic;
        #endregion

        #region Constructor
        public VarTable()
        { 
            this.varDic = new Dictionary<string, VarProp>();
        }        
        #endregion

        #region Public Method
        public void add(string varName, VarProp varProp)
        {
            this.varDic.Add(varName, varProp);
        }
        
        public VarProp getProp(string varName)
        { 
            return varDic[varName];
        }

        public VariableType getType(string varName)
        {
            return varDic[varName].VarType;
        }

        public int getValue(string varName)
        {
            return varDic[varName].VarValue;
        }

        public Stack<int> getRefeInfo(string varName)
        {
            return varDic[varName].VarRefeInfo;
        }

        public Stack<bool> getActInfo(string varName)
        {
            return varDic[varName].VarActInfo;
        }

        public List<string> getAddrInfo(string varName)
        {
            return varDic[varName].VarAddrInfo;
        }

        public int getAddr(string varName)
        {
            return varDic[varName].VarAddr;
        }

        #endregion
    }
}
