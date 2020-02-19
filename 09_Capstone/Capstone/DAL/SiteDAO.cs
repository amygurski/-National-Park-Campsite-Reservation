using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.DAL
{
    public class SiteDAO : ISiteDAO
    {
        private string connectionString;

        public SiteDAO(string connString)
        {
            this.connectionString = connString;
        }

        public List<Site> GetSites()
        {
            List<Site> list = new List<Site>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {

                    conn.Open();

                    // Create the command for the sql statement
                    string sql = "Select * from site";
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
                // TODO: Catch SiteDAO Exceptions
            }

            return list;
        }

        private static Site RowToObject(SqlDataReader rdr)
        {
            Site site = new Site()
            {
                SiteId = Convert.ToInt32(rdr["site_id"]),
                CampgroundId = Convert.ToInt32(rdr["campground_id"]),
                SiteNumber = Convert.ToInt32(rdr["site_number"]),
                MaxOccupancy = Convert.ToInt32(rdr["max_occupancy"]),
                IsAccessible = Convert.ToBoolean(rdr["accessible"]),
                MaxRVLength = Convert.ToInt32(rdr["max_rv_length"]),
                HasUtilities = Convert.ToBoolean(rdr["utilities"])
            };

            return site;
        }

    }
}
