using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EasyERP.Controllers
{
    public class TaskController : ApiController
    {
        // GET: api/Task
        public IEnumerable<Percent> GetPercent()
        {
            int percent1 = new Random().Next(1, 100);
            int percent2 = 100 - percent1;

            Percent[] percent = new Percent[2];
            percent[0] = new Percent();
            percent[1] = new Percent();
            percent[0].PercentStart = percent1;
            percent[0].PercentEnd = percent2;
            percent[1].PercentStart = percent2;
            percent[1].PercentEnd = percent1;

            return percent;
            
        }

        // GET: api/Task/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Task
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Task/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Task/5
        public void Delete(int id)
        {
        }
    }

    public class Percent
    {
        public int PercentStart { get; set; }
        public int PercentEnd { get; set; }
    }
}
