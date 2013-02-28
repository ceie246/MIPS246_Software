using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler
{
    //变量类型，必须大写
    public enum SymbolType:byte
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

    //变量的定义：变量名-变量类型-变量值-行号
    public class Symbol
    {
        #region Fields
        private string symName;

        //变量名不能长于128
        public string SymName
        {
            get { return symName; }
            set
            {
                if (value.Length <= 128)
                {
                    symName = value;
                }
                else
                { 
                    //错误处理
                }
            }
        }
        private SymbolType symType;

        public SymbolType SymType
        {
            get { return symType; }
            set { symType = value; }
        }
        private int symValue;

        public int SymValue
        {
            get { return symValue; }
            set { symValue = value; }
        }

        private int symlineNo;

        public int SymLineNo
        {
            get { return symlineNo; }
            set { symlineNo = value; }
        }
        #endregion

        #region Contructor
        public Symbol(string symName, SymbolType symType, int symValue, int symLineNo)
        {
            this.symName = symName;
            this.symType = symType;
            this.symValue = symValue;
            this.symlineNo = symLineNo; 
        }
        #endregion

        #region Public Method
        public override string ToString()
        {
            StringBuilder strTemp = new StringBuilder();
            strTemp.Append("变量名称：");
            strTemp.Append(this.symName);
            strTemp.Append("\t");
            strTemp.Append("变量类型：");
            strTemp.Append(this.symType);
            strTemp.Append("\t");
            strTemp.Append("变量值：");
            strTemp.Append(this.symValue);
            strTemp.Append("\t");
            strTemp.Append("定义的行号：");
            strTemp.Append(this.symlineNo);
            return strTemp.ToString();
        }
        #endregion
    }
}
