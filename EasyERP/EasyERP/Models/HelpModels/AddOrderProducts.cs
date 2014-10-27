using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyERP.Models.HelpModels
{
    public class AddOrderProducts
    {
        public AddOrderProducts()
        {
            Orders = new List<Order>();
            Products = new List<Product>();
            Basket = new List<Product>();
        }
        public int ClientId { get; set; }
        public List<Order> Orders { get; set; }
        public List<Product> Products { get; set; }
        public List<Product> Basket { get; set; }
    }
}