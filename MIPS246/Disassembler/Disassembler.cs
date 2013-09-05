using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MIPS246.Core.DataStructure;
using System.IO;

namespace MIPS246.Core.Disassembler
{
    public struct tag
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
        //int counter;
        private List<string> sourceString;
        private List<Instruction> codelist;      
        private List<tag> taglist;
        private DisassemblerErrorInfo error;
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

        public List<tag> Taglist
        {
            get
            {
                return taglist;
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

        public DisassemblerErrorInfo Error
        {
            get
            {
                return this.error;
            }
        }
        #endregion

        #region Public Methods
        public bool DoDisassemble(bool isAlias = false)
        {        
            if(LoadFile() == false)
            {
                //erroe code
                Console.Write("\r\nfile load failed");
                return false;
            }

            if(StringToMachinecode() == false)
            {
                //erroe code
                Console.Write("\r\nto machinecode failed");
                return false;
            }

            if(SetTag(isAlias) == false)
            {
                //erroe code
                Console.Write("\r\ntag set failed");
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
                    Console.WriteLine("tag" + _temp[0].Name + ":");
                    _temp.RemoveAt(0);
                }
                else
                {
                    Console.WriteLine(codelist[i].ToString());
                }
            }
        }

        public void output(string outputpath)
        {
            StreamWriter sr = new StreamWriter(outputpath);
            
            List<tag> _temp = new List<tag>(taglist);               // a copy of taglist
            int _n = 0;
            for (int i = 0; i < codelist.Count; i++)
            {
                if (_temp.Count > 0)
                {
                    _n = _temp[0].Pos;
                    for (; i < _n; i++)
                    {
                        sr.WriteLine(codelist[i].ToString());
                    }
                    sr.WriteLine("tag" + _temp[0].Name + ":");
                    _temp.RemoveAt(0);
                }
                else
                {
                    sr.WriteLine(codelist[i].ToString());
                }
            }

            sr.Close();
        }

        public void displayError()
        {
            Console.WriteLine("Disassenmbler failed:");
            this.error.ConsoleDisplay();
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
                    this.error = new DisassemblerErrorInfo(0, AssemblerError.NOFILE);
                    //Console.Write("\r\nno file");
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
            for (int i = 0; i < sourceString.Count; i++)
            {
                BitArray _machinecode = new BitArray(32);
                if(isHex == true)
                {
                    if(sourceString[i].Length != 8)
                    {
                        /*error code*/
                        this.error = new DisassemblerErrorInfo(i, AssemblerError.WRONGFORM, "please check the length of code");
                        //Console.Write("\r\nhex length wrong");
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
                                    _machinecode[j*4] = false;
                                    _machinecode[j*4+1] = false;
                                    _machinecode[j*4+2] = false;
                                    _machinecode[j*4+3] = false;
                                    break;
                                case '1':
                                    _machinecode[j*4] = false;
                                    _machinecode[j*4+1] = false;
                                    _machinecode[j*4+2] = false;
                                    _machinecode[j*4+3] = true;
                                    break;
                                case '2':
                                    _machinecode[j*4] = false;
                                    _machinecode[j*4+1] = false;
                                    _machinecode[j*4+2] = true;
                                    _machinecode[j*4+3] = false;
                                    break;
                                case '3':
                                    _machinecode[j*4] = false;
                                    _machinecode[j*4+1] = false;
                                    _machinecode[j*4+2] = true;
                                    _machinecode[j*4+3] = true;
                                    break;
                                case '4':
                                    _machinecode[j*4] = false;
                                    _machinecode[j*4+1] = true;
                                    _machinecode[j*4+2] = false;
                                    _machinecode[j*4+3] = false;
                                    break;
                                case '5':
                                    _machinecode[j*4] = false;
                                    _machinecode[j*4+1] = true;
                                    _machinecode[j*4+2] = false;
                                    _machinecode[j*4+3] = true;
                                    break;
                                case '6':
                                    _machinecode[j*4] = false;
                                    _machinecode[j*4+1] = true;
                                    _machinecode[j*4+2] = true;
                                    _machinecode[j*4+3] = false;
                                    break;
                                case '7':
                                    _machinecode[j*4] = false;
                                    _machinecode[j*4+1] = true;
                                    _machinecode[j*4+2] = true;
                                    _machinecode[j*4+3] = true;
                                    break;
                                case '8':
                                    _machinecode[j*4] = true;
                                    _machinecode[j*4+1] = false;
                                    _machinecode[j*4+2] = false;
                                    _machinecode[j*4+3] = false;
                                    break;
                                 case '9':
                                    _machinecode[j*4] = true;
                                    _machinecode[j*4+1] = false;
                                    _machinecode[j*4+2] = false;
                                    _machinecode[j*4+3] = true;
                                    break;
                                case 'a':
                                case 'A':
                                    _machinecode[j*4] = true;
                                    _machinecode[j*4+1] = false;
                                    _machinecode[j*4+2] = true;
                                    _machinecode[j*4+3] = false;
                                    break;
                                case 'b':
                                case 'B':
                                    _machinecode[j*4] = true;
                                    _machinecode[j*4+1] = false;
                                    _machinecode[j*4+2] = true;
                                    _machinecode[j*4+3] = true;
                                    break;
                                case 'c':
                                case 'C':
                                    _machinecode[j*4] = true;
                                    _machinecode[j*4+1] = true;
                                    _machinecode[j*4+2] = false;
                                    _machinecode[j*4+3] = false;
                                    break;
                                case 'd':
                                case 'D':
                                    _machinecode[j*4] = true;
                                    _machinecode[j*4+1] = true;
                                    _machinecode[j*4+2] = false;
                                    _machinecode[j*4+3] = true;
                                    break;
                                case 'e':
                                case 'E':
                                    _machinecode[j*4] = true;
                                    _machinecode[j*4+1] = true;
                                    _machinecode[j*4+2] = true;
                                    _machinecode[j*4+3] = false;
                                    break;
                                case 'f':
                                case 'F':
                                    _machinecode[j*4] = true;
                                    _machinecode[j*4+1] = true;
                                    _machinecode[j*4+2] = true;
                                    _machinecode[j*4+3] = true;
                                    break;  
                                default:
                                    /*error code*/
                                    this.error = new DisassemblerErrorInfo(i, AssemblerError.WRONGFORM, "invalid character");
                                    return false;
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
                        this.error = new DisassemblerErrorInfo(i, AssemblerError.WRONGFORM, "please check the length of code");
                        //Console.Write("\r\nbinary length wrong");
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
                            else if (sourceString[i][j] == '0')
                            {
                                _machinecode[j] = false;
                            }
                            else
                            {
                                /*error code*/
                                this.error = new DisassemblerErrorInfo(i, AssemblerError.WRONGFORM, "invalid character");
                                return false;
                            }
                        }
                    }
                }
                codelist.Add(new Instruction(_machinecode));
            }
            return  true;
        }

        private bool SetTag(bool isAlias = false)
        {            
            for(int i = 0; i<codelist.Count; i++)
            {
                codelist[i].Alias = isAlias;               
                codelist[i].Validate();
                if(codelist[i].Mnemonic == MIPS246.Core.DataStructure.Mnemonic.BEQ)
                {
                    tag _tag = new tag();
                    _tag.Pos = Convert.ToInt32(codelist[i].Arg3)/*/4*/ + i;

                    if (_tag.Pos < 0 || _tag.Pos > codelist.Count)
                    {
                        /*error code*/
                        this.error = new DisassemblerErrorInfo(i, AssemblerError.WRONGTAG, "jump target OutOfBounds");
                        return false;
                    }

                    _tag.Name = Convert.ToString(taglist.Count);                     //use counter as name
                    codelist[i].Arg3 = "tag" + _tag.Name;
                    taglist.Add(_tag); 
                }
                else if(codelist[i].Mnemonic == MIPS246.Core.DataStructure.Mnemonic.BNE)
                {
                    tag _tag = new tag();
                    _tag.Pos = Convert.ToInt32(codelist[i].Arg3) /*/ 4*/ + i;

                    if (_tag.Pos < 0 || _tag.Pos > codelist.Count)
                    {
                        /*error code*/
                        this.error = new DisassemblerErrorInfo(i, AssemblerError.WRONGTAG, "jump target OutOfBounds");
                        return false;
                    }

                    _tag.Name = Convert.ToString(taglist.Count);                     //use counter as name
                    codelist[i].Arg3 = "tag" + _tag.Name;
                    taglist.Add(_tag); 
                }
                else if(codelist[i].Mnemonic == MIPS246.Core.DataStructure.Mnemonic.J)
                {
                    tag _tag = new tag();
                    _tag.Pos = Convert.ToInt32(codelist[i].Arg1) /*/ 4*/ + i;

                    if (_tag.Pos < 0 || _tag.Pos > codelist.Count)
                    {
                        /*error code*/
                        this.error = new DisassemblerErrorInfo(i, AssemblerError.WRONGTAG, "jump target OutOfBounds");
                        return false;
                    }

                    _tag.Name = Convert.ToString(taglist.Count);                     //use counter as name
                    codelist[i].Arg1 = "tag" + _tag.Name;
                    taglist.Add(_tag); 
                }
                else if(codelist[i].Mnemonic == MIPS246.Core.DataStructure.Mnemonic.JAL)
                {
                    tag _tag = new tag();
                    _tag.Pos = Convert.ToInt32(codelist[i].Arg1) /*/ 4*/ + i;

                    if (_tag.Pos < 0 || _tag.Pos > codelist.Count)
                    {
                        /*error code*/
                        this.error = new DisassemblerErrorInfo(i, AssemblerError.WRONGTAG, "jump target OutOfBounds");
                        return false;
                    }

                    _tag.Name = Convert.ToString(taglist.Count);                     //use counter as name
                    codelist[i].Arg1 = "tag" + _tag.Name;
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
