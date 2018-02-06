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
    public class highschoolProfilesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: highschoolProfiles
        public ActionResult Index()
        {
            var highschoolProfiles = db.highschoolProfiles.Include(h => h.school);
            return View(highschoolProfiles.ToList());
        }

        // GET: highschoolProfiles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            highschoolProfile highschoolProfile = db.highschoolProfiles.Find(id);
            if (highschoolProfile == null)
            {
                return HttpNotFound();
            }
            return View(highschoolProfile);
        }

        // GET: highschoolProfiles/Create
        public ActionResult Create()
        {
            ViewBag.schoolID = new SelectList(db.Schools, "ID", "SchoolName");
            return View();
        }

        // POST: highschoolProfiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,schoolID,dateCreated,dateApproved,schoolGrade,enrollment,freeOrReduced,isCharter,isMagnet,gradRate,emLearningGains,accelCoursework,emPerformance,AfricanAmerican,White,Asian,Hispanic,MixedOther,unspecified")] highschoolProfile highschoolProfile)
        {
            if (ModelState.IsValid)
            {
                db.highschoolProfiles.Add(highschoolProfile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.schoolID = new SelectList(db.Schools, "ID", "SchoolName", highschoolProfile.schoolID);
            return View(highschoolProfile);
        }

        // GET: highschoolProfiles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            highschoolProfile highschoolProfile = db.highschoolProfiles.Find(id);
            if (highschoolProfile == null)
            {
                return HttpNotFound();
            }
            ViewBag.schoolID = new SelectList(db.Schools, "ID", "SchoolName", highschoolProfile.schoolID);
            return View(highschoolProfile);
        }

        // POST: highschoolProfiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,schoolID,dateCreated,dateApproved,schoolGrade,enrollment,freeOrReduced,isCharter,isMagnet,gradRate,emLearningGains,accelCoursework,emPerformance,AfricanAmerican,White,Asian,Hispanic,MixedOther,unspecified")] highschoolProfile highschoolProfile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(highschoolProfile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.schoolID = new SelectList(db.Schools, "ID", "SchoolName", highschoolProfile.schoolID);
            return View(highschoolProfile);
        }

        // GET: highschoolProfiles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            highschoolProfile highschoolProfile = db.highschoolProfiles.Find(id);
            if (highschoolProfile == null)
            {
                return HttpNotFound();
            }
            return View(highschoolProfile);
        }

        // POST: highschoolProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            highschoolProfile highschoolProfile = db.highschoolProfiles.Find(id);
            db.highschoolProfiles.Remove(highschoolProfile);
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
