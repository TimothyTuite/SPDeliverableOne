using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace SeniorProjectPreReq.Models
{
    public class SchoolsProgram
    {
        [Key]
        public string ID { get; set; }

        public string SchoolID { get; set; }


        public string ProgramID { get; set; }
    }


}