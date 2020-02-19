using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.DAL
{
    public class ParkSqlDAO
    {
        private string connectionString;

        public ParkSqlDAO(string connString)
        {
            this.connectionString = connString;
        }

        public List<Park> GetParks()
        {
            // Declare the result variable
            List<Park> list = new List<Park>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Open the connection
                    conn.Open();

                    // Create the command for the sql statement
                    string sql = "Select * from park";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    // Execute the query and get the result set in a reader
                    SqlDataReader rdr = cmd.ExecuteReader();

                    // For each row, create a new country and add it to the list
                    while (rdr.Read())
                    {
                        Park park = RowToObject(rdr);

                        list.Add(park);
                    }

                }
            }
            catch (SqlException ex)
            {
                // TODO: Catch SQL Exceptions
            }

            return list;
        }

        private static Park RowToObject(SqlDataReader rdr)
        {
            //    public int Id { get; set; }
            //public string Name { get; set; }
            //public string Location { get; set; }
            //public int EstablishDate { get; set; }
            //public double Area { get; set; }
            //public int AnnualVisitors { get; set; }
            //public string Description { get; set; }

            Park park = new Park()
            {
                Id = Convert.ToInt32(rdr["park_id"]),
                Name = Convert.ToString(rdr["name"]),
                Location = Convert.ToString(rdr["location"]),
                EstablishDate = Convert.ToDateTime(rdr["establish_date"]),
                Area = Convert.ToInt32(rdr["area"]),
                AnnualVisitors = Convert.ToInt32(rdr["visitors"]),
                Description = Convert.ToString(rdr["description"])
            };

            return park;
        }

    }
}
