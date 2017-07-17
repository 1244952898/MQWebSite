using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace koala.application.common
{
    public class CommonVariables
    {

        //public static string LoginSessionSID { get { return "Login_Session_UserSID"; } }
        //public static string LoginCookieSID { get { return "Login_Cookies_UserSID"; } }
        //public static string LoginCookieName { get { return "Login_Cookies_UserName"; } }
        //public static string LoginCookieNickName { get { return "Login_Cookies_NickName"; } }
        //public static string LoginCookieUID { get { return "Login_Cookies_Userid"; } }
        //public static string NotLoginCookieUserGUID { get { return "NotLoginCookieUserGUID"; } }
        //public static string LoginCookieUserType { get { return "Login_Cookies_UserType"; } }


        public static string LoginCookiebgUserID { get { return "Login_Cookies_bgUserID"; } }
        public static string LoginCookiebgUserRealName { get { return "Login_Cookies_bgUserRealName"; } }
        public static string LoginCookiebgUserName { get { return "Login_Cookies_bgUserName"; } }
        public static string LoginCookiebgUserEmail { get { return "Login_Cookies_bgUserEmail"; } }
        public static string LoginCookiebgUserRoleID { get { return "Login_Cookies_bgUserRoleID"; } }
        public static string LoginCookiebgUserShopID { get { return "Login_Cookies_bgUserRoleShopID"; } }

        /// <summary>
        /// 无需登录验证的页面
        /// </summary>
        public static string[] ExcludePaths
        {
            get
            {
                return new string[] { "/api/", "/404", "/error", "home/login", "test" };
            }
        }

        public static string[] ExcludeFilterPaths { get { return new string[] { "/api/material/add", "/api/material/batchadd" }; } }

        private static bool _Debug = false;
        public static bool Debug
        {
            get
            {
#if DEBUG
                _Debug = true;
#endif
                return _Debug;
            }
        }
    }
}
