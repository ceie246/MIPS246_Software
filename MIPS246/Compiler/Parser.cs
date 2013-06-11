using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MIPS246.Core.Compiler;
using MIPS246.Core.Compiler.AstStructure;
using MIPS246.Core.DataStructure;

namespace MIPS246.Core.Compiler
{
    public class Parser
    {
        #region Fields
        static int i;
        static List<Token> tokenList;
        static Ast ast;
        #endregion

        #region Public Method
        public static bool DoParse(List<Token> tokenlist, Ast _ast, out CompilerErrorInfo error)
        {
            ast = _ast;
            error = null;
            i = 0;
            tokenList = tokenlist;

            while (i < tokenList.Count)
            {
                GenStatement(ast.Statements);
            }

            return true;
        }
        #endregion

        #region Internal Methods
        static private Token GetNextToken()
        {
            return tokenList[i++];            
        }

        static private Token TouchNextToken()
        {
            return tokenList[i]; 
        }

        static private bool GenStatement(List<Statement> StatementsNode)
        {
            Token tempToken = TouchNextToken();
            if (tempToken is ReservedWord)
            {
                if (IsTypeToken(tempToken))
                {
                    GenFieldDefineStatement(StatementsNode);                    
                }
            }
            return true;
        }

        static private void ParseToken(Token token)
        {
        }

        static private void GenIfStatement()
        {

        }

        static private void GenWhileStatement()
        {
        }

        static private void GenDoWhileStatement()
        {
        }

        static private void GenForStatement()
        {
        }

        static private bool GenFieldDefineStatement(List<Statement> StatementsNode)
        {
            Token identifierType = null;
            Token identifierToken = null;
            while (true)
            {
                if(IsTypeToken(TouchNextToken()))
                {
                    identifierType = GetNextToken();

                    if(IsIdentifier(TouchNextToken()))
                    {
                        identifierToken = GetNextToken();
                    }
                    else
                    {
                        //wrong, expect identifier
                        return false;
                    }
                }
                else if(IsIdentifier(TouchNextToken()))
                {
                    identifierToken = GetNextToken();
                }
                else
                {
                    //wrong, expect identifier or type
                    return false;
                }

                StatementsNode.Add(new FieldDefineStatement((VariableType)Enum.Parse(typeof(VariableType), ((ReservedWord)identifierType).WordType.ToString()), ((Identifier)identifierToken).Name));

                if (IsAssign(TouchNextToken()))
                {
                    GetNextToken();
                    //add op expression
                }
                else if (IsSemicon(TouchNextToken()))
                {
                    //Done
                    GetNextToken();
                    return true;
                }
                else if (IsComma(TouchNextToken()))
                {
                    GetNextToken();
                    continue;
                }
                else
                {
                    //wrong token
                }
            }
        }

        static private void GenArrayDefineStatement()
        {
        }

        static private void GenAssignStatement()
        {
        }

        static private void GenRestStatement()
        {
        }

        static private bool IsIdentifier(Token token)
        {
            return token is Identifier;
        }

        static private bool IsAssign(Token token)
        {
            return token is Operator && ((Operator)token).Type == OperatorType.assign;
        }

        static private bool IsSemicon(Token token)
        {
            return token is Delimiter && ((Delimiter)token).Form == DelimiterType.semicolon;
        }

        static private bool IsComma(Token token)
        {
            return token is Delimiter && ((Delimiter)token).Form == DelimiterType.comma;
        }

        static private bool IsTypeToken(Token token)
        {
            return (token is ReservedWord)
                && (((ReservedWord)token).WordType == ReservedWordType.INT || ((ReservedWord)token).WordType == ReservedWordType.LONG || ((ReservedWord)token).WordType == ReservedWordType.CHAR);
        }
        #endregion
    }
}
