using System;
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
        private static void ReadConfig()
        {
            
        }

        private static Instruction GenerateCMD(int cmdIndex)
        {
            string mnemonic = cmdList[cmdIndex].ToString();
            string arg1 = null, arg2 = null, arg3 = null;

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
            int offset = r.Next(minIMM, maxIMM);
            return offset.ToString(); 
        }

        
        #endregion

        #region Args Geneator
        #endregion
    }
}

