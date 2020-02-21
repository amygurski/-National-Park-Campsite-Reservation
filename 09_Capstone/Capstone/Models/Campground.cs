using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Campground
    {
        #region Properties
        public int Id { get; set; }
        public int ParkId { get; set; }
        public string Name { get; set; }
        public int OpenMonths { get; set; }
        public int ClosedMonths { get; set; }
        public decimal DailyFee { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Calculates the total cost to stay at this camgpround
        /// </summary>
        /// <param name="arrivalDate"></param>
        /// <param name="departureDate"></param>
        /// <returns>decimal for total cost of stay</returns>
        public decimal TotalStayCost(DateTime arrivalDate, DateTime departureDate)
        {
            return this.DailyFee * Convert.ToDecimal((departureDate - arrivalDate).TotalDays);
        }

        /// <summary>
        /// Checks if the campground is open (in-season)
        /// </summary>
        /// <param name="arrivalDate"></param>
        /// <param name="departureDate"></param>
        /// <returns>True if the reservation request is in season</returns>
        public bool IsCampgroundOpen(DateTime arrivalDate, DateTime departureDate)
        {
            if (arrivalDate.Month >= OpenMonths & departureDate.Month <= ClosedMonths)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
