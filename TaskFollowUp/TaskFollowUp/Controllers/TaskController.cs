using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TaskFollowUp.Controllers
{
    public class TaskController : ApiController
    {
        // GET api/task
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/task/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/task
        public void Post([FromBody]string value)
        {
        }

        // PUT api/task/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/task/5
        public void Delete(int id)
        {
        }
    }
}
