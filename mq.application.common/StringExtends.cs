using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace mq.application.common
{
    public static class StringExtends
    {
        /// <summary>
        /// 去掉全部HTML的 小于号=>  gt;,大于号=>  lt;,空格=>  nbsp
        /// </summary>
        /// <param name="strHtml"></param>
        /// <param name="isReservationSimpleTag">是否保留大于小于号、制表和换行符</param>
        /// <param name="maxLength">截取的长度</param>
        /// <returns></returns>
        public static string StringNoHtml(this string strHtml, bool isReservationSimpleTag = false, int maxLength = 0)
        {
            if (String.IsNullOrWhiteSpace(strHtml))
            {
                return string.Empty;
            }
            else
            {
                string[] aryReg ={ 
                @"<script[^>]*?>.*?</script>", 
                @"<iframe[^>]*?>.*?</iframe>", 
                @"<!--.*\n(-->)?", 
                @"<(\/\s*)?(.|\n)*?(\/\s*)?>", 
                @"<(\w|\s|""|'| |=|\\|\.|\/|#)*", 
                @"([\r\n|\s])*", 
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
                string strOutput = strHtml.Replace("&nbsp;", " ");
                for (int i = 0; i < aryReg.Length; i++)
                {
                    Regex regex = new Regex(aryReg[i], RegexOptions.IgnoreCase);
                    strOutput = regex.Replace(strOutput, string.Empty);
                }
                if (!isReservationSimpleTag)
                {
                    strOutput = strOutput.Replace("<", "&gt;").Replace(">", "&lt;").Replace("\r", string.Empty).Replace("\n", string.Empty);
                }
                if (maxLength > 0 && strOutput.Length > maxLength)
                    strOutput = strOutput.Substring(0, maxLength);
                return strOutput.Replace(" ", "&nbsp;");
            }
        }

        private static string sqlfilter = "exec|insert|select|delete|update|chr|mid|master|or|truncate|char|declare|join|cmd|;|'|--";
        /// <summary>
        /// 去除sql保留字
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static string SqlFilterExt(this string sql)
        {
            if (string.IsNullOrEmpty(sql))
                return string.Empty;
            foreach (string i in sqlfilter.Split('|'))
            {
                sql = sql.Replace(i + " ", "").Replace(" " + i, "");
            }
            sql = sql.Replace("'", "");
            return sql;
        }

        public static bool IsContainsHtmlFilter(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return false;
            bool isContains = false;
            str = str.Trim();
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
                //@"&(nbsp|#160);", 
                @"&(iexcl|#161);", 
                @"&(cent|#162);", 
                @"&(pound|#163);", 
                @"&(copy|#169);", 
                @"%(27|32|3E|3C|3D|3F)",
                @"&#(\d+);"};

            string newReg = aryReg[0];
            for (int i = 0; i < aryReg.Length; i++)
            {
                Regex regex = new Regex(aryReg[i], RegexOptions.IgnoreCase);
                if (regex.IsMatch(str))
                {
                    isContains = true;
                    break;
                }
            }
            return isContains;
        }

        public static bool IsContainsSqlFilter(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return false;
            bool isContains = false;
            str = str.Trim();
            foreach (string i in sqlfilter.Split('|'))
            {
                if (str.IndexOf(i + "%20") != -1 || str.IndexOf("%20" + i) != -1 || str.IndexOf(i + " ") != -1 || str.IndexOf(" " + i) != -1)
                {
                    isContains = true;
                    break;
                }
            }
            return isContains;
        }

        /// <summary>
        /// 替换掉',()\n\r---等
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ReplaceSqlTag(this string str)
        {
            if (str == null || str.Length == 0)
                return string.Empty;
            str = str.StringNoHtml();
            str = str.Replace("'", "‘").Replace(",", "，").Replace("(", "（").Replace(")", "）").Replace("\n", string.Empty).Replace("\r", string.Empty).Replace("--", "－－").Replace("-", "－");
            return str;
        }
    }
}
