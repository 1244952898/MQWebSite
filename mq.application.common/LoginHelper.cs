using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using mq.application.common;
using mq.model.dbentity;

namespace koala.application.common
{
    public class LoginHelper
    {
        private static string SessionDomain = CommonHelper.GetConfigValue("SessionDomain");
        
        /// <summary>
        /// 机构Id
        /// </summary>
        public static string OrgId
        {
            get { return CookieHelper.GetCookie(CommonVariables.LoginCookiebgUserID); }
        }
        /// <summary>
        /// Mysql数据库中的ID
        /// </summary>
        public static long UserId
        {
            get { return 1L; }
            //get { return CookieHelper.GetCookie(CommonVariables.LoginCookiebgUserID).ToLong(0); }
        }
    
        /// <summary>
        /// 账号
        /// </summary>
        public static string UserName
        {
            get { return CookieHelper.GetCookie(CommonVariables.LoginCookiebgUserName); }
        }
       
        /// <summary>
        /// 真实姓名
        /// </summary>
        public static string UserRealName
        {
            get { return CookieHelper.GetCookie(CommonVariables.LoginCookiebgUserRealName); }
        }
    

        public static bool isOnline()
        {
            //if (HttpContext.Current.Session != null && HttpContext.Current.Session["UserToken"] != null) {
            //    return true;
            //}
            //!string.IsNullOrEmpty(LoginHelper.UID) &&
            if ( !string.IsNullOrEmpty(LoginHelper.UserName) && LoginHelper.UserId > 0)
                return true;
            return false;
        }

        public static void CallBackUrl(HttpRequest mRequest, string LoginUrl, string AppCode)
        {
            if (mRequest.QueryString.HasKeys() && (mRequest.QueryString.AllKeys.Contains("token") || mRequest.QueryString.AllKeys.Contains("app")))
            {
                StringBuilder querystring = new StringBuilder();
                string host = string.Format("http://{0}{1}", mRequest.Url.Authority, mRequest.Url.AbsolutePath);
                string[] keys = mRequest.QueryString.AllKeys;
                for (int i = 0; i < keys.Length; i++)
                {
                    if (keys[i].ToLower().Equals("token") || keys[i].ToLower().Equals("app"))
                    {
                        continue;
                    }
                    querystring.AppendFormat("{0}={1}{2}", keys[i],HttpContext.Current.Server.UrlEncode(mRequest.QueryString[keys[i]]), i < (keys.Length - 1) ? "&" : "");
                }
                HttpContext.Current.Response.Redirect(string.Format("{0}?url={1}&app={2}", LoginUrl, HttpContext.Current.Server.UrlEncode(string.Format("{0}?{1}", host, querystring)), AppCode.ToLower()));
            }
            else
            {//页面跳转到 登录页面
                HttpContext.Current.Response.Redirect(string.Format("{0}?url={1}&app={2}", LoginUrl, HttpContext.Current.Server.UrlEncode(mRequest.Url.ToString()), AppCode.ToLower()));
            }
        }

        public static string GetMaxMindOmniData(string IP)
        {
            System.Uri objUrl = new System.Uri("http://geoip.maxmind.com/e?l=YOUR_LICENSE_KEY&i=" + IP);
            System.Net.WebRequest objWebReq;
            System.Net.WebResponse objResp;
            System.IO.StreamReader sReader;
            string strReturn = string.Empty;

            try
            {
                objWebReq = System.Net.WebRequest.Create(objUrl);
                objResp = objWebReq.GetResponse();

                sReader = new System.IO.StreamReader(objResp.GetResponseStream());
                strReturn = sReader.ReadToEnd();

                sReader.Close();
                objResp.Close();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                objWebReq = null;
            }

            return strReturn;
        }

        /// <summary>
        /// 登陆后台设置Cookie
        /// </summary>
        /// <param name="bgUser"></param>
        public static void SetBgUserCookie(T_BG_User bgUser)
        {
            CookieHelper.SetSessionCookie(SessionDomain, CommonVariables.LoginCookiebgUserID, bgUser.ID.ToString());
            CookieHelper.SetSessionCookie(SessionDomain, CommonVariables.LoginCookiebgUserName, bgUser.Name);
            CookieHelper.SetSessionCookie(SessionDomain, CommonVariables.LoginCookiebgUserRealName, bgUser.RealName);
            CookieHelper.SetSessionCookie(SessionDomain, CommonVariables.LoginCookiebgUserEmail, bgUser.Email);
            CookieHelper.SetSessionCookie(SessionDomain, CommonVariables.LoginCookiebgUserRoleID, bgUser.RoleID.ToString());
            CookieHelper.SetSessionCookie(SessionDomain, CommonVariables.LoginCookiebgUserShopID, bgUser.ShopID.ToString());
        }

        public static void DelBgUserCookie()
        {
            CookieHelper.DelSessionCookie(SessionDomain, CommonVariables.LoginCookiebgUserID);
            CookieHelper.DelSessionCookie(SessionDomain, CommonVariables.LoginCookiebgUserID);
            CookieHelper.DelSessionCookie(SessionDomain, CommonVariables.LoginCookiebgUserName);
            CookieHelper.DelSessionCookie(SessionDomain, CommonVariables.LoginCookiebgUserRealName);
            CookieHelper.DelSessionCookie(SessionDomain, CommonVariables.LoginCookiebgUserEmail);
            CookieHelper.DelSessionCookie(SessionDomain, CommonVariables.LoginCookiebgUserRoleID);
            CookieHelper.DelSessionCookie(SessionDomain, CommonVariables.LoginCookiebgUserShopID);

            CookieHelper.Del(CommonVariables.LoginCookiebgUserID);
            CookieHelper.Del(CommonVariables.LoginCookiebgUserID);
            CookieHelper.Del(CommonVariables.LoginCookiebgUserID);
            CookieHelper.Del(CommonVariables.LoginCookiebgUserName);
            CookieHelper.Del(CommonVariables.LoginCookiebgUserRealName);
            CookieHelper.Del(CommonVariables.LoginCookiebgUserEmail);
            CookieHelper.Del(CommonVariables.LoginCookiebgUserRoleID);
            CookieHelper.Del(CommonVariables.LoginCookiebgUserShopID);
            if (HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session.Clear();
            }
        }

    }
}
