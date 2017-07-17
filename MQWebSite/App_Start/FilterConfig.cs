using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MQWebSite.Utils;

namespace MQWebSite
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new LoginAuthorizeAttribute());
            filters.Add(new HandleErrorAttribute());
        }
    }
}