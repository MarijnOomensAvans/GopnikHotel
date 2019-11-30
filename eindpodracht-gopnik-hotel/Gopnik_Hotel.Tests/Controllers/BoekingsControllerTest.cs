using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gopnik_Hotel;
using Gopnik_Hotel.Controllers;
using Gopnik_Hotel.ViewModels;
using Moq;
using Gopnik_Hotel.DomainModel.Repositories;
using Gopnik_Hotel.Models;

namespace Gopnik_Hotel.Tests.Controllers
{
    [TestClass]
    public class BoekingsControllerTest
    {
        Mock<IBoekingRepository> repo;
        List<Boeking> boekingen;

        Mock<IKamerRepository> repoKamers;
        List<Kamer> kamers;

        Mock<IKlantRepository> repoKlanten;
        List<Klant> klanten;

        public BoekingsControllerTest()
        {
            repo = new Mock<IBoekingRepository>();
            boekingen = new List<Boeking>
                {
                    new Boeking{ Datum = DateTime.Now, IdKamer = 1, IdKlant = 2, BoekingId = 1 },
                    new Boeking{ Datum = DateTime.Now, IdKamer = 2, IdKlant = 1, BoekingId = 2 }
                };

            repo.Setup(r => r.GetAll()).Returns(boekingen);

            repo.Setup(mr => mr.GetBoekingById(
                It.IsAny<int>())).Returns((int i) => boekingen.Where(
                x => x.BoekingId == i).Single());

            repo.Setup(mr => mr.Create(
                It.IsAny<Boeking>())).Callback((Boeking target) => boekingen.Add(target));

            repo.Setup(mr => mr.Delete(
                It.IsAny<Boeking>())).Callback((Boeking target) => boekingen.Remove(target));

            repoKamers = new Mock<IKamerRepository>();
            kamers = new List<Kamer>
                {
                    new Kamer{ Naam = "Kamer 1", Afbeelding = "https://i.imgur.com/UIcZyA4.png", Grootte = 1, KamerId = 1, Prijs = 1000 },
                    new Kamer{ Naam = "Kamer 2", Afbeelding = "https://i.imgur.com/PIjjABM.jpgg", Grootte = 3, KamerId = 2, Prijs = 4000 }
                };

            repoKamers.Setup(r => r.GetAll()).Returns(kamers);

            repoKamers.Setup(mr => mr.GetKamerById(
                It.IsAny<int>())).Returns((int i) => kamers.Where(
                x => x.KamerId == i).Single());

            repoKamers.Setup(mr => mr.Create(
                It.IsAny<Kamer>())).Callback((Kamer target) => kamers.Add(target));

            repoKamers.Setup(mr => mr.Delete(
                It.IsAny<Kamer>())).Callback((Kamer target) => kamers.Remove(target));

            repoKlanten = new Mock<IKlantRepository>();
            List<Boeking> klant1boekings = new List<Boeking>();
            klant1boekings.Add(repo.Object.GetBoekingById(1));
            klanten = new List<Klant>
            {
                    new Klant{ Naam = "Jopie", KlantId = 1, Woonplaats = "Wildewesten", Huisnummer = 12, Mail = "dhfjopie@gmail.com", Postcode = "5821", Straat = "Hoghenstraat", Boekings = klant1boekings },
                    new Klant{ Naam = "Klaas", KlantId = 2, Woonplaats = "Vladivostok", Huisnummer = 42, Mail = "dhfjopie@gmail.com", Postcode = "5821", Straat = "Hoghenstraat" }
            };

            repoKlanten.Setup(r => r.GetAll()).Returns(klanten);

            repoKlanten.Setup(mr => mr.GetKlantById(
                It.IsAny<int>())).Returns((int i) => klanten.Where(
                x => x.KlantId == i).Single());

            repoKlanten.Setup(mr => mr.Create(
                It.IsAny<Klant>())).Callback((Klant target) => klanten.Add(target));

            repoKlanten.Setup(mr => mr.Delete(
                It.IsAny<Klant>())).Callback((Klant target) => klanten.Remove(target));
        }


        [TestMethod]
        public void Index()
        {
            // Arrange
            BoekingsController controller = new BoekingsController(repo.Object,repoKamers.Object,repoKlanten.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CanReturnAll()
        {
            // Arrange
            BoekingsController controller = new BoekingsController(repo.Object, repoKamers.Object, repoKlanten.Object);

            // Act
            List<Boeking> boekingen = repo.Object.GetAll();
            List<BoekingViewModel> vms = new List<BoekingViewModel>();
            foreach (var b in boekingen)
            {
                vms.Add(new BoekingViewModel(b));
            }

            // Assert
            Assert.IsNotNull(vms);
            Assert.AreEqual(2, vms.Count());
        }

        [TestMethod]
        public void Details()
        {
            // Arrange
            BoekingsController controller = new BoekingsController(repo.Object, repoKamers.Object, repoKlanten.Object);

            // Act
            BoekingViewModel result = new BoekingViewModel(repo.Object.GetBoekingById(2));

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(BoekingViewModel));
            Assert.AreEqual(result.BoekingId, 2);
        }

        [TestMethod]
        public void Create()
        {
            // Arrange
            BoekingsController controller = new BoekingsController(repo.Object, repoKamers.Object, repoKlanten.Object);
            Boeking newBoeking = new Boeking { Datum = DateTime.Now, IdKamer = 2, IdKlant = 1, BoekingId = 3 };
            int boekingcount = this.repo.Object.GetAll().Count();

            // Act
            repo.Object.Create(newBoeking);
            boekingcount = this.repo.Object.GetAll().Count();

            // Assert
            Assert.AreEqual(3, boekingcount);
            var testboeking = repo.Object.GetBoekingById(3);
            Assert.IsNotNull(testboeking);
            Assert.IsInstanceOfType(testboeking, typeof(Boeking));
            Assert.AreEqual(3, testboeking.BoekingId);
        }

        [TestMethod]
        public void DeletePost()
        {
            // Arrange
            BoekingsController controller = new BoekingsController(repo.Object, repoKamers.Object, repoKlanten.Object);
            Boeking deleteBoeking = repo.Object.GetBoekingById(2);
            int boekingcount = this.repo.Object.GetAll().Count();

            // Act
            repo.Object.Delete(deleteBoeking);
            boekingcount = this.repo.Object.GetAll().Count();

            Assert.AreEqual(1, boekingcount);
        }

        [TestMethod]
        public void Edit()
        {
            // Arrange
            BoekingsController controller = new BoekingsController(repo.Object, repoKamers.Object, repoKlanten.Object);

            // Act
            ViewResult result = controller.Edit(1) as ViewResult;

            // Assert
            Assert.IsInstanceOfType(result.Model, typeof(BoekingViewModel));
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            BoekingsController controller = new BoekingsController(repo.Object, repoKamers.Object, repoKlanten.Object);

            // Act
            ViewResult result = controller.Delete(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
