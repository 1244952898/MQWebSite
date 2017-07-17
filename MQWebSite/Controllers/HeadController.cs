using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MQWebSite.Controllers
{
    public class HeadController : Controller
    {
        // GET: Head
        public ActionResult Head()
        {
            ViewBag.isHead = true;
            return View();
        }
    }
}