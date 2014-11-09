using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyERP.Models.HelpModels
{
    public class Basket
    {
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public Product Product { get; set; }
    }
}