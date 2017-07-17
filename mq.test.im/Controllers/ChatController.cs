using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mq.application.service.Implement;
using mq.application.service.Interface;

namespace mq.test.im.Controllers
{
    public class ChatController : Controller
    {
        //IBgUserService _bgUserService=new BgUserService();
        // GET: Chat
        public ActionResult Users()
        {
            return View();
        }
    }
}