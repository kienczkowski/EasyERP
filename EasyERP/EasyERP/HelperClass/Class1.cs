using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyERP.Context;
using EasyERP.Models;
using EasyERP.Models.HelpModels;

namespace EasyERP.Controllers
{
    [Authorize]
    public class OrdersController1 : Controller
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

        public ActionResult Order()
        {
            return View(db.Orders.ToList());
        }

        [HttpGet]
        public ActionResult AddOrder(int ClientId)
        {
            AddOrderProducts model = new AddOrderProducts();
            model.Orders = GetOrders(ClientId);
            model.Products = GetAllProducts();
            if (NumberProducts != null)
                model.Basket = GetBasketProduct();
            model.ClientId = ClientId;
            return View(model);
        }

        [HttpPost]
        [ActionName("AddOrder")]
        public ActionResult AddOrderPost(int ClientId)
        {
            AddOrderProducts model = new AddOrderProducts();
            try
            {
                using (var dbContext = new DBContext())
                {

                    List<Product> basketProduct = GetBasketProduct();
                    if (basketProduct.Count == 0)
                    {
                        model.Products = GetAllProducts();
                        model.Orders = GetOrders(ClientId);
                        model.ClientId = ClientId;
                        throw new Exception("Nie wybrano żadengo produktu");
                    }
                    Client client = dbContext.Clients.Find(ClientId);
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
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(model);
            }
            return RedirectToAction("DetailsClient", "Clients", new { id = ClientId });
        }

        [HttpPost]
        public ActionResult AddProductToOrder(AddOrderProducts model)
        {
            return View("AddOrder", model);
        }

        [HttpGet]
        public ActionResult AddProductToBasket(int ProductId, int ClientId)
        {
            NumberProducts += ProductId + ",";
            return RedirectToAction("AddOrder", new { ClientId = ClientId });
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