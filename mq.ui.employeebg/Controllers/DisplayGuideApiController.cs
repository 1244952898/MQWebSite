using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using koala.application.common;
using log4net;
using mq.application.common;
using mq.application.enumlib;
using mq.application.service;
using mq.application.service.Interface;
using mq.model.dbentity;
using mq.model.viewentity;

namespace mq.ui.employeebg.Controllers
{
    public class DisplayGuideApiController : ApiController
    {
        ILog logger = log4net.LogManager.GetLogger(typeof(DisplayGuideApiController));
        private readonly IBgUserService _bgUserService;
        private readonly IBgUpFilesService _bgUpFilesService;

        public DisplayGuideApiController(IBgUserService bgUserService, IBgUpFilesService bgUpFilesService)
        {
            _bgUserService = bgUserService;
            _bgUpFilesService = bgUpFilesService;
        }

    }
}
