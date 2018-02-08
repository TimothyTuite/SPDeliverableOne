using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SeniorProjectPreReq.Models
{
    public class Program
    {
        [Key]
        public int ID { get; set; }

        [Display(name="Name of Attribute")]
        public string programName { get; set; }

        public string programDescription { get; set; }
        
    }
}