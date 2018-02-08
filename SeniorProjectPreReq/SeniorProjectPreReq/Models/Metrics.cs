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
        [key]
        public int ID { get; set; }

        public string MetricName { get; set; }

        public string Description { get; set; }

        public int schoolLevel { get; set; }

        public virtual SchoolType type { get; set; }
    }
}