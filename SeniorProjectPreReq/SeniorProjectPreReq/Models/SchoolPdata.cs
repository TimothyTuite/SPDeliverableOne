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
        [Display(Name = "School")]
        public int ID { get; set; }
        [Required]
        [Display( Name ="Name")]
        public string SchoolName { get; set; }
        [Required]
        [Display(Name = "Phone")]
        public string SchoolPhone { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string SchoolAddress { get; set; }
        [Required]
        [Display(Name = "Website")]
        public string SchoolWebsite { get; set; }
        [Required]
        [Display(Name = "Principal")]
        public string SchoolPrincipal { get; set; }

        [Required]
        [ForeignKey("type")]
        [Display(Name = "School Type")]
        public int schoolTypeID { get; set; }
        public virtual SchoolType type { get; set; }

        public Boolean Approved { get; set; }

        public virtual IEnumerable<SchoolProgramsValues> schoolsPrograms { get; set; }

        public virtual IEnumerable<SchoolMetricValues> schoolsMetrics { get; set; }
    }
}