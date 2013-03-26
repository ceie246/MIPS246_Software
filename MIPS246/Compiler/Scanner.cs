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
        public static void GetSymbol(string line)
        {
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == '\t' || line[i] == ' ')
                {
                    continue;
                }

                if (line[i] == '\\')
                {
                    if (line[i + 1] == '\\')
                    {
                        return;
                    }
                }

                if (char.IsLetter(line[i]))
                {
                    tempStr.Clear();
                    for (int j = 0; j < maxIdentifierLength; j++)
                    {

                        if (char.IsLetterOrDigit(line[i]) || line[i] == '\t')
                        {
                            tempStr.Append(line[i]);
                            i++;
                        }
                        else if (line[i] == ' ' || line[i] == '\t')
                        {
                            //add to symbol table or ORI WORDS table;
                            break;
                        }
                        else
                        {
                            //add wrong Identifier error                            
                        }
                    }
                }



            }



        }
        #endregion

        #region Internal Methods 
        private bool IsC_OP(char c)
        {
            return false;
        }
        #endregion
    }
}
