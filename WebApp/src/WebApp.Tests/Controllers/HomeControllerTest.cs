using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApp;
using WebApp.Controllers;
using WebApp.Infrastructure;
using WebApp.Models;

namespace WebApp.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private IRecordRepository getRecordRepositoryObject()
        {
            return new RecordRepository(@"../../InputSource/SampleTestInput.txt");
        }

        [TestMethod]
        public void Check_For_Records_In_PassengerList()
        {
            // Arrange
            HomeController controller = new HomeController(getRecordRepositoryObject());

            // Act
            ViewResult result = controller.Index().Result as ViewResult;

            // Assert
            Assert.IsNotNull(result.Model as PassengerListViewModel);
            Assert.IsNotNull((result.Model as PassengerListViewModel).PassengerRecords);
            Assert.IsTrue((result.Model as PassengerListViewModel).PassengerRecords.Any());
        }

        [TestMethod]
        public void Add_with_Correct_Values()
        {
            // Arrange
            HomeController controller = new HomeController(getRecordRepositoryObject());

            // Act
            ViewResult result = controller.Add("1JAIME/KARENMRS-M2 .L/LVKBTB") as ViewResult;

            // Assert
            Assert.IsTrue(result.ViewBag.Success);
        }

        [TestMethod]
        public void Add_with_SpecialCharcter_Values()
        {
            // Arrange
            HomeController controller = new HomeController(getRecordRepositoryObject());

            // Act
            ViewResult result = controller.Add("1JAIME/ATKINS@-M2 .L/LVKBTB") as ViewResult;

            // Assert
            Assert.IsTrue(result.ViewBag.InvalidInput);
        }

        [TestMethod]
        public void Search_with_WildCard()
        {
            // Arrange
            HomeController controller = new HomeController(getRecordRepositoryObject());

            // Act
            ViewResult result = controller.Search("*.*").Result as ViewResult;
            // Assert
            Assert.IsNotNull(result.Model as List<PassengerRecord>);
            Assert.IsTrue((result.Model as List<PassengerRecord>).Any());
        }

        [TestMethod]
        public void Search_for_Specfic_Record()
        {
            // Arrange
            HomeController controller = new HomeController(getRecordRepositoryObject());

            // Act
            ViewResult result = controller.Search("LVGVUP").Result as ViewResult;
            // Assert
            Assert.IsNotNull(result.Model as List<PassengerRecord>);
            Assert.AreEqual((result.Model as List<PassengerRecord>).Count(), 1);
        }
    }
}
