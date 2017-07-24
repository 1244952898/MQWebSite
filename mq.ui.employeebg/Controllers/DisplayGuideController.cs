using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using koala.application.common;
using mq.application.common;
using mq.application.enumlib;
using mq.application.service;
using mq.application.service.Interface;
using mq.model.dbentity;
using mq.model.viewentity;

namespace mq.ui.employeebg.Controllers
{
    public class DisplayGuideController : Controller
    {
        private readonly IBgDisplayGuideFileService _bgDisplayGuideFileService;
        private readonly IBgUserService _bgUserService;
        private readonly IBgUpFilesService _bgUpFilesService;

        public DisplayGuideController(IBgDisplayGuideFileService bgDisplayGuideFileService, IBgUserService bgUserService, IBgUpFilesService bgUpFilesService)
        {
            _bgDisplayGuideFileService = bgDisplayGuideFileService;
            _bgUserService = bgUserService;
            _bgUpFilesService = bgUpFilesService;
        }

        // GET: DisplayGuide
        public ActionResult AddGuideFile()
        {
            long id = CommonHelper.GetPostValue("id").ToLong(-1L);
            ViewBag.id = id;
            return View();
        }

        public JsonResult GetDisplayFile()
        {
            JsonDisplayGuideEntity json = new JsonDisplayGuideEntity();
            long id = CommonHelper.GetPostValue("id").ToLong(-1L);
            if (id <= 0)
            {
                json.ErrorCode = "E001";
                json.ErrorMessage = "未获得Id!";
                return Json(json);
            }
            json.ErrorCode = "E000";
            json.ErrorMessage = "成功!";
            json.File = _bgDisplayGuideFileService.GetBgDisplayGuideFile(id);
            return Json(json);
        }

        public JsonResult SaveGuideFile()
        {
            string title = CommonHelper.GetPostValue("title");
            string time = CommonHelper.GetPostValue("time");
            string fileoriginname = CommonHelper.GetPostValue("fileoriginname");
            string filenewname = CommonHelper.GetPostValue("filenewname");
            string filepath = CommonHelper.GetPostValue("filepath");
            string filetype = CommonHelper.GetPostValue("filetype");;
            title = HttpUtility.UrlDecode(title);
            fileoriginname = HttpUtility.UrlDecode(fileoriginname);
            filepath = HttpUtility.UrlDecode(filepath);
            JsonGuideFleSaveEntity json=new JsonGuideFleSaveEntity();
            if (string.IsNullOrEmpty(time) || string.IsNullOrEmpty(fileoriginname) || string.IsNullOrEmpty(filenewname) || string.IsNullOrEmpty(filepath) || string.IsNullOrEmpty(filetype))
            {
                json.ErrorCode = "E001";
                json.ErrorMessage = "参数不全！";
            }
            T_BG_DisplayGuideFile displayGuideFile=new T_BG_DisplayGuideFile();
            displayGuideFile.Title = title;
            displayGuideFile.FileNewName = filenewname;
            displayGuideFile.FileOriginName = fileoriginname;
            displayGuideFile.FilePath = filepath;
            displayGuideFile.FileType = filetype;
            displayGuideFile.UserId = LoginHelper.UserId;
            displayGuideFile.PublishTime = time.ToDateTime(DateTime.MinValue);
            displayGuideFile.AddTime = DateTime.Now;
            displayGuideFile.IsDel = 0;
            long result =_bgDisplayGuideFileService.AddDisplayGuideFile(displayGuideFile);
            if (result>0)
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
    }
}