using Gopnik_Hotel.DomainModel.Repositories;
using Gopnik_Hotel.Models;
using Gopnik_Hotel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gopnik_Hotel.Controllers
{
    public class KamerOverzichtController : Controller
    {
        private IKamerRepository kamerRepository;

        public KamerOverzichtController()
        {
            kamerRepository = new EntityKamerRepository(new GopnikHotelDBEntities());
        }

        public ActionResult Index()
        {
            return View(kamerRepository.GetAll());
        }
    }
}