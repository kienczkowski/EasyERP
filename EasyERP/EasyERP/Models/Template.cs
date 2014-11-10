using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EasyERP.Models
{
    public class Template
    {
        [Key]
        public int TemplateId { get; set; }
        [Required(ErrorMessage="Nazwa szablonu jest wymagana")]
        [DisplayName("Nazwa szablonu")]
        public string TemplateName { get; set; }
        [DisplayName("Plik z szablonem")]
        [DataType(DataType.Upload)]
        public byte[] TemplateFile { get; set; }
        [DisplayName("Data dodania")]
        [DataType(DataType.Date)]
        public DateTime? EnteredOn { get; set; }
        [DisplayName("Flaga aktywności")]
        public bool IsActive { get; set; }
    }
}