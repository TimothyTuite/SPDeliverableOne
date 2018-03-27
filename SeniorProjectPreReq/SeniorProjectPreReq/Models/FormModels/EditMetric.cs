using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeniorProjectPreReq.Models.FormModels
{
    public class EditMetric
    {
        public string metricName { get; set; }

        public int metricID { get; set; }

        public int schoolID { get; set; }
        public string description { get; set; }
        public string level { get; set; }
        public string year { get; set; }
        public float rangeTop { get; set; }
        public float rangeBottom { get; set; }

        public float Value { get; set; }
    }
}