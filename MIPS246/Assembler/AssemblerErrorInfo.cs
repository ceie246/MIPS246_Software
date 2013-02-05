using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIPS246.Core.Assembler
{
    enum AssemblerError
    {
        NOFILE,UNKNOWNCMD,TWOADD0,WRONGREGNAME,UNKNOWNADDLABEL
    }

    class AssemblerErrorInfo
    {
        #region Fields
        uint line;
        AssemblerError assemblererror;
        string description;
        #endregion

        #region Constructors
        public AssemblerErrorInfo(uint line, AssemblerError assemblererror, string description)
        {
            this.line = line;
            this.assemblererror = assemblererror;
            this.description = description;
        }
        #endregion

        #region Properties
        public uint Line
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
