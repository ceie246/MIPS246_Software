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
        public static bool DoScan(List<string> sourceList, out List<Token> tokenList, out CompilerErrorInfo error)
        {
            error = null;
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
                    else if (sourceList[i][j]=='/')
                    {
                        if (sourceList[i].Length > j + 1 && sourceList[i][j + 1] == '/')
                        {
                            break;
                        }
                        else if (sourceList[i][j + 1] == '*' && sourceList[i].Length >= j + 1)
                        {
                            bool inComment = true;

                            do
                            {
                                if (j + 1 == sourceList[i].Length)
                                {
                                    j = -1;
                                    i++;
                                    continue;
                                }

                                j++;

                                if (j + 1 < sourceList[i].Length && sourceList[i][j] == '*' && sourceList[i][j + 1] == '/')
                                {
                                    inComment = false;
                                    j++;
                                    break;
                                }
                            } while (i < sourceList.Count);

                            if (inComment == true)
                            {
                                //error
                            }
                        }
                    }
                    else if (sourceList[i][j] == '(')
                    {
                        tokenList.Add(new Delimiter(DelimiterType.leftParenthesis));
                    }
                    else if (sourceList[i][j] == ')')
                    {
                        tokenList.Add(new Delimiter(DelimiterType.rightParenthesis));
                    }
                    else if (sourceList[i][j] == '{')
                    {
                        tokenList.Add(new Delimiter(DelimiterType.leftBrace));
                    }
                    else if (sourceList[i][j] == '}')
                    {
                        tokenList.Add(new Delimiter(DelimiterType.rightParenthesis));
                    }
                    else if (sourceList[i][j] == ';')
                    {
                        tokenList.Add(new Delimiter(DelimiterType.semicolon));
                    }
                    else if (sourceList[i][j] == ',')
                    {
                        tokenList.Add(new Delimiter(DelimiterType.comma));
                    }
                    else if (sourceList[i][j] == '#')
                    {
                        tokenList.Add(new Delimiter(DelimiterType.pound));
                    }
                    else if (sourceList[i][j] == '!')
                    {
                        if (j < sourceList[i].Length && sourceList[i][j + 1] == '=')
                        {
                            j++;
                            tokenList.Add(new Operator(OperatorType.notequal));
                        }
                        else
                        {
                            tokenList.Add(new Operator(OperatorType.not));
                        }
                    }
                    else if (sourceList[i][j] == '=')
                    {
                        if (j < sourceList[i].Length &&  sourceList[i][j + 1] == '=')
                        {
                            j++;
                            tokenList.Add(new Operator(OperatorType.equal));
                        }
                        else
                        {
                            tokenList.Add(new Operator(OperatorType.assign));
                        }                        
                    }
                    else if (sourceList[i][j] == '[')
                    {
                        tokenList.Add(new Operator(OperatorType.leftbracket));
                    }
                    else if (sourceList[i][j] == ']')
                    {
                        tokenList.Add(new Operator(OperatorType.rightbracket));
                    }
                    else if (sourceList[i][j] == '+')
                    {
                        if (j < sourceList[i].Length && sourceList[i][j + 1] == '+')
                        {
                            j++;
                            tokenList.Add(new Operator(OperatorType.selfadd));
                        }
                        else if (j < sourceList[i].Length && sourceList[i][j + 1] == '=')
                        {
                            j++;
                            tokenList.Add(new Operator(OperatorType.addassign));
                        }
                        else
                        {
                            tokenList.Add(new Operator(OperatorType.add));
                        }
                    }
                    else if (sourceList[i][j] == '-')
                    {
                        if (j < sourceList[i].Length && sourceList[i][j + 1] == '-')
                        {
                            j++;
                            tokenList.Add(new Operator(OperatorType.selfsub));
                        }
                        else if (j < sourceList[i].Length && sourceList[i][j + 1] == '=')
                        {
                            j++;
                            tokenList.Add(new Operator(OperatorType.subassign));
                        }
                        else
                        {
                            tokenList.Add(new Operator(OperatorType.sub));
                        }
                    }
                    else if (sourceList[i][j] == '*')
                    {
                        if (j < sourceList[i].Length && sourceList[i][j + 1] == '=')
                        {
                            j++;
                            tokenList.Add(new Operator(OperatorType.mul));
                        }
                        else
                        {
                            tokenList.Add(new Operator(OperatorType.mulassign));
                        }
                    }
                    else if (sourceList[i][j] == '/')
                    {
                        if (j < sourceList[i].Length && sourceList[i][j + 1] == '=')
                        {
                            j++;
                            tokenList.Add(new Operator(OperatorType.divassign));
                        }
                        else
                        {
                            tokenList.Add(new Operator(OperatorType.div));
                        }
                    }
                    else if (sourceList[i][j] == '%')
                    {
                        if (j < sourceList[i].Length && sourceList[i][j + 1] == '=')
                        {
                            j++;
                            tokenList.Add(new Operator(OperatorType.aliquotassign));
                        }
                        else
                        {
                            tokenList.Add(new Operator(OperatorType.aliquot));
                        }
                    }
                    else if (sourceList[i][j] == '&')
                    {
                        if (j < sourceList[i].Length && sourceList[i][j + 1] == '&')
                        {
                            j++;
                            tokenList.Add(new Operator(OperatorType.and));
                        }
                        else if (j < sourceList[i].Length && sourceList[i][j + 1] == '=')
                        {
                            j++;
                            tokenList.Add(new Operator(OperatorType.andassign));
                        }
                        else
                        {
                            tokenList.Add(new Operator(OperatorType.bitand));
                        }
                    }
                    else if (sourceList[i][j] == '&')
                    {
                        if (j < sourceList[i].Length && sourceList[i][j + 1] == '&')
                        {
                            j++;
                            tokenList.Add(new Operator(OperatorType.and));
                        }
                        else if (j < sourceList[i].Length && sourceList[i][j + 1] == '=')
                        {
                            j++;
                            tokenList.Add(new Operator(OperatorType.andassign));
                        }
                        else
                        {
                            tokenList.Add(new Operator(OperatorType.bitand));
                        }
                    }
                    else if (sourceList[i][j] == '|')
                    {
                        if (j < sourceList[i].Length && sourceList[i][j + 1] == '|')
                        {
                            j++;
                            tokenList.Add(new Operator(OperatorType.or));
                        }
                        else if (j < sourceList[i].Length && sourceList[i][j + 1] == '=')
                        {
                            j++;
                            tokenList.Add(new Operator(OperatorType.orassign));
                        }
                        else
                        {
                            tokenList.Add(new Operator(OperatorType.bitor));
                        }
                    }
                    else if (sourceList[i][j] == '^')
                    {
                        if (j < sourceList[i].Length && sourceList[i][j + 1] == '=')
                        {
                            j++;
                            tokenList.Add(new Operator(OperatorType.notassign));
                        }
                        else
                        {
                            tokenList.Add(new Operator(OperatorType.not));
                        }
                    }
                    else if (sourceList[i][j] == '>')
                    {
                        if (j < sourceList[i].Length && sourceList[i][j + 1] == '=')
                        {
                            j++;
                            tokenList.Add(new Operator(OperatorType.greatereuqal));
                        }
                        else if (j < sourceList[i].Length && sourceList[i][j + 1] == '>')
                        {
                            j++;
                            tokenList.Add(new Operator(OperatorType.rightmove));
                        }
                        else
                        {
                            tokenList.Add(new Operator(OperatorType.greater));
                        }
                    }
                    else if (sourceList[i][j] == '<')
                    {
                        if (j < sourceList[i].Length && sourceList[i][j + 1] == '=')
                        {
                            j++;
                            tokenList.Add(new Operator(OperatorType.lessequal));
                        }
                        else if (j < sourceList[i].Length && sourceList[i][j + 1] == '<')
                        {
                            j++;
                            tokenList.Add(new Operator(OperatorType.leftmove));
                        }
                        else
                        {
                            tokenList.Add(new Operator(OperatorType.less));
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
