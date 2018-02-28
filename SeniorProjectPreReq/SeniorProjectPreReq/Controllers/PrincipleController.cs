using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SeniorProjectPreReq.Models;

namespace SeniorProjectPreReq.Controllers
{
    public class PrincipleController : Controller
    {
        private ApplicationDbContext dataContext = new ApplicationDbContext();
        //use this to get the current user data; 
        private ApplicationUserManager UserManager;  
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

        public ActionResult AddProgram()
        {
            //get the current users data 
            var user = UserManager.FindById(User.Identity.GetUserId());
            //user it to query for their schools program data for the last year save the ids to list
            List<int> listOfOld = user.school.schoolsPrograms.Where(m => m.year == lastYear()).Select(m=> m.programID).ToList();
            //query for current avaliable programs 
            List<Program> p = dataContext.Programs.Where(m => m.TypeID == user.school.schoolTypeID).ToList();
            // create a multiple selection list that will be displayed and edited by the user, use the list of old ids to preselect values 
            MultiSelectList possible = new MultiSelectList(p, "ID", "Name", listOfOld);
            return View();
        }
    }
}