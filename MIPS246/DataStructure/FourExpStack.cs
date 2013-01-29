using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace MIPS246.Core.DataStructure
{
    class FourExpStack
    {
        #region Fields
        private Hashtable fourExpTable;
        #endregion

        #region Constructor
        public FourExpStack()
        {
            fourExpTable = new Hashtable();
        }
        #endregion

        #region Public Methon
        public void Push(int index, FourExp fourExp)
        {
            fourExpTable.Add(index, fourExp);
        }

        public void Push(int index, FourExpOperation op, string arg1, string arg2, int nextFourExp)
        {
            FourExp fourExp = new FourExp(op, arg1, arg2, nextFourExp);
            this.Push(index, fourExp);
        }

        public void Push(int index, FourExpOperation op, string arg1, string arg2, string result)
        {
            FourExp fourExp = new FourExp(op, arg1, arg2, result);
            this.Push(index, fourExp);
        }

        public bool Contains(int index)
        {
            return fourExpTable.ContainsKey(index);
        }

        public FourExp GetFourExp(int index)
        {
            return (FourExp)fourExpTable[index];

        }

        public override string ToString()
        { 
            StringBuilder strTemp = new StringBuilder();
            int numOfKeys = fourExpTable.Count;
            for (int index = 0; index < numOfKeys; index++ )
            {
                strTemp.Append(index)
                    .Append(" : ")
                    .Append(fourExpTable[index].ToString());
            }
            return strTemp.ToString();
        }

        public ArrayList ToList()
        {
            ArrayList fourExpList = new ArrayList();
            int numOfKeys = fourExpTable.Count;
            for (int index = 0; index < numOfKeys; index++)
            {
                fourExpList.Add(fourExpTable[index]);
            }
            return fourExpList;
        }

        public FourExp Pop()
        {
            int maxIndex = this.fourExpTable.Count;
            return (FourExp)fourExpTable[maxIndex];
        }
        #endregion
    }
}
