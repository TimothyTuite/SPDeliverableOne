using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SeniorProjectPreReq.Models
{
    public class SchoolProgramsValues
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }

        [ForeignKey("school")]
        [Display(Name = "School ID")]
        public int schoolID { get; set; }

        public virtual SchoolPdata school { get; set; }

        [ForeignKey("theProgram")]
        public int programID { get; set; }

        public virtual Program theProgram {get; set;}
        [Display(Name = "Year at the start of the school year")]
        public int year { get; set; }

        public Boolean Approved { get; set; }

        public Boolean hasProgram { get; set; }

        public DateTime dateCreated { get; set; }
    }
}