using Libar_Hotel.Models; // dodato
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Libar_Hotel.Helpers;

namespace Libar_Hotel.Controllers
{
    [Authorize(Roles="Menadzer")]
    public class ServiceTypeController : Controller
    {
        ApplicationDbContext dbContext;

        public ServiceTypeController()
        {
            dbContext = new ApplicationDbContext();
        }

        // GET: ServiceType
        public ActionResult Index()
        {
            try
            {
                var serviceTypesFromDb = dbContext.ServiceTypes.Where(m => m.IsActive == true).ToList();

                return View(serviceTypesFromDb);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            return View();
        }

        // GET : Create ServiceType - prikaz forme za dodavanje tipa usluge
        public ActionResult Create()
        {
            return View();
        }

        // POST : Create ServiceType - forma za dodavanje tipa usluge u bazu

        [HttpPost]
        public ActionResult Create(ServiceType serviceType)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var serviceTypeByName = dbContext.ServiceTypes.Where(m => m.Name == serviceType.Name && m.IsActive == true);

                    if (serviceTypeByName.Count() == 0)
                    {
                        serviceType.IsActive = true;

                        dbContext.ServiceTypes.Add(serviceType);
                        dbContext.SaveChanges();

                        Message.Display(this, $"Uspešno ste dodali novi tip usluge: {serviceType.Name}");

                        return RedirectToAction("Index", "ServiceType");

                    }
                }
                catch (Exception ex)
                {
                    Message.Display(this, "Niste uspešno dodali tip usluge");
                    Console.WriteLine(ex.Message);
                }
            }

            return View(serviceType);
        }

        // GET : Edit ServiceType - prikaz forme za menjanje tipa usluge
        public ActionResult Edit(int id)
        {
            ServiceType serviceTypeFromDb = null;
            try
            {
                serviceTypeFromDb = dbContext.ServiceTypes.SingleOrDefault(m => m.Id == id && m.IsActive == true);

                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View(serviceTypeFromDb);
        }

        // post : Edit ServiceType - forma za menjanje cene tipa usluge

        [HttpPost]
        public ActionResult Edit(int id, ServiceType serviceType)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var serviceTypeFromDb = dbContext.ServiceTypes.SingleOrDefault(m => m.Id == id && m.IsActive == true);

                    if (serviceTypeFromDb != null)
                    {
                       
                        serviceTypeFromDb.Price = serviceType.Price;

                        dbContext.SaveChanges();

                        Message.Display(this, $"Uspešno ste izmenili cenu za tip usluge: {serviceTypeFromDb.Name}");
                        return RedirectToAction("Index", "ServiceType");


                    }

                }
                catch (Exception ex)
                {
                  
             
                   Console.WriteLine(ex.Message);
                }


            }

            return View(serviceType);
        }

        public ActionResult Details(int id)
        {
            try
            {
                var serviceTypeFromDb = dbContext.ServiceTypes.SingleOrDefault(m => m.Id == id && m.IsActive == true);

                if (serviceTypeFromDb != null)
                {

                    return View(serviceTypeFromDb);
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
                var serviceTypeFromDb = dbContext.ServiceTypes.SingleOrDefault(m => m.Id == id && m.IsActive == true);

                if (serviceTypeFromDb != null)
                {

                    return View(serviceTypeFromDb);
                }

                
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View();



        }

        // Delete : Delete ServiceType - forma za brisanje tipa usluge
        [HttpPost, ActionName("DeleteDetails")]
        public ActionResult Delete(int id)
        {
            try
            {
                var serviceTypeFromDb = dbContext.ServiceTypes.SingleOrDefault(m => m.Id == id && m.IsActive == true);

                if (serviceTypeFromDb != null)
                {
                    serviceTypeFromDb.IsActive = false;

                    dbContext.SaveChanges();
                    Message.Display(this, $"Uspešno ste izbrisali tip : {serviceTypeFromDb.Name}");

                    return RedirectToAction("Index", "ServiceType");
                }

           

            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View();


        }

    }    
}