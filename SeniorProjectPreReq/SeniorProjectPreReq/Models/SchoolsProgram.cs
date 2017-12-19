using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace SeniorProjectPreReq.Models
{
    public class SchoolsProgram
    {
        [Key]
        
        public int ID { get; set; }
        [ForeignKey("school")]
        public int SchoolID { get; set; }

        public virtual School school { get; set; }

        [ForeignKey("Programs")]
        public int ProgramID { get; set; }

        public virtual Programs Programs { get; set; }
    }


}