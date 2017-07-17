using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http.WebHost;
using System.Web.SessionState;
using System.Web.Routing;

namespace mq.ui.employeebg
{
    public class WebApiSessionControllerHandler : HttpControllerHandler, IRequiresSessionState
    {
        public WebApiSessionControllerHandler(RouteData routeData) : base(routeData) { }
    }
}
