using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SeniorProjectPreReq.Models;

namespace SeniorProjectPreReq.Controllers
{

   

    [Authorize]
    public class youtubeURLsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;
        public youtubeURLsController()
        {

        }
        public youtubeURLsController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: youtubeURLs
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            var youtubeURLs = db.youtubeURLs.Include(y => y.school);
            return View(youtubeURLs.ToList());
        }

        // GET: youtubeURLs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            youtubeURL youtubeURL = db.youtubeURLs.Find(id);
            if (youtubeURL == null)
            {
                return HttpNotFound();
            }
            return View(youtubeURL);
        }

        // GET: youtubeURLs/Create
        public ActionResult Create()
        {
            ViewBag.schoolID = new SelectList(db.SchoolPdatas, "ID", "SchoolName");
            ViewBag.date = DateTime.Now.ToString();
            return View();
        }

        // POST: youtubeURLs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "schoolID,URL,year,dateCreated")] youtubeURL youtubeURL)
        {
            var userID = User.Identity.GetUserId();
            var user = UserManager.FindById(userID);
            youtubeURL.schoolID = Convert.ToInt32(user.schoolID);
            if (ModelState.IsValid)
            {
                db.youtubeURLs.Add(youtubeURL);
                db.SaveChanges();
                if (User.IsInRole("Administrator"))
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Saved", "Principal");
            }
            else
            {
                return RedirectToAction("Create", "YoutubeURLs");
            }
        }

        // GET: youtubeURLs/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult CreateByAdmin()
        {
            ViewBag.schoolID = new SelectList(db.SchoolPdatas, "ID", "SchoolName");
            ViewBag.date = DateTime.Now.ToString();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult CreateByAdmin([Bind(Include = "schoolID,URL,year,dateCreated")] youtubeURL youtubeURL)
        {
            if (ModelState.IsValid)
            {
                youtubeURL.Approved = true; 
                db.youtubeURLs.Add(youtubeURL);
                db.SaveChanges();
                if (User.IsInRole("Administrator"))
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("UserHome", "Account");
            }

            ViewBag.schoolID = new SelectList(db.SchoolPdatas, "ID", "SchoolName", youtubeURL.schoolID);
            return View(youtubeURL);
        }

        // GET: youtubeURLs/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            youtubeURL youtubeURL = db.youtubeURLs.Find(id);
            if (youtubeURL == null)
            {
                return HttpNotFound();
            }
            ViewBag.schoolID = new SelectList(db.SchoolPdatas, "ID", "SchoolName", youtubeURL.schoolID);
            return View(youtubeURL);
        }

        // POST: youtubeURLs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "ID,schoolID,URL,year,dateCreated,Approved")] youtubeURL youtubeURL)
        {
            if (ModelState.IsValid)
            {
                db.Entry(youtubeURL).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("YoutubeApprovals");
            }
            ViewBag.schoolID = new SelectList(db.SchoolPdatas, "ID", "SchoolName", youtubeURL.schoolID);
            return View(youtubeURL);
        }

        // GET: youtubeURLs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            youtubeURL youtubeURL = db.youtubeURLs.Find(id);
            if (youtubeURL == null)
            {
                return HttpNotFound();
            }
            return View(youtubeURL);
        }

        // POST: youtubeURLs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            youtubeURL youtubeURL = db.youtubeURLs.Find(id);
            db.youtubeURLs.Remove(youtubeURL);
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
        [Authorize(Roles = "Administrator")]
        // GET: youtubeURLs
        public ActionResult YoutubeApprovals()
        {
            var schools = db.SchoolPdatas.ToList();
            List<int> ids = new List<int>();
            foreach(var item in schools)
            {
                ids.Add(item.ID);
            }
            var youtubeURLs = CollectRecentYoutubeURLs(ids);
            return View(youtubeURLs);
        }
        public List<youtubeURL> CollectRecentYoutubeURLs(List<int> ids)
        {
            List<youtubeURL> urls = new List<youtubeURL>();
            //IEnumerable<youtubeURL> urls;
            foreach(var item in ids)
            {
                urls.Add(YoutubeQuery(item));
            }
            return urls;
        }
        public youtubeURL YoutubeQuery(int? id)
        {
            var index = id;
            var query = from a in db.youtubeURLs
                        where a.schoolID == index
                        orderby a.dateCreated descending
                        select a;
            var item = query.FirstOrDefault();
            if (item != null)
            {
                Debug.WriteLine(item.school.SchoolName);
            }
            return item;
        }
    }
}
