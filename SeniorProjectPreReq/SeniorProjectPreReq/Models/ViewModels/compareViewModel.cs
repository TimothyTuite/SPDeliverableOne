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
        public virtual AllDetailsViewModel schoolOne {get; set;}
        public virtual AllDetailsViewModel schoolTwo { get; set; }
        public virtual AllDetailsViewModel schoolThree { get; set; }

    }
}