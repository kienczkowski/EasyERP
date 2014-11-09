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
            ProductOrders = new List<ProductOrders>();
        }
        [Key]
        [DisplayName("Numer zamówienia")]
        public int OrderId { get; set; }
        [Required]
        [DisplayName("Sprzedawca przyjmujący")]
        public string Seller { get; set; }
        [DisplayName("Data utworzenia")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }
        [DisplayName("Data zakończenia")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
        [DisplayName("Cena netto")]
        public decimal ListPrice { get; set; }
        [DisplayName("Cena brutto")]
        public decimal PurchasePrice { get; set; }

        public virtual Client Client { get; set; }
        public virtual ICollection<ProductOrders> ProductOrders { get; set; }
    }
}