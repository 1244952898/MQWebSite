using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mq.application.common;
using mq.application.service;
using mq.model.dbentity;
using mq.model.viewentity.employeebg;

namespace mq.ui.employeebg.Controllers
{
    public class AreaController : Controller
    {
        private IBgAreaService _areaService;
        public AreaController(IBgAreaService areaService)
        {
            _areaService = areaService;
        }

        // GET: Area
        public ActionResult List()
        {
            List<T_BG_Area> areas = _areaService.List();
            return View(areas);
        }

        public ActionResult Add()
        {
            return View();
        }

        public JsonResult AddArea()
        {
            string areaName = CommonHelper.GetPostValue("name");
            areaName = HttpUtility.UrlDecode(areaName);

            JsonAreaAddAreaEntity result = new JsonAreaAddAreaEntity();

            if (string.IsNullOrEmpty(areaName))
            {
                result.ErrorCode = "E001";
                result.ErrorMessage = "未获得添加名字";
                return Json(result);
            }

            T_BG_Area bgArea = new T_BG_Area
            {
                AreaName = areaName,
                AddTime = DateTime.Now,
                IsDel = 0
            };

            bool success = _areaService.Add(bgArea);
            if (success)
            {
                result.ErrorCode = "E000";
                result.ErrorMessage = "更新成功!";
            }
            else
            {
                result.ErrorCode = "E002";
                result.ErrorMessage = "更新失败!";
            }
            return Json(result);
        }

        public JsonResult ModifyArea()
        {
            long id = CommonHelper.GetPostValue("id").ToLong(-1); 
            string areaName = CommonHelper.GetPostValue("name");
            areaName = HttpUtility.UrlDecode(areaName);

            JsonAreaAddAreaEntity result = new JsonAreaAddAreaEntity();

            if (id < 0 || string.IsNullOrEmpty(areaName))
            {
                result.ErrorCode = "E001";
                result.ErrorMessage = "未获得数据异常!";
                return Json(result);
            }

            T_BG_Area area = _areaService.Get(id);
            if (area==null)
            {
                result.ErrorCode = "E002";
                result.ErrorMessage = "该对象不存在或者已经删除！";
                return Json(result);
            }
            bool success = _areaService.Update(area);
            if (success)
            {
                result.ErrorCode = "E000";
                result.ErrorMessage = "更新成功!";
            }
            else
            {
                result.ErrorCode = "E002";
                result.ErrorMessage = "更新失败!";
            }
            return Json(result);
        }

        public JsonResult DelArea()
        {
            long id = CommonHelper.GetPostValue("id").ToLong(-1);

            JsonAreaAddAreaEntity result = new JsonAreaAddAreaEntity();

            if (id < 0)
            {
                result.ErrorCode = "E001";
                result.ErrorMessage = "未获得数据异常!";
                return Json(result);
            }

            T_BG_Area area = _areaService.Get(id);
            if (area == null)
            {
                result.ErrorCode = "E002";
                result.ErrorMessage = "该对象不存在或者已经删除！";
                return Json(result);
            }
            bool success = _areaService.Del(area);
            if (success)
            {
                result.ErrorCode = "E000";
                result.ErrorMessage = "更新成功!";
            }
            else
            {
                result.ErrorCode = "E002";
                result.ErrorMessage = "更新失败!";
            }
            return Json(result);
        }
    }
}