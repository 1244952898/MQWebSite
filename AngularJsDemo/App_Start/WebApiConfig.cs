using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace AngularJsDemo
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { controller = "Demo", action = "B", id = RouteParameter.Optional }
            );
        }
    }
}
