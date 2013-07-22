using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace MipsSimulator.Tools
{
    class CommonTool
    {

        public static string decToBin(int n,int L)//十进制转二进制字符串
        {
            string tmpStr = "";
            int temp = Math.Abs(n);
            int i = L;
            while (temp != 0 || i > 0)
            {
                tmpStr = (temp % 2).ToString() + tmpStr;
                temp = temp / 2;
                i--;
            }

            if (n >= 0)
            {
                if (tmpStr.Length > L||tmpStr[0]=='1')
                    return "over";
                return tmpStr;
            }
            else
            {
                char[] chars=tmpStr.ToCharArray();
                tmpStr = "";
                //取反
                for (int k = 0; k < chars.Length; k++)
                {
                    if (chars[k] == '1')
                    {
                        chars[k] = '0';
                    }
                    else
                    {
                        chars[k] = '1';
                    }
                }
                int mask = 1;
                //+1
                for (int k = chars.Length-1; k >=0; k--)
                {
                    if (chars[k] == '1')
                    {
                        if (mask == 1)
                            chars[k] = '0';
                    }
                    else
                    {
                        if (mask == 1)
                        {
                            chars[k] = '1';
                            mask = 0;
                        }
                    }
                    tmpStr = chars[k] + tmpStr;
                }

                if (tmpStr.Length > L||tmpStr[0]=='0')
                    return "over";
                return tmpStr;
            }
            
        }

        public static int binToDec(string binStr)
        {
            int tmp = 0;
            char[] chars = binStr.ToCharArray();
            string nagativeStr = "";
            if (chars[0] == '1')//负数求补码
            {
                for (int k = 0; k < chars.Length; k++)
                {
                    if (chars[k] == '1')
                    {
                        chars[k] = '0';
                    }
                    else
                    {
                        chars[k] = '1';
                    }
                }
                int mask = 1;
                //+1
                for (int k = chars.Length - 1; k >= 0; k--)
                {
                    if (chars[k] == '1')
                    {
                        if (mask == 1)
                            chars[k] = '0';
                    }
                    else
                    {
                        if (mask == 1)
                        {
                            chars[k] = '1';
                            mask = 0;
                        }
                    }
                    nagativeStr= chars[k] + nagativeStr;
                }
            }
            for (int i = chars.Length - 1; i >= 0; i--)
            {
                if(chars[i]=='1')
                {
                    tmp = tmp +(int) Math.Pow(2, chars.Length - 1 - i);
                }
            }
            if (nagativeStr!=null||nagativeStr!="")
            {
                tmp = 0 - tmp;
            }
            return tmp;
        }

        public static string sign_extend(string binStr,int length)
        {
            if (binStr.Length > length)
                return binStr;
            else
            {
                string extendStr = binStr;
                char[] chars = binStr.ToCharArray();
                if (chars[0] == '1')
                {
                    for (int i = 0; i < length - binStr.Length; i++)
                    {
                        extendStr = "1" + extendStr;
                    }
                }
                if (chars[0] == '0')
                {
                    for (int i = 0; i < length - binStr.Length; i++)
                    {
                        extendStr = "0" + extendStr;
                    }
                }
                return extendStr;
            }
        }
        public static string zero_extend(string binStr, int length)
        {
            if (binStr.Length <= length)
                return binStr;
            else
            {
                string extendStr = binStr;
                for (int i = 0; i < length - binStr.Length; i++)
                {
                    extendStr = "0" + extendStr;
                }
                return extendStr;
            }
        }
        public static string NumToStr(TypeCode typeCode, object value, string baseMark, bool ifAlignment)
        {
            string tmp = null;
            switch (typeCode)
            {
                case TypeCode.Int32:
                    {
                        tmp = ((Int32)value).ToString(baseMark + (ifAlignment ? "8" : ""));
                        break;
                    }
                case TypeCode.UInt32:
                    {
                        tmp = ((UInt32)value).ToString(baseMark + (ifAlignment ? "8" : ""));
                        break;
                    }
                case TypeCode.Int64:
                    {
                        tmp = ((Int64)value).ToString(baseMark + (ifAlignment ? "16" : ""));
                        break;
                    }
                case TypeCode.UInt64:
                    {
                        tmp = ((UInt64)value).ToString(baseMark + (ifAlignment ? "16" : ""));
                        break;
                    }
                case TypeCode.Int16:
                    {
                        tmp = ((Int16)value).ToString(baseMark + (ifAlignment ? "4" : ""));
                        break;
                    }
                case TypeCode.UInt16:
                    {
                        tmp = ((UInt16)value).ToString(baseMark + (ifAlignment ? "4" : ""));
                        break;
                    }
                case TypeCode.SByte:
                    {
                        tmp = ((SByte)value).ToString(baseMark + (ifAlignment ? "2" : ""));
                        break;
                    }
                case TypeCode.Byte:
                    {
                        tmp = ((Byte)value).ToString(baseMark + (ifAlignment ? "2" : ""));
                        break;
                    }
                case TypeCode.Single:
                    {
                        tmp = FloatNumToStr((float)value, baseMark, ifAlignment);
                        break;
                    }
                case TypeCode.Double:
                    {
                        tmp = DoubleNumToStr((double)value, baseMark, ifAlignment);
                        break;
                    }
            }
            return tmp;
        }

        static public object StrToNum(TypeCode typeCode, string str, int baseMark)
        {
            object value = null;
            try
            {
                switch (typeCode)
                {
                    case TypeCode.Int32:
                        {
                            value = Convert.ToInt32(str, baseMark);
                            break;
                        }
                    case TypeCode.UInt32:
                        {
                            value = Convert.ToUInt32(str, baseMark);
                            break;
                        }
                    case TypeCode.Int16:
                        {
                            value = Convert.ToInt16(str, baseMark);
                            break;
                        }
                    case TypeCode.UInt16:
                        {
                            value = Convert.ToUInt16(str, baseMark);
                            break;
                        }
                    case TypeCode.Int64:
                        {
                            value = Convert.ToInt64(str, baseMark);
                            break;
                        }
                    case TypeCode.UInt64:
                        {
                            value = Convert.ToUInt64(str, baseMark);
                            break;
                        }
                    case TypeCode.SByte:
                        {
                            value = Convert.ToSByte(str, baseMark);
                            break;
                        }
                    case TypeCode.Byte:
                        {
                            value = Convert.ToByte(str, baseMark);
                            break;
                        }
                    case TypeCode.Single:
                        {
                            //value = Convert.ToSingle(
                            value = FloatStrToNum(str, baseMark);
                            break;
                        }
                    case TypeCode.Double:
                        {
                            //value = Convert.ToSingle(
                            value = DoubleStrToNum(str, baseMark);
                            break;
                        }
                }
            }
            catch (System.Exception)
            {
                value = null;
            }
            return value;
        }

        static public object FloatStrToNum(string str, int baseMark)
        {
            object value = null;
            try
            {
                switch (baseMark)
                {
                    case 10:
                        {
                            value = Single.Parse(str, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign | NumberStyles.AllowLeadingWhite);
                            break;
                        }
                    case 16:
                        {
                            string tmpStr = "";

                            Int32 tmp = (Int32)CommonTool.StrToNum(TypeCode.Int32, str, 16);
                            if ((tmp & (1 << 31)) != 0)
                            {
                                tmpStr = Convert.ToString(tmp, 2);
                            }
                            else
                            {
                                tmp = tmp | (1 << 31);
                                tmpStr = Convert.ToString(tmp, 2);
                                tmpStr = '0' + tmpStr.Substring(1);
                            }
                            tmpStr = tmpStr + new string('0', 32 - tmpStr.Length);

                            string str1 = tmpStr.Substring(0, 1);
                            string str2 = tmpStr.Substring(1, 8);
                            string str3 = tmpStr.Substring(9);

                            byte num1 = (byte)CommonTool.StrToNum(TypeCode.Byte, str2, 2);
                            int num2 = Convert.ToInt32(num1);

                            if (num2 > (127 + 23))
                            {
                                return float.MaxValue;
                            }
                            else if (num1 < 127)
                            {
                                double dtmp = 0;
                                for (int i = 0; i < str3.Length; i++)
                                {
                                    if (str3[i] == '1')
                                    {
                                        dtmp += (double)1 / (double)(Math.Pow(2, (i + 1)));
                                    }
                                }
                                float result = Convert.ToSingle(dtmp);
                                if (str1 == "1") return 0 - result;
                                else return result;
                            }
                            else
                            {
                                string stra1 = str3.Substring(0, num1 - 127);
                                string stra2 = str3.Substring(num1 - 127);
                                object left = CommonTool.StrToNum(TypeCode.UInt32, stra1, 2);
                                double dtmp = 0;
                                for (int i = 0; i < stra2.Length; i++)
                                {
                                    if (stra2[i] == '1')
                                    {
                                        dtmp += (double)1 / (double)(Math.Pow(2, (i + 1)));
                                    }
                                }
                                float result = Convert.ToSingle(left) + Convert.ToSingle(dtmp);
                                if (str1 == "1") return 0 - result;
                                else return result;
                            }
                        }
                }
            }
            catch (System.Exception)
            {
                value = null;
            }
            return value;
        }

        static public string FloatNumToStr(float value, string baseMark, bool ifAlignment)
        {
            switch (baseMark)
            {
                case "D":
                    {
                        return value.ToString();
                    }
                case "X":
                    {
                        float tmp = 0;
                        if (value < 0)
                        {
                            tmp = 0 - value;
                        }
                        else
                        {
                            tmp = value;
                        }
                        string tmpStr = tmp.ToString();
                        string[] str = tmpStr.Split(new char[1] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                        UInt32 tmpint = (UInt32)CommonTool.StrToNum(TypeCode.UInt32, str[0], 10);
                        string leftStr = Convert.ToString(tmpint, 2);

                        string rightStr = "";
                        if (str.Length > 1)
                        {
                            double orginal = Convert.ToDouble(tmp);
                            double intPart = Convert.ToDouble(tmpint);
                            double floatPart = orginal - intPart;
                            while (true)
                            {
                                if (leftStr.Length + rightStr.Length >= 23) break;
                                floatPart = floatPart * 2;
                                if (floatPart > (double)1)
                                {
                                    floatPart = floatPart - (double)1;
                                    rightStr += "1";
                                }
                                else
                                {
                                    rightStr += "0";
                                }
                            }
                        }

                        string middle = "";
                        if (leftStr == "0")
                        {
                            leftStr = "";
                            middle += "01111111";
                        }
                        else
                        {
                            middle = Convert.ToString(leftStr.Length + 127, 2);
                            middle = middle + new string('0', 8 - middle.Length);
                        }
                        string headerStr = (value > (float)0) ? "0" : "1";
                        string tmpStrNew = leftStr + rightStr;
                        if (tmpStrNew.Length < 23)
                        {
                            tmpStrNew = tmpStrNew + new string('0', 23 - tmpStrNew.Length);
                        }
                        string result = headerStr + middle + tmpStrNew;
                        int resultInt = Convert.ToInt32(result, 2);
                        result = CommonTool.NumToStr(TypeCode.Int32, resultInt, "X", true);
                        return result;
                    }
            }
            return "00000000";
        }

        static public object DoubleStrToNum(string str, int baseMark)
        {
            object value = null;
            try
            {
                switch (baseMark)
                {
                    case 10:
                        {
                            value = Double.Parse(str, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign | NumberStyles.AllowLeadingWhite);
                            break;
                        }
                    case 16:
                        {
                            string tmpStr = "";

                            Int64 tmp = (Int64)CommonTool.StrToNum(TypeCode.Int64, str, 16);
                            if ((tmp & (Int64)(((Int64)1) << 63)) != 0)
                            {
                                tmpStr = Convert.ToString(tmp, 2);
                            }
                            else
                            {
                                tmp = tmp | (Int64)(((Int64)1) << 63);
                                tmpStr = Convert.ToString(tmp, 2);
                                tmpStr = '0' + tmpStr.Substring(1);
                            }
                            tmpStr = tmpStr + new string('0', 64 - tmpStr.Length);

                            string str1 = tmpStr.Substring(0, 1);
                            string str2 = tmpStr.Substring(1, 16);
                            string str3 = tmpStr.Substring(17);

                            UInt16 num1 = (UInt16)CommonTool.StrToNum(TypeCode.UInt16, str2, 2);
                            int num2 = Convert.ToInt32(num1);

                            if (num2 > (32767 + 23))
                            {
                                return Double.MaxValue;
                            }
                            else if (num1 < 32767)
                            {
                                double dtmp = 0;
                                for (int i = 0; i < str3.Length; i++)
                                {
                                    if (str3[i] == '1')
                                    {
                                        dtmp += (double)1 / (double)(Math.Pow(2, (i + 1)));
                                    }
                                }
                                double result = dtmp;
                                if (str1 == "1") return 0 - result;
                                else return result;
                            }
                            else
                            {
                                string stra1 = str3.Substring(0, num1 - 32767);
                                string stra2 = str3.Substring(num1 - 32767);
                                object left = CommonTool.StrToNum(TypeCode.UInt64, stra1, 2);
                                double dtmp = 0;
                                for (int i = 0; i < stra2.Length; i++)
                                {
                                    if (stra2[i] == '1')
                                    {
                                        dtmp += (double)1 / (double)(Math.Pow(2, (i + 1)));
                                    }
                                }
                                double result = Convert.ToDouble(left) + dtmp;
                                if (str1 == "1") return 0 - result;
                                else return result;
                            }
                        }
                }
            }
            catch (System.Exception)
            {
                value = null;
            }
            return value;
        }

        static public string DoubleNumToStr(double value, string baseMark, bool ifAlignment)
        {
            switch (baseMark)
            {
                case "D":
                    {
                        return value.ToString();
                    }
                case "X":
                    {
                        double tmp = 0;
                        if (value < 0)
                        {
                            tmp = 0 - value;
                        }
                        else
                        {
                            tmp = value;
                        }
                        string tmpStr = tmp.ToString();
                        string[] str = tmpStr.Split(new char[1] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                        Int64 tmpint = (Int64)CommonTool.StrToNum(TypeCode.Int64, str[0], 10);
                        string leftStr = Convert.ToString(tmpint, 2);

                        string rightStr = "";
                        if (str.Length > 1)
                        {
                            double orginal = Convert.ToDouble(tmp);
                            double intPart = Convert.ToDouble(tmpint);
                            double floatPart = orginal - intPart;
                            while (true)
                            {
                                if (leftStr.Length + rightStr.Length >= 47) break;
                                floatPart = floatPart * 2;
                                if (floatPart > (double)1)
                                {
                                    floatPart = floatPart - (double)1;
                                    rightStr += "1";
                                }
                                else
                                {
                                    rightStr += "0";
                                }
                            }
                        }

                        string middle = "";
                        if (leftStr == "0")
                        {
                            leftStr = "";
                            middle += "0111111111111111";
                        }
                        else
                        {
                            middle = Convert.ToString(leftStr.Length + 32767, 2);
                            middle = middle + new string('0', 16 - middle.Length);
                        }
                        string headerStr = (value > (double)0) ? "0" : "1";
                        string tmpStrNew = leftStr + rightStr;
                        if (tmpStrNew.Length < 47)
                        {
                            tmpStrNew = tmpStrNew + new string('0', 47 - tmpStrNew.Length);
                        }
                        string result = headerStr + middle + tmpStrNew;
                        Int64 resultInt = Convert.ToInt64(result, 2);
                        result = CommonTool.NumToStr(TypeCode.Int64, resultInt, "X", true);
                        return result;
                    }
            }
            return "0000000000000000";
        }
    }
}
