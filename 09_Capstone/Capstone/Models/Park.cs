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
        public int EstablishDate { get; set; }
        public double Area { get; set; }
        public int AnnualVisitors { get; set; }
        public string Description { get; set; }
        public Park (int id, string name, string location, int establishDate, double area, int annualVisitors, string description)
        {
            Id = id;
            Name = name;
            Location = location;
            EstablishDate = establishDate;
            Area = area;
            AnnualVisitors = annualVisitors;
            Description = description;
        }
    }
}
