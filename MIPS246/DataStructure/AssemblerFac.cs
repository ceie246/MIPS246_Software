using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIPS246.Core.DataStructure
{
    public static class AssemblerFac
    {
        #region Private Method
        private static AssemblerIns genrdrsrt(string operation, string rd, string rs, string rt)
        {
            AssemblerIns a = new AssemblerIns();
            a.Op = operation;
            a.Rd = rd;
            a.Rs = rs;
            a.Rt = rt;
            return a;
        }

        private static AssemblerIns genrdrtshamt(string operation, string rd, string rt, string shamt)
        {
            AssemblerIns a = new AssemblerIns();
            a.Op = operation;
            a.Rd = rd;
            a.Rt = rt;
            a.Shamt = a.Shamt;
            return a;
        }

        private static AssemblerIns genrdrtrs(string operation, string rd, string rt, string rs)
        {
            AssemblerIns a = new AssemblerIns();
            a.Op = operation;
            a.Rd = rd;
            a.Rt = rt;
            a.Rs = rs;
            return a;
        }

        private static AssemblerIns genrs(string operation, string rs)
        {
            AssemblerIns a = new AssemblerIns();
            a.Op = operation;
            a.Rs = rs;
            return a;
        }

        private static AssemblerIns genrtrsimm(string operation, string rt, string rs, string immediate)
        {
            AssemblerIns a = new AssemblerIns();
            a.Op = operation;
            a.Rt = rt;
            a.Rs = rs;
            a.Immediate = immediate;
            return a;
        }

        private static AssemblerIns genrtimm(string operation, string rt, string immediate)
        {
            AssemblerIns a = new AssemblerIns();
            a.Op = operation;
            a.Rt = rt;
            a.Immediate = immediate;
            return a;
        }

        private static AssemblerIns genrtoffsetrs(string operation, string rt, string offset, string rs)
        {
            AssemblerIns a = new AssemblerIns();
            a.Op = operation;
            a.Rt = rt;
            a.Offset = offset;
            a.Rs = rs;
            return a;
        }

        private static AssemblerIns genrsrtoffset(string operation, string rs, string rt, string offset)
        {
            AssemblerIns a = new AssemblerIns();
            a.Op = operation;
            a.Rs = rs;
            a.Rt = rt;
            a.Offset = offset;
            return a;
        }

        private static AssemblerIns genrsrtlabel(string operation, string rs, string rt, string label)
        {
            AssemblerIns a = new AssemblerIns();
            a.Op = operation;
            a.Rs = rs;
            a.Rt = rt;
            a.Label = label;
            return a;
        }

        private static AssemblerIns genrsoffset(string operation, string rs, string offset)
        {
            AssemblerIns a = new AssemblerIns();
            a.Op = operation;
            a.Rs = rs;
            a.Offset = offset;
            return a;
        }

        private static AssemblerIns genrslabel(string operation, string rs, string label)
        {
            AssemblerIns a = new AssemblerIns();
            a.Op = operation;
            a.Rs = rs;
            a.Offset = label;
            return a;
        }

        private static AssemblerIns genaddress(string operation, string address)
        {
            AssemblerIns a = new AssemblerIns();
            a.Op = operation;
            a.Address = address;
            return a;
        }

        private static AssemblerIns genrdrt(string operation, string rd, string rt)
        {
            AssemblerIns a = new AssemblerIns();
            a.Op = operation;
            a.Rd = rd;
            a.Rt = rt;
            return a;
        }

        private static AssemblerIns gennull(string operation)
        {
            AssemblerIns a = new AssemblerIns();
            a.Op = operation;
            return a;
        }

        private static AssemblerIns genrtlabel(string operation, string rt, string label)
        {
            AssemblerIns a = new AssemblerIns();
            a.Op = operation;
            a.Rt = rt;
            a.Label = label;
            return a;
        }

        private static AssemblerIns genlabel(string operation, string label)
        {
            AssemblerIns a = new AssemblerIns();
            a.Op = operation;
            a.Label = label;
            return a;
        }
        #endregion

        #region Public Method
        public static AssemblerIns GenADD(string rd, string rs, string rt)
        {
            return genrdrsrt("ADD", rd, rs, rt);
        }

        public static AssemblerIns GenADDU(string rd, string rs, string rt)
        {
            return genrdrsrt("ADDU", rd, rs, rt);
        }

        public static AssemblerIns GenSUB(string rd, string rs, string rt)
        {
            return genrdrsrt("SUB", rd, rs, rt);
        }

        public static AssemblerIns GenSUBU(string rd, string rs, string rt)
        {
            return genrdrsrt("SUBU", rd, rs, rt);
        }

        public static AssemblerIns GenAND(string rd, string rs, string rt)
        {
            return genrdrsrt("AND", rd, rs, rt);
        }

        public static AssemblerIns GenOR(string rd, string rs, string rt)
        {
            return genrdrsrt("OR", rd, rs, rt);
        }

        public static AssemblerIns GenXOR(string rd, string rs, string rt)
        {
            return genrdrsrt("XOR", rd, rs, rt);
        }

        public static AssemblerIns GenNOR(string rd, string rs, string rt)
        {
            return genrdrsrt("NOR", rd, rs, rt);
        }

        public static AssemblerIns GenSLT(string rd, string rs, string rt)
        {
            return genrdrsrt("SLT", rd, rs, rt);
        }

        public static AssemblerIns GenSLTU(string rd, string rs, string rt)
        {
            return genrdrsrt("SLTU", rd, rs, rt);
        }

        public static AssemblerIns GenSLL(string rd, string rt, string shamt)
        {
            return genrdrtshamt("SLL", rd, rt, shamt);
        }

        public static AssemblerIns GenSRL(string rd, string rt, string shamt)
        {
            return genrdrtshamt("SRL", rd, rt, shamt);
        }

        public static AssemblerIns GenSRA(string rd, string rt, string shamt)
        {
            return genrdrtshamt("SRA", rd, rt, shamt);
        }

        public static AssemblerIns GenSLLV(string rd, string rt, string rs)
        {
            return genrdrtrs("SLLV", rd, rt, rs);
        }

        public static AssemblerIns GenSRLV(string rd, string rt, string rs)
        {
            return genrdrtrs("SRLV", rd, rt, rs);
        }

        public static AssemblerIns GenSRAV(string rd, string rt, string rs)
        {
            return genrdrtrs("SRAV", rd, rt, rs);
        }

        public static AssemblerIns GenJR(string rs)
        {
            return genrs("JR", rs);
        }

        public static AssemblerIns GenJALR(string rs)
        {
            return genrs("JALR", rs);
        }

        public static AssemblerIns GenADDI(string rt, string rs, string immediate)
        {
            return genrtrsimm("ADDI", rt, rs, immediate);
        }

        public static AssemblerIns GenADDIU(string rt, string rs, string immediate)
        {
            return genrtrsimm("ADDIU", rt, rs, immediate);
        }

        public static AssemblerIns GenANDI(string rt, string rs, string immediate)
        {
            return genrtrsimm("ANDI", rt, rs, immediate);
        }

        public static AssemblerIns GenORI(string rt, string rs, string immediate)
        {
            return genrtrsimm("ORI", rt, rs, immediate);
        }

        public static AssemblerIns GenXORI(string rt, string rs, string immediate)
        {
            return genrtrsimm("XORI", rt, rs, immediate);
        }

        public static AssemblerIns GenLUI(string rt, string immediate)
        {
            return genrtimm("LUI", rt, immediate);
        }

        public static AssemblerIns GenSLTI(string rt, string rs, string immediate)
        {
            return genrtrsimm("SLTI", rt, rs, immediate);
        }

        public static AssemblerIns GenSLTIU(string rt, string rs, string immediate)
        {
            return genrtrsimm("SLTIU", rt, rs, immediate);
        }

        public static AssemblerIns GenLW(string rt, string offset, string rs)
        {
            return genrtoffsetrs("LW", rt, offset, rs);
        }

        public static AssemblerIns GenSW(string rt, string offset, string rs)
        {
            return genrtoffsetrs("SW", rt, offset, rs);
        }

        public static AssemblerIns GenLB(string rt, string offset, string rs)
        {
            return genrtoffsetrs("LB", rt, offset, rs);
        }

        public static AssemblerIns GenLBU(string rt, string offset, string rs)
        {
            return genrtoffsetrs("LBU", rt, offset, rs);
        }

        public static AssemblerIns GenLH(string rt, string offset, string rs)
        {
            return genrtoffsetrs("LH", rt, offset, rs);
        }

        public static AssemblerIns GenLHU(string rt, string offset, string rs)
        {
            return genrtoffsetrs("LHU", rt, offset, rs);
        }

        public static AssemblerIns GenSB(string rt, string offset, string rs)
        {
            return genrtoffsetrs("SB", rt, offset, rs);
        }

        public static AssemblerIns GenSH(string rt, string offset, string rs)
        {
            return genrtoffsetrs("SH", rt, offset, rs);
        }

        public static AssemblerIns GenBEQ(string rs, string rt, string label)
        {
            return genrsrtlabel("BEQ", rs, rt, label);
        }

        public static AssemblerIns GenBNE(string rs, string rt, string label)
        {
            return genrsrtlabel("BNE", rs, rt, label);
        }

        public static AssemblerIns GenBGEZ(string rs, string label)
        {
            return genrslabel("BGEZ", rs, label);
        }

        public static AssemblerIns GenBGEZAL(string rs, string label)
        {
            return genrslabel("BGEZAL", rs, label);
        }

        public static AssemblerIns GenBGTZ(string rs, string label)
        {
            return genrslabel("BGTZ", rs, label);
        }

        public static AssemblerIns GenBLEZ(string rs, string label)
        {
            return genrslabel("BLEZ", rs, label);
        }

        public static AssemblerIns GenBLTZ(string rs, string label)
        {
            return genrslabel("BLTZ", rs, label);
        }

        public static AssemblerIns GenBLTZAL(string rs, string label)
        {
            return genrslabel("BLTZAL", rs, label);
        }

        public static AssemblerIns GenJ(string address)
        {
            return genaddress("J", address);
        }

        public static AssemblerIns GenJAL(string address)
        {
            return genaddress("JAL", address);
        }

        public static AssemblerIns GenSUBI(string rt, string rs, string immediate)
        {
            return genrtrsimm("SUBI", rt, rs, immediate);
        }

        public static AssemblerIns GenMOVE(string rd, string rt)
        {
            return genrdrt("MOVE", rd, rt);
        }

        public static AssemblerIns GenNOP()
        {
            return gennull("NOP");
        }

        public static AssemblerIns GenLI(string rt, string immediate)
        {
            return genrtimm("LI", rt, immediate);
        }

        public static AssemblerIns GenLA(string rt, string label)
        {
            return genrtlabel("LA", rt, label);
        }

        public static AssemblerIns GenLABEL(string label)
        {
            return genlabel("LABEL", label);
        }

        public static AssemblerIns GenSYSCALL()
        {
            return gennull("SYSCALL");
        }

        public static AssemblerIns GenJUMP(string operation, string rs, string label)
        {
            return genrslabel(operation, rs, label);
        }

        public static AssemblerIns GenMathOrLog(string operation, string rd, string rs, string rt)
        {
            return genrdrsrt(operation, rd, rs, rt);
        }

        public static List<string> ConvertToStrList(List<AssemblerIns> cmdList)
        {
            List<string> cmdStrList = new List<string>();
            foreach (AssemblerIns a in cmdList)
            {
                cmdStrList.Add(a.ToString());
            }
            return cmdStrList;
        }
        #endregion
    }
}
