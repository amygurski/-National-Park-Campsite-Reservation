using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.DAL
{
    public class CampgroundDAO : ICampgroundDAO
    {
        private string connectionString;

        public CampgroundDAO(string connString)
        {
            this.connectionString = connString;
        }

        public List<Campground> GetCampgrounds()
        {
            List<Campground> list = new List<Campground>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Create the command for the sql statement
                    string sql = "Select * from campground";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    // Execute the query and get the result set in a reader
                    SqlDataReader rdr = cmd.ExecuteReader();

                    // For each row, create a new country and add it to the list
                    while (rdr.Read())
                    {
                        list.Add(RowToObject(rdr));
                    }

                }
            }
            catch (SqlException ex)
            {
                // TODO: Catch CampgroundDAO Exceptions
                throw;
            }

            return list;
        }

        private static Campground RowToObject(SqlDataReader rdr)
        {

            Campground campground = new Campground()
            {
                Id = Convert.ToInt32(rdr["campground_id"]),
                ParkId = Convert.ToInt32(rdr["park_id"]),
                Name = Convert.ToString(rdr["name"]),
                OpenMonths = Convert.ToInt32(rdr["open_from_mm"]),
                ClosedMonths = Convert.ToInt32(rdr["open_to_mm"]),
                DailyFee = Convert.ToDecimal(rdr["daily_fee"])
            };

            return campground;
        }
    }
}
