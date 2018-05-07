using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SeniorProjectPreReq.Models
{
    public class Metrics
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
        [Display(Name = "Metric Name")]
        [MaxLength(50)]
        public string MetricName { get; set; }
        [Display(Name = "Metric Description")]
        [MaxLength(450)]
        public string Description { get; set; }

        [Display(Name = "School Type")]
        public int schoolLevel { get; set; }
        [Display(Name = "Metric Maximum")]
        public float rangeTop { get; set; }
        [Display(Name = "Metric Minimum")]
        public float rangeBottom { get; set; }
    }
}