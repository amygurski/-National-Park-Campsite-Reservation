using Capstone.Models;
using System;
using System.Collections.Generic;

namespace Capstone.DAL
{
    public interface IReservationDAO
    {
        List<Reservation> GetReservations();
        bool IsReservationAvailable(int campground, DateTime arrivalDate, DateTime departureDate);
    }
}