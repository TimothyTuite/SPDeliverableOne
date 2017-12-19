using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeniorProjectPreReq.Models
{
    public class School
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
        [DisplayName("Name")]
        public string SchoolName { get; set; }
        [DisplayName("Phone")]
        public string SchoolPhone { get; set; }
        [DisplayName("Address")]
        public string SchoolAddress { get; set; }
        [DisplayName("Website")]
        public string SchoolWebsite { get; set; }
        [DisplayName("Principal")]
        public string SchoolPrincipal { get; set; }
        [DisplayName("Enrolled")]
        public int StudentsEnrolled { get; set; }
        [DisplayName("Free/Reduced Lunch")]
        public int ReducedLunchPercentage { get; set; }
        [DisplayName("Grade")]
        public char SchoolGrade { get; set; }
        [DisplayName("Charter?")]
        public bool IsCharter { get; set; }
        [DisplayName("MHS Reading")]
        public int MHSReadingPercentage { get; set; }
        [DisplayName("MHS Math")]
        public int MHSMathPercentage { get; set; }
        [DisplayName("Algebra I Pass")]
        public int AlgebraIPassRatePercentage { get; set; }
        [DisplayName("Acc Course Participation")]
        public int AccCourseParticipationPercentage { get; set; }
        [DisplayName("PSR Ready Reading")]
        public int PSRReadyReadingPercentage { get; set; }
        [DisplayName("PSR Ready Math")]
        public int PSReadyMathPercentage { get; set; }

        //The attributes below represent the relations to the other models

        [DisplayName("Programs Offered")]
        public virtual ICollection<SchoolsProgram> SchoolsProgram { get; set; }
        [DisplayName("Pictures")]
        public virtual ICollection<Pictures> Pictures { get; set; }

    }
}