using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EasyERP.Models
{
    public class Product
    {
        //DOTO
        public Product()
        {
            //Orders = new List<Order>();
        }
        [Key]
        [DisplayName("Id produktu")]
        public int ProductId { get; set; }
        [DisplayName("Nazwa produktu")]
        public string Name { get; set; }
        [DisplayName("Kod produktu")]
        public string Code { get; set; }
        [DisplayName("Ilość")]
        public int Amount { get; set; }
        [DisplayName("Cena netto")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        public decimal ListPrice { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        [DisplayName("Cena brutto")]
        public decimal PurchasePrice { get; set; }
        [DisplayName("Ilość w paczce")]
        public int QuantityPackage { get; set; }
        [DisplayName("Kategoria")]
        public int Category { get; set; }
        [DisplayName("Data dostawy")]
        [DataType(DataType.Date)]
        public DateTime? DeliveryDate { get; set; }
        [DisplayName("Opis produktu")]
        public string Description { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}