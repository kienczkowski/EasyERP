using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyERP.Context;
using EasyERP.Models;

namespace EasyERP.Repository
{
    public class LogErrorRepo
    {
        //private DBContext dbContext;
        
        public int AddLogError(LogError logError)
        {
            using(var dbContext = new DBContext())
            {
                dbContext.LogError.Add(logError);
                return dbContext.SaveChanges();
            }
        }
    }
}