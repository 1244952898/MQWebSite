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
        private readonly IBgUserService _bgUserService;
        private readonly IBgUpFilesService _bgUpFilesService;

        public DisplayGuideApiController(IBgUserService bgUserService, IBgUpFilesService bgUpFilesService)
        {
            _bgUserService = bgUserService;
            _bgUpFilesService = bgUpFilesService;
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.HttpPost]
        public FileUploadEntity Post()
        {
            string userid = CommonHelper.GetPostValue("key");
            string filename = CommonHelper.GetPostValue("fn");
            string type = CommonHelper.GetPostValue("type").ToString("PublicFile");
            string id = CommonHelper.GetPostValue("id"); 
            string action = CommonHelper.GetPostValue("action");
            long cid = CommonHelper.GetPostValue("cid").ToLong(-1);

            userid = "1";

            long lUserId = userid.ToLong(-1);
            //string.IsNullOrEmpty(sign) || 
            if (string.IsNullOrEmpty(userid) || lUserId <= 0)
            {
                return new FileUploadEntity { ErrorCode = "10000", ErrorMessage = "非法请求" };
            }
            T_BG_User bgUser = _bgUserService.GetUserById(userid.ToLong(-1));
            if (bgUser == null)
            {
                return new FileUploadEntity { ErrorCode = "10000", ErrorMessage = "请重新登录" };
            }
            if (HttpContext.Current.Request.Files.Count > 0)
            {
                try
                {
                    HttpPostedFile file = System.Web.HttpContext.Current.Request.Files[0];
                    if (file.ContentLength <= 0)
                    {
                        return new FileUploadEntity { ErrorCode = "10000", ErrorMessage = "上传的文档为空！" };
                    }
                    //11111111111111111
                    FileExtension[] fileExs = new FileExtension[] { FileExtension.DOC, FileExtension.DOCX, FileExtension.PDF };
                    string fileExt = FileValidation.FileExtension(file, fileExs);
                    if (string.IsNullOrEmpty(fileExt))
                    {
                        return new FileUploadEntity { ErrorCode = "10000", ErrorMessage = "请上传WORD、PDF格式的文档！" };
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
                        return new FileUploadEntity { ErrorCode = "10000", ErrorMessage = "服务器不能解析上传的pdf，请选择其他文件重新上传！" };
                    }

                    T_BG_UpFiles bgUpFiles = new T_BG_UpFiles { filename = fn, filehash = FileHelper.GetFileHash(newFilePath), userid = lUserId, fileoriginname = originFileName, filepath = newFilePath, ext = fileExt, filetype = 0, addtime = DateTime.Now };

                    string fileorigin = originFileName.ReplaceSqlTag();
                    long cnt = _bgUpFilesService.GetListByUserIdAndFileNameAndExt(originFileName, lUserId, fileExt);
                    long reslt = _bgUpFilesService.Add(bgUpFiles);
                    if (reslt > 0)
                    {
                        return new FileUploadEntity { ErrorCode = "00000", ErrorMessage = fn, Attach = string.Format("{0}({1}).{2}", fileorigin.Replace("." + bgUpFiles.ext, ""), cnt, bgUpFiles.ext), FilePath = bgUpFiles.filepath, FileType = fileExt };
                    }
                    else
                    {
                        return new FileUploadEntity { ErrorCode = "10003", ErrorMessage = "服务端保存文件失败" };
                    }
                }
                catch (Exception)
                {
                    return new FileUploadEntity { ErrorCode = "10000", ErrorMessage = "服务端异常" };
                }
            }
            else
                return new FileUploadEntity { ErrorCode = "10002", ErrorMessage = "请选择文件" };
        }
    }
}
