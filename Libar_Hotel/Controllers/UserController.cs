using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Libar_Hotel.Models;
using System.Data.Entity;
using Libar_Hotel.Helpers;
namespace Libar_Hotel.Controllers
{
    public class UserController : Controller
    {
        
        private ApplicationDbContext dbContext;

        public UserController()
        {
            dbContext = new ApplicationDbContext();
        }


        // GET: User //Prikaz korisnika
        [Authorize(Roles="Menadzer")]
        public ActionResult Index()
        {
            List<User> users = new List<User>();

            try
            {
                users = dbContext.UserInfos
                       .Include(m => m.AspNetUser)
                       .Where(m => m.IsActive == true)
                       .ToList();

                if (users.Count <= 0)
                {
                    Message.Display(this, "Trenutno nema ni jednog korisnika u bazi");
                }

                
             
            }

            catch (Exception ex)
            {
                //Message.Display(this, $"Greska {ex.Message}");
                Console.WriteLine(ex.Message);
            }


            return View(users);

        }

        //Prikaz detalja korisnika
        [Authorize(Roles = "Menadzer")]
        public ActionResult Details(int id)
        {
             User userFromDb = null;

            try
            {
                userFromDb = dbContext.UserInfos
                            .Include(m => m.AspNetUser)
                            .SingleOrDefault(m => m.Id == id && m.IsActive == true);

                if (userFromDb != null)
                {
                    return View(userFromDb);
                }

                Message.Display(this, $"Korisnik sa id {id} ne postoji");
                
                            
                             
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction("Index", "User");
        }

        [Authorize(Roles ="Gost, Recepcionar, Menadzer")]
        //Pokazivanje detalje korisnika preko AspNetUserId ova metoda se koristi kada korisnik klikne na svoje korisnicko ime da prikaze svoje detalje 
       public ActionResult Manage(string userId)
        {
            User userFromDb = null;

            try
            {
                userFromDb = dbContext.UserInfos
                            .Include(m => m.AspNetUser)
                            .SingleOrDefault(m => m.AspNetUserId == userId && m.IsActive == true);

                if (userFromDb != null)
                {
                    
                    return View("Details",userFromDb);
                }

                Message.Display(this, $"Korisnik sa id {userId} ne postoji");



            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction("Index", "User");
        }
        
        [Authorize(Roles = "Menadzer")]
        public ActionResult DeleteDetails(int id)
        {
            User userFromDb = null;

            try
            {
                userFromDb = dbContext.UserInfos
                            .Include(m => m.AspNetUser)
                            .SingleOrDefault(m => m.Id == id && m.IsActive == true);

                if (userFromDb != null)
                {
                    return View(userFromDb);
                }

                Message.Display(this, $"Korisnik sa id {id} ne postoji");
                
                            
                             
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction("Index", "User");
        }

        [Authorize(Roles = "Menadzer")]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            User userFromDb = null;

            try
            {
                userFromDb = dbContext.UserInfos
                             .SingleOrDefault(m => m.Id == id && m.IsActive == true);

                if (userFromDb.Id != 0)
                {
                    userFromDb.IsActive = false;
                    dbContext.SaveChanges();

                    Message.Display(this, $"Korisnik sa id ${userFromDb.Id} je uspešno izbrisan");
                    return RedirectToAction("Index", "User");
                }

                else
                {
                    Message.Display(this, $"Korisnik sa id {userFromDb.Id} nije pronađen");
                }


            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return RedirectToAction("Index", "User");
        }


        //Detalji rezervacije korisnika
        [Authorize(Roles = "Gost, Recepcionar, Menadzer")]
        public ActionResult ReservationDetails(int id)
        {
            ReservationRequest reservationRequestFromDb = null;
            Reservation reservationFromDb = null;

            UserReservationViewModel userReservationView = new UserReservationViewModel();
            try
            {
                reservationRequestFromDb = dbContext.ReservationRequests
                                           .Include(m => m.User)
                                           .Include(m => m.ServiceType)
                                           .Include(m => m.RoomType)
                                           .SingleOrDefault(m => m.UserId == id);
                

                ViewBag.Message = "Vaš zahtev za rezervaciju je poslat";
                userReservationView.ReservationRequest = reservationRequestFromDb;
                
                //ukoliko  nema zahtev rezervacije proveri da li vec ima odobrenu
                if (reservationRequestFromDb == null)
                {
                    reservationFromDb = dbContext.Reservations
                                        .Include(m => m.User)
                                        .Include(m => m.ServiceType)
                                        .Include(m => m.Room)
                                        .Include(m => m.Room.RoomType)
                                        .SingleOrDefault(m => m.UserId == id && m.IsActive==true);



                    //Ukoliko nema ni rezervaciju
                    if (reservationFromDb == null)
                    {
                        ViewBag.Message = "Nemate rezervaciju";
                    }

                    //Ukoliko ima rezervaciju
                    else
                    {
                        userReservationView.Reservation = reservationFromDb;
                       
                        ViewBag.Message = "Vaša rezervacija je odobrena";
                    }
                  
                }


            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            userReservationView.UserId = id;
            return View(userReservationView);
        }
    }

    

}