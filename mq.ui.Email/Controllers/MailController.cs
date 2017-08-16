using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mq.ui.Email.Controllers
{
    public class MailController : Controller
    {
        // GET: Mail
        public ActionResult Send()
        {
            return View();
        }
    }
}