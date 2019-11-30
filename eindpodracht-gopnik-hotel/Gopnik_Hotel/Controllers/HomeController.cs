using Gopnik_Hotel.DomainModel.Repositories;
using Gopnik_Hotel.Models;
using System.Web.Mvc;
using Gopnik_Hotel.ViewModels;
using System.Collections.Generic;

namespace Gopnik_Hotel.Controllers
{
    public class HomeController : Controller
    {
        private IKamerRepository kamerRepository;

        public HomeController()
        {
            kamerRepository = new EntityKamerRepository(new GopnikHotelDBEntities());
        }

        public HomeController(IKamerRepository kamerRepository)
        {
            this.kamerRepository = kamerRepository;
        }

        public ActionResult Index()
        {
            List<KamerViewModel> feuturedKamersList = new List<KamerViewModel>();
            var feuturedKamers = kamerRepository.GetAll();
            while(feuturedKamers.Count > 4)
            {
                feuturedKamers.RemoveAt(feuturedKamers.Count - 1);
            }

            foreach (var kamer in feuturedKamers)
            {
                feuturedKamersList.Add(new KamerViewModel(kamer));
            }
            return View(feuturedKamersList);
        }

        [HttpPost]
        public ActionResult RedirectToBooking()
        {
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Over het GopnikHotel";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}