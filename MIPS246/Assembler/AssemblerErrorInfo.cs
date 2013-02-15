using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIPS246.Core.Assembler
{
    enum AssemblerError
    {
        NOFILE, INVALIDLABEL, UNKNOWNCMD, TWOADD0, WRONGREGNAME, UNKNOWNADDLABEL
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
            switch (this.assemblererror)
            {
                case AssemblerError.NOFILE:
                    Console.WriteLine("Line 0: Could not found the source file.");
                    break;
                default:
                    break;
            }
        }
        #endregion

    }
}
