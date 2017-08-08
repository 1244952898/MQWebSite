using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mq.application.common;
using mq.application.service;
using mq.model.dbentity;
using mq.model.viewentity.publicmessage;

namespace mq.ui.employeebg.Controllers
{
    public class DisplayPartitionController : Controller
    {
        private readonly IBgDisplayPartitionService _bgDisplayPartitionService;

        public DisplayPartitionController(IBgDisplayPartitionService bgDisplayPartitionService)
        {
            _bgDisplayPartitionService = bgDisplayPartitionService;
        }

        public ActionResult EditList()
        {
            List<T_BG_DisplayPartition> displayPartitions = _bgDisplayPartitionService.GetList();
            return View(displayPartitions);
        }


        public JsonResult DelFile()
        {
            long id = CommonHelper.GetPostValue("id").ToLong(-1L);

            JsonDisplayPartitionDel json = new JsonDisplayPartitionDel();
            if (id < 0)
            {
                json.ErrorCode = "E001";
                json.ErrorMessage = "参数获取失败！";
                return Json(json);
            }

            T_BG_DisplayPartition displayPartition = _bgDisplayPartitionService.Get(id);
            if (displayPartition == null)
            {
                json.ErrorCode = "E002";
                json.ErrorMessage = "未获得该数据！";
                return Json(json);
            }
            displayPartition.IsDel = 1;
            bool re = _bgDisplayPartitionService.UpdateAndDelMongoDb(displayPartition, "mq_bg_displaypartition");
            if (re)
            {//D250A7C4-8314-4626-BA2C-9AC6C7728061.JPG
                json.ErrorCode = "E000";
                json.ErrorMessage = "删除成功！";
            }
            else
            {
                json.ErrorCode = "E003";
                json.ErrorMessage = "删除失败！";
            }
            return Json(json);
        }

    }
}