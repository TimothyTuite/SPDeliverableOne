using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SeniorProjectPreReq.Models
{
    public class Volunteer
    {
        public int iD { get; set; }

        

        public string firstName { get; set; }
        public string lastName { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public bool pendingApproval { get; set; }
        public bool approved { get; set; }

    }

    public class VolunteerDBContext : DbContext
    {
        public DbSet<Volunteer> Volunteers { get; set; }
    }
}