using System;
using System.Collections
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MIPS246.Core.DataStructure;
using System.IO;

namespace MIPS246.Core.Disassembler
{
    public class Disassembler
    {
        #region Fields
        int counter;
        private List<string> sourceString;
        private List<Instruction> codelist;
       //private List<string[]> sourceList;
        private List<int> taglist;
        
        //private AssemblerErrorInfo error;
        private string sourcepath;
        private string outputpath;
        private bool isHex;
        
       /* private struct tag
        {
            int counter;                        //the counter_th instruction
            int offset;
            string name;
        }*/


        private static int startAddress = 0;    //add 0 line, for future use
        #endregion

        #region Constructors
       /* public Disassembler()
        {
        }*/

        public Disassembler(string sorcepath, string outputpath)
        {
            this.sourcepath = sorcepath;
            this.outputpath = outputpath;
            this.sourceString = new List<string>();
            this.taglist = new List<int>();
            this.codelist = new List<Instruction>();
            isHex = true;
        }

        public Disassembler(List<string> sourceString)
        {
            this.sourcepath = null;
            this.outputpath = null;
            this.sourceString = sourceString;
            this.taglist = new List<int>();
            this.codelist = new List<Instruction>();
            isHex = true;
        }

        public Disassembler(string sourceCode)
        {
            this.sourcepath = null;
            this.outputpath = null;
            this.sourceString = new List<string>(sourceCode.Split(new char[] {'\n'}, StringSplitOptions.RemoveEmptyEntries));
            this.taglist = new List<int>();
            this.codelist = new List<Instruction>();
            isHex = true;
        }

        #endregion

        #region Properties
        public List<string> SourceString
        {
            get
            {
                return sourceString;
            }
        }

        public List<Instruction> Codelist
        {
            get
            {
                return codelist;
            }
        }

        public List<int> Jumptag
        {
            get
            {
                return jumptag;
            }
        }

        public bool IsHex
        {
            get 
            {
                return isHex;
            }
            set
            {
                this.isHex = value;
            }
        }
        #endregion

        #region Public Methods
        public bool DoDisassemble
        {        


        }
        #endregion

        #region Internal Methods
        private bool LoadFile()
        {
            if (sourcepath != null)
            {
                if (File.Exists(sourcepath) == false)
                {
                    //error code
                    return false;
                }
                else
                {
                    StreamReader sr = new StreamReader(sourcepath);
                    string _line;
                    while ((_line = sr.ReadLine()) != null)
                    {
                        this.sourceString.Add(_line);
                    }
                    sr.Close();
                    return true;
                }
            }
            else
            {
                /* it's useless so far, just to fufill the return queset*/
                if (sourceString != null)
                    return true;
                else
                    return false;
            }

        }

        private bool StringToMachinecode()
        {
            BitArray _machinecode = new BitArray(32);
            for (int i = 0; i < sourceString.Count; i++)
            {
                if(isHex == false)
                {
                    if(sourceString[i].Length != 8)
                    {
                        /*error code*/
                        return false;
                    }
                    else
                    {
                        for(int j = 0; j<8; j++)
                        {
                            #region case
                            switch(sourceString[i][j]) //get the char on j_th position
                            {
                                case '0':
                                    _machinecode[31-j*4] = false;
                                    _machinecode[31-j*4-1] = false;
                                    _machinecode[31-j*4-2] = false;
                                    _machinecode[31-j*4-3] = false;
                                    break;
                                case '1':
                                    _machinecode[31-j*4] = false;
                                    _machinecode[31-j*4-1] = false;
                                    _machinecode[31-j*4-2] = false;
                                    _machinecode[31-j*4-3] = true;
                                    break;
                                case '2':
                                    _machinecode[31-j*4] = false;
                                    _machinecode[31-j*4-1] = false;
                                    _machinecode[31-j*4-2] = true;
                                    _machinecode[31-j*4-3] = false;
                                    break;
                                case '3':
                                    _machinecode[31-j*4] = false;
                                    _machinecode[31-j*4-1] = false;
                                    _machinecode[31-j*4-2] = true;
                                    _machinecode[31-j*4-3] = true;
                                    break;
                                case '4':
                                    _machinecode[31-j*4] = false;
                                    _machinecode[31-j*4-1] = true;
                                    _machinecode[31-j*4-2] = false;
                                    _machinecode[31-j*4-3] = false;
                                    break;
                                case '5':
                                    _machinecode[31-j*4] = false;
                                    _machinecode[31-j*4-1] = true;
                                    _machinecode[31-j*4-2] = false;
                                    _machinecode[31-j*4-3] = true;
                                    break;
                                case '6':
                                    _machinecode[31-j*4] = false;
                                    _machinecode[31-j*4-1] = true;
                                    _machinecode[31-j*4-2] = true;
                                    _machinecode[31-j*4-3] = false;
                                    break;
                                case '7':
                                    _machinecode[31-j*4] = false;
                                    _machinecode[31-j*4-1] = true;
                                    _machinecode[31-j*4-2] = true;
                                    _machinecode[31-j*4-3] = true;
                                    break;
                                case '8':
                                    _machinecode[31-j*4] = true;
                                    _machinecode[31-j*4-1] = false;
                                    _machinecode[31-j*4-2] = false;
                                    _machinecode[31-j*4-3] = false;
                                    break;
                                 case '9':
                                    _machinecode[31-j*4] = true;
                                    _machinecode[31-j*4-1] = false;
                                    _machinecode[31-j*4-2] = false;
                                    _machinecode[31-j*4-3] = true;
                                    break;
                                case 'a':
                                case 'A':
                                    _machinecode[31-j*4] = true;
                                    _machinecode[31-j*4-1] = false;
                                    _machinecode[31-j*4-2] = true;
                                    _machinecode[31-j*4-3] = false;
                                    break;
                                case 'b':
                                case 'B':
                                    _machinecode[31-j*4] = true;
                                    _machinecode[31-j*4-1] = false;
                                    _machinecode[31-j*4-2] = true;
                                    _machinecode[31-j*4-3] = true;
                                    break;
                                case 'c':
                                case 'C':
                                    _machinecode[31-j*4] = true;
                                    _machinecode[31-j*4-1] = true;
                                    _machinecode[31-j*4-2] = false;
                                    _machinecode[31-j*4-3] = false;
                                    break;
                                case 'd':
                                case 'D':
                                    _machinecode[31-j*4] = true;
                                    _machinecode[31-j*4-1] = true;
                                    _machinecode[31-j*4-2] = false;
                                    _machinecode[31-j*4-3] = true;
                                    break;
                                case 'e':
                                case 'E':
                                    _machinecode[31-j*4] = true;
                                    _machinecode[31-j*4-1] = true;
                                    _machinecode[31-j*4-2] = true;
                                    _machinecode[31-j*4-3] = false;
                                    break;
                                case 'f':
                                case 'F':
                                    _machinecode[31-j*4] = true;
                                    _machinecode[31-j*4-1] = true;
                                    _machinecode[31-j*4-2] = true;
                                    _machinecode[31-j*4-3] = true;
                                    break;                                
                            }
                            #endregion 
                        }                        
                    }
                }
                else
                {
                    if(sourceString[i].Length != 32)
                    {
                        /*error code*/
                        return false;
                    }
                    else
                    {
                        for(int j = 0; j<32; j++)
                        {
                            if(sourceString[i][j] == '1')
                            {
                                _machinecode[j] = true;
                            }
                            else
                            {
                                _machinecode[j] = false;
                            }
                        }
                    }
                }
                codelist.Add(new Instruction(_machinecode));
            }
            return  true;
        }

        private bool SetTag()
        {
            int _tag = 0;
            for(int i = 0; i<codelist.Count; i++)
            {
                codelist[i].Validate();
                if(codelist[i].Mnemonic == MIPS246.Core.DataStructure.Mnemonic.BEQ)
                {
                    _tag = Convert.ToInt32(codelist[i].Arg3)/4 + i;
                    taglist.Add(i);
                }
                else if(codelist[i].Mnemonic == MIPS246.Core.DataStructure.Mnemonic.BNE)
                {
                }
                else if(codelist[i].Mnemonic == MIPS246.Core.DataStructure.Mnemonic.J)
                {
                }
                else if(codelist[i].Mnemonic == MIPS246.Core.DataStructure.Mnemonic.JAL)
                {
                }
                else
                {
                }
            }
            return true;
        }

        #endregion
    }
}
