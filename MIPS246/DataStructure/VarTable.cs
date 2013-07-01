using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIPS246.Core.DataStructure
{
    public enum VariableType
    {
        VOID,
        BOOL,       //布尔值
        CHAR,       //8位
        INT,        //16位
        //FLOAT,
        //DOUBLE,
        //SHORT,
        LONG        //32位
        //SIGNED,
        //UNSIGNED,
        //STRUCT,
        //UNION,
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
        /// <summary>
        /// 根据变量名，变量类型，变量值向符号表中增加一个变量
        /// </summary>
        /// <param name="varName">变量名</param>
        /// <param name="varType">变量类型</param>
        /// <param name="varValue">变量值</param>
        public void Add(string varName, VariableType varType, int varValue)
        {
            VarProp prop = new VarProp(varType, varValue);
            this.Add(varName, prop);
        }

        public string NewTemp(VariableType varType)
        {
            string varName = "t" + tempIndex.ToString("000");
            tempIndex++;
            this.Add(varName, varType, 0);
            return varName;
        }

        /// <summary>
        /// 在变量表中生成一个源变量的副本，用于后置自增、自减运算、左移运算、右移运算，新临时变量的值和类型与源变量相同
        /// </summary>
        /// <param name="sourceName">原变量</param>
        /// <returns>临时变量名</returns>
        public string NewTemp(string sourceName)
        {
            string varName = "t" + tempIndex.ToString("000");
            tempIndex++;
            int value = this.GetValue(sourceName);
            VariableType type = this.GetType(sourceName);
            this.Add(sourceName, type, value);
            return varName;
        }

        /// <summary>
        /// 根据两个源变量中类型较大的变量的类型，产生一个新的临时变量,新临时变量的值为0
        /// </summary>
        /// <param name="source1">源变量1</param>
        /// <param name="source2">源变量2</param>
        /// <returns>临时变量名</returns>
        public string NewTemp(string source1, string source2)
        {
            string varName = "t" + tempIndex.ToString("000");
            tempIndex++;
            VariableType type1 = this.GetType(source1);
            VariableType type2 = this.GetType(source2);
            VariableType type = 0;
            if (type1 > type2)
            {
                type = type1;
            }
            else
            {
                type = type2;
            }
            this.Add(varName, type, 0);
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
            if (this.Contains(varName))
            {
                return varDic[varName].varType;
            }
            else
                return 0;
        }

        public bool SetType(string varName, VariableType varType)
        {
            if (this.GetNames().Contains(varName))
            {
                varDic[varName].varType = varType;
                return true;
            }
            else
                return false;
        }

        public int GetValue(string varName)
        {
            return varDic[varName].varValue;
        }

        public void SetValue(string varName, int varValue)
        {
            varDic[varName].varValue = varValue;
        }

        /// <summary>
        /// 根据源变量，给变量表中的变量赋值
        /// </summary>
        /// <param name="varName">待赋值变量</param>
        /// <param name="sourceVar">源变量</param>
        public void SetValue(string varName, string sourceVar)
        {
            int value = 0;
            try
            {
                value = Convert.ToInt32(sourceVar);
            }
            catch
            {
                value = this.GetValue(sourceVar);
            }
            finally
            {
                this.SetValue(varName, value);
            }
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

        public bool Contains(string varName)
        {
            if (this.GetNames().Contains(varName))
            {
                return true;
            }
            else 
                return false;
        }

        #endregion
    }
}
