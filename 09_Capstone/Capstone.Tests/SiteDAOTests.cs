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
            SiteDAO dao = new SiteDAO(connectionString); //arrange
            IList<Site> sites = dao.GetSites(); //act
            Assert.AreEqual(4, sites.Count); //assert
        }

        //TODO: Test failing now that changed method
        [TestMethod]
        public void GetTop5AvailableSitesTests()
        {
            SiteDAO dao = new SiteDAO(connectionString); //arrange

            //Date conflict with 1 of 4 sites
            IList<Site> sites = dao.GetTop5AvailableSites(newCampgroundId, DateTime.Now, DateTime.Now); //act
            Assert.AreEqual(3, sites.Count); //assert

            //Fully available date
            sites = dao.GetTop5AvailableSites(newCampgroundId, Convert.ToDateTime("2021/01/01"), Convert.ToDateTime("2021/05/01")); //act
            Assert.AreEqual(4, sites.Count); //assert
        }

        //ToDo: Test failing now that changed method
        [TestMethod]
        public void HasAvailableSitesTests()
        {
            SiteDAO dao = new SiteDAO(connectionString); //arrange

            //Site is available
            bool isAvailable = dao.HasAvailableSites(newCampgroundId, Convert.ToDateTime("2021/01/01"), Convert.ToDateTime("2021/05/01")); //act
            Assert.IsTrue(isAvailable);

            //No sites available
            isAvailable = dao.HasAvailableSites(newCampgroundId, DateTime.Now, DateTime.Now); //act
            Assert.IsTrue(isAvailable);
        }
    }
}
