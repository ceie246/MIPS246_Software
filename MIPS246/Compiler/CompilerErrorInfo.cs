using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIPS246.Core.Compiler
{
    enum CompilerError
    {
        NOFILE, INVALIDLABEL, UNKNOWNCMD, WRONGARGUNUM, ADDNOTFOUND, TWOADD0, WRONGREGNAME, WRONGSHAMT, UNKNOWNADDLABEL, INVALIDIMMEDIATE,
        WRONGARG, WRONGOFFSET
    }

    class CompilerErrorInfo
    {
        #region Fields
        int line;
        CompilerError compilererror;
        string description;
        #endregion

        #region Constructors
        public CompilerErrorInfo(int line, CompilerError compilererror)
        {
            this.line = line;
            this.compilererror = compilererror;
        }

        public CompilerErrorInfo(int line, CompilerError compilererror, string description)
        {
            this.line = line;
            this.compilererror = compilererror;
            this.description = description;
        }
        #endregion

        #region Public Methods
        public void Display()
        {
            string printline = (line + 1).ToString();
            switch (this.compilererror)
            {
                
                default:
                    break;
            }
        }
    }
}
