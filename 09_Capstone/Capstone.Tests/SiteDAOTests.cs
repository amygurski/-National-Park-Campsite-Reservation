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
            IList<Site> sites = dao.GetTop5AvailableSites(2, Convert.ToDateTime("2020-02-20"), Convert.ToDateTime("2020-02-20")); //act
            Assert.AreEqual(4, sites.Count); //assert
            CleanupDatabase(); //reset database
        }
    }
}
