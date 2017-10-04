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
    public class SchoolsController : Controller
    {
        private SchoolDBContext db = new SchoolDBContext();

        // GET: Schools
        public ActionResult Index()
        {
            return View(db.Movies.ToList());
        }

        // GET: Schools/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            School school = db.Movies.Find(id);
            if (school == null)
            {
                return HttpNotFound();
            }
            return View(school);
        }

        // GET: Schools/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Schools/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Editors_ID,SchoolName,SchoolPhone,SchoolAddress,SchoolWebsite,SchoolPrincipal,StudentsEnrolled,ReducedLunchPercentage,SchoolGrade,IsCharter,MHSReadingPercentage,MHSMathPercentage,AlgebraIPassRatePercentage,AccCourseParticipationPercentage,PSRReadyReadingPercentage,PSReadyMathPercentage")] School school)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(school);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(school);
        }

        // GET: Schools/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            School school = db.Movies.Find(id);
            if (school == null)
            {
                return HttpNotFound();
            }
            return View(school);
        }

        // POST: Schools/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Editors_ID,SchoolName,SchoolPhone,SchoolAddress,SchoolWebsite,SchoolPrincipal,StudentsEnrolled,ReducedLunchPercentage,SchoolGrade,IsCharter,MHSReadingPercentage,MHSMathPercentage,AlgebraIPassRatePercentage,AccCourseParticipationPercentage,PSRReadyReadingPercentage,PSReadyMathPercentage")] School school)
        {
            if (ModelState.IsValid)
            {
                db.Entry(school).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(school);
        }

        // GET: Schools/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            School school = db.Movies.Find(id);
            if (school == null)
            {
                return HttpNotFound();
            }
            return View(school);
        }

        // POST: Schools/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            School school = db.Movies.Find(id);
            db.Movies.Remove(school);
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
