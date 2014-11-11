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
using Aspose.Words;
using System.IO;

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
            {
                int pageNumber = page ?? 1;
                List<Order> orders = db.Orders.Where(o => o.Client.ClientId == clientId).ToList();
                return View(orders.ToPagedList(pageNumber, Global.Global.pageSize));
            }
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
                        newOrder.Seller = this.Session["FirstName"] != null && this.Session["LastName"] != null ? this.Session["FirstName"].ToString() + " " + this.Session["LastName"].ToString() : "Brak";
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
            Basket = GetBasket(order.ProductOrders);
            model.Basket = Basket;
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
                try
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
                }
                catch (Exception ex)
                {
                    Basket = null;
                }
                Basket = null;
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
                else
                    flag = true;
            }
            if (flag)
            {
                Basket item = new Basket() { ProductId = ProductId, Amount = Amount, Product = product };
                basket.Add(item);
            }
            Basket = basket;
            return PartialView("_Products", Basket);
        }

        [HttpGet]
        public ActionResult CreateInvoice(int orderId)
        {
            byte[] templateByte = db.Templates.Where(t => t.IsActive == true).Select(s => s.TemplateFile).FirstOrDefault();
            byte[] contents = null;
            Order order = db.Orders.Find(orderId);
            if(templateByte == null)
            {
                ViewBag.Error = "Nie aktualnie żadnych aktywnych szablonów faktur";
                return View("Details", order);
            }
            MemoryStream msIn = new MemoryStream(templateByte);
            Document docTemplate = new Document(msIn);
            msIn.Dispose();
            msIn.Close();
            //zwracamy plik do użytkownika
            Company company = db.Companies.FirstOrDefault();
            Client client = db.Clients.Find(order.Client.ClientId);
            List<ProductOrders> productOrders = db.ProductOrders.Where(p => p.OrderId == order.OrderId).ToList();
            Dictionary<Product, int> products = new Dictionary<Product, int>();
            foreach (ProductOrders item in productOrders)
            {
                Product product = db.Products.Find(item.ProductId);
                products.Add(product, item.Amount);
            }
            Dictionary<string, string> freeMergeFields = new Dictionary<string, string>();

            freeMergeFields.Add("NameCompany", company.NameCompany);
            freeMergeFields.Add("DateNow", DateTime.Now.ToString("dd-MM-yyyy"));
            freeMergeFields.Add("Address", company.Address);
            freeMergeFields.Add("PostalCode", "");
            freeMergeFields.Add("City", company.Country);
            freeMergeFields.Add("WebSiteAddress", company.WebSiteAddress);
            freeMergeFields.Add("PhoneNumber", company.PhoneNumber);
            freeMergeFields.Add("FaxNumber", company.FaxNumber);
            freeMergeFields.Add("Email", company.Email);
            freeMergeFields.Add("InvoiceNumber", order.OrderId.ToString());
            freeMergeFields.Add("ClientName", client.FirstName +  " " + client.LastName);
            freeMergeFields.Add("ClientCompany", client.Company);
            freeMergeFields.Add("ClientId", client.ClientId.ToString());
            freeMergeFields.Add("ClientAddress", client.Address);
            freeMergeFields.Add("ClientPostalCode", client.CountryCode);
            freeMergeFields.Add("ClientCity", client.City);
            freeMergeFields.Add("ClientPhoneNumber", client.Phone);
            freeMergeFields.Add("PayDate", order.EndDate.Value.Date.ToString("dd-MM-yyyy"));
            freeMergeFields.Add("Seller", order.Seller);
            freeMergeFields.Add("DeliverDate", DateTime.Now.AddDays(3).ToString("dd-MM-yyyy"));
            freeMergeFields.Add("MethodSend", "Poczta Polska");

            DataTable dtProducts = new DataTable();
            dtProducts.TableName = "Products";
            dtProducts.Columns.Add("Amount");
            dtProducts.Columns.Add("ProductId");
            dtProducts.Columns.Add("Description");
            dtProducts.Columns.Add("PurchasePrice");
            dtProducts.Columns.Add("ProductDiscount");

            decimal suma = 0;

            foreach (KeyValuePair<Product, int> item in products)
            {
                DataRow dr = dtProducts.NewRow();
                dr["Amount"] = item.Value;
                dr["ProductId"] = item.Key.ProductId.ToString();
                dr["Description"] = item.Key.Description;
                dr["PurchasePrice"] = item.Key.PurchasePrice;
                dr["ProductDiscount"] = "0%";
                suma += item.Key.PurchasePrice;
                dtProducts.Rows.Add(dr);
            }

            freeMergeFields.Add("Suma", suma.ToString());
            freeMergeFields.Add("Discount", "0%");
            freeMergeFields.Add("tax", "23%");

            docTemplate.MailMerge.Execute(freeMergeFields.Keys.ToArray(), freeMergeFields.Values.ToArray());
            docTemplate.MailMerge.ExecuteWithRegions(dtProducts);

            using(MemoryStream msOut = new MemoryStream())
            {
                docTemplate.Save(msOut, SaveFormat.Pdf);
                contents = msOut.ToArray();
            }
            msIn.Dispose();
            msIn.Close();
            Response.AddHeader("Content-Disposition", "inline; filename=test.pdf");
            return File(contents, "application/pdf");

            //doc
            //Response.AddHeader("Content-Disposition", "inline; filename=test.doc");
            //return File(contents, "aplication/force-download");
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
