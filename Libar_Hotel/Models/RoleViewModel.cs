using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Libar_Hotel.Models
{
    public class RoleViewModel
    {
        public string Id { get; set; }

        [Display(Name="Uloga")]
        public string Name { get; set; }

        public List<ApplicationUser> AspUsers { get; set; }

        public List<IdentityRole> Roles {get; set;}

        public UserRoleModel UserRoleModel { get; set; }
    }
}