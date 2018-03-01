using System;
using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using System.Collections.Generic;
using System.Drawing;
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
        
        [AllowAnonymous]
        public ActionResult getSchoolDataByID(string id)
        {
            
            int intschoolId = Convert.ToInt32(id);
            var schoolData = dataContext.SchoolPdatas.Find(intschoolId);
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

        public ActionResult SchoolGraphsView()
        {
            Highcharts columnChart = new Highcharts("columnchart");

            columnChart.InitChart(new Chart()
            {
                Type = DotNet.Highcharts.Enums.ChartTypes.Column,
                BackgroundColor = new BackColorOrGradient(System.Drawing.Color.AliceBlue),
                Style = "fontWeight: 'bold', fontSize: '17px'",
                BorderColor = System.Drawing.Color.LightBlue,
                BorderRadius = 0,
                BorderWidth = 2

            });

            columnChart.SetTitle(new Title()
            {
                Text = "Accelerated Coursework"
            });

            columnChart.SetSubtitle(new Subtitle()
            {
                Text = "Indicates the Percentage of Students Participating in Accelerated Coursework Options"
            });

            columnChart.SetXAxis(new XAxis()
            {
                Type = AxisTypes.Category,
                Title = new XAxisTitle() { Text = "Participation", Style = "fontWeight: 'bold', fontSize: '17px'" },
                Categories = new[] { "2004", "2005", "2006", "2007", "2008", "2009", "2010", "2011", "2012" }
            });

            columnChart.SetYAxis(new YAxis()
            {
                Title = new YAxisTitle()
                {
                    Text = "Runs",
                    Style = "fontWeight: 'bold', fontSize: '17px'"
                },
                ShowFirstLabel = true,
                ShowLastLabel = true,
                Min = 0
            });

            columnChart.SetLegend(new Legend
            {
                Enabled = true,
                BorderColor = System.Drawing.Color.CornflowerBlue,
                BorderRadius = 6,
                BackgroundColor = new BackColorOrGradient(ColorTranslator.FromHtml("#FFADD8E6"))
            });

            columnChart.SetSeries(new Series[]
            {
                new Series{

                    Name = "School 1",
                    Data = new Data(new object[] { 81, 41, 72, 74, 60, 67, 75, 83, 95 })
                },
                new Series()
                {
                    Name = "School 2",
                    Data = new Data(new object[] { 59, 65, 71, 73, 77, 88, 70, 84, 94, })
                },
                new Series()
                {
                    Name = "School 3",
                    Data = new Data(new object[] { 69, 65, 71, 83, 77, 70, 67, 76, 83, })
                }
            }
            );
            return View(columnChart);

        }




    }
}