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
        #endregion

        #region Constructors
        public AssemblerErrorInfo(int line, AssemblerError assemblererror)
        {
            this.line = line;
            this.assemblererror = assemblererror;
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
        #endregion

    }
}
