using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Configuration; 
using System.Web.Mvc;
using SeniorProjectPreReq.Models;

namespace SeniorProjectPreReq.Controllers
{
    public class SchoolPdatasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //googleMapsAPIKey configure the api key in the web.config folder 
        //******CHANGE TIM's KEY BEFORER GOING TO PRODUCTION**********
        private string googleAPIkey = ConfigurationManager.AppSettings["googleMapsAPIKey"];

        // GET: SchoolPdatas
        [Authorize]
        public ActionResult Index()
        {
            var schoolPdatas = db.SchoolPdatas.Include(s => s.type);
            return View(schoolPdatas.ToList());
        }

        // GET: SchoolPdatas/AllDetails/id
        public ActionResult AllDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllDetailsViewModel schoolInfo = new AllDetailsViewModel();
            int parsedID = Convert.ToInt32(id);
            schoolInfo.schoolID = parsedID;
            schoolInfo.generalSchoolData = db.SchoolPdatas.Find(id);
            var item = YoutubeQuery(id);
            try
            {
                schoolInfo.schoolVideo = EmbedLink(item.URL);
            }
            catch(NullReferenceException e)
            {
                schoolInfo.schoolVideo = null; 
            }
            schoolInfo.allMetrics = MetricQuery(id);
            schoolInfo.allPrograms = ProgramQuery(id);
            if (schoolInfo.generalSchoolData == null)
            {
                return HttpNotFound();
            }
            return View("AllDetailsSchoolView", schoolInfo);
        }

        public string EmbedLink(string url)
        {
            if(!string.IsNullOrEmpty(url))
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
        public youtubeURL YoutubeQuery(int? id)
        {
            var index = id;
            var query = from a in db.youtubeURLs
                        where a.schoolID == index && a.Approved == true
                        orderby a.dateCreated descending
                        select a;
            var item = query.FirstOrDefault();
            return item;
        }

        public List<Program> ProgramQuery(int? id)
        {
            int year = DateTime.Now.Year;
            List<Program> pValues = db.SchoolProgramsValues.Where(p => (p.schoolID == id) && (p.year == year)).Select(p => p.theProgram).ToList(); 
            return pValues;
        }

        public List<SchoolMetricValues> MetricQuery(int? id)
        {
            int year = DateTime.Now.Year;
            List<SchoolMetricValues> mValues = db.SchoolMetricValues.Where(p => (p.schoolID == id) && (p.year == year)).ToList();
            return mValues;
        }

        // GET: SchoolPdatas/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolPdata schoolPdata = db.SchoolPdatas.Find(id);
            if (schoolPdata == null)
            {
                return HttpNotFound();
            }
            return View(schoolPdata);
        }

        // GET: SchoolPdatas/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ViewBag.schoolTypeID = new SelectList(db.SchoolTypes, "ID", "Name");
            return View();
        }

        // POST: SchoolPdatas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "ID,SchoolName,SchoolPhone,SchoolAddress,SchoolWebsite,SchoolPrincipal,schoolTypeID")] SchoolPdata schoolPdata)
        {
            if (ModelState.IsValid)
            {
                if (schoolPdata.SchoolWebsite != null && (schoolPdata.SchoolWebsite.Substring(0, 7) == "http://" || schoolPdata.SchoolWebsite.Substring(0, 8) == "https://"))
                {
                    db.SchoolPdatas.Add(schoolPdata);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.schoolTypeID = new SelectList(db.SchoolTypes, "ID", "Name", schoolPdata.schoolTypeID);
            return View(schoolPdata);
        }

        // GET: SchoolPdatas/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolPdata schoolPdata = db.SchoolPdatas.Find(id);
            if (schoolPdata == null)
            {
                return HttpNotFound();
            }
            ViewBag.schoolTypeID = new SelectList(db.SchoolTypes, "ID", "Name", schoolPdata.schoolTypeID);
            return View(schoolPdata);
        }

        // POST: SchoolPdatas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "ID,SchoolName,SchoolPhone,SchoolAddress,SchoolWebsite,SchoolPrincipal,schoolTypeID")] SchoolPdata schoolPdata)
        {
            if (ModelState.IsValid)
            {
                if (schoolPdata.SchoolWebsite != null && (schoolPdata.SchoolWebsite.Substring(0, 7) == "http://" || schoolPdata.SchoolWebsite.Substring(0, 8) == "https://"))
                {
                    db.Entry(schoolPdata).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.schoolTypeID = new SelectList(db.SchoolTypes, "ID", "Name", schoolPdata.schoolTypeID);
            return View(schoolPdata);
        }

        // GET: SchoolPdatas/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolPdata schoolPdata = db.SchoolPdatas.Find(id);
            if (schoolPdata == null)
            {
                return HttpNotFound();
            }
            return View(schoolPdata);
        }

        // POST: SchoolPdatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            SchoolPdata schoolPdata = db.SchoolPdatas.Find(id);
            db.SchoolPdatas.Remove(schoolPdata);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
