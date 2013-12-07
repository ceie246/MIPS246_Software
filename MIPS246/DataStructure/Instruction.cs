using System;
using System.Collections;
using System.Collections.Generic;

namespace MIPS246.Core.DataStructure
{
    //mips246 instruction c# model

    public enum Mnemonic
    {
        ADD,ADDI,ADDU,ADDIU,AND,
        ANDI,DIV,DIVU,MULT,MULTU,
        NOR,OR,ORI,SLL,SRL,
        SRA,SRLV,SRAV,SLLV,SUB,
        SUBU,XOR,XORI,LUI,SLT,
        SLTI,SLTU,SLTIU,BEQ,BGEZ,
        BGTZ,BLEZ,BLTZ,BNE,J,
        JAL,JR,JALR,LBU,LHU,
        LB,LH,LW,SB,SH,
        SW,BREAK,SYSCALL,ERET,MFHI,
        MFLO,MTHI,MTLO,MFC0,MTC0,

        NULL,BGEZAL,BLTZAL
    }

    public enum Register
    {
        ZERO,AT,V0,v1,A0,A1,A2,A3,T0,T1,T2,T3,T4,T5,T6,T7,S0,S1,S2,S3,S4,S5,S6,S7,T8,T9,K0,K1,GP,SP,FP,RA
    }

    public class Instruction
    {
        #region Fields
        private Mnemonic mnemonic;
        private BitArray machine_code;
        private string arg1, arg2, arg3;    
        private int address;                
        private static Hashtable AssemblerTable;

        //add by wong
        private static Dictionary<int, string> Regs;
        private static Dictionary<int, Mnemonic> R_type;
        private static Dictionary<int, Mnemonic> IJ_type;       
        private bool isAlias;
        #endregion

        #region Constructors
        static Instruction()
        {
            InitAssemblerTable();
            InitRegDic();
            InitR_type();
            InitIJ_type();
        }

        public Instruction(string mnemonic, string arg1, string arg2, string arg3)
        {
            this.mnemonic = (Mnemonic)Enum.Parse(typeof(Mnemonic), mnemonic.ToUpper());
            this.arg1 = arg1;
            this.arg2 = arg2;
            this.arg3 = arg3;
        }

        public Instruction(BitArray machine_code ,bool isAlias = false)
        {
            this.machine_code = machine_code;
            this.mnemonic = Mnemonic.NULL;
            this.isAlias = isAlias;
            this.arg1 = string.Empty;
            this.arg2 = string.Empty;
            this.arg3 = string.Empty;
        }
        #endregion

        #region Properties
        public int Address
        {
            get
            {
                return this.address;
            }
            set
            {
                this.address = value;
            }
        }

        public BitArray Machine_Code
        {
            get
            {
                return this.machine_code;
            }
        }

        public string Arg1
        {
            get
            {
                return this.arg1;
            }
            set
            {
                this.arg1 = value;
            }
        }

        public string Arg2
        {
            get
            {
                return this.arg2;
            }
            set
            {
                this.arg2 = value;
            }
        }

        public string Arg3
        {
            get
            {
                return this.arg3;
            }
            set
            {
                this.arg3 = value;
            }
        }

        public Mnemonic Mnemonic
        {
            get
            {
                return this.mnemonic;
            }
            set
            {
                this.mnemonic = value;
            }
        }

       /// <summary>
       /// add by wong
       /// </summary>
        public bool Alias
        {
            get
            {
                return this.isAlias;
            }
            set
            {
                this.isAlias = value;
            }
        }
        #endregion

        #region Public Methods
        public void Validate()
        {
            
            if (this.mnemonic != Mnemonic.NULL)
            {
                ToMachineCode();                
            }
            else
            {
                ToAsmCode();
            }
        }

        //to raw stiring
        public override string ToString()
        {
            string mnemonicString="";

            switch (this.mnemonic)
            {                
                case Mnemonic.JR:
                case Mnemonic.JALR:
                case Mnemonic.J:
                case Mnemonic.JAL:
                case Mnemonic.MFHI:
                case Mnemonic.MFLO:
                case Mnemonic.MTHI:
                case Mnemonic.MTLO:
                    mnemonicString = mnemonicString + this.mnemonic.ToString() + " " + this.arg1;
                    break;                
                case Mnemonic.LUI:
                case Mnemonic.BGEZ:
                case Mnemonic.BGEZAL:
                case Mnemonic.BGTZ:
                case Mnemonic.BLEZ:
                case Mnemonic.BLTZ:
                case Mnemonic.BLTZAL:
                case Mnemonic.DIV:
                case Mnemonic.DIVU:
                case Mnemonic.MULT:
                case Mnemonic.MULTU:
                case Mnemonic.MFC0:
                case Mnemonic.MTC0:
                    mnemonicString = mnemonicString + this.mnemonic.ToString() + " " + this.arg1 + "," + this.arg2;
                    break;             
                case Mnemonic.LW:
                case Mnemonic.SW:
                case Mnemonic.LB:
                case Mnemonic.LBU:
                case Mnemonic.LH:
                case Mnemonic.LHU:
                case Mnemonic.SB:
                case Mnemonic.SH:
                    mnemonicString = mnemonicString + this.mnemonic.ToString() + " " + this.arg1 + "," + this.arg3 + "(" + this.arg2 + ")";
                    break;
                case Mnemonic.ADD:
                case Mnemonic.ADDU:
                case Mnemonic.SUB:
                case Mnemonic.SUBU:
                case Mnemonic.AND:
                case Mnemonic.OR:
                case Mnemonic.XOR:
                case Mnemonic.NOR:
                case Mnemonic.SLT:
                case Mnemonic.SLTU:
                case Mnemonic.SLL:
                case Mnemonic.SRL:
                case Mnemonic.SRA:
                case Mnemonic.SLLV:
                case Mnemonic.SRLV:
                case Mnemonic.SRAV:
                case Mnemonic.ADDI:
                case Mnemonic.ADDIU:
                case Mnemonic.ANDI:
                case Mnemonic.ORI:
                case Mnemonic.XORI:
                case Mnemonic.SLTI:
                case Mnemonic.SLTIU:
                case Mnemonic.BEQ:
                case Mnemonic.BNE:
                    mnemonicString = mnemonicString + this.mnemonic.ToString() + " " + this.arg1 + "," + this.arg2 + "," + this.arg3;
                    break;      
                case Mnemonic.BREAK:
                case Mnemonic.SYSCALL:
                case Mnemonic.ERET:
                    mnemonicString = this.mnemonic.ToString();
                    break;
                /*case Mnemonic.SUBI:
                case Mnemonic.MOVE:
                case Mnemonic.NOP:
                case Mnemonic.LI:
                case Mnemonic.LA:
                case Mnemonic.SYSCALL:*/
                default:
                    break;
            }

            return mnemonicString;
        }
        #endregion

        #region Internal Methods
        private static BitArray InitBoolArray(string codestring)
        {
            BitArray machine_code = new BitArray(32);
            for (int i = 0; i < 32; i++)
            {
                if (codestring[i] == '0') machine_code[i] = false;
                else machine_code[i] = true;
            }
            return machine_code;
        }

        private static void InitAssemblerTable()
        {
            AssemblerTable = new Hashtable();

            AssemblerTable.Add(Mnemonic.ADD, "00000000000000000000000000100000");
            AssemblerTable.Add(Mnemonic.ADDU, "00000000000000000000000000100001");
            AssemblerTable.Add(Mnemonic.ADDI, "00100000000000000000000000000000");
            AssemblerTable.Add(Mnemonic.ADDIU, "00100100000000000000000000000000");
            AssemblerTable.Add(Mnemonic.AND, "00000000000000000000000000100100");

            AssemblerTable.Add(Mnemonic.ANDI, "00110000000000000000000000000000");
            AssemblerTable.Add(Mnemonic.DIV, "00000000000000000000000000011010");
            AssemblerTable.Add(Mnemonic.DIVU, "00000000000000000000000000011011");
            AssemblerTable.Add(Mnemonic.MULT, "00000000000000000000000000011000");
            AssemblerTable.Add(Mnemonic.MULTU, "00000000000000000000000000011001");

            AssemblerTable.Add(Mnemonic.NOR, "00000000000000000000000000100111");
            AssemblerTable.Add(Mnemonic.OR, "00000000000000000000000000100101");
            AssemblerTable.Add(Mnemonic.ORI, "00110100000000000000000000000000");
            AssemblerTable.Add(Mnemonic.SLL, "00000000000000000000000000000000");
            AssemblerTable.Add(Mnemonic.SRL, "00000000000000000000000000000010");

            AssemblerTable.Add(Mnemonic.SRA, "00000000000000000000000000000011");
            AssemblerTable.Add(Mnemonic.SRLV, "00000000000000000000000000000110");
            AssemblerTable.Add(Mnemonic.SRAV, "00000000000000000000000000000111");
            AssemblerTable.Add(Mnemonic.SLLV, "00000000000000000000000000000100");
            AssemblerTable.Add(Mnemonic.SUB, "00000000000000000000000000100010");

            AssemblerTable.Add(Mnemonic.SUBU, "00000000000000000000000000100011");            
            AssemblerTable.Add(Mnemonic.XOR, "00000000000000000000000000100110");
            AssemblerTable.Add(Mnemonic.XORI, "00111000000000000000000000000000");
            AssemblerTable.Add(Mnemonic.LUI, "00111100000000000000000000000000");
            AssemblerTable.Add(Mnemonic.SLT, "00000000000000000000000000101010");

            AssemblerTable.Add(Mnemonic.SLTI, "00101000000000000000000000000000");
            AssemblerTable.Add(Mnemonic.SLTU, "00000000000000000000000000101011");
            AssemblerTable.Add(Mnemonic.SLTIU, "00101100000000000000000000000000");
            AssemblerTable.Add(Mnemonic.BEQ, "00010000000000000000000000000000");
            AssemblerTable.Add(Mnemonic.BGEZ, "00000100000000010000000000000000");

            AssemblerTable.Add(Mnemonic.BGTZ, "00011100000000000000000000000000");
            AssemblerTable.Add(Mnemonic.BLEZ, "00011000000000000000000000000000");
            AssemblerTable.Add(Mnemonic.BLTZ, "00000100000000000000000000000000");
            AssemblerTable.Add(Mnemonic.BNE, "00010100000000000000000000000000");
            AssemblerTable.Add(Mnemonic.J, "00001000000000000000000000000000");

            AssemblerTable.Add(Mnemonic.JAL, "00001100000000000000000000000000");            
            AssemblerTable.Add(Mnemonic.JR, "00000000000000000000000000001000");
            AssemblerTable.Add(Mnemonic.JALR,"00000000000000001111100000001001");
            AssemblerTable.Add(Mnemonic.LBU, "10010000000000000000000000000000");
            AssemblerTable.Add(Mnemonic.LHU, "10010100000000000000000000000000");

            AssemblerTable.Add(Mnemonic.LB, "10000000000000000000000000000000");
            AssemblerTable.Add(Mnemonic.LH, "10000100000000000000000000000000");
            AssemblerTable.Add(Mnemonic.LW, "10001100000000000000000000000000");
            AssemblerTable.Add(Mnemonic.SB, "10100000000000000000000000000000");
            AssemblerTable.Add(Mnemonic.SH, "10100100000000000000000000000000"); 
           
            AssemblerTable.Add(Mnemonic.SW, "10101100000000000000000000000000");
            AssemblerTable.Add(Mnemonic.BREAK, "00000000000000000000000000001101");
            AssemblerTable.Add(Mnemonic.SYSCALL, "00000000000000000000000000001100");
            AssemblerTable.Add(Mnemonic.ERET, "01000010000000000000000000011000");
            AssemblerTable.Add(Mnemonic.MFHI, "00000000000000000000000000010000");

            AssemblerTable.Add(Mnemonic.MFLO, "00000000000000000000000000010010");
            AssemblerTable.Add(Mnemonic.MTHI, "00000000000000000000000000010001");
            AssemblerTable.Add(Mnemonic.MTLO, "00000000000000000000000000010011");
            AssemblerTable.Add(Mnemonic.MFC0, "01000000000000000000000000000000");
            AssemblerTable.Add(Mnemonic.MTC0, "01000000100000000000000000000000");
            
            
            AssemblerTable.Add(Mnemonic.BGEZAL, "00000100000100010000000000000000"); 
            AssemblerTable.Add(Mnemonic.BLTZAL, "00000100000100000000000000000000");

            
            
            //AssemblerTable.Add(Mnemonic.SUBI, InitBoolArray(""));
            //AssemblerTable.Add(Mnemonic.MOVE, InitBoolArray(""));
            //AssemblerTable.Add(Mnemonic.NOP, InitBoolArray(""));
            //AssemblerTable.Add(Mnemonic.LI, InitBoolArray(""));
            //AssemblerTable.Add(Mnemonic.LA, InitBoolArray(""));
            //AssemblerTable.Add(Mnemonic.SYSCALL, InitBoolArray(""));
        }

        private static void InitRegDic()
        {
            Regs = new Dictionary<int,string>();
            Regs.Add(0, "$0");          Regs.Add(32, "zero");
            Regs.Add(1, "$1");          Regs.Add(33, "at");
            Regs.Add(2, "$2");          Regs.Add(34, "v0");
            Regs.Add(3, "$3");          Regs.Add(35, "v1"); 
            Regs.Add(4, "$4");          Regs.Add(36, "a0");
            Regs.Add(5, "$5");          Regs.Add(37, "a1");
            Regs.Add(6, "$6");          Regs.Add(38, "a2");
            Regs.Add(7, "$7");          Regs.Add(39, "a3");
            Regs.Add(8, "$8");          Regs.Add(40, "t0");
            Regs.Add(9, "$9");          Regs.Add(41, "t1");
            Regs.Add(10, "$10");        Regs.Add(42, "t2");
            Regs.Add(11, "$11");        Regs.Add(43, "t3");
            Regs.Add(12, "$12");        Regs.Add(44, "t4");
            Regs.Add(13, "$13");        Regs.Add(45, "t5");
            Regs.Add(14, "$14");        Regs.Add(46, "t6");
            Regs.Add(15, "$15");        Regs.Add(47, "t7");
            Regs.Add(16, "$16");        Regs.Add(48, "s0");
            Regs.Add(17, "$17");        Regs.Add(49, "s1");
            Regs.Add(18, "$18");        Regs.Add(50, "s2");
            Regs.Add(19, "$19");        Regs.Add(51, "s3");
            Regs.Add(20, "$20");        Regs.Add(52, "s4");
            Regs.Add(21, "$21");        Regs.Add(53, "s5");
            Regs.Add(22, "$22");        Regs.Add(54, "s6");
            Regs.Add(23, "$23");        Regs.Add(55, "t7");
            Regs.Add(24, "$24");        Regs.Add(56, "t8");
            Regs.Add(25, "$25");        Regs.Add(57, "t9");
            Regs.Add(26, "$26");        Regs.Add(58, "k0");
            Regs.Add(27, "$27");        Regs.Add(59, "k1");
            Regs.Add(28, "$28");        Regs.Add(60, "gp");
            Regs.Add(29, "$29");        Regs.Add(61, "sp");
            Regs.Add(30, "$30");        Regs.Add(62, "fp");
            Regs.Add(31, "$31");        Regs.Add(63, "ra");
        }

        private static void InitR_type()
        {
            R_type = new Dictionary<int, Mnemonic>();
            R_type.Add(32, Mnemonic.ADD);
            R_type.Add(33, Mnemonic.ADDU);
            R_type.Add(34, Mnemonic.SUB);
            R_type.Add(35, Mnemonic.SUBU);
            R_type.Add(36, Mnemonic.AND);
            R_type.Add(37, Mnemonic.OR);
            R_type.Add(38, Mnemonic.XOR);
            R_type.Add(39, Mnemonic.NOR);
            R_type.Add(40, Mnemonic.SLT);
            R_type.Add(41, Mnemonic.SLTU);
            R_type.Add(0, Mnemonic.SLL);
            R_type.Add(2, Mnemonic.SRL);
            R_type.Add(3, Mnemonic.SRA);
            R_type.Add(4, Mnemonic.SLLV);
            R_type.Add(6, Mnemonic.SRLV);
            R_type.Add(7, Mnemonic.SRAV);
            R_type.Add(8, Mnemonic.JR);
        }

        private static void InitIJ_type()
        {
            IJ_type = new Dictionary<int, Mnemonic>();
            IJ_type.Add(8, Mnemonic.ADDI);
            IJ_type.Add(9, Mnemonic.ADDIU);
            IJ_type.Add(12, Mnemonic.ANDI);
            IJ_type.Add(13, Mnemonic.ORI);
            IJ_type.Add(14, Mnemonic.XORI);
            IJ_type.Add(35, Mnemonic.LW);
            IJ_type.Add(43, Mnemonic.SW);
            IJ_type.Add(4, Mnemonic.BEQ);
            IJ_type.Add(5, Mnemonic.BNE);
            IJ_type.Add(10, Mnemonic.SLTI);
            IJ_type.Add(11, Mnemonic.SLTIU);
            IJ_type.Add(15, Mnemonic.LUI);
            IJ_type.Add(2, Mnemonic.J);
            IJ_type.Add(3, Mnemonic.JAL);
        }

        private void ToMachineCode()
        {
            this.machine_code = InitBoolArray(AssemblerTable[this.mnemonic].ToString());
            switch (this.mnemonic)
            {
                case Mnemonic.ADD:
                    OP_ADD();
                    break;
                case Mnemonic.ADDU:
                    OP_ADDU();
                    break;
                case Mnemonic.SUB:
                    OP_SUB();
                    break;
                case Mnemonic.SUBU:
                    OP_SUBU();
                    break;
                case Mnemonic.AND:
                    OP_AND();
                    break;
                case Mnemonic.OR:
                    OP_OR();
                    break;
                case Mnemonic.XOR:
                    OP_XOR();
                    break;
                case Mnemonic.NOR:
                    OP_NOR();
                    break;
                case Mnemonic.SLT:
                    OP_SLT();
                    break;
                case Mnemonic.SLTU:
                    OP_SLTU();
                    break;
                case Mnemonic.SLL:
                    OP_SLL();
                    break;
                case Mnemonic.SRL:
                    OP_SRL();
                    break;
                case Mnemonic.SRA:
                    OP_SRA();
                    break;
                case Mnemonic.SLLV:
                    OP_SLLV();
                    break;
                case Mnemonic.SRLV:
                    OP_SRLV();
                    break;
                case Mnemonic.SRAV:
                    OP_SRAV();
                    break;
                case Mnemonic.JR:
                    OP_JR();
                    break;
                case Mnemonic.JALR:
                    OP_JALR();
                    break;
                case Mnemonic.ADDI:
                    OP_ADDI();
                    break;
                case Mnemonic.ADDIU:
                    OP_ADDIU();
                    break;
                case Mnemonic.ANDI:
                    OP_ANDI();
                    break;
                case Mnemonic.ORI:
                    OP_ORI();
                    break;
                case Mnemonic.XORI:
                    OP_XORI();
                    break;
                case Mnemonic.LUI:
                    OP_LUI();
                    break;
                case Mnemonic.SLTI:
                    OP_SLTI();
                    break;
                case Mnemonic.SLTIU:
                    OP_SLTIU();
                    break;
                case Mnemonic.LW:
                    OP_LW();
                    break;
                case Mnemonic.SW:
                    OP_SW();
                    break;
                case Mnemonic.LB:
                    OP_LB();
                    break;
                case Mnemonic.LBU:
                    OP_LBU();
                    break;
                case Mnemonic.LH:
                    OP_LH();
                    break;
                case Mnemonic.LHU:
                    OP_LHU();
                    break;
                case Mnemonic.SB:
                    OP_SB();
                    break;
                case Mnemonic.SH:
                    OP_SH();
                    break;
                case Mnemonic.BEQ:
                    OP_BEQ();
                    break;
                case Mnemonic.BNE:
                    OP_BNE();
                    break;
                case Mnemonic.BGEZ:
                    OP_BGEZ();
                    break;
                case Mnemonic.BGEZAL:
                    OP_BGEZAL();
                    break;
                case Mnemonic.BGTZ:
                    OP_BGTZ();
                    break;
                case Mnemonic.BLEZ:
                    OP_BLEZ();
                    break;
                case Mnemonic.BLTZAL:
                    OP_BLTZAL();
                    break;
                case Mnemonic.J:
                    OP_J();
                    break;
                case Mnemonic.JAL:
                    OP_JAL();
                    break;
                case Mnemonic.DIV:
                    OP_DIV();
                    break;
                case Mnemonic.DIVU:
                    OP_DIVU();
                    break;
                case Mnemonic.MULT:
                    OP_MULT();
                    break;
                case Mnemonic.MULTU:
                    OP_MULTU();
                    break;
                case Mnemonic.BLTZ:
                    OP_BLTZ();
                    break;
                case Mnemonic.BREAK:
                    OP_BREAK();
                    break;
                case Mnemonic.SYSCALL:
                    OP_SYSCALL();
                    break;
                case Mnemonic.ERET:
                    OP_ERET();
                    break;
                case Mnemonic.MFHI:
                    OP_MFHI();
                    break;
                case Mnemonic.MFLO:
                    OP_MFLO();
                    break;
                case Mnemonic.MTHI:
                    OP_MTHI();
                    break;
                case Mnemonic.MTLO:
                    OP_MTLO();
                    break;
                case Mnemonic.MFC0:
                    OP_MFC0();
                    break;
                case Mnemonic.MTC0:
                    OP_MTC0();
                    break;

                /*
                case Mnemonic.SUBI:
                    break;
                case Mnemonic.MOVE:
                    break;
                 case Mnemonic.NOP:
                    break;
                 case Mnemonic.LI:
                    break;
                 case Mnemonic.LA:
                    break;
                 case Mnemonic.SYSCALL:
                    break;
                 */
                default:
                    return;
            }
        }

        /// <summary>
        /// begin wong's
        /// </summary>
        private void ToAsmCode()
        {
             int _imm = 0;
             this.mnemonic = getMnemonic();    

             switch (this.mnemonic)
             {
                 case Mnemonic.ADD:
                 case Mnemonic.ADDU:
                 case Mnemonic.SUB:
                 case Mnemonic.SUBU:
                 case Mnemonic.AND:
                 case Mnemonic.OR:
                 case Mnemonic.XOR:
                 case Mnemonic.NOR:
                 case Mnemonic.SLT:
                 case Mnemonic.SLTU:
                 case Mnemonic.SLL:
                 case Mnemonic.SRL:
                 case Mnemonic.SRA:
                 case Mnemonic.SLLV:
                 case Mnemonic.SRLV:
                 case Mnemonic.SRAV:
                     this.arg1 = getRd();
                     this.arg2 = getRs();
                     this.arg3 = getRt();                
                     break;
                 case Mnemonic.JR:
                 case Mnemonic.JALR:
                     this.arg1 = getRs();
                     break;
                 case Mnemonic.ADDI:
                 case Mnemonic.ADDIU:
                     this.arg1 = getRt();
                     this.arg2 = getRs();
                     _imm = getImm();
                     if (_imm < 32768)
                     {
                         this.arg3 = Convert.ToString(_imm);
                     }
                     else
                     {
                         this.arg3 = Convert.ToString(_imm-65536);
                     }
                     break;
                 case Mnemonic.ANDI:
                 case Mnemonic.ORI:
                 case Mnemonic.XORI:
                     this.arg1 = getRt();
                     this.arg2 = getRs();
                     this.arg3 =  Convert.ToString(getImm());
                     break;
                 case Mnemonic.LUI:                     
                     this.arg1 = getRt();
                     _imm = getImm();
                     if (_imm < 32768)
                     {
                         this.arg2 = Convert.ToString(_imm);
                     }
                     else
                     {
                         this.arg2 = Convert.ToString(_imm-65536);
                     }
                     break;
                 case Mnemonic.SLTI:
                 case Mnemonic.SLTIU:
                     this.arg1 = getRt();
                     this.arg2 = getRs();
                     _imm = getImm();
                     if (_imm < 32768)
                     {
                         this.arg3 = Convert.ToString(_imm);
                     }
                     else
                     {
                         this.arg3 = Convert.ToString(_imm-65536);
                     }
                     break;
                 case Mnemonic.LW:
                 case Mnemonic.SW:
                 case Mnemonic.LB:
                 case Mnemonic.LBU:
                 case Mnemonic.LH:
                 case Mnemonic.LHU:
                 case Mnemonic.SB:
                 case Mnemonic.SH:
                     this.arg1 = getRt();
                     this.arg2 = getRs();
                     _imm = getImm();
                     if (_imm < 32768)
                     {
                         this.arg3 = Convert.ToString(_imm);
                     }
                     else
                     {
                         this.arg3 = Convert.ToString(_imm-65536);
                     }
                     break;
                 case Mnemonic.BEQ:
                 case Mnemonic.BNE:
                     this.arg1 = getRt();
                     this.arg2 = getRs();
                     _imm = getImm();
                     if (_imm < 32768)
                     {
                         this.arg3 = Convert.ToString(_imm);
                     }
                     else
                     {
                         this.arg3 = Convert.ToString(_imm-65536);
                     }
                     break;                     
                /* case Mnemonic.BGEZ:
                 case Mnemonic.BGEZAL:
                 case Mnemonic.BGTZ:
                 case Mnemonic.BLEZ:
                 case Mnemonic.BLTZ:
                 case Mnemonic.BLTZAL:
                     mnemonicString = mnemonicString + this.mnemonic.ToString() + " ";
                     mnemonicString = mnemonicString + this.arg1;
                     mnemonicString = mnemonicString + "," + this.arg2;
                     break;*/
                 case Mnemonic.J:
                 case Mnemonic.JAL:
                     int _address = getAdress();
                     if (_imm < 33554432)
                     {
                         this.arg1 = Convert.ToString(address);
                     }
                     else
                     {
                         this.arg1 = Convert.ToString(address-67108864);
                     }
                     break;
                 default:
                     break;
             }
             //arg3 = getRd();
             //private int address;
        }
       
        private int getImm()
        { 
            int _imm = getDecValueFromBitarray(16, 16);
            return _imm;
        }

        private int getShamt()
        {
            int _shamt = getDecValueFromBitarray(21, 5);
            return _shamt;
        }

        private int getAdress()
        {
            int _adress = getDecValueFromBitarray(6, 26);
            return _adress;
        }

        private string getRs()
        {
            int _reg = getDecValueFromBitarray(6, 5);
            if (!isAlias)
            {
                return Regs[_reg].ToString();
            }
            else
            {
                return Regs[_reg + 32].ToString();
            }
        }

        private string getRt()
        {
            int _reg = getDecValueFromBitarray(11, 5);
            if (!isAlias)
            {
                return Regs[_reg].ToString();
            }
            else
            {
                return Regs[_reg + 32].ToString();
            }
        }

        private string getRd()
        {
            int _reg = getDecValueFromBitarray(16,5);
            if (!isAlias)
            {
                return Regs[_reg].ToString() ;
            }
            else
            {
                return Regs[_reg + 32].ToString();
            }
        }

        private int getOP()
        {
            int _op = getDecValueFromBitarray(0, 6);
            return _op;
        }

        private int getfunc()
        {
            int _func = getDecValueFromBitarray(26, 6);
            return _func;
        }

        private Mnemonic getMnemonic()
        {
            int _op = 0;
            int _func = 0;
            _op = getOP();
            _func = getfunc();
            if (_op == 0)
            {
                return R_type[_func];
            }
            else
            {
                return IJ_type[_op];
            }
        }

        private int getDecValueFromBitarray(int begin, int length)
        {
            int _value = 0;
            for (int i = 0; i <length; i++)
            {
                _value *= 2;
                if (machine_code[begin + i] == true)
                {
                    _value +=1; 
                }               
            }
            return _value;
        }
        /// <summary>
        /// end
        /// </summary>
       
        private bool ConvertBit(string i)
        {
            if (i == "1")
                return true;
            else
                return false;
        }

        private void setRegMachineCode(int startPosition, string reg)
        {
            switch (reg)
            {
                case "$0":
                case "$zero":
                    this.machine_code[startPosition] = false;
                    this.machine_code[startPosition + 1] = false;
                    this.machine_code[startPosition + 2] = false;
                    this.machine_code[startPosition + 3] = false;
                    this.machine_code[startPosition + 4] = false;
                    break;
                case "$1":
                case "$at":
                    this.machine_code[startPosition] = false;
                    this.machine_code[startPosition + 1] = false;
                    this.machine_code[startPosition + 2] = false;
                    this.machine_code[startPosition + 3] = false;
                    this.machine_code[startPosition + 4] = true;
                    break;
                case "$2":
                case "$v0":
                    this.machine_code[startPosition] = false;
                    this.machine_code[startPosition + 1] = false;
                    this.machine_code[startPosition + 2] = false;
                    this.machine_code[startPosition + 3] = true;
                    this.machine_code[startPosition + 4] = false;
                    break;
                case "$3":
                case "$v1":
                    this.machine_code[startPosition] = false;
                    this.machine_code[startPosition + 1] = false;
                    this.machine_code[startPosition + 2] = false;
                    this.machine_code[startPosition + 3] = true;
                    this.machine_code[startPosition + 4] = true;
                    break;
                case "$4":
                case "$a0":
                    this.machine_code[startPosition] = false;
                    this.machine_code[startPosition + 1] = false;
                    this.machine_code[startPosition + 2] = true;
                    this.machine_code[startPosition + 3] = false;
                    this.machine_code[startPosition + 4] = false;
                    break;
                case "$5":
                case "$a1":
                    this.machine_code[startPosition] = false;
                    this.machine_code[startPosition + 1] = false;
                    this.machine_code[startPosition + 2] = true;
                    this.machine_code[startPosition + 3] = false;
                    this.machine_code[startPosition + 4] = true;
                    break;
                case "$6":
                case "$a2":
                    this.machine_code[startPosition] = false;
                    this.machine_code[startPosition + 1] = false;
                    this.machine_code[startPosition + 2] = true;
                    this.machine_code[startPosition + 3] = true;
                    this.machine_code[startPosition + 4] = false;
                    break;
                case "$7":
                case "$a3":
                    this.machine_code[startPosition] = false;
                    this.machine_code[startPosition + 1] = false;
                    this.machine_code[startPosition + 2] = true;
                    this.machine_code[startPosition + 3] = true;
                    this.machine_code[startPosition + 4] = true;
                    break;
                case "$8":
                case "$t0":
                    this.machine_code[startPosition] = false;
                    this.machine_code[startPosition + 1] = true;
                    this.machine_code[startPosition + 2] = false;
                    this.machine_code[startPosition + 3] = false;
                    this.machine_code[startPosition + 4] = false;
                    break;
                case "$9":
                case "$t1":
                    this.machine_code[startPosition] = false;
                    this.machine_code[startPosition + 1] = true;
                    this.machine_code[startPosition + 2] = false;
                    this.machine_code[startPosition + 3] = false;
                    this.machine_code[startPosition + 4] = true;
                    break;
                case "$10":
                case "$t2":
                    this.machine_code[startPosition] = false;
                    this.machine_code[startPosition + 1] = true;
                    this.machine_code[startPosition + 2] = false;
                    this.machine_code[startPosition + 3] = true;
                    this.machine_code[startPosition + 4] = false;
                    break;
                case "$11":
                case "$t3":
                    this.machine_code[startPosition] = false;
                    this.machine_code[startPosition + 1] = true;
                    this.machine_code[startPosition + 2] = false;
                    this.machine_code[startPosition + 3] = true;
                    this.machine_code[startPosition + 4] = true;
                    break;
                case "$12":
                case "$t4":
                    this.machine_code[startPosition] = false;
                    this.machine_code[startPosition + 1] = true;
                    this.machine_code[startPosition + 2] = true;
                    this.machine_code[startPosition + 3] = false;
                    this.machine_code[startPosition + 4] = false;
                    break;
                case "$13":
                case "$t5":
                    this.machine_code[startPosition] = false;
                    this.machine_code[startPosition + 1] = true;
                    this.machine_code[startPosition + 2] = true;
                    this.machine_code[startPosition + 3] = false;
                    this.machine_code[startPosition + 4] = true;
                    break;
                case "$14":
                case "$t6":
                    this.machine_code[startPosition] = false;
                    this.machine_code[startPosition + 1] = true;
                    this.machine_code[startPosition + 2] = true;
                    this.machine_code[startPosition + 3] = true;
                    this.machine_code[startPosition + 4] = false;
                    break;
                case "$15":
                case "$t7":                    
                    this.machine_code[startPosition] = false;
                    this.machine_code[startPosition + 1] = true;
                    this.machine_code[startPosition + 2] = true;
                    this.machine_code[startPosition + 3] = true;
                    this.machine_code[startPosition + 4] = true;
                    break;
                case "$16":
                case "$s0":
                    this.machine_code[startPosition] = true;
                    this.machine_code[startPosition + 1] = false;
                    this.machine_code[startPosition + 2] = false;
                    this.machine_code[startPosition + 3] = false;
                    this.machine_code[startPosition + 4] = false;
                    break;
                case "$17":
                case "$s1":                    
                    this.machine_code[startPosition] = true;
                    this.machine_code[startPosition + 1] = false;
                    this.machine_code[startPosition + 2] = false;
                    this.machine_code[startPosition + 3] = false;
                    this.machine_code[startPosition + 4] = true;
                    break;
                case "$18":
                case "$s2":
                    this.machine_code[startPosition] = true;
                    this.machine_code[startPosition + 1] = false;
                    this.machine_code[startPosition + 2] = false;
                    this.machine_code[startPosition + 3] = true;
                    this.machine_code[startPosition + 4] = false;
                    break;
                case "$19":
                case "$s3":
                    this.machine_code[startPosition] = true;
                    this.machine_code[startPosition + 1] = false;
                    this.machine_code[startPosition + 2] = false;
                    this.machine_code[startPosition + 3] = true;
                    this.machine_code[startPosition + 4] = true;
                    break;
                case "$20":
                case "$s4":
                    this.machine_code[startPosition] = true;
                    this.machine_code[startPosition + 1] = false;
                    this.machine_code[startPosition + 2] = true;
                    this.machine_code[startPosition + 3] = false;
                    this.machine_code[startPosition + 4] = false;
                    break;
                case "$21":
                case "$s5":
                    this.machine_code[startPosition] = true;
                    this.machine_code[startPosition + 1] = false;
                    this.machine_code[startPosition + 2] = true;
                    this.machine_code[startPosition + 3] = false;
                    this.machine_code[startPosition + 4] = true;
                    break;
                case "$22":
                case "$s6":
                    this.machine_code[startPosition] = true;
                    this.machine_code[startPosition + 1] = false;
                    this.machine_code[startPosition + 2] = true;
                    this.machine_code[startPosition + 3] = true;
                    this.machine_code[startPosition + 4] = false;
                    break;
                case "$23":
                case "$s7":
                    this.machine_code[startPosition] = true;
                    this.machine_code[startPosition + 1] = false;
                    this.machine_code[startPosition + 2] = true;
                    this.machine_code[startPosition + 3] = true;
                    this.machine_code[startPosition + 4] = true;
                    break;
                case "$24":
                case "$t8":
                    this.machine_code[startPosition] = true;
                    this.machine_code[startPosition + 1] = true;
                    this.machine_code[startPosition + 2] = false;
                    this.machine_code[startPosition + 3] = false;
                    this.machine_code[startPosition + 4] = false;
                    break;
                case "$25":
                case "$t9":
                    this.machine_code[startPosition] = true;
                    this.machine_code[startPosition + 1] = true;
                    this.machine_code[startPosition + 2] = false;
                    this.machine_code[startPosition + 3] = false;
                    this.machine_code[startPosition + 4] = true;
                    break;
                case "$26":
                case "$k0":
                    this.machine_code[startPosition] = true;
                    this.machine_code[startPosition + 1] = true;
                    this.machine_code[startPosition + 2] = false;
                    this.machine_code[startPosition + 3] = true;
                    this.machine_code[startPosition + 4] = false;
                    break;
                case "$27":
                case "$k1":
                    this.machine_code[startPosition] = true;
                    this.machine_code[startPosition + 1] = true;
                    this.machine_code[startPosition + 2] = false;
                    this.machine_code[startPosition + 3] = true;
                    this.machine_code[startPosition + 4] = true;
                    break;
                case "$28":
                case "$gp":
                    this.machine_code[startPosition] = true;
                    this.machine_code[startPosition + 1] = true;
                    this.machine_code[startPosition + 2] = true;
                    this.machine_code[startPosition + 3] = false;
                    this.machine_code[startPosition + 4] = false;
                    break;
                case "$29":
                case "$sp":
                    this.machine_code[startPosition] = true;
                    this.machine_code[startPosition + 1] = true;
                    this.machine_code[startPosition + 2] = true;
                    this.machine_code[startPosition + 3] = false;
                    this.machine_code[startPosition + 4] = true;
                    break;
                case "$30":
                case "$fp":
                    this.machine_code[startPosition] = true;
                    this.machine_code[startPosition + 1] = true;
                    this.machine_code[startPosition + 2] = true;
                    this.machine_code[startPosition + 3] = true;
                    this.machine_code[startPosition + 4] = false;
                    break;
                case "$31":
                case "$ra":
                    this.machine_code[startPosition] = true;
                    this.machine_code[startPosition + 1] = true;
                    this.machine_code[startPosition + 2] = true;
                    this.machine_code[startPosition + 3] = true;
                    this.machine_code[startPosition + 4] = true;
                    break;
                default:
                    break;
            }
        }

        private void setShamtMachineCode(int startPosition, string shamt)
        {
            string HEXstring = Convert.ToString(int.Parse(shamt), 2).PadLeft(5, '0');
            for (int i = 0; i < 5; i++)
            {
                if (HEXstring[i] == '1')
                {
                    machine_code[i + startPosition] = true;
                }
                else
                {
                    machine_code[i + startPosition] = false;
                }
            }
        }

        private void setImmediateMachineCode(int startPosition, string immediate)
        {
            string immediatestr = string.Empty;
            if (int.Parse(immediate) <= 32767)
            {
                immediatestr = Convert.ToString(Int16.Parse(immediate), 2).PadLeft(16, '0');
            }
            else
            {
                immediatestr = Convert.ToString(Int32.Parse(immediate), 2).PadLeft(32, '0').Substring(16, 16);
            }

            for (int i = 0; i < 16; i++)
            {
                if (immediatestr[i] == '0')
                {
                    machine_code[i + startPosition] = false;
                }
                else
                {
                    machine_code[i + startPosition] = true;
                }
            }
        }

        private void setOffsetMachineCode(int startPosition, string offset)
        {
            string offsetstr = Convert.ToString(Int16.Parse(offset), 2).PadLeft(16, '0');
            for (int i = 0; i < 16; i++)
            {
                if (offsetstr[i] == '0')
                {
                    machine_code[i + startPosition] = false;
                }
                else
                {
                    machine_code[i + startPosition] = true;
                }
            }
        }

        private void setAddressMachineCode(string address)
        {
            string addressstr = Convert.ToString(Int32.Parse(address), 2);
            addressstr = addressstr.PadLeft(32,'0');
            addressstr = addressstr.Substring(4, addressstr.Length - 6);
            for (int i = 0; i < 26; i++)
            {
                if (addressstr[i] == '0')
                {
                    machine_code[i + 6] = false;
                }
                else
                {
                    machine_code[i + 6] = true;
                }
            }
        }
        #endregion

        #region OPs
        private void OP_ADD()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.ADD].ToString());
            setRegMachineCode(16, arg1);
            setRegMachineCode(6, arg2);
            setRegMachineCode(11, arg3);
        }

        private void OP_ADDU()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.ADDU].ToString());
            setRegMachineCode(16, arg1);
            setRegMachineCode(6, arg2);
            setRegMachineCode(11, arg3);
        }

        private void OP_SUB()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.SUB].ToString());
            setRegMachineCode(16, arg1);
            setRegMachineCode(6, arg2);
            setRegMachineCode(11, arg3);
        }

        private void OP_SUBU()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.SUBU].ToString());
            setRegMachineCode(16, arg1);
            setRegMachineCode(6, arg2);
            setRegMachineCode(11, arg3);
        }

        private void OP_AND()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.AND].ToString());
            setRegMachineCode(16, arg1);
            setRegMachineCode(6, arg2);
            setRegMachineCode(11, arg3);
        }

        private void OP_OR()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.OR].ToString());
            setRegMachineCode(16, arg1);
            setRegMachineCode(6, arg2);
            setRegMachineCode(11, arg3);
        }

        private void OP_XOR()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.XOR].ToString());
            setRegMachineCode(16, arg1);
            setRegMachineCode(6, arg2);
            setRegMachineCode(11, arg3);
        }

        private void OP_NOR()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.NOR].ToString());
            setRegMachineCode(16, arg1);
            setRegMachineCode(6, arg2);
            setRegMachineCode(11, arg3);
        }

        private void OP_SLT()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.SLT].ToString());
            setRegMachineCode(16, arg1);
            setRegMachineCode(6, arg2);
            setRegMachineCode(11, arg3);
        }

        private void OP_SLTU()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.SLTU].ToString());
            setRegMachineCode(16, arg1);
            setRegMachineCode(6, arg2);
            setRegMachineCode(11, arg3);
        }

        private void OP_SLL()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.SLL].ToString());
            setRegMachineCode(16, arg1);
            setRegMachineCode(11, arg2);
            setShamtMachineCode(21, arg3);
        }

        private void OP_SRL()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.SRL].ToString());
            setRegMachineCode(16, arg1);
            setRegMachineCode(11, arg2);
            setShamtMachineCode(21, arg3);
        }

        private void OP_SRA()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.SRA].ToString());
            setRegMachineCode(16, arg1);
            setRegMachineCode(11, arg2);
            setShamtMachineCode(21, arg3);
        }

        private void OP_SLLV()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.SLLV].ToString());
            setRegMachineCode(16, arg1);
            setRegMachineCode(11, arg2);
            setRegMachineCode(6, arg3);
        }

        private void OP_SRLV()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.SRLV].ToString());
            setRegMachineCode(16, arg1);
            setRegMachineCode(11, arg2);
            setRegMachineCode(6, arg3);
        }

        private void OP_SRAV()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.SRAV].ToString());
            setRegMachineCode(16, arg1);
            setRegMachineCode(11, arg2);
            setRegMachineCode(6, arg3);
        }

        private void OP_JR()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.JR].ToString());
            setRegMachineCode(6, arg1);
        }

        private void OP_JALR()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.JR].ToString());
            setRegMachineCode(6, arg1);
        }

        private void OP_ADDI()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.ADDI].ToString());
            setRegMachineCode(11, arg1);
            setRegMachineCode(6, arg2);
            setImmediateMachineCode(16, arg3);
        }

        private void OP_ADDIU()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.ADDIU].ToString());
            setRegMachineCode(11, arg1);
            setRegMachineCode(6, arg2);
            setImmediateMachineCode(16, arg3);
        }

        private void OP_ANDI()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.ANDI].ToString());
            setRegMachineCode(11, arg1);
            setRegMachineCode(6, arg2);
            setImmediateMachineCode(16, arg3);
        }

        private void OP_ORI()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.ORI].ToString());
            setRegMachineCode(11, arg1);
            setRegMachineCode(6, arg2);
            setImmediateMachineCode(16, arg3);
        }

        private void OP_XORI()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.XORI].ToString());
            setRegMachineCode(11, arg1);
            setRegMachineCode(6, arg2);
            setImmediateMachineCode(16, arg3);
        }

        private void OP_LUI()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.LUI].ToString());
            setRegMachineCode(11, arg1);
            setImmediateMachineCode(16, arg2);
        }

        private void OP_SLTI()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.SLTI].ToString());
            setRegMachineCode(11, arg1);
            setRegMachineCode(6, arg2);
            setImmediateMachineCode(16, arg3);
        }

        private void OP_SLTIU()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.SLTIU].ToString());
            setRegMachineCode(11, arg1);
            setRegMachineCode(6, arg2);
            setImmediateMachineCode(16, arg3);
        }

        private void OP_LW()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.LW].ToString());
            setRegMachineCode(11, arg1);
            setRegMachineCode(6, arg2);
            setOffsetMachineCode(16, arg3);
        }

        private void OP_SW()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.SW].ToString());
            setRegMachineCode(11, arg1);
            setRegMachineCode(6, arg2);
            setOffsetMachineCode(16, arg3);
        }

        private void OP_LB()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.LB].ToString());
            setRegMachineCode(11, arg1);
            setRegMachineCode(6, arg2);
            setOffsetMachineCode(16, arg3);
        }

        private void OP_LBU()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.LBU].ToString());
            setRegMachineCode(11, arg1);
            setRegMachineCode(6, arg2);
            setOffsetMachineCode(16, arg3);
        }

        private void OP_LH()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.LH].ToString());
            setRegMachineCode(11, arg1);
            setRegMachineCode(6, arg2);
            setOffsetMachineCode(16, arg3);
        }

        private void OP_LHU()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.LHU].ToString());
            setRegMachineCode(11, arg1);
            setRegMachineCode(6, arg2);
            setOffsetMachineCode(16, arg3);
        }

        private void OP_SB()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.SB].ToString());
            setRegMachineCode(11, arg1);
            setRegMachineCode(6, arg2);
            setOffsetMachineCode(16, arg3);
        }

        private void OP_SH()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.SH].ToString());
            setRegMachineCode(11, arg1);
            setRegMachineCode(6, arg2);
            setOffsetMachineCode(16, arg3);
        }

        private void OP_BEQ()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.BEQ].ToString());
            setRegMachineCode(6, arg1);
            setRegMachineCode(11, arg2);
            setOffsetMachineCode(16, arg3);
        }

        private void OP_BNE()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.BNE].ToString());
            setRegMachineCode(6, arg1);
            setRegMachineCode(11, arg2);
            setOffsetMachineCode(16, arg3);
        }

        private void OP_BGEZ()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.BGEZ].ToString());
            setRegMachineCode(6, arg1);
            setRegMachineCode(11, arg2);
            setOffsetMachineCode(16, arg3);
        }

        private void OP_BGEZAL()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.BGEZAL].ToString());
            setRegMachineCode(6, arg1);
            setOffsetMachineCode(16, arg2);
        }

        private void OP_BGTZ()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.BGTZ].ToString());
            setRegMachineCode(6, arg1);
            setOffsetMachineCode(16, arg2);
        }

        private void OP_BLEZ()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.BLEZ].ToString());
            setRegMachineCode(6, arg1);
            setOffsetMachineCode(16, arg2);
        }

        private void OP_BLTZ()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.BLTZ].ToString());
            setRegMachineCode(6, arg1);
            setOffsetMachineCode(16, arg2);
        }

        private void OP_BLTZAL()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.BLTZAL].ToString());
            setRegMachineCode(6, arg1);
            setOffsetMachineCode(16, arg2);
        }

        private void OP_J()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.J].ToString());
            setAddressMachineCode(arg1);
        }

        private void OP_JAL()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.JAL].ToString());
            setAddressMachineCode(arg1);

        }

        public void OP_DIV()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.DIV].ToString());
            setRegMachineCode(6, arg1);
            setRegMachineCode(11, arg2);
        }

        public void OP_DIVU()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.DIVU].ToString());
            setRegMachineCode(6, arg1);
            setRegMachineCode(11, arg2);
        }

        public void OP_MULT()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.MULT].ToString());
            setRegMachineCode(6, arg1);
            setRegMachineCode(11, arg2);
        }

        public void OP_MULTU()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.MULTU].ToString());
            setRegMachineCode(6, arg1);
            setRegMachineCode(11, arg2);
        }

        public void OP_BREAK()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.BREAK].ToString());
        }

        public void OP_SYSCALL()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.SYSCALL].ToString());
        }

        public void OP_ERET()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.ERET].ToString());
        }

        public void OP_MFHI()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.MFHI].ToString());
            setRegMachineCode(16, arg1);
        }

        public void OP_MFLO()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.MFLO].ToString());
            setRegMachineCode(16, arg1);
        }

        public void OP_MTHI()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.MTHI].ToString());
            setRegMachineCode(16, arg1);
        }

        public void OP_MTLO()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.MTLO].ToString());
            setRegMachineCode(16, arg1);
        }

        public void OP_MFC0()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.MFC0].ToString());
            setRegMachineCode(11, arg1);
            setRegMachineCode(16, arg2);
        }

        public void OP_MTC0()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.MTC0].ToString());
            setRegMachineCode(11, arg1);
            setRegMachineCode(16, arg2);
        }
        /*private void OP_SUBI()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.SUBI].ToString());
            setAddressMachineCode(arg1);

        }

        private void OP_NOP()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.NOP].ToString());
            setAddressMachineCode(arg1);

        }

        private void OP_LI()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.LI].ToString());
            setAddressMachineCode(arg1);

        }

        private void OP_LA()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.LA].ToString());
            setAddressMachineCode(arg1);

        }

        private void OP_SYSCALL()
        {
            this.machine_code = InitBoolArray(AssemblerTable[Mnemonic.SYSCALL].ToString());
            setAddressMachineCode(arg1);

        }*/
        #endregion
    }
}