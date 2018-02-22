using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SeniorProjectPreReq.Models;

namespace SeniorProjectPreReq.Controllers
{
    public class SchoolPdatasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SchoolPdatas
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
            IEnumerable<SchoolMetricValues> collectMetrics = db.SchoolMetricValues.ToList();
            IEnumerable<Metrics> collectedMetrics;
            foreach(var i in allMetrics)
            {
                if(id == i.schoolID)
                {
                    
                }
            }
            if (schoolInfo.generalSchoolData == null)
            {
                return HttpNotFound();
            }
            return View(schoolInfo);
        }


        // GET: SchoolPdatas/Details/5
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
        public ActionResult Create([Bind(Include = "ID,SchoolName,SchoolPhone,SchoolAddress,SchoolWebsite,SchoolPrincipal,schoolTypeID")] SchoolPdata schoolPdata)
        {
            if (ModelState.IsValid)
            {
                db.SchoolPdatas.Add(schoolPdata);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.schoolTypeID = new SelectList(db.SchoolTypes, "ID", "Name", schoolPdata.schoolTypeID);
            return View(schoolPdata);
        }

        // GET: SchoolPdatas/Edit/5
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
        public ActionResult Edit([Bind(Include = "ID,SchoolName,SchoolPhone,SchoolAddress,SchoolWebsite,SchoolPrincipal,schoolTypeID")] SchoolPdata schoolPdata)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schoolPdata).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.schoolTypeID = new SelectList(db.SchoolTypes, "ID", "Name", schoolPdata.schoolTypeID);
            return View(schoolPdata);
        }

        // GET: SchoolPdatas/Delete/5
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
