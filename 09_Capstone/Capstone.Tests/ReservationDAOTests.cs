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
            ReservationDAO dao = new ReservationDAO(connectionString); //arrange
            IList<Reservation> reservations = dao.GetReservations(); //act
            Assert.AreEqual(1, reservations.Count); //assert
        }

        [TestMethod]
        public void AddReservationTests()
        {
            //Arrange, Act, Assert
            ReservationDAO dao = new ReservationDAO(connectionString);

            Reservation reservation = new Reservation()
            {
                SiteId = newSiteId,
                Name = "No-Name Family Reservation",
                FromDate = DateTime.Today.AddDays(14),
                ToDate = DateTime.Today.AddDays(21),
                CreateDate = DateTime.Today
            };

            int expectedResult = dao.AddReservation(reservation);

            Assert.AreEqual(newReservationId + 1, expectedResult);
        }
    }
}
