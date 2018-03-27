using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SeniorProjectPreReq.Models
{
    public class SchoolMetricValues
    {
        //TODO: There is no Actual Value Variable in the Model, It will have to Migrated 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }

        [ForeignKey("school")]
        public int schoolID { get; set; }

        public virtual SchoolPdata school { get; set; }
        [ForeignKey("metric")]
        public int metricID { get; set; }
       
        public virtual Metrics metric {get; set;}

        public Boolean Approved { get; set; }

        [Display(Name = "Year at the start of the school year")]
        public int year { get; set; }
        [Display(Name = "Date Created")]
        public DateTime dateCreated { get; set; }
    }
}