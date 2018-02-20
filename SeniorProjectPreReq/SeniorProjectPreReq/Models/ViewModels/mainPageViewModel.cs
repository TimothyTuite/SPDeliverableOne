using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeniorProjectPreReq.Models
{
    public class mainPageViewModel
    {
        public IEnumerable<SchoolPdata> schoolList { get; set; }
        public SchoolPdata displaySchool { get; set; }

        public IEnumerator<SchoolPdata> GetEnumerator()
        {
            return schoolList.GetEnumerator();
        }
    }
}