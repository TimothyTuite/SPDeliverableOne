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
    public class MetricsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Metrics
        public ActionResult Index()
        {
            return View(db.Metrics.ToList());
        }

        // GET: Metrics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Metrics metrics = db.Metrics.Find(id);
            if (metrics == null)
            {
                return HttpNotFound();
            }
            return View(metrics);
        }

        // GET: Metrics/Create
        public ActionResult Create()
        {
            ViewBag.schoolLevel = new SelectList(db.SchoolTypes, "ID", "Name");
            return View();
        }

        // POST: Metrics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MetricName,Description,schoolLevel,rangeTop,rangeBottom")] Metrics metrics)
        {
            if (ModelState.IsValid)
            {
                db.Metrics.Add(metrics);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(metrics);
        }

        // GET: Metrics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Metrics metrics = db.Metrics.Find(id);
            if (metrics == null)
            {
                return HttpNotFound();
            }
            var list = new SelectList(db.SchoolTypes, "ID", "Name",metrics.schoolLevel);
            ViewBag.schoolLevel = list; 
            return View(metrics);
        }

        // POST: Metrics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MetricName,Description,schoolLevel,rangeTop,rangeBottom")] Metrics metrics)
        {
            if (ModelState.IsValid)
            {
                db.Entry(metrics).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(metrics);
        }

        // GET: Metrics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Metrics metrics = db.Metrics.Find(id);
            if (metrics == null)
            {
                return HttpNotFound();
            }
            return View(metrics);
        }

        // POST: Metrics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Metrics metrics = db.Metrics.Find(id);
            db.Metrics.Remove(metrics);
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
