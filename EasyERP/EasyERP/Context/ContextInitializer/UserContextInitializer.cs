using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using EasyERP.HelperClass;
using EasyERP.Models;

namespace EasyERP.Context.ContextInitializer
{
    public class UserContextInitializer : DropCreateDatabaseIfModelChanges<DBContext>
    {
        protected override void Seed(DBContext context)
        {
            User admin = new User() { Name = "admin", Password = Encryption.GetSaltedHash("admin", "admin"), RememberMe = false, EnteredOn = DateTime.Now };
            context.Users.Add(admin);
        }
    }
}