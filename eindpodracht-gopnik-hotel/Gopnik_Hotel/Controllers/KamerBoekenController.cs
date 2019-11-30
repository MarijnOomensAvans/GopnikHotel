using Gopnik_Hotel.DomainModel.Repositories;
using Gopnik_Hotel.Models;
using Gopnik_Hotel.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gopnik_Hotel.Controllers
{
    public class KamerBoekenController : Controller
    {

        private IKamerRepository _kamerRepository;
        private IKlantRepository _klantRepository;
        private IBoekingRepository _boekingRepository;

        public KamerBoekenController()
        {
            _kamerRepository = new EntityKamerRepository(new GopnikHotelDBEntities());
            _klantRepository = new EntityKlantRepository(new GopnikHotelDBEntities());
            _boekingRepository = new EntityBoekingRepository(new GopnikHotelDBEntities());
        }

        public KamerBoekenController(IKamerRepository kamerRepository, IKlantRepository klantRepository, IBoekingRepository boekingRepository)
        {
            this._kamerRepository = kamerRepository;
            this._klantRepository = klantRepository;
            this._boekingRepository = boekingRepository;
        }

        private int _kamerId;

        public ActionResult Index(int kamerID)
        {
            _kamerId = kamerID;
            TempData["Kamer"] = _kamerId;
            TempData["Kamer2"] = _kamerId;
            TempData["Kamer3"] = _kamerId;
            ViewBag.Today = DateTime.Now.Year + "-" + DateTime.Now.Month.ToString("d2") + "-" + DateTime.Now.Day;
            KamerViewModel gekozenKamer = new KamerViewModel(_kamerRepository.GetKamerById(_kamerId));
            return View(gekozenKamer);
        }

        [HttpPost]
        public ActionResult Next(DateTime datepicker, int Amount)
        {
            TempData["Date"] = datepicker;
            TempData["Amount"] = Amount;
            return RedirectToAction("Stap2");
        }
        
        public ActionResult Stap2()
        {
            ViewBag.Date = TempData["Date"];
            ViewBag.Amount = TempData["Amount"];
            KamerViewModel kamer = new KamerViewModel(_kamerRepository.GetKamerById((int)TempData["Kamer"]));
            return View("Stap2",kamer);
        }

        [HttpPost]
        public ActionResult Next2(string date, string amount, string naam1, string naam2, string naam3, string naam4, string naam5, string woonplaats1, string woonplaats2, string woonplaats3, string woonplaats4, string woonplaats5, string postcode1, string postcode2, string postcode3, string postcode4, string postcode5, string straatnaam1, string straatnaam2, string straatnaam3, string straatnaam4, string straatnaam5, string huisnummer1, string huisnummer2, string huisnummer3, string huisnummer4, string huisnummer5, string email1, string email2, string email3, string email4, string email5)
        {
            Klant kl1 = new Klant();
            Klant kl2 = new Klant();
            Klant kl3 = new Klant();
            Klant kl4 = new Klant();
            Klant kl5 = new Klant();
            kl1.Naam = naam1;
            kl1.Woonplaats = woonplaats1;
            kl1.Postcode = postcode1;
            kl1.Straat = straatnaam1;
            kl1.Huisnummer = Convert.ToInt32(huisnummer1);
            kl1.Mail = email1;
            if(naam2 != null)
            {
                kl2.Naam = naam2;
                kl2.Woonplaats = woonplaats2;
                kl2.Postcode = postcode2;
                kl2.Straat = straatnaam2;
                kl2.Huisnummer = Convert.ToInt32(huisnummer2);
                kl2.Mail = email2;
                if(naam3 != null)
                {
                    kl3.Naam = naam3;
                    kl3.Woonplaats = woonplaats3;
                    kl3.Postcode = postcode3;
                    kl3.Straat = straatnaam3;
                    kl3.Huisnummer = Convert.ToInt32(huisnummer3);
                    kl3.Mail = email3;
                    if(naam4 != null)
                    {
                        kl4.Naam = naam4;
                        kl4.Woonplaats = woonplaats4;
                        kl4.Postcode = postcode4;
                        kl4.Straat = straatnaam4;
                        kl4.Huisnummer = Convert.ToInt32(huisnummer4);
                        kl4.Mail = email4;
                        if(naam5 != null)
                        {
                            kl5.Naam = naam5;
                            kl5.Woonplaats = woonplaats5;
                            kl5.Postcode = postcode5;
                            kl5.Straat = straatnaam5;
                            kl5.Huisnummer = Convert.ToInt32(huisnummer5);
                            kl5.Mail = email5;
                        }
                    }
                }
            }
            List<Klant> klanten = new List<Klant>();
            klanten.Add(kl1);
            if(kl2.Naam != null)
            {
                klanten.Add(kl2);
                if (kl3.Naam != null)
                {
                    klanten.Add(kl3);
                    if (kl4.Naam != null)
                    {
                        klanten.Add(kl4);
                        if (kl5.Naam != null)
                        {
                            klanten.Add(kl5);
                        }
                    }
                }
            }
            DateTime dtm = Convert.ToDateTime(date);
            Klant[] kla = klanten.ToArray();
            Korting k = new Korting(dtm,_kamerRepository.GetAll().Count() ,kla);
            TempData["Korting"] = k.getKorting();
            TempData["Klant"] = klanten;
            TempData["EndKlant"] = klanten;
            TempData["Date"] = date;
            TempData["EndDate"] = date;
            TempData["Amount"] = amount;

            return RedirectToAction("Stap3");
        }

        public ActionResult Stap3()
        {
            string Data = (string) TempData["Values"];
            if (Data == null)
            {
                Data = "ERROR - - - 0";
            }
             string[] split = Data.Split(null);

            ViewBag.Date = TempData["Date"];
            ViewBag.Amount = TempData["Amount"];
            ViewBag.Klant = TempData["Klant"];
            ViewBag.Korting = TempData["Korting"];
            KamerViewModel kamer = new KamerViewModel(_kamerRepository.GetKamerById((int)TempData["Kamer2"]));
            ViewBag.TotaalPrijs = (double)kamer.Prijs * (1 - ((double)ViewBag.Korting / 100));
            return View(kamer);
        }

        [HttpPost]
        public ActionResult Bevestigen()
        {
            List<Klant> klanten = (List<Klant>) TempData["EndKlant"];
            DateTime date = Convert.ToDateTime(TempData["EndDate"]);

            foreach (var klant in klanten)
            {
                _klantRepository.Create(klant);
            }
            _klantRepository.Save();
            foreach (var klant in klanten)
            {
                Boeking boek = new Boeking();
                if (date != null)
                {
                    boek.Datum = date;
                }
                boek.IdKamer = Convert.ToInt32(TempData["Kamer3"]);
                boek.IdKlant = klant.KlantId;
                _boekingRepository.Create(boek);
            }
            _boekingRepository.Save();
            return RedirectToAction("Index", "Home");
        }
    }
}