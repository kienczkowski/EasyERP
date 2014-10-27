using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EasyERP.Models
{
    public class Note
    {
        [Key]
        public int ID { get; set; }
        public string Description { get; set; }
        public DateTime? EnteredOn { get; set; }
        public string EnteredBy { get; set; }

        public virtual Client Client { get; set; }
    }
}