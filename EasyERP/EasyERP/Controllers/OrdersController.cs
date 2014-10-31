using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EasyERP.Context;
using EasyERP.Models;
using EasyERP.Models.HelpModels;

namespace EasyERP.Controllers
{
    public class OrdersController : Controller
    {
        private DBContext db = new DBContext();

        private string NumberProducts
        {
            get
            {
                return Session["NumberProducts"] as string;
            }
            set
            {
                if (Session["NumberProducts"] != null)
                    Session["NumberProducts"] = value;
                else
                    Session.Add("NumberProducts", value);
            }
        }

        // GET: Orders
        public ActionResult Orders()
        {
            return View(db.Orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create(int ClientId)
        {
            AddOrderProducts model = new AddOrderProducts();
            if (ClientId == 0)
            {
                model.ClientId = 0;
                model.Clients = db.Clients.ToList();
            }
            else
            {
                model.Orders = GetOrders(ClientId);
                model.ClientId = ClientId;
            }

            model.Products = GetAllProducts();
            if (NumberProducts != null)
                model.Basket = GetBasketProduct();
            return View(model);
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddOrderProducts model)
        {
            //AddOrderProducts model = new AddOrderProducts();
            try
            {
                if (ModelState.IsValid)
                {
                    using (var dbContext = new DBContext())
                    {

                        List<Product> basketProduct = GetBasketProduct();
                        if (basketProduct.Count == 0)
                        {
                            model.Products = GetAllProducts();
                            model.Orders = GetOrders(model.ClientId);
                            model.ClientId = model.ClientId;
                            throw new Exception("Nie wybrano żadengo produktu");
                        }
                        Client client = dbContext.Clients.Find(model.ClientId);
                        Order newOrder = new Order();
                        newOrder.StartDate = DateTime.Now;
                        newOrder.Seller = this.Session["LoginName"] != null ? this.Session["LoginName"].ToString() : "Brak danych";
                        newOrder.Client = client;
                        foreach (Product product in basketProduct)
                        {
                            Product item = dbContext.Products.Find(product.ProductId);
                            newOrder.Products.Add(item);
                        }
                        dbContext.Orders.Add(newOrder);
                        dbContext.SaveChanges();
                        NumberProducts = null;
                    }
                }
                else
                {
                    model.Clients = db.Clients.ToList();
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(model);
            }
            return RedirectToAction("Orders", "Orders", new { id = model.ClientId });
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,Seller,StartDate,EndDate,ListPrice,PurchasePrice")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Orders");
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Orders");
        }

        [HttpGet]
        public ActionResult AddProductToBasket(int ProductId, int ClientId)
        {
            NumberProducts += ProductId + ",";
            return RedirectToAction("Create", new { ClientId = ClientId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public static List<Order> GetOrders(int ClientId)
        {
            List<Order> orders = null;
            using (DBContext db = new DBContext())
            {
                var order = from c in db.Orders select c;
                orders = order.ToList();
            }
            return orders;
        }

        public static List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            using (DBContext db = new DBContext())
            {
                var productsSelect = db.Products.ToList();
                products = productsSelect.ToList();
            }
            return products;
        }

        public static Product GetProductId(int id)
        {
            Product product = null;
            using (DBContext db = new DBContext())
            {
                var productsSelect = from c in db.Products where c.ProductId == id select c;
                product = productsSelect.FirstOrDefault();
            }
            return product;
        }

        private List<Product> GetBasketProduct()
        {
            List<Product> listProducts = new List<Product>();
            if (NumberProducts != null)
            {
                string[] numbers = NumberProducts.TrimEnd(',').Split(',');
                foreach (string number in numbers)
                {
                    listProducts.Add(GetProductId(int.Parse(number)));
                }
            }
            return listProducts;
        }
    }
}
