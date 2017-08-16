using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace mq.ui.Email
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

        }
        //public static void Register(HttpConfiguration config)
        //{
        //    config.MapHttpAttributeRoutes();
        //    config.Routes.MapHttpRoute(
        //        name: "push.api.v1",
        //        routeTemplate: "v1/{controller}/{id}",
        //        defaults: new { id = RouteParameter.Optional }
        //    );

        //    config.Filters.Add(new PushFilter());
        //}
    }
}
