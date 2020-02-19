using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Campground
    {
        public int Id { get; set; }
        public int ParkId { get; set; }
        public string Name { get; set; }
        public int OpenMonths { get; set; }
        public int ClosedMonths { get; set; }
        public decimal DailyFee { get; set; }
        public Campground (int id, int parkId, string name, int openMonths, int closedMonths, decimal dailyFee)
        {
            Id = id;
            ParkId = parkId;
            Name = name;
            OpenMonths = openMonths;
            ClosedMonths = closedMonths;
            DailyFee = dailyFee;
        }
    }
}
