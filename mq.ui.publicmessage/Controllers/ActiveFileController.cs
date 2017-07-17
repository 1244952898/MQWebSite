using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mq.application.common;
using mq.application.enumlib;
using mq.application.service;
using mq.model.extendedentity.employeebg;

namespace mq.ui.publicmessage.Controllers
{
    public class ActiveFileController : Controller
    {
        private readonly IActiveFileDepartmentExtendService _activeFileDepartmentExtendService;

        public ActiveFileController(IActiveFileDepartmentExtendService activeFileDepartmentExtendService)
        {
            _activeFileDepartmentExtendService = activeFileDepartmentExtendService;
        }

        // GET: ActiveFile
        public ActionResult ActiveFileList()
        {
            int typ = CommonHelper.GetPostValue("typ").ToInt(1);
            ViewBag.typ = typ;
            ActiveFileTypeEnum type = typ == 1 ? ActiveFileTypeEnum.Publish : typ == 2 ? ActiveFileTypeEnum.Office : ActiveFileTypeEnum.Shop;
            List<ActiveFileDepartmentExtend> list = _activeFileDepartmentExtendService.GetList(type);
            return View(list);
        }
    }
}