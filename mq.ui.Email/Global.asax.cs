using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using System.Web.Optimization;

namespace mq.ui.Email
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
            AutofacConfig.Register();
        }
    }
}