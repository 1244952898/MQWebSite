using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using mq.application.common;

namespace koala.application.common
{

    public struct CookieKeyValue
    {
        string key;
        string value;
        public string Key
        {
            get { return this.key; }
            set { this.key = value; }
        }
        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public CookieKeyValue(string key, string value)
        {
            this.key = key;
            this.value = value;
        }
    }
    public enum TimeUtil
    {
        /// <summary>
        /// 以年为过期单位
        /// </summary>
        Y = 0,
        /// <summary>
        /// 以月为过期单位
        /// </summary>
        M = 1,
        /// <summary>
        /// 以天为过期单位
        /// </summary>
        D = 2,
        /// <summary>
        /// 以小时为过期单位
        /// </summary>
        H = 3,
        /// <summary>
        /// 以分钟为过期单位
        /// </summary>
        mi = 4,
        /// <summary>
        /// 以秒为过期单位
        /// </summary>
        s = 5,
        /// <summary>
        /// 以默认一个月为过期单位
        /// </summary>
        None
    }
    /// <summary>
    /// Cookie操作类
    /// </summary>
    public class CookieHelper
    {
        public readonly static string strCookiesDomain = CommonHelper.GetConfigValue("CookiesDoamin");
        public readonly static string strDomain = System.Configuration.ConfigurationManager.AppSettings["StrDomain"] != null ? System.Configuration.ConfigurationManager.AppSettings["StrDomain"] : "";
        public static void SetCookie(string strCookieName, int iExpires, CookieKeyValue[] kvArray)
        {
            HttpCookie objCookie = new HttpCookie(strCookieName.Trim());
            int len = kvArray.Length;
            if (len > 0)
            {
                for (int i = 0; i < len; i++)
                {
                    objCookie[kvArray[i].Key] = System.Web.HttpUtility.UrlEncode(kvArray[i].Value.Trim());
                }
            }
            if (!string.IsNullOrEmpty(strDomain))
            {
                objCookie.Domain = strDomain.Trim();
            }
            if (iExpires > 0)
            {
                if (iExpires == 1)
                {
                    objCookie.Expires = DateTime.MaxValue;
                }
                else
                {
                    objCookie.Expires = DateTime.Now.AddHours(iExpires);
                }
            }
            HttpContext.Current.Response.Cookies.Add(objCookie);
        }


        public static void DelWithCurrentDomain(string strCookieName)
        {
            HttpCookie objCookie = new HttpCookie(strCookieName.Trim());
            objCookie.Domain = HttpContext.Current.Request.Url.Host; 
            objCookie.Expires = DateTime.Now.AddYears(-5);
            HttpContext.Current.Response.Cookies.Add(objCookie);
        }

        /*****************Modify By Mr.Yao 2014-11-28***********************/
        public static void SetSessionCookie(string _domain,string _name, string _value)
        {
            _value = System.Web.HttpUtility.UrlEncode(_value);
            HttpCookie _cookie = new HttpCookie(_name);
            _cookie.Value = _value;
            _cookie.Domain = _domain;
            _cookie.Expires = GetExpireTime(TimeUtil.None, string.Empty);
            _cookie.HttpOnly = false;
            if (!string.IsNullOrEmpty(string.Empty))
                _cookie.Path = string.Empty;
            _cookie.Secure = false;
            HttpContext.Current.Response.Cookies.Add(_cookie);
        }
        public static void DelSessionCookie(string _domain, string _name)
        {
            HttpCookie objCookie = new HttpCookie(_name);
            objCookie.Domain = _domain;
            objCookie.Expires = DateTime.Now.AddYears(-5);
            HttpContext.Current.Response.Cookies.Add(objCookie);
        }
        /*****************Modify By Mr.Yao 2014-11-28***********************/

        public static void Del(string strCookieName)
        {
            HttpCookie objCookie = new HttpCookie(strCookieName.Trim());
            if (!string.IsNullOrEmpty(strDomain))
            {
                objCookie.Domain = strDomain;
            }
            objCookie.Expires = DateTime.Now.AddYears(-5);
            HttpContext.Current.Response.Cookies.Add(objCookie);
        }

        public static string GetCookie(string strCookieName, string strKeyName)
        {
            if (HttpContext.Current.Request.Cookies[strCookieName] == null)
            {
                return "";
            }
            else
            {
                string strObjValue = HttpContext.Current.Request.Cookies[strCookieName].Value;
                string strKeyName2 = strKeyName + "=";
                if (strObjValue.IndexOf(strKeyName2) == -1)
                {
                    return "";
                }
                else
                {
                    return System.Web.HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies[strCookieName][strKeyName]);
                }
            }
        }

        public static string GetCookie(string _name, bool check = true)
        {
            //if (!check)
            //{
                if (HttpContext.Current.Request.Cookies[_name] != null && !string.IsNullOrEmpty(HttpContext.Current.Request.Cookies[_name].Value))
                {
                    return System.Web.HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies[_name].Value);
                }
                else
                    return string.Empty;
            //}
            //else
            //{
            //    if (HttpContext.Current.Request.Cookies != null)
            //    {
            //        HttpCookieCollection collection = HttpContext.Current.Request.Cookies;
            //        for (int i = 0; i < collection.AllKeys.Count(); i++)
            //        {
            //            HttpCookie cookie = collection[i];
            //            if (cookie.Name == _name && cookie.Domain.ToLower() == strCookiesDomain.ToLower())
            //                return System.Web.HttpUtility.UrlDecode(cookie.Value);
            //        }
            //        return string.Empty;
            //    }
            //    else
            //        return string.Empty;
            //}
        }

        /// <summary>
        /// 设置客户端Cookie值
        /// </summary>
        /// <param name="_name">Cookie名称</param>
        /// <param name="_value">Cookie值</param>
        public static void SetCookie(string _name, string _value)
        {
            SetCookie(_name, _value, TimeUtil.None, string.Empty, string.Empty, false, string.Empty, false);
        }
        /// <summary>
        /// 设置客户端Cookie值
        /// </summary>
        /// <param name="_name">Cookie名称</param>
        /// <param name="_value">Cookie值</param>
        /// <param name="_expireTimeUtil">过期时间单位</param>
        /// <param name="_expireTimeSpan">过期时间间隔</param>
        public static void SetCookie(string _name, string _value, TimeUtil _expireTimeUtil, string _expireTimeSpan)
        {
            SetCookie(_name, _value, _expireTimeUtil, _expireTimeSpan, string.Empty, false, string.Empty, false);
        }
        /// <summary>
        /// 设置客户端Cookie值
        /// </summary>
        /// <param name="_name">Cookie名称</param>
        /// <param name="_value">Cookie值</param>
        /// <param name="_expireTimeUtil">过期时间单位</param>
        /// <param name="_expireTimeSpan">过期时间间隔</param>
        /// <param name="_domain">设置将此 Cookie 与其关联的域</param>
        /// <param name="_httpOnly">指定 Cookie 是否可通过客户端脚本访问，默认为false</param>
        /// <param name="_path">要与当前 Cookie 一起传输的虚拟路径,默认值为当前请求的路径</param>
        /// <param name="_secure">指示是否使用安全套接字层 (SSL)（即仅通过 HTTPS）传输 Cookie，默认为false</param>
        public static void SetCookie(string _name, string _value, TimeUtil _expireTimeUtil, string _expireTimeSpan, string _domain, bool _httpOnly, string _path, bool _secure)
        {
            SetCookie(_name, _value, GetExpireTime(_expireTimeUtil, _expireTimeSpan), _domain, _httpOnly, _path, _secure);
        }

        /// <summary>
        /// 设置客户端Cookie值
        /// </summary>
        /// <param name="_name">Cookie名称</param>
        /// <param name="_value">Cookie值</param>
        /// <param name="_expireTime">过期时间</param>
        /// <param name="_domain">设置将此 Cookie 与其关联的域</param>
        /// <param name="_httpOnly">指定 Cookie 是否可通过客户端脚本访问，默认为false</param>
        /// <param name="_path">要与当前 Cookie 一起传输的虚拟路径,默认值为当前请求的路径</param>
        /// <param name="_secure">指示是否使用安全套接字层 (SSL)（即仅通过 HTTPS）传输 Cookie，默认为false</param>
        public static void SetCookie(string _name, string _value, DateTime _expireTime, string _domain, bool _httpOnly, string _path, bool _secure)
        {
            _value = System.Web.HttpUtility.UrlEncode(_value);
            HttpCookie _cookie = new HttpCookie(_name);
            _cookie.Value = _value;
            if (!string.IsNullOrEmpty(_domain))
                _cookie.Domain = _domain;
            _cookie.Expires = _expireTime;
            _cookie.HttpOnly = _httpOnly;
            if (!string.IsNullOrEmpty(_path))
                _cookie.Path = _path;
            _cookie.Secure = _secure;
            HttpContext.Current.Response.Cookies.Add(_cookie);
        }
        /// <summary>
        /// 根据时间单位和过期时长返回过期日期
        /// </summary>
        /// <param name="_expireTimeUtil">过期时间单位</param>
        /// <param name="_expireTimeSpan">过期时间间隔，_expireTimeSpan为正整数</param>
        /// <returns></returns>
        public static DateTime GetExpireTime(TimeUtil _expireTimeUtil, string _expireTimeSpan)
        {
            DateTime _dateTime = DateTime.Now.AddMonths(1);
            if (!string.IsNullOrEmpty(_expireTimeSpan))
            {
                switch (_expireTimeUtil)
                {
                    case TimeUtil.Y:
                        _dateTime = DateTime.Now.AddYears(int.Parse(_expireTimeSpan));
                        break;
                    case TimeUtil.M:
                        _dateTime = DateTime.Now.AddMonths(int.Parse(_expireTimeSpan));
                        break;
                    case TimeUtil.D:
                        _dateTime = DateTime.Now.AddDays(int.Parse(_expireTimeSpan));
                        break;
                    case TimeUtil.H:
                        _dateTime = DateTime.Now.AddHours(int.Parse(_expireTimeSpan));
                        break;
                    case TimeUtil.mi:
                        _dateTime = DateTime.Now.AddMinutes(int.Parse(_expireTimeSpan));
                        break;
                    case TimeUtil.s:
                        _dateTime = DateTime.Now.AddSeconds(int.Parse(_expireTimeSpan));
                        break;
                    case TimeUtil.None:
                    default:
                        _dateTime = DateTime.Now.AddMonths(1);
                        break;
                }
            }
            return _dateTime;
        }

        public static string GetLogCookieID()
        {
            string cookielogid = GetCookie("c_lid",false);
            if (string.IsNullOrEmpty(cookielogid))
            {
                Guid tmpid = Guid.NewGuid();
                cookielogid = tmpid.ToString();
                SetCookie("c_lid", cookielogid);
            }
            return cookielogid;
        }

        public static string GetNoLoginVoteId()
        { 
            string cookielogid = GetCookie("zhikuvotecookiesid", false);
            if (string.IsNullOrEmpty(cookielogid))
            {
                Guid tmpid = Guid.NewGuid();
                cookielogid = tmpid.ToString();
                SetCookie("zhikuvotecookiesid", cookielogid);
            }
            return cookielogid;
        }
    }
}
