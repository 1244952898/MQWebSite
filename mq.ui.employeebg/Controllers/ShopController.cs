using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net.Core;
using mq.application.common;
using mq.application.service;
using mq.application.service.Interface;
using mq.model.dbentity;
using mq.model.viewentity;
using mq.model.viewentity.employeebg;

namespace mq.ui.employeebg.Controllers
{
    public class ShopController : Controller
    {
        private IBgAreaService _areaService;
        private IBgShopService _bgShopService;
        private IBgUserService _bgUserService;
        private IBgVShopAreaUserService _bgVShopAreaUserService;
        public ShopController(IBgAreaService areaService, IBgShopService bgShopService, IBgUserService bgUserService, IBgVShopAreaUserService bgVShopAreaUserService)
        {
            _areaService = areaService;
            _bgShopService = bgShopService;
            _bgUserService = bgUserService;
            _bgVShopAreaUserService = bgVShopAreaUserService;
        }

        // GET: Shop

        public ActionResult List()
        {
            ShopAddEntity entity = new ShopAddEntity();
            entity.areaList = _areaService.List();
            entity.userList = _bgUserService.GetList();
            return View(entity);
        }

        public ActionResult ListPartial()
        {
            long areaId = CommonHelper.GetPostValue("areaId").ToLong(-1);
            List<V_Shop_Area_User> list = _bgVShopAreaUserService.List(areaId);
            return PartialView(list);
        }

        public ActionResult Add()
        {
            ShopAddEntity entity = new ShopAddEntity();
            entity.areaList = _areaService.List();
            entity.userList = _bgUserService.GetList();
            return View(entity);
        }

        public JsonResult AddShop()
        {
            JsonShopAddShopEntity json = new JsonShopAddShopEntity();
            string name = CommonHelper.GetPostValue("shopname");
            name = HttpUtility.UrlDecode(name);
            string address = CommonHelper.GetPostValue("address");
            address = HttpUtility.UrlDecode(address);
            string tel = CommonHelper.GetPostValue("tel");
            int leaderId = CommonHelper.GetPostValue("leaderId").ToInt(1);
            long areas = CommonHelper.GetPostValue("areas").ToLong(-1L);
            long areaId = CommonHelper.GetPostValue("areaId").ToLong(-1L);
            DateTime openDate = CommonHelper.GetPostValue("openDate").ToDateTime(DateTime.Now);
            if (string.IsNullOrEmpty(name) || leaderId < 0 || areaId < 0)
            {
                json.ErrorCode = "E001";
                json.ErrorMessage = "参数不全！";
                return Json(json);
            }
            T_BG_Shop shop = new T_BG_Shop();
            shop.Name = name;
            shop.Address = address;
            shop.Tel = tel;
            shop.LeaderID = leaderId;
            shop.Areas = areas;
            shop.AreaId = areaId;
            shop.OpenDate = openDate;
            shop.State = 0;
            bool result = _bgShopService.Add(shop);
            if (result)
            {
                json.ErrorCode = "E000";
                json.ErrorMessage = "添加成功！";
            }
            else
            {
                json.ErrorCode = "E002";
                json.ErrorMessage = "添加失败！";
            }
            return Json(json);
        }

        public JsonResult GetShop()
        {
            long id = CommonHelper.GetPostValue("id").ToLong(-1L);
            JsonShopGetShopEntity json = new JsonShopGetShopEntity();
            if (id <= 0)
            {
                json.ErrorCode = "E001";
                json.ErrorMessage = "参数错误！";
                return Json(json);
            }
            json.Shop = _bgShopService.Get(id);
            if (json.Shop == null)
            {
                json.ErrorCode = "E002";
                json.ErrorMessage = "未获得该店铺！";
                return Json(json);
            }
            json.OpenDate = json.Shop.OpenDate.ToString("yyyy/MM/dd hh:mm:ss");
            json.ErrorCode = "E000";
            json.ErrorMessage = "获得成功！";
            return Json(json);
        }

        public JsonResult DelShop()
        {
            JsonShopDelShopEntity json = new JsonShopDelShopEntity();
            long id = CommonHelper.GetPostValue("id").ToLong(-1L);
            if (id <= 0)
            {
                json.ErrorCode = "E001";
                json.ErrorMessage = "参数错误！";
                return Json(json);
            }
            T_BG_Shop shop = _bgShopService.Get(id);
            if (shop == null)
            {
                json.ErrorCode = "E000";
                json.ErrorMessage = "未获得该店铺！";
                return Json(json);
            }
            bool result = _bgShopService.Del(shop);
            if (result)
            {
                json.ErrorCode = "E000";
                json.ErrorMessage = "删除成功！";
            }
            else
            {
                json.ErrorCode = "E002";
                json.ErrorMessage = "删除失败！";
            }
            return Json(json);
        }

        public JsonResult ModifyShop()
        {
            JsonShopAddShopEntity json = new JsonShopAddShopEntity();
            long id = CommonHelper.GetPostValue("id").ToLong(-1L);
            string name = CommonHelper.GetPostValue("shopname");
            name = HttpUtility.UrlDecode(name);
            string address = CommonHelper.GetPostValue("address");
            address = HttpUtility.UrlDecode(address);
            string tel = CommonHelper.GetPostValue("tel");
            int leaderId = CommonHelper.GetPostValue("leaderId").ToInt(1);
            long areas = CommonHelper.GetPostValue("areas").ToLong(-1L);
            long areaId = CommonHelper.GetPostValue("areaId").ToLong(-1L);
            DateTime openDate = CommonHelper.GetPostValue("openDate").ToDateTime(DateTime.Now);
            string stat = CommonHelper.GetPostValue("state");
            int state = stat.Equals("on", StringComparison.OrdinalIgnoreCase) ? 0 : 1;
            if (id <= 0)
            {
                json.ErrorCode = "E001";
                json.ErrorMessage = "参数错误！";
                return Json(json);
            }
            if (string.IsNullOrEmpty(name) || leaderId < 0 || areaId < 0)
            {
                json.ErrorCode = "E001";
                json.ErrorMessage = "参数不全！";
                return Json(json);
            }
            T_BG_Shop shop = _bgShopService.Get(id);
            shop.Name = name;
            shop.Address = address;
            shop.Tel = tel;
            shop.LeaderID = leaderId;
            shop.Areas = areas;
            shop.AreaId = areaId;
            shop.OpenDate = openDate;
            shop.State = state;
            if (state==1)
            {
               shop.WithdrawDate=DateTime.Now; 
            }
            bool result = _bgShopService.Update(shop);
            if (result)
            {
                json.ErrorCode = "E000";
                json.ErrorMessage = "修改成功！";
            }
            else
            {
                json.ErrorCode = "E002";
                json.ErrorMessage = "修改失败！";
            }
            return Json(json);
        }
    }
}