﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SeniorProjectPreReq.Models;
namespace SeniorProjectPreReq.Controllers
{
    public class APIController : Controller
    {
        private ApplicationDbContext dataContext = new ApplicationDbContext();

        public APIController()
        {

        }

        public ActionResult index()
        {
            return View(); 
        }
        // GET: API
        [AllowAnonymous]
        public ActionResult School(string id)
        {
            Dictionary<string, string> schoolAttributes = new Dictionary<string, string>();
            int intschoolId = Convert.ToInt32(id);
            var schoolData = dataContext.SchoolPdatas.Find(intschoolId);
            var schoolName = schoolData.SchoolName;
            var schoolAddress = schoolData.SchoolAddress;
            
            
            if (schoolData == null)
            {
                // return HttpNotFound("id could not be found");
                schoolAttributes.Add("Error","School Not Found"); 
                return Json(schoolAttributes, JsonRequestBehavior.AllowGet);
            }
            schoolAttributes.Add("schoolName", schoolName);
            schoolAttributes.Add("Address", schoolAddress);
            return Json(schoolAttributes, JsonRequestBehavior.AllowGet);

        }
        [AllowAnonymous]
        public ActionResult SchoolsPrograms(string id,string year)
        {
            int intschoolId = Convert.ToInt32(id);
            var schoolData = dataContext.SchoolPdatas.Find(intschoolId);
            if(schoolData.schoolsPrograms != null)
            {
                List<SchoolProgramsValues> hasPrograms = schoolData.schoolsPrograms.ToList();
                return Json(hasPrograms, JsonRequestBehavior.AllowGet);
            }
            else
            { 
                return Json("has no Programs", JsonRequestBehavior.AllowGet); 
            }
          
        }
        [AllowAnonymous]
        public ActionResult SchoolsMetrics(string id)
        {
            int intschoolId = Convert.ToInt32(id);
            var schoolData = dataContext.SchoolPdatas.Find(intschoolId);
            if (schoolData.schoolsPrograms != null)
            {
                List<SchoolMetricValues> hasPrograms = schoolData.schoolsMetrics.ToList();
                return Json(hasPrograms, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("has no Metrics", JsonRequestBehavior.AllowGet);
            }
        }
        [AllowAnonymous]
        public ActionResult MiddleSchools()
        {
            List<SchoolPdata> MiddleSchools = dataContext.SchoolPdatas.Where(m => m.type.Name == "Middle School").ToList(); 
            return Json(MiddleSchools, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ElementarySchools()
        {
            List<SchoolPdata> ElementarySchools = dataContext.SchoolPdatas.Where(m => m.type.Name == "Elementary School").ToList();
            return Json(ElementarySchools, JsonRequestBehavior.AllowGet);
        }

        public ActionResult HighSchools()
        {
            List<SchoolPdata> HighSchools = dataContext.SchoolPdatas.Where(m => m.type.Name == "high School").ToList();
            return Json(HighSchools, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CollaborationSchools()
        {
            var mSchool = dataContext.SchoolTypes.Where(m => m.Name == "Collaboration School").ToList();
            var school = mSchool.First();
            var TID = school.ID;
            List<SchoolPdata> CollaborationSchools = dataContext.SchoolPdatas.Where(m => m.schoolTypeID == TID).ToList();
            return Json(CollaborationSchools, JsonRequestBehavior.AllowGet);
        }
    }
}