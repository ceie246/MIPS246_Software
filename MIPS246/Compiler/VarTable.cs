using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIPS246.Core.Compiler
{
    public class VarTable
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

        public void SetType(string varName, VariableType varType)
        {
            varDic[varName].VarType = varType;
        }

        public int GetValue(string varName)
        {
            return varDic[varName].VarValue;
        }

        public void SetValue(string varName, int varValue)
        {
            varDic[varName].VarValue = varValue;
        }

        public Stack<int> GetRefeInfo(string varName)
        {
            return varDic[varName].VarRefeInfo;
        }

        public int PopRefeInfo(string varName)
        {
            return varDic[varName].VarRefeInfo.Pop();
        }

        public int GetPeekRefeInfo(string varName)
        {
            return varDic[varName].VarRefeInfo.Peek();
        }

        public void PushRefeInfo(string varName, int newRefe)
        { 
            varDic[varName].VarRefeInfo.Push(newRefe);
        }

        public void ClearRefeInfo(string varName)
        {
            varDic[varName].VarRefeInfo.Clear();
        }

        public Stack<bool> GetActInfo(string varName)
        {
            return varDic[varName].VarActInfo;
        }

        public bool PopActInfo(string varName)
        {
            return varDic[varName].VarActInfo.Pop();
        }

        public bool GetPeekActInfo(string varName)
        {
            return varDic[varName].VarActInfo.Peek();
        }

        public void PushActInfo(string varName, bool newAct)
        {
            varDic[varName].VarActInfo.Push(newAct);
        }

        public void ClearActInfo(string varName)
        {
            varDic[varName].VarActInfo.Clear();
        }

        public List<string> GetAddrInfo(string varName)
        {
            return varDic[varName].VarAddrInfo;
        }

        public void ClearAddrInfo(string varName)
        {
            varDic[varName].VarAddrInfo.Clear();
        }

        public void AddAddrInfo(string varName, string newAddrInfo)
        {
            varDic[varName].VarAddrInfo.Add(newAddrInfo);
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
