using Libar_Hotel.Models; // dodato zbog ApplicationDbContext
using Microsoft.AspNet.Identity; // dodato zbog RoleManager
using Microsoft.AspNet.Identity.EntityFramework; //dodato zbog Rolestore
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; // dodato zbog HTTP post metode Create koja je async metoda
using System.Web;
using System.Web.Mvc;
using Libar_Hotel.Helpers;

namespace Libar_Hotel.Controllers
{

    [Authorize(Roles="Menadzer")]
    public class RoleController : Controller
    {
        // ovo je veza sa tabelom asp.net roles
        private RoleStore<IdentityRole> roleStore;

        //ima metode create, update, edit, delete
        private RoleManager<IdentityRole> roleManager;

        private ApplicationDbContext dbContext;

        public RoleController()
        {

            dbContext = new ApplicationDbContext();
            roleStore = new RoleStore<IdentityRole>(new ApplicationDbContext());
            roleManager = new RoleManager<IdentityRole>(roleStore);
        }

        // GET: Role
        public ActionResult Index()
        {
            try
            {
                
                //Prosledjivanje pogledu nazive korisnika i njegove uloge
                var usersAndRoles = (from users in dbContext.Users
                                     select new
                                     {
                                         UserName = users.UserName,
                                         Email = users.Email,
                                         UserId = users.Id,
                                         RoleNames = (from userRoles in users.Roles
                                                      join role in dbContext.Roles
                                                      on userRoles.RoleId equals role.Id
                                                      select role.Name).ToList()
                                     }).ToList()
                                     .Select(m => new UserInRolesViewModel()
                                     {
                                         UserId = m.UserId,
                                         UserName = m.UserName,
                                         Email = m.Email,
                                         Role = string.Join(",", m.RoleNames)
                                     }).ToList();

                return View(usersAndRoles);
                                                    
                                                   
                                   
                
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            return View();
        }

        //Get: Role/Create
        public ActionResult Create()
        {
            return View();
        }

        //Ubacuje novu ulogu u bazu
        [HttpPost]
        public async Task<ActionResult> Create(RoleViewModel roleModel)
        {
            var role = new IdentityRole(roleModel.Name);
            var result = await roleManager.CreateAsync(role);
            if(result.Succeeded)
            {
                Message.Display(this, $"Uspešno ste kreirali ulogu: {roleModel.Name}");
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

      

       

    }
}