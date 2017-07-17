using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace 网络简单登录破解1.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(User user)
        {
            if (user.UserName == "11" && user.Password == "11")
                return View(user);
            else
                return RedirectToAction("Login");
        }

        public ActionResult About()
        {
            return View();
        }
    }
    public class User
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}