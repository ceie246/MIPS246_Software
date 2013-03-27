using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIPS246.Core.Compiler
{
    public enum ReservedWordType
    {
        //Comment if not realize
        //AUTO,
        BREAK,
        //CASE,     soon
        CHAR,
        CONST,
        CONTINUE,
        //DEFAULT,  soon
        DO,
        DOUBLE,
        ELSE,
        //ENUM,  soon
        //EXTERN,
        //FLOAT,
        FOR,
        //GOTO,
        IF,
        INT,
        LONG,
        REGISTER,
        RETURN,
        //SHORT,
        SIGNED,
        //SIZEOF,
        STATIC,
        //STRUCT,
        //SWITCH,
        //TYPEDEF,
        //UNION,
        //UNSIGNED,
        VOID,
        //VOLATILE,
        WHILE
    }

    public enum OperatorType
    {
        
        //[
        //]
        //->
        //.
        //!
        //++
        //--
        //*
        //&
        //~
        //+
        //-
        //sizeof
        //>>
        //<<
        //<
        //<=
        //>
        //>=
        //==
        //!=
        //&
        //^
        //|
        //&&
        //||
        //?:
        //=
        //+=
        //-=
        //*=
        ///=
        //%=
        //&=
        //|=
        //^=
        //,        
    }

    public enum DelimiterType
    {
        //(
        //)
        //{
        //}
        //;
        ////
        ///*
        ////
        //*/
    }

    public abstract class Token
    {     

    }

    public class Identifier : Token
    {
    }

    public class ReservedWord : Token
    {
        ReservedWordType type;
    }

    public class Number : Token
    {

    }

    public class Delimiter : Token
    {

    }

    public class Operator : Token
    {
        OperatorType type;
    }
}
