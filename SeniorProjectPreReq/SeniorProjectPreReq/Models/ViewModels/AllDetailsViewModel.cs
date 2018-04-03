using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeniorProjectPreReq.Models
{
    public class AllDetailsViewModel
    {
        public int schoolID { get; set; }
        public SchoolPdata generalSchoolData { get; set; }
        public string schoolVideo { get; set; }
        public List<SchoolMetricValues> allMetrics { get; set; }
        public List<Program> allPrograms { get; set; }

    }
}