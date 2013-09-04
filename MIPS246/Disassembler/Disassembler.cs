using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MIPS246.Core.DataStructure;
using System.IO;

namespace MIPS246.Core.Disassembler
{
    private struct tag
    {
        private int pos;                        //pos of the tag
        private string name;
        public int Pos
        {
            get
            {
                return pos;
            }
            set
            {
                pos = value;
            }
        }
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
    }

    public class Disassembler
    {
        #region Fields
        int counter;
        private List<string> sourceString;
        private List<Instruction> codelist;      
        private List<tag> taglist;
       
        
        //private AssemblerErrorInfo error;
        private string sourcepath;
        private string outputpath;
        private bool isHex;

        //private bool isDisassembled;

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
            this.taglist = new List<tag>();
            this.codelist = new List<Instruction>();
            isHex = true;            
        }

        public Disassembler(List<string> sourceString)
        {
            this.sourcepath = null;
            this.outputpath = null;
            this.sourceString = sourceString;
            this.taglist = new List<tag>();
            this.codelist = new List<Instruction>();
            isHex = true;
        }

        public Disassembler(string sourceCode)
        {
            this.sourcepath = null;
            this.outputpath = null;
            this.sourceString = new List<string>(sourceCode.Split(new char[] {'\n'}, StringSplitOptions.RemoveEmptyEntries));
            this.taglist = new List<tag>();
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
        public bool DoDisassemble()
        {        
            if(LoadFile() == false)
            {
                //erroe code
                return false;
            }

            if(StringToMachinecode() == false)
            {
                return false;
            }

            if(SetTag() == false)
            {
                return false;
            }

            return true;
        }

        public void display()
        {
            List<tag> _temp = new List<tag>(taglist);               // a copy of taglist
            int _n = 0;
            for (int i = 0; i < codelist.Count; i++)
            {
                if(_temp.Count>0)
                {
                    _n = _temp[0].Pos;
                    for(;i<_n;i++)
                    {
                        Console.WriteLine(codelist[i].ToString());
                    }
                    Console.WriteLine(_temp[0].Name);
                    _temp.RemoveAt(0);
                }
                else
                {
                    Console.WriteLine(codelist[i].ToString());
                }
            }
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
            for(int i = 0; i<codelist.Count; i++)
            {                
                codelist[i].Validate();
                if(codelist[i].Mnemonic == MIPS246.Core.DataStructure.Mnemonic.BEQ)
                {
                    tag _tag = new tag();
                    _tag.Pos = Convert.ToInt32(codelist[i].Arg3)/4 + i;
                    _tag.Name = Convert.ToString(codelist.Count);                     //use counter as name
                    codelist[i].Arg3 = _tag.Name;
                    taglist.Add(_tag); 
                }
                else if(codelist[i].Mnemonic == MIPS246.Core.DataStructure.Mnemonic.BNE)
                {
                    tag _tag = new tag();
                    _tag.Pos = Convert.ToInt32(codelist[i].Arg3) / 4 + i;
                    _tag.Name = Convert.ToString(codelist.Count);                     //use counter as name
                    codelist[i].Arg3 = _tag.Name;
                    taglist.Add(_tag); 
                }
                else if(codelist[i].Mnemonic == MIPS246.Core.DataStructure.Mnemonic.J)
                {
                    tag _tag = new tag();
                    _tag.Pos = Convert.ToInt32(codelist[i].Arg1) / 4 + i;
                    _tag.Name = Convert.ToString(codelist.Count);                     //use counter as name
                    codelist[i].Arg1 = _tag.Name;
                    taglist.Add(_tag); 
                }
                else if(codelist[i].Mnemonic == MIPS246.Core.DataStructure.Mnemonic.JAL)
                {
                    tag _tag = new tag();
                    _tag.Pos = Convert.ToInt32(codelist[i].Arg1) / 4 + i;
                    _tag.Name = Convert.ToString(codelist.Count);                     //use counter as name
                    codelist[i].Arg1 = _tag.Name;
                    taglist.Add(_tag);
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
