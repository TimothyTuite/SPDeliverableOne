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

        public ActionResult AddPrograms()
        {
            //get the current users data 
            var userID = User.Identity.GetUserId(); 
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
            if (user.school.schoolsPrograms != null)
            {
                //user it to query for their schools program data for the last year save the ids to list
              listOfOld = user.school.schoolsPrograms.Where(m => m.year == lastYear()).Select(m => m.programID.ToString()).ToList();
              listOfCurrent = user.school.schoolsPrograms.Where(m => m.year == currentYear()).Select(m => m.programID.ToString()).ToList();
            } 

           if (listOfCurrent == null)
                {
                    
                    ViewBag.Message = "Showing the programs from last year, no programs for the current year";
                    ViewModel.ThePrograms = possible;
                    ViewData["oldPrograms"] = possible;
                    ViewModel.SelectedPrograms = listOfCurrent; 

                }
                else
                {
                    ViewBag.Message = "Showing selected Programs for the current ";
                    ViewModel.ThePrograms = possible;
                    ViewData["newPrograms"] = possible;
                    ViewModel.SelectedPrograms = listOfOld;

                }
            
           
            
            // create a multiple selection list that will be displayed and edited by the user, use the list of old ids to preselect values 
            if(user.schoolID.HasValue)
            {
                ViewModel.schoolID = user.schoolID.Value; 
            }
            ViewModel.ThePrograms = possible;
            ViewModel.Year = currentYear();
            //TODO: https://stackoverflow.com/questions/18363158/super-simple-implementation-of-multiselect-list-box-in-edit-view
            return View(ViewModel);
        }

        [HttpPost]
        public string AddPrograms(AddProgram model)
        {
            if(model.SelectedPrograms == null)
            {
                return "No Values Selected";
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Selected - " + string.Join(",", model.SelectedPrograms));
                return sb.ToString(); 
            }
        }
  
    }
}