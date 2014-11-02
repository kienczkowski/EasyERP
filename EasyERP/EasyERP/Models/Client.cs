using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EasyERP.Models
{
    public class Client
    {
        public Client()
        {
            Notes = new List<Note>() { new Note() };
            Orders = new List<Order>();
        }

        [Key]
        public int ClientId { get; set; }
        [Required(ErrorMessage = "Pole jest wymagane.")]
        public string Company { get; set; }
        [Required]
        public string CompanyCode { get; set; }
        [Required(ErrorMessage = "Pole jest wymagane.")]
        [DisplayName("Imię klienta")]
        public string FirstName { get; set; }
        public string Regon { get; set; }
        public string Nip { get; set; }
        [Required(ErrorMessage = "Pole jest wymagane.")]
        [DisplayName("Nazwisko klienta")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Pole jest wymagane.")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Pole jest wymagane.")]
        public string City { get; set; }
        [Required(ErrorMessage = "Pole jest wymagane.")]
        public string Region { get; set; }
        [Required(ErrorMessage = "Pole jest wymagane.")]
        public string CountryCode { get; set; }
        [Required(ErrorMessage = "Pole jest wymagane.")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Pole jest wymagane.")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Pole jest wymagane.")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Pole jest wymagane.")]
        public string WebSiteAddress { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}