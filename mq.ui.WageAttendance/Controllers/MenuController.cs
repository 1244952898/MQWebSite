using System.Web;
using System.Web.Mvc;
using koala.application.common;
using mq.application.common;
using mq.application.service;

namespace mq.ui.Email.Controllers
{
    public class MenuController : Controller
    {
        private readonly IBgMenuService _bgMenuService;
        public MenuController(IBgMenuService bgMenuService)
        {
            _bgMenuService = bgMenuService;
        }

        public ActionResult LeftMenuLayoutLayUI()
        {
            int channelId = CommonHelper.GetPostValue("cid").ToInt(0);
            ViewBag.ChannelId = channelId;
            long userId = LoginHelper.UserId;
            var menuList = _bgMenuService.GetBgMenuByUserId(userId);
            return PartialView(menuList);
        }

        public ActionResult Error()
        {
            string errorCode = CommonHelper.GetPostValue("ErrorCode");
            string errorMessage = CommonHelper.GetPostValue("ErrorMsg");
            errorMessage = HttpUtility.UrlDecode(errorMessage);
            ViewBag.errorCode = errorCode;
            ViewBag.errorMessage = errorMessage;
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}