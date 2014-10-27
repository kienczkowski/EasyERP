using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EasyERP.Models
{
    public class Company
    {
        [Key]
        [DisplayName("Numer firmy")]
        public int CompanyId { get; set; }
        [DisplayName("Nazwa firmy")]
        public string NameCompany { get; set; }
        [DisplayName("Kod firmy")]
        public string CompanyCode { get; set; }
        [DisplayName("Regon")]
        public string Regon { get; set; }
        [DisplayName("Nip")]
        public string Nip { get; set; }
        [DisplayName("Adres")]
        public string Address { get; set; }
        [DisplayName("Województwo")]
        public string Region { get; set; }
        [DisplayName("Kraj")]
        public string Country { get; set; }
        [DisplayName("Adres e-mail")]
        public string Email { get; set; }
        [DisplayName("Telefon")]
        public string PhoneNumber { get; set; }
        [DisplayName("Fax")]
        public string FaxNumber { get; set; }
        [DisplayName("Strona internetowa")]
        public string WebSiteAddress { get; set; }
        [DisplayName("Aktywna")]
        public bool IsActive { get; set; }
    }
}