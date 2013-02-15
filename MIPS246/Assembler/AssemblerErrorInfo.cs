using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIPS246.Core.Assembler
{
    enum AssemblerError
    {
        NOFILE, INVALIDLABEL, UNKNOWNCMD, WRONGARGUNUM, ADDNOTFOUND, TWOADD0, WRONGREGNAME, WRONGSHAMT, UNKNOWNADDLABEL
    }

    class AssemblerErrorInfo
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
        public void Display()
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
                default:
                    break;
            }
        }
        #endregion

    }
}
