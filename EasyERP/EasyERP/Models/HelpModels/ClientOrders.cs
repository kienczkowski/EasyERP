using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyERP.Models.HelpModels
{
    public class ClientOrders
    {
        public ClientOrders()
        {
            Orders = new List<Order>();
        }

        public Client Client { get; set; }
        public List<Order> Orders { get; set; }
    }
}