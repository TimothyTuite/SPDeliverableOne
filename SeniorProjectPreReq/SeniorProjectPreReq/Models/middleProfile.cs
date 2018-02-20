using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeniorProjectPreReq.Models
{
    //TODO: Generate controller and Views for new model first check annotations 
    public class middleProfile
    {
        // key and connections 
        [Key]
        public int Id { get; set; }

        [ForeignKey("School")]
        public int SchoolID { get; set; }

        public virtual School School { get; set; }

        [Display(Name = "Programs Offered")]
        public virtual ICollection<SchoolsProgram> ProfilesPrograms { get; set; }

        //TODO: group discussion on how profile timeline will work

        public DateTime DateCreated { get; set; }

        public DateTime DateApproved { get; set; }



        // data attributes 

        //TODO: Research and find ranges for test scores then add the range validators 
        [Display(Name = "School Grade")]
        public string SchoolGrade { get; set; }

        [Display(Name = "Enrollment")]
        public int Enrollment { set; get; }

        [Display(Name = "Free/Reduced Lunch")]
        [DisplayFormat(DataFormatString = "{0:P2}", ApplyFormatInEditMode = true)]
        public float FreeOrReduced { set; get; }

        [Display(Name = "Is a Charter School")]
        public Boolean IsCharter { set; get; }

        [Display(Name = "ELA Reading Performance")]
        public Boolean IsMagnet { set; get; }

        [Display(Name = "ELA Reading Performance")]
        [DisplayFormat(DataFormatString = "{0:P2}", ApplyFormatInEditMode = true)]
        public float ReadingELA { set; get; }

        [Display(Name = "Math Performane")]
        [DisplayFormat(DataFormatString = "{0:P2}", ApplyFormatInEditMode = true)]
        public float MathPerformance { set; get; }

        [Display(Name = "Algebra 1 Pass Rate")]
        [DisplayFormat(DataFormatString = "{0:P2}", ApplyFormatInEditMode = true)]
        public float AlgebraPassRate { set; get; }


        //demographics 
        //TODO: find data annotations capable of insuring these 5 floats add up to 100% Also add names
        public float AfricanAmerican { get; set; }
        public float White { get; set; }
        public float Asian { get; set; }
        public float Hispanic { get; set; }
        public float MixedOther { get; set; }
        public float Unspecified { get; set; }
    }
}