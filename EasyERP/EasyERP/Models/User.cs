using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EasyERP.Models
{
    public class User
    {
        [Key]
        [DisplayName("Numer użytkownika")]
        public int UserId { get; set; }
        [DisplayName("Imię")]
        public string FirstName { get; set; }
        [DisplayName("Nazwisko")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Login jest wymagana")]
        [DisplayName("Nazwa użytkownika")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Hasło jest wymagane")]
        [DisplayName("Hasło użytkownika")]
        public string Password { get; set; }
        [DisplayName("Zapamiętać")]
        public bool RememberMe { get; set; }
        [DisplayName("Data dodania")]
        public DateTime? EnteredOn { get; set; }
        [DisplayName("E-mail")]
        public string Email { get; set; }
    }
}