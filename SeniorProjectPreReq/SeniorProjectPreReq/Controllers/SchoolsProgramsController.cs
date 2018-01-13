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
    public class SchoolsProgramsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        private IEnumerable<SelectListItem> populateProgramsList(object ProgramsData = null)
        {
            var s = db.Programs;
            var items = new HashSet<SelectListItem>();
            foreach (var i in s)
            {
                var item = new SelectListItem();
                item.Value = i.ProgramID.ToString();
                item.Text = i.Program_Name;

                items.Add(item);
            }

            return items;
        }
        // GET: SchoolsPrograms
        public ActionResult Index()
        {

            return View(db.SchoolsPrograms.ToList());
        }
        public ActionResult AddSchoolsPrograms(string id)
        {
            if (id == null)
            {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }         
            var schoolID = Int32.Parse(id);
            School school = db.Schools.Find(schoolID);
          if (school == null)
          {
               return HttpNotFound();
          }
            var pList = populateProgramsList();
            ViewData["SchoolName"] = school.SchoolName;
            ViewData["schoolID"] = school.ID; 
            ViewData["ProgramsList"] = pList;
            return View();
        }

        //POST: SchoolsPrograms/AddSchoolsPrograms/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSchoolsPrograms([Bind(Include = "ID,SchoolID,ProgramID")] SchoolsProgram schoolsProgram)
        {
            if (ModelState.IsValid)
            {
                db.SchoolsPrograms.Add(schoolsProgram);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(schoolsProgram); 

        }
        // GET: SchoolsPrograms/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var schoolID = Int32.Parse(id); 
            SchoolsProgram schoolsProgram = db.SchoolsPrograms.Find(schoolID);
            if (schoolsProgram == null)
            {
                return HttpNotFound();
            }
            return View(schoolsProgram);
        }

        // GET: SchoolsPrograms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SchoolsPrograms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,SchoolID,ProgramID")] SchoolsProgram schoolsProgram)
        {
            if (ModelState.IsValid)
            {
                db.SchoolsPrograms.Add(schoolsProgram);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(schoolsProgram);
        }

        // GET: SchoolsPrograms/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolsProgram schoolsProgram = db.SchoolsPrograms.Find(id);
            if (schoolsProgram == null)
            {
                return HttpNotFound();
            }
            return View(schoolsProgram);
        }

        // POST: SchoolsPrograms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,SchoolID,ProgramID")] SchoolsProgram schoolsProgram)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schoolsProgram).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(schoolsProgram);
        }

        // GET: SchoolsPrograms/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolsProgram schoolsProgram = db.SchoolsPrograms.Find(id);
            if (schoolsProgram == null)
            {
                return HttpNotFound();
            }
            return View(schoolsProgram);
        }

        // POST: SchoolsPrograms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SchoolsProgram schoolsProgram = db.SchoolsPrograms.Find(id);
            db.SchoolsPrograms.Remove(schoolsProgram);
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
