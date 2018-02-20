using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeniorProjectPreReq.Models
{
    //TODO: Generate controller and Views for new model first check annotations 
    public class highschoolViewModel
    {

        // key and connections 
        [Key]
        public int id { get; set; }

        [ForeignKey("school")]
        public int schoolID { get; set; }

        public virtual SchoolPdata school { get; set; }

        public string Name { get; set; }

        [Display(Name = "Programs Offered")]
        public virtual ICollection<SchoolProgramsValues> ProfilesPrograms { get; set; }

        //TODO: group discussion on how profile timeline will work

        public DateTime dateCreated { get; set; }

        public DateTime dateApproved { get; set; }



        // data attributes 

        //TODO: Research and find ranges for test scores then add the range validators 
        [Display(Name = "School Grade")]
        public string schoolGrade { get; set; }

        [Display(Name = "Enrollment")]
        public int enrollment { set; get; }

        [Display(Name = "Free/Reduced Lunch")]
        [DisplayFormat(DataFormatString = "{0:P2}", ApplyFormatInEditMode = true)]
        public float freeOrReduced { set; get; }

        [Display(Name = "Is a Charter School?")]
        public Boolean isCharter { set; get; }

        [Display(Name = "Is a Magnet School?")]
        public Boolean isMagnet { set; get; }

        [Display(Name = "Graduation Rate")]
        [DisplayFormat(DataFormatString = "{0:P2}", ApplyFormatInEditMode = true)]
        public float gradRate { set; get; }

        [Display(Name = "ELA & Math Learning Gains")]
        [DisplayFormat(DataFormatString = "{0:P2}", ApplyFormatInEditMode = true)]
        public float emLearningGains { set; get; }

        [Display(Name = "Accelerated Coursework")]
        [DisplayFormat(DataFormatString = "{0:P2}", ApplyFormatInEditMode = true)]
        public float accelCoursework { set; get; }

        [Display(Name = "ELA & Math Performance")]
        [DisplayFormat(DataFormatString = "{0:P2}", ApplyFormatInEditMode = true)]
        public float emPerformance { set; get; }


        //demographics 
        //TODO: find data annotations capable of insuring these 5 floats add up to 100% Also add names
        public float AfricanAmerican { get; set; }
        public float White { get; set; }
        public float Asian { get; set; }
        public float Hispanic { get; set; }
        public float MixedOther { get; set; }
        public float unspecified { get; set; }

    }
}