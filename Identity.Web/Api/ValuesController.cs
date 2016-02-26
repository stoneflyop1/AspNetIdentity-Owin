using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Identity.Web.Api
{
    public class ValuesController : ApiController
    {
        [Authorize]
        public string[] GetAll()
        {
            return new[] { "v1", "v2" };
        }


    }
}
