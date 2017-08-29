using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using mq.application.common;
using mq.application.service;
using mq.model.dbentity;
using mq.model.viewentity;

namespace mq.ui.Email.Controllers
{
    public class MailApiController : ApiController
    {
        private readonly IBgEmailService _bgEmailService; 
        private readonly IBgEmailRecieverService _bgEmailRecieverService;
        public MailApiController(IBgEmailService bgEmailService,IBgEmailRecieverService bgEmailRecieverService)
        {
            _bgEmailService = bgEmailService;
            _bgEmailRecieverService = bgEmailRecieverService;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.HttpGet]
        public JsonMailApiGetModelEntity GetModel()
        {
            long emailId = CommonHelper.GetPostValue("EmailId").ToLong(-1L);
            JsonMailApiGetModelEntity result = new JsonMailApiGetModelEntity();
            if (emailId < 0)
            {
                result.ErrorCode = "E001";
                result.ErrorMessage = "未获得该邮件Id";
                return result;
            }

            result.Email = _bgEmailService.Get(emailId);
            if (result.Email == null)
            {
                result.ErrorCode = "E002";
                result.ErrorMessage = "未查询到该邮件Id";
                return result;
            }

            result.ErrorCode = "E000";
            result.ErrorMessage = "成功";
            return result;
        }

        public JsonBaseEntity UpdateRead()
        {
            long recieverEmailId = CommonHelper.GetPostValue("recieverEmailId").ToLong(-1L);
            if (recieverEmailId <= 0)
            {
                return new JsonBaseEntity { ErrorCode = "E001", ErrorMessage = "参数缺失！" };
            }
            T_BG_EmailReciever emailReciever = _bgEmailRecieverService.Get(recieverEmailId);
            if (emailReciever == null)
            {
                return  new JsonBaseEntity { ErrorCode = "E002", ErrorMessage = "未发现该邮件！" };
            }

            emailReciever.State = 1;

            if (_bgEmailRecieverService.Update(emailReciever))
            {
                return  new JsonBaseEntity { ErrorCode = "E000", ErrorMessage = "邮件删除成功！" };
            }
            else
            {
                return  new JsonBaseEntity {ErrorCode = "E004", ErrorMessage = "邮件删除失败！" };
            }
        }
    }
}
