using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIPS246.Core.Compiler
{
    #region Expression classes
    public class Ast
    {

    }

    public class Statement
    {
        #region Fields
        #endregion

        #region Constructors
        #endregion

        #region Properties
        #endregion
    }

    public class StatementList : Statement
    {
    }

    public class IfStatment : Statement
    {
    }

    public class WhileStatment : Statement
    {
    }

    public class DoWhileStatment : Statement
    {
    }

    public class ForStatement : Statement
    {
    }

    public class FieldDefineStatment : Statement
    {

    }

    public class FieldDefineStatment : Statement
    {
    }

    public class ArrayDefineStatement : Statement
    {
    }

    public class AssignStatement : Statement
    {
    }

    public class RestStatement : Statement
    {
    }



    public class Expression
    {
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
    }

    public class ArrayGet : Expression
    {
        #region Fields
        private string identify;
        private int offset;
        #endregion

        #region Constructors
        public ArrayGet(string identify, int offset)
        {
            this.identify = identify;
            this.offset = offset;
        }
        #endregion

        #region Properties
        public string Identify
        {
            get
            {
                return this.identify;
            }
            set
            {
                this.identify = value;
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
    

    
}
