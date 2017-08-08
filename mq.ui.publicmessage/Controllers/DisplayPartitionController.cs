using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using koala.application.common;
using mq.application.common;
using mq.application.service;
using mq.model.dbentity;
using mq.model.viewentity;
using mq.model.viewentity.publicmessage;

namespace mq.ui.publicmessage.Controllers
{
    public class DisplayPartitionController : Controller
    {
        private readonly IBgDepartmentService _bgDepartmentService;
        private readonly IBgDisplayPartitionService _bgDisplayPartitionService;

        public DisplayPartitionController(IBgDepartmentService bgDepartmentService, IBgDisplayPartitionService bgDisplayPartitionService)
        {
            _bgDepartmentService = bgDepartmentService;
            _bgDisplayPartitionService = bgDisplayPartitionService;
        }

        // GET: DisplayPartition
        public ActionResult Add()
        {
            List<T_BG_Department> departmentList = _bgDepartmentService.GetListDepartment();
            return View(departmentList);
        }

        public ActionResult List()
        {
            List<T_BG_DisplayPartition> displayPartitions = _bgDisplayPartitionService.GetList(false);
            return View(displayPartitions);
        }


        public JsonResult SaveDisplayPartition()
        {
            JsonDisplayPartitionSaveDisplayPartition json = new JsonDisplayPartitionSaveDisplayPartition();
            long departId = CommonHelper.GetPostValue("departId").ToLong(-1L);
            string departName = CommonHelper.GetPostValue("departName");
            DateTime time = CommonHelper.GetPostValue("time").ToDateTime(DateTime.MinValue);
            string fileName = CommonHelper.GetPostValue("fileName");
            string oldFileName = CommonHelper.GetPostValue("oldFileName");

            departName = HttpUtility.UrlDecode(departName);
            oldFileName = HttpUtility.UrlDecode(oldFileName);
            if (departId < 0 || string.IsNullOrEmpty(departName) || string.IsNullOrEmpty(fileName))
            {
                json.ErrorCode = "E001";
                json.ErrorMessage = "参数不全！";
                return Json(json);
            }

            T_BG_DisplayPartition displayPartition = new T_BG_DisplayPartition();
            displayPartition.UserId = LoginHelper.UserId;
            displayPartition.FileName = fileName;
            displayPartition.DepartmentId = departId;
            displayPartition.DepartmentName = departName;
            displayPartition.PublishTime = time;
            displayPartition.AddTime = DateTime.Now;
            displayPartition.IsDel = 0;
            displayPartition.OldFileName = oldFileName;
            long result = _bgDisplayPartitionService.AddDisplayPartition(displayPartition);
            if (result > 0)
            {
                json.ErrorCode = "E000";
                json.ErrorMessage = "添加成功！";
            }
            else
            {
                json.ErrorCode = "E001";
                json.ErrorMessage = "添加失败！";
            }
            return Json(json);
        }


        //public HttpResponseMessage DownLoadImg()
        //{
        //    string url = CommonHelper.GetPostValue("url");
        //    string[] arrayurl = url.Split('?');
        //    string filename = (arrayurl[0].Substring(arrayurl[0].LastIndexOf('/') + 1));


        //    var wreq = HttpWebRequest.Create(url) as HttpWebRequest;
        //    HttpWebResponse response = wreq.GetResponse() as HttpWebResponse;
        //    MemoryStream ms = null;
        //    using (var stream = response.GetResponseStream())
        //    {
        //        Byte[] buffer = new Byte[response.ContentLength];
        //        int offset = 0, actuallyRead = 0;
        //        do
        //        {
        //            actuallyRead = stream.Read(buffer, offset, buffer.Length - offset);
        //            offset += actuallyRead;
        //        }
        //        while (actuallyRead > 0);
        //        ms = new MemoryStream(buffer);
        //    }
        //    response.Close();

        //    ms.Close();
        //    HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);

        //    result.Content = new ByteArrayContent(ms.ToArray());
        //    result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/Png"); //设置http响应上的Content-Type 为image/Png媒体类型
        //    result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
        //    {
        //        FileName = filename
        //    };
        //    return result;
        //}


    }
}