using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using koala.application.common;
using match.application.common;
using mq.application.common;
using mq.application.service;
using mq.model.dbentity;
using mq.model.viewentity;

namespace mq.ui.employeebg.Controllers
{
    public class PublicMessageController : Controller
    {
        private readonly IBgPublishFileService _bgPublishFileService;
        private readonly IBgVPublishFileUserService _bgVPublishFileService;
        private readonly IBgActiveFileService _bgActiveFileService;
        public PublicMessageController(IBgPublishFileService bgPublishFileService, IBgVPublishFileUserService bgVPublishFileService, IBgActiveFileService bgActiveFileService)
        {
            _bgPublishFileService = bgPublishFileService;
            _bgVPublishFileService = bgVPublishFileService;
            _bgActiveFileService = bgActiveFileService;
        }

        public ActionResult PublicFileList()
        {
            long count;
            int pageIndex = CommonHelper.GetPostValue("pageIndex").ToInt(1);
            int pageSize = CommonHelper.GetPostValue("pageSize").ToInt(20);
            ViewBag.pageIndex = pageIndex;
            List<V_BG_PublishFile_User> publishFileList = _bgVPublishFileService.GetFlieList(pageIndex - 1, pageSize, out count);
            ViewBag.page = count / pageSize;
            return View(publishFileList);
        }

        // GET: PublicMessage
        public ActionResult UploadMessage(long id = -1)
        {
            ViewBag.Id = id;
            return View();
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
            if (fileytpe.Contains(".doc"))
            {
                type = "application/msword";
            }
            else if (fileytpe.Contains(".pdf"))
            {
                type = "application/pdf";
            }
            else
            {
                return RedirectToAction("Error", "Menu", new { ErrorCode = "E0012", ErrorMsg = ErrorInfoHelper.GetErrorValue("E0012") });
            }
            return File(filepath, type, fileorgname);
        }

        public JsonResult AddFileMessage(string title, string lvl, string publictime, string htmlContent, string textContent, string fileOriginName, string fileNewName, string filePath, string filetype)
        {
            title = HttpUtility.UrlDecode(title);
            htmlContent = HttpUtility.UrlDecode(htmlContent);
            textContent = HttpUtility.UrlDecode(textContent);
            fileOriginName = HttpUtility.UrlDecode(fileOriginName);
            filePath = HttpUtility.UrlDecode(filePath);
            long userid = LoginHelper.UserId;
            T_BG_PublishFile publishFile = new T_BG_PublishFile
            {
                Title = title,
                EditorId = userid,
                PublishTime = publictime.ToDateTime(DateTime.MinValue),
                PublishState = 1,
                Lvl = lvl.ToLong(-1),
                TextContent = textContent,
                HtmlContent = htmlContent,
                IsDel = 0,
                FileNewName = fileNewName,
                FileOriginName = fileOriginName,
                FilePath = filePath,
                FileType = filetype
            };
            //没有审核的列表
            bool result = _bgPublishFileService.AddPublishFile(publishFile);

            JsonBaseEntity json = new JsonBaseEntity { ErrorCode = result ? "E0001" : "E0009" };
            json.ErrorMessage = ErrorInfoHelper.GetErrorValue(json.ErrorCode);
            return Json(json);
        }

        public JsonResult UpdateFileMessage(string title, string lvl, string publictime, string htmlContent, string textContent, string fileOriginName, string fileNewName, string filePath, string filetype, long id = -1)
        {
            if (id <= 0)
                return Json(new JsonBaseEntity { ErrorCode = "E0014", ErrorMessage = ErrorInfoHelper.GetErrorValue("E0014") });

            title = HttpUtility.UrlDecode(title);
            htmlContent = HttpUtility.UrlDecode(htmlContent);
            textContent = HttpUtility.UrlDecode(textContent);
            fileOriginName = HttpUtility.UrlDecode(fileOriginName);
            filePath = HttpUtility.UrlDecode(filePath);
            long userid = LoginHelper.UserId;
            var publishFile = new T_BG_PublishFile
            {
                Id = id,
                Title = title,
                EditorId = userid,
                PublishTime = publictime.ToDateTime(DateTime.MinValue),
                PublishState = 1,
                Lvl = lvl.ToLong(-1),
                TextContent = textContent,
                HtmlContent = htmlContent,
                IsDel = 0,
                FileNewName = fileNewName,
                FileOriginName = fileOriginName,
                FilePath = filePath,
                FileType = filetype
            };
            //没有审核的列表
            bool result = _bgPublishFileService.UpdatePulishFile(publishFile);

            JsonBaseEntity json = new JsonBaseEntity { ErrorCode = result ? "E0001" : "E0009" };
            json.ErrorMessage = ErrorInfoHelper.GetErrorValue(json.ErrorCode);
            return Json(json);
        }

        public JsonResult DelAddFileList(List<T_BG_PublishFile> activeFileList)
        {
            JsonBaseEntity result = new JsonBaseEntity();
            if (activeFileList == null || activeFileList.Count <= 0)
            {
                result.ErrorCode = "E0001";
                result.ErrorMessage = "未发现删除数据！";
                return Json(result);
            }
            bool reslt = _bgPublishFileService.UpdatBath(activeFileList);
            if (reslt)
            {
                result.ErrorCode = "E0000";
                result.ErrorMessage = "删除数据成功！";
            }
            else
            {
                result.ErrorCode = "E0001";
                result.ErrorMessage = "删除数据失败！";
            }
            return Json(result);
        }

        public JsonResult DelSingle()
        {
            long id = CommonHelper.GetPostValue("id").ToLong(-1);
            JsonBaseEntity result = new JsonBaseEntity();
            if (id <= 0)
            {
                result.ErrorCode = "E0001";
                result.ErrorMessage = "未发现删除数据！";
                return Json(result);
            }
            T_BG_PublishFile publishFile = new T_BG_PublishFile { Id = id, IsDel = 1 };
            bool reslt = _bgPublishFileService.DelDateById(publishFile);
            if (reslt)
            {
                result.ErrorCode = "E0000";
                result.ErrorMessage = "删除数据成功！";
            }
            else
            {
                result.ErrorCode = "E0001";
                result.ErrorMessage = "删除数据失败！";
            }
            return Json(result);
        }

    }
}