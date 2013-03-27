using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIPS246.Core.Compiler
{
    public static class Scanner
    {
        #region Fields
        private const int maxIdentifierLength = 256;
        private const int intMax = int.MaxValue;
        private static StringBuilder tempStr = new StringBuilder();        
        #endregion

        #region Public Method
        public static void DoScan(List<string> sourceList)
        {

            for (int i = 0; i < sourceList.Count; i++)
            {
            }
        }
        #endregion

        #region Internal Methods
        #endregion
    }
}
