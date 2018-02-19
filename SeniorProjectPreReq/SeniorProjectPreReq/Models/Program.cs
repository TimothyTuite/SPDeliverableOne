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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }

        [Display(Name="Program Name")]
        [MaxLength(50)]
        public string programName { get; set; }
        [Display(Name = "Program Description")]
        [MaxLength(450)]
        public string programDescription { get; set; }
        public int TypeID { get; set; }

        
    }
}