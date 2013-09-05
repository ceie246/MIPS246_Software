using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIPS246.Core.Disassembler
{
    public enum AssemblerError
    {
        NOFILE, WRONGFORM, WRONGTAG/*, WRONGARGUNUM, ADDNOTFOUND, TWOADD0, WRONGREGNAME, WRONGSHAMT, UNKNOWNADDLABEL, INVALIDIMMEDIATE,
        WRONGARG, WRONGOFFSET*/
    }

    public class DisassemblerErrorInfo
    {
         #region Fields
        int line;
        AssemblerError assemblererror;
        string description;
        #endregion

        #region Constructors
        public DisassemblerErrorInfo(int line, AssemblerError assemblererror)
        {
            this.line = line;
            this.assemblererror = assemblererror;
        }

        public DisassemblerErrorInfo(int line, AssemblerError assemblererror, string description)
        {
            this.line = line;
            this.assemblererror = assemblererror;
            this.description = description;
        }
        #endregion

        #region Public Methods
        public void ConsoleDisplay()
        {
            string printline = (line + 1).ToString();
            switch (this.assemblererror)
            {
                case AssemblerError.NOFILE:
                    Console.WriteLine("Line 0: Could not found the source file.");
                    break;                
                case AssemblerError.WRONGFORM:
                    Console.WriteLine("Line " + printline + ": The Machinecode is invalid: " + this.description);
                    break;
                case AssemblerError.WRONGTAG:
                    Console.WriteLine("Line " + printline + ": The Machinecode is invalid: " + this.description);
                    break;               
                default:
                    break;
            }
        }

        public override string ToString()
        {
            string printline = (line + 1).ToString();
            switch (this.assemblererror)
            {
                case AssemblerError.NOFILE:
                    return "Line 0: Could not found the source file.";              
                case AssemblerError.WRONGFORM:
                    return "Line " + printline + ": The Machinecode is invalid: " + this.description;
                case AssemblerError.WRONGTAG:
                    Console.WriteLine("Line " + printline + ": The Machinecode is invalid: " + this.description);
                    break;    
                default:
                    break;
            }
            return base.ToString();
        }
        #endregion
    }
}
