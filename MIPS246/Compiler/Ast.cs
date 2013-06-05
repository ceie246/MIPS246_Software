using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIPS246.Core.Compiler.AstStructure
{
   
    public class Ast
    {
        #region Fields
        private List<Statement> statements;
        #endregion

        #region Constructors
        public Ast()
        {
            statements = new List<Statement>();
        }
        #endregion

        #region Properties
        public List<Statement> Statements
        {
            get
            {
                return this.statements;
            }
            set
            {
                this.statements = value;
            }
        }
        #endregion
    }

    #region Statement classes

    public class Statement
    {
        #region Fields
        #endregion

        #region Constructors
        #endregion

        #region Properties
        #endregion
    }

    public class IfStatment : Statement
    {
        #region Fields
        #endregion

        #region Constructors
        #endregion

        #region Properties
        #endregion
    }

    public class WhileStatment : Statement
    {
        #region Fields
        #endregion

        #region Constructors
        #endregion

        #region Properties
        #endregion
    }

    public class DoWhileStatment : Statement
    {
        #region Fields
        #endregion

        #region Constructors
        #endregion

        #region Properties
        #endregion
    }

    public class ForStatement : Statement
    {
        #region Fields
        #endregion

        #region Constructors
        #endregion

        #region Properties
        #endregion
    }

    public class FieldDefineStatement : Statement
    {
        #region Fields
        private IdentifierType type;
        private string name;
        #endregion

        #region Constructors
        public FieldDefineStatement(IdentifierType type, string name)
        {
            this.type = type;
            this.name = name;
        }
        #endregion

        #region Properties
        public IdentifierType Type
        {
            set
            {
                this.type = value;
            }
            get
            {
                return this.type;
            }
        }

        public string Name
        {
            set
            {
                this.name = value;
            }
            get
            {
                return this.name;
            }
        }
        #endregion
    }

    public class ArrayDefineStatement : Statement
    {
        #region Fields
        #endregion

        #region Constructors
        #endregion

        #region Properties
        #endregion
    }

    public class AssignStatement : Statement
    {
        #region Fields
        #endregion

        #region Constructors
        #endregion

        #region Properties
        #endregion
    }

    public class RestStatement : Statement
    {
        #region Fields
        #endregion

        #region Constructors
        #endregion

        #region Properties
        #endregion
    }

    #endregion    

    #region Expression classes
    public class Expression
    {
        #region Fields
        #endregion

        #region Constructors
        #endregion

        #region Properties
        #endregion

        #region Virtual Method
        public virtual string GetValue(VarTable vartable, Stack<string> labelStack, List<FourExp> fourExpList)
        {
            return "";
        }
        #endregion
    }

    public class OPExpression : Expression
    {
        #region Fields
        private Expression expression1;
        private Expression expression2;
        private Operator op;
        #endregion

        #region Constructors
        public OPExpression(Expression expression1, Expression expression2, Operator op)
        {
            this.expression1 = expression1;
            this.expression2 = expression2;
            this.op = op;
        }
        #endregion

        #region Properties
        public Expression Expression1
        {
            get
            {
                return this.Expression1;
            }
            set
            {
                this.expression1 = value;
            }
        }

        public Expression Expression2
        {
            get
            {
                return this.Expression2;
            }
            set
            {
                this.expression2 = value;
            }
        }

        public Operator Op
        {
            get
            {
                return this.op;
            }
            set
            {
                this.op = value;
            }
        }
        #endregion

        #region Public Method
        public virtual string GetValue(VarTable vartable, Stack<string> labelStack, List<FourExp> fourExpList)
        {
            string returnValue = vartable.newVar(VariableType.INT);
            string arg1, arg2;
            switch (this.op.Type)
            { 
                case OperatorType.add:
                    arg1 = this.expression1.GetValue(vartable, labelStack, fourExpList);
                    arg2 = this.expression2.GetValue(vartable, labelStack, fourExpList);
                    fourExpList.Add(FourExpFac.GenAdd(arg1, arg2, returnValue));
                    break;
                case OperatorType.sub: 
                    arg1 = this.expression1.GetValue(vartable, labelStack, fourExpList);
                    arg2 = this.expression2.GetValue(vartable, labelStack, fourExpList);
                    fourExpList.Add(FourExpFac.GenSub(arg1, arg2, returnValue));
                    break;
                case OperatorType.mul:
                    arg1 = this.expression1.GetValue(vartable, labelStack, fourExpList);
                    arg2 = this.expression2.GetValue(vartable, labelStack, fourExpList);
                    fourExpList.Add(FourExpFac.GenMul(arg1, arg2, returnValue));
                    break;
                case OperatorType.div:
                    arg1 = this.expression1.GetValue(vartable, labelStack, fourExpList);
                    arg2 = this.expression2.GetValue(vartable, labelStack, fourExpList);
                    fourExpList.Add(FourExpFac.GenDiv(arg1, arg2, returnValue));
                    break;
                case OperatorType.and:
                    break;
                case OperatorType.or:
                    break;
                default:
                    //错误处理
                    break;
            }
            return "";
        }
        #endregion
    }

    public class ArrayGet : Expression
    {
        #region Fields
        private string identifier;
        private int offset;
        #endregion

        #region Constructors
        public ArrayGet(string identifier, int offset)
        {
            this.identifier = identifier;
            this.offset = offset;
        }
        #endregion

        #region Properties
        public string Identifier
        {
            get
            {
                return this.identifier;
            }
            set
            {
                this.identifier = value;
            }
        }

        public int Offset
        {
            get
            {
                return this.offset;
            }
            set
            {
                this.offset = value;
            }
        }
        #endregion
    }

    public class BoolValue : Expression
    {
        #region Fields
        private bool value;
        #endregion

        #region Constructors
        public BoolValue(bool value)
        {
            this.value = value;
        }
        #endregion

        #region Properties
        public bool Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
            }
        }
        #endregion
    }    

    public class NotExpression : Expression
    {
        #region Fields
        private Expression expression;
        #endregion

        #region Constructors
        public NotExpression(Expression expression)
        {
            this.expression = expression;
        }
        #endregion

        #region Properties
        public Expression Expression
        {
            get
            {
                return this.expression;
            }
            set
            {
                this.expression = value;
            }
        }
        #endregion
    }

    public class IdentifyExpression : Expression
    {
        #region Fields
        private string name;
        private VariableType type;
        #endregion

        #region Constructors
        public IdentifyExpression(string name, VariableType type)
        {
            this.name = name;
            this.type = type;
        }
        #endregion

        #region Properties
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public VariableType Type
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
            }
        }
        #endregion  
    }

    public class IntValue : Expression
    {
        #region Fields
        private int value;
        #endregion

        #region Constructors
        public IntValue(int value)
        {
            this.value = value;
        }
        #endregion

        #region Properties
        public int Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
            }
        }
        #endregion
    }

    public class ArrayExpression : Expression
    {
        #region Fields
        private string name;
        private VariableType type;
        #endregion

        #region Constructors
        public ArrayExpression(string name, VariableType type)
        {
            this.name = name;
            this.type = type;
        }
        #endregion

        #region Properties
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public VariableType Type
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
            }
        }
        #endregion  
    }

    public class CharValue : Expression
    {
        #region Fields
        private char value;
        #endregion

        #region Constructors
        public CharValue(char value)
        {
            this.value = value;
        }
        #endregion

        #region Properties
        public char Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
            }
        }
        #endregion
    }

    public class LongValue : Expression
    {
        #region Fields
        private long value;
        #endregion

        #region Constructors
        public LongValue(long value)
        {
            this.value = value;
        }
        #endregion

        #region Properties
        public long Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
            }
        }
        #endregion
    }        

    public class RestExpression : Expression
    {
        #region Fields
        private Expression expression;
        #endregion

        #region Constructors
        public RestExpression(Expression expression)
        {
            this.expression = expression;
        }
        #endregion

        #region Properties
        public Expression Expression
        {
            get
            {
                return this.expression;
            }
            set
            {
                this.expression = value;
            }
        }
        #endregion
    }

    public class ParenthesesExpression : Expression
    {
        #region Fields
        private Expression expression;
        #endregion

        #region Constructors
        public ParenthesesExpression(Expression expression)
        {
            this.expression = expression;
        }
        #endregion

        #region Properties
        public Expression Expression
        {
            get
            {
                return this.expression;
            }
            set
            {
                this.expression = value;
            }
        }
        #endregion
    }
    #endregion

    #region IdentifierType
    public enum IdentifierType
    {
        INT,LONG,CHAR
    }
    #endregion
    

    
}
