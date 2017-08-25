using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using koala.application.common;
using match.application.common;
using mq.application.common;
using mq.application.service;
using mq.application.service.Interface;
using mq.model.dbentity;

namespace mq.ui.Email.Controllers
{
    public class MailController : Controller
    {
        private readonly IBgShopService _bgShopService;
        private readonly IBgUserService _bgUserService;
        private readonly IBgEmailService _bgEmailService;
        private readonly IBgEmailRecieverService _bgEmailRecieverService;
        private readonly IBgVEmailRecieverService _bgVEmailRecieverService;
        public MailController(IBgShopService bgShopService, IBgUserService bgUserService, IBgEmailService bgEmailService, IBgEmailRecieverService bgEmailRecieverService, IBgVEmailRecieverService bgVEmailRecieverService)
        {
            _bgShopService = bgShopService;
            _bgUserService = bgUserService;
            _bgEmailService = bgEmailService;
            _bgEmailRecieverService = bgEmailRecieverService;
            _bgVEmailRecieverService = bgVEmailRecieverService;
        }

        // GET: Mail
        public ActionResult Send()
        {
            List<T_BG_User> users = _bgUserService.GetList();
            return View(users);
        }

        public ActionResult SendList()
        {
            long userId = LoginHelper.UserId;
            List<V_BG_Email_Reciever> list = _bgVEmailRecieverService.GetList(userId);
            return View(list);
        }

        public JsonResult SendEmail()
        {
            string users = CommonHelper.GetPostValue("usernames");
            string title = CommonHelper.GetPostValue("title");
            string content = CommonHelper.GetPostValue("content");
            //string sendEmail = CommonHelper.GetPostValue("sendEmail");
            string fileurl = CommonHelper.GetPostValue("fileurl");
            string fileorgname = CommonHelper.GetPostValue("fileorgname");
            string fileext = CommonHelper.GetPostValue("fileext");

            title = HttpUtility.UrlDecode(title);
            content = HttpUtility.UrlDecode(content);
            fileurl = HttpUtility.UrlDecode(fileurl);
            fileorgname = HttpUtility.UrlDecode(fileorgname);

            if (string.IsNullOrEmpty(users))
            {
                return Json(new { ErrorCode = "E001", ErrorMessage = "參數錯誤！" });
            }
            string[] recieverUsers = users.Split(';');
            if (recieverUsers.Length <= 0)
            {
                return Json(new { ErrorCode = "E001", ErrorMessage = "參數数量錯誤！" });
            }

            T_BG_Email email = new T_BG_Email();
            email.SendUserId = LoginHelper.UserId;
            email.SendUserName = LoginHelper.UserName;
            email.Title = title;
            email.Context = content;
            email.AddTime = DateTime.Now;
            email.FileUrl = fileurl;
            email.FileName = fileorgname;
            email.FileExt = fileext;
            email.IsDel = 0;
            email.Lvl = 0;
            email.Recievers = users;
            long result = _bgEmailService.Add(email);

            if (result <= 0)
            {
                return Json(new { ErrorCode = "E001", ErrorMessage = "邮件信息添加失败！" });
            }

            List<T_BG_EmailReciever> emailRecieverList = recieverUsers.Select(user => new T_BG_EmailReciever { EmailId = result,state = 0,RevieverUserId = user.ToLong(-1L) }).ToList();

            bool reslt = _bgEmailRecieverService.BatchAdd(emailRecieverList);
            if (reslt)
            {
                return Json(new { ErrorCode = "E000", ErrorMessage = "邮件信息添加成功！" });
            }
            else
            {
                return Json(new { ErrorCode = "E002", ErrorMessage = "邮件发送人添加失败！" });
            }

            #region 发送邮件

            //MailMessage message = new MailMessage();
            //            message.From = new MailAddress("1244952898@qq.com","MAX QUILL");

            //            string[] emailString = users.Split(';');
            //            if (emailString.Length > 0)
            //            {
            //                foreach (var address in emailString)
            //                {
            //                    message.To.Add(address);
            //                }
            //            }
            //            message.Subject = title;
            //            if (content != null) message.Body = content;
            //            message.IsBodyHtml = true;
            //            Attachment data = new Attachment(fileurl, MediaTypeNames.Application.Octet);
            //            ContentDisposition disposition = data.ContentDisposition;
            //            disposition.CreationDate = System.IO.File.GetCreationTime(fileurl);
            //            disposition.ModificationDate = System.IO.File.GetLastWriteTime(fileurl);
            //            disposition.ReadDate = System.IO.File.GetLastAccessTime(fileurl);

            //            SmtpClient client = new SmtpClient("smtp.qq.com")
            //            {
            //                EnableSsl = true,
            //                UseDefaultCredentials = false,
            //                DeliveryMethod = SmtpDeliveryMethod.Network,
            //                Credentials = new System.Net.NetworkCredential("1244952898@qq.com", "naapvvgtiwhlfhgd")
            //            };
            //            try
            //            {
            //                client.Send(message);
            //            }
            //            catch (Exception ex)
            //            {
            //                return Json(new { ErrorCode = "E001", ErrorMessage = "郵件發送失敗！" });
            //            }

            #endregion

        }

        public JsonResult DelEmail()
        {
            long id = CommonHelper.GetPostValue("id").ToLong(-1L);
            if (id <= 0)
            {
                return Json(new { ErrorCode = "E001", ErrorMessage = "参数缺失！" });
            }

            T_BG_Email email = _bgEmailService.Get(id);
            if (email == null)
            {
                return Json(new { ErrorCode = "E002", ErrorMessage = "未发现该邮件！" });
            }
            if (email.IsDel == 1)
            {
                return Json(new { ErrorCode = "E000", ErrorMessage = "该邮件已经被删除！" });
            }
            email.IsDel = 1;

            if (_bgEmailService.Update(email))
            {
                return Json(new { ErrorCode = "E000", ErrorMessage = "邮件删除成功！" });
            }
            else
            {
                return Json(new { ErrorCode = "E004", ErrorMessage = "邮件删除失败！" });
            }
        }

        //public JsonResult GetJsonData()
        //{
        //    List<T_BG_User> users = _bgUserService.GetList();
        //    List<T_BG_Shop> shops = _bgShopService.List();
        //}

        public void DownloadFilePathResult()
        {
            string filepath = CommonHelper.GetPostValue("filepath");
            string filename = CommonHelper.GetPostValue("filename");
            filepath = HttpUtility.UrlDecode(filepath);
            filename = HttpUtility.UrlDecode(filename);
        
            try
            {
                WebClient client = new WebClient();
                string receivePath = @"F:\";
                client.DownloadFile(filepath, receivePath+Path.GetFileName(filepath));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult DownloadFilePath()
        {
            string filepath = CommonHelper.GetPostValue("filepath");
            string filename = CommonHelper.GetPostValue("filename");
            filepath = HttpUtility.UrlDecode(filepath);
            filename = HttpUtility.UrlDecode(filename);
            string filetype = CommonHelper.GetPostValue("filetype");
            filepath = HttpUtility.UrlDecode(filepath);

            if (string.IsNullOrEmpty(filepath))
            {
                return RedirectToAction("Error", "Menu", new { ErrorCode = "E0013", ErrorMsg = ErrorInfoHelper.GetErrorValue("E0013") });
            }

            //string type;
            //if (filetype.Contains(".doc"))
            //{
            //    type = "application/msword";
            //}
            //else if (filetype.Contains(".pdf"))
            //{
            //    type = "application/pdf";
            //}
            //else
            //{
            //    return RedirectToAction("Error", "Menu", new { ErrorCode = "E0012", ErrorMsg = ErrorInfoHelper.GetErrorValue("E0012") });
            //}
            return File(filepath, filetype, filename);
        }

    }
}