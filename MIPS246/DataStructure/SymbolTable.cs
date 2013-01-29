using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace DataStructure
{
    class SymbolTable
    {
        #region Fields
        private Hashtable symtable;
        #endregion

        #region Constructor
        public SymbolTable()
        {
            symtable = new Hashtable();
        }
        #endregion

        #region Public Method
        public bool Contains(string name)
        {
            return symtable.ContainsKey(name);
        }

        public bool Add(string name, Symbol sym)
        {
            if (!this.Contains(name))
            {
                symtable.Add(name, sym);
                return true;
            }
            else
                return false;
        }

        public bool Add(string symName, SymbolType symType, int symValue, int symLineNo) 
        {
            if (!this.Contains(symName))
            {
                Symbol sym = new Symbol(symName, symType, symValue, symLineNo);
                this.Add(symName, sym);
                return true;
            }
            else
                return false;
        }

        public Symbol GetSymbol(string name)
        {
            return (Symbol)symtable[name];
        }

        public SymbolType GetSymType(string name)
        {
            return ((Symbol)symtable[name]).SymType;
        }

        public int GetSymValue(string name)
        {
            return ((Symbol)symtable[name]).SymValue;
        }
            
        public int GetSymLineNo(string name)
        {
            return ((Symbol)symtable[name]).SymLineNo;
        }

        public override string ToString()
        {
            StringBuilder strTemp = new StringBuilder();
            foreach (string name in symtable.Keys)
            {
                strTemp.Append(name)
                    .Append(":")
                    .Append(((Symbol)symtable[name]).ToString())
                    .Append("\n");
            }
            return strTemp.ToString();
        }

        public void remove(string name)
        { 
            this.symtable.Remove(name);
        }
        #endregion
    }
}
