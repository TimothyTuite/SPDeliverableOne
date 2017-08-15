using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SeniorProjectPreReq.Models
{
    public class UserRoles
    {
        public class ExpandedUserDTO
        {
            [Key]
            [Display(Name = "User Name")]
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            [Display(Name = "Lockout End Date UTC")]
            public DateTime? LockoutEndDateUtc { get; set; }
            public int AccessFailedCount { get; set; }
            public string PhoneNumber { get; set; }

            public IEnumerable<UserRoleDTO> Roles { get; set; }
        }

        public class UserRoleDTO
        {
            [Key]
            [Display(Name = "Role Name")]
            public string RoleName { get; set; }
        }

        public class RoleDTO
        {
            [Key]
            public string Id { get; set; }
            [Display(Name = "Role Name")]
            public string RoleName { get; set; }
        }

        public class UserAndRoleDTO
        {
            [Key]
            [Display(Name = "User Name")]
            public string UserName { get; set; }
            public List<UserRoleDTO> colUserRoleDTO { get; set; }
        }
    }
}