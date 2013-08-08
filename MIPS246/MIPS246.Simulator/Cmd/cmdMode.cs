using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MIPS246.Core.Assembler;
using MIPS246.Core.DataStructure;
using MipsSimulator.Assembler;
using MipsSimulator.Monocycle;
using MipsSimulator.Devices;
using MipsSimulator.Tools;
using System.IO;

namespace MipsSimulator.Cmd
{
    class cmdMode
    {
        MIPS246.Core.Assembler.Assembler assembler = new MIPS246.Core.Assembler.Assembler();
        public static Hashtable lineTable;
        public static string outPath="";
        public void start(string inputPath, string outputPath)
        {
            outPath=outputPath;
            MipsSimulator.Devices.Register.ResInitialize();
            MipsSimulator.Devices.Memory.MemInitialize();
            RunTimeCode.CodeTInitial();
            if (doAssembler(inputPath, outputPath))
            {
                if (File.Exists(outputPath))
                {
                    File.Delete(outputPath);
                }
                
                for( int k = 0; k < RunTimeCode.codeList.Count; k++)
                {
                    try
                    {
                        mMasterSwitch.StepInto();
                    }
                    catch (Exception e)
                    {
                        MipsSimulator.Tools.FileControl.WriteFile(outputPath, e.Message);
                        return;
                    }
                    string codeStr = RunTimeCode.codeList[k].codeStr;
                    MipsSimulator.Tools.FileControl.WriteFile(outputPath, codeStr + "\r\n");
                    for (int i = 0; i <= 31; i++)
                    {
                        string registerName = "$" + i;
                        string value =MipsSimulator.Devices.Register.GetRegisterValue(registerName) + " ";
                        MipsSimulator.Tools.FileControl.WriteFile(outputPath, value);
                    }
                   
                    MipsSimulator.Tools.FileControl.WriteFile(outputPath,"\r\n");
                }
            }
        }
        public static void addMessage(string message)
        {
            MipsSimulator.Tools.FileControl.WriteFile(outPath, message);            
        }
        public bool doAssembler(string inputPath, string outputPath)
        {
            assembler = new MIPS246.Core.Assembler.Assembler(inputPath, outputPath);
            Form1.Reset();
            if (assembler.DoAssemble() == true)
            {
                List<String[]> sourceList = assembler.SourceList;
                lineTable = assembler.Linetable;
                List<Instruction> codeList = assembler.CodeList;

                if (MipsSimulator.Program.mode != 1)
                {
                    RunTimeCode.CodeTInitial();
                }
                
                for (int i = 0; i < codeList.Count; i++)
                {
                    CodeType codeType = convertToCodeType(codeList[i].Mnemonic.ToString());
                    string machineCode = convertToMachineCode(codeList[i].Machine_Code);
                    
                    int p = (int)lineTable[i];
                    string codeStr = sourceList[p][0] + " ";
                    for (int s = 1; s < sourceList[p].Count(); s++)
                    {
                        codeStr += sourceList[p][s] + ",";
                    }
                    if (codeStr.Substring(codeStr.Length - 1, 1) == ",")
                    {
                        codeStr = codeStr.Substring(0, codeStr.Length - 1);
                    }
                    Code code = new Assembler.Code(codeType, null, codeStr, machineCode);
                    code.index = i;
                    RunTimeCode.codeList.Add(code);
                }
                
                for (int i = 0; i < sourceList.Count; i++)
                {
                    string codeStr = sourceList[i][0] + " ";
                    for (int s = 1; s < sourceList[i].Count(); s++)
                    {
                        codeStr += sourceList[i][s] + ",";
                    }
                    if (codeStr.Substring(codeStr.Length - 1, 1) == ",")
                    {
                        codeStr = codeStr.Substring(0, codeStr.Length - 1);
                    }
                    string machineCode = "";
                    int j = 0;
                    for (j = 0; j < lineTable.Count; j++)
                    {
                        if ((int)lineTable[j] == i)
                        {
                            break;
                        }
                    }
                    if (j != lineTable.Count)
                    {
                        machineCode = RunTimeCode.codeList[j].machineCode;
                        Int32 tmp = (Int32)CommonTool.StrToNum(TypeCode.Int32, machineCode, 2);
                        machineCode = "0x" + tmp.ToString("X8");
                    }
                    Code code = new Assembler.Code(CodeType.NOP, null, codeStr, machineCode);
                    code.index = i;
                    RunTimeCode.Add(code);
                }

            }
            else
            {
                MipsSimulator.Tools.FileControl.WriteFile(outputPath, assembler.Error.ToString());
                return false;
            }
            return true;
        }
        private CodeType convertToCodeType(string mnemonic)
        {
            switch (mnemonic)
            {
                case "ADD":
                    return CodeType.ADD;
                case "ADDU":
                    return CodeType.ADDU;
                case "SUB":
                    return CodeType.SUB;
                case "SUBU":
                    return CodeType.SUBU;
                case "AND":
                    return CodeType.AND;
                case "OR":
                    return CodeType.OR;
                case "XOR":
                    return CodeType.XOR;
                case "NOR":
                    return CodeType.NOR;
                case "SLT":
                    return CodeType.SLT;
                case "SLTU":
                    return CodeType.SLTU;
                case "SLL":
                    return CodeType.SLL;
                case "SRL":
                    return CodeType.SRL;
                case "SRA":
                    return CodeType.SRA;
                case "SLLV":
                    return CodeType.SLLV;
                case "SRLV":
                    return CodeType.SRLV;
                case "SRAV":
                    return CodeType.SRAV;
                case "JR":
                    return CodeType.JR;
                case "ADDI":
                    return CodeType.ADDI;
                case "ADDIU":
                    return CodeType.ADDIU;
                case "ANDI":
                    return CodeType.ANDI;
                case "ORI":
                    return CodeType.ORI;
                case "XORI":
                    return CodeType.XORI;
                case "LUI":
                    return CodeType.LUI;
                case "LW":
                    return CodeType.LW;
                case "SW":
                    return CodeType.SW;
                case "BEQ":
                    return CodeType.BEQ;
                case "BNE":
                    return CodeType.BNE;
                case "SLTI":
                    return CodeType.SLTI;
                case "SLTIU":
                    return CodeType.SLTIU;
                case "J":
                    return CodeType.J;
                case "JAL":
                    return CodeType.JAL;
                default:
                    return CodeType.UNSUPPOTED;
            }
        }

        private string convertToMachineCode(BitArray Machine_Code)
        {
            string machineCode = "";
            bool[] boolArray = new bool[32];
            Machine_Code.CopyTo(boolArray, 0);
            for (int j = 0; j < 32; j++)
            {
                if (boolArray[j] == true)
                {
                    machineCode += "1";
                }
                else
                {
                    machineCode += "0";
                }
            }
            return machineCode;
        }
    }
}
