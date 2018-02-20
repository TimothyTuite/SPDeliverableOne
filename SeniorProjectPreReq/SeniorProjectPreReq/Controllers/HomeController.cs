using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SeniorProjectPreReq.Models; 

namespace SeniorProjectPreReq.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        private ApplicationDbContext dataContext = new ApplicationDbContext();

        public HomeController()
        {
            
        }

        private IEnumerable<SelectListItem> populateSchoolsList(object schoolData = null)
        {
            var s = dataContext.Schools;
            var items = new HashSet<SelectListItem>();
            foreach (var i in s)
            {
                var item = new SelectListItem();
                item.Value = i.ID.ToString();
                item.Text = i.SchoolName;

                items.Add(item);
            }

            return items;
        }

        public ActionResult Index(string id)
        {
            mainPageViewModel model = new mainPageViewModel();
            model.schoolList = dataContext.Schools.ToList();
            model.displaySchool = new School();
            int intschoolId = Convert.ToInt32(id);
            if (id != null)
            {
                model.displaySchool = dataContext.Schools.Find(intschoolId);
            }
            else
            {
                model.displaySchool.SchoolName = "Test";
            }
            return View(model);
        }
        
        [AllowAnonymous]
        public ActionResult getSchoolDataByID(string id)
        {
            
            int intschoolId = Convert.ToInt32(id);
            var schoolData = dataContext.Schools.Find(intschoolId);
            if(schoolData == null)
            {
                return HttpNotFound();
            }
            var schoolName = schoolData.SchoolName;
            var schoolAddress = schoolData.SchoolAddress; 
            Dictionary<string, string> schoolAttributes = new Dictionary<string, string>();
            schoolAttributes.Add("schoolName", schoolName);
            schoolAttributes.Add("Address", schoolAddress); 
            if(schoolData == null)
            {
                return HttpNotFound("id could not be found"); 
            }
            return Json(schoolAttributes, JsonRequestBehavior.AllowGet); 

        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(int?[] compare)
        {
            var schools = dataContext.Schools.ToList();
            var compareViewData = new compareViewModel();

            if (compare.Length == 1)
            {
                if(compare[0] != null)
                    compareViewData.schoolOne = dataContext.Schools.Find(compare[0]);
            }
            if (compare.Length == 2)
            {
                if (compare[0] != null && compare[1] != null)
                {
                    compareViewData.schoolOne = dataContext.Schools.Find(compare[0]);
                    compareViewData.schoolTwo = dataContext.Schools.Find(compare[1]);
                }
            }
            if (compare.Length == 3)
            {
                if (compare[0] != null && compare[1] != null && compare[2] != null)
                {
                    compareViewData.schoolOne = dataContext.Schools.Find(compare[0]);
                    compareViewData.schoolTwo = dataContext.Schools.Find(compare[1]);
                    compareViewData.schoolThree = dataContext.Schools.Find(compare[2]);
                }
            }

            return View("Compare", compareViewData);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

       

        
    }
}