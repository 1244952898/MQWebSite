using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using mq.application.common;

namespace AngularJsDemo.Controllers
{
    public class DemoApiController : ApiController
    {
        [HttpPost]
        [HttpGet]
        public JsonDemoApi GetApi()
        {
            string p = CommonHelper.GetPostValue("p");
            JsonDemoApi j = new JsonDemoApi
            {
                Name = "aa",
                Age = 32,
                Parm = p
            };
            return j;
        }

        [HttpPost]
        [HttpGet]
        public List<JsonDemoApi> GetListApi()
        {
            string p = CommonHelper.GetPostValue("p");
            string p1 = CommonHelper.GetPostValue("p1");
            List<JsonDemoApi> list = new List<JsonDemoApi>()
            {
                new JsonDemoApi{
                    Name = "aa",
                    Age = 1,
                    Parm = p
                },
                new JsonDemoApi
                {
                     Name = "bb",
                    Age = 2,
                    Parm = p1
                }
            };
            return list;
        }
    }

    public class JsonDemoApi
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Parm { get; set; }
    }
}
