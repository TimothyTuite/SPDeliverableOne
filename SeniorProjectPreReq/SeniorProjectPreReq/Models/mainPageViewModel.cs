using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeniorProjectPreReq.Models
{
    public class mainPageViewModel
    {
        public IEnumerable<School> schoolList { get; set; }
        public School displaySchool { get; set; }

        public IEnumerator<School> GetEnumerator()
        {
            return schoolList.GetEnumerator();
        }
    }
}