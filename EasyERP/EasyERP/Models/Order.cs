using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EasyERP.Models
{
    public class Order
    {
        public Order()
        {
            Products = new List<Product>();
        }
        [Key]
        [DisplayName("Numer zamówienia")]
        public int OrderId { get; set; }
        [DisplayName("Sprzedawca przyjmujący")]
        public string Seller { get; set; }
        [DisplayName("Data utworzenia")]
        public DateTime? StartDate { get; set; }
        [DisplayName("Data zakończenia")]
        public DateTime? EndDate { get; set; }
        [DisplayName("Cena netto")]
        public decimal ListPrice { get; set; }
        [DisplayName("Cena brutto")]
        public decimal PurchasePrice { get; set; }

        public virtual Client Client { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}