using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeniorProjectPreReq.Models
{
    public class SchoolType
    {
        [key]
        public int ID { get; set; }

        public string Name { get; set; }
    }
}