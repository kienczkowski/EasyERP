using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EasyERP.Models
{
    public class LogError
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
    }
}