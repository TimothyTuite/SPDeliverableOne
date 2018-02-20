﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SeniorProjectPreReq.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here

            return userIdentity;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        [ForeignKey("school")]
        public int? schoolID { get; set; }

        public virtual School school { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<SeniorProjectPreReq.Models.Pictures> Pictures { get; set; }

        public System.Data.Entity.DbSet<SeniorProjectPreReq.Models.SchoolsProgram> SchoolsPrograms { get; set; }

        public System.Data.Entity.DbSet<SeniorProjectPreReq.Models.School> Schools { get; set; }

        public System.Data.Entity.DbSet<SeniorProjectPreReq.Models.Programs> Programs { get; set; }


        public System.Data.Entity.DbSet<SeniorProjectPreReq.Models.middleProfile> middleProfiles { get; set; }

        public System.Data.Entity.DbSet<SeniorProjectPreReq.Models.highschoolProfile> highschoolProfiles { get; set; }

        public System.Data.Entity.DbSet<SeniorProjectPreReq.Models.k_12Profile> k_12Profile { get; set; }
    }
}