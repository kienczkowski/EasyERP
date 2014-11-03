using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyERP.Models.HelpModels
{
    public class AddOrderProducts
    {
        public AddOrderProducts()
        {
            Orders = new List<Order>();
            Products = new List<Product>();
            Basket = new List<Product>();
            Clients = new List<Client>();
            Order = new Order();
        }
        [Required(ErrorMessage=("Pole klient jest wymagane"))]
        public int ClientId { get; set; }
        public Order Order { get; set; }
        public List<Order> Orders { get; set; }
        public List<Product> Products { get; set; }
        public List<Product> Basket { get; set; }
        public IEnumerable<Client> Clients { get; set; }
    }
}