using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.DAL
{
    public class ReservationDAO : IReservationDAO
    {
        private string connectionString;

        public ReservationDAO(string connString)
        {
            this.connectionString = connString;
        }

        public List<Reservation> GetReservations()
        {
            List<Reservation> list = new List<Reservation>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {

                    conn.Open();

                    // Create the command for the sql statement
                    string sql = "Select * from reservation";
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
                // TODO: Catch ReservationDAO Exceptions
            }

            return list;
        }

        private static Reservation RowToObject(SqlDataReader rdr)
        {
            Reservation reservation = new Reservation()
            {
                ReservationId = Convert.ToInt32(rdr["reservation_id"]),
                SiteId = Convert.ToInt32(rdr["campground_id"]),
                Name = Convert.ToString(rdr["name"]),
                FromDate = Convert.ToDateTime(rdr["from_date"]),
                ToDate = Convert.ToDateTime(rdr["to_date"]),
                CreateDate = Convert.ToDateTime(rdr["create_date"])
            };

            return reservation;
        }

    }
}
