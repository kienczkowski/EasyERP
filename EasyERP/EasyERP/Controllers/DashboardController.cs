using EasyERP.Context;
using EasyERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EasyERP.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private DBContext db = new DBContext();

        [HttpGet]
        public ActionResult Index()
        {
            if (this.Session["LoginName"] == null)
                this.Session["LoginName"] = User.Identity.Name;
            ViewData["TaskType"] = CreateTaskType();
            ViewData["Priority"] = CreatePriority();
            var user = db.Users.Where(m => m.Name == User.Identity.Name).FirstOrDefault();
            if(user == null)
                return HttpNotFound();
            if (Session["FirstName"] == null)
                Session["FirstName"] = user.FirstName;
            if(Session["LastName"] == null)
                    Session["LastName"] = user.LastName;
            var listTask = db.Tasks.ToList();
            return View(listTask);
        }

        [HttpGet]
        public ActionResult AddTask(string data)
        {
            ViewData["TaskType"] = CreateTaskType();
            ViewData["Priority"] = CreatePriority();
            var task = new Task();
            task.TaskId = 0;
            task.Priority = 0;
            if (!string.IsNullOrEmpty(data))
                task.TaskDate = DateTime.Parse(data);
            return PartialView("_CreateTask", task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTask(Task task)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Where(m => m.Name == User.Identity.Name).FirstOrDefault();
                if (user == null)
                    return HttpNotFound();
                task.User = user;
                task.Status = 1;
                db.Tasks.Add(task);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
                return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult SelectTask(int TaskId)
        {
            ViewData["TaskType"] = CreateTaskType();
            ViewData["Priority"] = CreatePriority();
            var task = db.Tasks.Find(TaskId);
            return PartialView("_DetailsTask", task);
        }

        [HttpGet]
        public ActionResult DeleteTask(int? taskId)
        {
            if (taskId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(taskId);
            if (task == null)
            {
                return HttpNotFound();
            }
            db.Tasks.Remove(task);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult FinishTask(int? taskId)
        {
            if(taskId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(taskId);
            task.Status = 2; //zakończone
            db.Entry(task).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private IEnumerable<SelectListItem> CreateTaskType()
        {
            //Typ zadania
            //Ważne
            //Spóznione
            //Pilne
            var listItem = new List<SelectListItem>();
            listItem.Add(new SelectListItem() { Text = "Typ zadania", Value = "0" });
            listItem.Add(new SelectListItem() { Text = "Ważne", Value = "1" });
            listItem.Add(new SelectListItem() { Text = "Spóznione", Value = "2" });
            listItem.Add(new SelectListItem() { Text = "Pilne", Value = "3" });

            return listItem;
        }

        private IEnumerable<SelectListItem> CreatePriority()
        {
            //Priorytet
            //Ważne
            //Spóznione
            //Pilne
            var listItem = new List<SelectListItem>();
            listItem.Add(new SelectListItem() { Text = "Priorytet", Value = "0" });
            listItem.Add(new SelectListItem() { Text = "Wysoki", Value = "1" });
            listItem.Add(new SelectListItem() { Text = "Średni", Value = "2" });
            listItem.Add(new SelectListItem() { Text = "Niski", Value = "3" });

            return listItem;
        }
    }
}