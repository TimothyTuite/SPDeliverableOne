using System;
using System.Collections.Generic;
using System.Linq;
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

        public ActionResult Compare(int? id1, int? id2, int? id3)
        {
            var compareViewData= new compareViewModel(); 

            if(id1 != null)
            {
                compareViewData.schoolOne = dataContext.Schools.Find(id1);
            }
            if (id1 != null)
            {
                compareViewData.schoolTwo = dataContext.Schools.Find(id2);
            }
            if (id1 != null)
            {
                compareViewData.schoolOne = dataContext.Schools.Find(id3);
            }

            return View(compareViewData);
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