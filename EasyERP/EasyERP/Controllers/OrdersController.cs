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
using PagedList;

namespace EasyERP.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private DBContext db = new DBContext();

        private List<Basket> Basket
        {
            get
            {
                if (Session["NumberProducts"] == null)
                    return new List<Basket>();
                else
                    return Session["NumberProducts"] as List<Basket>;
            }
            set
            {
                Session["NumberProducts"] = value;
            }
        }

        // GET: Orders
        public ActionResult Orders(int? clientId, int? page)
        {
            if (clientId == null)
            {
                int pageNumber = page ?? 1;
                List<Order> orders = db.Orders.ToList();
                return View(orders.ToPagedList(pageNumber, Global.Global.pageSize));
            }
            else
                return View(db.Orders.Where(o => o.Client.ClientId == clientId).ToList());
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
                model.Order.Seller = this.Session["FirstName"] != null && this.Session["LastName"] != null ? this.Session["FirstName"].ToString() + " " + this.Session["LastName"].ToString() : "";
            }
            else
            {
                model.Orders = GetOrders(ClientId);
                model.ClientId = ClientId;
            }

            model.Products = GetAllProducts();
            if (Basket != null)
                model.Basket = Basket;
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
                        decimal sallary1 = 0;
                        decimal sallary2 = 0;
                        List<Basket> basketProduct = Basket;
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
                        if (model.Order.EndDate != null)
                            newOrder.EndDate = model.Order.EndDate;
                        newOrder.Seller = this.Session["FirstName"] != null && this.Session["LastName"] != null ? this.Session["FirstName"].ToString() + this.Session["LastName"].ToString() : "Brak";
                        newOrder.Client = client;
                        foreach (Basket product in basketProduct)
                        {
                            Product item = dbContext.Products.Find(product.ProductId);
                            newOrder.ProductOrders.Add(new ProductOrders() { ProductId = product.ProductId, OrderId = model.Order.OrderId, Amount = product.Amount });
                            sallary1 += product.Product.ListPrice;
                            sallary2 += product.Product.PurchasePrice;
                        }
                        newOrder.ListPrice = sallary1;
                        newOrder.PurchasePrice = sallary2;
                        dbContext.Orders.Add(newOrder);
                        dbContext.SaveChanges();
                        Basket = null;
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
            Basket = null;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            AddOrderProducts model = new AddOrderProducts();
            model.Clients = db.Clients.ToList();
            model.Order = order;
            model.ClientId = order.Client.ClientId;
            model.Products = GetAllProducts();
            model.Basket = GetBasket(order.ProductOrders);
            return View(model);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AddOrderProducts model)
        {
            if (ModelState.IsValid)
            {
                decimal sallary1 = 0;
                decimal sallary2 = 0;
                List<Basket> basketProduct = Basket;
                if (basketProduct.Count == 0)
                {
                    model.Products = GetAllProducts();
                    model.Orders = GetOrders(model.ClientId);
                    model.ClientId = model.ClientId;
                    throw new Exception("Nie wybrano żadengo produktu");
                }
                Order newOrder = db.Orders.Find(model.Order.OrderId);
                newOrder.ProductOrders = new List<ProductOrders>();
                foreach (Basket item in basketProduct)
                {
                    newOrder.ProductOrders.Add(new ProductOrders() { ProductId = item.ProductId, Amount = item.Amount, OrderId = newOrder.OrderId });
                    Product product = db.Products.Find(item.ProductId);
                    sallary1 += product.ListPrice;
                    sallary2 += product.PurchasePrice;
                }
                newOrder.Seller = model.Order.Seller;
                newOrder.StartDate = model.Order.StartDate;
                newOrder.EndDate = model.Order.EndDate;
                db.Entry(newOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Orders");
            }
            return View(model);
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
        public ActionResult AddProductToBasket(int ProductId, int ClientId, int Amount)
        {
            bool flag = true;
            Product product = db.Products.Find(ProductId);
            List<Basket> basket = Basket;
            foreach (Basket thisSame in basket)
            {
                if (thisSame.ProductId == ProductId && thisSame.Amount == Amount)
                {
                    flag = false;
                    break;
                }
                else if (thisSame.ProductId == ProductId && thisSame.Amount != Amount)
                {
                    flag = false;
                    thisSame.Amount = Amount;
                    break;
                }
            }
            if (flag)
            {
                Basket item = new Basket() { ProductId = ProductId, Amount = Amount, Product = product };
                basket.Add(item);
            }
            Basket = basket;
            return PartialView("_Products", Basket);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public static List<Order> GetOrders(int? ClientId)
        {
            List<Order> orders = null;
            using (DBContext db = new DBContext())
            {
                var order = from c in db.Orders select c;
                orders = order.ToList();
            }
            return orders;
        }

        public List<Product> GetAllProducts()
        {
            return db.Products.ToList();
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

        private List<Basket> GetBasket(ICollection<ProductOrders> productOrders)
        {
            List<Basket> basket = new List<Basket>();
            foreach (ProductOrders item in productOrders)
            {
                Product product = db.Products.Find(item.ProductId);
                basket.Add(new Basket() { Product = product, Amount = item.Amount, ProductId = item.ProductId });
            }
            return basket;
        }
    }
}
