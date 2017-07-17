using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace mq.application.common
{
    public class IPHelper
    {
        public static string GetLoginIp(HttpRequest Request = null)
        {
            if (Request == null)
                Request = System.Web.HttpContext.Current.Request;
            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (!string.IsNullOrEmpty(ip))
            {
                string[] t = ip.Split(',');
                if (t.Length > 1 && !string.IsNullOrEmpty(t[t.Length - 1]))
                    ip = t[t.Length - 1];
            }

            if (string.IsNullOrEmpty(ip))
                ip = Request.ServerVariables["REMOTE_ADDR"];

            if (string.IsNullOrEmpty(ip))
                ip = Request.UserHostAddress;

            if (ip == "::1")
            {
                ip = "127.0.0.1";
            }
            return ip.Trim();
        }
    }
}
