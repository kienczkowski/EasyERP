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
            var user = new User();
            user.Name = "Login";
            user.Password = "Hasło";
            user.FirstName = "Imię";
            user.LastName = "Nazwisko";
            user.Email = "E-mail";
            ViewBag.User = user;
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
            
            return RedirectToAction("Basic");
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
    }
}