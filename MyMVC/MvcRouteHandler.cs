using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyMVC
{
    public class MvcRouteHandler : IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new MvcHandler(requestContext);
        }
    }
}