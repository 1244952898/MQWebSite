using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using match.application.common;
using mq.application.common;
using mq.application.service;
using mq.model.dbentity;

namespace mq.ui.publicmessage.Controllers
{
    public class DisplayGuideController : Controller
    {
        ILog logger = log4net.LogManager.GetLogger(typeof(DisplayGuideController));
        private readonly IBgVDisplayGuideFileService _bgVDisplayGuideFileService;
        public DisplayGuideController(IBgVDisplayGuideFileService bgVDisplayGuideFileService)
        {
            _bgVDisplayGuideFileService = bgVDisplayGuideFileService;
        }

        // GET: DisplayGuide
        public ActionResult GuideFileList()
        {
           List<V_BG_DisplayGuideFile_User> displayGuideFiles = _bgVDisplayGuideFileService.GetList(false);
           return View(displayGuideFiles);
        }

        public ActionResult DownloadFilePathResult()
        {
            string filepath = CommonHelper.GetPostValue("filepath");
            string fileorgname = CommonHelper.GetPostValue("fileorgname");
            fileorgname = HttpUtility.UrlDecode(fileorgname);
            string fileytpe = CommonHelper.GetPostValue("fileytpe");

            if (string.IsNullOrEmpty(filepath))
            {
                return RedirectToAction("Error", "Menu", new { ErrorCode = "E0013", ErrorMsg = ErrorInfoHelper.GetErrorValue("E0013") });
            }

            string type;
            if (fileytpe.EndsWith("zip",StringComparison.OrdinalIgnoreCase))
            {
                type = "application/x-zip-compressed";
            }
            else if (fileytpe.EndsWith("rar", StringComparison.OrdinalIgnoreCase))
            {
                type = "application/octet-stream";
            }
            else if (fileytpe.Contains("pdf"))
            {
                type = "application/pdf";
            }
            else
            {
                return RedirectToAction("Error", "Menu", new { ErrorCode = "E0012", ErrorMsg = "文件类型错误！" });
            }
            return File(filepath, type, fileorgname);
        }
    }
}