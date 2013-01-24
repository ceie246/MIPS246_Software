using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIPS246.Core.Assembler
{
    enum AssemblerError
    {
        NOFILE
    }

    class AssemblerErrorInfo
    {
        #region Fields
        int line;
        AssemblerError assemblererror;
        string description;
        #endregion

        #region Constructors
        public AssemblerErrorInfo(int line, AssemblerError assemblererror, string description)
        {
            this.line = line;
            this.assemblererror = assemblererror;
            this.description = description;
        }
        #endregion

        #region Properties
        public int Line
        {
            get
            {
                return this.line;
            }
        }

        public AssemblerError Assemblererror
        {
            get
            {
                return this.assemblererror;
            }
        }

        public string Description
        {
            get
            {
                return this.description;
            }
        }
        #endregion

    }
}
