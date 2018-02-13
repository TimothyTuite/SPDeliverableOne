using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SeniorProjectPreReq.Models
{
    public class compareViewModel
    {
        public virtual SchoolPdata schoolOne {get; set;}
        public virtual SchoolPdata schoolTwo { get; set; }
        public virtual SchoolPdata schoolThree { get; set; }

    }
}