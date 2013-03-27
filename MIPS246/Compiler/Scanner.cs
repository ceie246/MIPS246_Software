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
        public static bool DoScan(List<string> sourceList, out List<Token> tokenList)
        {
            tokenList = new List<Token>();
            for (int i = 0; i < sourceList.Count; i++)
            {
                if (sourceList[i].StartsWith("//")) continue; 
            
                for (int j = 0; j < sourceList[i].Length; j++)
                {
                    if (sourceList[i][j] == ' ') continue;

                    if(char.IsDigit(sourceList[i][j]))
                    {
                        tempStr.Clear();
                        tempStr.Append(sourceList[i][j]);
                        j++;
                        while (j < sourceList[i].Length && char.IsDigit(sourceList[i][j]))
                        {
                            tempStr.Append(sourceList[i][j]);
                            j++;
                        }
                        tokenList.Add(new Number(int.Parse(tempStr.ToString())));
                        j--;
                    }
                    else if (char.IsLetter(sourceList[i][j]) || sourceList[i][j] == '_')
                    {
                        tempStr.Clear();
                        tempStr.Append(sourceList[i][j]);
                        j++;
                        while (j < sourceList[i].Length && (char.IsLetterOrDigit(sourceList[i][j]) || sourceList[i][j] == '_'))
                        {
                            tempStr.Append(sourceList[i][j]);
                            j++;
                        }
                        if (ReservedWord.ReservedWordList.Contains(tempStr.ToString()))
                        {
                            tokenList.Add(new ReservedWord((ReservedWordType)Enum.Parse(typeof(ReservedWordType), tempStr.ToString().ToUpper())));
                            j--;
                        }
                        else
                        {
                            tokenList.Add(new Identifier(tempStr.ToString()));
                            j--;
                        }                       
                    }
                }
            }
            return true;
        }
        #endregion

        #region Internal Methods
        #endregion
    }
}
