using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using match.application.common;
using mq.application.service;
using mq.model.dbentity;

namespace MQWebSite.Controllers
{
    public class TestController : BaseController
    {
        ILog logger = LogManager.GetLogger(typeof(TestController));
        private readonly IUserService _userService;
        public TestController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: Test
        public ActionResult Index()
        {
            logger.ErrorFormat("具体位置="+DateTime.Now + ": login success  \n\r");
            //logger.ErrorFormat("具体位置={0},Message={1},StackTrace={2},Source={3},重要参数paperGUID={4}", "PaperSettingController-RandomPaperSetting", ex.Message, ex.StackTrace, ex.Source, paperGUID);
            T_User user = _userService.Gt("1");
            ErrorInfoHelper.GetErrorValue("E0009");
           // T_User user = new T_User { Code = "d", Name ="2"};
            return View(user);
        }
    }
}