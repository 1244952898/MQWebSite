using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using koala.application.common;
using match.application.common;
using mq.application.common;
using mq.application.enumlib;
using mq.application.service;
using mq.model.dbentity;
using mq.model.viewentity;

namespace mq.ui.employeebg.Controllers
{
    public class ActiveFileController : Controller
    {
        private readonly IBgDepartmentService _bgDepartmentService;
        private readonly IBgActiveFileService _bgActiveFileService;
        private readonly IBgVActiveFileDepartmentService _activeFileDepartmentService;

        public ActiveFileController(IBgDepartmentService bgDepartmentService, IBgActiveFileService bgActiveFileService, IBgVActiveFileDepartmentService activeFileDepartmentService)
        {
            _bgDepartmentService = bgDepartmentService;
            _bgActiveFileService = bgActiveFileService;
            _activeFileDepartmentService = activeFileDepartmentService;
        }

        // GET: ActiveFile
        public ActionResult AddFile(long id = -1)
        {
            ViewBag.id = id;
            List<T_BG_Department> departmentList = _bgDepartmentService.GetListDepartment();
            return View(departmentList);
        }

        public ActionResult FileList()
        {
          
            //int type = CommonHelper.GetPostValue("type").ToInt(0);
            //ActiveFileTypeEnum typ = (ActiveFileTypeEnum)type;
            //List<V_BG_ActiveFile_Department> list = _activeFileDepartmentService.GetList(typ);
            return View();
        }

        public JsonResult AddAddFile(long departId, int natureTyp, int typ, DateTime time, string fileOriginName, string fileNewName, string filePath, string filetype, string remark)
        {
            fileOriginName = HttpUtility.UrlDecode(fileOriginName);
            filePath = HttpUtility.UrlDecode(filePath);
            remark = HttpUtility.UrlDecode(remark);

            T_BG_ActiveFile actineFile = new T_BG_ActiveFile
            {
                Type = typ,
                DeparementId = departId,
                PublicTime = time,
                NatureType = natureTyp,
                FileNewName = fileNewName,
                FileOriginName = fileOriginName,
                FilePath = filePath,
                FileType = filetype,
                IsDel = 0,
                Remark = remark,
                AddTime = DateTime.Now
            };
            long result = _bgActiveFileService.AddActiveFile(actineFile);

            JsonBaseEntity json = new JsonBaseEntity { ErrorCode = result > 0 ? "E0001" : "E0009" };
            json.ErrorMessage = ErrorInfoHelper.GetErrorValue(json.ErrorCode);
            return Json(json);
        }

        public JsonResult GetAddFileModel()
        {
            long id = CommonHelper.GetPostValue("id").ToLong(-1L);
            JsonAddFileEntity json = new JsonAddFileEntity();
            if (id <= 0)
            {
                json.ErrorCode = "E0015";
            }
            json.ActiveFile = _bgActiveFileService.GetActiveFile(id);
            if (json.ActiveFile == null)
            {
                json.ErrorCode = "E0016";
            }
            else
            {
                //防止时间被json格式化
                json.PublishTime = json.ActiveFile.PublicTime.ToDateTime(DateTime.MaxValue).ToString("yyyy-MM-dd hh:mm:ss");
            }
            json.ErrorCode = "E0001";
            json.ErrorMessage = ErrorInfoHelper.GetErrorValue(json.ErrorCode);
            return Json(json);
        }

        public JsonResult DelAddFileList(List<T_BG_ActiveFile> activeFileList)
        {
            JsonBaseEntity result = new JsonBaseEntity();
            if (activeFileList == null || activeFileList.Count <= 0)
            {
                result.ErrorCode = "E0001";
                result.ErrorMessage = "未发现删除数据！";
                return Json(result);
            }
            bool reslt = _bgActiveFileService.UpdatBath(activeFileList);
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