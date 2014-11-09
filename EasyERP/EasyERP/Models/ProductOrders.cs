using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EasyERP.Models
{
    public class ProductOrders
    {
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int Amount { get; set; }
    }
}