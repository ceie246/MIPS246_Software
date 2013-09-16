using System;


//assembler error info model
namespace MIPS246.Core.Assembler
{
    //error type
    public enum AssemblerError
    {
        NOFILE, INVALIDLABEL, UNKNOWNCMD, WRONGARGUNUM, ADDNOTFOUND, TWOADD0, WRONGREGNAME, WRONGSHAMT, UNKNOWNADDLABEL, INVALIDIMMEDIATE,
        WRONGARG, WRONGOFFSET
    }

    public class AssemblerErrorInfo
    {
        #region Fields
        int line;
        AssemblerError assemblererror;
        string description;
        #endregion

        #region Constructors
        public AssemblerErrorInfo(int line, AssemblerError assemblererror)
        {
            this.line = line;
            this.assemblererror = assemblererror;
        }

        public AssemblerErrorInfo(int line, AssemblerError assemblererror, string description)
        {
            this.line = line;
            this.assemblererror = assemblererror;
            this.description = description;
        }
        #endregion

        #region Public Methods
        //error message in console
        public void ConsoleDisplay()
        {
            string printline = (line + 1).ToString();
            switch (this.assemblererror)
            {
                case AssemblerError.NOFILE:
                    Console.WriteLine("Line 0: Could not found the source file.");
                    break;
                case AssemblerError.INVALIDLABEL:
                    Console.WriteLine("Line " + printline + ": The address label is invalid: " + this.description);
                    break;
                case AssemblerError.UNKNOWNCMD:
                    Console.WriteLine("Line " + printline + ": The Mnemonic is invalid: " + this.description);
                    break;
                case AssemblerError.WRONGARGUNUM:
                    Console.WriteLine("Line " + printline + ": The Mnemonic takes " + this.description + " arguments.");
                    break;
                case AssemblerError.ADDNOTFOUND:
                    Console.WriteLine("Line " + printline + ": The address label is not found in the source file: " + this.description);
                    break;
                case AssemblerError.WRONGREGNAME:
                    Console.WriteLine("Line " + printline + ": The Register name is invalid.");
                    break;
                case AssemblerError.WRONGSHAMT:
                    Console.WriteLine("Line " + printline + ": The Shamt should between 0 to 31. Shamt:" + this.description);
                    break;
                case AssemblerError.INVALIDIMMEDIATE:
                    Console.WriteLine("Line " + printline + ": The immediate formate is incorrect: " + this.description);
                    break;
                case AssemblerError.WRONGARG:
                    Console.WriteLine("Line " + printline + ": The Args are incorrect: " + this.description);
                    break;
                case AssemblerError.WRONGOFFSET:
                    Console.WriteLine("Line " + printline + ": The Offset are incorrect: " + this.description);
                    break;
                default:
                    break;
            }
        }

        //error message rawdata
        public override string ToString()
        {
            string printline = (line + 1).ToString();
            switch (this.assemblererror)
            {
                case AssemblerError.NOFILE:
                    return "Line 0: Could not found the source file.";
                case AssemblerError.INVALIDLABEL:
                    return "Line " + printline + ": The address label is invalid: " + this.description;
                case AssemblerError.UNKNOWNCMD:
                    return "Line " + printline + ": The Mnemonic is invalid: " + this.description;
                case AssemblerError.WRONGARGUNUM:
                    return "Line " + printline + ": The Mnemonic takes " + this.description + " arguments.";
                case AssemblerError.ADDNOTFOUND:
                    return "Line " + printline + ": The address label is not found in the source file: " + this.description;
                case AssemblerError.WRONGREGNAME:
                    return "Line " + printline + ": The Register name is invalid.";
                case AssemblerError.WRONGSHAMT:
                    return "Line " + printline + ": The Shamt should between 0 to 31. Shamt:" + this.description;
                case AssemblerError.INVALIDIMMEDIATE:
                    return "Line " + printline + ": The immediate formate is incorrect: " + this.description;
                case AssemblerError.WRONGARG:
                    return  "Line " + printline + ": The Args are incorrect: " + this.description;
                case AssemblerError.WRONGOFFSET:
                    return "Line " + printline + ": The Offset are incorrect: " + this.description;
                default:
                    break;
            }
            return base.ToString();
        }
        #endregion

    }
}
