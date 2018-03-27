using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeniorProjectPreReq.Models.FormModels
{
    public class MetricsList
    {
        public IList<MetricListItem> listOfMetrics { get; set; }
        public string year { get; set; }

        public MetricsList()
        {
            listOfMetrics = new List<MetricListItem>(); 
        }
    }
}