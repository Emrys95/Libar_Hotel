using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Libar_Hotel.Models;
using Libar_Hotel.Helpers;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Libar_Hotel.Controllers
{
    [Authorize(Roles = "Recepcionar, Menadzer")]
    public class ReservationController : Controller
    {

        private ApplicationDbContext dbContext;

        public ReservationController()
        {
            dbContext = new ApplicationDbContext();
        }

        // GET: Reservation Prikaz Rezervacija
        
        public ActionResult Index()
        {
            try
            {
                List<Reservation> reservationsFromDb = dbContext.Reservations
               .Include(m => m.User)
               .Include(m => m.Room)
               .Include(m => m.ServiceType)
               .Where(m => m.IsActive == true)
               .ToList();
                return View(reservationsFromDb);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View();
           
        }
        //GET: Reservation/Create Prikaz forme za rezervaciju i dodeljivanje soba
        //Prosledujemo Id create metodi kako bi smo iscitali zahtev rezervacije kako bi dobili odgovarajuce sobe u kojima korisnik moze da odseda
        public ActionResult Create(int idReservationRequest)
        {

            ReservationRequest reservationRequestFromDb = null;
            try
            {
                reservationRequestFromDb = dbContext.ReservationRequests.SingleOrDefault(m => m.Id == idReservationRequest);
                
                ReservationViewModel reservationModel = new ReservationViewModel();

                reservationModel.Reservation = new Reservation();

                //Zbog brisanja Zahteva moramo da prosledimo id formi kada se izvrsi post methoda za create
                reservationModel.ReservationRequestId = idReservationRequest;

                reservationModel.Reservation.UserId = reservationRequestFromDb.UserId;

                reservationModel.Reservation.ServiceTypeId = reservationRequestFromDb.ServiceTypeId;
               

                reservationModel.Reservation.ReservationDate = reservationRequestFromDb.ReservationDate;
                reservationModel.Reservation.CheckInDate = reservationRequestFromDb.CheckInDate;
                reservationModel.Reservation.CheckOutDate = reservationRequestFromDb.CheckOutDate;

                reservationModel.Reservation.NumberOfGuests = reservationRequestFromDb.NumberOfGuests;
                reservationModel.Reservation.Price = reservationRequestFromDb.Price;

                

                
                //Popunjavanje za listu sobu koju recepionar bira. Ponudjene sobe su sa zahteva rezervacije o sobama i koje su slobodne
                var roomsFromDb = dbContext.Rooms.Where(m => m.RoomTypeId == reservationRequestFromDb.RoomTypeId
                                                       && m.RoomViewId == reservationRequestFromDb.RoomViewId
                                                       //1=Slobodan
                                                       && (m.RoomStatusId == 1));


                

                reservationModel.Rooms = roomsFromDb.ToList();

              

                return View(reservationModel);


            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View("Error");
        }


        [HttpPost]
        public ActionResult Create(int idReservationRequest, Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    reservation.IsActive = true;
                    dbContext.Reservations.Add(reservation);
                    dbContext.SaveChanges();
                    //Postavljanje statusa sobe na rezervisano nakon odobrene rezervacije
                    ChangeRoomStatus(reservation.RoomId.Value);

                    //Brisanje Zahteva rezervacije nakon uspesno sacuvane rezervacije
                    DeleteRequestedReservation(idReservationRequest);

                    return RedirectToAction("Index", "ReservationRequest");
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }




            return RedirectToAction("Index", "Reservation");
        }

        public ActionResult Edit(int id)
        {
            Reservation reservationFromDb = null;
            

            try
            {
                reservationFromDb = dbContext.Reservations
                    .Include(m => m.User)
                    .Include(m => m.Room.RoomType)
                    .Include(m => m.ServiceType)
                    .SingleOrDefault(m => m.Id == id && m.IsActive == true);
                                    
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View(reservationFromDb);
        }

        [HttpPost]
        public ActionResult Edit(int id, Reservation reservation)
        {
            Reservation reservationFromDb = null;

            try
            {
                    reservationFromDb = dbContext.Reservations
                                       .Include(m => m.Room.RoomType)
                                       .Include(m=> m.User)
                                       .Include(m => m.ServiceType)
                                        
                                       .SingleOrDefault(m => m.Id == id);

                    //reservation = reservationFromDb;

                    if (ModelState.IsValid)
                    {
                        reservationFromDb.CheckInDate = reservation.CheckInDate;
                        reservationFromDb.CheckOutDate = reservation.CheckOutDate;

                        reservationFromDb.Price = CaluclateTotalPrice(reservationFromDb.ServiceTypeId, reservationFromDb.Room.RoomTypeId,
                                                                        reservationFromDb.NumberOfGuests, reservation.CheckInDate,
                                                                        reservation.CheckOutDate);



                        dbContext.SaveChanges();

                        Message.Display(this, $"Uspešno ste izmenili rezervacije sa id: {reservationFromDb.Id} " +
                                           $"za korisnika: {reservationFromDb.User.FirstName} " +
                                           $"{reservationFromDb.User.LastName}");

                        return RedirectToAction("Index", "Reservation");
                    }
                

            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View(reservation);
        }

        public ActionResult Details(int id)
        {
            Reservation reservationFromDb = null;

            try
            {

                reservationFromDb = dbContext.Reservations
                                   .Include(m => m.User).Include(m => m.ServiceType)
                                   .SingleOrDefault(m => m.Id == id && m.IsActive == true);

                return View(reservationFromDb);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View();
        }
        public ActionResult DeleteDetails(int id)
        {
            Reservation reservationFromDb = null;

            try
            {

                reservationFromDb = dbContext.Reservations
                                   .Include(m => m.User).Include(m=>m.ServiceType)
                                   .SingleOrDefault(m => m.Id == id && m.IsActive == true);

                return View(reservationFromDb);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View();
        }

        //preimenovana u DeleteDetails da bi mogao da se izvrsi submit
        [HttpPost, ActionName("DeleteDetails")]
        public ActionResult Delete(int id)
        {
            Reservation reservationFromDb = null;

            try
            {
                //dodato Include(ServiceType)
                reservationFromDb = dbContext.Reservations
                                   .Include(m => m.User).Include(m=>m.ServiceType)
                               
                                   .SingleOrDefault(m => m.Id == id && m.IsActive == true);


                if (reservationFromDb != null)
                {
                    reservationFromDb.IsActive = false;
                    //Promeni status sobe na slobodno nakon uklonjene rezervacije
                    ChangeRoomStatus(reservationFromDb.RoomId.Value);
                    dbContext.SaveChanges();

                    Message.Display(this, "Rezervacija je uspesno obrisana za korisnika ", $"{reservationFromDb.User.FirstName} {reservationFromDb.User.LastName}");
                }

                else
                {
                    Message.Display(this, "Rezervacija nije pronadjena za korisnika", $"{reservationFromDb.User.FirstName} ${reservationFromDb.User.LastName}");
                }

                
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction("Index", "Reservation");
        }


        //Brisanje Zahteva nakon rezervacije
        private void DeleteRequestedReservation(int id)
        {
            try
            {
                ReservationRequest reservationRequest = dbContext.ReservationRequests.Find(id);

                if (reservationRequest != null)
                {
                    dbContext.ReservationRequests.Remove(reservationRequest);
                    dbContext.SaveChanges();
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private double CaluclateTotalPrice(int serviceTypeId, int roomTypeId, int numOfGuests, DateTime checkInDate, DateTime checkOutDate)
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

                Debug.WriteLine($"Total price: {totalPrice}");
            }

            return totalPrice;
        }


        private void ChangeRoomStatus(int roomId)
        {
            Room roomFromDb = null;
            try
            {
                roomFromDb = dbContext.Rooms.SingleOrDefault(m => m.Id == roomId && m.IsActive == true);

                //Provera statusa sobe
                switch (roomFromDb.RoomStatusId)
                {
                    //Ako je roomStatusId=1 to znaci da je status sobe slobodna i postavi je na rezervisano roomStatusId=3
                    case 1:
                        roomFromDb.RoomStatusId = 3;
                        break;
                    //Ako je status sobe id = 3 to znaci da je status sobe rezervisana i postavi je na slobodno
                    case 3:
                        roomFromDb.RoomStatusId = 1;
                        break;
                }

                //Sacuvaj promene
                dbContext.SaveChanges();
                

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}