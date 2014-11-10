using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EasyERP.Context;
using EasyERP.Models;
using EasyERP.Models.HelpModels;
using PagedList;

namespace EasyERP.Controllers
{
    [Authorize]
    public class ClientsController : Controller
    {
        [HttpGet]
        public ActionResult AllClients(int? page)
        {
            return View(GetClients(page));
        }

        [HttpGet]
        public ActionResult AddClient()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddClient(Client client)
        {
            if (ModelState.IsValid)
            {
                using (var db = new DBContext())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            client.Notes.ToList()[0].EnteredOn = DateTime.Now;
                            client.Notes.ToList()[0].EnteredBy = "System";
                            client.CompanyCode = (db.Clients.Max(m => m.ClientId) + 1).ToString();
                            db.Clients.Add(client);
                            db.SaveChanges();

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            ViewBag.Error = ex.Message;
                            return View();
                        }
                    }
                }
                return RedirectToAction("AllClients");
            }
            return View();
        }

        
        [HttpGet]
        public ActionResult DetailsClient(int id)
        {
            ClientOrders ClientOrders = new ClientOrders();
            try
            {
                ClientOrders.Client = GetClient(id);
                ClientOrders.Orders = OrdersController.GetOrders(id);
                ViewBag.Edit = false;
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(ClientOrders);
        }

        [HttpGet]
        public ActionResult EditClient(int id)
        {
            Client client = null;
            try
            {
                client = GetClient(id);
                ViewBag.Edit = true;
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
            return View(client);
        }

        [HttpPost]
        public ActionResult EditClient(Client client, int? ClientId)
        {
            if (ModelState.IsValid)
            {
                client.ClientId = ClientId ?? 0;
                using (var db = new DBContext())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            db.Entry(client).State = EntityState.Modified;
                            db.SaveChanges();

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            ViewBag.Error = ex.Message;
                            return RedirectToAction("EditClient", new { id = client.ClientId });
                        }
                    }
                }
                return RedirectToAction("DetailsClient", new { id = client.ClientId });
            }
            return View();
        }

        private PagedList.IPagedList<Client> GetClients(int? page)
        {
            int pageNumber = page ?? 1;
            List<Client> Clients = new List<Client>();
            using (DBContext db = new DBContext())
            {
                Clients = db.Clients.ToList();
            }
            return Clients.ToPagedList(pageNumber, Global.Global.pageSize);
        }

        public static Client GetClient(int id)
        {
            Client client = null;
            using (DBContext db = new DBContext())
            {
                var clients = from c in db.Clients where c.ClientId == id select c;
                client = clients.First();
            }
            return client;
        }

        [HttpGet]
        public ActionResult EditClientAsync(int id)
        {
            Client model = GetClient(id);
            return PartialView("_ClientAddEdit", model);
        }
    }
}