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

        public ActionResult Index()
        {
            var schools = dataContext.Schools.ToList();
            
            return View(schools);
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
                    compareViewData.schoolOne = dataContext.Schools.Find(compare[2]);
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