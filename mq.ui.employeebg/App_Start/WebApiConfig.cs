using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Routing;

namespace mq.ui.employeebg
{
    public static class WebApiConfig
    {

        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
               name: "DefaultApi",
               routeTemplate: "api/{controller}/{action}/{id}",
               defaults: new { id = RouteParameter.Optional }
           );
           // var route = RouteTable.Routes.MapHttpRoute(
           //    name: "DefaultApi2",
           //    routeTemplate: "api/{controller}/{action}/{id}",
           //    defaults: new { id = RouteParameter.Optional }
           //);

           // var route2 = RouteTable.Routes.MapHttpRoute(
           //     name: "DefaultApi",
           //     routeTemplate: "api/{controller}/{id}",
           //     defaults: new { id = RouteParameter.Optional }
           // );

           // route.RouteHandler = new WebApiSessionRouteHandler();
           // route2.RouteHandler = new WebApiSessionRouteHandler();
        }
    }
}
