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
using mq.application.service.Implement;
using mq.application.service.Interface;
using mq.model.dbentity;
using mq.model.viewentity;

namespace mq.webservice.upload.Controllers
{
    public class FilesController : Controller
    {
        private readonly IBgUserService _bgUserService;
        private readonly IBgUpFilesService _bgUpFilesService;

        public FilesController(IBgUserService bgUserService, IBgUpFilesService bgUpFilesService)
        {
            _bgUserService = bgUserService;
            _bgUpFilesService = bgUpFilesService;
        }

        //[HttpPost]
        public JsonResult Post(string id)
        {
            string userid = CommonHelper.GetPostValue("key");
            //string sign = CommonHelper.GetPostValue("sign");
            string action = CommonHelper.GetPostValue("action");
            string filename = CommonHelper.GetPostValue("fn");
            string type = CommonHelper.GetPostValue("type").ToString("PublicFile");
            long cid = CommonHelper.GetPostValue("cid").ToLong(-1);

            userid = "1";

            OrignalTypeEnum saveType = type.Equals("GuideFile") || type.Equals("3")
                ? OrignalTypeEnum.GuideFile
                : type.Equals("ActiveFile") || type.Equals("1")
                    ? OrignalTypeEnum.ActiveFile
                    : OrignalTypeEnum.PublicFile;

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
                    //22222222222222222
                    //string fileexs = ".doc,.docx,.pdf";
                    //string fileext1 = Path.GetExtension(file.FileName);
                    //if (!fileexs.Contains(fileext1))
                    //{
                    //    return new FileUploadEntity { ErrorCode = "10000", ErrorMessage = "请上传WORD、PDF格式的文档！" };
                    //}

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

                    T_BG_UpFiles bgUpFiles = new T_BG_UpFiles { filename = fn, filehash = FileHelper.GetFileHash(newFilePath), userid = lUserId, fileoriginname = originFileName, filepath = newFilePath, ext = fileExt, filetype = 0, addtime = DateTime.Now, type = saveType.ToInt(0) };

                    string fileorigin = originFileName.ReplaceSqlTag();
                    long cnt = _bgUpFilesService.GetListByUserIdAndFileNameAndExt(originFileName, lUserId, fileExt,saveType.ToInt(0));
                    long reslt = _bgUpFilesService.Add(bgUpFiles);
                    if (reslt > 0)
                    {
                        string oname = fileorigin.ToLower().Replace("." + bgUpFiles.ext.ToLower(), "");
                        string attach = cnt > 0
                            ? string.Format("{0}.{1}", oname, bgUpFiles.ext)
                            : string.Format("{0}({1}).{2}", oname, cnt, bgUpFiles.ext);

                        return Json(new FileUploadEntity { ErrorCode = "00000", ErrorMessage = fn, Attach = attach, FilePath = bgUpFiles.filepath, FileType = fileExt });
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

        public JsonResult PostGuideFile(string id)
        {
            string userid = CommonHelper.GetPostValue("key");
            string action = CommonHelper.GetPostValue("action");
            string filename = CommonHelper.GetPostValue("fn");
            string type = CommonHelper.GetPostValue("filetype").ToString("PublicFile");
            long cid = CommonHelper.GetPostValue("cid").ToLong(-1);



            userid = "1";


            OrignalTypeEnum saveType = type.Equals("GuideFile") || type.Equals("3")
               ? OrignalTypeEnum.GuideFile
               : type.Equals("ActiveFile") || type.Equals("1")
                   ? OrignalTypeEnum.ActiveFile
                   : OrignalTypeEnum.PublicFile;

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

                    FileExtension[] fileExs = new FileExtension[] { FileExtension.ZIP, FileExtension.RAR, FileExtension.PDF };
                    string fileExt = FileValidation.FileExtensionPdfRarZip(file, fileExs);
                    if (string.IsNullOrEmpty(fileExt))
                    {
                        return Json(new FileUploadEntity { ErrorCode = "10000", ErrorMessage = "请上传PDF、RAR、ZIP格式的文档！" });
                    }

                    string originFileName = Regex.Replace(file.FileName, "." + fileExt, "." + fileExt, RegexOptions.IgnoreCase);
                    string savename = Guid.NewGuid().ToString("N");
                    if (!string.IsNullOrEmpty(filename))
                        savename = filename.Replace(string.Format("{0}_", LoginHelper.UserId), "");
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

                    T_BG_UpFiles bgUpFiles = new T_BG_UpFiles { filename = fn, filehash = FileHelper.GetFileHash(newFilePath), userid = lUserId, fileoriginname = originFileName, filepath = newFilePath, ext = fileExt, filetype = 0, addtime = DateTime.Now, type = saveType.ToInt(0) };

                    string fileorigin = originFileName.ReplaceSqlTag();
                    long cnt = _bgUpFilesService.GetListByUserIdAndFileNameAndExt(originFileName, lUserId, fileExt, saveType.ToInt(0));
                    long reslt = _bgUpFilesService.Add(bgUpFiles);
                    if (reslt > 0)
                    {
                        string oname = fileorigin.ToLower().Replace("." + bgUpFiles.ext.ToLower(), "");
                        string attach = cnt > 0
                            ? string.Format("{0}.{1}", oname, bgUpFiles.ext)
                            : string.Format("{0}({1}).{2}", oname, cnt, bgUpFiles.ext);

                        return Json(new FileUploadEntity { ErrorCode = "00000", ErrorMessage = fn, Attach = attach, FilePath = bgUpFiles.filepath, FileType = fileExt });
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

        public JsonResult DelFile()
        {
            string filename = CommonHelper.GetPostValue("filename");
            T_BG_UpFiles upFiles = _bgUpFilesService.GetListByFilename(filename);
            if (upFiles==null)
            {
                return Json(new FileUploadEntity { ErrorCode = "E0001", ErrorMessage = "未获得文件信息" });
            }
            string filePath = upFiles.filepath;
            if (string.IsNullOrEmpty(filePath))
            {
                return Json(new FileUploadEntity { ErrorCode = "E0001", ErrorMessage = "未获得文件地址" });
            }
            filePath = HttpUtility.UrlDecode(filePath);
            if (Directory.Exists(filePath))
            {
                Directory.Delete(filePath,false);
                return Json(new FileUploadEntity { ErrorCode = "E000", ErrorMessage = "删除成功" });
            }
            else
            {
                return Json(new FileUploadEntity { ErrorCode = "E0001", ErrorMessage = "该文件不存在" });
            }
        }
    }
}