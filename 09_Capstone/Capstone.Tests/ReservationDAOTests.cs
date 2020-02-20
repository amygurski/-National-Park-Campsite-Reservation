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
    public class ReservationDAOTests : TestInitCleanup
    {
        private string connectionString = "Server=.\\SqlExpress;Database=npcampground;Trusted_Connection=True;";

        [TestMethod]
        public void GetReservationsTests()
        {
            SetupDatabase(); //Setup data
            ReservationDAO dao = new ReservationDAO(connectionString); //arrange
            IList<Reservation> reservations = dao.GetReservations(); //act
            Assert.AreEqual(1, reservations.Count); //assert
            CleanupDatabase(); //reset database
        }
    }
}
