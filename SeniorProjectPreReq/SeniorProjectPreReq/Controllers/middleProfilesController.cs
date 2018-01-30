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
    public class middleProfilesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: middleProfiles
        public ActionResult Index()
        {
            var middleProfiles = db.middleProfiles.Include(m => m.School);
            return View(middleProfiles.ToList());
        }

        // GET: middleProfiles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            middleProfile middleProfile = db.middleProfiles.Find(id);
            if (middleProfile == null)
            {
                return HttpNotFound();
            }
            return View(middleProfile);
        }

        // GET: middleProfiles/Create
        public ActionResult Create()
        {
            ViewBag.SchoolID = new SelectList(db.Schools, "ID", "SchoolName");
            return View();
        }

        // POST: middleProfiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SchoolID,DateCreated,DateApproved,SchoolGrade,Enrollment,FreeOrReduced,IsCharter,IsMagnet,ReadingELA,MathPerformance,AlgebraPassRate,AfricanAmerican,White,Asian,Hispanic,MixedOther,Unspecified")] middleProfile middleProfile)
        {
            if (ModelState.IsValid)
            {
                db.middleProfiles.Add(middleProfile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SchoolID = new SelectList(db.Schools, "ID", "SchoolName", middleProfile.SchoolID);
            return View(middleProfile);
        }

        // GET: middleProfiles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            middleProfile middleProfile = db.middleProfiles.Find(id);
            if (middleProfile == null)
            {
                return HttpNotFound();
            }
            ViewBag.SchoolID = new SelectList(db.Schools, "ID", "SchoolName", middleProfile.SchoolID);
            return View(middleProfile);
        }

        // POST: middleProfiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SchoolID,DateCreated,DateApproved,SchoolGrade,Enrollment,FreeOrReduced,IsCharter,IsMagnet,ReadingELA,MathPerformance,AlgebraPassRate,AfricanAmerican,White,Asian,Hispanic,MixedOther,Unspecified")] middleProfile middleProfile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(middleProfile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SchoolID = new SelectList(db.Schools, "ID", "SchoolName", middleProfile.SchoolID);
            return View(middleProfile);
        }

        // GET: middleProfiles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            middleProfile middleProfile = db.middleProfiles.Find(id);
            if (middleProfile == null)
            {
                return HttpNotFound();
            }
            return View(middleProfile);
        }

        // POST: middleProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            middleProfile middleProfile = db.middleProfiles.Find(id);
            db.middleProfiles.Remove(middleProfile);
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
