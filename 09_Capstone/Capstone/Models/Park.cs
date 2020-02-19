using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Park
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime EstablishDate { get; set; }
        public int Area { get; set; }
        public int AnnualVisitors { get; set; }
        public string Description { get; set; }
        //public Park (int id, string name, string location, int establishDate, double area, int annualVisitors, string description)
        //{
        //    Id = id;
        //    Name = name;
        //    Location = location;
        //    EstablishDate = establishDate;
        //    Area = area;
        //    AnnualVisitors = annualVisitors;
        //    Description = description;
        //}

        public void ViewParks()
        {
            List<Park> parks = ParksSqlDAO.GetParks();

                Console.WriteLine($"Select a Park for Further Details: ");

            foreach (Park park in parks)
            {
                Console.WriteLine($" {park.Id}. {park.Name}");
            }
            Console.WriteLine($"Please enter selection here: ");
                string response = Console.ReadLine();
                int selection = int.Parse(response);


        }
    }
}
