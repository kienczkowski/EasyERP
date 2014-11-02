using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using EasyERP.Context;
using EasyERP.HelperClass;
using EasyERP.Models;

namespace EasyERP.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
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
                    List<User> users = db.Users.Where(u => u.Name == user.Name && u.Password == password).ToList();
                    if (users.Count > 0)
                    {
                        FormsAuthentication.SetAuthCookie(user.Name, false);
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