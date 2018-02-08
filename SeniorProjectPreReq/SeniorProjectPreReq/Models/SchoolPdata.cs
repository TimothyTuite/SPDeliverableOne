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
        [DisplayName("Name")]
        public string SchoolName { get; set; }
        [DisplayName("Phone")]
        public string SchoolPhone { get; set; }
        [DisplayName("Address")]
        public string SchoolAddress { get; set; }
        [DisplayName("Website")]
        public string SchoolWebsite { get; set; }
        [DisplayName("Principal")]
        public string SchoolPrincipal { get; set; }

        public virtual IEnumerable<SchoolProgramsValues> availablePrograms { get; set; }

        public virtual IEnumerable<SchoolMetricValues> schoolsMetrics { get; set; }
    }
}