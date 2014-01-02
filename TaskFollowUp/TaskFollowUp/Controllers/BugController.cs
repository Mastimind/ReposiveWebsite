using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TaskFollowUp.Controllers
{
    public class BugController : ApiController
    {
        // GET api/bug
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/bug/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/bug
        public void Post([FromBody]string value)
        {
        }

        // PUT api/bug/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/bug/5
        public void Delete(int id)
        {
        }
    }
}
