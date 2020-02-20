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
    public class ParkDAOTests : TestInitCleanup
    {
        private string connectionString = "Server=.\\SqlExpress;Database=npcampground;Trusted_Connection=True;";

        [TestMethod]
        public void GetParksTests()
        {
            SetupDatabase(); //Setup data
            ParkDAO dao = new ParkDAO(connectionString); //arrange
            IList<Park> parks = dao.GetParks(); //act
            Assert.AreEqual(1, parks.Count); //assert
            CleanupDatabase(); //reset database
        }
    }
}
