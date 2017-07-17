using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.WebHost;
using System.Web.Routing;

namespace mq.ui.employeebg
{
    public class WebApiSessionRouteHandler:HttpControllerRouteHandler
    {
        protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new WebApiSessionControllerHandler(requestContext.RouteData);
        }     
    }
}