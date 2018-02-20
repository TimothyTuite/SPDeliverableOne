using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.ModelConfiguration.Conventions;

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

        public virtual SchoolPdata school { get; set; }
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
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // need this line or identity error 
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }
        public System.Data.Entity.DbSet<SeniorProjectPreReq.Models.SchoolPdata> SchoolPdatas { get; set; }

        public System.Data.Entity.DbSet<SeniorProjectPreReq.Models.SchoolType> SchoolTypes { get; set; }

        public System.Data.Entity.DbSet<SeniorProjectPreReq.Models.SchoolMetricValues> SchoolMetricValues { get; set; }

        public System.Data.Entity.DbSet<SeniorProjectPreReq.Models.Metrics> Metrics { get; set; }

        public System.Data.Entity.DbSet<SeniorProjectPreReq.Models.SchoolProgramsValues> SchoolProgramsValues { get; set; }

        public System.Data.Entity.DbSet<SeniorProjectPreReq.Models.Program> Programs { get; set; }

        public System.Data.Entity.DbSet<SeniorProjectPreReq.Models.youtubeURL> youtubeURLs { get; set; }
    }
}