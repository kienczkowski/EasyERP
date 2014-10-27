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
        [DisplayName("Typ zadania")]
        public int TaskType { get; set; }
        [DisplayName("Priorytet")]
        public int Priority { get; set; }
        [DisplayName("Data zadania")]
        public DateTime? TaskDate { get; set; }
        [DisplayName("Opis zadania")]
        public string Description { get; set; }

        public virtual User User { get; set; }
    }
}