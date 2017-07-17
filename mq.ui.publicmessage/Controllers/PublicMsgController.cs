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
using mq.model.viewentity;

namespace mq.ui.publicmessage.Controllers
{
    public class PublicMsgController : Controller
    {
        ILog logger = LogManager.GetLogger(typeof(PublicMsgController));
        private readonly IBgVPublishFileUserService _bgVPublishFileService;
        public PublicMsgController(IBgVPublishFileUserService bgVPublishFileService)
        {
            _bgVPublishFileService = bgVPublishFileService;
        }
        // GET: PublicMsg
        public ActionResult PublicFiles()
        {
            long count;
            int pageIndex = CommonHelper.GetPostValue("pageIndex").ToInt(1);
            int pageSize = CommonHelper.GetPostValue("pageSize").ToInt(20);
            ViewBag.pageIndex = pageIndex;
            List<V_BG_PublishFile_User> publishFileList = _bgVPublishFileService.GetFlieList(pageIndex - 1, pageSize, out count);
            ViewBag.page = count / pageSize;
            return View(publishFileList);
        }

        public ActionResult PublicFileDetial()
        {

            long fileId = CommonHelper.GetPostValue("fileid").ToInt(-1); 
            int pageIndex = CommonHelper.GetPostValue("pageIndex").ToInt(1);
            ViewBag.pageIndex = pageIndex;
            if (fileId <= 0)
            {
                return RedirectToAction("Error", "Menu", new { ErrorCode = "E0014", ErrorMsg = ErrorInfoHelper.GetErrorValue("E0014") });
            }
            PublicFileDetailView publicFileDetailView = new PublicFileDetailView
            {
                BgPublishFileUser = _bgVPublishFileService.GetFlieById(fileId)
            };
            if (publicFileDetailView.BgPublishFileUser == null)
            {
                return RedirectToAction("Error", "Menu", new { ErrorCode = "E0014", ErrorMsg = ErrorInfoHelper.GetErrorValue("E0014") });
            }
            if (publicFileDetailView.BgPublishFileUser.PublishTime != null)
            {
                publicFileDetailView.BgPublishFileUserPre =
                    _bgVPublishFileService.GetFlieByIdPre(publicFileDetailView.BgPublishFileUser.PublishTime.Value, fileId);
                publicFileDetailView.BgPublishFileUserNext =
                    _bgVPublishFileService.GetFlieByIdNext(publicFileDetailView.BgPublishFileUser.PublishTime.Value, fileId);
            }
            
            return View(publicFileDetailView);
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
            if (fileytpe.Contains("doc") || fileytpe.Contains("docx"))
            {
                type = "application/msword";
            }
            else if (fileytpe.Contains("pdf"))
            {
                type = "application/pdf";
            }
            else
            {
                return RedirectToAction("Error", "Menu", new { ErrorCode = "E0012", ErrorMsg = ErrorInfoHelper.GetErrorValue("E0012") });
            }
            return File(filepath, type, fileorgname);
        }
    }
}