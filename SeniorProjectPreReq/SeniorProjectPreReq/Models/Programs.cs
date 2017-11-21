using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SeniorProjectPreReq.Models
{
    public class Programs
    { 
        [Key]
        [DisplayName("ID")]
        public string ProgramID { get; set; }

        [DisplayName("Name")]
        public string Program_Name { get; set; }

        [DisplayName("Description")]
        public string Program_Description { get; set; }

        [DisplayName("Level")]
        public string Program_Level { get; set; }
    }
}