using EasyERP.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EasyERP.Controllers
{
    [Authorize]
    public class TaskController : ApiController
    {
        DBContext db = new DBContext();
        // GET: api/Task
        public IEnumerable<Percent> GetPercent(int id)
        {
            //Pobieramy task-i dla danego użytkownika
            int percent1 = 0;
            int percent2 = 0;
            if (!string.IsNullOrEmpty(id.ToString()))
            {
                int allTask = db.Tasks.Where(m => m.User.UserId == id).Count();
                int InFiniteTask = db.Tasks.Where(m => m.User.UserId == id && m.Status == 1).Count();
                percent2 = (InFiniteTask * 100) / allTask;
                percent1 = (100 - percent2);
            } 
            Percent[] percent = new Percent[2];
            percent[0] = new Percent() { PercentStart = percent1, PercentEnd = percent2 };
            percent[1] = new Percent() { PercentStart = percent2, PercentEnd = percent1 };
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
