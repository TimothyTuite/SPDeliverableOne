using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SeniorProjectPreReq.Models
{
    public class SchoolPdata
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
        [Display( Name ="Name")]
        public string SchoolName { get; set; }
        [Display(Name = "Phone")]
        public string SchoolPhone { get; set; }
        [Display(Name = "Address")]
        public string SchoolAddress { get; set; }
        [Display(Name = "Website")]
        public string SchoolWebsite { get; set; }
        [Display(Name = "Principal")]
        public string SchoolPrincipal { get; set; }
        [ForeignKey("type")]
        [Display(Name = "School Level")]
        public int schoolTypeID { get; set; }
        public virtual SchoolType type { get; set; }

        public Boolean Approved { get; set; }

        public virtual IEnumerable<SchoolProgramsValues> schoolsPrograms { get; set; }

        public virtual IEnumerable<SchoolMetricValues> schoolsMetrics { get; set; }
    }
}