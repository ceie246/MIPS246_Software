using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIPS246.Core.DataStructure
{
    public class AssemblerIns
    {
        #region Private Fields
        private string op = "", rs = "", rt = "", rd = "", immediate = "", offset = "", shamt = "", address = "", label = "";

        #endregion

        #region Public Fields

        public string Label
        {
            get { return label; }
            set { label = value; }
        }

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
        new public string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.Op).Append(" ");
            switch (this.Op)
            {
                case "ADD":
                case "ADDU":
                case "SUB":
                case "SUBU":
                case "AND":
                case "OR":
                case "XOR":
                case "NOR":
                case "SLT":
                case "SLTU":
                    sb.Append(this.Rd)
                        .Append(", ")
                        .Append(this.Rs)
                        .Append(", ")
                        .Append(this.Rt);
                    break;
                case "SLL":
                case "SRL":
                case "SRA":
                    sb.Append(this.Rd)
                        .Append(", ")
                        .Append(this.Rt)
                        .Append(", ")
                        .Append(this.Shamt);
                    break;
                case "SLLV":
                case "SRLV":
                case "SRAV":
                    sb.Append(this.Rd)
                        .Append(", ")
                        .Append(this.Rt)
                        .Append(", ")
                        .Append(this.Rs);
                    break;
                case "JR":
                case "JALR":
                    sb.Append(this.Rs);
                    break;
                case "ADDI":
                case "ADDIU":
                case "ANDI":
                case "ORI":
                case "XORI":
                case "SLTI":
                case "SLTIU":
                case "SUBI":
                    sb.Append(this.Rt)
                        .Append(", ")
                        .Append(this.Rs)
                        .Append(", ")
                        .Append(this.Immediate);
                    break;
                case "LUI":
                case "LI":
                    sb.Append(this.Rt)
                        .Append(", ")
                        .Append(this.Immediate);
                    break;
                case "LW":
                case "SW":
                case "LB":
                case "SBU":
                case "LH":
                case "LHU":
                case "SB":
                case "SH":
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
                case "BGEZAL":
                case "BGTZ":
                case "BLEZ":
                case "BLTZ":
                case "BLTZAL":
                    sb.Append(this.Rs)
                        .Append(", ")
                        .Append(this.Offset);
                    break;
                case "J":
                case "JAL":
                    sb.Append(this.Address);
                    break;
                case "MOVE":
                    sb.Append(this.Rd)
                        .Append(", ")
                        .Append(this.Rt);
                    break;
                case "NOP":
                case "SYSCALL":
                    break;
                case "LA":
                    sb.Append(this.Rt)
                        .Append(", ")
                        .Append(this.Label);
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
