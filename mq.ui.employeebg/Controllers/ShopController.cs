using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mq.application.common;
using mq.application.service;
using mq.application.service.Interface;
using mq.model.dbentity;
using mq.model.viewentity;

namespace mq.ui.employeebg.Controllers
{
    public class ShopController : Controller
    {
        private IBgAreaService _areaService;
        private IBgShopService _bgShopService;
        private IBgUserService _bgUserService;
        public ShopController(IBgAreaService areaService, IBgShopService bgShopService, IBgUserService bgUserService)
        {
            _areaService = areaService;
            _bgShopService = bgShopService;
            _bgUserService = bgUserService;
        }

        // GET: Shop

        public ActionResult List()
        {
            return View();
        }

        public ActionResult ListPartial()
        {
            long areaId = CommonHelper.GetPostValue("areaId").ToLong(-1);
            List<T_BG_Shop> list = _bgShopService.List(areaId);
            return PartialView(list);
        }

        public ActionResult Add()
        {
            ShopAddEntity entity = new ShopAddEntity();
            entity.areaList = _areaService.List();
            entity.userList = _bgUserService.GetList();
            return View(entity);
        }
    }
}