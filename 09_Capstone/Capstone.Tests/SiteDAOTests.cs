using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Transactions;
using Capstone.DAL;
using System.Collections.Generic;
using Capstone.Models;

namespace Capstone.Tests
{
    [TestClass]
    public class SiteDAOTests : TestInitCleanup
    {
        private string connectionString = "Server=.\\SqlExpress;Database=npcampground;Trusted_Connection=True;";

        [TestMethod]
        public void GetSitesTests()
        {
            SetupDatabase(); //Setup data
            SiteDAO dao = new SiteDAO(connectionString); //arrange
            IList<Site> sites = dao.GetSites(); //act
            Assert.AreEqual(4, sites.Count); //assert
            CleanupDatabase(); //reset database
        }

        [TestMethod]
        public void GetTop5AvailableSitesTests()
        {
            //int campgroundId, DateTime arrivalDate, DateTime departureDate
            SetupDatabase(); //Setup data
            SiteDAO dao = new SiteDAO(connectionString); //arrange

            //Date conflict
            IList<Site> sites = dao.GetTop5AvailableSites(newCampgroundId, DateTime.Now, DateTime.Now); //act
            Assert.AreEqual(0, sites.Count); //assert

            //Available date
            sites = dao.GetTop5AvailableSites(newCampgroundId, Convert.ToDateTime("2021/01/01"), Convert.ToDateTime("2021/05/01")); //act
            Assert.AreEqual(1, sites.Count); //assert

            CleanupDatabase(); //reset database
        }

        [TestMethod]
        public void HasAvailableSitesTests()
        {
            //int campgroundId, DateTime arrivalDate, DateTime departureDate
            SetupDatabase(); //Setup data
            SiteDAO dao = new SiteDAO(connectionString); //arrange

            //Site is not available
            bool isAvailable = dao.HasAvailableSites(newCampgroundId, DateTime.Now, DateTime.Now); //act
            Assert.IsFalse(isAvailable);

            //Site is available
            isAvailable = dao.HasAvailableSites(newCampgroundId, Convert.ToDateTime("2021/01/01"), Convert.ToDateTime("2021/05/01")); //act
            Assert.IsTrue(isAvailable);

            CleanupDatabase(); //reset database
        }
    }
}
