using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Gopnik_Hotel.DomainModel.Repositories;
using Gopnik_Hotel.Models;
using Gopnik_Hotel.ViewModels;

namespace Gopnik_Hotel.Controllers
{
    public class BoekingsController : Controller
    {
        IBoekingRepository boekingRepository;
        IKamerRepository kamerRepository;
        IKlantRepository klantRepository;

        public BoekingsController()
        {
            GopnikHotelDBEntities db = new GopnikHotelDBEntities();
            boekingRepository = new EntityBoekingRepository(db);
            kamerRepository = new EntityKamerRepository(db);
            klantRepository = new EntityKlantRepository(db);
        }

        public BoekingsController(IBoekingRepository boekingRepository,IKamerRepository kamerRepository,IKlantRepository klantRepository)
        {
            this.boekingRepository = boekingRepository;
            this.kamerRepository = kamerRepository;
            this.klantRepository = klantRepository;
        }

        public ActionResult Index()
        {
            List<BoekingViewModel> boekings = new List<BoekingViewModel>();
            foreach (var boeking in boekingRepository.GetAll())
            {
                boekings.Add(new BoekingViewModel(boeking));
            }
            return View(boekings.ToList());
        }

        public ActionResult Details(int id)
        {
            BoekingViewModel boeking = new BoekingViewModel(boekingRepository.GetBoekingById(id));
            if (boeking == null)
            {
                return HttpNotFound();
            }
            return View(boeking);
        }

        public ActionResult Create()
        {
            List<KamerViewModel> kamerList = new List<KamerViewModel>();
            List<KlantViewModel> klantList = new List<KlantViewModel>();
            foreach (var kamer in kamerRepository.GetAll())
            {
                kamerList.Add(new KamerViewModel(kamer));
            }
            foreach (var klant in klantRepository.GetAll())
            {
                klantList.Add(new KlantViewModel(klant));
            }
            ViewBag.IdKamer = new SelectList(kamerList, "KamerId", "Naam");
            ViewBag.IdKlant = new SelectList(klantList, "KlantId", "Naam");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Datum,IdKamer,IdKlant")] Boeking boeking)
        {
            if (ModelState.IsValid)
            {
                boekingRepository.Create(boeking);
                boekingRepository.Save();
                return RedirectToAction("Index");
            }
            List<KamerViewModel> kamerList = new List<KamerViewModel>();
            List<KlantViewModel> klantList = new List<KlantViewModel>();
            foreach (var kamer in kamerRepository.GetAll())
            {
                kamerList.Add(new KamerViewModel(kamer));
            }
            foreach (var klant in klantRepository.GetAll())
            {
                klantList.Add(new KlantViewModel(klant));
            }
            ViewBag.IdKamer = new SelectList(kamerList, "KamerId", "Naam", boeking.IdKamer);
            ViewBag.IdKlant = new SelectList(klantList, "KlantId", "Naam", boeking.IdKlant);
            return View(boeking);
        }

        public ActionResult Edit(int id)
        {
            BoekingViewModel boeking = new BoekingViewModel(boekingRepository.GetBoekingById(id));
            if (boeking == null)
            {
                return HttpNotFound();
            }
            List<KamerViewModel> kamerList = new List<KamerViewModel>();
            List<KlantViewModel> klantList = new List<KlantViewModel>();
            foreach (var kamer in kamerRepository.GetAll())
            {
                kamerList.Add(new KamerViewModel(kamer));
            }
            foreach (var klant in klantRepository.GetAll())
            {
                klantList.Add(new KlantViewModel(klant));
            }
            ViewBag.IdKamer = new SelectList(kamerList, "KamerId", "Naam", boeking.IdKamer);
            ViewBag.IdKlant = new SelectList(klantList, "KlantId", "Naam", boeking.IdKlant);
            return View(boeking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BoekingId,Datum,IdKamer,IdKlant")] Boeking boeking)
        {
            if (ModelState.IsValid)
            {
                boekingRepository.Update(boeking);
                boekingRepository.Save();
                return RedirectToAction("Index");
            }
            List<KamerViewModel> kamerList = new List<KamerViewModel>();
            List<KlantViewModel> klantList = new List<KlantViewModel>();
            foreach (var kamer in kamerRepository.GetAll())
            {
                kamerList.Add(new KamerViewModel(kamer));
            }
            foreach (var klant in klantRepository.GetAll())
            {
                klantList.Add(new KlantViewModel(klant));
            }
            ViewBag.IdKamer = new SelectList(kamerList, "KamerId", "Naam", boeking.IdKamer);
            ViewBag.IdKlant = new SelectList(klantList, "KlantId", "Naam", boeking.IdKlant);
            return View(boeking);
        }

        public ActionResult Delete(int id)
        {
            BoekingViewModel boeking = new BoekingViewModel(boekingRepository.GetBoekingById(id));
            if (boeking == null)
            {
                return HttpNotFound();
            }
            return View(boeking);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BoekingViewModel boeking = new BoekingViewModel(boekingRepository.GetBoekingById(id));
            boekingRepository.Delete(boeking.ToModel());
            boekingRepository.Save();
            return RedirectToAction("Index");
        }
    }
}
