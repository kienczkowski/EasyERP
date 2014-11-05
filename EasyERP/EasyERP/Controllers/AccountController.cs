using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using EasyERP.Context;
using EasyERP.HelperClass;
using EasyERP.Models;
using System.Data.Entity;

namespace EasyERP.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        DBContext db = new DBContext();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            try
            {
                using (var db = new DBContext())
                {
                    var password = Encryption.GetSaltedHash(user.Password, user.Name);
                    this.Session.Add("LoginName", user.Name);
                    this.Session.Add("IdUser", user.UserId);
                    User userFind = db.Users.Where(u => u.Name == user.Name && u.Password == password).FirstOrDefault();
                    if (userFind != null)
                    {
                        FormsAuthentication.SetAuthCookie(user.Name, user.RememberMe);
                        userFind.RememberMe = user.RememberMe;
                        db.Entry(userFind).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index", "Dashboard");
                    }
                    else
                        ViewBag.Error = "Nie poprawny login lub hasło.";
                }
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return View("Login");
        }
    }
}