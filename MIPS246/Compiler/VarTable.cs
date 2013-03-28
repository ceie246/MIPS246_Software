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
        public void Add(string varName, VarProp varProp)
        {
            this.varDic.Add(varName, varProp);
        }

        public List<string> GetNames()
        {
            List<string> names = new List<string>();
            foreach (string name in varDic.Keys)
            {
                names.Add(name);
            }
            return names;
        }
        
        public VarProp GetProp(string varName)
        { 
            return varDic[varName];
        }

        public VariableType GetType(string varName)
        {
            return varDic[varName].VarType;
        }

        public int GetValue(string varName)
        {
            return varDic[varName].VarValue;
        }

        public Stack<int> GetRefeInfo(string varName)
        {
            return varDic[varName].VarRefeInfo;
        }

        public Stack<bool> GetActInfo(string varName)
        {
            return varDic[varName].VarActInfo;
        }

        public List<string> GetAddrInfo(string varName)
        {
            return varDic[varName].VarAddrInfo;
        }

        public int GetAddr(string varName)
        {
            return varDic[varName].VarAddr;
        }

        public void SetAddr(string varName, int addr)
        {
            varDic[varName].VarAddr = addr;
        }

        public bool GetTempInfo(string varName)
        {
            return varDic[varName].IsTemp;
        }

        #endregion
    }
}
