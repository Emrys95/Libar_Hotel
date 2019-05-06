using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Libar_Hotel.Models
{
    public class UserRoleModel
    {
        public string AspUserId { get; set; }
        [Display(Name = "Korisničko ime")]
        public string UserName { get; set; }
        public string RoleName { get; set; }
        [Display(Name = "Uloge")]
        public List<string> Roles { get; set;}
    }
}