﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mq.test.im.Controllers
{
    public class ChatController : Controller
    {
        // GET: Chat
        public ActionResult Users()
        {
            return View();
        }
    }
}