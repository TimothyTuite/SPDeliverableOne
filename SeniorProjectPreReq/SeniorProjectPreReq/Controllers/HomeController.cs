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

        /*
         * Takes a YouTube URL and cuts the end off to make the source of an embed link.
         **/
        public string EmbedLink(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                int index = url.IndexOf("=") + 1;
                string end = url.Substring(index);
                string embed = "https://www.youtube.com/embed/" + end;
                return embed;
            }
            else
            {
                return null;
            }
        }
        public List<Program> ProgramQuery(int? id)
        {
            int year = DateTime.Now.Year;
            List<Program> pValues = dataContext.SchoolProgramsValues.Where(p => (p.schoolID == id) && (p.year == year)).Select(p => p.theProgram).ToList();
            return pValues;
        }
        public youtubeURL YoutubeQuery(int? id)
        {
            var index = id;
            var query = from a in dataContext.youtubeURLs
                        where a.schoolID == index && a.Approved == true
                        orderby a.dateCreated descending
                        select a;
            var item = query.FirstOrDefault();
            return item;   
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
                    {
                        CompareViewData.schoolOne = new AllDetailsViewModel();
                        CompareViewData.schoolOne.generalSchoolData = dataContext.SchoolPdatas.Find(compare[0]);
                        CompareViewData.schoolOne.allPrograms = ProgramQuery(compare[0]);
                        var item = YoutubeQuery(compare[0]);
                        try
                        {
                            CompareViewData.schoolOne.schoolVideo = EmbedLink(item.URL);
                        }
                        catch (NullReferenceException e)
                        {
                            CompareViewData.schoolOne.schoolVideo = null;
                        }

                    }
                }
                if (compare.Length == 2)
                {
                    if (compare[0] != null && compare[1] != null)
                    {
                        CompareViewData.schoolOne = new AllDetailsViewModel();
                        CompareViewData.schoolTwo = new AllDetailsViewModel();
                        CompareViewData.schoolOne.generalSchoolData = dataContext.SchoolPdatas.Find(compare[0]);
                        CompareViewData.schoolOne.allPrograms = ProgramQuery(compare[0]);
                        var item = YoutubeQuery(compare[0]);
                        try
                        {
                            CompareViewData.schoolOne.schoolVideo = EmbedLink(item.URL);
                        }
                        catch (NullReferenceException e)
                        {
                            CompareViewData.schoolOne.schoolVideo = null;
                        }
                        CompareViewData.schoolTwo.generalSchoolData = dataContext.SchoolPdatas.Find(compare[1]);
                        CompareViewData.schoolTwo.allPrograms = ProgramQuery(compare[1]);
                        item = YoutubeQuery(compare[1]);
                        try
                        {
                            CompareViewData.schoolTwo.schoolVideo = EmbedLink(item.URL);
                        }
                        catch (NullReferenceException e)
                        {
                            CompareViewData.schoolTwo.schoolVideo = null;
                        }
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
                        CompareViewData.schoolOne.allPrograms = ProgramQuery(compare[0]);
                        var item = YoutubeQuery(compare[0]);
                        try
                        {
                            CompareViewData.schoolOne.schoolVideo = EmbedLink(item.URL);
                        }
                        catch (NullReferenceException e)
                        {
                            CompareViewData.schoolOne.schoolVideo = null;
                        }
                        CompareViewData.schoolTwo.generalSchoolData = dataContext.SchoolPdatas.Find(compare[1]);
                        CompareViewData.schoolTwo.allPrograms = ProgramQuery(compare[1]);
                        item = YoutubeQuery(compare[1]);
                        try
                        {
                            CompareViewData.schoolTwo.schoolVideo = EmbedLink(item.URL);
                        }
                        catch (NullReferenceException e)
                        {
                            CompareViewData.schoolTwo.schoolVideo = null;
                        }
                        CompareViewData.schoolThree.generalSchoolData = dataContext.SchoolPdatas.Find(compare[2]);
                        CompareViewData.schoolThree.allPrograms = ProgramQuery(compare[2]);
                        item = YoutubeQuery(compare[2]);
                        try
                        {
                            CompareViewData.schoolThree.schoolVideo = EmbedLink(item.URL);
                        }
                        catch (NullReferenceException e)
                        {
                            CompareViewData.schoolThree.schoolVideo = null;
                        }
                    }
                }
            }

            return View("CompareView", CompareViewData);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Jacksonville Public Education Fund";

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