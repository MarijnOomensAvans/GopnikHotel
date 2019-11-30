using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gopnik_Hotel;
using Gopnik_Hotel.Controllers;
using Gopnik_Hotel.ViewModels;
using Gopnik_Hotel.DomainModel.Repositories;
using Moq;
using Gopnik_Hotel.Models;

namespace Gopnik_Hotel.Tests.Controllers
{
    [TestClass]
    public class KamersControllerTest
    {
        Mock<IKamerRepository> repo;
        List<Kamer> kamers;

        public KamersControllerTest()
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
            KamersController controller = new KamersController(repo.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CanReturnAll()
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
            Assert.AreEqual(2,vms.Count());
        }

        [TestMethod]
        public void Details()
        {
            // Arrange
            KamersController controller = new KamersController(repo.Object);

            // Act
            KamerViewModel result = new KamerViewModel(repo.Object.GetKamerById(2));

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(KamerViewModel));
            Assert.AreEqual(result.Naam,"Kamer 2");
        }

        [TestMethod]
        public void Create()
        {
            // Arrange
            KamersController controller = new KamersController(repo.Object);
            Kamer newKamer = new Kamer { KamerId = 3, Afbeelding = "https://i.ytimg.com/vi/ZJ6fQhE4pcY/maxresdefault.jpg", Prijs = 1600, Naam = "Wadi Kamer", Grootte = 5 };
            int kamercount = this.repo.Object.GetAll().Count();

            // Act
            repo.Object.Create(newKamer);
            kamercount = this.repo.Object.GetAll().Count();

            // Assert
            Assert.AreEqual(3, kamercount);
            var testkamer = repo.Object.GetKamerById(3);
            Assert.IsNotNull(testkamer);
            Assert.IsInstanceOfType(testkamer, typeof(Kamer));
            Assert.AreEqual(3, testkamer.KamerId);
        }

        [TestMethod]
        public void DeletePost()
        {
            // Arrange
            KamersController controller = new KamersController(repo.Object);
            Kamer deleteKamer = repo.Object.GetKamerById(2);
            int kamercount = this.repo.Object.GetAll().Count();

            // Act
            repo.Object.Delete(deleteKamer);
            kamercount = this.repo.Object.GetAll().Count();

            Assert.AreEqual(1, kamercount);
        }

        [TestMethod]
        public void Edit()
        {
            // Arrange
            KamersController controller = new KamersController(repo.Object);

            // Act
            ViewResult result = controller.Edit(1) as ViewResult;

            // Assert
            Assert.IsInstanceOfType(result.Model, typeof(KamerViewModel));
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            KamersController controller = new KamersController(repo.Object);

            // Act
            ViewResult result = controller.Delete(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }


    }
}
