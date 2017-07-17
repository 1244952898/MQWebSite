using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using koala.application.common;
using mq.application.common;
using mq.application.webmvc;

namespace MQWebSite
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (Request.Url != null)
            {
                string url = Request.Url.ToString().ToLower();
                //string controller = ((string)RouteData.Values["controller"]).ToLower();
                //string action = ((string)RouteData.Values["action"]).ToLower();
                if (CommonHelper.ExcludeUrl(url))
                    return;
                string strUserName = LoginHelper.UserName;
                if (string.IsNullOrEmpty(strUserName))
                {
                    filterContext.Result = new RedirectResult("Test/index", true);
                    return;
                }
            }

        }
    }
}