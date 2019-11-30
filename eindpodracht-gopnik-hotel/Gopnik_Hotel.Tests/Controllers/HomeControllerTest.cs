using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gopnik_Hotel;
using Gopnik_Hotel.Controllers;
using Gopnik_Hotel.DomainModel.Repositories;
using Moq;
using Gopnik_Hotel.Models;
using Gopnik_Hotel.ViewModels;

namespace Gopnik_Hotel.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {

        Mock<IKamerRepository> repo;
        List<Kamer> kamers;

        public HomeControllerTest()
        {
            repo = new Mock<IKamerRepository>();
            kamers = new List<Kamer>
                {
                    new Kamer{ Naam = "Kamer 1", Afbeelding = "https://i.imgur.com/UIcZyA4.png", Grootte = 1, KamerId = 1, Prijs = 1000 },
                    new Kamer{ Naam = "Kamer 2", Afbeelding = "https://i.imgur.com/PIjjABM.jpgg", Grootte = 3, KamerId = 2, Prijs = 4000 }
                };

            repo.Setup(r => r.GetAll()).Returns(kamers);

            repo.Setup(mr => mr.GetKamerById(
                It.IsAny<int>())).Returns((int i) => kamers.Where(
                x => x.KamerId == i).Single());

            repo.Setup(mr => mr.Create(
                It.IsAny<Kamer>())).Callback((Kamer target) => kamers.Add(target));

            repo.Setup(mr => mr.Delete(
                It.IsAny<Kamer>())).Callback((Kamer target) => kamers.Remove(target));
        }

        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AllRoomsVisible()
        {
            // Arrange
            KamersController controller = new KamersController(repo.Object);

            // Act
            List<Kamer> kamers = repo.Object.GetAll();
            List<KamerViewModel> vms = new List<KamerViewModel>();
            foreach (var k in kamers)
            {
                vms.Add(new KamerViewModel(k));
            }

            // Assert
            Assert.IsNotNull(vms);
            Assert.AreEqual(2, vms.Count());
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Over het GopnikHotel", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
