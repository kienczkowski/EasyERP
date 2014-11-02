using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EasyERP.Models
{
    public class Task
    {
        [Key]
        [DisplayName("Numer zadania")]
        public int TaskId { get; set; }
        [Required(ErrorMessage = "Typ zadania jest wymagany")]
        [DisplayName("Typ zadania")]
        [Range(1, 3, ErrorMessage="Typ zadnia jest wymagany")]
        public int TaskType { get; set; }
        [DisplayName("Priorytet")]
        [Required(ErrorMessage = "Priorytet zadania jest wymagany")]
        [Range(1, 3, ErrorMessage="Priorytet zadania jest wymagany")]
        public int Priority { get; set; }
        [DisplayName("Data zadania")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Data zadania jest wymagana")]
        public DateTime? TaskDate { get; set; }
        [DisplayName("Opis zadania")]
        [Required(ErrorMessage = "Opis zadania jest wymagany")]
        public string Description { get; set; }
        [DisplayName("Status zadania")]
        [DefaultValue(1)]
        public int Status { get; set; }

        public virtual User User { get; set; }
    }
}