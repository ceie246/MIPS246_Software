using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIPS246.Core.Compiler
{
    //变量类型，必须大写
    public enum VariableType
    {
        VOID,
        CHAR,
        INT,
        FLOAT,
        DOUBLE,
        SHORT,
        LONG,
        SIGNED,
        UNSIGNED,
        STRUCT,
        UNION
    }

    public class VarProp
    {
        #region Private Fields
        private VariableType varType;
        private int varValue;
        private Stack<int> varRefeInfo;
        private Stack<bool> varActInfo;
        private List<string> varAddrInfo;
        private int varAddr;
        private bool isTemp;
        #endregion
        
        #region Public Fields
        public bool IsTemp
        {
            get { return isTemp; }
            set { isTemp = value; }
        }

        public VariableType VarType
        {
            get { return varType; }
            set { varType = value; }
        }

        public int VarValue
        {
            get { return varValue; }
            set { varValue = value; }
        }

        public Stack<int> VarRefeInfo
        {
            get { return varRefeInfo; }
            set { varRefeInfo = value; }
        }

        public Stack<bool> VarActInfo
        {
            get { return varActInfo; }
            set { varActInfo = value; }
        }

        public List<string> VarAddrInfo
        {
            get { return varAddrInfo; }
            set { varAddrInfo = value; }
        }

        public int VarAddr
        {
            get { return varAddr; }
            set { varAddr = value; }
        }
        #endregion

        #region Constructor
        public VarProp(VariableType varType, int varValue, bool isTemp)
        {
            this.VarType = varType;
            this.VarValue = varValue;
            this.IsTemp = isTemp;
            this.VarRefeInfo = new Stack<int>();
            this.VarActInfo = new Stack<bool>();
            this.VarAddrInfo = new List<string>();
            this.VarAddr = -1;
        }
        #endregion
    }
}
