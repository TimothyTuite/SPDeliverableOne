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



        [Display(Name = "School")]
        [ForeignKey("school")]
        public int schoolID { get; set; }
        
        public virtual SchoolPdata school { get; set; }
        [Display(Name = "Video URL")]
        public string URL { get; set; }


        [Display(Name = "School Year")]
        public int year { get; set; }

        public Boolean Approved { get; set; }
        [Display(Name = "Date Posted")]
        public DateTime dateCreated { get; set; }
    }
}