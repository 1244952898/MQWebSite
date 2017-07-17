using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MQWebSite.App_Start;

namespace MQWebSite
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //独立的log4net.config
            //log4net.Config.XmlConfigurator.Configure(new FileInfo("log4net.config"));
            log4net.Config.XmlConfigurator.Configure();

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            //AutoFac初始化数据仓储层Repository.dll中所有类的对象实例。这些对象实例以其接口的形式保存在AutoFac容器中 
            //AutoFac初始化业务逻辑层Services.dll中所有类的对象实例。这些对象实例以其接口的形式保存在AutoFac容器中 
            //控制器工厂替换成AutoFac的工厂
            AutofacConfig.Register();
        }


    }
}
