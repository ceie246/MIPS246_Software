using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIPS246.Core.DataStructure
{
    internal class AssemblerIns
    {
        #region Private Fields
        private string op = "", rs = "", rt = "", rd = "", immediate = "", offset = "", shamt = "", address = "";
        #endregion

        #region Public Fields
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
        #endregion

        #region Public Method
        public string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.Op).Append(" ");
            switch (this.Op)
            {
                case "ADD":
                case "SUB":
                case "AND":
                case "OR":
                case "XOR":
                case "NOR":
                case "SLT":
                    sb.Append(this.Rd)
                        .Append(", ")
                        .Append(this.Rs)
                        .Append(", ")
                        .Append(this.Rt);
                    break;
                case "SRL":
                    sb.Append(this.Rd)
                        .Append(", ")
                        .Append(this.Rt)
                        .Append(", ")
                        .Append(this.Shamt);
                    break;
                case "ORI":
                case "XORI":
                    sb.Append(this.Rt)
                        .Append(", ")
                        .Append(this.Rs)
                        .Append(", ")
                        .Append(this.Immediate);
                    break;
                case "LUI":
                    sb.Append(this.Rt)
                        .Append(", ")
                        .Append(this.Immediate);
                    break;
                case "LW":
                case "SW":
                    sb.Append(this.rt)
                        .Append(", ")
                        .Append(this.Offset)
                        .Append("(")
                        .Append(this.Rs)
                        .Append(")");
                    break;
                case "BEQ":
                case "BNE":
                    sb.Append(this.Rs)
                        .Append(", ")
                        .Append(this.Rt)
                        .Append(", ")
                        .Append(this.Offset);
                    break;
                case "BGEZ":
                case "BGTZ":
                case "BLEZ":
                case "BLTZ":
                    sb.Append(this.Rs)
                        .Append(", ")
                        .Append(this.Offset);
                    break;
                case "J":
                    sb.Append(this.Address);
                    break;
                default:
                    //错误处理
                    break;
            }
            return sb.ToString();
        }
        #endregion
    }
}
