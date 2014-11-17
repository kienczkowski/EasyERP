using EasyERP.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyERP.Models;

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
        public JsonResult Report1()
        {
            JsonResult json = new JsonResult();
            List<string> labels = new List<string>();
            Dictionary<string, List<decimal>> data = new Dictionary<string, List<decimal>>();

            List<User> usersList = db.Users.ToList();

            foreach (User item in usersList)
            {
                //Add Label 
                string seller = item.FirstName + " " + item.LastName;
                labels.Add(seller);
                List<decimal> ordersPrice = db.Orders.Where(o => o.Seller == seller).Select(o => o.PurchasePrice).ToList();
                decimal Price = 0;
                foreach (decimal price in ordersPrice)
                {
                    Price += price;

                }
                data.Add(seller, new List<decimal>() { Price });

            }
            json.Data = data;
            return json;
        }
    }


}