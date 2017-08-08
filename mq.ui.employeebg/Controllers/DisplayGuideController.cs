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

        public ActionResult GuideFileList()
        {
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
            }
            else
            {
                json.ErrorCode = "E000";
                json.ErrorMessage = "成功!";
                json.File = _bgDisplayGuideFileService.GetBgDisplayGuideFile(id);
                json.PublishTime = json.File.PublishTime.ToDateTime(DateTime.MinValue).ToString("yyyy-MM-dd hh:mm:ss");
            }
            return Json(json);
        }

        public JsonResult SaveGuideFile()
        {
            long id = CommonHelper.GetPostValue("id").ToLong(-1L);
            string title = CommonHelper.GetPostValue("title");
            string time = CommonHelper.GetPostValue("time");
            string fileoriginname = CommonHelper.GetPostValue("fileoriginname");
            string filenewname = CommonHelper.GetPostValue("filenewname");
            string filepath = CommonHelper.GetPostValue("filepath");
            string filetype = CommonHelper.GetPostValue("filetype"); ;
            title = HttpUtility.UrlDecode(title);
            fileoriginname = HttpUtility.UrlDecode(fileoriginname);
            filepath = HttpUtility.UrlDecode(filepath);
            JsonGuideFleSaveEntity json = new JsonGuideFleSaveEntity();
            if (string.IsNullOrEmpty(time) || string.IsNullOrEmpty(fileoriginname) || string.IsNullOrEmpty(filenewname) || string.IsNullOrEmpty(filepath) || string.IsNullOrEmpty(filetype))
            {
                json.ErrorCode = "E001";
                json.ErrorMessage = "参数不全！";
                return Json(json);
            }

            T_BG_DisplayGuideFile displayGuideFile = new T_BG_DisplayGuideFile
            {
                Title = title,
                FileNewName = filenewname,
                FileOriginName = fileoriginname,
                FilePath = filepath,
                FileType = filetype,
                UserId = LoginHelper.UserId,
                PublishTime = time.ToDateTime(DateTime.MinValue),
                AddTime = DateTime.Now,
                IsDel = 0
            };

            bool result;
            if (id > 0)
            {
                T_BG_DisplayGuideFile file = _bgDisplayGuideFileService.GetBgDisplayGuideFile(id);
                if (file == null)
                {
                    json.ErrorCode = "E001";
                    json.ErrorMessage = "参数不全！";
                    return Json(json);
                }
                displayGuideFile.Id = file.Id;
                if (!file.FilePath.Equals(filepath,StringComparison.InvariantCultureIgnoreCase))
                {
                    //上传表里的数据也应该删除T_BG_UpFiles
                    string msg;
                    bool del = FileHelper.DelFile(file.FilePath, out msg);
                    if (!del)
                    {
                        json.ErrorCode = "E002";
                        json.ErrorMessage = "删除本地的文件失败！";
                        return Json(json);
                    }
                    bool delResult = _bgUpFilesService.DelFileByFileNewName(filenewname);
                    if (!delResult)
                    {
                        json.ErrorCode = "E002";
                        json.ErrorMessage = "删除T_BG_UpFiles记录的文件失败！";
                        return Json(json);
                    }
                }
                result = _bgDisplayGuideFileService.UpdateDisplayGuideFile(displayGuideFile);
            }
            else
            {
                result = _bgDisplayGuideFileService.AddDisplayGuideFile(displayGuideFile) > 0;
            }

            if (result)
            {
                json.ErrorCode = "E000";
                json.ErrorMessage = "保存成功！";
            }
            else
            {
                json.ErrorCode = "E002";
                json.ErrorMessage = "保存失败！";
            }
            return Json(json);
        }

    }
}