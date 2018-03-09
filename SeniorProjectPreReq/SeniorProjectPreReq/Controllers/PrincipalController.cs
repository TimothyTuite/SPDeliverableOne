using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SeniorProjectPreReq.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Mvc;
using SeniorProjectPreReq.Models.FormModels;
using System.Text;
using System.Data.SqlClient;

namespace SeniorProjectPreReq.Controllers
{
    public class PrincipalController : Controller
    {
        private ApplicationDbContext dataContext = new ApplicationDbContext();
        //use this to get the current user data; 
        private ApplicationUserManager _userManager;
        public PrincipalController()
        {

        }
        public PrincipalController(ApplicationUserManager userManager)
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
        // GET: Principle
        public ActionResult Index()
        {
            return View();
        }

        //simple functions return the current year 
        public int currentYear()
        {
            return DateTime.Now.Year; 
        }
        //simple function returns last year
        public int lastYear()
        {
            return DateTime.Now.Year - 1;
        }
        public int nextYear()
        {
            return DateTime.Now.Year + 1; 
        }
        public ActionResult EditProgramYear()
        {
            List<SelectListItem> year = new List<SelectListItem>();
            year.Add(new SelectListItem
            {
                Text = currentYear().ToString(),
                Value = currentYear().ToString()
            });
            year.Add(new SelectListItem
            {
                Text = nextYear().ToString(),
                Value = currentYear().ToString()
            });

            ViewBag.year = year; 
            return View(); 
        }

        [HttpPost]
        public ActionResult EditProgramYear(string year)
        {
            return RedirectToAction("AddPrograms/" + year);
        }
        [HttpGet]
        public ActionResult AddPrograms(string year)
        {
            //if no year is submitted and the year is any other than the current or the next redirect 
            if (year == null || (year != currentYear().ToString() && year != nextYear().ToString()))
            {
                return RedirectToAction("EditProgramYear");
            }
            //get the current users data 
            var userID = User.Identity.GetUserId();
            var ArePrograms = false; 
            var user = UserManager.FindById(userID);
            IEnumerable<string> listOfOld = null;
            IEnumerable<string> listOfCurrent = null;
            AddProgram ViewModel = new AddProgram();
            //query for current avaliable programs 
            var p = dataContext.Programs.Where(m => m.TypeID == user.school.schoolTypeID);
            IEnumerable<SelectListItem> possible = p.Select(x => new SelectListItem
            {
                Value = x.ID.ToString(),
                Text = x.programName
            });
            try
            {
                if (user.school.schoolsPrograms.Any())
                {
                    ArePrograms = true; 
                }
            }catch(ArgumentNullException e)
            {

            }
            if (ArePrograms)
            {
                //user it to query for their schools program data for the last year save the ids to list
              listOfOld = user.school.schoolsPrograms.Where(m => m.year == lastYear()).Select(m => m.programID.ToString()).ToList();
              listOfCurrent = user.school.schoolsPrograms.Where(m => m.year == currentYear()).Select(m => m.programID.ToString()).ToList();
            } 

           if (listOfCurrent == null)
                {
                    
                    ViewBag.Message = "Showing the programs from last year, no programs for the current year";
                    ViewModel.ThePrograms = possible;
                    ViewModel.SelectedPrograms = listOfOld;
                    ViewModel.previousPrograms = listOfOld;

                }
                else
                {
                    ViewBag.Message = "Showing selected Programs for the current ";
                    ViewModel.ThePrograms = possible;
                    ViewModel.SelectedPrograms = listOfCurrent;
                    ViewModel.previousPrograms = listOfCurrent;  

                }
            
           
            
            // create a multiple selection list that will be displayed and edited by the user, use the list of old ids to preselect values 
            if(user.schoolID.HasValue)
            {
                ViewModel.schoolID = user.schoolID.Value; 
            }
            ViewModel.schoolID = user.schoolID ?? default(int);
            ViewModel.schoolName = user.school.SchoolName; 
            ViewModel.ThePrograms = possible;
            ViewModel.Year = Int32.Parse(year);
            //TODO: https://stackoverflow.com/questions/18363158/super-simple-implementation-of-multiselect-list-box-in-edit-view
            return View(ViewModel);
        }

        [HttpPost]
        public ActionResult savePrograms(AddProgram model)
        {
            string[] previous = new string[] {};
            string[] updated = new string[] { };
          
            if(model.SelectedPrograms == null)
            {
                return RedirectToAction("addPrograms",model.Year);
            }
            else
            {
                try
                {
                    previous = model.previousPrograms.Select(x => x).ToArray();
                    updated = model.SelectedPrograms.Select(x => x).ToArray();
                    List<string> add = findNew(previous, updated);
                    List<string> del = findRemoved(previous, updated);
                    for(var i =0; i < add.Count; i++)
                    {
                        var schoolProgram = new SchoolProgramsValues();
                        schoolProgram.programID = Int32.Parse(add[i]);
                        schoolProgram.schoolID = model.schoolID;
                        schoolProgram.year = model.Year;
                        schoolProgram.hasProgram = true;
                        schoolProgram.Approved = false;
                        schoolProgram.dateCreated = DateTime.Now;
                        dataContext.SchoolProgramsValues.Add(schoolProgram); 
                    }
                    for (var i = 0; i < del.Count; i++)
                    {
                        var delSchoolProgram = dataContext.SchoolProgramsValues.SingleOrDefault(x => x.schoolID == model.schoolID && x.year == model.Year && x.programID == Int32.Parse(del[i]));
                        dataContext.SchoolProgramsValues.Remove(delSchoolProgram); 
                    }
                    dataContext.SaveChanges(); 
                }
                catch(ArgumentNullException x)
                {
                    //if there are no previous add all the updated to database
                    

                    
                    List<string> add = model.SelectedPrograms.ToList();
                    for (var i = 0; i < add.Count; i++)
                    {
                       
                        var schoolProgram = new SchoolProgramsValues();
                        schoolProgram.programID = Int32.Parse(add[i]);
                        schoolProgram.schoolID = model.schoolID;
                        schoolProgram.year = model.Year;
                        schoolProgram.hasProgram = true;
                        schoolProgram.Approved = false;
                        schoolProgram.dateCreated = DateTime.Now;
                        dataContext.SchoolProgramsValues.Add(schoolProgram);
                    }
                    try
                    {
                        dataContext.SaveChanges();
                    }catch(SqlException e)
                    {
                        return Content(e.ToString(),model.schoolID.ToString()); 
                    }
                }
               
                return RedirectToAction("Saved");
            }
            return RedirectToAction("Account", "UserHome"); 
        }

        public ActionResult Saved()
        {
            return View(); 
        }
        public List<string> findNew(string[] previous, string[] updated)
        {
            List<string> result = new List<string>(updated); 
            for(var i = 0; i < previous.Length; i++)
            {
                result.Remove(previous[i]); 
            }

            return result; 
        }

        public List<string> findRemoved(string[] previous, string[] updated)
        {
            List<string> upd = new List<string>(updated); 
            List<string> result = new List<string>();
            for(var i = 0; i < previous.Length; i++)
            {
                var found = upd.Remove(previous[i]);
                if (!found)
                {
                    result.Add(previous[i]); 
                }
            }
            return result; 
        }
    }
}