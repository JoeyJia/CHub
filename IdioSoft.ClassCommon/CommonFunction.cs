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
    /// 公用操作类
    /// </summary>
    public static class CommonFunction
    {
        #region "数据转换函数"
        /// <summary>
        /// 将格式Js中Alert显示的字符串
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static string funString_JsToString(this string strValue)
        {

            string strTmp;
            strTmp = strValue;

            if ((strTmp != string.Empty) && (strTmp != ""))
            {
                strTmp = strTmp.Replace("'", "’");
                strTmp = strTmp.Replace("\"", "＂");
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
        /// 将字符串格式化为存入SQL的字符串，被替换字符：'-->’;%-->％,小于号和大于号
        /// </summary>   
        /// <param name="InputValue">字符串</param>
        public static string funString_SQLToString(this string InputValue)
        {
            string strTmp;
            strTmp = InputValue;
            if ((strTmp != string.Empty) && (strTmp != ""))
            {
                strTmp = strTmp.Replace("'", "’");
                strTmp = strTmp.Replace("%", "％");
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
        /// 将字符串格式化为存入SQL的字符串，被替换字符：'-->&#39;
        /// </summary>   
        /// <param name="InputValue">字符串</param>
        public static string funString_SQLToStringDYH(this string InputValue)
        {
            string strTmp;
            strTmp = InputValue;
            if ((strTmp != string.Empty) && (strTmp != ""))
            {
                strTmp = strTmp.Replace("'", "’");
            }
            else
            {
                strTmp = "";
            }
            return strTmp;
        }
        /// <summary>
        /// 将字符串格式化为存入SQL的字符串，被替换字符：'-->’;%-->％,小于号和大于号
        /// </summary>
        /// <param name="InputValue">字符串</param>
        /// <param name="DefaultText">如果字符串为Null或者''，那么使用默认值</param>
        /// <returns></returns>
        public static string funString_SQLToString(this string InputValue, string DefaultText)
        {
            string strTmp;
            strTmp = InputValue;
            if ((strTmp != string.Empty) && (strTmp != ""))
            {
                strTmp = strTmp.Replace("'", "’");
                strTmp = strTmp.Replace("%", "％");
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
        /// 将字符串格式化为存入SQL的字符串，被替换字符：'-->’;%-->％,小于号和大于号
        /// </summary>   
        /// <param name="InputValue">字符串</param>
        /// <param name="MaxLen">最大长度</param>
        public static string funString_SQLToString(this string InputValue, int MaxLen)
        {
            string strTmp;
            strTmp = InputValue;
            if ((strTmp != string.Empty) && (strTmp != ""))
            {
                strTmp = strTmp.Replace("'", "’");
                strTmp = strTmp.Replace("%", "％");
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
        /// 将字符串格式化为存入SQL的字符串，被替换字符：'-->’;%-->％,小于号和大于号
        /// </summary>   
        /// <param name="InputValue">字符串</param>
        /// <param name="DefaultText">如果字符串为Null或者''，那么使用默认值，默认值的长度也受MaxLen的限制</param>
        /// <param name="MaxLen">最大长度</param>
        public static string funString_SQLToString(this string InputValue, string DefaultText, int MaxLen)
        {
            string strTmp;
            strTmp = InputValue;
            if ((strTmp != string.Empty) && (strTmp != ""))
            {
                strTmp = strTmp.Replace("'", "’");
                strTmp = strTmp.Replace("%", "％");
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
        /// 将被格式化过的SQL的字符串转换为正常SQL语句，被替换字符：’-->';％-->%,小于号和大于号
        /// </summary>   
        /// <param name="strValue">字符串</param>
        public static string funString_StringToSQL(this string strValue)
        {
            string strTmp;
            strTmp = strValue;
            if ((strTmp != string.Empty) && (strTmp != ""))
            {
                strTmp = strTmp.Replace("’", "'");
                strTmp = strTmp.Replace("％", "%");
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
        /// 格式化做URL跳转的字符串到可做URL传值的字符串，被替换字符：?-->？;&-->＆;=-->＝;/-->／
        /// </summary>   
        /// <param name="strValue">字符串</param>
        public static string funString_RequestToString(this string strValue)
        {
            string strTmp;
            strTmp = strValue;
            if ((strTmp != string.Empty) && (strTmp != ""))
            {
                strTmp = strTmp.Replace("?", "？");
                strTmp = strTmp.Replace("&", "＆");
                strTmp = strTmp.Replace("=", "＝");
                strTmp = strTmp.Replace("/", "／");
            }
            else
            {
                strTmp = "";
            }
            return strTmp;
        }

        /// <summary>   
        /// 将被格式化过的URL的字符串转换为正常URL语句，被替换字符：？-->?;＆-->&;＝-->=;／-->/
        /// </summary>   
        /// <param name="strValue">字符串</param>
        public static string funString_StringToRequest(this string strValue)
        {
            string strTmp;
            strTmp = strValue;
            if ((strTmp != string.Empty) && (strTmp != ""))
            {
                strTmp = strTmp.Replace("？", "?");
                strTmp = strTmp.Replace("＆", "&");
                strTmp = strTmp.Replace("＝", "=");
                strTmp = strTmp.Replace("／", "/");
            }
            else
            {
                strTmp = "";
            }
            return strTmp;
        }

        /// <summary>
        /// 将字符串格式化成XML,主要是将特殊字符转换为XML可用字符，被替换字符：&lt;-->＆lt;;&gt;-->＆gt;;&-->＆amp;;'-->＆apos;;\-->＆quot;
        /// </summary>
        /// <param name="strContent">字符串</param>
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
        /// 这个过过程是为了防止导出数据中有","为分割
        /// 导出CSV格式的Excel时，如是有","就会有问题了
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
                strContent = strContent.Replace(",", "，");
            }
            if (strContent != "")
            {
                strContent = strContent.Replace("\"", "");
            }
            return strContent;
        }

        /// <summary>
        /// 将出来的文字中的换行符替换成＜br＞
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
        /// 数字月转换成英文缩写月如Jan.,Feb.等
        /// </summary>   
        /// <param name="intMonth">传入1到12个数字</param>
        public static string funString_NumMonthToEnglishAbb(this int intMonth)
        {
            string[] aryMonthAbb = { "Jan.", "Feb.", "Mar.", "Apr.", "May", "Jun.", "Jul.", "Aug.", "Sep.", "Oct.", "Nov.", "Dec." };
            return aryMonthAbb[intMonth - 1];
        }

        /// <summary>
        /// 将字符转换为Ascw格式
        /// </summary>
        /// <param name="strContent"></param>
        /// <returns></returns>
        public static string funString_AscW(this string strContent)
        {
            return Microsoft.VisualBasic.Strings.AscW(strContent).ToString();
        }
        /// <summary>
        /// 将字符转成日期,如果不为日期就转为1900-01-01
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
        /// 将字符串固定位置的字符替换成需要的字符
        /// </summary>
        /// <param name="strValue">旧有的字符串</param>
        /// <param name="StartIndex">开始替换的位置索引，从0开始</param>
        /// <param name="StuffLength">需要替换掉的字符的长度，设置为0，即为追加字符串</param>
        /// <param name="StuffValue">用来替换的字符值</param>
        /// <param name="IsForceStuff">如果长度不够的情况下，是否强制替换。True：强制替换；False：取消替换</param>
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
                #region "长度够"
                strTemp = strTemp.Substring(0, StartIndex) + StuffValue + strTemp.Substring(StartIndex + StuffLength);
                #endregion
            }
            else
            {
                #region "长度不够"
                if (IsForceStuff)
                {
                    //允许强制转换，那么字符串会被加长
                    if (intLength <= StartIndex)
                    {
                        //总长度都不够
                        strTemp = strTemp + StuffValue;
                    }
                    else
                    {
                        //开始替换的够，但是总长度不够
                        strTemp = strTemp.Substring(0, StartIndex) + StuffValue;
                    }
                }
                #endregion
            }

            return strTemp;
        }
        #endregion

        #region "日期操作"
        /// <summary>   
        /// 得到开始时间到结束时间中间隔的时间数
        /// </summary>   
        /// <param name="strInterval">年份yyyy年,q季,m月,y一年的日数,d日,w一周的日数,ww周,h时,n分钟,s秒</param>
        /// <param name="dtStartDate">开始日期</param>
        /// <param name="dtEndDate">结束日期</param>
        public static int funInt_DateInterval(this string strInterval, DateTime dtStartDate, DateTime dtEndDate)
        {
            return (int)Microsoft.VisualBasic.DateAndTime.DateDiff(strInterval, dtStartDate, dtEndDate, 0, 0);
        }
        /// <summary>   
        /// 得到日期是周几,返回数字,例如:0为星期日.
        /// </summary>   
        /// <param name="dtCurrentDate">日期</param>
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
        ///// 得到日期是本年的第几个星期
        ///// </summary>
        ///// <param name="dtCurrentDate"></param>
        ///// <returns></returns>
        //public static int funInt_WeekNumber(this DateTime dtCurrentDate)
        //{
        //    //取日期的方法很多（我这里是用控件）就不啰嗦了。
        //    string firstDateText = DateTime.Now.Year.ToString() + "年1月1日";
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

        #region"验证函数"
        /// <summary>   
        /// 验证是否是闰年,如果是返回true,否则false
        /// </summary>   
        /// <param name="intYear">年份</param>
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
        /// 验证是否是正确格式电话号码,如果是返回true,否则false
        /// </summary>   
        /// <param name="strTel">电话号码</param>
        public static Boolean funBoolean_ValidTel(this string strTel)
        {
            return Regex.IsMatch(strTel, @"(\d+-)?(\d{4}-?\d{7}|\d{3}-?\d{8}|^\d{7,8})(-\d+)?");
        }
        /// <summary>   
        /// 验证是否是正确格式手机号码,如果是返回true,否则false
        /// </summary>   
        /// <param name="strMobile">手机号码</param>
        public static Boolean funBoolean_ValidMobile(this string strMobile)
        {
            return Regex.IsMatch(strMobile, @"\d{11}");
        }
        /// <summary>   
        /// 验证是否是正确格式邮政编码,如果是返回true,否则false
        /// </summary>   
        /// <param name="strPostCode">邮政编码</param>
        public static Boolean funBoolean_ValidPostcode(this string strPostCode)
        {
            return Regex.IsMatch(strPostCode, @"\d{6}");
        }
        /// <summary>   
        /// 验证是否是正确格式Email,如果是返回true,否则false
        /// </summary>   
        /// <param name="strEmail">Email</param>
        public static bool funBoolean_ValidEmail(this string strEmail)
        {
            return Regex.IsMatch(strEmail, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
        /// <summary>   
        /// 验证是否是正确格式日期时间,如果是返回true,否则false
        /// </summary>   
        /// <param name="strDate">日期格式</param>
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
        /// 验证是否是正确格式数字,如果是返回true,否则false
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
        /// 验证身份证号码，包括18位和15位
        /// </summary>
        /// <param name="strIDNo">身份证字符串</param>
        /// <returns></returns>
        public static bool funBoolean_ValidIDNo(this string strIDNo)
        {
            string[] aCity = new string[] { null, null, null, null, null, null, null, null, null, null, null, "北京", "天津", "河北", "山西", "内蒙古", null, null, null, null, null, "辽宁", "吉林", "黑龙江", null, null, null, null, null, null, null, "上海", "江苏", "浙江", "安微", "福建", "江西", "山东", null, null, null, "河南", "湖北", "湖南", "广东", "广西", "海南", null, null, null, "重庆", "四川", "贵州", "云南", "西藏", null, null, null, null, null, null, "陕西", "甘肃", "青海", "宁夏", "新疆", null, null, null, null, null, "台湾", null, null, null, null, null, null, null, null, null, "香港", "澳门", null, null, null, null, null, null, null, null, "国外" };
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
            //如果是15位身份证，则转换成18位身份证校验
            strtmpIDNo = strIDNo.ToLower();
            if (strtmpIDNo.Length == 15)
            {
                strtmpIDNo = ConvertIDNoLength15To18(strtmpIDNo);
            }
            strtmpIDNo = strtmpIDNo.ToLower();
            strtmpIDNo = strtmpIDNo.Replace("x", "a");
            if (aCity[int.Parse(strtmpIDNo.Substring(0, 2))] == null)
            {
                return false;//非法地区
            }
            try
            {
                DateTime.Parse(strtmpIDNo.Substring(6, 4) + "-" + strtmpIDNo.Substring(10, 2) + "-" + strtmpIDNo.Substring(12, 2));
            }
            catch
            {
                return false;//非法生日
            }
            for (int i = 17; i >= 0; i--)
            {
                iSum += (System.Math.Pow(2, i) % 11) * int.Parse(strtmpIDNo[17 - i].ToString(), System.Globalization.NumberStyles.HexNumber);
            }
            if (iSum % 11 != 1)
            {
                return false;//非法证号
            }

            return true;
            //return (aCity[int.Parse(strtmpIDNo.Substring(0, 2))] + "," + strtmpIDNo.Substring(6, 4) + "-" + strtmpIDNo.Substring(10, 2) + "-" + strtmpIDNo.Substring(12, 2) + "," + (int.Parse(strtmpIDNo.Substring(16, 1) % 2 == 1 ? "男" : "女")));

        }
        //将15位身份证号码转换为18位
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
        /// 验证是否为中文
        /// </summary>
        /// <param name="strIn">需要验证的字符串</param>
        /// <returns></returns>
        public static bool funBoolean_ValidChineseChar(this string strIn)
        {
            int code = 0;
            int chfrom = Convert.ToInt32("4e00", 16);    //范围（0x4e00～0x9fff）转换成int（chfrom～chend）
            int chend = Convert.ToInt32("9fff", 16);
            if (strIn != "")
            {
                code = Char.ConvertToUtf32(strIn, 0);    //获得字符串input中指定索引index处字符unicode编码

                if (code >= chfrom && code <= chend)
                {
                    return true;     //当code在中文范围内返回true

                }
                else
                {
                    return false;    //当code不在中文范围内返回false
                }
            }
            return false;
        }

        #endregion

        #region "数据验证函数，并且返回值"
        /// <summary>   
        /// 验证是否是数字,如果true返回strValue,false返回intDefault
        /// </summary>   
        /// <param name="strValue">字符串</param>
        /// <param name="intDefault">默认值</param>
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
        /// 验证是否是数字,如果true,再判断是否在intMin与intMax之间,如果true返回strValue,false返回intDefault
        /// </summary>   
        /// <param name="strValue">字符串</param>
        /// <param name="intMin">区间的最小允许值</param>
        /// <param name="intMax">区间的最大允许值</param>
        /// <param name="intDefault">默认值</param>
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
        /// 验证是否是数字,并且返回decimal(带小数点类型),如果true返回strValue,false返回intDefault
        /// </summary>   
        /// <param name="strValue">字符串</param>
        /// <param name="decDefault">默认值</param>
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
        /// 验证是否是Uniqueidentifier格式,并且返回string,如果true返回strValue,false返回strDefault
        /// </summary>   
        /// <param name="strValue">字符串</param>
        /// <param name="strDefault">默认值</param>
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
        /// 将Bool字符串转化为0,1
        /// </summary>   
        /// <param name="strValue">字符串</param>
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
        /// 将字符转换为boolean，如是是true就为true，如果是false就为false；如果是0就为false，大于0的都变True
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
        /// 验证是否是时间，并且返回数据库操作的字符：如果是''，那么返回null；否则，返回'+date+'
        /// </summary>   
        /// <param name="strValue">字符串</param>
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
        /// 返回数据库操作的字符串：如果是字符串，就加上''；否则就返回默认值
        /// </summary>
        /// <param name="strValue"></param>
        /// <param name="strDefault">默认值</param>
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
        /// 返回非空的字符串
        /// </summary>
        /// <param name="strValue">需要验证的字符串</param>
        /// <param name="strDefault">如果为空的默认值</param>
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
        /// 返回只允许存在的字符串字符
        /// </summary>
        /// <param name="strValue">需要校验的字符串</param>
        /// <param name="strAllowString">只允许存在的字符</param>
        /// <returns></returns>
        public static string funString_ValidReplaceString(this string strValue, string strAllowString)
        {
            int intLen = 0;
            intLen = strValue.Length;

            string strTemp = "";
            string strChar = "";
            //循环所有的字符
            for (int i = 0; i < intLen; i++)
            {
                strChar = strValue.Substring(i, 1);
                //如果此字符是允许的范围内的字符，那么添加；否则不添加
                if (strAllowString.IndexOf(strChar) >= 0)
                {
                    strTemp = strTemp + strChar;
                }
            }
            return strTemp;
        }
        #endregion

        #region "文件操作相关函数"
        /// <summary>
        /// 验证选择上传的文件是否在允许的文件内
        /// </summary>
        /// <param name="fileAttachmentName">上传文件控件</param>
        /// <param name="blnAllowNull">是否允许空文件：True为允许；False为不允许</param>
        /// <param name="blnFilter">是允许strFilter类型的文件上传还是不允许strFilter类型的文件上传：True为不允许；False为允许</param>
        /// <param name="strFilter">定义什么类型文件为才能上传,如果为false,只能是Filter中的文件类型,true为不能为Filter中的文件类型|.asp|.exe|.htm|.html|.aspx|.cs|.vb|.js|</param>
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
                        return "不允许上传" + strFilter + "等后缀名以外的文件！";
                    }
                }
                else
                {
                    if (strFilter.IndexOf(strDocExtension) >= 0)
                    {
                        return "不允许上传" + strFilter + "等后缀名的文件！";
                    }
                }
            }
            else
            {
                if (!blnAllowNull)
                {
                    return "请选择相关附件！";
                }
            }
            return "";
        }

        /// <summary>
        /// 得到上传的附件格式化的名字一般格式为yyyyMMddHHmmss
        /// </summary>
        /// <param name="strAttachmentName">完整文件名</param>
        /// <param name="FileNameLength">新文件名的长度</param>
        /// <returns></returns>
        public static string funString_FormatAttachmentName(this string strAttachmentName, int FileNameLength)
        {
            //得到文件名
            string strFileName;
            strFileName = strAttachmentName;
            if (strFileName == "")
            {
                return "";
            }

            //得到原文件的后缀名(包括.号)
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

            //得到加上时间格式后的文件实际长度
            int intLen;
            intLen = strFileName.Length + 14;//14是时间字符串的长度

            //不允许实际长度超过设置的文件名长度
            if (intLen > FileNameLength)
            {
                intLen = FileNameLength;
            }
            //将文件名长度减掉默认的时间的长度和后缀，就是我们要使用的文件名称长度
            intLen = intLen - 14 - intSuffix;
            //文件名=时间格式+文件名+后缀
            strFileName = System.DateTime.Now.ToString("yyyyMMddHHmmss") + strFileName.Substring(0, intLen) + strSuffix;

            return strFileName;
        }

        /// <summary>
        /// 上传文件附件,并返回文件名,如果用户不指定文件名,文件名将格式为yyyyMMddhhmmss
        /// </summary>
        /// <param name="fileAttachmentName">上传文件控件</param>
        /// <param name="strFileName">新文件名</param>
        /// <param name="FileNameLength">如果不指定文件名，那么文件名称最大长度</param>
        /// <param name="strDirPath">保存文件目录名，使用/结尾</param>
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
        /// 删除文件
        /// </summary>
        /// <param name="FileName">文件名称</param>
        /// <param name="strPath">文件相对路径,带/</param>
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
        /// 读取文件的内容
        /// </summary>
        /// <param name="strRelativePath">文件的相对路径</param>
        /// <param name="objEncoding">编码格式</param>
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

        #region "ComboBox,CheckBoxList,RadioButtonList函数"
        /// <summary>   
        /// 根据Value设置选中ComboBox的选项
        /// </summary>   
        /// <param name="cboName">ComboBox</param>
        /// <param name="SelectedValue">要选中的Item的值</param>
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
        /// 根据Value设置选中ComboBox的选项
        /// </summary>   
        /// <param name="cboName">ComboBox</param>
        /// <param name="SelectedValue">要选中的Item的值</param>
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
        /// 根据Value内容选择Item，如果没有选择相应选项，那么就选择默认选项
        /// </summary>   
        /// <param name="cboName">ComboBox</param>
        /// <param name="SelectedValue">要选中的Item的值</param>
        /// <param name="DefaultValue">默认选项的Value</param>
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
        /// 根据Value内容选择Item，如果没有选择相应选项，那么就选择默认选项
        /// </summary>   
        /// <param name="cboName">ComboBox</param>
        /// <param name="SelectedValue">要选中的Item的值</param>
        /// <param name="DefaultValue">默认选项的Value</param>
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
        /// 填充CheckListBox
        /// </summary>
        /// <param name="objchkListBox"></param>
        /// <param name="strSearchSQL">第一位为ID,第二位为Text</param>
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
        /// 填充CheckListBox用SQL的列
        /// </summary>
        /// <param name="objchkListBox"></param>
        /// <param name="strSearchSQL">列的Text</param>
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
        /// 根据Value勾选CheckListBox
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
        /// 返回CheckListBox勾选中的值
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
        /// 返回CheckListBox勾选中的值用[]加上
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
        /// 返回CheckListBox勾选中的文本
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
        /// 设置选中CheckBoxList的选项
        /// </summary>   
        /// <param name="chklstName">CheckBoxList</param>
        /// <param name="SelectedValues">要选中的Item的值列表</param>
        /// <param name="strSplitChar">SelectedValues中的分割符</param>
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
        /// 根据Text设置选中ComboBox的选项
        /// </summary>   
        /// <param name="cboName">ComboBox</param>
        /// <param name="SelectedValue">要选中的Item的值</param>
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
        /// 根据Text设置选中ComboBox的选项
        /// </summary>   
        /// <param name="cboName">ComboBox</param>
        /// <param name="SelectedValue">要选中的Item的值</param>
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
        /// 根据Text内容选择Item，如果没有选择相应选项，那么就选择默认选项
        /// </summary>   
        /// <param name="cboName">ComboBox</param>
        /// <param name="SelectedValue">要选中的Item的值</param>
        /// <param name="DefaultValue">默认选项的Text</param>
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
        /// 根据Text内容选择Item，如果没有选择相应选项，那么就选择默认选项
        /// </summary>   
        /// <param name="cboName">ComboBox</param>
        /// <param name="SelectedValue">要选中的Item的值</param>
        /// <param name="DefaultValue">默认选项的Text</param>
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
        /// 载入数据列到ComboBox
        /// </summary>   
        /// <param name="cboName">ComboBox</param>
        /// <param name="strSearchSQL">取值的SQL</param>
        /// <param name="IsDisplayDefaultText">是否显示默认的选项</param>
        public static void subComboBox_LoadItemsByDBColumnName(this HtmlSelect cboName, string strSearchSQL, bool IsDisplayDefaultText)
        {
            cboName.Items.Clear();
            ListItem item;
            if (IsDisplayDefaultText)
            {
                item = new ListItem();
                item.Value = "SelectAll";
                item.Text = "-----选择所有记录-----";
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
        /// 载入下拉菜单真假
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
        /// 载入数据列到ComboBox
        /// </summary>   
        /// <param name="cboName">ComboBox</param>
        /// <param name="strSearchSQL">取值的SQL</param>
        /// <param name="IsDisplayDefaultText">是否显示默认的选项</param>
        public static void subComboBox_LoadItemsByDBColumnName(this DropDownList cboName, string strSearchSQL, bool IsDisplayDefaultText)
        {
            cboName.Items.Clear();
            ListItem item;
            if (IsDisplayDefaultText)
            {
                item = new ListItem();
                item.Value = "SelectAll";
                item.Text = "-----选择所有记录-----";
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
        /// 返回RadioButtonList是否有选中项,如果选中的Item值为1就返回1,否则返回0,如是SelectedIndex为0返回null,这个函数主要用于数据库存RadioButtonList值用.
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
        /// 设置RadioButtonList被选中的项
        /// </summary>   
        /// <param name="rdoButtonlist">RadioButtonList</param>
        /// <param name="itemValue">需要被选中的Item项的值</param>
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
        /// 返回ComboBox选中Item的Text
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
        /// 返回ComboBox选中Item的Text
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
        /// 返回ComboBox选中Item的Value
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
        /// 返回ComboBox选中Item的Value
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
        /// 载入ComboBox的Item项,如果列数大于1,将0列为Value,1列为Text
        /// </summary>   
        /// <param name="cboBox">cboBox</param>
        /// <param name="strSearchSQL">操作的SQL</param>
        /// <param name="intSelectIndex">默认选中的Index</param>
        /// <param name="ItemDefault">如果为空(NULL)，则不指定默认Item项，否则ItemDefault为默认项</param>
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
        /// 载入ComboBox的Item项,如果列数大于1,将0列为Value,1列为Text
        /// </summary>   
        /// <param name="cboBox">cboBox</param>
        /// <param name="strSearchSQL">操作的SQL</param>
        /// <param name="intSelectIndex">默认选中的Index</param>
        /// <param name="ItemDefault">如果为空(NULL)，则不指定默认Item项，否则ItemDefault为默认项</param>
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
        /// 返回选中的RadionButtonList的值
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
        /// 返回CheckBox状态,用0,1返回,0为没选中,1为选中
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
        /// 返回CheckBox状态,用0,1返回,0为没选中,1为选中
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

        #region"发送Email"
        /// <summary>
        /// 用webMail方式发送Mail,成功返回"",失败返回错误代码
        /// </summary>
        /// <param name="strSendMail">发送人的Mail</param>
        /// <param name="strSendUserName">发送人的用户名</param>
        /// <param name="strSendUserPassword">发送人的密码</param>
        /// <param name="strBody">发送内容</param>
        /// <param name="strSmtp">Smtp Server</param>
        /// <param name="strToMail">接收人的Mail</param>
        /// <param name="strSubject">发送主题</param>
        /// <param name="strCc">抄送人的Mail</param>
        /// <param name="isDellAttachment">发送后是否删除附件</param>
        /// <param name="strBcc">暗送人的Mail</param>
        /// <param name="aryAttachment">附件列表</param>
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
            //附件
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
            //验证
            objMailMessage.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1"); //basic authentication 
            objMailMessage.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", strSendUserName); //set your username here 
            objMailMessage.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", strSendUserPassword); //set your password here 
            //发送
            try
            {
                System.Web.Mail.SmtpMail.Send(objMailMessage);
            }
            catch (Exception ex)
            {
                strReturn = ex.Source + "\n" + ex.Message;
            }
            //删除文件
            try
            {
                //删除文件
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
        /// 用NetMail方式发送Mail,成功返回"",失败返回错误代码
        /// </summary>
        /// <param name="strSendMail">发送人的Mail</param>
        /// <param name="strSendUserName">发送人的用户名</param>
        /// <param name="strSendUserPassword">发送人的密码</param>
        /// <param name="strBody">发送内容</param>
        /// <param name="strSmtp">Smtp Server</param>
        /// <param name="strToMail">接收人的Mail</param>
        /// <param name="strSubject">发送主题</param>
        /// <param name="strCc">抄送人的Mail</param>
        /// <param name="isDellAttachment">发送后是否删除附件</param>
        /// <param name="strBcc">暗送人的Mail</param>
        /// <param name="aryAttachment">附件列表</param>
        /// <returns></returns>
        public static string funString_SendMailByNetMail(this string strSendMail, string strSendUserName, string strSendUserPassword, string strBody, string strSmtp, string strToMail, string strSubject, string strCc, bool isDellAttachment, string strBcc, ArrayList aryAttachment)
        {
            string strReturn = "";
            System.Net.Mail.MailMessage objMailMessage = new System.Net.Mail.MailMessage();
            //设置信件的基本信息
            objMailMessage.Subject = strSubject;
            objMailMessage.Body = strBody;
            objMailMessage.IsBodyHtml = true;
            objMailMessage.BodyEncoding = Encoding.GetEncoding("GB2312");
            //设置发件人
            System.Net.Mail.MailAddress objMailAddress = new System.Net.Mail.MailAddress(strSendMail);
            objMailMessage.From = objMailAddress;

            //设置收件人
            string[] aryToMail = strToMail.Split(';');
            for (int i = 0; i < aryToMail.Length; i++)
            {
                objMailMessage.To.Add(aryToMail[i].ToString());
            }
            //设置CC
            if (strCc != "")
            {
                string[] aryCCMail = strCc.Split(';');
                for (int i = 0; i < aryCCMail.Length; i++)
                {
                    objMailMessage.To.Add(aryCCMail[i].ToString());
                }
            }
            //设置BCC
            if (strBcc != "")
            {
                string[] aryBCCMail = strCc.Split(';');
                for (int i = 0; i < aryBCCMail.Length; i++)
                {
                    objMailMessage.Bcc.Add(aryBCCMail[i].ToString());
                }
            }
            //附件
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
            //发送
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
                //删除文件
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

        #region"判断进程启动时间，找出相应的进程并杀死进程"
        /// <summary>
        /// 判断进程启动时间，找出相应的进程并杀死进程
        /// </summary>   
        /// <param name="dtStartDate">进程启用开始时间</param>
        /// <param name="dtEndDate">进程启用结束时间</param>
        /// <param name="strProcessName">杀死的进程名</param>
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

        #region "读取一个word的XML模板文件"
        /// <summary>
        /// 读取一个word的XML模板文件
        /// </summary>   
        /// <param name="strTemplateAbsolutePath">模板的完整地址</param>
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

        #region "中文->UTF8 CODE"
        /// <summary>
        /// 中文->UTF8 CODE
        /// </summary>   
        /// <param name="strContent">字符串</param>
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

        #region "搜索控件FindControl功能扩展"
        /// <summary>
        /// 用递规搜索
        /// </summary>
        /// <param name="o">一个虚的传值</param>
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

        #region "按比例缩放图片"
        /// <summary>
        /// 按比例保存图片
        /// </summary>
        /// <param name="ImagePathFileName">源图片文件相对路径与文件名称</param>
        /// <param name="TargetImagePath">目标图片文件相对路径,带/</param>
        /// <param name="TargetImageFileName">目标图片文件名称</param>
        /// <param name="ScaleZoom">缩放比例</param>
        /// <returns>False:没有图片缩放成功；True:图片缩放成功</returns>
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
            //缩放后的文件大小定义
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
        /// 按比例保存图片
        /// </summary>
        /// <param name="ImagePathFileName">源图片文件相对路径与文件名称</param>
        /// <param name="TargetImagePath">目标图片文件相对路径,带/</param>
        /// <param name="TargetImageFileName">目标图片文件名称</param>
        /// <param name="ScaleZoom">缩放比例</param>
        /// <param name="FixWidth">具体缩放的图片宽</param>
        /// <param name="FixHeight">具体缩放的图片高</param>
        /// <param name="IsTargetWidth">以具体缩放中的宽或者高做为标准，True:以宽为标准；False:以高为标准；</param>
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
            //缩放后的文件大小定义
            if (FixWidth != 0 && FixHeight != 0)
            {
                thumWidth = intWidth;
                thumHeight = intHeight;
                #region "根据原始尺寸计算使用宽的那个缩放比例"
                if (intWidth > FixWidth && IsTargetWidth == true)//如果使用宽做为比例，而且现有图片的宽大于缩放图片的宽
                {
                    thumWidth = Convert.ToInt16(FixWidth);
                    thumHeight = Convert.ToInt16(intHeight * (Convert.ToDouble(FixWidth) / Convert.ToDouble(intWidth)));
                    //如果得到的高度还超过标准高度，那么直接压缩成标准高度(这样容易让图片变形)
                    if (thumHeight > Convert.ToInt16(FixHeight))
                    {
                        thumHeight = Convert.ToInt16(FixHeight);
                    }
                }
                if (intHeight > FixHeight && IsTargetWidth == false)//如果使用高做为比例，而且现有图片的高大于缩放图片的高
                {
                    thumWidth = Convert.ToInt16(intWidth * (Convert.ToDouble(FixHeight) / Convert.ToDouble(intHeight)));
                    //如果得到的宽度还超过标准宽度，那么直接压缩成标准宽度(这样容易让图片变形)
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
        /// 根据图片判断是否大于最大宽度，如果大于，使用最大宽度
        /// </summary>
        /// <param name="ImagePathFileName">文件相对路径与文件名称</param>
        /// <param name="MaxImageWidth">图片最大允许宽度</param>
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

        #region "汉字首字母"
        /// <summary> 
        /// 在指定的字符串列表CnStr中检索符合拼音索引字符串 
        /// </summary> 
        /// <param name="CnStr">汉字字符串</param> 
        /// <returns>相对应的汉语拼音首字母串</returns> 
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
        /// 得到一个汉字的拼音第一个字母，如果是一个英文字母则直接返回大写字母 
        /// </summary> 
        /// <param name="CnChar">单个汉字</param> 
        /// <returns>单个大写字母</returns> 
        public static string funString_GetCharSpellCode(this string CnChar)
        {
            long iCnChar;

            byte[] ZW = System.Text.Encoding.Default.GetBytes(CnChar);

            //如果是字母，则直接返回 
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
            //没有U,V 
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

        #region "语音播放"
        public static string funHTML_FilePlay(this string PathFileName, int intHeight, int intWidth)
        {
            string strHTML = "";

            #region "网易有道的播放读音"
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

            #region "网上的代码"
            //http://topic.csdn.net/u/20080427/13/7894d5b7-02e7-447c-bc97-50b941ffca0d.html
            strHTML = "<object id='player' height='" + intHeight + "' width='" + intWidth + "' classid='CLSID:6BF52A52-394A-11d3-B153-00C04F79FAA6'> ";
            //<!--是否自动播放--> 
            strHTML = strHTML + "<param NAME='AutoStart' VALUE='-1'>";
            //<!--调整左右声道平衡,同上面旧播放器代码--> 
            strHTML = strHTML + "<param NAME='Balance' VALUE='0'>";
            //<!--播放器是否可人为控制--> 
            strHTML = strHTML + "<param name='enabled' value='-1'>";
            //<!--是否启用上下文菜单--> 
            strHTML = strHTML + "<param NAME='EnableContextMenu' VALUE='-1'>";
            //<!--播放的文件地址--> 
            //strHTML = strHTML + "<param NAME='url' value='/blog/1.wma'>";
            strHTML = strHTML + "<param NAME='url' value='" + PathFileName + "'>";
            //<!--播放次数控制,为整数--> 
            strHTML = strHTML + "<param NAME='PlayCount' VALUE='1'>";
            //<!--播放速率控制,1为正常,允许小数,1.0-2.0--> 
            strHTML = strHTML + "<param name='rate' value='1'>";
            //<!--控件设置:当前位置--> 
            strHTML = strHTML + "<param name='currentPosition' value='0'>";
            //<!--控件设置:当前标记--> 
            strHTML = strHTML + "<param name='currentMarker' value='0'>";
            //<!--显示默认框架--> 
            strHTML = strHTML + "<param name='defaultFrame' value=''>";
            //<!--脚本命令设置:是否调用URL--> 
            strHTML = strHTML + "<param name='invokeURLs' value='0'>";
            //<!--脚本命令设置:被调用的URL--> 
            strHTML = strHTML + "<param name='baseURL' value=''>";
            //<!--是否按比例伸展--> 
            strHTML = strHTML + "<param name='stretchToFit' value='0'>";
            //<!--默认声音大小0%-100%,50则为50%--> 
            strHTML = strHTML + "<param name='volume' value='50'>";
            //<!--是否静音--> 
            strHTML = strHTML + "<param name='mute' value='0'>";
            //<!--播放器显示模式:Full显示全部;mini最简化;None不显示播放控制,只显示视频窗口;invisible全部不显示--> 
            strHTML = strHTML + "<param name='uiMode' value='mini'>";
            //<!--如果是0可以允许全屏,否则只能在窗口中查看--> 
            strHTML = strHTML + "<param name='windowlessVideo' value='0'>";
            //<!--开始播放是否自动全屏--> 
            strHTML = strHTML + "<param name='fullScreen' value='0'>";
            //<!--是否启用错误提示报告--> 
            strHTML = strHTML + "<param name='enableErrorDialogs' value='-1'>";
            //<!--SAMI样式--> 
            strHTML = strHTML + "<param name='SAMIStyle' value>";
            //<!--SAMI语言--> 
            strHTML = strHTML + "<param name='SAMILang' value>";
            //<!--字幕ID--> 
            strHTML = strHTML + "<param name='SAMIFilename' value>";
            strHTML = strHTML + "</object>";
            #endregion

            return strHTML;
        }
        #endregion

        #region "递规控件中的控件"
        private static System.Web.UI.Control _objFindContorl;
        /// <summary>
        /// 递规控件中的控件
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

        #region "读取一个HTML文件"
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
        #region "将Js的输入为正常的HTML"
        /// <summary>
        /// 将Js的输入为正常的HTML
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
        /// 将Js的输入为正常的HTML
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

        #region "除去HTML"
        public static string funString_RemoveHTML(this string strHtml)
        {
            string strOutput = strHtml;
            Regex regex = new Regex(@"<[^>]+>|</[^>]+>");
            strOutput = regex.Replace(strOutput, "");
            return strOutput;
        }
        #endregion

        /// <summary>
        /// 发送匿名Email
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
            //附件
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