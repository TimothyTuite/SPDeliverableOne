using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SeniorProjectPreReq.Models
{
    public class Volunteer
    {
        public int ID { get; set; }

        

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool PendingApproval { get; set; }
        public bool approved { get; set; }

    }

    public class VolunteerDBContext : DbContext
    {
        public DbSet<Volunteer> Volunteers { get; set; }
    }
}