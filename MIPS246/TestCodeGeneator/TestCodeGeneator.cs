﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MIPS246.Core.DataStructure;
using System.Configuration;

namespace MIPS246.Core.TestCodeGeneator
{
    public static class TestCodeGeneator
    {
        #region Fields
        private static int count;
        private static int seed;
        private static List<Instruction> codeList;
        private static Random r;
        private static int maxIMM, minIMM;

        private static List<Mnemonic> cmdList;
        #endregion

        #region Constructors
        static TestCodeGeneator()
        {
            r = new Random();
            codeList = new List<Instruction>();
        }
        #endregion

        #region Properties
        public static int Count
        {
            set
            {
                count = value;
            }

            get
            {
                return count;
            }
        }

        public static int Seed
        {
            set
            {
                seed = value;
            }
            get
            {
                return seed;
            }
        }

        public static List<Instruction> CodeList
        {
            set
            {
                codeList = value;
            }
            get
            {
                return codeList;
            }
        }
        #endregion

        #region Public Methods
        public static void ConfigGeneator(int num, int maximm, int minimm, List<Mnemonic> targetCMDList)
        {
            count = num;
            maxIMM = maximm;
            minIMM = minimm;
            cmdList = targetCMDList;
        }

        public static void Generate()
        {
            for (int i = 0; i < count; i++)
            {
                codeList.Add(GenerateCMD(r.Next(0, cmdList.Count)));
            }
        }
        #endregion

        #region Internal Methods

        private static Instruction GenerateCMD(int cmdIndex)
        {
            string mnemonic = cmdList[cmdIndex].ToString();
            string arg1 = null, arg2 = null, arg3 = null;

            switch(mnemonic)
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
                    arg1 = GenerateReg();
                    arg2 = GenerateReg();
                    arg3 = GenerateReg();
                    break;
                case "SLL":
                case "SRL":
                case "SRA":
                    arg1 = GenerateReg();
                    arg2 = GenerateReg();
                    arg3 = GenerateShamt();
                    break;
                case "SLLV":
                case "SRLV":
                case "SRAV":
                    arg1 = GenerateReg();
                    arg2 = GenerateReg();
                    arg3 = GenerateReg();
                    break;
                case "JR":
                case "JALR":
                    arg1 = GenerateReg();
                    break;
                case "ADDI":
                case "ADDIU":
                case "ANDI":
                case "ORI":
                case "XORI":
                    arg1 = GenerateReg();
                    arg2 = GenerateReg();
                    arg3 = GenerateImmediate();
                    break;
                case "LUI":
                    arg1 = GenerateReg();
                    arg2 = GenerateImmediate();
                    break;
                case "SLTI":
                case "SLTIU":
                    arg1 = GenerateReg();
                    arg2 = GenerateReg();
                    arg3 = GenerateImmediate();
                    break;
                case "LW":
                case "SW":
                case "LB":
                case "LBU":
                case "LH":
                case "LHU":
                case "SB":
                case "SH":
                case "BEQ":
                case "BNE":                    
                    arg1 = GenerateReg();
                    arg2 = GenerateReg();
                    arg3 = GenerateOffset();
                    break;
                case "BGEZ":
                case "BGEZAL":
                case "BGTZ":
                case "BLEZ":
                case "BLTZ":
                case "BLTZAL":                                     
                    arg1 = GenerateReg();
                    arg2 = GenerateOffset();
                    break;
                case "J":
                case "JAL":
                    arg1 = GenerateAddress();
                    break;
                case "SUBI":
                case "MOVE":
                case "NOP":
                case "LI":
                case "LA":
                case "SYSCALL":
                default:
                    break;
            }

            return new Instruction(mnemonic, arg1, arg2, arg3);
        }

        private static string GenerateReg()
        {
            Register register = (Register)(r.Next(0, 31));
            return register.ToString();
        }

        private static string GenerateImmediate()
        {
            int immediate = r.Next(minIMM,maxIMM);
            return immediate.ToString(); 
        }

        private static string GenerateOffset()
        {
            int offset;
            if (codeList.Count <= (count - codeList.Count))
            {
                offset = r.Next(codeList.Count * 4, (count - codeList.Count) * 4);
            }
            else
            {
                offset = r.Next((count - codeList.Count) * 4, codeList.Count * 4);
            }
            
            return offset.ToString(); 
        }

        private static string GenerateShamt()
        {
            int shamt = r.Next(0, 31);
            return shamt.ToString();
        }

        private static string GenerateAddress()
        {
            int address = r.Next(0, count-1);
            return address.ToString();
        }
        #endregion

        #region Args Geneator
        #endregion
    }
}
