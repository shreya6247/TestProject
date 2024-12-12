using Q1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Q1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult Details()
        {
            ViewData["msg"] = TempData["msg"];
            ViewData["msg2"] = TempData["msg2"];
            PassengerBusinessLogic ps = new PassengerBusinessLogic();
            List<Passenger> list = new List<Passenger>();
            list = ps.GetDetails();
            return View(list);
        }

        public ActionResult Insert()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Insert(Passenger pass)
        {
            PassengerBusinessLogic ps = new PassengerBusinessLogic();
            TempData["msg"]=ps.InsertPassengers(pass);
            return RedirectToAction("Details");
        }

        public ActionResult Delete(int id)
        {
            PassengerBusinessLogic ps = new PassengerBusinessLogic();
            TempData["msg2"] = ps.DeletePassenger(id);
            return RedirectToAction("Details");
        }

        public ActionResult Edit(int id)
        {
            PassengerBusinessLogic ps = new PassengerBusinessLogic();
            Passenger pass=new Passenger();
            pass = ps.StoreDetail(id);
            return View(pass);
        }
        [HttpPost]
        public ActionResult Edit(Passenger pass,int id)
        {
            PassengerBusinessLogic ps = new PassengerBusinessLogic();
            TempData["msg"]=ps.UpdatePassenger(id, pass);
            return RedirectToAction("Details");
        }
    }
}