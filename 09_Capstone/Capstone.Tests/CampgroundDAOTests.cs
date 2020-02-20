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
    public class CampgroundDAOTests : TestInitCleanup
    {
        private string connectionString = "Server=.\\SqlExpress;Database=npcampground;Trusted_Connection=True;";
        private int newCampgroundId;

        [TestMethod]
        public void GetCampgroundsTests()
        {
            newCampgroundId = SetupDatabase("newCampgroundId"); //Setup data
            CampgroundDAO dao = new CampgroundDAO(connectionString); //arrange
            IList<Campground> campgrounds = dao.GetCampgrounds(); //act
            Assert.AreEqual(1, campgrounds.Count); //assert
            CleanupDatabase(); //reset database
        }
    }
}
