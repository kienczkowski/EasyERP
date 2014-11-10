using EasyERP.Context;
using EasyERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.IO;

namespace EasyERP.Controllers
{
    [Authorize]
    public class TemplatesController : Controller
    {
        DBContext db = new DBContext();
        // GET: Templates
        [HttpGet]
        public ActionResult Templates(int? page)
        {
            int pageNumber = page ?? 1;
            List<Template> templates = db.Templates.ToList();
            return PartialView("_SettingsTemplates", templates.ToPagedList(pageNumber, Global.Global.pageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
            Template temp = new Template();
            return View(temp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Template temp ,HttpPostedFileBase file)
        {
            try
            {
                if (file == null)
                {
                    ViewBag.Error = "Brak pliku";
                    return View();
                }
                var allowedExtensions = new[] { ".doc", ".docx"};
                var extension = Path.GetExtension(file.FileName);
                if (!allowedExtensions.Contains(extension))
                {
                    ViewBag.Error = "Plik musi mieć rozszerzenie *.doc lub *.docx";
                    return View();
                }
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    temp.TemplateFile = ms.ToArray();
                }
                temp.EnteredOn = DateTime.Now.Date;
                db.Templates.Add(temp);
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
            ViewBag.Success = "Szablon dodano pomyślnie";
            return View("~/Views/Settings/Basic.cshtml");
        }

        [HttpGet]
        public ActionResult Delete(int templateId)
        {
            Template temp = db.Templates.Find(templateId);
            db.Templates.Remove(temp);
            db.SaveChanges();
            return RedirectToAction("Templates");
        }
    }
}