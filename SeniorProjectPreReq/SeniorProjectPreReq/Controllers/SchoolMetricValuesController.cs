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
    public class SchoolMetricValuesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SchoolMetricValues
        public ActionResult Index()
        {
            var schoolMetricValues = db.SchoolMetricValues.Include(s => s.metric).Include(s => s.school);
            return View(schoolMetricValues.ToList());
        }

        // GET: SchoolMetricValues/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolMetricValues schoolMetricValues = db.SchoolMetricValues.Find(id);
            if (schoolMetricValues == null)
            {
                return HttpNotFound();
            }
            return View(schoolMetricValues);
        }

        // GET: SchoolMetricValues/Create
        public ActionResult Create()
        {
            ViewBag.metricID = new SelectList(db.Metrics, "ID", "MetricName");
            ViewBag.schoolID = new SelectList(db.SchoolPdatas, "ID", "SchoolName");
            return View();
        }

        // POST: SchoolMetricValues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,schoolID,metricID,year,dateCreated")] SchoolMetricValues schoolMetricValues)
        {
            if (ModelState.IsValid)
            {
                db.SchoolMetricValues.Add(schoolMetricValues);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.metricID = new SelectList(db.Metrics, "ID", "MetricName", schoolMetricValues.metricID);
            ViewBag.schoolID = new SelectList(db.SchoolPdatas, "ID", "SchoolName", schoolMetricValues.schoolID);
            return View(schoolMetricValues);
        }

        // GET: SchoolMetricValues/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolMetricValues schoolMetricValues = db.SchoolMetricValues.Find(id);
            if (schoolMetricValues == null)
            {
                return HttpNotFound();
            }
            ViewBag.metricID = new SelectList(db.Metrics, "ID", "MetricName", schoolMetricValues.metricID);
            ViewBag.schoolID = new SelectList(db.SchoolPdatas, "ID", "SchoolName", schoolMetricValues.schoolID);
            return View(schoolMetricValues);
        }

        // POST: SchoolMetricValues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,schoolID,metricID,year,dateCreated")] SchoolMetricValues schoolMetricValues)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schoolMetricValues).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.metricID = new SelectList(db.Metrics, "ID", "MetricName", schoolMetricValues.metricID);
            ViewBag.schoolID = new SelectList(db.SchoolPdatas, "ID", "SchoolName", schoolMetricValues.schoolID);
            return View(schoolMetricValues);
        }

        // GET: SchoolMetricValues/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolMetricValues schoolMetricValues = db.SchoolMetricValues.Find(id);
            if (schoolMetricValues == null)
            {
                return HttpNotFound();
            }
            return View(schoolMetricValues);
        }

        // POST: SchoolMetricValues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SchoolMetricValues schoolMetricValues = db.SchoolMetricValues.Find(id);
            db.SchoolMetricValues.Remove(schoolMetricValues);
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
