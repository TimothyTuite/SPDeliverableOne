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
    public class k_12ProfileController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: k_12Profile
        public ActionResult Index()
        {
            var k_12Profile = db.k_12Profile.Include(k => k.school);
            return View(k_12Profile.ToList());
        }

        // GET: k_12Profile/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            k_12Profile k_12Profile = db.k_12Profile.Find(id);
            if (k_12Profile == null)
            {
                return HttpNotFound();
            }
            return View(k_12Profile);
        }

        // GET: k_12Profile/Create
        public ActionResult Create()
        {
            ViewBag.schoolID = new SelectList(db.Schools, "ID", "SchoolName");
            return View();
        }

        // POST: k_12Profile/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,schoolID,dateCreated,dateApproved,schoolGrade,enrollment,freeOrReduced,isCharter,isMagnet,KReadiness,algPassRate,gradRate,emLearningGains,accelCoursework,emPerformance,AfricanAmerican,White,Asian,Hispanic,MixedOther,unspecified")] k_12Profile k_12Profile)
        {
            if (ModelState.IsValid)
            {
                db.k_12Profile.Add(k_12Profile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.schoolID = new SelectList(db.Schools, "ID", "SchoolName", k_12Profile.schoolID);
            return View(k_12Profile);
        }

        // GET: k_12Profile/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            k_12Profile k_12Profile = db.k_12Profile.Find(id);
            if (k_12Profile == null)
            {
                return HttpNotFound();
            }
            ViewBag.schoolID = new SelectList(db.Schools, "ID", "SchoolName", k_12Profile.schoolID);
            return View(k_12Profile);
        }

        // POST: k_12Profile/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,schoolID,dateCreated,dateApproved,schoolGrade,enrollment,freeOrReduced,isCharter,isMagnet,KReadiness,algPassRate,gradRate,emLearningGains,accelCoursework,emPerformance,AfricanAmerican,White,Asian,Hispanic,MixedOther,unspecified")] k_12Profile k_12Profile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(k_12Profile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.schoolID = new SelectList(db.Schools, "ID", "SchoolName", k_12Profile.schoolID);
            return View(k_12Profile);
        }

        // GET: k_12Profile/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            k_12Profile k_12Profile = db.k_12Profile.Find(id);
            if (k_12Profile == null)
            {
                return HttpNotFound();
            }
            return View(k_12Profile);
        }

        // POST: k_12Profile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            k_12Profile k_12Profile = db.k_12Profile.Find(id);
            db.k_12Profile.Remove(k_12Profile);
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
