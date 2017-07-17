using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace mq.ui.publicmessage.Controllers
{
    public class TestApiController : ApiController
    {
        [HttpGet]
        [HttpPost]
        public string A()
        {
            return "aa";

        }
    }
}
