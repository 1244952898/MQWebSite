using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mq.ui.publicmessage.Controllers
{
    public class TestController : Controller
    {
        public ActionResult A()
        {
            return View();
        }

        // GET: Share
        public ActionResult Menu()
        {
            return View("~/Views/Share/_BootStrapLayout.cshtml");
        }
    }
}