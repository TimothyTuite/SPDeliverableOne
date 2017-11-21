using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace SeniorProjectPreReq.Models
{
    public class Pictures
    {
        [Key]
        public int id {get;set; }
        [ForeignKey("school")]
        public string School_id { get; set; }
        public virtual School school { get; set; }

        public byte[] picture { get; set; }

    }
}