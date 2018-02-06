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