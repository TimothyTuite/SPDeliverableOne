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
        
        public string ID { get; set; }
        [ForeignKey("school")]
        public string SchoolID { get; set; }

        public virtual School school { get; set; }

        [ForeignKey("Programs")]
        public string ProgramID { get; set; }

        public virtual Programs Programs { get; set; }
    }


}