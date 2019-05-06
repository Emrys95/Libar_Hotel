using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Libar_Hotel.Models;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Diagnostics;
using Libar_Hotel.Helpers;

namespace Libar_Hotel.Controllers
{
    public class ReservationRequestController : Controller
    {

        private ApplicationDbContext dbContext;

        public ReservationRequestController()
        {
            dbContext = new ApplicationDbContext();
        }
        // GET: ReservationRequest
        [Authorize(Roles ="Recepcionar, Menadzer")]
        public ActionResult Index()
        {
            List<ReservationRequest> reservationRequestsfromDb = new List<ReservationRequest>();
            try
            {
                reservationRequestsfromDb = dbContext.ReservationRequests
                                            .Include(m=>m.User)
                                            .Include(m=>m.RoomType)
                                            .Include(m=>m.ServiceType)
                                            .Include(m=>m.RoomView)
                                            .ToList();

                return View(reservationRequestsfromDb);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            return View();
        }

        [Authorize(Roles ="Gost, Recepcionar, Menadzer")]
        public ActionResult Create(string id)
        {
            try
            {
                

                //Uzimanje idOdUser kako bi ga vezali za zahtev rezervacije
                int idUser = dbContext.UserInfos.SingleOrDefault(m => m.AspNetUserId == id).Id;


                //Ako je korisnik poslao zahtev ili mu je odobrena rezervacija automatski ga odvedi do svoje stranice sa prikazom rezervacije
                if (IsHavingReservation(idUser))
                {
                    return RedirectToAction("ReservationDetails", "User", new { id = idUser });
                }

                if (idUser != 0)
                {
                    //Postavljanje IdUser za zahtev rezervacije
                    ReservationRequestViewModel reservationRequestModel = new ReservationRequestViewModel();
                    reservationRequestModel.ReservationRequest = new ReservationRequest();
                    reservationRequestModel.ReservationRequest.UserId = idUser;
                    

                    //Popunjavanje Lista za padajuci meni
                    var serviceTypes = dbContext.ServiceTypes.Where(m => m.IsActive == true).ToList();
                    var roomTypes = dbContext.RoomTypes.Where(m => m.IsActive == true).ToList();
                    var roomViews = dbContext.RoomViews.ToList();
                    //Postavi modelu te liste kako bi korisnik izabrao uslugu, tip sobe, pogled
                    reservationRequestModel.ServiceTypes = serviceTypes;
                    reservationRequestModel.RoomTypes = roomTypes;
                    reservationRequestModel.RoomViews = roomViews;
                    
                    
                    return View(reservationRequestModel);
                }
            }

            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            

            return View();
        }

        [Authorize(Roles = "Gost, Recepcionar, Menadzer")]
        [HttpPost]
        public ActionResult Create(ReservationRequestViewModel reservationRequestModel)
        {



            //Popunjavanje Lista za padajuci meni
            var serviceTypes = dbContext.ServiceTypes.Where(m => m.IsActive == true).ToList();
            var roomTypes = dbContext.RoomTypes.Where(m => m.IsActive == true).ToList();
            var roomViews = dbContext.RoomViews.ToList();

            reservationRequestModel.ServiceTypes = serviceTypes;
            reservationRequestModel.RoomTypes = roomTypes;
            reservationRequestModel.RoomViews = roomViews;





            if (ModelState.IsValid)
            {
                try
                {
                    //Postavi zahtevu rezervacije trenutno vreme rezevacije
                    reservationRequestModel.ReservationRequest.ReservationDate = DateTime.Now;

                    //Ukupna cena
                    reservationRequestModel.ReservationRequest.Price = CaluclateTotalPrice(reservationRequestModel.ReservationRequest.ServiceTypeId,
                                                                    reservationRequestModel.ReservationRequest.RoomTypeId,
                                                                    reservationRequestModel.ReservationRequest.NumberOfGuests,
                                                                    reservationRequestModel.ReservationRequest.CheckInDate, 
                                                                    reservationRequestModel.ReservationRequest.CheckOutDate);
                    
                    //Dodaj u bazu zahtev rezervacije
                    dbContext.ReservationRequests.Add(reservationRequestModel.ReservationRequest);
                    dbContext.SaveChanges();
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                //Kada je poslao zahtev redirektuj ga do prikaza detalja rezervacije
                return RedirectToAction("ReservationDetails", "User", routeValues: new { id = reservationRequestModel.ReservationRequest.UserId});
            }

            return View(reservationRequestModel);
        }


        //Prikaz zahteva rezervacije sa odredjenim id
        [Authorize(Roles ="Recepcionar, Menadzer")]
        public ActionResult Show(int id)
        {

            
            ReservationRequest reservationRequestfromDb = null;
            try
            {
                reservationRequestfromDb = dbContext.ReservationRequests
                                            .Include(m => m.User)
                                            .Include(m => m.RoomType)
                                            .Include(m => m.ServiceType)
                                            .Include(m => m.RoomView)
                                            .SingleOrDefault(m=>m.Id == id);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return View(reservationRequestfromDb);
        }


        //Brisanje zahteva ukoliko ne zeli da odobri rezervaciju
        [Authorize(Roles = "Recepcionar, Menadzer")]
        [HttpPost]
        public ActionResult Delete(int idReservationRequest)
        {
            var reservationRequestFromDb = dbContext.ReservationRequests
                                           .Include(m => m.User)
                                           .SingleOrDefault(m => m.Id == idReservationRequest);

            if (reservationRequestFromDb != null)
            {
                //Poruka uspesno ste izbrisali zahtev rezervacije
                Message.Display(this, $"Uspešno ste izbrisali zahtev rezervacije sa id: {reservationRequestFromDb.Id} " +
                                     $"za korisnika: {reservationRequestFromDb.User.FirstName} " +
                                     $"{reservationRequestFromDb.User.LastName}");

                dbContext.ReservationRequests.Remove(reservationRequestFromDb);
                dbContext.SaveChanges();



            }
            else
            {
                //Poruka nije uspesno izbrisan
                Message.Display(this, $"Neuspešno brisanje zahteva rezervacije sa id {idReservationRequest}");

            }


            return RedirectToAction("Index", "ReservationRequest");
        }

        //Metoda za izracunavanje ukupne cene
        private double CaluclateTotalPrice(int serviceTypeId, int roomTypeId,int numOfGuests, DateTime checkInDate, DateTime checkOutDate)
        {
            double totalPrice = 0;
            var numOfDays = (checkOutDate - checkInDate).TotalDays;

            ServiceType serviceType = null;
            RoomType roomType = null;

            try
            {
                serviceType = dbContext.ServiceTypes.SingleOrDefault(m => m.Id == serviceTypeId && m.IsActive == true);
                roomType = dbContext.RoomTypes.SingleOrDefault(m => m.Id == roomTypeId && m.IsActive == true);


            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (serviceType != null && roomType != null)
            {
                double servicePrice = serviceType.Price * numOfDays * numOfGuests;
                double roomPrice = roomType.Price * numOfDays;
                totalPrice = servicePrice + roomPrice;

                //Debug.WriteLine($"Total price: {totalPrice}");
            }

            return totalPrice;
        }


        //Provera da li korisnik ima rezervaciju ako korisnik ima vec zahtev ili rezervaciju ne moze da posalji novi zahtev
        private bool IsHavingReservation(int userId)
        {
            ReservationRequest reservationRequestFromDb = null;
            Reservation reservationFromDb = null;

            try
            {
                reservationRequestFromDb = dbContext.ReservationRequests.SingleOrDefault(m => m.UserId == userId);
                //Ukoliko nema zahtev rezervacije
                if (reservationRequestFromDb == null)
                {
                    //Proveri rezervaciju
                    reservationFromDb = dbContext.Reservations.SingleOrDefault(m => m.UserId == userId && m.IsActive==true);

                    //Ukoliko nema rezervacije
                    if (reservationFromDb == null)
                    {
                        return false;
                    }

                    
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return true;
        }


        

    }
}