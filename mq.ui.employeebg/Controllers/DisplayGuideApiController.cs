using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using koala.application.common;
using log4net;
using mq.application.common;
using mq.application.enumlib;
using mq.application.service;
using mq.application.service.Interface;
using mq.model.dbentity;
using mq.model.viewentity;

namespace mq.ui.employeebg.Controllers
{
    public class DisplayGuideApiController : ApiController
    {
        ILog logger = log4net.LogManager.GetLogger(typeof(DisplayGuideApiController));
        private readonly IBgVDisplayGuideFileService _bgVDisplayGuideFileService;
        private readonly IBgDisplayGuideFileService _bgDisplayGuideFileService;
        private readonly IBgUpFilesService _bgUpFilesService;

        public DisplayGuideApiController(IBgVDisplayGuideFileService bgVDisplayGuideFileService, IBgDisplayGuideFileService bgDisplayGuideFileService, IBgUpFilesService bgUpFilesService)
        {
            _bgVDisplayGuideFileService = bgVDisplayGuideFileService;
            _bgDisplayGuideFileService = bgDisplayGuideFileService;
            _bgUpFilesService = bgUpFilesService;
        }
        [System.Web.Http.HttpPost]
        [System.Web.Http.HttpGet]
        public JsonDisplayGuideAptGetListData GetListData()
        {
            JsonDisplayGuideAptGetListData result = new JsonDisplayGuideAptGetListData
            {
                DisplayGuideFileList = _bgVDisplayGuideFileService.GetList()
            };
            return result;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.HttpGet]
        public JsonDisplayGuideApiDelEntity DelDisplayGuide()
        {
            long id = CommonHelper.GetPostValue("id").ToLong(-1L);
            JsonDisplayGuideApiDelEntity json = new JsonDisplayGuideApiDelEntity();
            if (id < 0)
            {
                json.ErrorCode = "E0001";
                json.ErrorMessage = "获得删除信息失败！";
                return json;
            }
            T_BG_DisplayGuideFile file = _bgDisplayGuideFileService.GetBgDisplayGuideFile(id);
            if (file == null)
            {
                json.ErrorCode = "E0002";
                json.ErrorMessage = "获得文件删除信息失败！";
                return json;
            }
            string errorMsg;
            bool delFile = FileHelper.DelFile(file.FilePath, out errorMsg);
            if (!delFile)
            {
                json.ErrorCode = "E0003";
                json.ErrorMessage = "删除本地文件失败！";
                return json;
            }
            bool result = _bgDisplayGuideFileService.DelGuideFileAndUploadFile(file.FileNewName);
            if (!result)
            {
                json.ErrorCode = "E0004";
                json.ErrorMessage = "删除数据库信息失败！";
            }
            else
            {
                json.ErrorCode = "E0000";
                json.ErrorMessage = "删除成功！";
            }
            return json;
        }
    }
}
