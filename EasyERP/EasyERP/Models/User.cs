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
        [Required(ErrorMessage="Pole imię jest wymagane")]
        public string FirstName { get; set; }
        [DisplayName("Nazwisko")]
        [Required(ErrorMessage = "Pole imię jest wymagane")]
        public string LastName { get; set; }
        [DisplayName("Nazwa użytkownika")]
        [Required(ErrorMessage = "Login jest wymagana")]
        public string Name { get; set; }
        [DisplayName("Hasło użytkownika")]
        [Required(ErrorMessage = "Hasło jest wymagane")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DisplayName("Zapamiętać")]
        public bool RememberMe { get; set; }
        [DisplayName("Data dodania")]
        [DataType(DataType.Date)]
        public DateTime? EnteredOn { get; set; }
        [DisplayName("E-mail")]
        [Required(ErrorMessage = "Pole imię jest wymagane")]
        [EmailAddress(ErrorMessage="Adres e-mail ma nie poprawną formę")]
        public string Email { get; set; }
    }
}