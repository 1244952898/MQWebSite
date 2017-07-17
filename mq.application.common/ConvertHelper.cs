using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace mq.application.common
{
    public static class ConvertHelper
    {
        #region 普通转换
        /// <summary>
        /// 转换Int类型
        /// </summary>
        /// <param name="data"></param>
        /// <param name="RtnData"></param>
        /// <returns></returns>
        public static int ToInt(this object data, int RtnData)
        {
            int rtnData = 0;
            try
            {
                rtnData = Convert.ToInt32(data);
            }
            catch
            {
                rtnData = RtnData;
            }
            return rtnData;
        }

        /// <summary>
        /// 转换long类型
        /// </summary>
        /// <param name="data"></param>
        /// <param name="RtnData"></param>
        /// <returns></returns>
        public static long ToLong(this object data, long RtnData)
        {
            long rtnData = 0;
            try
            {
                if (data != null)
                    rtnData = Convert.ToInt64(data);
            }
            catch
            {
                rtnData = RtnData;
            }
            return rtnData;
        }

        /// <summary>
        /// 转换float类型
        /// </summary>
        /// <param name="data"></param>
        /// <param name="RtnData"></param>
        /// <returns></returns>
        public static float ToFloat(this object data, float RtnData)
        {
            float rtnData = 0;
            try
            {
                if (data != null)
                    rtnData = Convert.ToSingle(data);
            }
            catch
            {
                rtnData = RtnData;
            }
            return rtnData;
        }

        /// <summary>
        /// 转换人名币类型字符串
        /// </summary>
        /// <param name="Money"></param>
        /// <returns></returns>
        public static string ToMoney(this object Money)
        {
            double floatMoney = Money.ToDouble(0);
            if (floatMoney <= 0)
            {
                return "0.00";
            }
            else
            {
                floatMoney = Math.Round(floatMoney, 1, MidpointRounding.AwayFromZero);//
                string rtnStr = floatMoney.ToString("0.0") + "0";
                return rtnStr;
            }
        }

        /// <summary>
        /// 转换人名币类型字符串
        /// </summary>
        /// <param name="Money"></param>
        /// <returns></returns>
        public static string ToMoney_2(this object Money)
        {
            double floatMoney = Money.ToDouble(0);
            if (floatMoney <= 0)
            {
                return "0.00";
            }
            else
            {
                floatMoney = Math.Round(floatMoney, 2, MidpointRounding.AwayFromZero);//
                string rtnStr = floatMoney.ToString("0.00");
                return rtnStr;
            }
        }


        /// <summary>
        /// 转换double类型
        /// </summary>
        /// <param name="data"></param>
        /// <param name="RtnData"></param>
        /// <returns></returns>
        public static double ToDouble(this object data, double RtnData)
        {
            double rtnData = 0;
            try
            {
                if (data != null)
                    rtnData = Convert.ToDouble(data);
            }
            catch
            {
                rtnData = RtnData;
            }
            return rtnData;
        }
        /// <summary>
        /// 转换decimal类型
        /// </summary>
        /// <param name="data"></param>
        /// <param name="RtnData"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this object data, decimal RtnData)
        {
            decimal rtnData = 0;
            try
            {
                if (data != null)
                    rtnData = Math.Round(Convert.ToDecimal(data), 2, MidpointRounding.AwayFromZero);
            }
            catch
            {
                rtnData = RtnData;
            }
            return rtnData;
        }

        /// <summary>
        /// 转换decimal类型(保留两位小数)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="RtnData"></param>
        /// <returns></returns>
        public static decimal ToDecimal_2(this object data, decimal RtnData)
        {
            decimal rtnData = 0;
            try
            {
                if (data != null)
                    rtnData = Convert.ToDecimal(data);
            }
            catch
            {
                rtnData = RtnData;
            }
            return rtnData;
        }

        /// <summary>
        /// 转换string类型
        /// </summary>
        /// <param name="data"></param>
        /// <param name="RtnData"></param>
        /// <returns></returns>
        public static string ToString(this object data, string RtnData)
        {
            string rtnData = "";
            try
            {
                if (data != null && data.ToString() != "")
                    rtnData = Convert.ToString(data);
            }
            catch
            {
                rtnData = RtnData;
            }
            return rtnData;
        }

        /// <summary>
        /// 转换char类型
        /// </summary>
        /// <param name="data"></param>
        /// <param name="RtnData"></param>
        /// <returns></returns>
        public static char ToChar(this object data, char RtnData)
        {
            char rtnData = ' ';
            try
            {
                if (data != null && data.ToString() != "")
                    rtnData = Convert.ToChar(data);
            }
            catch
            {
                rtnData = RtnData;
            }
            return rtnData;
        }

        /// <summary>
        /// 转换datetime类型
        /// </summary>
        /// <param name="data">要转换的字符串</param>
        /// <param name="RtnData">默认返回值</param>
        /// <param name="format">格式化时间字符串</param>
        /// <returns></returns>
        public static string ToDateTime(this object data, string RtnData, string format)
        {
            string rtnData = RtnData;
            try
            {
                if (data != null && data.ToString() != "" && Convert.ToDateTime(data).ToString("yyyy") != "1900")
                    rtnData = Convert.ToDateTime(data).ToString(format);
            }
            catch
            {
            }
            return rtnData;
        }

        /// <summary>
        /// 转换datetime类型
        /// </summary>
        /// <param name="data"></param>
        /// <param name="RtnData"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this object data, DateTime RtnData)
        {
            DateTime rtnData = RtnData;
            try
            {
                if (data != null && data.ToString() != "" && Convert.ToDateTime(data).ToString("yyyy") != "1900")
                    rtnData = Convert.ToDateTime(data);
            }
            catch
            {
            }
            return rtnData;
        }

        /// <summary>
        /// 转换guid类型
        /// </summary>
        /// <param name="value"></param>
        /// <param name="guid"></param>
        /// <returns></returns>
        //public static Guid ToGuid(this string value, Guid guid)
        //{
        //    Guid rtnGuid;
        //    if (!Guid.TryParse(value, out rtnGuid))
        //        rtnGuid = guid;
        //    return rtnGuid;
        //}
        #endregion

        #region 强制转化

        /// <summary>
        /// object转化为Bool类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool ObjToBool(this object obj)
        {
            bool flag;
            if (obj == null)
            {
                return false;
            }
            if (obj.Equals(DBNull.Value))
            {
                return false;
            }
            return (bool.TryParse(obj.ToString(), out flag) && flag);
        }

        /// <summary>
        /// object强制转化为DateTime类型(吃掉异常)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime? ObjToDateNull(this object obj)
        {
            if (obj == null)
            {
                return null;
            }
            try
            {
                return new DateTime?(Convert.ToDateTime(obj));
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// int强制转化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ObjToInt(this object obj)
        {
            if (obj != null)
            {
                int num;
                if (obj.Equals(DBNull.Value))
                {
                    return 0;
                }
                if (int.TryParse(obj.ToString(), out num))
                {
                    return num;
                }
            }
            return 0;
        }

        /// <summary>
        /// 强制转化为long
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static long ObjToLong(this object obj)
        {
            if (obj != null)
            {
                long num;
                if (obj.Equals(DBNull.Value))
                {
                    return 0;
                }
                if (long.TryParse(obj.ToString(), out num))
                {
                    return num;
                }
            }
            return 0;
        }

        /// <summary>
        /// 强制转化可空int类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int? ObjToIntNull(this object obj)
        {
            if (obj == null)
            {
                return null;
            }
            if (obj.Equals(DBNull.Value))
            {
                return null;
            }
            return new int?(ObjToInt(obj));
        }

        /// <summary>
        /// 强制转化为string
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ObjToStr(this object obj)
        {
            if (obj == null)
            {
                return "";
            }

            if (obj.Equals(DBNull.Value))
            {
                return "";
            }
            return Convert.ToString(obj);
        }

        /// <summary>
        /// Decimal转化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal ObjToDecimal(this object obj)
        {
            if (obj == null)
            {
                return 0M;
            }
            if (obj.Equals(DBNull.Value))
            {
                return 0M;
            }
            try
            {
                return Convert.ToDecimal(obj);
            }
            catch
            {
                return 0M;
            }
        }

        /// <summary>
        /// Decimal可空类型转化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal? ObjToDecimalNull(this object obj)
        {
            if (obj == null)
            {
                return null;
            }
            if (obj.Equals(DBNull.Value))
            {
                return null;
            }
            return new decimal?(ObjToDecimal(obj));
        }

        /// <summary>
        /// Double强制转化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static double ObjToDouble(this object obj)
        {
            if (obj != null)
            {
                double num;
                if (obj.Equals(DBNull.Value))
                {
                    return 0;
                }
                if (double.TryParse(obj.ToString(), out num))
                {
                    return num;
                }
            }
            return 0;
        }
        #endregion

        #region 判断对象是否为空

        /// <summary>
        /// 判断对象是否为空，为空返回true
        /// </summary>
        /// <typeparam name="T">要验证的对象的类型</typeparam>
        /// <param name="data">要验证的对象</param>        
        public static bool IsNullOrEmpty<T>(this T data)
        {
            //如果为null
            if (data == null)
            {
                return true;
            }

            //如果为""
            if (data.GetType() == typeof(String))
            {
                if (string.IsNullOrEmpty(data.ToString().Trim()) || data.ToString() == "")
                {
                    return true;
                }
            }

            //如果为DBNull
            if (data.GetType() == typeof(DBNull))
            {
                return true;
            }

            //不为空
            return false;
        }

        /// <summary>
        /// 判断对象是否为空，为空返回true
        /// </summary>
        /// <param name="data">要验证的对象</param>
        public static bool IsNullOrEmpty(this object data)
        {
            //如果为null
            if (data == null)
            {
                return true;
            }

            //如果为""
            if (data.GetType() == typeof(String))
            {
                if (string.IsNullOrEmpty(data.ToString().Trim()))
                {
                    return true;
                }
            }

            //如果为DBNull
            if (data.GetType() == typeof(DBNull))
            {
                return true;
            }

            //不为空
            return false;
        }
        #endregion

        #region 验证判断
        public static bool IsInclude(string[] IPRegion, string IP)
        {
            //验证    
            if (null == IPRegion || null == IP || 0 == IPRegion.Length)
                return false;

            if (!ValidateIPAddress(IP))
                return false;

            if (1 == IPRegion.Length)
            {
                if (!ValidateIPAddress(IPRegion[0]))
                    return false;

                if (0 == Compare(IPRegion[0], IP))
                    return true;
            }

            if (!(ValidateIPAddress(IPRegion[0]) && ValidateIPAddress(IPRegion[1])))
                return false;

            uint IPNum = TransNum(IP);
            uint IPNum1 = TransNum(IPRegion[0]);
            uint IPNum2 = TransNum(IPRegion[1]);

            //比较    
            if (Math.Min(IPNum1, IPNum2) <= IPNum && Math.Max(IPNum1, IPNum2) >= IPNum)
                return true;

            return false;
        }
        public static bool ValidateIPAddress(string strIP)
        {
            if (null == strIP || "" == strIP.Trim() || Convert.IsDBNull(strIP))
                return false;

            return Regex.IsMatch(strIP, @"^((\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.){3}(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$");
        }
        public static int Compare(string IP1, string IP2)
        {
            if (!(ValidateIPAddress(IP1) && ValidateIPAddress(IP2)))
                throw new Exception("IP Address isn''t Well Format!");

            uint IPNum1 = TransNum(IP1);
            uint IPNum2 = TransNum(IP2);

            if (IPNum1 == IPNum2)
                return 0;

            return IPNum1 > IPNum2 ? 1 : -1;
        }


        public static bool IsContainsHtml(string strHtml)
        {
            bool bContains = false;
            if (string.IsNullOrEmpty(strHtml))
                return bContains;
            string[] aryReg ={ 
                @"<script[^>]*?>.*?</script>", 
                @"<iframe[^>]*?>.*?</iframe>", 
                @"<!--.*\n(-->)?", 
                @"<(\/\s*)?(.|\n)*?(\/\s*)?>", 
                @"<(\w|\s|""|'| |=|\\|\.|\/|#)*", 
                //@"([\r\n|\s])*", 
                @"&(quot|#34);", 
                @"&(amp|#38);", 
                @"&(lt|#60);", 
                @"&(gt|#62);", 
                @"&(nbsp|#160);", 
                @"&(iexcl|#161);", 
                @"&(cent|#162);", 
                @"&(pound|#163);", 
                @"&(copy|#169);", 
                @"&#(\d+);"};

            string newReg = aryReg[0];
            for (int i = 0; i < aryReg.Length; i++)
            {
                Regex regex = new Regex(aryReg[i], RegexOptions.IgnoreCase);
                bContains = regex.IsMatch(strHtml);
                if (bContains)
                    break;
            }

            return bContains;
        }
        #region 检查输入的参数是不是某些定义好的特殊字符：这个方法目前用于密码输入的安全检查
        /// <summary>
        /// 检查输入的参数是不是某些定义好的特殊字符：这个方法目前用于密码输入的安全检查
        /// </summary>
        public static bool isContainSpecChar(string strInput)
        {
            string[] list = new string[] { "123456", "654321" };
            bool result = new bool();
            for (int i = 0; i < list.Length; i++)
            {
                if (strInput == list[i])
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
        #endregion


        #region 判断是否以英文字符结束
        public static bool IsEndWithLetter(string inputStr)
        {
            if (string.IsNullOrEmpty(inputStr))
                return false;
            return Regex.IsMatch(inputStr, "[a-zA-Z]$");
        }
        #endregion
        #endregion

        #region 其它转换
        public static uint TransNum(string IPAddr)
        {
            if (!ValidateIPAddress(IPAddr))
                throw new Exception("IP Address isn''t Well Format!");

            string[] IPStrArray = new string[4];
            IPStrArray = IPAddr.Split('.');
            return MAKELONG(MAKEWORD(byte.Parse(IPStrArray[3]), byte.Parse(IPStrArray[2])), MAKEWORD(byte.Parse(IPStrArray[1]), byte.Parse(IPStrArray[0])));
        }
        /// <summary>    
        /// 移位转换_8    
        /// </summary>    
        /// <param name="bLow"></param>    
        /// <param name="bHigh"></param>    
        /// <returns></returns>    
        private static ushort MAKEWORD(byte bLow, byte bHigh)
        {
            return ((ushort)(((byte)(bLow)) | ((ushort)((byte)(bHigh))) << 8));
        }

        /// <summary>    
        /// 移位转换_16    
        /// </summary>    
        /// <param name="bLow"></param>    
        /// <param name="bHigh"></param>    
        /// <returns></returns>    
        private static uint MAKELONG(ushort bLow, ushort bHigh)
        {
            return ((uint)(((ushort)(bLow)) | ((uint)((ushort)(bHigh))) << 16));
        }

        /// <summary>
        /// 移除字符串中的所有空格
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string TrimAll(string str)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            CharEnumerator CEnumerator = str.GetEnumerator();
            while (CEnumerator.MoveNext())
            {
                byte[] array = new byte[1];
                array = System.Text.Encoding.ASCII.GetBytes(CEnumerator.Current.ToString());
                int asciicode = (short)(array[0]);
                if (asciicode != 32)
                {
                    sb.Append(CEnumerator.Current.ToString());
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 将字符串转换为数组
        /// </summary>
        /// <param name="str"></param>
        /// <param name="strSplit"></param>
        /// <returns></returns>
        public static string[] TransStringToArray(string str, char strSplit)
        {
            string[] arr;
            str = string.IsNullOrEmpty(str) ? "" : str;
            if (str.IndexOf(strSplit) == -1)
            {
                arr = new string[] { str };
            }
            else
            {
                string[] temparr = str.Split(strSplit);
                arr = new string[temparr.Length - 1];
                for (int i = 0; i < temparr.Length; i++)
                {
                    if (temparr[i] != "")
                        arr[i] = temparr[i];
                }
            }
            return arr;
        }
        #endregion

        #region 可转换类型验证
        /// <summary>
        /// 是否为日期型字符串
        /// </summary>
        /// <param name="StrSource">日期字符串(2008-05-08)</param>
        /// <returns></returns>
        public static bool IsDate(string StrSource)
        {
            return Regex.IsMatch(StrSource, @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-9]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$");
        }

        /// <summary>
        /// 是否为时间型字符串
        /// </summary>
        /// <param name="source">时间字符串(15:00:00)</param>
        /// <returns></returns>
        public static bool IsTime(string StrSource)
        {
            return Regex.IsMatch(StrSource, @"^((20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d)$");
        }

        /// <summary>
        /// 是否为日期+时间型字符串
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsDateTime(string StrSource)
        {
            return Regex.IsMatch(StrSource, @"^(((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-)) (20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d)$ ");
        }

        /// <summary>
        /// 是否为Int32
        /// </summary>
        /// <param name="StrSource"></param>
        /// <returns></returns>
        public static bool IsInt32(string StrSource)
        {
            try
            {
                Int32.Parse(StrSource);
            }
            catch
            {
                return false;
            }
            return true;
        }
        #endregion

        #region 充值类型转换
        public static string ShowRechargeTypeName(int type)
        {

            string rtnStr = "";
            switch (type)
            {
                case 2:
                    rtnStr = "银联在线";
                    break;
                case 3:
                    rtnStr = "网银";
                    break;
                case 4:
                    rtnStr = "支付宝";
                    break;
                case 5:
                    rtnStr = "智库充值卡";
                    break;
                case 6:
                    rtnStr = "财付通";
                    break;
                case 7:
                    rtnStr = "神州行卡";
                    break;
                case 8:
                    rtnStr = "移动短信";
                    break;
                case 9:
                    rtnStr = "联通短信";
                    break;
                case 10:
                    rtnStr = "电信短信";
                    break;
            }
            return rtnStr;
        }
        #endregion
    }
}
