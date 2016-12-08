using System;
using System.Data;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Diagnostics;
using System.Web.UI;
using System.Runtime.CompilerServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.Mail;

namespace IdioSoft.ClassCommon
{
    /// <summary>
    /// ���ò�����
    /// </summary>
    public static class CommonFunction
    {
        #region "����ת������"
        /// <summary>
        /// ����ʽJs��Alert��ʾ���ַ���
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static string funString_JsToString(this string strValue)
        {

            string strTmp;
            strTmp = strValue;

            if ((strTmp != string.Empty) && (strTmp != ""))
            {
                strTmp = strTmp.Replace("'", "��");
                strTmp = strTmp.Replace("\"", "��");
                strTmp = strTmp.Replace("\n", "");
                strTmp = strTmp.Replace("\r", "");
            }
            else
            {
                strTmp = "";
            }
            return strTmp;
        }

        /// <summary>   
        /// ���ַ�����ʽ��Ϊ����SQL���ַ��������滻�ַ���'-->��;%-->��,С�ںźʹ��ں�
        /// </summary>   
        /// <param name="InputValue">�ַ���</param>
        public static string funString_SQLToString(this string InputValue)
        {
            string strTmp;
            strTmp = InputValue;
            if ((strTmp != string.Empty) && (strTmp != ""))
            {
                strTmp = strTmp.Replace("'", "��");
                strTmp = strTmp.Replace("%", "��");
                strTmp = strTmp.Replace("<", "&lt;");
                strTmp = strTmp.Replace(">", "&gt;");
            }
            else
            {
                strTmp = "";
            }
            return strTmp;
        }
        /// <summary>   
        /// ���ַ�����ʽ��Ϊ����SQL���ַ��������滻�ַ���'-->&#39;
        /// </summary>   
        /// <param name="InputValue">�ַ���</param>
        public static string funString_SQLToStringDYH(this string InputValue)
        {
            string strTmp;
            strTmp = InputValue;
            if ((strTmp != string.Empty) && (strTmp != ""))
            {
                strTmp = strTmp.Replace("'", "��");
            }
            else
            {
                strTmp = "";
            }
            return strTmp;
        }
        /// <summary>
        /// ���ַ�����ʽ��Ϊ����SQL���ַ��������滻�ַ���'-->��;%-->��,С�ںźʹ��ں�
        /// </summary>
        /// <param name="InputValue">�ַ���</param>
        /// <param name="DefaultText">����ַ���ΪNull����''����ôʹ��Ĭ��ֵ</param>
        /// <returns></returns>
        public static string funString_SQLToString(this string InputValue, string DefaultText)
        {
            string strTmp;
            strTmp = InputValue;
            if ((strTmp != string.Empty) && (strTmp != ""))
            {
                strTmp = strTmp.Replace("'", "��");
                strTmp = strTmp.Replace("%", "��");
                strTmp = strTmp.Replace("<", "&lt;");
                strTmp = strTmp.Replace(">", "&gt;");
            }
            else
            {
                strTmp = DefaultText;
            }
            return strTmp;
        }
        /// <summary>   
        /// ���ַ�����ʽ��Ϊ����SQL���ַ��������滻�ַ���'-->��;%-->��,С�ںźʹ��ں�
        /// </summary>   
        /// <param name="InputValue">�ַ���</param>
        /// <param name="MaxLen">��󳤶�</param>
        public static string funString_SQLToString(this string InputValue, int MaxLen)
        {
            string strTmp;
            strTmp = InputValue;
            if ((strTmp != string.Empty) && (strTmp != ""))
            {
                strTmp = strTmp.Replace("'", "��");
                strTmp = strTmp.Replace("%", "��");
                strTmp = strTmp.Replace("<", "&lt;");
                strTmp = strTmp.Replace(">", "&gt;");
            }
            else
            {
                strTmp = "";
            }
            if (strTmp.Length > MaxLen)
            {
                strTmp = strTmp.Substring(0, MaxLen);
            }
            return strTmp;
        }
        /// <summary>   
        /// ���ַ�����ʽ��Ϊ����SQL���ַ��������滻�ַ���'-->��;%-->��,С�ںźʹ��ں�
        /// </summary>   
        /// <param name="InputValue">�ַ���</param>
        /// <param name="DefaultText">����ַ���ΪNull����''����ôʹ��Ĭ��ֵ��Ĭ��ֵ�ĳ���Ҳ��MaxLen������</param>
        /// <param name="MaxLen">��󳤶�</param>
        public static string funString_SQLToString(this string InputValue, string DefaultText, int MaxLen)
        {
            string strTmp;
            strTmp = InputValue;
            if ((strTmp != string.Empty) && (strTmp != ""))
            {
                strTmp = strTmp.Replace("'", "��");
                strTmp = strTmp.Replace("%", "��");
                strTmp = strTmp.Replace("<", "&lt;");
                strTmp = strTmp.Replace(">", "&gt;");
            }
            else
            {
                strTmp = DefaultText;
            }
            if (strTmp.Length > MaxLen)
            {
                strTmp = strTmp.Substring(0, MaxLen);
            }
            return strTmp;
        }

        /// <summary>   
        /// ������ʽ������SQL���ַ���ת��Ϊ����SQL��䣬���滻�ַ�����-->';��-->%,С�ںźʹ��ں�
        /// </summary>   
        /// <param name="strValue">�ַ���</param>
        public static string funString_StringToSQL(this string strValue)
        {
            string strTmp;
            strTmp = strValue;
            if ((strTmp != string.Empty) && (strTmp != ""))
            {
                strTmp = strTmp.Replace("��", "'");
                strTmp = strTmp.Replace("��", "%");
                strTmp = strTmp.Replace("&lt;", "<");
                strTmp = strTmp.Replace("&gt;", ">");
            }
            else
            {
                strTmp = "";
            }
            return strTmp;
        }

        /// <summary>   
        /// ��ʽ����URL��ת���ַ���������URL��ֵ���ַ��������滻�ַ���?-->��;&-->��;=-->��;/-->��
        /// </summary>   
        /// <param name="strValue">�ַ���</param>
        public static string funString_RequestToString(this string strValue)
        {
            string strTmp;
            strTmp = strValue;
            if ((strTmp != string.Empty) && (strTmp != ""))
            {
                strTmp = strTmp.Replace("?", "��");
                strTmp = strTmp.Replace("&", "��");
                strTmp = strTmp.Replace("=", "��");
                strTmp = strTmp.Replace("/", "��");
            }
            else
            {
                strTmp = "";
            }
            return strTmp;
        }

        /// <summary>   
        /// ������ʽ������URL���ַ���ת��Ϊ����URL��䣬���滻�ַ�����-->?;��-->&;��-->=;��-->/
        /// </summary>   
        /// <param name="strValue">�ַ���</param>
        public static string funString_StringToRequest(this string strValue)
        {
            string strTmp;
            strTmp = strValue;
            if ((strTmp != string.Empty) && (strTmp != ""))
            {
                strTmp = strTmp.Replace("��", "?");
                strTmp = strTmp.Replace("��", "&");
                strTmp = strTmp.Replace("��", "=");
                strTmp = strTmp.Replace("��", "/");
            }
            else
            {
                strTmp = "";
            }
            return strTmp;
        }

        /// <summary>
        /// ���ַ�����ʽ����XML,��Ҫ�ǽ������ַ�ת��ΪXML�����ַ������滻�ַ���&lt;-->��lt;;&gt;-->��gt;;&-->��amp;;'-->��apos;;\-->��quot;
        /// </summary>
        /// <param name="strContent">�ַ���</param>
        /// <returns></returns>
        public static string funString_StringToXML(this string strContent)
        {
            string strTemp = "";
            if (strContent == "")
            {
                return strTemp;
            }
            else
            {
                strContent = strContent.Replace("<", "&lt;");
                strContent = strContent.Replace(">", "&gt;");
                strContent = strContent.Replace("&", "&amp;");
                strContent = strContent.Replace("'", "&apos;");
                strContent = strContent.Replace("\"", "&quot;");
                return strContent;
            }
        }

        /// <summary>
        /// �����������Ϊ�˷�ֹ������������","Ϊ�ָ�
        /// ����CSV��ʽ��Excelʱ��������","�ͻ���������
        /// </summary>
        /// <param name="strContent"></param>
        /// <returns></returns>
        public static string funString_ExcelCSV(this string strContent)
        {
            if (strContent != "")
            {
                strContent = strContent.Replace("\r\n", "");
            }
            if (strContent != "")
            {
                strContent = strContent.Replace(",", "��");
            }
            if (strContent != "")
            {
                strContent = strContent.Replace("\"", "");
            }
            return strContent;
        }

        /// <summary>
        /// �������������еĻ��з��滻�ɣ�br��
        /// </summary>
        /// <param name="strContent"></param>
        /// <returns></returns>
        public static string funString_StringToDisplayText(this string strContent)
        {
            string strTmp;
            strTmp = strContent;
            if ((strTmp != string.Empty) && (strTmp != ""))
            {
                strTmp = strTmp.Replace("\r\n", "<br>");
                strTmp = strTmp.Replace("\r", "<br>");
                strTmp = strTmp.Replace("\n", "<br>");
            }
            else
            {
                strTmp = "";
            }
            return strTmp;
        }

        /// <summary>   
        /// ������ת����Ӣ����д����Jan.,Feb.��
        /// </summary>   
        /// <param name="intMonth">����1��12������</param>
        public static string funString_NumMonthToEnglishAbb(this int intMonth)
        {
            string[] aryMonthAbb = { "Jan.", "Feb.", "Mar.", "Apr.", "May", "Jun.", "Jul.", "Aug.", "Sep.", "Oct.", "Nov.", "Dec." };
            return aryMonthAbb[intMonth - 1];
        }

        /// <summary>
        /// ���ַ�ת��ΪAscw��ʽ
        /// </summary>
        /// <param name="strContent"></param>
        /// <returns></returns>
        public static string funString_AscW(this string strContent)
        {
            return Microsoft.VisualBasic.Strings.AscW(strContent).ToString();
        }
        /// <summary>
        /// ���ַ�ת������,�����Ϊ���ھ�תΪ1900-01-01
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static DateTime funDateTime_StringToDatetime(this string strValue)
        {
            if (Microsoft.VisualBasic.Information.IsDate(strValue))
            {
                return DateTime.Parse(strValue);
            }
            else
            {
                return DateTime.Parse("1900-01-01");
            }
        }
        /// <summary>
        /// ���ַ����̶�λ�õ��ַ��滻����Ҫ���ַ�
        /// </summary>
        /// <param name="strValue">���е��ַ���</param>
        /// <param name="StartIndex">��ʼ�滻��λ����������0��ʼ</param>
        /// <param name="StuffLength">��Ҫ�滻�����ַ��ĳ��ȣ�����Ϊ0����Ϊ׷���ַ���</param>
        /// <param name="StuffValue">�����滻���ַ�ֵ</param>
        /// <param name="IsForceStuff">������Ȳ���������£��Ƿ�ǿ���滻��True��ǿ���滻��False��ȡ���滻</param>
        /// <returns></returns>
        public static string funString_StringStuff(this string strValue, int StartIndex, int StuffLength, string StuffValue, bool IsForceStuff)
        {
            //strValue="aabbccdd"
            //StartIndex=5
            //StuffLength=2
            //StuffValue="xx"
            //IsForceStuff=true

            string strTemp = "";
            strTemp = strValue;

            int intLength = strTemp.Length;
            if (intLength >= StartIndex + StuffLength)
            {
                #region "���ȹ�"
                strTemp = strTemp.Substring(0, StartIndex) + StuffValue + strTemp.Substring(StartIndex + StuffLength);
                #endregion
            }
            else
            {
                #region "���Ȳ���"
                if (IsForceStuff)
                {
                    //����ǿ��ת������ô�ַ����ᱻ�ӳ�
                    if (intLength <= StartIndex)
                    {
                        //�ܳ��ȶ�����
                        strTemp = strTemp + StuffValue;
                    }
                    else
                    {
                        //��ʼ�滻�Ĺ��������ܳ��Ȳ���
                        strTemp = strTemp.Substring(0, StartIndex) + StuffValue;
                    }
                }
                #endregion
            }

            return strTemp;
        }
        #endregion

        #region "���ڲ���"
        /// <summary>   
        /// �õ���ʼʱ�䵽����ʱ���м����ʱ����
        /// </summary>   
        /// <param name="strInterval">���yyyy��,q��,m��,yһ�������,d��,wһ�ܵ�����,ww��,hʱ,n����,s��</param>
        /// <param name="dtStartDate">��ʼ����</param>
        /// <param name="dtEndDate">��������</param>
        public static int funInt_DateInterval(this string strInterval, DateTime dtStartDate, DateTime dtEndDate)
        {
            return (int)Microsoft.VisualBasic.DateAndTime.DateDiff(strInterval, dtStartDate, dtEndDate, 0, 0);
        }
        /// <summary>   
        /// �õ��������ܼ�,��������,����:0Ϊ������.
        /// </summary>   
        /// <param name="dtCurrentDate">����</param>
        public static int ReturnWeekDayNumeric(this DateTime dtCurrentDate)
        {

            int intWeekDay = 0;

            switch (dtCurrentDate.DayOfWeek)
            {
                case DayOfWeek.Friday:
                    intWeekDay = 5;
                    break;
                case DayOfWeek.Monday:
                    intWeekDay = 1;
                    break;
                case DayOfWeek.Saturday:
                    intWeekDay = 6;
                    break;
                case DayOfWeek.Sunday:
                    intWeekDay = 7;
                    break;
                case DayOfWeek.Thursday:
                    intWeekDay = 4;
                    break;
                case DayOfWeek.Tuesday:
                    intWeekDay = 2;
                    break;
                case DayOfWeek.Wednesday:
                    intWeekDay = 3;
                    break;
                default:
                    intWeekDay = 0;
                    break;
            }
            return intWeekDay;
        }

        ///// <summary>
        ///// �õ������Ǳ���ĵڼ�������
        ///// </summary>
        ///// <param name="dtCurrentDate"></param>
        ///// <returns></returns>
        //public static int funInt_WeekNumber(this DateTime dtCurrentDate)
        //{
        //    //ȡ���ڵķ����ܶࣨ���������ÿؼ����Ͳ������ˡ�
        //    string firstDateText = DateTime.Now.Year.ToString() + "��1��1��";
        //    DateTime firstDay = Convert.ToDateTime(firstDateText);
        //    int theday;

        //    if (firstDay.DayOfWeek == DayOfWeek.Sunday || firstDay.DayOfWeek == DayOfWeek.Monday)
        //    {
        //        theday = 0;
        //    }
        //    else if (firstDay.DayOfWeek == DayOfWeek.Tuesday)
        //    {
        //        theday = 1;
        //    }
        //    else if (firstDay.DayOfWeek == DayOfWeek.Wednesday)
        //    {
        //        theday = 2;
        //    }
        //    else if (firstDay.DayOfWeek == DayOfWeek.Thursday)
        //    {
        //        theday = 3;
        //    }
        //    else if (firstDay.DayOfWeek == DayOfWeek.Friday)
        //    {
        //        theday = 4;
        //    }
        //    else
        //    {
        //        theday = 5;
        //    }

        //    DateTime nowDate = dtCurrentDate;
        //    int weekNum = (nowDate.DayOfYear + theday) / 7 + 1;

        //    return weekNum;
        //}

        #endregion

        #region"��֤����"
        /// <summary>   
        /// ��֤�Ƿ�������,����Ƿ���true,����false
        /// </summary>   
        /// <param name="intYear">���</param>
        public static bool funBoolean_LeapYear(this int intYear)
        {
            if (intYear % 400 == 0 || ((intYear % 4 == 0) && (intYear % 100 != 0)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>   
        /// ��֤�Ƿ�����ȷ��ʽ�绰����,����Ƿ���true,����false
        /// </summary>   
        /// <param name="strTel">�绰����</param>
        public static Boolean funBoolean_ValidTel(this string strTel)
        {
            return Regex.IsMatch(strTel, @"(\d+-)?(\d{4}-?\d{7}|\d{3}-?\d{8}|^\d{7,8})(-\d+)?");
        }
        /// <summary>   
        /// ��֤�Ƿ�����ȷ��ʽ�ֻ�����,����Ƿ���true,����false
        /// </summary>   
        /// <param name="strMobile">�ֻ�����</param>
        public static Boolean funBoolean_ValidMobile(this string strMobile)
        {
            return Regex.IsMatch(strMobile, @"\d{11}");
        }
        /// <summary>   
        /// ��֤�Ƿ�����ȷ��ʽ��������,����Ƿ���true,����false
        /// </summary>   
        /// <param name="strPostCode">��������</param>
        public static Boolean funBoolean_ValidPostcode(this string strPostCode)
        {
            return Regex.IsMatch(strPostCode, @"\d{6}");
        }
        /// <summary>   
        /// ��֤�Ƿ�����ȷ��ʽEmail,����Ƿ���true,����false
        /// </summary>   
        /// <param name="strEmail">Email</param>
        public static bool funBoolean_ValidEmail(this string strEmail)
        {
            return Regex.IsMatch(strEmail, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
        /// <summary>   
        /// ��֤�Ƿ�����ȷ��ʽ����ʱ��,����Ƿ���true,����false
        /// </summary>   
        /// <param name="strDate">���ڸ�ʽ</param>
        public static bool funBoolean_ValidDate(this string strDate)
        {
            if (Microsoft.VisualBasic.Information.IsDate(strDate))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>   
        /// ��֤�Ƿ�����ȷ��ʽ����,����Ƿ���true,����false
        /// </summary>   
        /// <param name="strNum">Email</param>
        public static bool funBoolean_ValidNumeric(this string strNum)
        {
            if (Microsoft.VisualBasic.Information.IsNumeric(strNum))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// ��֤���֤���룬����18λ��15λ
        /// </summary>
        /// <param name="strIDNo">���֤�ַ���</param>
        /// <returns></returns>
        public static bool funBoolean_ValidIDNo(this string strIDNo)
        {
            string[] aCity = new string[] { null, null, null, null, null, null, null, null, null, null, null, "����", "���", "�ӱ�", "ɽ��", "���ɹ�", null, null, null, null, null, "����", "����", "������", null, null, null, null, null, null, null, "�Ϻ�", "����", "�㽭", "��΢", "����", "����", "ɽ��", null, null, null, "����", "����", "����", "�㶫", "����", "����", null, null, null, "����", "�Ĵ�", "����", "����", "����", null, null, null, null, null, null, "����", "����", "�ຣ", "����", "�½�", null, null, null, null, null, "̨��", null, null, null, null, null, null, null, null, null, "���", "����", null, null, null, null, null, null, null, null, "����" };
            double iSum = 0;
            //string info = "";
            //System.Text.RegularExpressions.Regex rg = new Regex(@"^\d{17}(\d|x)$");
            System.Text.RegularExpressions.Regex rg = new Regex(@"^\d{17}(\d|x)$|^\d{15}$");
            System.Text.RegularExpressions.Match mc = rg.Match(strIDNo);
            if (!mc.Success)
            {
                return false;
            }
            string strtmpIDNo = "";
            //�����15λ���֤����ת����18λ���֤У��
            strtmpIDNo = strIDNo.ToLower();
            if (strtmpIDNo.Length == 15)
            {
                strtmpIDNo = ConvertIDNoLength15To18(strtmpIDNo);
            }
            strtmpIDNo = strtmpIDNo.ToLower();
            strtmpIDNo = strtmpIDNo.Replace("x", "a");
            if (aCity[int.Parse(strtmpIDNo.Substring(0, 2))] == null)
            {
                return false;//�Ƿ�����
            }
            try
            {
                DateTime.Parse(strtmpIDNo.Substring(6, 4) + "-" + strtmpIDNo.Substring(10, 2) + "-" + strtmpIDNo.Substring(12, 2));
            }
            catch
            {
                return false;//�Ƿ�����
            }
            for (int i = 17; i >= 0; i--)
            {
                iSum += (System.Math.Pow(2, i) % 11) * int.Parse(strtmpIDNo[17 - i].ToString(), System.Globalization.NumberStyles.HexNumber);
            }
            if (iSum % 11 != 1)
            {
                return false;//�Ƿ�֤��
            }

            return true;
            //return (aCity[int.Parse(strtmpIDNo.Substring(0, 2))] + "," + strtmpIDNo.Substring(6, 4) + "-" + strtmpIDNo.Substring(10, 2) + "-" + strtmpIDNo.Substring(12, 2) + "," + (int.Parse(strtmpIDNo.Substring(16, 1) % 2 == 1 ? "��" : "Ů")));

        }
        //��15λ���֤����ת��Ϊ18λ
        private static string ConvertIDNoLength15To18(string strIDNo)
        {
            char[] strJiaoYan = { '1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2' };
            int[] intQuan = { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2, 1 };
            string strTemp;
            int intTemp = 0;
            strTemp = strIDNo.Substring(0, 6) + "19" + strIDNo.Substring(6);
            for (int i = 0; i <= strTemp.Length - 1; i++)
            {
                intTemp += int.Parse(strTemp.Substring(i, 1)) * intQuan[i];
            }
            intTemp = intTemp % 11;
            return strTemp + strJiaoYan[intTemp];
        }

        /// <summary>
        /// ��֤�Ƿ�Ϊ����
        /// </summary>
        /// <param name="strIn">��Ҫ��֤���ַ���</param>
        /// <returns></returns>
        public static bool funBoolean_ValidChineseChar(this string strIn)
        {
            int code = 0;
            int chfrom = Convert.ToInt32("4e00", 16);    //��Χ��0x4e00��0x9fff��ת����int��chfrom��chend��
            int chend = Convert.ToInt32("9fff", 16);
            if (strIn != "")
            {
                code = Char.ConvertToUtf32(strIn, 0);    //����ַ���input��ָ������index���ַ�unicode����

                if (code >= chfrom && code <= chend)
                {
                    return true;     //��code�����ķ�Χ�ڷ���true

                }
                else
                {
                    return false;    //��code�������ķ�Χ�ڷ���false
                }
            }
            return false;
        }

        #endregion

        #region "������֤���������ҷ���ֵ"
        /// <summary>   
        /// ��֤�Ƿ�������,���true����strValue,false����intDefault
        /// </summary>   
        /// <param name="strValue">�ַ���</param>
        /// <param name="intDefault">Ĭ��ֵ</param>
        public static int funInt_StringToInt(this string strValue, int intDefault)
        {
            if (Microsoft.VisualBasic.Information.IsNumeric(strValue))
            {
                return int.Parse(strValue);
            }
            else
            {
                return intDefault;
            }
        }
        /// <summary>   
        /// ��֤�Ƿ�������,���true,���ж��Ƿ���intMin��intMax֮��,���true����strValue,false����intDefault
        /// </summary>   
        /// <param name="strValue">�ַ���</param>
        /// <param name="intMin">�������С����ֵ</param>
        /// <param name="intMax">������������ֵ</param>
        /// <param name="intDefault">Ĭ��ֵ</param>
        public static int funInt_StringToInt(this string strValue, int intMin, int intMax, int intDefault)
        {
            if (Microsoft.VisualBasic.Information.IsNumeric(strValue))
            {
                int intValue;
                intValue = int.Parse(strValue);
                if (intMin <= intValue && intValue <= intMax)
                {
                    return intValue;
                }
                else
                {
                    return intDefault;
                }
            }
            else
            {
                return intDefault;
            }
        }
        /// <summary>   
        /// ��֤�Ƿ�������,���ҷ���decimal(��С��������),���true����strValue,false����intDefault
        /// </summary>   
        /// <param name="strValue">�ַ���</param>
        /// <param name="decDefault">Ĭ��ֵ</param>
        public static decimal funDec_StringToDecimal(this string strValue, decimal decDefault)
        {
            if (Microsoft.VisualBasic.Information.IsNumeric(strValue))
            {
                return decimal.Parse(strValue);
            }
            else
            {
                return decDefault;
            }
        }
        /// <summary>   
        /// ��֤�Ƿ���Uniqueidentifier��ʽ,���ҷ���string,���true����strValue,false����strDefault
        /// </summary>   
        /// <param name="strValue">�ַ���</param>
        /// <param name="strDefault">Ĭ��ֵ</param>
        public static string funUuid_StringToUniqueidentifier(this string strValue, string strDefault)
        {
            if (strValue == null || strValue == "")
            {
                return strDefault;
            }
            else
            {
                try
                {
                    Guid gd = new Guid(strValue);
                    return strValue;
                }
                catch
                {
                    return strDefault;
                }
            }

        }
        /// <summary>   
        /// ��Bool�ַ���ת��Ϊ0,1
        /// </summary>   
        /// <param name="strValue">�ַ���</param>
        public static int funInt_BoolToString(this string strValue)
        {
            //return (strValue.Trim().ToLower() == "true") ? 1 : 0;
            string strTemp = strValue.Trim().ToLower();
            if (strTemp == "true" || strTemp == "1")
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// ���ַ�ת��Ϊboolean��������true��Ϊtrue�������false��Ϊfalse�������0��Ϊfalse������0�Ķ���True
        /// </summary>
        /// <param name="strContent"></param>
        /// <returns></returns>
        public static Boolean funBoolean_StringToBoolean(this string strContent)
        {
            if (strContent.ToLower() == "true")
            {
                return true;
            }
            if (strContent.ToLower() == "false")
            {
                return false;
            }
            int intContent = funInt_StringToInt(strContent, 0);
            if (intContent > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>   
        /// ��֤�Ƿ���ʱ�䣬���ҷ������ݿ�������ַ��������''����ô����null�����򣬷���'+date+'
        /// </summary>   
        /// <param name="strValue">�ַ���</param>
        public static string funString_StringToDBDate(this string strValue)
        {
            if (strValue == "")
            {
                return "NULL";
            }
            else
            {
                if (funBoolean_ValidDate(strValue))
                {
                    return "'" + strValue + "'";
                }
                else
                {
                    return "NULL";
                }
            }
        }
        /// <summary>
        /// �������ݿ�������ַ�����������ַ������ͼ���''������ͷ���Ĭ��ֵ
        /// </summary>
        /// <param name="strValue"></param>
        /// <param name="strDefault">Ĭ��ֵ</param>
        /// <returns></returns>
        public static string funString_StringToDBString(this string strValue, string strDefault)
        {
            if (strValue == null || strValue == "")
            {
                return strDefault;
            }
            else
            {
                return "'" + strValue + "'";
            }
        }

        /// <summary>
        /// ���طǿյ��ַ���
        /// </summary>
        /// <param name="strValue">��Ҫ��֤���ַ���</param>
        /// <param name="strDefault">���Ϊ�յ�Ĭ��ֵ</param>
        /// <returns></returns>
        public static string funString_ValidEmptyString(this string strValue, string strDefault)
        {
            string strTmp;
            strTmp = strValue;
            if ((strTmp == string.Empty) || (strTmp == ""))
            {
                strTmp = strDefault;
            }
            return strTmp;
        }

        /// <summary>
        /// ����ֻ������ڵ��ַ����ַ�
        /// </summary>
        /// <param name="strValue">��ҪУ����ַ���</param>
        /// <param name="strAllowString">ֻ������ڵ��ַ�</param>
        /// <returns></returns>
        public static string funString_ValidReplaceString(this string strValue, string strAllowString)
        {
            int intLen = 0;
            intLen = strValue.Length;

            string strTemp = "";
            string strChar = "";
            //ѭ�����е��ַ�
            for (int i = 0; i < intLen; i++)
            {
                strChar = strValue.Substring(i, 1);
                //������ַ�������ķ�Χ�ڵ��ַ�����ô��ӣ��������
                if (strAllowString.IndexOf(strChar) >= 0)
                {
                    strTemp = strTemp + strChar;
                }
            }
            return strTemp;
        }
        #endregion

        #region "�ļ�������غ���"
        /// <summary>
        /// ��֤ѡ���ϴ����ļ��Ƿ���������ļ���
        /// </summary>
        /// <param name="fileAttachmentName">�ϴ��ļ��ؼ�</param>
        /// <param name="blnAllowNull">�Ƿ�������ļ���TrueΪ����FalseΪ������</param>
        /// <param name="blnFilter">������strFilter���͵��ļ��ϴ����ǲ�����strFilter���͵��ļ��ϴ���TrueΪ������FalseΪ����</param>
        /// <param name="strFilter">����ʲô�����ļ�Ϊ�����ϴ�,���Ϊfalse,ֻ����Filter�е��ļ�����,trueΪ����ΪFilter�е��ļ�����|.asp|.exe|.htm|.html|.aspx|.cs|.vb|.js|</param>
        /// <returns></returns>
        public static string funString_FileUploadFormat(this FileUpload fileAttachmentName, bool blnAllowNull, bool blnFilter, string strFilter)
        {
            if (fileAttachmentName.HasFile)
            {
                string strFileName = "c:\\" + fileAttachmentName.FileName;
                FileInfo f = new FileInfo(strFileName);
                string strDocExtension = f.Extension.ToLower();
                if (!blnFilter)
                {
                    if (strFilter.IndexOf(strDocExtension) < 0)
                    {
                        return "�������ϴ�" + strFilter + "�Ⱥ�׺��������ļ���";
                    }
                }
                else
                {
                    if (strFilter.IndexOf(strDocExtension) >= 0)
                    {
                        return "�������ϴ�" + strFilter + "�Ⱥ�׺�����ļ���";
                    }
                }
            }
            else
            {
                if (!blnAllowNull)
                {
                    return "��ѡ����ظ�����";
                }
            }
            return "";
        }

        /// <summary>
        /// �õ��ϴ��ĸ�����ʽ��������һ���ʽΪyyyyMMddHHmmss
        /// </summary>
        /// <param name="strAttachmentName">�����ļ���</param>
        /// <param name="FileNameLength">���ļ����ĳ���</param>
        /// <returns></returns>
        public static string funString_FormatAttachmentName(this string strAttachmentName, int FileNameLength)
        {
            //�õ��ļ���
            string strFileName;
            strFileName = strAttachmentName;
            if (strFileName == "")
            {
                return "";
            }

            //�õ�ԭ�ļ��ĺ�׺��(����.��)
            int intSuffix = strFileName.LastIndexOf(".");
            string strSuffix = "";
            if (intSuffix < 0)
            {
                intSuffix = 0;
                strSuffix = "";
            }
            else
            {
                strSuffix = strFileName.Substring(intSuffix, strFileName.Length - intSuffix);
                intSuffix = strFileName.Length - intSuffix;
            }

            //�õ�����ʱ���ʽ����ļ�ʵ�ʳ���
            int intLen;
            intLen = strFileName.Length + 14;//14��ʱ���ַ����ĳ���

            //������ʵ�ʳ��ȳ������õ��ļ�������
            if (intLen > FileNameLength)
            {
                intLen = FileNameLength;
            }
            //���ļ������ȼ���Ĭ�ϵ�ʱ��ĳ��Ⱥͺ�׺����������Ҫʹ�õ��ļ����Ƴ���
            intLen = intLen - 14 - intSuffix;
            //�ļ���=ʱ���ʽ+�ļ���+��׺
            strFileName = System.DateTime.Now.ToString("yyyyMMddHHmmss") + strFileName.Substring(0, intLen) + strSuffix;

            return strFileName;
        }

        /// <summary>
        /// �ϴ��ļ�����,�������ļ���,����û���ָ���ļ���,�ļ�������ʽΪyyyyMMddhhmmss
        /// </summary>
        /// <param name="fileAttachmentName">�ϴ��ļ��ؼ�</param>
        /// <param name="strFileName">���ļ���</param>
        /// <param name="FileNameLength">�����ָ���ļ�������ô�ļ�������󳤶�</param>
        /// <param name="strDirPath">�����ļ�Ŀ¼����ʹ��/��β</param>
        /// <returns></returns>
        public static string funString_FileUpLoadAttachment(this FileUpload fileAttachmentName, string strFileName, int FileNameLength, string strDirPath)
        {
            FileUpload fileAttachment;
            fileAttachment = fileAttachmentName;
            strFileName = (strFileName == "") ? funString_FormatAttachmentName(fileAttachmentName.FileName, FileNameLength) : strFileName;
            if (fileAttachment.HasFile)
            {
                string SaveAddPath = "";
                SaveAddPath = System.Web.HttpContext.Current.Server.MapPath(strDirPath);
                if (!Directory.Exists(SaveAddPath))
                {
                    Directory.CreateDirectory(SaveAddPath);
                }
                //SaveAddPath=SaveAddPath+"\\";
                SaveAddPath += strFileName;
                fileAttachment.SaveAs(SaveAddPath);
            }
            else
            {
                strFileName = "";
            }
            return strFileName;
        }

        /// <summary>
        /// ɾ���ļ�
        /// </summary>
        /// <param name="FileName">�ļ�����</param>
        /// <param name="strPath">�ļ����·��,��/</param>
        /// <returns></returns>
        public static string funString_FileDelete(this string FileName, string strPath)
        {
            string strFileName = System.Web.HttpContext.Current.Server.MapPath(strPath + FileName);
            try
            {
                if (File.Exists(strFileName))
                {
                    File.Delete(strFileName);
                }
            }
            catch
            {
                return "";
            }
            return "";
        }

        /// <summary>
        /// ��ȡ�ļ�������
        /// </summary>
        /// <param name="strRelativePath">�ļ������·��</param>
        /// <param name="objEncoding">�����ʽ</param>
        /// <returns></returns>
        public static string funString_FileContent(this string strRelativePath, Encoding objEncoding)
        {
            string strAbsolutePath = System.Web.HttpContext.Current.Server.MapPath(strRelativePath);
            if (!File.Exists(strAbsolutePath))
            {
                return "";
            }
            FileStream objFileStream = new FileStream(strAbsolutePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            StreamReader objStreamReader = new StreamReader(objFileStream, objEncoding);
            string strConent = "";
            strConent = objStreamReader.ReadToEnd();
            objStreamReader.Close();
            objFileStream.Close();
            return strConent;
        }


        #endregion

        #region "ComboBox,CheckBoxList,RadioButtonList����"
        /// <summary>   
        /// ����Value����ѡ��ComboBox��ѡ��
        /// </summary>   
        /// <param name="cboName">ComboBox</param>
        /// <param name="SelectedValue">Ҫѡ�е�Item��ֵ</param>
        public static void subComboBox_SelectItemByValue(this HtmlSelect cboName, string SelectedValue)
        {
            int intCount;
            intCount = cboName.Items.Count;
            for (int i = 0; i < intCount; i++)
            {
                if (cboName.Items[i].Value.ToLower() == SelectedValue.ToLower())
                {
                    cboName.SelectedIndex = i;
                    break;
                }
            }
        }
        /// <summary>   
        /// ����Value����ѡ��ComboBox��ѡ��
        /// </summary>   
        /// <param name="cboName">ComboBox</param>
        /// <param name="SelectedValue">Ҫѡ�е�Item��ֵ</param>
        public static void subComboBox_SelectItemByValue(this DropDownList cboName, string SelectedValue)
        {
            int intCount;
            intCount = cboName.Items.Count;
            for (int i = 0; i < intCount; i++)
            {
                if (cboName.Items[i].Value.ToLower() == SelectedValue.ToLower())
                {
                    cboName.SelectedIndex = i;
                    break;
                }
            }
        }
        /// <summary>   
        /// ����Value����ѡ��Item�����û��ѡ����Ӧѡ���ô��ѡ��Ĭ��ѡ��
        /// </summary>   
        /// <param name="cboName">ComboBox</param>
        /// <param name="SelectedValue">Ҫѡ�е�Item��ֵ</param>
        /// <param name="DefaultValue">Ĭ��ѡ���Value</param>
        public static void subComboBox_SelectItemByValue(this HtmlSelect cboName, string SelectedValue, string DefaultValue)
        {
            int i;
            int intCount;
            intCount = cboName.Items.Count;
            bool blnSelected = false;
            for (i = 0; i < intCount; i++)
            {
                if (cboName.Items[i].Value == SelectedValue)
                {
                    cboName.SelectedIndex = i;
                    blnSelected = true;
                    break;
                }
            }
            if (blnSelected == false)
            {
                string strItemValue;
                for (i = 0; i < intCount; i++)
                {
                    strItemValue = cboName.Items[i].Value.ToLower();
                    if (strItemValue == DefaultValue)
                    {
                        cboName.SelectedIndex = i;
                        break;
                    }
                }
            }
        }
        /// <summary>   
        /// ����Value����ѡ��Item�����û��ѡ����Ӧѡ���ô��ѡ��Ĭ��ѡ��
        /// </summary>   
        /// <param name="cboName">ComboBox</param>
        /// <param name="SelectedValue">Ҫѡ�е�Item��ֵ</param>
        /// <param name="DefaultValue">Ĭ��ѡ���Value</param>
        public static void subComboBox_SelectItemByValue(this DropDownList cboName, string SelectedValue, string DefaultValue)
        {
            int i;
            int intCount;
            intCount = cboName.Items.Count;
            bool blnSelected = false;
            for (i = 0; i < intCount; i++)
            {
                if (cboName.Items[i].Value == SelectedValue)
                {
                    cboName.SelectedIndex = i;
                    blnSelected = true;
                    break;
                }
            }
            if (blnSelected == false)
            {
                string strItemValue;
                for (i = 0; i < intCount; i++)
                {
                    strItemValue = cboName.Items[i].Value.ToLower();
                    if (strItemValue == DefaultValue)
                    {
                        cboName.SelectedIndex = i;
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// ���CheckListBox
        /// </summary>
        /// <param name="objchkListBox"></param>
        /// <param name="strSearchSQL">��һλΪID,�ڶ�λΪText</param>
        public static void subCheckListBox_Load(this CheckBoxList chklstName, string strSearchSQL)
        {
            chklstName.Items.Clear();
            IdioSoft.ClassDbAccess.ClassDbAccess objclassDbAccess = new IdioSoft.ClassDbAccess.ClassDbAccess();
            DataSet ds = new DataSet();
            ds = objclassDbAccess.funDataset_SQLExecuteNonQuery(strSearchSQL);
            ListItem item;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                item = new ListItem();
                item.Text = ds.Tables[0].Rows[i][1].ToString();
                item.Value = ds.Tables[0].Rows[i][0].ToString();
                chklstName.Items.Add(item);
            }
        }
        /// <summary>
        /// ���CheckListBox��SQL����
        /// </summary>
        /// <param name="objchkListBox"></param>
        /// <param name="strSearchSQL">�е�Text</param>
        public static void subCheckListBox_LoadColumns(this CheckBoxList chklstName, string strSearchSQL)
        {
            chklstName.Items.Clear();
            IdioSoft.ClassDbAccess.ClassDbAccess objclassDbAccess = new IdioSoft.ClassDbAccess.ClassDbAccess();
            DataSet ds = new DataSet();
            ds = objclassDbAccess.funDataset_SQLExecuteNonQuery(strSearchSQL);
            ListItem item;
            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                item = new ListItem();
                item.Text = ds.Tables[0].Columns[i].ColumnName.ToString();
                item.Value = ds.Tables[0].Columns[i].ColumnName.ToString();
                chklstName.Items.Add(item);
            }
        }



        /// <summary>
        /// ����Value��ѡCheckListBox
        /// </summary>
        /// <param name="objchkListBox"></param>
        /// <param name="strValues"></param>
        public static void subCheckListBox_CheckByValues(this CheckBoxList chklstName, string strValues)
        {
            strValues = "," + strValues + ",";
            for (int i = 0; i < chklstName.Items.Count; i++)
            {
                if (strValues.IndexOf("," + ((ListItem)chklstName.Items[i]).Value) >= 0)
                {
                    chklstName.Items[i].Selected = true;
                }
                else
                {
                    chklstName.Items[i].Selected = false;
                }
            }
        }
        /// <summary>
        /// ����CheckListBox��ѡ�е�ֵ
        /// </summary>
        /// <param name="objchkListBox"></param>
        /// <returns></returns>
        public static string funCheckListBox_SelectValue(this CheckBoxList chklstName)
        {
            string strValues = "";
            for (int i = 0; i < chklstName.Items.Count; i++)
            {
                if (chklstName.Items[i].Selected)
                {
                    strValues += ((ListItem)chklstName.Items[i]).Value + ",";
                }
            }
            if (strValues != "")
            {
                strValues = strValues.Substring(0, strValues.Length - 1);
            }
            return strValues;
        }
        /// <summary>
        /// ����CheckListBox��ѡ�е�ֵ��[]����
        /// </summary>
        /// <param name="objchkListBox"></param>
        /// <returns></returns>
        public static string funCheckListBox_SelectValueColumns(this CheckBoxList chklstName)
        {
            string strValues = "";
            for (int i = 0; i < chklstName.Items.Count; i++)
            {
                if (chklstName.Items[i].Selected)
                {
                    strValues += "["+((ListItem)chklstName.Items[i]).Value + "],";
                }
            }
            if (strValues != "")
            {
                strValues = strValues.Substring(0, strValues.Length - 1);
            }
            return strValues;
        }
        /// <summary>
        /// ����CheckListBox��ѡ�е��ı�
        /// </summary>
        /// <param name="chklstName"></param>
        /// <returns></returns>
        public static string funCheckListBox_SelectText(this CheckBoxList chklstName)
        {
            string strValues = "";
            for (int i = 0; i < chklstName.Items.Count; i++)
            {
                if (chklstName.Items[i].Selected)
                {
                    strValues += ((ListItem)chklstName.Items[i]).Text + ",";
                }
            }
            if (strValues != "")
            {
                strValues = strValues.Substring(0, strValues.Length - 1);
            }
            return strValues;
        }
        /// <summary>   
        /// ����ѡ��CheckBoxList��ѡ��
        /// </summary>   
        /// <param name="chklstName">CheckBoxList</param>
        /// <param name="SelectedValues">Ҫѡ�е�Item��ֵ�б�</param>
        /// <param name="strSplitChar">SelectedValues�еķָ��</param>
        public static void subCheckBoxList_SelectItemByValue(this CheckBoxList chklstName, string SelectedValues, string strSplitChar)
        {
            int intCount;
            intCount = chklstName.Items.Count;
            string strSelectedValues = "";
            strSelectedValues = strSplitChar + SelectedValues.ToLower() + strSplitChar;
            for (int i = 0; i < intCount; i++)
            {
                if (strSelectedValues.IndexOf(strSplitChar + chklstName.Items[i].Value.ToString() + strSplitChar) >= 0)
                {
                    chklstName.Items[i].Selected = true;
                }
                else
                {
                    chklstName.Items[i].Selected = false;
                }
            }
        }
        /// <summary>   
        /// ����Text����ѡ��ComboBox��ѡ��
        /// </summary>   
        /// <param name="cboName">ComboBox</param>
        /// <param name="SelectedValue">Ҫѡ�е�Item��ֵ</param>
        public static void subComboBox_SelectItemByText(this HtmlSelect cboName, string SelectedValue)
        {
            int i;
            int intCount;
            intCount = cboName.Items.Count;
            for (i = 0; i < intCount; i++)
            {
                if (cboName.Items[i].Text.ToLower() == SelectedValue.ToLower())
                {
                    //cboName.Items[i].Selected = true;
                    cboName.SelectedIndex = i;
                    break;
                }
            }
        }
        /// <summary>   
        /// ����Text����ѡ��ComboBox��ѡ��
        /// </summary>   
        /// <param name="cboName">ComboBox</param>
        /// <param name="SelectedValue">Ҫѡ�е�Item��ֵ</param>
        public static void subComboBox_SelectItemByText(this DropDownList cboName, string SelectedValue)
        {
            int i;
            int intCount;
            intCount = cboName.Items.Count;
            for (i = 0; i < intCount; i++)
            {
                if (cboName.Items[i].Text.ToLower() == SelectedValue.ToLower())
                {
                    //cboName.Items[i].Selected = true;
                    cboName.SelectedIndex = i;
                    break;
                }
            }
        }
        /// <summary>   
        /// ����Text����ѡ��Item�����û��ѡ����Ӧѡ���ô��ѡ��Ĭ��ѡ��
        /// </summary>   
        /// <param name="cboName">ComboBox</param>
        /// <param name="SelectedValue">Ҫѡ�е�Item��ֵ</param>
        /// <param name="DefaultValue">Ĭ��ѡ���Text</param>
        public static void subComboBox_SelectItemByText(this HtmlSelect cboName, string SelectedValue, string DefaultValue)
        {
            int i;
            int intCount;
            intCount = cboName.Items.Count;
            bool blnSelected = false;
            for (i = 0; i < intCount; i++)
            {
                if (cboName.Items[i].Text == SelectedValue)
                {
                    cboName.SelectedIndex = i;
                    blnSelected = true;
                    break;
                }
            }
            if (blnSelected == false)
            {
                string strItemText;
                for (i = 0; i < intCount; i++)
                {
                    strItemText = cboName.Items[i].Text.ToLower();
                    if (strItemText == DefaultValue)
                    {
                        cboName.SelectedIndex = i;
                        break;
                    }
                }
            }
        }
        /// <summary>   
        /// ����Text����ѡ��Item�����û��ѡ����Ӧѡ���ô��ѡ��Ĭ��ѡ��
        /// </summary>   
        /// <param name="cboName">ComboBox</param>
        /// <param name="SelectedValue">Ҫѡ�е�Item��ֵ</param>
        /// <param name="DefaultValue">Ĭ��ѡ���Text</param>
        public static void subComboBox_SelectItemByText(this DropDownList cboName, string SelectedValue, string DefaultValue)
        {
            int i;
            int intCount;
            intCount = cboName.Items.Count;
            bool blnSelected = false;
            for (i = 0; i < intCount; i++)
            {
                if (cboName.Items[i].Text == SelectedValue)
                {
                    cboName.SelectedIndex = i;
                    blnSelected = true;
                    break;
                }
            }
            if (blnSelected == false)
            {
                string strItemText;
                for (i = 0; i < intCount; i++)
                {
                    strItemText = cboName.Items[i].Text.ToLower();
                    if (strItemText == DefaultValue)
                    {
                        cboName.SelectedIndex = i;
                        break;
                    }
                }
            }
        }
        /// <summary>   
        /// ���������е�ComboBox
        /// </summary>   
        /// <param name="cboName">ComboBox</param>
        /// <param name="strSearchSQL">ȡֵ��SQL</param>
        /// <param name="IsDisplayDefaultText">�Ƿ���ʾĬ�ϵ�ѡ��</param>
        public static void subComboBox_LoadItemsByDBColumnName(this HtmlSelect cboName, string strSearchSQL, bool IsDisplayDefaultText)
        {
            cboName.Items.Clear();
            ListItem item;
            if (IsDisplayDefaultText)
            {
                item = new ListItem();
                item.Value = "SelectAll";
                item.Text = "-----ѡ�����м�¼-----";
                cboName.Items.Add(item);
            }

            string strSQL;
            strSQL = strSearchSQL;
            IdioSoft.ClassDbAccess.ClassDbAccess objclassDbAccess = new IdioSoft.ClassDbAccess.ClassDbAccess();
            DataSet ds = new DataSet();
            ds = objclassDbAccess.funDataset_SQLExecuteNonQuery(strSearchSQL);

            int intCount = -1;
            intCount = ds.Tables[0].Columns.Count;

            for (int i = 0; i < intCount; i++)
            {
                item = new ListItem();
                item.Text = ds.Tables[0].Columns[i].ColumnName;
                item.Value = ds.Tables[0].Columns[i].ColumnName;
                cboName.Items.Add(item);
            }
            cboName.SelectedIndex = 0;
        }
        /// <summary>
        /// ���������˵����
        /// </summary>
        /// <param name="cboBox"></param>
        /// <param name="blnCategory"></param>
        public static void subComboBox_LoadItems(this HtmlSelect cboBox, bool blnCategory)
        {
            if (blnCategory)
            {
                cboBox.Items.Clear();
                ListItem item;
                item = new ListItem("True", "1");
                cboBox.Items.Add(item);
                item = new ListItem("False", "0");
                cboBox.Items.Add(item);
                cboBox.SelectedIndex = 0;
            }
        }
        /// <summary>   
        /// ���������е�ComboBox
        /// </summary>   
        /// <param name="cboName">ComboBox</param>
        /// <param name="strSearchSQL">ȡֵ��SQL</param>
        /// <param name="IsDisplayDefaultText">�Ƿ���ʾĬ�ϵ�ѡ��</param>
        public static void subComboBox_LoadItemsByDBColumnName(this DropDownList cboName, string strSearchSQL, bool IsDisplayDefaultText)
        {
            cboName.Items.Clear();
            ListItem item;
            if (IsDisplayDefaultText)
            {
                item = new ListItem();
                item.Value = "SelectAll";
                item.Text = "-----ѡ�����м�¼-----";
                cboName.Items.Add(item);
            }
            string strSQL;
            strSQL = strSearchSQL;
            IdioSoft.ClassDbAccess.ClassDbAccess objclassDbAccess = new IdioSoft.ClassDbAccess.ClassDbAccess();
            DataSet ds = new DataSet();
            ds = objclassDbAccess.funDataset_SQLExecuteNonQuery(strSearchSQL);
            if (ds != null && ds.Tables[0].Columns.Count > 0)
            {
                int intCount = -1;
                intCount = ds.Tables[0].Columns.Count;

                for (int i = 0; i < intCount; i++)
                {
                    item = new ListItem();
                    item.Text = ds.Tables[0].Columns[i].ColumnName;
                    item.Value = ds.Tables[0].Columns[i].ColumnName;
                    cboName.Items.Add(item);
                }
                cboName.SelectedIndex = 0;
            }
        }
        /// <summary>
        /// ����RadioButtonList�Ƿ���ѡ����,���ѡ�е�ItemֵΪ1�ͷ���1,���򷵻�0,����SelectedIndexΪ0����null,���������Ҫ�������ݿ��RadioButtonListֵ��.
        /// </summary>   
        /// <param name="rdoButtonlist">RadioButtonList</param>
        public static string funBoolean_RadioButtonListCheck(this RadioButtonList rdoButtonlist)
        {
            string strCheck = "";
            for (int i = 0; i < rdoButtonlist.Items.Count; i++)
            {
                if (rdoButtonlist.Items[i].Selected)
                {
                    if (rdoButtonlist.Items[i].Value == "1")
                    {
                        strCheck = "1";
                        break;
                    }
                    else
                    {
                        strCheck = "0";
                    }
                }
            }
            if (rdoButtonlist.SelectedIndex < 0)
            {
                strCheck = "null";
            }
            return strCheck;
        }
        /// <summary>
        /// ����RadioButtonList��ѡ�е���
        /// </summary>   
        /// <param name="rdoButtonlist">RadioButtonList</param>
        /// <param name="itemValue">��Ҫ��ѡ�е�Item���ֵ</param>
        public static void subRadioButtonList_CheckItem(this RadioButtonList rdoButtonlist, string itemValue)
        {
            string strTmp = "";
            strTmp = "";
            if (itemValue.ToLower() == "false")
            {
                strTmp = "0";
            }
            if (itemValue.ToLower() == "true")
            {
                strTmp = "1";
            }
            if (strTmp == "")
            {
                strTmp = itemValue;
            }
            for (int i = 0; i < rdoButtonlist.Items.Count; i++)
            {
                rdoButtonlist.Items[i].Selected = false;
                if (rdoButtonlist.Items[i].Value.ToString() == strTmp)
                {
                    rdoButtonlist.Items[i].Selected = true;
                    break;
                }
            }
        }
        public static void subRadioButtonList_Selected(this RadioButtonList rdoButtonlist, string itemValue)
        {
        //    for (int i = 0; i < rdoButtonlist.Items.Count; i++)
        //    {
        //        rdoButtonlist.Items[i].Selected = false;
        //        if (rdoButtonlist.Items[i].Value.ToString() == strTmp)
        //        {
        //            rdoButtonlist.Items[i].Selected = true;
        //            break;
        //        }
        //    }
        }
        /// <summary>
        /// ����ComboBoxѡ��Item��Text
        /// </summary>   
        /// <param name="cboBox">cboBox</param>
        public static string funComboBox_SelectedText(this HtmlSelect cboBox)
        {
            string strText = "";
            if (cboBox.Items.Count > 0)
            {
                strText = cboBox.Items[cboBox.SelectedIndex].Text;
            }
            return strText;
        }
        /// <summary>
        /// ����ComboBoxѡ��Item��Text
        /// </summary>   
        /// <param name="cboBox">cboBox</param>
        public static string funComboBox_SelectedText(this DropDownList cboBox)
        {
            string strText = "";
            if (cboBox.Items.Count > 0)
            {
                strText = cboBox.Items[cboBox.SelectedIndex].Text;
            }
            return strText;
        }
        /// <summary>
        /// ����ComboBoxѡ��Item��Value
        /// </summary>   
        /// <param name="cboBox">cboBox</param>
        public static string funComboBox_SelectedValue(this HtmlSelect cboBox)
        {
            string strText = "";
            if (cboBox.Items.Count > 0)
            {
                strText = cboBox.Items[cboBox.SelectedIndex].Value;
            }
            return strText;
        }
        /// <summary>
        /// ����ComboBoxѡ��Item��Value
        /// </summary>   
        /// <param name="cboBox">cboBox</param>
        public static string funComboBox_SelectedValue(this DropDownList cboBox)
        {
            string strText = "";
            if (cboBox.Items.Count > 0)
            {
                strText = cboBox.Items[cboBox.SelectedIndex].Value;
            }
            return strText;
        }
        /// <summary>
        /// ����ComboBox��Item��,�����������1,��0��ΪValue,1��ΪText
        /// </summary>   
        /// <param name="cboBox">cboBox</param>
        /// <param name="strSearchSQL">������SQL</param>
        /// <param name="intSelectIndex">Ĭ��ѡ�е�Index</param>
        /// <param name="ItemDefault">���Ϊ��(NULL)����ָ��Ĭ��Item�����ItemDefaultΪĬ����</param>
        public static void subComboBox_LoadItems(this HtmlSelect cboBox, string strSearchSQL, int intSelectIndex, ListItem ItemDefault)
        {
            cboBox.Items.Clear();
            ListItem item;
            string strSQL;
            strSQL = strSearchSQL;
            IdioSoft.ClassDbAccess.ClassDbAccess objclassDbAccess = new IdioSoft.ClassDbAccess.ClassDbAccess();
            DataSet ds = new DataSet();
            ds = objclassDbAccess.funDataset_SQLExecuteNonQuery(strSearchSQL);
            if (ItemDefault != null)
            {
                cboBox.Items.Add(ItemDefault);
            }
            int intCount = -1;
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                intCount = ds.Tables[0].Rows.Count;
            }

            int intColumnsCount = 0;
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                intColumnsCount = ds.Tables[0].Columns.Count;
            }
            for (int i = 0; i < intCount; i++)
            {
                item = new ListItem();
                if (intColumnsCount > 1)
                {
                    item.Text = ds.Tables[0].Rows[i][1].ToString();
                }
                else
                {
                    item.Text = ds.Tables[0].Rows[i][0].ToString();
                }
                item.Value = ds.Tables[0].Rows[i][0].ToString();
                cboBox.Items.Add(item);
            }
            if (cboBox.Items.Count > 0)
            {
                cboBox.SelectedIndex = intSelectIndex;
            }
            else
            {
                cboBox.SelectedIndex = -1;
            }
        }
        /// <summary>
        /// ����ComboBox��Item��,�����������1,��0��ΪValue,1��ΪText
        /// </summary>   
        /// <param name="cboBox">cboBox</param>
        /// <param name="strSearchSQL">������SQL</param>
        /// <param name="intSelectIndex">Ĭ��ѡ�е�Index</param>
        /// <param name="ItemDefault">���Ϊ��(NULL)����ָ��Ĭ��Item�����ItemDefaultΪĬ����</param>
        public static void subComboBox_LoadItems(this DropDownList cboBox, string strSearchSQL, int intSelectIndex, ListItem ItemDefault)
        {
            cboBox.Items.Clear();
            ListItem item;
            string strSQL;
            strSQL = strSearchSQL;
            IdioSoft.ClassDbAccess.ClassDbAccess objclassDbAccess = new IdioSoft.ClassDbAccess.ClassDbAccess();
            DataSet ds = new DataSet();
            ds = objclassDbAccess.funDataset_SQLExecuteNonQuery(strSearchSQL);
            if (ds == null)
            {
                return;
            }
            int intCount = -1;
            intCount = ds.Tables[0].Rows.Count;
            if (ItemDefault != null)
            {
                cboBox.Items.Add(ItemDefault);
            }
            int intColumnsCount = ds.Tables[0].Columns.Count;
            for (int i = 0; i < intCount; i++)
            {
                item = new ListItem();
                if (intColumnsCount > 1)
                {
                    item.Text = ds.Tables[0].Rows[i][1].ToString().funString_StringToSQL().funString_RemoveHTML();
                    item.Attributes.Add("title", ds.Tables[0].Rows[i][1].ToString().funString_StringToSQL().funString_RemoveHTML());
                }
                else
                {
                    item.Text = ds.Tables[0].Rows[i][0].ToString().funString_StringToSQL().funString_RemoveHTML();
                }

                item.Value = ds.Tables[0].Rows[i][0].ToString();
                cboBox.Items.Add(item);
            }
            if (cboBox.Items.Count > 0)
            {
                cboBox.SelectedIndex = intSelectIndex;
            }
            else
            {
                cboBox.SelectedIndex = -1;
            }
        }
        /// <summary>
        /// ����ѡ�е�RadionButtonList��ֵ
        /// </summary>
        /// <param name="rdoButtonlist"></param>
        /// <returns></returns>
        public static string funString_RadioButtonList(this RadioButtonList rdoButtonlist)
        {
            string strCheck = "";
            bool blnNull = false;
            for (int i = 0; i < rdoButtonlist.Items.Count; i++)
            {
                if (rdoButtonlist.Items[i].Selected == true)
                {
                    if (rdoButtonlist.Items[i].Value == "1")
                    {
                        strCheck = "1";
                        break;
                    }
                    else
                    {
                        strCheck = "0";
                    }
                }
            }
            if (rdoButtonlist.SelectedIndex < 0)
            {
                strCheck = "null";
            }
            return strCheck;
        }
        /// <summary>
        /// ����CheckBox״̬,��0,1����,0Ϊûѡ��,1Ϊѡ��
        /// </summary>
        /// <param name="chkBox"></param>
        /// <returns></returns>
        public static int funInt_CheckBoxStatus(this HtmlInputCheckBox chkBox)
        {
            if (chkBox.Checked)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// ����CheckBox״̬,��0,1����,0Ϊûѡ��,1Ϊѡ��
        /// </summary>
        /// <param name="chkBox"></param>
        /// <returns></returns>
        public static int funInt_CheckBoxStatus(this CheckBox chkBox)
        {
            if (chkBox.Checked)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        #endregion

        #region"����Email"
        /// <summary>
        /// ��webMail��ʽ����Mail,�ɹ�����"",ʧ�ܷ��ش������
        /// </summary>
        /// <param name="strSendMail">�����˵�Mail</param>
        /// <param name="strSendUserName">�����˵��û���</param>
        /// <param name="strSendUserPassword">�����˵�����</param>
        /// <param name="strBody">��������</param>
        /// <param name="strSmtp">Smtp Server</param>
        /// <param name="strToMail">�����˵�Mail</param>
        /// <param name="strSubject">��������</param>
        /// <param name="strCc">�����˵�Mail</param>
        /// <param name="isDellAttachment">���ͺ��Ƿ�ɾ������</param>
        /// <param name="strBcc">�����˵�Mail</param>
        /// <param name="aryAttachment">�����б�</param>
        /// <returns></returns>
        public static string funString_SendMailByWebMail(this string strSendMail, string strSendUserName, string strSendUserPassword, string strBody, string strSmtp, string strToMail, string strSubject, string strCc, bool isDellAttachment, string strBcc, ArrayList aryAttachment)
        {
            string strReturn = "";
            System.Web.Mail.MailMessage objMailMessage = new System.Web.Mail.MailMessage();
            objMailMessage.From = strSendMail;
            objMailMessage.To = strToMail;
            objMailMessage.BodyEncoding = Encoding.GetEncoding("GB2312");
            //Cc
            if (strCc != "")
            {
                objMailMessage.Cc = strCc;
            }
            //Bcc
            if (strBcc != "")
            {
                objMailMessage.Bcc = strBcc;
            }
            objMailMessage.Subject = strSubject;
            objMailMessage.Body = strBody;
            objMailMessage.BodyFormat = System.Web.Mail.MailFormat.Html;
            System.Web.Mail.SmtpMail.SmtpServer = strSmtp;
            //����
            if (aryAttachment != null)
            {
                if (aryAttachment.Count > 0)
                {
                    FileInfo objFile;
                    for (int i = 0; i < aryAttachment.Count; i++)
                    {
                        objFile = new FileInfo(aryAttachment[i].ToString());
                        if (objFile.Exists)
                        {
                            System.Web.Mail.MailAttachment objAttachment = new System.Web.Mail.MailAttachment(aryAttachment[i].ToString());
                            objMailMessage.Attachments.Add(objAttachment);
                        }
                    }
                }
            }
            //��֤
            objMailMessage.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1"); //basic authentication 
            objMailMessage.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", strSendUserName); //set your username here 
            objMailMessage.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", strSendUserPassword); //set your password here 
            //����
            try
            {
                System.Web.Mail.SmtpMail.Send(objMailMessage);
            }
            catch (Exception ex)
            {
                strReturn = ex.Source + "\n" + ex.Message;
            }
            //ɾ���ļ�
            try
            {
                //ɾ���ļ�
                if (isDellAttachment)
                {
                    for (int i = 0; i < aryAttachment.Count; i++)
                    {
                        File.Delete(aryAttachment[i].ToString());
                    }
                }
                return "";
            }
            catch
            {
            }
            return strReturn;
        }
        /// <summary>
        /// ��NetMail��ʽ����Mail,�ɹ�����"",ʧ�ܷ��ش������
        /// </summary>
        /// <param name="strSendMail">�����˵�Mail</param>
        /// <param name="strSendUserName">�����˵��û���</param>
        /// <param name="strSendUserPassword">�����˵�����</param>
        /// <param name="strBody">��������</param>
        /// <param name="strSmtp">Smtp Server</param>
        /// <param name="strToMail">�����˵�Mail</param>
        /// <param name="strSubject">��������</param>
        /// <param name="strCc">�����˵�Mail</param>
        /// <param name="isDellAttachment">���ͺ��Ƿ�ɾ������</param>
        /// <param name="strBcc">�����˵�Mail</param>
        /// <param name="aryAttachment">�����б�</param>
        /// <returns></returns>
        public static string funString_SendMailByNetMail(this string strSendMail, string strSendUserName, string strSendUserPassword, string strBody, string strSmtp, string strToMail, string strSubject, string strCc, bool isDellAttachment, string strBcc, ArrayList aryAttachment)
        {
            string strReturn = "";
            System.Net.Mail.MailMessage objMailMessage = new System.Net.Mail.MailMessage();
            //�����ż��Ļ�����Ϣ
            objMailMessage.Subject = strSubject;
            objMailMessage.Body = strBody;
            objMailMessage.IsBodyHtml = true;
            objMailMessage.BodyEncoding = Encoding.GetEncoding("GB2312");
            //���÷�����
            System.Net.Mail.MailAddress objMailAddress = new System.Net.Mail.MailAddress(strSendMail);
            objMailMessage.From = objMailAddress;

            //�����ռ���
            string[] aryToMail = strToMail.Split(';');
            for (int i = 0; i < aryToMail.Length; i++)
            {
                objMailMessage.To.Add(aryToMail[i].ToString());
            }
            //����CC
            if (strCc != "")
            {
                string[] aryCCMail = strCc.Split(';');
                for (int i = 0; i < aryCCMail.Length; i++)
                {
                    objMailMessage.To.Add(aryCCMail[i].ToString());
                }
            }
            //����BCC
            if (strBcc != "")
            {
                string[] aryBCCMail = strCc.Split(';');
                for (int i = 0; i < aryBCCMail.Length; i++)
                {
                    objMailMessage.Bcc.Add(aryBCCMail[i].ToString());
                }
            }
            //����
            if (aryAttachment != null)
            {
                if (aryAttachment.Count > 0)
                {
                    FileInfo objFile;
                    for (int i = 0; i < aryAttachment.Count; i++)
                    {
                        objFile = new FileInfo(aryAttachment[i].ToString());
                        if (objFile.Exists)
                        {
                            System.Net.Mail.Attachment objAttachment = new System.Net.Mail.Attachment(aryAttachment[i].ToString());
                            objMailMessage.Attachments.Add(objAttachment);
                        }
                    }
                }
            }
            //����
            try
            {

                System.Net.Mail.SmtpClient objSmtpClient = new System.Net.Mail.SmtpClient();
                objSmtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                System.Net.CredentialCache objCache = new System.Net.CredentialCache();
                objCache.Add(strSmtp, 25, "NTLM", new System.Net.NetworkCredential(strSendUserName, strSendUserPassword));//NTLM//Basic
                objSmtpClient.Credentials = objCache;
                objSmtpClient.Send(objMailMessage);
            }
            catch (Exception ex)
            {
                strReturn = ex.Source + "\n" + ex.Message;
            }
            try
            {
                //ɾ���ļ�
                if (isDellAttachment)
                {
                    for (int i = 0; i < aryAttachment.Count; i++)
                    {
                        File.Delete(aryAttachment[i].ToString());
                    }
                }
            }
            catch
            {
            }
            return strReturn;
        }
        #endregion

        #region"�жϽ�������ʱ�䣬�ҳ���Ӧ�Ľ��̲�ɱ������"
        /// <summary>
        /// �жϽ�������ʱ�䣬�ҳ���Ӧ�Ľ��̲�ɱ������
        /// </summary>   
        /// <param name="dtStartDate">�������ÿ�ʼʱ��</param>
        /// <param name="dtEndDate">�������ý���ʱ��</param>
        /// <param name="strProcessName">ɱ���Ľ�����</param>
        public static void subProcess_Kill(DateTime dtStartDate, DateTime dtEndDate, string strProcessName)
        {
            Process[] myProcesses;
            DateTime startTime;
            myProcesses = Process.GetProcessesByName(strProcessName);
            foreach (Process myProcess in myProcesses)
            {
                try
                {
                    startTime = myProcess.StartTime;
                    if (startTime > dtStartDate && startTime < dtEndDate)
                    {
                        try
                        {
                            myProcess.Kill();
                        }
                        catch
                        {
                        }
                    }
                }
                catch
                {
                }
            }
        }
        #endregion

        #region "��ȡһ��word��XMLģ���ļ�"
        /// <summary>
        /// ��ȡһ��word��XMLģ���ļ�
        /// </summary>   
        /// <param name="strTemplateAbsolutePath">ģ���������ַ</param>
        public static StringBuilder funStringBuilder_WordXMLToStringBuilder(this string strTemplateAbsolutePath)
        {
            StringBuilder strBWord = new StringBuilder();
            FileStream fsWord = new FileStream(strTemplateAbsolutePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            StreamReader sRword = new StreamReader(fsWord, Encoding.UTF8);
            strBWord.Append(sRword.ReadToEnd());
            fsWord.Close();
            return strBWord;
        }
        #endregion

        #region "����->UTF8 CODE"
        /// <summary>
        /// ����->UTF8 CODE
        /// </summary>   
        /// <param name="strContent">�ַ���</param>
        public static string funString_ChinesetoUTF8Code(this string strContent)
        {
            string strTemp = "";
            if (strContent == "")
            {
                return strTemp;
            }
            for (int i = 0; i < strContent.ToString().Length; i++)
            {
                strTemp = strTemp + "&#" + Microsoft.VisualBasic.Strings.AscW(strContent.ToString().Substring(i, 1));
            }
            return strTemp;
        }
        #endregion

        #region "�����ؼ�FindControl������չ"
        /// <summary>
        /// �õݹ�����
        /// </summary>
        /// <param name="o">һ����Ĵ�ֵ</param>
        /// <param name="id"></param>
        /// <param name="controls"></param>
        /// <returns></returns>
        public static System.Web.UI.Control FindControlEx(this System.Web.UI.Control o, string id, ControlCollection controls)
        {
            int i;
            System.Web.UI.Control reControl = null;
            for (i = 0; i < controls.Count; i++)
            {
                if (controls[i].ID == id)
                {
                    reControl = controls[i];
                    break;
                }
                if (controls[i].Controls.Count > 0)
                {
                    reControl = FindControlEx(o, id, controls[i].Controls);
                    if (reControl != null)
                    {
                        break;
                    }
                }
            }

            return reControl;
        }
        #endregion

        #region "����������ͼƬ"
        /// <summary>
        /// ����������ͼƬ
        /// </summary>
        /// <param name="ImagePathFileName">ԴͼƬ�ļ����·�����ļ�����</param>
        /// <param name="TargetImagePath">Ŀ��ͼƬ�ļ����·��,��/</param>
        /// <param name="TargetImageFileName">Ŀ��ͼƬ�ļ�����</param>
        /// <param name="ScaleZoom">���ű���</param>
        /// <returns>False:û��ͼƬ���ųɹ���True:ͼƬ���ųɹ�</returns>
        public static string funString_ImageScaleZoomSave(this string ImagePathFileName, string TargetImagePath, string TargetImageFileName, double ScaleZoom)
        {
            string strImagePathFileName = System.Web.HttpContext.Current.Server.MapPath(ImagePathFileName);
            FileInfo fi = new FileInfo(strImagePathFileName);
            if (!fi.Exists)
            {
                return "";
            }
            string strTargetImagePathFileName = System.Web.HttpContext.Current.Server.MapPath(TargetImagePath + TargetImageFileName);
            int intWidth = 0;
            int intHeight = 0;
            Bitmap objBitMap = new Bitmap(1, 1);
            try
            {
                objBitMap = new Bitmap(strImagePathFileName);
                intWidth = objBitMap.Width;
                intHeight = objBitMap.Height;
            }
            catch
            {
                return "";
            }
            int thumWidth = 0;
            int thumHeight = 0;
            //���ź���ļ���С����
            thumWidth = Convert.ToInt16(intWidth * ScaleZoom);
            thumHeight = Convert.ToInt16(intHeight * ScaleZoom);
            Bitmap objTarget = new Bitmap(thumWidth, thumHeight);
            Graphics g = Graphics.FromImage(objTarget);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.Clear(System.Drawing.Color.Transparent);
            g.DrawImage(objBitMap, new System.Drawing.Rectangle(0, 0, thumWidth, thumHeight));
            try
            {
                if (File.Exists(strTargetImagePathFileName))
                {
                    File.Delete(strTargetImagePathFileName);
                }
                switch (fi.Extension)
                {
                    case ".gif":
                        objTarget.Save(strTargetImagePathFileName, System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                    case ".jpg":
                        objTarget.Save(strTargetImagePathFileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case ".jpeg":
                        objTarget.Save(strTargetImagePathFileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case ".png":
                        objTarget.Save(strTargetImagePathFileName, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    default:
                        TargetImageFileName = "";
                        break;
                }
            }
            catch
            {
                TargetImageFileName = "";
            }
            finally
            {
                objBitMap.Dispose();
                objTarget.Dispose();
                g.Dispose();
            }
            return TargetImageFileName;
        }
        /// <summary>
        /// ����������ͼƬ
        /// </summary>
        /// <param name="ImagePathFileName">ԴͼƬ�ļ����·�����ļ�����</param>
        /// <param name="TargetImagePath">Ŀ��ͼƬ�ļ����·��,��/</param>
        /// <param name="TargetImageFileName">Ŀ��ͼƬ�ļ�����</param>
        /// <param name="ScaleZoom">���ű���</param>
        /// <param name="FixWidth">�������ŵ�ͼƬ��</param>
        /// <param name="FixHeight">�������ŵ�ͼƬ��</param>
        /// <param name="IsTargetWidth">�Ծ��������еĿ���߸���Ϊ��׼��True:�Կ�Ϊ��׼��False:�Ը�Ϊ��׼��</param>
        /// <returns></returns>
        //public static string funString_ImageScaleZoom(this string ImagePath, double ScaleZoom, double FixWidth, double FixHeight)
        public static string funString_ImageScaleZoomSave(this string ImagePathFileName, string TargetImagePath, string TargetImageFileName, double ScaleZoom, double FixWidth, double FixHeight, bool IsTargetWidth)
        {
            string strImagePathFileName = System.Web.HttpContext.Current.Server.MapPath(ImagePathFileName);
            FileInfo fi = new FileInfo(strImagePathFileName);
            if (!fi.Exists)
            {
                return "";
            }
            string strTargetImagePathFileName = System.Web.HttpContext.Current.Server.MapPath(TargetImagePath + TargetImageFileName);
            int intWidth = 0;
            int intHeight = 0;
            Bitmap objBitMap = new Bitmap(1, 1);
            try
            {
                objBitMap = new Bitmap(strImagePathFileName);
                intWidth = objBitMap.Width;
                intHeight = objBitMap.Height;
            }
            catch
            {
                return "";
            }
            int thumWidth = 0;
            int thumHeight = 0;
            //���ź���ļ���С����
            if (FixWidth != 0 && FixHeight != 0)
            {
                thumWidth = intWidth;
                thumHeight = intHeight;
                #region "����ԭʼ�ߴ����ʹ�ÿ���Ǹ����ű���"
                if (intWidth > FixWidth && IsTargetWidth == true)//���ʹ�ÿ���Ϊ��������������ͼƬ�Ŀ��������ͼƬ�Ŀ�
                {
                    thumWidth = Convert.ToInt16(FixWidth);
                    thumHeight = Convert.ToInt16(intHeight * (Convert.ToDouble(FixWidth) / Convert.ToDouble(intWidth)));
                    //����õ��ĸ߶Ȼ�������׼�߶ȣ���ôֱ��ѹ���ɱ�׼�߶�(����������ͼƬ����)
                    if (thumHeight > Convert.ToInt16(FixHeight))
                    {
                        thumHeight = Convert.ToInt16(FixHeight);
                    }
                }
                if (intHeight > FixHeight && IsTargetWidth == false)//���ʹ�ø���Ϊ��������������ͼƬ�ĸߴ�������ͼƬ�ĸ�
                {
                    thumWidth = Convert.ToInt16(intWidth * (Convert.ToDouble(FixHeight) / Convert.ToDouble(intHeight)));
                    //����õ��Ŀ�Ȼ�������׼��ȣ���ôֱ��ѹ���ɱ�׼���(����������ͼƬ����)
                    if (thumWidth > Convert.ToInt16(FixWidth))
                    {
                        thumWidth = Convert.ToInt16(FixWidth);
                    }
                    thumHeight = Convert.ToInt16(FixHeight);
                }
                #endregion
            }
            else
            {
                thumWidth = Convert.ToInt16(intWidth * ScaleZoom);
                thumHeight = Convert.ToInt16(intHeight * ScaleZoom);
            }
            Bitmap objTarget = new Bitmap(thumWidth, thumHeight);
            Graphics g = Graphics.FromImage(objTarget);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.Clear(System.Drawing.Color.Transparent);
            g.DrawImage(objBitMap, new System.Drawing.Rectangle(0, 0, thumWidth, thumHeight));
            try
            {
                if (File.Exists(strTargetImagePathFileName))
                {
                    File.Delete(strTargetImagePathFileName);
                }
                switch (fi.Extension)
                {
                    case ".gif":
                        objTarget.Save(strTargetImagePathFileName, System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                    case ".jpg":
                        objTarget.Save(strTargetImagePathFileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case ".jpeg":
                        objTarget.Save(strTargetImagePathFileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case ".png":
                        objTarget.Save(strTargetImagePathFileName, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    case ".bmp":
                        objTarget.Save(strTargetImagePathFileName, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                    case ".ico":
                        objTarget.Save(strTargetImagePathFileName, System.Drawing.Imaging.ImageFormat.Icon);
                        break;
                    default:
                        TargetImageFileName = "";
                        break;
                }
            }
            catch
            {
                TargetImageFileName = "";
            }
            finally
            {
                objBitMap.Dispose();
                objTarget.Dispose();
                g.Dispose();
            }
            return TargetImageFileName;
        }

        /// <summary>
        /// ����ͼƬ�ж��Ƿ��������ȣ�������ڣ�ʹ�������
        /// </summary>
        /// <param name="ImagePathFileName">�ļ����·�����ļ�����</param>
        /// <param name="MaxImageWidth">ͼƬ���������</param>
        /// <returns></returns>
        public static int funInt_ImageMaxWidth(this string ImagePathFileName, int MaxImageWidth)
        {
            string strImagePathFileName = System.Web.HttpContext.Current.Server.MapPath(ImagePathFileName);
            int intWidth = 0;
            int intHeight = 0;
            Bitmap objBitMap = new Bitmap(1, 1);
            try
            {
                objBitMap = new Bitmap(strImagePathFileName);
                intWidth = objBitMap.Width;
                intHeight = objBitMap.Height;
            }
            catch
            {
                return 0;
            }
            if (intWidth > MaxImageWidth)
            {
                return MaxImageWidth;
            }
            else
            {
                return intWidth;
            }
        }
        #endregion

        #region "��������ĸ"
        /// <summary> 
        /// ��ָ�����ַ����б�CnStr�м�������ƴ�������ַ��� 
        /// </summary> 
        /// <param name="CnStr">�����ַ���</param> 
        /// <returns>���Ӧ�ĺ���ƴ������ĸ��</returns> 
        public static string funString_GetSpellCode(this string CnStr)
        {
            string strTemp = "";
            int iLen = CnStr.Length;
            int i = 0;

            for (i = 0; i <= iLen - 1; i++)
            {
                strTemp += CnStr.Substring(i, 1).funString_GetCharSpellCode();
            }

            return strTemp;
        }

        /// <summary> 
        /// �õ�һ�����ֵ�ƴ����һ����ĸ�������һ��Ӣ����ĸ��ֱ�ӷ��ش�д��ĸ 
        /// </summary> 
        /// <param name="CnChar">��������</param> 
        /// <returns>������д��ĸ</returns> 
        public static string funString_GetCharSpellCode(this string CnChar)
        {
            long iCnChar;

            byte[] ZW = System.Text.Encoding.Default.GetBytes(CnChar);

            //�������ĸ����ֱ�ӷ��� 
            if (ZW.Length == 1)
            {
                return CnChar.ToUpper();
            }
            else
            {
                // get the array of byte from the single char 
                int i1 = (short)(ZW[0]);
                int i2 = (short)(ZW[1]);
                iCnChar = i1 * 256 + i2;
            }

            //expresstion 
            //table of the constant list 
            // 'A'; //45217..45252 
            // 'B'; //45253..45760 
            // 'C'; //45761..46317 
            // 'D'; //46318..46825 
            // 'E'; //46826..47009 
            // 'F'; //47010..47296 
            // 'G'; //47297..47613 

            // 'H'; //47614..48118 
            // 'J'; //48119..49061 
            // 'K'; //49062..49323 
            // 'L'; //49324..49895 
            // 'M'; //49896..50370 
            // 'N'; //50371..50613 
            // 'O'; //50614..50621 
            // 'P'; //50622..50905 
            // 'Q'; //50906..51386 

            // 'R'; //51387..51445 
            // 'S'; //51446..52217 
            // 'T'; //52218..52697 
            //û��U,V 
            // 'W'; //52698..52979 
            // 'X'; //52980..53640 
            // 'Y'; //53689..54480 
            // 'Z'; //54481..55289 

            // iCnChar match the constant 
            if ((iCnChar >= 45217) && (iCnChar <= 45252))
            {
                return "A";
            }
            else if ((iCnChar >= 45253) && (iCnChar <= 45760))
            {
                return "B";
            }
            else if ((iCnChar >= 45761) && (iCnChar <= 46317))
            {
                return "C";
            }
            else if ((iCnChar >= 46318) && (iCnChar <= 46825))
            {
                return "D";
            }
            else if ((iCnChar >= 46826) && (iCnChar <= 47009))
            {
                return "E";
            }
            else if ((iCnChar >= 47010) && (iCnChar <= 47296))
            {
                return "F";
            }
            else if ((iCnChar >= 47297) && (iCnChar <= 47613))
            {
                return "G";
            }
            else if ((iCnChar >= 47614) && (iCnChar <= 48118))
            {
                return "H";
            }
            else if ((iCnChar >= 48119) && (iCnChar <= 49061))
            {
                return "J";
            }
            else if ((iCnChar >= 49062) && (iCnChar <= 49323))
            {
                return "K";
            }
            else if ((iCnChar >= 49324) && (iCnChar <= 49895))
            {
                return "L";
            }
            else if ((iCnChar >= 49896) && (iCnChar <= 50370))
            {
                return "M";
            }

            else if ((iCnChar >= 50371) && (iCnChar <= 50613))
            {
                return "N";
            }
            else if ((iCnChar >= 50614) && (iCnChar <= 50621))
            {
                return "O";
            }
            else if ((iCnChar >= 50622) && (iCnChar <= 50905))
            {
                return "P";
            }
            else if ((iCnChar >= 50906) && (iCnChar <= .51386))
            {
                return "Q";
            }
            else if ((iCnChar >= 51387) && (iCnChar <= 51445))
            {
                return "R";
            }
            else if ((iCnChar >= 51446) && (iCnChar <= 52217))
            {
                return "S";
            }
            else if ((iCnChar >= 52218) && (iCnChar <= 52697))
            {
                return "T";
            }
            else if ((iCnChar >= 52698) && (iCnChar <= 52979))
            {
                return "W";
            }
            else if ((iCnChar >= 52980) && (iCnChar <= 53640))
            {
                return "X";
            }
            else if ((iCnChar >= 53689) && (iCnChar <= 54480))
            {
                return "Y";
            }
            else if ((iCnChar >= 54481) && (iCnChar <= 55289))
            {
                return "Z";
            }
            else return ("?");
        }
        #endregion

        #region "��������"
        public static string funHTML_FilePlay(this string PathFileName, int intHeight, int intWidth)
        {
            string strHTML = "";

            #region "�����е��Ĳ��Ŷ���"
            //strHTML = "function insertaudio(a, query, action, type){document.write( '<object classid=\"clsid:d27cdb6e-ae6d-11cf-96b8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,0,0\" width=\"17\" height=\"17\" align=\"absmiddle\" onmouseover=\"javascript:ctlog(this, \'' + query + '\', 1, 1, 1, \'' + action + '\', \'' + type + '\')\" >' +'<param name=\"allowScriptAccess\" value=\"sameDomain\" />' +'<param name=\"movie\" value=\"../../UI/Images/voice.swf\" />' +'<param name=\"loop\" value=\"false\" />' +'<param name=\"menu\" value=\"false\" />' +'<param name=\"quality\" value=\"high\" />' +'<param name=\"wmode\"  value=\"transparent\">'+'<param name=\"FlashVars\" value=\"audio=' + a + '\">' +'<embed wmode=\"transparent\" src=\"../../UI/Images/voice.swf\" loop=\"false\" menu=\"false\" quality=\"high\" bgcolor=\"#ffffff\" width=\"17\" height=\"17\" align=\"absmiddle\" allowScriptAccess=\"sameDomain\" FlashVars=\"audio=' + a + '\" type=\"application/x-shockwave-flash\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" />' +'</object>');}";
            //strHTML = "function ctlog(url, q, pos, cfd, spt, action, ctype){var i=new Image();i.src=\"/ctlog?q=\"+q+\"&url=\"+encodeURIComponent(url.href)+\"&pos=\"+pos+\"&cfd=\"+cfd+\"&spt=\"+spt+\"&action=\"+action+\"&ctype=\"+ctype;return true;}";

            //strHTML = "<script language='javascript'>insertaudio('speech?audio=audio', 'audio', 'CLICK', 'dictcn_speech');</script>";
            //strHTML = strHTML + "<object classid='clsid:d27cdb6e-ae6d-11cf-96b8-444553540000' codebase='http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,0,0' onMouseOver='javascript:ctlog(this, 'audio', 1, 1, 1, 'CLICK', 'dictcn_speech')' align='absmiddle' height='17' width='17'>";
            //strHTML = strHTML + "<param name='allowScriptAccess' value='sameDomain'>";
            //strHTML = strHTML + "<param name='movie' value='voice.swf'>";
            //strHTML = strHTML + "<param name='loop' value='false'>";
            //strHTML = strHTML + "<param name='menu' value='false'>";
            //strHTML = strHTML + "<param name='quality' value='high'>";
            //strHTML = strHTML + "<param name='wmode' value='transparent'>";
            //strHTML = strHTML + "<param name='FlashVars' value='audio=speech?audio=audio'>";
            //strHTML = strHTML + "<embed wmode='transparent' src='search_files/voice.swf' loop='false' menu='false' quality='high' bgcolor='#ffffff' allowscriptaccess='sameDomain' flashvars='audio=speech?audio=audio' type='application/x-shockwave-flash' pluginspage='http://www.macromedia.com/go/getflashplayer' align='absmiddle' height='17' width='17'>";
            //strHTML = strHTML + "</object>";
            #endregion

            #region "���ϵĴ���"
            //http://topic.csdn.net/u/20080427/13/7894d5b7-02e7-447c-bc97-50b941ffca0d.html
            strHTML = "<object id='player' height='" + intHeight + "' width='" + intWidth + "' classid='CLSID:6BF52A52-394A-11d3-B153-00C04F79FAA6'> ";
            //<!--�Ƿ��Զ�����--> 
            strHTML = strHTML + "<param NAME='AutoStart' VALUE='-1'>";
            //<!--������������ƽ��,ͬ����ɲ���������--> 
            strHTML = strHTML + "<param NAME='Balance' VALUE='0'>";
            //<!--�������Ƿ����Ϊ����--> 
            strHTML = strHTML + "<param name='enabled' value='-1'>";
            //<!--�Ƿ����������Ĳ˵�--> 
            strHTML = strHTML + "<param NAME='EnableContextMenu' VALUE='-1'>";
            //<!--���ŵ��ļ���ַ--> 
            //strHTML = strHTML + "<param NAME='url' value='/blog/1.wma'>";
            strHTML = strHTML + "<param NAME='url' value='" + PathFileName + "'>";
            //<!--���Ŵ�������,Ϊ����--> 
            strHTML = strHTML + "<param NAME='PlayCount' VALUE='1'>";
            //<!--�������ʿ���,1Ϊ����,����С��,1.0-2.0--> 
            strHTML = strHTML + "<param name='rate' value='1'>";
            //<!--�ؼ�����:��ǰλ��--> 
            strHTML = strHTML + "<param name='currentPosition' value='0'>";
            //<!--�ؼ�����:��ǰ���--> 
            strHTML = strHTML + "<param name='currentMarker' value='0'>";
            //<!--��ʾĬ�Ͽ��--> 
            strHTML = strHTML + "<param name='defaultFrame' value=''>";
            //<!--�ű���������:�Ƿ����URL--> 
            strHTML = strHTML + "<param name='invokeURLs' value='0'>";
            //<!--�ű���������:�����õ�URL--> 
            strHTML = strHTML + "<param name='baseURL' value=''>";
            //<!--�Ƿ񰴱�����չ--> 
            strHTML = strHTML + "<param name='stretchToFit' value='0'>";
            //<!--Ĭ��������С0%-100%,50��Ϊ50%--> 
            strHTML = strHTML + "<param name='volume' value='50'>";
            //<!--�Ƿ���--> 
            strHTML = strHTML + "<param name='mute' value='0'>";
            //<!--��������ʾģʽ:Full��ʾȫ��;mini���;None����ʾ���ſ���,ֻ��ʾ��Ƶ����;invisibleȫ������ʾ--> 
            strHTML = strHTML + "<param name='uiMode' value='mini'>";
            //<!--�����0��������ȫ��,����ֻ���ڴ����в鿴--> 
            strHTML = strHTML + "<param name='windowlessVideo' value='0'>";
            //<!--��ʼ�����Ƿ��Զ�ȫ��--> 
            strHTML = strHTML + "<param name='fullScreen' value='0'>";
            //<!--�Ƿ����ô�����ʾ����--> 
            strHTML = strHTML + "<param name='enableErrorDialogs' value='-1'>";
            //<!--SAMI��ʽ--> 
            strHTML = strHTML + "<param name='SAMIStyle' value>";
            //<!--SAMI����--> 
            strHTML = strHTML + "<param name='SAMILang' value>";
            //<!--��ĻID--> 
            strHTML = strHTML + "<param name='SAMIFilename' value>";
            strHTML = strHTML + "</object>";
            #endregion

            return strHTML;
        }
        #endregion

        #region "�ݹ�ؼ��еĿؼ�"
        private static System.Web.UI.Control _objFindContorl;
        /// <summary>
        /// �ݹ�ؼ��еĿؼ�
        /// </summary>
        /// <param name="objControl"></param>
        /// <param name="strControlName"></param>
        /// <returns></returns>
        public static System.Web.UI.Control funControl_FindEx(System.Web.UI.Control objControl, string strControlName)
        {
            System.Web.UI.Control objCon = null;

            for (int i = 0; i < objControl.Controls.Count; i++)
            {
                objCon = objControl.Controls[i];
                if (objCon.ID == strControlName)
                {
                    _objFindContorl = objCon;
                    break;
                }
                if (objCon.Controls.Count > 0 && _objFindContorl == null)
                {
                    funControl_FindEx(objCon, strControlName);
                }
            }
            return _objFindContorl;
        }
        #endregion

        #region "��ȡһ��HTML�ļ�"
        public static string funString_GetHtmlFile(this string strFullFileName)
        {
            string strText = "";
            FileStream fs = new FileStream(strFullFileName,FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            StreamReader sr = new StreamReader(fs,System.Text.Encoding.GetEncoding("gb2312"));
            strText = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            return strText;
        }
        #endregion
        #region "��Js������Ϊ������HTML"
        /// <summary>
        /// ��Js������Ϊ������HTML
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static  string funString_JsToHtml(this string value)
        {
            value = value.Replace("<", "&lt;");
            value = value.Replace(">", "&gt;");
            value = value.Replace("&", "&amp;");
            value = value.Replace("\"", "&quot;");
            value = value.Replace("\r", "<BR>");
            value = value.Replace("\n", "<BR>");
            value = value.Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp");
            return value;
        }

        /// <summary>
        /// ��Js������Ϊ������HTML
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string funString_HtmlToJs(this string value)
        {
            value = value.Replace("&lt;", "<");
            value = value.Replace("&gt;", ">");
            value = value.Replace("&amp;", "&");
            value = value.Replace("&quot;", "\"");
            value = value.Replace("<BR>", "\r");
            value = value.Replace("<BR>", "\n");
            value = value.Replace("&nbsp;&nbsp;&nbsp;&nbsp", "\t");
            return value;
        }
        #endregion

        #region "��ȥHTML"
        public static string funString_RemoveHTML(this string strHtml)
        {
            string strOutput = strHtml;
            Regex regex = new Regex(@"<[^>]+>|</[^>]+>");
            strOutput = regex.Replace(strOutput, "");
            return strOutput;
        }
        #endregion

        /// <summary>
        /// ��������Email
        /// </summary>
        /// <param name="strSendMail"></param>
        /// <param name="strSendUserName"></param>
        /// <param name="strSendUserPassword"></param>
        /// <param name="strBody"></param>
        /// <param name="strSmtp"></param>
        /// <param name="strToMail"></param>
        /// <param name="strSubject"></param>
        /// <param name="strCc"></param>
        /// <param name="isDellAttachment"></param>
        /// <param name="strBcc"></param>
        /// <param name="aryAttachment"></param>
        /// <returns></returns>
        public static string funString_SendNoMailByWebMail(this string strSendMail, string strSendUserName, string strSendUserPassword, string strBody, string strSmtp, string strToMail, string strSubject, string strCc, bool isDellAttachment, string strBcc, List<string> aryAttachment)
        {
            System.Web.Mail.MailMessage myMail = new MailMessage();
            myMail.From = strSendMail;
            myMail.To = strToMail;
            myMail.Cc = strCc;
            myMail.Bcc = strBcc;
            myMail.Subject = strSubject;
            myMail.BodyEncoding = Encoding.GetEncoding("GB2312");
            myMail.Priority = MailPriority.High;
            myMail.BodyFormat = MailFormat.Html;
            myMail.Body = strBody;
            string strMessage = "";
            //����
            if (aryAttachment != null)
            {
                if (aryAttachment.Count > 0)
                {
                    FileInfo objFile;
                    for (int i = 0; i < aryAttachment.Count; i++)
                    {
                        if (aryAttachment[i].ToString() == "")
                        {
                            continue;
                        }
                        try
                        {
                            objFile = new FileInfo(aryAttachment[i].ToString());
                            if (objFile.Exists)
                            {
                                System.Web.Mail.MailAttachment objAttachment = new MailAttachment(aryAttachment[i].ToString());
                                myMail.Attachments.Add(objAttachment);
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
            SmtpMail.SmtpServer = "mailrelay.cummins.com"; //your smtp server here
            try
            {
                SmtpMail.Send(myMail);
            }
            catch (Exception ex)
            {
                strMessage = ex.Source + "\n" + ex.Message;
            }
            return strMessage;
        }

    }
}