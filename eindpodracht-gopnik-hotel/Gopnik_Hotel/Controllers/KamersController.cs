using System.Collections.Generic;
using System.Web.Mvc;
using Gopnik_Hotel.DomainModel.Repositories;
using Gopnik_Hotel.Models;
using Gopnik_Hotel.ViewModels;

namespace Gopnik_Hotel.Controllers
{
    public class KamersController : Controller
    {
        private IKamerRepository _kamerRepository;

        public KamersController()
        {
            _kamerRepository = new EntityKamerRepository(new GopnikHotelDBEntities());
        }

        public KamersController(IKamerRepository kamerRepository)
        {
            this._kamerRepository = kamerRepository;
        }

        public ActionResult Index()
        {
            List<KamerViewModel> kamersList = new List<KamerViewModel>();
            foreach (var kamer in _kamerRepository.GetAll())
            {
                kamersList.Add(new KamerViewModel(kamer));
            }
            return View(kamersList);
        }

        public ActionResult Details(int id)
        {
            KamerViewModel kamer = new KamerViewModel(_kamerRepository.GetKamerById(id));
            if (kamer == null)
            {
                return HttpNotFound();
            }
            return View(kamer);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KamerId,Naam,Grootte,Prijs,Afbeelding")] Kamer kamer)
        {
            if (ModelState.IsValid)
            {
                _kamerRepository.Create(kamer);
                _kamerRepository.Save();
                return RedirectToAction("Index");
            }

            return View(kamer);
        }

        public ActionResult Edit(int id)
        {
            KamerViewModel kamer = new KamerViewModel(_kamerRepository.GetKamerById(id));
            if (kamer == null)
            {
                return HttpNotFound();
            }
            return View(kamer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KamerId,Naam,Grootte,Prijs,Afbeelding")] Kamer kamer)
        {
            if (ModelState.IsValid)
            {
                _kamerRepository.Update(kamer);
                _kamerRepository.Save();
                return RedirectToAction("Index");
            }
            return View(kamer);
        }

        public ActionResult Delete(int id)
        {
            KamerViewModel kamer = new KamerViewModel(_kamerRepository.GetKamerById(id));
            if (kamer == null)
            {
                return HttpNotFound();
            }
            return View(kamer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KamerViewModel kamer = new KamerViewModel(_kamerRepository.GetKamerById(id));
            _kamerRepository.Delete(kamer.ToModel());
            _kamerRepository.Save();
            return RedirectToAction("Index");
        }

    }
}
