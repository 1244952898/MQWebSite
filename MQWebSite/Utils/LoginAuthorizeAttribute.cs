using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using System.Web.Routing;
using koala.application.common;
using mq.application.webmvc;
using AuthorizeAttribute = System.Web.Http.AuthorizeAttribute;
using RedirectResult = System.Web.Http.Results.RedirectResult;

namespace MQWebSite.Utils
{
    public class LoginAuthorizeAttribute:AuthorizeAttribute
    {
        //protected override bool AuthorizeCore(HttpContextBase httpContext)
        //{
        //    return true;
        //    //return base.AuthorizeCore(httpContext);
        //}

        //public override void OnAuthorization(AuthorizationContext filterContext)
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            RouteData rd = RouteTable.Routes.GetRouteData((HttpContextBase)new HttpContextWrapper(HttpContext.Current));
            string c = rd.GetRequiredString("controller");
            string a = rd.GetRequiredString("action");
            if ((c.ToLower() == "images" && a.ToLower() == "uploadimage") || (c.ToLower() == "excelupload" && a.ToLower() == "importquestion"))
            {
                return;
            }
            //获取Cookies中的UserName
            var userName = LoginHelper.UserName;
            if (string.IsNullOrEmpty(userName))
            {
                //页面跳转到 登录页面
                //filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Member", action = "Index" }));
                string returnUrl = string.Format("{0}?retUrl={1}", DomainUrlHelper.MqWebSiteBg, HttpContext.Current.Request.Url.AbsoluteUri);
               // actionContext.Request.RequestUri
                return;
            }
            //var sessionUser = System.Web.HttpContext.Current.Session["userTemp"] as InstitutionLayoutViewEntity;
            //if (sessionUser != null)
            //{
            //    if(sessionUser.UserType==2 && sessionUser.IsInstituAuthentication==0)
            //    {
            //        string returnUrl = string.Format("{0}?retUrl={1}", DomainUrlHelper.LoginAction, HttpContext.Current.Request.Url.AbsoluteUri);
            //        filterContext.Result = new RedirectResult(returnUrl);
            //        return;
            //    }
            //}
            //通过验证
            return;
            //base.OnAuthorization(filterContext);//此语句可以导致AuthorizeCore方法的调用
        }
    }
}