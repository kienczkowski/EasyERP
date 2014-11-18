using EasyERP.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyERP.Models;
using Newtonsoft.Json;

namespace EasyERP.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        DBContext db = new DBContext();

        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public string Report1()
        {
            TopSeller topSeller = new TopSeller();

            List<User> usersList = db.Users.ToList();

            foreach (User item in usersList)
            {
                //Add Label 
                string seller = item.FirstName + " " + item.LastName;
                topSeller.label.Add(seller);
                List<decimal> ordersPrice = db.Orders.Where(o => o.Seller == seller).Select(o => o.PurchasePrice).ToList();
                decimal Price = 0;
                foreach (decimal price in ordersPrice)
                {
                    Price += price;

                }
                topSeller.value.Add(Price);

            }
            return JsonConvert.SerializeObject(topSeller);
        }

        [HttpPost]
        [AllowAnonymous]
        public string Report2()
        {
            TopSeller topSeller = new TopSeller();
            topSeller.label.AddRange(new string[] { "Styczeń", "Luty", "Marzec", "Kwiecień", "Maj", "Czerwiec", "Lipiec", "Sierpień", "Wrzesień", "Październik", "Listopad", "Grudzień" });

            for (int i = 1; i <= 12; i++)
            {
                DateTime dateStart = new DateTime(2014, i, 1);
                DateTime dateEnd = new DateTime(2014, i, DateTime.DaysInMonth(2014, i));
                List<decimal> ordersPrice = db.Orders.Where(o => o.StartDate >= dateStart && o.StartDate <= dateEnd).Select(o => o.PurchasePrice).ToList();
                decimal price = 0;
                foreach (decimal item in ordersPrice)
                {
                    price += item;
                }
                topSeller.value.Add(price);
            }
            return JsonConvert.SerializeObject(topSeller);
        }

        [HttpPost]
        [AllowAnonymous]
        public string Report3()
        {
            bool flag = true;
            TopSeller topSeller = new TopSeller();
            var list = from c in db.Clients
                       from o in db.Orders
                       where (c.ClientId == o.Client.ClientId)
                       select new { c.FirstName, c.LastName, o.PurchasePrice };
            var result = list
                        .GroupBy(a => new { a.FirstName, a.LastName })
                        .Select(a => new { Amount = a.Sum(b => b.PurchasePrice), Name = a.Key })
                        .OrderByDescending(a => a.Amount)
                        .ToList();
            foreach (var item in result)
            {
                topSeller.label.Add(item.Name.FirstName + " " + item.Name.LastName);
                topSeller.value.Add(item.Amount);
            }
            return JsonConvert.SerializeObject(topSeller);
        }
    }

    public class TopSeller
    {
        public TopSeller()
        {
            label = new List<string>();
            value = new List<decimal>();
        }
        public List<string> label { get; set; }
        public List<decimal> value { get; set; }
    }


}