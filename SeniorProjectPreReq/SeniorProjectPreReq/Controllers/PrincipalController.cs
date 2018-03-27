using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SeniorProjectPreReq.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Mvc;
using SeniorProjectPreReq.Models.FormModels;
using System.Text;
using System.Data.SqlClient;

namespace SeniorProjectPreReq.Controllers
{
    public class PrincipalController : Controller
    {
        private ApplicationDbContext dataContext = new ApplicationDbContext();
        //use this to get the current user data; 
        private ApplicationUserManager _userManager;
        public PrincipalController()
        {

        }
        public PrincipalController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: Principle
        public ActionResult Index()
        {
            return View();
        }

        //simple functions return the current year 
        public int currentYear()
        {
            return DateTime.Now.Year; 
        }
        //simple function returns last year
        public int lastYear()
        {
            return DateTime.Now.Year - 1;
        }
        public int nextYear()
        {
            return DateTime.Now.Year + 1; 
        }
        public ActionResult AddYouTubeVideo()
        {
            var yCurrent = currentYear().ToString();
            var yLast = lastYear().ToString(); 
            List<SelectListItem> years = new List<SelectListItem>();
            years.Add(new SelectListItem
            {
                Text = yCurrent,
                Value = yCurrent

            });
            years.Add(new SelectListItem
            {
                Text = yLast,
                Value = yLast
            });
            ViewBag.year = years; 
            return View(); 
        }
        public ActionResult EditProgramYear()
        {
            List<SelectListItem> year = new List<SelectListItem>();
            year.Add(new SelectListItem
            {
                Text = currentYear().ToString(),
                Value = currentYear().ToString()
            });
            year.Add(new SelectListItem
            {
                Text = nextYear().ToString(),
                Value = currentYear().ToString()
            });

            ViewBag.year = year; 
            return View(); 
        }
        //User selectes year to edit 
        [HttpPost]
        public ActionResult EditProgramYear(string year)
        {
            return RedirectToAction("SelectPrograms/" + year);
        }
        //Shows the user the Unapproved programs 
        public ActionResult ViewUnApproved()
        {
            var schoolID = getUserSchoolID();
            List<SchoolProgramsValues> UnApprovedPrograms = dataContext.SchoolProgramsValues.Include("theProgram").Where(x => (x.schoolID == schoolID) && (x.Approved == false)).ToList(); 
            return View(UnApprovedPrograms); 
        }
        public ActionResult Saved()
        {
            return View(); 
        }
    
        public string[] getProgramsIds(int id,int year)
        {
            try
            {
                IEnumerable<SchoolProgramsValues> sp =  dataContext.SchoolProgramsValues.Where(x => (x.schoolID == id) && (x.year == year)).ToList();
                return sp.Select(x => x.programID.ToString()).ToArray();
            }
            catch(ArgumentNullException e)
            {
                string[] result = new string[]{ };
                return result; 
            }
            


        }

        public int getUserSchoolID()
        {
            var userID = User.Identity.GetUserId();
            var user = UserManager.FindById(userID);
            return user.school.ID; 
        }

        private IList<CheckBoxes> GetAvailablePrograms(int schoolsTypeID)
        {
            var p = dataContext.Programs.Where(m => m.TypeID == schoolsTypeID);
            List<CheckBoxes> possible = p.Select(x => new CheckBoxes
            {
                Value = x.ID.ToString(),
                Text = x.programName
            }).ToList();
            // https://stackoverflow.com/questions/17037446/get-multiple-selected-checkboxes-in-mvc
            return possible;
        }

        public ActionResult SelectPrograms(string year)
        {
            //if no year is submitted and the year is any other than the current or the next redirect 
            if (year == null || (year != currentYear().ToString() && year != nextYear().ToString()))
            {
                return RedirectToAction("EditProgramYear");
            }

            List<string> listOfCurrent = getProgramsIds(getUserSchoolID(), currentYear()).ToList();
            List<string> listOfOld = getProgramsIds(getUserSchoolID(), lastYear()).ToList();
            IList<CheckBoxes> possible = GetAvailablePrograms(getUserSchoolType());
            var model = new ProgramsCheckBoxList { AvailablePrograms = possible, year = 2018,preSelectids = listOfCurrent };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectPrograms(FormCollection collection,ProgramsCheckBoxList model)
        {
            string selected = Request.Form["CategoryIds"];
            List<string> newPrograms = new List<string>();
            List<string> AfterRemoval = new List<string>();
            List<string> AfterAddNew = new List<string>(); 
            List<string> listOfCurrent = getProgramsIds(getUserSchoolID(), model.year).ToList();
            var schoolId = getUserSchoolID(); 
            if (string.IsNullOrEmpty(selected))
            {
                dataContext.SchoolProgramsValues.RemoveRange(dataContext.SchoolProgramsValues.Where(s => (s.schoolID == schoolId) && (s.year == model.year)));
                return RedirectToAction("Saved");
            }
            else
            {
                //querys and removes all the old programs for the school for the selected year
               dataContext.SchoolProgramsValues.RemoveRange(dataContext.SchoolProgramsValues.Where(s => (s.schoolID == schoolId) && (s.year == model.year))); 
               newPrograms = selected.Split(',').ToList();
               AfterRemoval = getProgramsIds(getUserSchoolID(), model.year).ToList();
                foreach (var Program in newPrograms)
                {
                    SchoolProgramsValues schoolProgram = new SchoolProgramsValues();
                    schoolProgram.programID = Int32.Parse(Program);
                    schoolProgram.schoolID = schoolId;
                    schoolProgram.year = model.year;
                    schoolProgram.hasProgram = true;
                    schoolProgram.Approved = false;
                    schoolProgram.dateCreated = DateTime.Now;
                    dataContext.SchoolProgramsValues.Add(schoolProgram);
                }
                dataContext.SaveChanges();
                AfterAddNew = getProgramsIds(getUserSchoolID(), model.year).ToList();
            }
            // save this code for error checking 
            //var data = new { NewProgramsIDs = newPrograms,currentProgramsIds = listOfCurrent, shouldBeNone= AfterRemoval, year = model.year, New = AfterAddNew };
            //return Json(data, JsonRequestBehavior.AllowGet);

            return RedirectToAction("Saved");
        }
        private int getUserSchoolType()
        {
            var userID = User.Identity.GetUserId();
            var user = UserManager.FindById(userID);
            return user.school.schoolTypeID; 
        }

        public ActionResult EditMetricYear()
        {
            List<SelectListItem> year = new List<SelectListItem>();
            year.Add(new SelectListItem
            {
                Text = currentYear().ToString(),
                Value = currentYear().ToString()
            });
            year.Add(new SelectListItem
            {
                Text = nextYear().ToString(),
                Value = currentYear().ToString()
            });

            ViewBag.year = year;
            return View();
        }
        //User selectes year to edit 
        [HttpPost]
        public ActionResult EditMetricYear(string year)
        {
            return RedirectToAction("SelectMetrics/" + year);
        }

        public ActionResult SelectMetrics(string year)
        {
            if (year == null || (year != currentYear().ToString() && year != nextYear().ToString()))
            {
                return RedirectToAction("EditMetricYear");
            }

            List<MetricListItem> PossibleMetrics = GetAvailableMetrics(getUserSchoolType()).ToList();
            var model = new MetricsList { listOfMetrics = PossibleMetrics };
            model.year = year; 
            return View(model); 
        }
        private IList<MetricListItem> GetAvailableMetrics(int schoolsTypeID)
        {
            var p = dataContext.Metrics.Where(m => m.schoolLevel == schoolsTypeID);
            List<MetricListItem> possible = p.Select(x => new MetricListItem
            {
                id = x.ID.ToString(),
                MetricName = x.MetricName,
                MetricValue = null
            }).ToList();

            return possible;
        }
        public ActionResult EditMetricForYear(string year,string MetricID)
        {
            int schoolID = getUserSchoolID(); 
            int mID = Convert.ToInt32(MetricID);
            int mYear = Convert.ToInt32(year); 
            var Metric = dataContext.Metrics.FirstOrDefault(m => m.ID == mID);
            //check to see if there isn't already a value for the school metric year 
            var currentMetricData = dataContext.SchoolMetricValues.FirstOrDefault(m => (m.ID == mID) && (m.year == mYear) && (m.schoolID == schoolID));
            var type = dataContext.SchoolTypes.FirstOrDefault(t => t.ID == Metric.schoolLevel);
            string LevelName = type.Name; 
            if(Metric == null)
            {
                //Error Redirect 
            }
            var model = new EditMetric();
            model.metricID = mID;
            model.schoolID = schoolID;
            model.metricName = Metric.MetricName;
            model.description = Metric.Description; 
            model.rangeTop = Metric.rangeTop;
            model.rangeBottom = Metric.rangeBottom;
            model.year = year;
            model.level = LevelName; 
            var data = new { year = year, MetricID = MetricID, Metric = Metric, FromModel = model};
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMetricForYear(EditMetric model)
        {
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}