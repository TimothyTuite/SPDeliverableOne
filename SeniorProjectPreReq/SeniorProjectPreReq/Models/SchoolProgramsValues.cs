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
        [key]
        public int ID { get; set; }

        [ForeignKey("school")]
        public int schoolID { get; set; }

        public virtual SchoolPData school { get; set; }

        [foreignKey("Attributes")]
        public int AttributeID { get; set; }

        public virtual Program theProgram {get; set;} 
        public int year { get; set; }

        public Boolean hasProgram { get; set; }
    }
}