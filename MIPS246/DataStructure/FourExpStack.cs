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

        public bool IsContains(int index)
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
        #endregion
    }
}
