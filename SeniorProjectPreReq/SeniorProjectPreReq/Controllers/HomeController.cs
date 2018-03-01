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
       
        
        public highschoolViewModel getDummyData()
        {
            var dummyHS = new highschoolViewModel();
            dummyHS.Name = "Lone Star High School"; 
            return dummyHS; 
        }

        public HomeController()
        {

                
                }

        private IEnumerable<SelectListItem> populateSchoolsList(object schoolData = null)
        {
            //var s = dataContext.SchoolPdata;
            var items = new HashSet<SelectListItem>();
            //foreach (var i in s)
           // {
           //     var item = new SelectListItem();
           //     item.Value = i.ID.ToString();
           //     item.Text = i.SchoolName;

           //     items.Add(item);
           // }

            return items;
        }

        public ActionResult Index(string id)
        {
            mainPageViewModel model = new mainPageViewModel();
            model.schoolList = dataContext.SchoolPdatas.ToList();
            model.displaySchool = new SchoolPdata();
            int intschoolId = Convert.ToInt32(id);
            if (id != null)
            {
                model.displaySchool = dataContext.SchoolPdatas.Find(intschoolId);
            }
            else
            {
                model.displaySchool.SchoolName = "Test";
            }
            return View(model);
        }
        
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(int?[] compare)
        {
            var schools = dataContext.SchoolPdatas.ToList();
            var CompareViewData = new CompareViewModel();

            if (compare != null)
            {
                if (compare.Length == 1)
                {
                    if (compare[0] != null)
                        CompareViewData.schoolOne = new AllDetailsViewModel();
                        CompareViewData.schoolOne.generalSchoolData = dataContext.SchoolPdatas.Find(compare[0]);
                }
                if (compare.Length == 2)
                {
                    if (compare[0] != null && compare[1] != null)
                    {
                        CompareViewData.schoolOne = new AllDetailsViewModel();
                        CompareViewData.schoolTwo = new AllDetailsViewModel();
                        CompareViewData.schoolOne.generalSchoolData = dataContext.SchoolPdatas.Find(compare[0]);
                        CompareViewData.schoolTwo.generalSchoolData = dataContext.SchoolPdatas.Find(compare[1]);
                    }
                }
                if (compare.Length == 3)
                {
                    if (compare[0] != null && compare[1] != null && compare[2] != null)
                    {
                        CompareViewData.schoolOne = new AllDetailsViewModel();
                        CompareViewData.schoolTwo = new AllDetailsViewModel();
                        CompareViewData.schoolThree = new AllDetailsViewModel();
                        CompareViewData.schoolOne.generalSchoolData = dataContext.SchoolPdatas.Find(compare[0]);
                        CompareViewData.schoolTwo.generalSchoolData = dataContext.SchoolPdatas.Find(compare[1]);
                        CompareViewData.schoolThree.generalSchoolData = dataContext.SchoolPdatas.Find(compare[2]);
                    }
                }
            }
            return View("CompareView", CompareViewData);
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