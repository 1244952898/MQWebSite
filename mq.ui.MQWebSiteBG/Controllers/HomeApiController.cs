using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using log4net;
using match.application.common;
using match.ui.institutioncenter;
using mq.application.common;
using mq.application.service.Interface;
using mq.model.viewentity;

namespace mq.ui.EmployeeWebSite.Controllers
{
    public class HomeApiController : MPApiController
    {
        ILog logger = LogManager.GetLogger(typeof(HomeApiController));
        private readonly IBgUserService _bgUserService;

        public HomeApiController(IBgUserService bgUserService)
        {
            _bgUserService = bgUserService;
            base.AddDisposableObject(this._bgUserService);
        }

    }
}
