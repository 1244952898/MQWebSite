using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using mq.application.common;

namespace mq.ui.WageAttendance
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // 在应用程序启动时运行的代码
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //独立的log4net.config
            //log4net.Config.XmlConfigurator.Configure(new FileInfo("log4net.config"));
            log4net.Config.XmlConfigurator.Configure();
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //AutoFac初始化数据仓储层Repository.dll中所有类的对象实例。这些对象实例以其接口的形式保存在AutoFac容器中 
            //AutoFac初始化业务逻辑层Services.dll中所有类的对象实例。这些对象实例以其接口的形式保存在AutoFac容器中 
            //控制器工厂替换成AutoFac的工厂
            AutoFacConfig.Register();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //过滤非法请求或攻击
            if (HttpContext.Current.Request.UserAgent != null && (HttpContext.Current.Request.UserAgent.ToLower().Contains("spider") || HttpContext.Current.Request.UserAgent.ToLower().Contains("yahoo")) && (HttpContext.Current.Request.Url.ToString().ToLower().Contains("/web/searchcollection") || HttpContext.Current.Request.Url.ToString().ToLower().Contains("/web/searcharticle"))) //Web/SearchArticle
            {
                Response.Clear();
                Response.StatusCode = 404;
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                Response.AddHeader("Content-Type", "text/html; charset=utf-8");
                Response.Write("别抓了");
                Response.End();
            }


            //对无需登录页面返回
            if (CommonHelper.ExcludeUrl(Request.Url.ToString().ToLower()))
                return;

            if (LoginHelper.isOnline())
            {
            }
            else
            {
                if (Request.AcceptTypes != null && Request.AcceptTypes.Any(t => t.Contains("application/json")))
                {
                    Response.Clear();
                    Response.ContentEncoding = System.Text.Encoding.UTF8;
                    Response.AddHeader("Content-Type", "application/json; charset=utf-8");
                    Response.Write("{\"ErrorCode\":\"E1001\",\"ErrorMesssage\":\"登录超时\"}");
                    Response.End();
                }
                else
                {
                    string url = DomainUrlHelper.PublicMessagePath + "/Login/Login";
                    //UrlHelper.GenerateUrl("Login", "login", "home", null, RouteTable.Routes, Request.RequestContext, true)
                    Response.Redirect(url);
                }
            }
        }

    }
}