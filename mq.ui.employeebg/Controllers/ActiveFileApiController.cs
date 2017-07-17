using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using mq.application.common;
using mq.application.enumlib;
using mq.application.service;
using mq.model.viewentity;

namespace mq.ui.employeebg.Controllers
{
    public class ActiveFileApiController : ApiController
    {
        //private readonly IActiveFileDepartmentExtendService _activeFileDepartmentExtendService;
        private readonly IBgVActiveFileDepartmentService _bgVActiveFileDepartmentService;

        public ActiveFileApiController(IBgVActiveFileDepartmentService bgVActiveFileDepartmentService)
        {
            _bgVActiveFileDepartmentService = bgVActiveFileDepartmentService;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.HttpGet]
        public JsonActiveFileApiGetList GetList()
        {
            int type = CommonHelper.GetPostValue("type").ToInt(0);
            ActiveFileTypeEnum typ = (ActiveFileTypeEnum) type;
            JsonActiveFileApiGetList json = new JsonActiveFileApiGetList
            {
                ErrorCode = "E000",
                ErrorMessage = "Success",
                List = _bgVActiveFileDepartmentService.GetListForDel(typ)
            };
            return json;
        }

    }
}
