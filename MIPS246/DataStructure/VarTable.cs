using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIPS246.Core.DataStructure
{
    public enum VariableType
    {
        VOID,
        CHAR,       //8位
        INT,        //16位
        //FLOAT,
        //DOUBLE,
        //SHORT,
        LONG,       //32位
        //SIGNED,
        //UNSIGNED,
        //STRUCT,
        //UNION,
        BOOL        //布尔值
    }

    public class VarTable
    {   
        #region Fields
        private class VarProp
        {
            #region Private Fields
            internal VariableType varType;
            internal int varValue;
            internal Stack<int> varRefeInfo;
            internal Stack<bool> varActInfo;
            internal string varAddrInfo;
            internal short varAddr;
            #endregion

            #region Constructor
            public VarProp(VariableType varType, int varValue)
            {
                this.varType = varType;
                this.varValue = varValue;
                this.varRefeInfo = new Stack<int>();
                this.varActInfo = new Stack<bool>();
                this.varAddrInfo = "";
                this.varAddr = -1;
            }

            #endregion
        }
        
        private Dictionary<string, VarProp> varDic;
        private int tempIndex;
        #endregion

        #region Constructor
        public VarTable()
        { 
            this.varDic = new Dictionary<string, VarProp>();
            this.tempIndex = 0;
        }        
        #endregion

        #region Private Methon
        private void Add(string varName, VarProp varProp)
        {
            this.varDic.Add(varName, varProp);
        }
        #endregion

        #region Public Method
        public void Add(string varName, VariableType varType, int varValue)
        { 
            VarProp prop = new VarProp(varType, varValue);
            this.Add(varName, prop);
        }

        public string newTemp(VariableType varType)
        {
            string varName = "t" + tempIndex.ToString("000");
            tempIndex++;
            this.Add(varName, varType, 0);
            return varName;
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

        public VariableType GetType(string varName)
        {
            return varDic[varName].varType;
        }

        public void SetType(string varName, VariableType varType)
        {
            varDic[varName].varType = varType;
        }

        public int GetValue(string varName)
        {
            return varDic[varName].varValue;
        }

        public void SetValue(string varName, int varValue)
        {
            varDic[varName].varValue = varValue;
        }

        public Stack<int> GetRefeInfo(string varName)
        {
            return varDic[varName].varRefeInfo;
        }

        public int PopRefeInfo(string varName)
        {
            return varDic[varName].varRefeInfo.Pop();
        }

        public int GetPeekRefeInfo(string varName)
        {
            return varDic[varName].varRefeInfo.Peek();
        }

        public void PushRefeInfo(string varName, int newRefe)
        { 
            varDic[varName].varRefeInfo.Push(newRefe);
        }

        public void ClearRefeInfo(string varName)
        {
            varDic[varName].varRefeInfo.Clear();
        }

        public Stack<bool> GetActInfo(string varName)
        {
            return varDic[varName].varActInfo;
        }

        public bool PopActInfo(string varName)
        {
            return varDic[varName].varActInfo.Pop();
        }

        public bool GetPeekActInfo(string varName)
        {
            return varDic[varName].varActInfo.Peek();
        }

        public void PushActInfo(string varName, bool newAct)
        {
            varDic[varName].varActInfo.Push(newAct);
        }

        public void ClearActInfo(string varName)
        {
            varDic[varName].varActInfo.Clear();
        }

        public string GetAddrInfo(string varName)
        {
            return varDic[varName].varAddrInfo;
        }

        public void SetAddrInfo(string varName, string regName)
        {
            varDic[varName].varAddrInfo = regName;
        }

        public short GetAddr(string varName)
        {
            return varDic[varName].varAddr;
        }

        public void SetAddr(string varName, short addr)
        {
            varDic[varName].varAddr = addr;
        }

        #endregion
    }
}
