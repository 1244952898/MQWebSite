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

        [HttpGet]
        [HttpPost]
        public JsonResult Post(string id)
        {
            string userid = CommonHelper.GetPostValue("key");
            //string sign = CommonHelper.GetPostValue("sign");
            string action = CommonHelper.GetPostValue("action");
            string filename = CommonHelper.GetPostValue("fn");
            string type = CommonHelper.GetPostValue("type").ToString("PublicFile");
            long cid = CommonHelper.GetPostValue("cid").ToLong(-1);

            userid = "1";


            long lUserId = userid.ToLong(-1);
            //string.IsNullOrEmpty(sign) || 
            if (string.IsNullOrEmpty(userid) || lUserId <= 0)
            {
                return Json(new FileUploadEntity { ErrorCode = "10000", ErrorMessage = "非法请求" });
            }
            T_BG_User bgUser = _bgUserService.GetUserById(userid.ToLong(-1));
            if (bgUser == null)
            {
                return Json(new FileUploadEntity { ErrorCode = "10000", ErrorMessage = "请重新登录" });
            }
            if (HttpContext.Request.Files.Count > 0)
            {
                try
                {
                    HttpPostedFile file = System.Web.HttpContext.Current.Request.Files[0];
                    if (file.ContentLength <= 0)
                    {
                        return Json(new FileUploadEntity { ErrorCode = "10000", ErrorMessage = "上传的文档为空！" });
                    }
                    //11111111111111111
                    FileExtension[] fileExs = new FileExtension[] { FileExtension.DOC, FileExtension.DOCX, FileExtension.PDF };
                    string fileExt = FileValidation.FileExtension(file, fileExs);
                    if (string.IsNullOrEmpty(fileExt))
                    {
                        return Json(new FileUploadEntity { ErrorCode = "10000", ErrorMessage = "请上传WORD、PDF格式的文档！" });
                    }

                    string originFileName = Regex.Replace(file.FileName, "." + fileExt, "." + fileExt, RegexOptions.IgnoreCase);
                    string savename = Guid.NewGuid().ToString("N");
                    if (!string.IsNullOrEmpty(filename))
                        savename = filename.Replace(string.Format("{0}_", LoginHelper.UserId), "");
                    string ofile = originFileName;
                    string newFileName = OriginalSaveHelper.CreateNewSaveFileName(userid, savename, fileExt);
                    string savePath = OriginalSaveHelper.GetSavePath(newFileName, fileExt, type);
                    string fn = newFileName.Replace(string.Format(".{0}", fileExt), "");
                    if (!Directory.Exists(savePath))
                    {
                        Directory.CreateDirectory(savePath);
                    }
                    string strMs = string.Empty;
                    //string exception = "服务端异常";
                    string newFilePath = string.Format(@"{0}\{1}", savePath, newFileName);
                    file.SaveAs(newFilePath);
                    int iPageNum = 0;
                    fileExt = fileExt.ToLower();
                    if (fileExt == "pdf" || fileExt.EndsWith("pdf"))
                    {
                        iPageNum = PdfSaveHelper.GetPdfFilePages(newFilePath, out strMs);
                    }
                    //如果iPageNum 不更新或不上传
                    if (iPageNum < 0)
                    {
                        return Json(new FileUploadEntity { ErrorCode = "10000", ErrorMessage = "服务器不能解析上传的pdf，请选择其他文件重新上传！" });
                    }

                    T_BG_UpFiles bgUpFiles = new T_BG_UpFiles { filename = fn, filehash = FileHelper.GetFileHash(newFilePath), userid = lUserId, fileoriginname = originFileName, filepath = newFilePath, ext = fileExt, filetype = 0, addtime = DateTime.Now };

                    string fileorigin = originFileName.ReplaceSqlTag();
                    long cnt = _bgUpFilesService.GetListByUserIdAndFileNameAndExt(originFileName, lUserId, fileExt);
                    long reslt = _bgUpFilesService.Add(bgUpFiles);
                    if (reslt > 0)
                    {
                        return Json(new FileUploadEntity { ErrorCode = "00000", ErrorMessage = fn, Attach = string.Format("{0}({1}).{2}", fileorigin.Replace("." + bgUpFiles.ext, ""), cnt, bgUpFiles.ext), FilePath = bgUpFiles.filepath, FileType = fileExt });
                    }
                    else
                    {
                        return Json(new FileUploadEntity { ErrorCode = "10003", ErrorMessage = "服务端保存文件失败" });
                    }
                }
                catch (Exception)
                {
                    return Json(new FileUploadEntity { ErrorCode = "10000", ErrorMessage = "服务端异常" });
                }
            }
            else
                return Json(new FileUploadEntity { ErrorCode = "10002", ErrorMessage = "请选择文件" });
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
    }
}