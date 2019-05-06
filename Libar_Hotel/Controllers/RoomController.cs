using Libar_Hotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Libar_Hotel.Helpers;
namespace Libar_Hotel.Controllers
{
    [Authorize(Roles ="Recepcionar, Menadzer")]
    public class RoomController : Controller
    {
        ApplicationDbContext dbContext;


        public RoomController()
        {
            dbContext = new ApplicationDbContext();
        }
        
        // GET: Room
        public ActionResult Index()
        {

            var roomsFromDb = dbContext.Rooms.Include("RoomType")
                            .Include("RoomStatus").Include("RoomView")
                            .Where(m=>m.IsActive==true)
                            .ToList();
            return View(roomsFromDb);
        }
        //GET: Create Room-Prikaz forme za dodavanje sobe
        public ActionResult Create()
        {
            try
            {
                var roomTypes = dbContext.RoomTypes.Where(m => m.IsActive == true).ToList();
                var roomStatuses = dbContext.RoomStatuses.ToList();
                var roomViews = dbContext.RoomViews.ToList();
                
                var roomViewModel = new RoomViewModel()
                {
                    RoomTypes = roomTypes,
                    RoomStatuses = roomStatuses,
                    RoomViews=roomViews
                    



                };

                

                return View(roomViewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View();
        }
        //POST: Create Room-Dodavanje sobe u bazu
        [HttpPost]
        public ActionResult Create(RoomViewModel roomViewModel)
        {
            try
            {
                

                var roomTypes = dbContext.RoomTypes.Where(m => m.IsActive == true).ToList();
                var roomStatuses = dbContext.RoomStatuses.ToList();
                var roomViews = dbContext.RoomViews.ToList();



                roomViewModel.RoomTypes = roomTypes;
                roomViewModel.RoomStatuses = roomStatuses;
                roomViewModel.RoomViews = roomViews;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            if (ModelState.IsValid)
            {
                try

                {
                
                        roomViewModel.Room.IsActive = true;
                       
                        dbContext.Rooms.Add(roomViewModel.Room);
                        dbContext.SaveChanges();

                        Message.Display(this, $"Uspešno ste dodali sobu sa brojem: {roomViewModel.Room.Id}");

                        return RedirectToAction("Index", "Room");
                    
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }


            }
            return View(roomViewModel);
        }
        //GET: 
        [HttpGet]
        public ViewResult Edit(int id)
        {

            var roomFromDb = dbContext.Rooms.Include("RoomType").Include("RoomStatus").Include("RoomView").SingleOrDefault(m => m.IsActive == true && m.Id==id);

            
            var roomTypes = dbContext.RoomTypes.Where(m => m.IsActive == true).ToList();
            var roomStatuses = dbContext.RoomStatuses.ToList();
            var roomViews = dbContext.RoomViews.ToList();


          

            var roomViewModel = new RoomViewModel()
            {
                RoomTypes = roomTypes,
                RoomStatuses = roomStatuses,
                RoomViews=roomViews,
                Room=roomFromDb



            };
            

            return View(roomViewModel);

            
            
           

        }
   
        //PUT: Edit
        [HttpPost]
        public ActionResult Edit(int id,RoomViewModel roomViewModel)
        {

            try
            {


                var roomTypes = dbContext.RoomTypes.Where(m => m.IsActive == true).ToList();
                var roomStatuses = dbContext.RoomStatuses.ToList();
                var roomViews = dbContext.RoomViews.ToList();


                
                roomViewModel.RoomTypes = roomTypes;
                roomViewModel.RoomStatuses = roomStatuses;
                roomViewModel.RoomViews = roomViews;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (ModelState.IsValid)
            {


                try
                {

                    var roomFromDb = dbContext.Rooms.SingleOrDefault(m => m.Id == id && m.IsActive == true);

                   




                    roomFromDb.Floor = roomViewModel.Room.Floor;
                    roomFromDb.RoomViewId = roomViewModel.Room.RoomViewId;
                    roomFromDb.RoomTypeId = roomViewModel.Room.RoomTypeId;
                    roomFromDb.RoomStatusId = roomViewModel.Room.RoomStatusId;

                    dbContext.SaveChanges();
                    Message.Display(this, $"Uspešno ste izmenili sobu sa brojem: {roomFromDb.Id}");
                    return RedirectToAction("Index", "Room");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return View(roomViewModel);
        }

        public ActionResult Details(int id)
        {
            try
            {
                var roomFromDb = dbContext.Rooms.Include("RoomType").Include("RoomStatus").Include("RoomView").SingleOrDefault(m => m.IsActive == true && m.Id == id);



                return View(roomFromDb);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View();

        }

        public ActionResult DeleteDetails(int id)
        {
            try
            {
                var roomFromDb = dbContext.Rooms.Include("RoomType").Include("RoomStatus").Include("RoomView").SingleOrDefault(m => m.IsActive == true && m.Id==id);



                return View(roomFromDb);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View();

        }
        [HttpPost, ActionName("DeleteDetails")]
        public ActionResult Delete(int id)
        {


            try
            {
                var roomFromDb = dbContext.Rooms.SingleOrDefault(m => m.Id == id && m.IsActive == true);
                
                if (roomFromDb != null)
                {
                    roomFromDb.IsActive = false;
                    dbContext.SaveChanges();
                    Message.Display(this, $"Uspešno ste izbrisali sobu sa brojem: {roomFromDb.Id}");
                    return RedirectToAction("Index", "Room");

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View("Index", "Room");
        }

    }
}