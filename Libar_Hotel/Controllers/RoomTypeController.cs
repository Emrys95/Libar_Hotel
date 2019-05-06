using Libar_Hotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Libar_Hotel.Helpers;


namespace Libar_Hotel.Controllers
{
    [Authorize(Roles="Menadzer")]
    public class RoomTypeController : Controller
    {
        private ApplicationDbContext dbContext;


        public RoomTypeController()
        {
            dbContext = new ApplicationDbContext();

        }



        // GET: RoomType
        public ActionResult Index()
        {
            try
            {
                var roomTypesFromDb = dbContext.RoomTypes.Where(m => m.IsActive == true).ToList();
                return View(roomTypesFromDb);

            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View();
        }

        //GET: Create Room Prikaz forme za dodavanje sobe
        public ActionResult Create()
        {


            return View();
        }

        [HttpPost]
        //POST: Create Room Dodavanje tipa sobe u bazu
        public ActionResult Create(RoomType roomType)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var roomTypeByName = dbContext.RoomTypes.Where(m => m.Name == roomType.Name && m.IsActive == true);

                    if (roomTypeByName.Count() == 0)
                    {
                        roomType.IsActive = true;
                        dbContext.RoomTypes.Add(roomType);
                        dbContext.SaveChanges();

                        Message.Display(this, $"Uspešno ste dodali novi tip sobe: {roomType.Name}");

                        return RedirectToAction("Index", "RoomType");
                    }

                }


                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }



            }


            return View(roomType);
        }

        // GET : Edit RoomType - prikaz forme za menjanje tipa sobe
        public ActionResult Edit(int id)
        {
            RoomType roomTypeFromDb = null;

            try
            {
                roomTypeFromDb = dbContext.RoomTypes.SingleOrDefault(m => m.Id == id && m.IsActive == true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View(roomTypeFromDb);
        }

        // post : Edit RoomType - forma za menjanje tipa sobe
        [HttpPost]
        public ActionResult Edit(int id, RoomType roomType)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var roomTypeFromDb = dbContext.RoomTypes.SingleOrDefault(m => m.Id == id && m.IsActive == true);

                    if (roomTypeFromDb != null)
                    {
                        
                        roomTypeFromDb.Price = roomType.Price;

                        dbContext.SaveChanges();

                        Message.Display(this, $"Uspešno ste izmenili cenu tipa sobe: {roomType.Name}");

                        return RedirectToAction("Index", "RoomType");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }


            }
            return View(roomType);
        }
        public ActionResult Details(int id)
        {
            try
            {
                var roomTypeFromDb = dbContext.RoomTypes.SingleOrDefault(m => m.Id == id && m.IsActive == true);

                if (roomTypeFromDb != null)
                {

                    return View(roomTypeFromDb);
                }


            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View();

        }



        //get, prikaz detalja usluge za brisanje

        public ActionResult DeleteDetails(int id)
        {
            try
            {
                var roomTypeFromDb = dbContext.RoomTypes.SingleOrDefault(m => m.Id == id && m.IsActive == true);

                if (roomTypeFromDb != null)
                {

                    return View(roomTypeFromDb);
                }


            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View();
                       

        }

        // Post : Delete RoomType - forma za brisanje tipa usluge
        [HttpPost, ActionName("DeleteDetails")]
        public ActionResult Delete(int id)
        {
            try
            {
                var roomTypeFromDb = dbContext.RoomTypes.SingleOrDefault(m => m.Id == id && m.IsActive == true);

                if (roomTypeFromDb != null)
                {
                    roomTypeFromDb.IsActive = false;

                    dbContext.SaveChanges();

                    Message.Display(this, $"Uspešno ste izbrisali tip sobe: {roomTypeFromDb.Name}");

                    return RedirectToAction("Index", "RoomType");
                }
                
                
                   // ukoliko nije izbrisao
                    //Message.Display(this, "Neuspešno brisanje", roomTypeFromDb.Name);

                    //return RedirectToAction("Index", "RoomType");
          
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View();


        }





    }
}