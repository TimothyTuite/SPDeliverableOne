using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity; 
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
            if (id == null)
            {
                // return HttpNotFound("id could not be found");
                schoolAttributes.Add("Error", "School Not Found");
                return Json(schoolAttributes, JsonRequestBehavior.AllowGet);
            }

            if (Convert.ToInt32(id) == -1)
            {
                List<SchoolPdata> schools = dataContext.SchoolPdatas.ToList();
                return Json(schools, JsonRequestBehavior.AllowGet);
            }

            
            int intschoolId = Convert.ToInt32(id);
            var schoolData = dataContext.SchoolPdatas.Find(intschoolId);


            if (schoolData == null)
            {
                // return HttpNotFound("id could not be found");
                schoolAttributes.Add("Error", "School Not Found");
                return Json(schoolAttributes, JsonRequestBehavior.AllowGet);
            }
            
            
            
            var schoolName = schoolData.SchoolName;
            var schoolAddress = schoolData.SchoolAddress;
            var schoolPhone = schoolData.SchoolPhone;
            var schoolWebsite = schoolData.SchoolWebsite;
            var schoolPrincipal = schoolData.SchoolPrincipal;

            
            schoolAttributes.Add("schoolName", schoolName);
            schoolAttributes.Add("Address", schoolAddress);
            schoolAttributes.Add("Phone", schoolPhone);
            schoolAttributes.Add("Website", schoolWebsite);
            schoolAttributes.Add("Principal", schoolPrincipal);
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
            //TODO: Shannon, Here is the api, it gets all the data for each school metric, you probably only need the name, year and Value
            // you can cut out the rest of it or leave it as is and just get what you need from the json. 
            int intschoolId = Convert.ToInt32(id);
            var schoolData = dataContext.SchoolMetricValues.Where(v => v.schoolID == intschoolId).Include("metric")
                .Select(v => new { v.metric.MetricName, v.value, v.year });
            if (schoolData != null)
            {
                var hasPrograms = schoolData.ToList();
                return Json(hasPrograms, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("has no Metrics", JsonRequestBehavior.AllowGet);
            }
        }
        [AllowAnonymous]
        public ActionResult allSchools()
        {
            List<SchoolPdata> all = dataContext.SchoolPdatas.ToList();
            return Json(all, JsonRequestBehavior.AllowGet);
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
            var mSchool = dataContext.SchoolTypes.Where(m => m.Name == "combination School").ToList();
            var school = mSchool.First();
            var TID = school.ID;
            List<SchoolPdata> CollaborationSchools = dataContext.SchoolPdatas.Where(m => m.schoolTypeID == TID).ToList();
            return Json(CollaborationSchools, JsonRequestBehavior.AllowGet);
        }

        public ActionResult prekSchools()
        {
            var mSchool = dataContext.SchoolTypes.Where(m => m.Name == "Pre-Learning Center").ToList();
            var school = mSchool.First();
            var TID = school.ID;
            List<SchoolPdata> CollaborationSchools = dataContext.SchoolPdatas.Where(m => m.schoolTypeID == TID).ToList();
            return Json(CollaborationSchools, JsonRequestBehavior.AllowGet);
        }
    }
}