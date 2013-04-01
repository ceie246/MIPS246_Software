using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIPS246.Core.DataStructure
{
    public enum InsDataType 
    {
        BYTE, WORD, SPACE, STRING,
    }

    public class DataInstruction
    {
        private string varName;
        private InsDataType varType;
        private int varValue;

        public DataInstruction(string varName, InsDataType varType, int varValue)
        {
            this.varName = varName;
            this.varType = varType;
            this.varValue = varValue;
        }
    }
}
