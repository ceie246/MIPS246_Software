using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIPS246.Core.DataStructure
{
    internal class AssemblerIns
    {
        private string op = "", rs = "", rt = "", rd = "", immediate = "", offset = "", shamt = "", address = "";

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public string Shamt
        {
            get { return shamt; }
            set { shamt = value; }
        }

        public string Offset
        {
            get { return offset; }
            set { offset = value; }
        }

        public string Immediate
        {
            get { return immediate; }
            set { immediate = value; }
        }

        public string Rd
        {
            get { return rd; }
            set { rd = value; }
        }

        public string Rt
        {
            get { return rt; }
            set { rt = value; }
        }

        public string Rs
        {
            get { return rs; }
            set { rs = value; }
        }

        public string Op
        {
            get { return op; }
            set { op = value; }
        }



        #region Public Method
        public string ToString()
        {
            return "";
        }
        #endregion
    }
}
