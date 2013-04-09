using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIPS246.Core.DataStructure
{
    public static class AssemblerFac
    {
        public static AssemblerIns GenADD(string rd, string rs, string rt)
        {
            AssemblerIns a = new AssemblerIns();
            a.Op = "ADD";
            a.Rd = rd;
            a.Rs = rs;
            a.Rt = rt;
            return a;
        }

        public static AssemblerIns GenSUB(string rd, string rs, string rt)
        {
            AssemblerIns a = new AssemblerIns();
            a.Op = "SUB";
            a.Rd = rd;
            a.Rs = rs;
            a.Rt = rt;
            return a;
        }

        public static AssemblerIns GenAND(string rd, string rs, string rt)
        {
            AssemblerIns a = new AssemblerIns();
            a.Op = "AND";
            a.Rd = rd;
            a.Rs = rs;
            a.Rt = rt;
            return a;
        }

        public static AssemblerIns GenOR(string rd, string rs, string rt)
        {
            AssemblerIns a = new AssemblerIns();
            a.Op = "OR";
            a.Rd = rd;
            a.Rs = rs;
            a.Rt = rt;
            return a;
        }

        public static AssemblerIns GenXOR(string rd, string rs, string rt)
        {
            AssemblerIns a = new AssemblerIns();
            a.Op = "XOR";
            a.Rd = rd;
            a.Rs = rs;
            a.Rt = rt;
            return a;
        }

        public static AssemblerIns GenNOR(string rd, string rs, string rt)
        {
            AssemblerIns a = new AssemblerIns();
            a.Op = "NOR";
            a.Rd = rd;
            a.Rs = rs;
            a.Rt = rt;
            return a;
        }

        public static AssemblerIns GenMathOrLog(string operation, string rd, string rs, string rt)
        {
            AssemblerIns a = new AssemblerIns();
            a.Op = operation;
            a.Rd = rd;
            a.Rs = rs;
            a.Rt = rt;
            return a;
        }

        public static AssemblerIns GenSLT(string rd, string rs, string rt)
        {
            AssemblerIns a = new AssemblerIns();
            a.Op = "SLT";
            a.Rd = rd;
            a.Rs = rs;
            a.Rt = rt;
            return a;
        }

        public static AssemblerIns GenSRL(string rd, string rt, string shamt)
        {
            AssemblerIns a = new AssemblerIns();
            a.Op = "SRL";
            a.Rd = rd;
            a.Rt = rt;
            a.Shamt = shamt;
            return a;
        }

        public static AssemblerIns GenORI(string rt, string rs, string immediate)
        {
            AssemblerIns a = new AssemblerIns();
            a.Op = "ORI";
            a.Rt = rt;
            a.Rs = rs;
            a.Immediate = immediate;
            return a;
        }

        public static AssemblerIns GenXORI(string rt, string rs, string immediate)
        {
            AssemblerIns a = new AssemblerIns();
            a.Op = "XORI";
            a.Rt = rt;
            a.Rs = rs;
            a.Immediate = immediate;
            return a;
        }

        public static AssemblerIns GenLUI(string rt, string immediate)
        {
            AssemblerIns a = new AssemblerIns();
            a.Op = "LUI";
            a.Rt = rt;
            a.Immediate = immediate;
            return a;
        }

        public static AssemblerIns GenLW(string rt, string offset, string rs)
        {
            AssemblerIns a = new AssemblerIns();
            a.Op = "LW";
            a.Rt = rt;
            a.Rs = rs;
            a.Offset = offset;
            return a;
        }

        public static AssemblerIns GenSW(string rt, string offset, string rs)
        {
            AssemblerIns a = new AssemblerIns();
            a.Op = "SW";
            a.Rt = rt;
            a.Rs = rs;
            a.Offset = offset;
            return a;
        }

        public static AssemblerIns GenBEQ(string rs, string rt, string offset)
        {
            AssemblerIns a = new AssemblerIns();
            a.Op = "BEQ";
            a.Rt = rt;
            a.Rs = rs;
            a.Offset = offset;
            return a;
        }

        public static AssemblerIns GenBNE(string rs, string rt, string offset)
        {
            AssemblerIns a = new AssemblerIns();
            a.Op = "BEQ";
            a.Rt = rt;
            a.Rs = rs;
            a.Offset = offset;
            return a;
        }

        public static AssemblerIns GenBGEZ(string rs, string offset)
        {
            AssemblerIns a = new AssemblerIns();
            a.Op = "BGEZ";
            a.Rs = rs;
            a.Offset = offset;
            return a;
        }

        public static AssemblerIns GenBGTZ(string rs, string offset)
        {
            AssemblerIns a = new AssemblerIns();
            a.Op = "BGTZ";
            a.Rs = rs;
            a.Offset = offset;
            return a;
        }

        public static AssemblerIns GenBLEZ(string rs, string offset)
        {
            AssemblerIns a = new AssemblerIns();
            a.Op = "BLEZ";
            a.Rs = rs;
            a.Offset = offset;
            return a;
        }

        public static AssemblerIns GenBLTZ(string rs, string offset)
        {
            AssemblerIns a = new AssemblerIns();
            a.Op = "BLTZ";
            a.Rs = rs;
            a.Offset = offset;
            return a;
        }

        public static AssemblerIns GenJUMP(string operation, string rs, string offset)
        {
            AssemblerIns a = new AssemblerIns();
            a.Op = operation;
            a.Rs = rs;
            a.Offset = offset;
            return a;
        }

        public static AssemblerIns GenJ(string address)
        {
            AssemblerIns a = new AssemblerIns();
            a.Op = "J";
            a.Address = address;
            return a;
        }
    }
}
