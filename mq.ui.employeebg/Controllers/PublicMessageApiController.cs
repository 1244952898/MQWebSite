using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using log4net;
using match.application.common;
using mq.application.common;
using mq.application.service;
using mq.model.dbentity;
using mq.model.viewentity;

namespace mq.ui.employeebg.Controllers
{
    public class PublicMessageApiController : ApiController
    {
        ILog logger = LogManager.GetLogger(typeof(PublicMessageApiController));
        private readonly IBgPublishFileService _bgPublishFileService;
        public PublicMessageApiController(IBgPublishFileService bgPublishFileService)
        {
            _bgPublishFileService = bgPublishFileService;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.HttpGet]
        public JsonPublishFileEntity GetPublishFile(long id=-1)
        {
            //long id = CommonHelper.GetConfigValue("Id").ToLong(-1);
            JsonPublishFileEntity json = new JsonPublishFileEntity();
            try
            {
                if (id <= 0)
                {
                    json.ErrorCode = "E0001";
                    json.ErrorMessage = ErrorInfoHelper.GetErrorValue(json.ErrorCode);
                    return json;
                }
                json.ErrorCode = "E0000";
                json.ErrorMessage = ErrorInfoHelper.GetErrorValue(json.ErrorCode);
                json.PublishFile = _bgPublishFileService.GetPublishFileById(id);
            }
            catch (Exception ex)
            {
                json.ErrorCode = "E0011";
                json.ErrorMessage = ErrorInfoHelper.GetErrorValue(json.ErrorCode);
                logger.ErrorFormat("具体位置={0},Message={1},StackTrace={2},Source={3},重要参数Id={4}", "PublicMessageApiController-GetPublishFile", ex.Message, ex.StackTrace, ex.Source, id);
            }
            return json;
        }

    }
}
