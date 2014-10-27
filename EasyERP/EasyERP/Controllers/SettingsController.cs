using EasyERP.Context;
using EasyERP.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyERP.Controllers
{
    public class SettingsController : Controller
    {
        DBContext db = new DBContext();
        // GET: Settings
        public ActionResult Basic()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SettingsUsers()
        {
            var users = db.Users.ToList();
            ViewBag.User = CreateUserDefault();
            return PartialView("_SettingsUsers", users);
        }

        [HttpGet]
        public ActionResult SettingsCompany()
        {
            var company = db.Companies.FirstOrDefault();
            if (company == null)
                company = new Company();
            return PartialView("_SettingsCompany", company);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SettingsCompany(Company company)
        {
            if (ModelState.IsValid)
            {
                var companyOld = db.Companies.Find(company.CompanyId);
                if (companyOld == null)
                {
                    db.Companies.Add(company);
                    db.SaveChanges();
                }
                else
                {
                    using (db = new DBContext())
                    {
                        db.Entry(company).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            return RedirectToAction("Basic");
        }

        [HttpPost]
        public ActionResult AddUser(User user)
        {
            try
            {
                var checkUser = db.Users.Where(u => u.Name == user.Name).FirstOrDefault();
                if (checkUser != null)
                    throw new Exception("Uzytkownik o podanym Loginie już istnieje.");
                user.EnteredOn = DateTime.Now;
                db.Users.Add(user);
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                //ViewBag.User = user;
                //return PartialView("_SettingsUsers", db.Users.ToList());
            }
            return View("Basic");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = db.Users.Find(id);
            return PartialView("_EditUser", user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
            }
            ViewBag.User = null;
            ViewBag.User = CreateUserDefault();
            return PartialView("_SettingsUsers", db.Users.ToList());
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            ViewBag.User = null;
            ViewBag.User = CreateUserDefault();
            return PartialView("_SettingsUsers", db.Users.ToList());

        }

        [HttpGet]
        public ActionResult SettingsPassword()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private User CreateUserDefault()
        {
            var user = new User();
            user.Name = "Login";
            user.Password = "Hasło";
            user.FirstName = "Imię";
            user.LastName = "Nazwisko";
            user.Email = "E-mail";
            return user;
        }
    }
}