using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MIPS246.Core.DataStructure;

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
        private Expression expression;
        private Statement statement1;
        private Statement statement2;
        #endregion

        #region Constructors
        public IfStatment(Expression expression, Statement statement1, Statement statement2)
        {
            this.expression = expression;
            this.statement1 = statement1;
            this.statement2 = statement2;
        }
        #endregion

        #region Properties

        public Expression Expression
        {
            get { return expression; }
            set { expression = value; }
        }

        public Statement Statement1
        {
            get { return statement1; }
            set { statement1 = value; }
        }

        public Statement Statement2
        {
            get { return statement2; }
            set { statement2 = value; }
        }
        #endregion
    }

    public class WhileStatment : Statement
    {
        #region Fields
        private Expression expression;
        private Statement statement;
        #endregion

        #region Constructors
        public WhileStatment(Expression expression, Statement statement)
        {
            this.expression = expression;
            this.statement = statement;
        }
        #endregion

        #region Properties
        public Expression Expression
        {
            get { return expression; }
            set { expression = value; }
        }

        public Statement Statement
        {
            get { return statement; }
            set { statement = value; }
        }
        #endregion
    }

    public class DoWhileStatment : Statement
    {
        #region Fields
        private Expression expression;
        private Statement statement;
        #endregion

        #region Constructors
        public DoWhileStatment(Statement statement, Expression expression)
        {
            this.statement = statement;
            this.expression = expression;
        }
        #endregion

        #region Properties
        public Expression Expression
        {
            get { return expression; }
            set { expression = value; }
        }

        public Statement Statement
        {
            get { return statement; }
            set { statement = value; }
        }
        #endregion
    }

    public class ForStatement : Statement
    {
        #region Fields
        private Expression expression1, expression2, expression3;
        private Statement statement;
        #endregion

        #region Constructors
        public ForStatement(Expression expression1, Expression expression2, Expression expression3, Statement statement)
        {
            this.expression1 = expression1;
            this.expression2 = expression2;
            this.expression3 = expression3;
        }
        #endregion

        #region Properties
        public Expression Expression3
        {
            get { return expression3; }
            set { expression3 = value; }
        }

        public Expression Expression2
        {
            get { return expression2; }
            set { expression2 = value; }
        }

        public Expression Expression1
        {
            get { return expression1; }
            set { expression1 = value; }
        }

        public Statement Statement1
        {
            get { return statement; }
            set { statement = value; }
        }
        #endregion
    }

    public class FieldDefineStatement : Statement
    {
        #region Fields
        private VariableType type;
        private string name;
        #endregion

        #region Constructors
        public FieldDefineStatement(VariableType type, string name)
        {
            this.type = type;
            this.name = name;
        }
        #endregion

        #region Properties
        public VariableType Type
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
        private string identify;
        private Expression expression;
        #endregion

        #region Constructors
        public AssignStatement(string identify, Expression expression)
        {
            this.identify = identify;
            this.expression = expression;
        }
        #endregion

        #region Properties
        public string Identify
        {
            get { return identify; }
            set { identify = value; }
        }

        public Expression Expression
        {
            get { return expression; }
            set { expression = value; }
        }
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
        public virtual string GetValue(VarTable vartable, LabelStack labelStack, List<FourExp> fourExpList)
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
        public override string GetValue(VarTable vartable, LabelStack labelStack, List<FourExp> fourExpList)
        {
            string returnValue = "";
            string arg1, arg2;
            switch (this.op.Type)
            { 
                case OperatorType.add://加
                    break;
                case OperatorType.sub: //减
                    break;
                case OperatorType.mul://乘
                    break;
                case OperatorType.div://除
                    break;
                case OperatorType.and://与
                    break;
                case OperatorType.or://或
                    break;
                default:
                    //错误处理
                    break;
            }
            return returnValue;
        }
        #endregion
    }

    public class SingleOPExpression : Expression
    {
        #region Fields
        private Expression expression;
        private Operator op;
        #endregion

        #region Constructors
        public SingleOPExpression(Expression expression, Operator op)
        {
            this.expression = expression;
            this.op = op;
        }
        #endregion

        #region Properties
        public Expression Expression1
        {
            get { return expression; }
            set { expression = value; }
        }

        public Operator Op
        {
            get { return op; }
            set { op = value; }
        }
        #endregion

        #region Public Method
        public override string GetValue(VarTable vartable, LabelStack labelStack, List<FourExp> fourExpList)
        {
            string returnValue = "";
            switch (this.op.Type)
            { 
                case OperatorType.not:          //非
                    string label = labelStack.newLabel();
                    string t1 = vartable.NewTemp(VariableType.BOOL, expression.GetValue(vartable, labelStack, fourExpList));
                    fourExpList.Add(FourExpFac.GenJe(t1, "0", label));
                    fourExpList.Add(FourExpFac.GenMov("1", t1));
                    fourExpList.Add(FourExpFac.GenLabel(label));
                    string t2 = vartable.NewTemp(VariableType.BOOL);
                    fourExpList.Add(FourExpFac.GenNot(t1, t2));
                    returnValue = t2;
                    break;
                case OperatorType.selfadd:      //自增
                    break;
                case OperatorType.selfsub:      //自减
                    break;
                case OperatorType.bitnot:       //按位非
                    break;
                case OperatorType.neg:          //取反
                    break;
            }
            return returnValue;
        }
        #endregion
    }

    public class ArrayGetExpression : Expression
    {
        #region Fields
        private string identifier;
        private int offset;
        #endregion

        #region Constructors
        public ArrayGetExpression(string identifier, int offset)
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

        #region Public Method
        public override string GetValue(VarTable vartable, LabelStack labelStack, List<FourExp> fourExpList)
        {
            return "";
        }
        #endregion
    }

    public class TrueExpression : Expression
    {
        #region Fields
        private bool value;
        #endregion

        #region Constructors
        public TrueExpression(bool value)
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

        #region Public Method
        public override string GetValue(VarTable vartable, LabelStack labelStack, List<FourExp> fourExpList)
        {
            return 1+"";
        }
        #endregion
    }

    public class FalseExpression : Expression
    {
        #region Fields
        private bool value;
        #endregion

        #region Constructors
        public FalseExpression(bool value)
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

        #region Public Method
        public override string GetValue(VarTable vartable, LabelStack labelStack, List<FourExp> fourExpList)
        {
            return 0 + "";
        }
        #endregion
    } 

    //This should be contained in SingleOPExpression
    //public class NotExpression : Expression
    //{
    //    #region Fields
    //    private Expression expression;
    //    #endregion

    //    #region Constructors
    //    public NotExpression(Expression expression)
    //    {
    //        this.expression = expression;
    //    }
    //    #endregion

    //    #region Properties
    //    public Expression Expression
    //    {
    //        get
    //        {
    //            return this.expression;
    //        }
    //        set
    //        {
    //            this.expression = value;
    //        }
    //    }
    //    #endregion
    //}

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

        #region Public Method
        public override string GetValue(VarTable vartable, LabelStack labelStack, List<FourExp> fourExpList)
        {
            return this.name;
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

        #region Public Method
        public override string GetValue(VarTable vartable, LabelStack labelStack, List<FourExp> fourExpList)
        {
            return "";
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

        #region Public Method
        public override string GetValue(VarTable vartable, LabelStack labelStack, List<FourExp> fourExpList)
        {
            return this.value.ToString(); 
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

        #region Public Method
        public override string GetValue(VarTable vartable, LabelStack labelStack, List<FourExp> fourExpList)
        {
            return this.value.ToString(); 
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

        #region Public Method
        public override string GetValue(VarTable vartable, LabelStack labelStack, List<FourExp> fourExpList)
        {
            return this.value.ToString();            
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
    //public enum IdentifierType this already exists in MIPS.CORE.DATESTRUCTURE.VariableType
    //{
    //    CHAR, //8bit
    //    INT, //16bit
    //    LONG //32bit
    //}
    #endregion
    

    
}
