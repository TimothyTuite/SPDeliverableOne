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
    public class SchoolProgramsValuesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SchoolProgramsValues
        public ActionResult Index()
        {
            var schoolProgramsValues = db.SchoolProgramsValues.Include(s => s.school).Include(s => s.theProgram);
            return View(schoolProgramsValues.ToList());
        }

        // GET: SchoolProgramsValues/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolProgramsValues schoolProgramsValues = db.SchoolProgramsValues.Find(id);
            if (schoolProgramsValues == null)
            {
                return HttpNotFound();
            }
            return View(schoolProgramsValues);
        }

        // GET: SchoolProgramsValues/Create
        public ActionResult Create()
        {
            ViewBag.schoolID = new SelectList(db.SchoolPdatas, "ID", "SchoolName");
            ViewBag.programID = new SelectList(db.Programs, "ID", "programName");
            return View();
        }

        // POST: SchoolProgramsValues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,schoolID,programID,year,hasProgram,dateCreated")] SchoolProgramsValues schoolProgramsValues)
        {
            if (ModelState.IsValid)
            {
                db.SchoolProgramsValues.Add(schoolProgramsValues);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.schoolID = new SelectList(db.SchoolPdatas, "ID", "SchoolName", schoolProgramsValues.schoolID);
            ViewBag.programID = new SelectList(db.Programs, "ID", "programName", schoolProgramsValues.programID);
            return View(schoolProgramsValues);
        }

        // GET: SchoolProgramsValues/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolProgramsValues schoolProgramsValues = db.SchoolProgramsValues.Find(id);
            if (schoolProgramsValues == null)
            {
                return HttpNotFound();
            }
            ViewBag.schoolID = new SelectList(db.SchoolPdatas, "ID", "SchoolName", schoolProgramsValues.schoolID);
            ViewBag.programID = new SelectList(db.Programs, "ID", "programName", schoolProgramsValues.programID);
            return View(schoolProgramsValues);
        }

        // POST: SchoolProgramsValues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,schoolID,programID,year,hasProgram,dateCreated")] SchoolProgramsValues schoolProgramsValues)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schoolProgramsValues).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.schoolID = new SelectList(db.SchoolPdatas, "ID", "SchoolName", schoolProgramsValues.schoolID);
            ViewBag.programID = new SelectList(db.Programs, "ID", "programName", schoolProgramsValues.programID);
            return View(schoolProgramsValues);
        }

        // GET: SchoolProgramsValues/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolProgramsValues schoolProgramsValues = db.SchoolProgramsValues.Find(id);
            if (schoolProgramsValues == null)
            {
                return HttpNotFound();
            }
            return View(schoolProgramsValues);
        }

        // POST: SchoolProgramsValues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SchoolProgramsValues schoolProgramsValues = db.SchoolProgramsValues.Find(id);
            db.SchoolProgramsValues.Remove(schoolProgramsValues);
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
