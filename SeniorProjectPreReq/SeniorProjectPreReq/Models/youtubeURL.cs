using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;



namespace SeniorProjectPreReq.Models
{
    public class youtubeURL
    {
  
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID {get; set;}



        [Display(Name = "Video URL")]
        [ForeignKey("school")]
        public int schoolID { get; set; }
        
        public virtual SchoolPdata school { get; set; }



        public string URL { get; set; }



        [Display(Name = "Year at the start of the school year")]
        public int year { get; set; }

        public Boolean Approved { get; set; }

        public DateTime dateCreated { get; set; }
    }
}