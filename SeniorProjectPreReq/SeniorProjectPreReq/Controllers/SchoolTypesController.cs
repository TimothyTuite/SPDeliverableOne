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
    public class SchoolTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SchoolTypes
        public ActionResult Index()
        {
            return View(db.SchoolTypes.ToList());
        }

        // GET: SchoolTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolType schoolType = db.SchoolTypes.Find(id);
            if (schoolType == null)
            {
                return HttpNotFound();
            }
            return View(schoolType);
        }

        // GET: SchoolTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SchoolTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] SchoolType schoolType)
        {
            if (ModelState.IsValid)
            {
                db.SchoolTypes.Add(schoolType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(schoolType);
        }

        // GET: SchoolTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolType schoolType = db.SchoolTypes.Find(id);
            if (schoolType == null)
            {
                return HttpNotFound();
            }
            return View(schoolType);
        }

        // POST: SchoolTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] SchoolType schoolType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schoolType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(schoolType);
        }

        // GET: SchoolTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolType schoolType = db.SchoolTypes.Find(id);
            if (schoolType == null)
            {
                return HttpNotFound();
            }
            return View(schoolType);
        }

        // POST: SchoolTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SchoolType schoolType = db.SchoolTypes.Find(id);
            db.SchoolTypes.Remove(schoolType);
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
