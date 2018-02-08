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
        public virtual School schoolOne {get; set;}
        public virtual School schoolTwo { get; set; }
        public virtual School schoolThree { get; set; }

    }
}