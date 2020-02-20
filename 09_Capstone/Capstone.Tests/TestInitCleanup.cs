using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Transactions;
using Capstone.Models;
using Capstone.DAL;

namespace Capstone.Tests
{
    public class TestInitCleanup
    {
        private TransactionScope transaction = null;
        private string connectionString = "Server=.\\SqlExpress;Database=npcampground;Trusted_Connection=True;";

        [TestInitialize]
        public void SetupDatabase()
        {
            

            // Start a transaction, so we can roll back when we are finished with this test
            transaction = new TransactionScope();

            // Open Setup.Sql and read in the script to be executed
            string setupSQL;
            using (StreamReader rdr = new StreamReader("Setup.sql"))
            {
                setupSQL = rdr.ReadToEnd();
            }

            // Connect to the DB and execute the script
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(setupSQL, conn);
                SqlDataReader rdr = cmd.ExecuteReader();

                // Get the result and save it for use later in a test.

                if (rdr.Read())
                {
                    int newCampgroundId = Convert.ToInt32(rdr["newCampgroundId"]);
                    int newParkId = Convert.ToInt32(rdr["newParkId"]);
                    int newSiteId = Convert.ToInt32(rdr["newSiteId"]);
                    int newReservationId = Convert.ToInt32(rdr["newReservationId"]);
                }
            }
        }

        [TestCleanup]
        public void CleanupDatabase()
        {
            // Rollback the transaction to get our good data back
            transaction.Dispose();
        }
    }
}
