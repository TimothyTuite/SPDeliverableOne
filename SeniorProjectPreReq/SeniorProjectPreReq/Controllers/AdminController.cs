using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using SeniorProjectPreReq.Models; 
using PagedList;
namespace SeniorProjectPreReq.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private ApplicationDbContext db = new ApplicationDbContext();
        // Controllers
        // GET: /Admin/
        [Authorize(Roles = "Administrator")]
        #region public ActionResult Index(string searchStringUserNameOrEmail)
        public ActionResult Index(string searchStringUserNameOrEmail, string currentFilter, int? page)
        {
            string joinQuery = "from ";
            try
            {
                int intPage = 10;
                int intPageSize = 10;
                int intTotalPageCount = 30;
                if (searchStringUserNameOrEmail != null)
                {
                    intPage = 1;
                }
                else
                {
                    if (currentFilter != null)
                    {
                        searchStringUserNameOrEmail = currentFilter;
                        intPage = page ?? 1;
                    }
                    else
                    {
                        searchStringUserNameOrEmail = "";
                        intPage = page ?? 1;
                    }
                }
                ViewBag.CurrentFilter = searchStringUserNameOrEmail;
                List<UserRoles.ExpandedUserDTO> col_UserDTO = new List<UserRoles.ExpandedUserDTO>();
                int intSkip = (intPage - 1) * intPageSize;
                intTotalPageCount = UserManager.Users
                    .Where(x => x.UserName.Contains(searchStringUserNameOrEmail))
                    .Count();
                var result = UserManager.Users
                    .Where(x => x.UserName.Contains(searchStringUserNameOrEmail))
                    .OrderBy(x => x.UserName)
                    .Skip(intSkip)
                    .Take(intPageSize)
                    .ToList();
                foreach (var item in result)
                {
                    //This If Statment removes all the users that are JPEF Admins from the list view
                    if (!UserManager.GetRoles(item.Id).Contains("Administrator")) {
                        UserRoles.ExpandedUserDTO objUserDTO = new UserRoles.ExpandedUserDTO();
                        objUserDTO.UserName = item.UserName;
                        objUserDTO.Email = item.Email;
                        objUserDTO.FirstName = item.FirstName;
                        objUserDTO.LastName = item.LastName;
                        objUserDTO.school = item.school;
                        objUserDTO.schoolID = item.schoolID;
                        objUserDTO.LockoutEndDateUtc = item.LockoutEndDateUtc;
                        col_UserDTO.Add(objUserDTO);
                    }
                }
                // Set the number of pages
                var _UserDTOAsIPagedList =
                    new StaticPagedList<UserRoles.ExpandedUserDTO>
                    (
                        col_UserDTO, intPage, intPageSize, intTotalPageCount
                        );
                return View(_UserDTOAsIPagedList);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error: " + ex);
                List<UserRoles.ExpandedUserDTO> col_UserDTO = new List<UserRoles.ExpandedUserDTO>();
                
                return View(col_UserDTO.ToPagedList(1, 25));
            }
        }
        #endregion

        // Utility
        #region public ApplicationUserManager UserManager
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ??
                    HttpContext.GetOwinContext()
                    .GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        #endregion
        #region public ApplicationRoleManager RoleManager
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ??
                    HttpContext.GetOwinContext()
                    .GetUserManager<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        #endregion

        
    }
}