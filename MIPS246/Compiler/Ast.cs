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

        #region Virtual Method
        public virtual void Translate(VarTable varTable, LabelStack labelStack, List<FourExp> fourExpList)
        { 
            
        }
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

        #region Public Method
        public override void Translate(VarTable varTable, LabelStack labelStack, List<FourExp> fourExpList)
        {
            if (statement1 == null)
            {
                string label = labelStack.NewLabel();
                string condition = expression.GetValue(varTable, labelStack, fourExpList);
                fourExpList.Add(FourExpFac.GenJe(condition, 0+"", label));
                statement1.Translate(varTable, labelStack, fourExpList);
                fourExpList.Add(FourExpFac.GenLabel(label));
            }
            else
            {
                string label1 = labelStack.NewLabel();
                string label2 = labelStack.NewLabel();
                string condition = expression.GetValue(varTable, labelStack, fourExpList);
                fourExpList.Add(FourExpFac.GenJe(condition, 0 + "", label1));
                statement1.Translate(varTable, labelStack, fourExpList);
                fourExpList.Add(FourExpFac.GenJmp(label2));
                fourExpList.Add(FourExpFac.GenLabel(label1));
                statement2.Translate(varTable, labelStack, fourExpList);
                fourExpList.Add(FourExpFac.GenLabel(label2));
            }
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

        #region Public Method
        public override void Translate(VarTable varTable, LabelStack labelStack, List<FourExp> fourExpList)
        {
            string label1 = labelStack.NewLabel();
            string label2 = labelStack.NewLabel();
            fourExpList.Add(FourExpFac.GenLabel(label1));
            string condition = expression.GetValue(varTable, labelStack, fourExpList);
            fourExpList.Add(FourExpFac.GenJe(condition, 0 + "", label2));
            statement.Translate(varTable, labelStack, fourExpList);
            fourExpList.Add(FourExpFac.GenJmp(label1));
            fourExpList.Add(FourExpFac.GenLabel(label2));
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

        #region Public Method
        public override void Translate(VarTable varTable, LabelStack labelStack, List<FourExp> fourExpList)
        {
            string label = labelStack.NewLabel();
            fourExpList.Add(FourExpFac.GenLabel(label));
            statement.Translate(varTable, labelStack, fourExpList);
            string condition = expression.GetValue(varTable, labelStack, fourExpList);
            fourExpList.Add(FourExpFac.GenJne(condition, 0 + "", label));
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

        #region Public Method
        public override void Translate(VarTable varTable, LabelStack labelStack, List<FourExp> fourExpList)
        {

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

        #region Public Method
        public override void Translate(VarTable varTable, LabelStack labelStack, List<FourExp> fourExpList)
        {
            int value = 0;
            varTable.Add(this.name, this.type, value);
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

        #region Public Method
        public override void Translate(VarTable varTable, LabelStack labelStack, List<FourExp> fourExpList)
        {

        }
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

        #region Public Method
        public override void Translate(VarTable varTable, LabelStack labelStack, List<FourExp> fourExpList)
        {
            string value = expression.GetValue(varTable, labelStack, fourExpList);
            varTable.SetValue(this.identify, value);
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

        #region Public Method
        public override void Translate(VarTable varTable, LabelStack labelStack, List<FourExp> fourExpList)
        {

        }
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
        public virtual string GetValue(VarTable varTable, LabelStack labelStack, List<FourExp> fourExpList)
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
        public override string GetValue(VarTable varTable, LabelStack labelStack, List<FourExp> fourExpList)
        {
            string returnValue = "";
            //加、减、乘、除、按位与、按位或
            if (this.op.Type == OperatorType.add || this.op.Type == OperatorType.sub || this.op.Type == OperatorType.mul || this.op.Type == OperatorType.div || this.op.Type == OperatorType.bitand || this.op.Type == OperatorType.bitor)
            {
                string arg1 = expression1.GetValue(varTable, labelStack, fourExpList);
                string arg2 = expression2.GetValue(varTable, labelStack, fourExpList);
                string t = varTable.NewTemp(arg1, arg2);
                switch (this.op.Type)
                { 
                    case OperatorType.add:          //+
                        fourExpList.Add(FourExpFac.GenAdd(arg1, arg2, t));
                        break;
                    case OperatorType.sub:          //-
                        fourExpList.Add(FourExpFac.GenSub(arg1, arg2, t));
                        break;
                    case OperatorType.mul:          //*
                        fourExpList.Add(FourExpFac.GenMul(arg1, arg2, t));
                        break;
                    case OperatorType.div:          ///
                        fourExpList.Add(FourExpFac.GenDiv(arg1, arg2, t));
                        break;
                    case OperatorType.bitand:       //按位与
                        fourExpList.Add(FourExpFac.GenAnd(arg1, arg2, t));
                        break;
                    case OperatorType.bitor:        //按位或
                        fourExpList.Add(FourExpFac.GenOr(arg1, arg2, t));
                        break;
                    default:
                        break;
                }
                returnValue = t;
            }
            //与、或
            else if (this.op.Type == OperatorType.and || this.op.Type == OperatorType.or)
            {
                string label1 = labelStack.NewLabel();
                string arg1 = expression1.GetValue(varTable, labelStack, fourExpList);
                fourExpList.Add(FourExpFac.GenJe(arg1, 0 + "", label1));
                string t1 = varTable.NewTemp(VariableType.BOOL);
                fourExpList.Add(FourExpFac.GenMov(1 + "", t1));
                arg1 = t1;
                fourExpList.Add(FourExpFac.GenLabel(label1));

                string label2 = labelStack.NewLabel();
                string arg2 = expression2.GetValue(varTable, labelStack, fourExpList);
                fourExpList.Add(FourExpFac.GenJe(arg2, 0 + "", label2));
                string t2 = varTable.NewTemp(VariableType.BOOL);
                fourExpList.Add(FourExpFac.GenMov(1 + "", t2));
                arg2 = t2;
                fourExpList.Add(FourExpFac.GenLabel(label2));

                string t3 = varTable.NewTemp(VariableType.BOOL);
                switch (this.op.Type)
                { 
                    case OperatorType.and:      //与
                        fourExpList.Add(FourExpFac.GenAnd(arg1, arg2, t3));
                        break;
                    case OperatorType.or:       //或
                        fourExpList.Add(FourExpFac.GenOr(arg1, arg2, t3));
                        break;
                }
                returnValue = t3;
            }
            //左移、右移
            else if (this.op.Type == OperatorType.leftmove || this.op.Type == OperatorType.rightmove)
            {
                string arg1 = expression1.GetValue(varTable, labelStack, fourExpList);
                string arg2 = expression2.GetValue(varTable, labelStack, fourExpList);
                string t = varTable.NewTemp(arg1);
                switch (this.op.Type)
                { 
                    case OperatorType.leftmove:             //左移
                        fourExpList.Add(FourExpFac.GenLeftshift(arg1, arg2, t));
                        break;
                    case OperatorType.rightmove:            //右移
                        fourExpList.Add(FourExpFac.GenRightshift(arg1, arg2, t));
                        break;
                    default:
                        break;
                }
                returnValue = t;
            }
            //小于、小于等于、大于、大于等于、等于、不等于
            else if (this.op.Type == OperatorType.less || this.op.Type == OperatorType.lessequal || this.op.Type == OperatorType.greater || this.op.Type == OperatorType.greatereuqal || this.op.Type == OperatorType.equal || this.op.Type == OperatorType.notequal)
            {
                string label1 = labelStack.NewLabel();
                string label2 = labelStack.NewLabel();
                string t = varTable.NewTemp(VariableType.BOOL);
                string arg1 = expression1.GetValue(varTable, labelStack, fourExpList);
                string arg2 = expression2.GetValue(varTable, labelStack, fourExpList);
                switch (this.op.Type)
                { 
                    case OperatorType.less:         //<
                        fourExpList.Add(FourExpFac.GenJl(arg1, arg2, label1));
                        break;
                    case OperatorType.lessequal:    //<=
                        fourExpList.Add(FourExpFac.GenJle(arg1, arg2, label1));
                        break;
                    case OperatorType.greater:      //>
                        fourExpList.Add(FourExpFac.GenJg(arg1, arg2, label1));
                        break;
                    case OperatorType.greatereuqal: //>=
                        fourExpList.Add(FourExpFac.GenJge(arg1, arg2, label1));
                        break;
                    case OperatorType.equal:        //==
                        fourExpList.Add(FourExpFac.GenJe(arg1, arg2, label1));
                        break;
                    case OperatorType.notequal:     //!=
                        fourExpList.Add(FourExpFac.GenJne(arg1, arg2, label1));
                        break;
                    default:
                        //
                        break;
                }
                fourExpList.Add(FourExpFac.GenMov(0 + "", t));
                fourExpList.Add(FourExpFac.GenJmp(label2));
                fourExpList.Add(FourExpFac.GenLabel(label1));
                fourExpList.Add(FourExpFac.GenMov(1 + "", t));
                fourExpList.Add(FourExpFac.GenLabel(label2));
                returnValue = t;
            }
            else
            { 
                //错误处理
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
        public override string GetValue(VarTable varTable, LabelStack labelStack, List<FourExp> fourExpList)
        {
            string returnValue = "";
            string label, t1, t2, var;
            switch (this.op.Type)
            { 
                case OperatorType.not:          //非
                    label = labelStack.NewLabel();
                    var = expression.GetValue(varTable, labelStack, fourExpList);
                    fourExpList.Add(FourExpFac.GenJe(var, 0 + "", label));
                    t1 = varTable.NewTemp(VariableType.BOOL);
                    fourExpList.Add(FourExpFac.GenMov(1 + "", t1));
                    var = t1;
                    fourExpList.Add(FourExpFac.GenLabel(label));
                    t2 = varTable.NewTemp(VariableType.BOOL);
                    fourExpList.Add(FourExpFac.GenNot(t1, t2));
                    returnValue = t2;
                    break;
                case OperatorType.selfaddhead:      //自增前置
                    var = expression.GetValue(varTable, labelStack, fourExpList);
                    fourExpList.Add(FourExpFac.GenAdd(var, 1 + "", var));
                    returnValue = var;
                    break;
                case OperatorType.selfsubhead:      //自减前置
                    var = expression.GetValue(varTable, labelStack, fourExpList);
                    fourExpList.Add(FourExpFac.GenSub(var, 1 + "", var));
                    returnValue = var;
                    break;
                case OperatorType.selfaddtail:      //自增后置
                    var = expression.GetValue(varTable, labelStack, fourExpList);
                    t1 = varTable.NewTemp(var);
                    fourExpList.Add(FourExpFac.GenAdd(var, 1 + "", var));
                    returnValue = t1;
                    break;
                case OperatorType.selfsubtail:      //自减后置
                    var = expression.GetValue(varTable, labelStack, fourExpList);
                    t1 = varTable.NewTemp(var);
                    fourExpList.Add(FourExpFac.GenSub(var, 1 + "", var));
                    returnValue = t1;
                    break;
                case OperatorType.bitnot:       //按位非
                    var = expression.GetValue(varTable, labelStack, fourExpList);
                    t1 = varTable.NewTemp(var);
                    fourExpList.Add(FourExpFac.GenNot(var, t1));
                    returnValue = t1;
                    break;
                case OperatorType.neg:          //取反
                    var = expression.GetValue(varTable, labelStack, fourExpList);
                    t1 = varTable.NewTemp(var);
                    fourExpList.Add(FourExpFac.GenNeg(var, t1));
                    returnValue = t1;
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
        public override string GetValue(VarTable varTable, LabelStack labelStack, List<FourExp> fourExpList)
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
        public override string GetValue(VarTable varTable, LabelStack labelStack, List<FourExp> fourExpList)
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
        public override string GetValue(VarTable varTable, LabelStack labelStack, List<FourExp> fourExpList)
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
        public override string GetValue(VarTable varTable, LabelStack labelStack, List<FourExp> fourExpList)
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
        public override string GetValue(VarTable varTable, LabelStack labelStack, List<FourExp> fourExpList)
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
        public override string GetValue(VarTable varTable, LabelStack labelStack, List<FourExp> fourExpList)
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
        public override string GetValue(VarTable varTable, LabelStack labelStack, List<FourExp> fourExpList)
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
        public override string GetValue(VarTable varTable, LabelStack labelStack, List<FourExp> fourExpList)
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
