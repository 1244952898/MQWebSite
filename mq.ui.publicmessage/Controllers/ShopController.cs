using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mq.application.common;
using mq.application.service;
using mq.model.dbentity;

namespace mq.ui.publicmessage.Controllers
{
    public class ShopController : Controller
    {
        private readonly IBgVShopAreaUserService _bgVShopAreaUserService;

        public ShopController(IBgVShopAreaUserService bgVShopAreaUserService)
        {  
            _bgVShopAreaUserService = bgVShopAreaUserService;
        }

        // GET: Shop
        public ActionResult List()
        {
          
            return View();
        }

        public ActionResult ListPartial()
        {
            string likeWWords = CommonHelper.GetPostValue("likeWWords");
            List<V_Shop_Area_User> list = _bgVShopAreaUserService.PublicList(likeWWords);
            return PartialView(list);
        }
    }
}