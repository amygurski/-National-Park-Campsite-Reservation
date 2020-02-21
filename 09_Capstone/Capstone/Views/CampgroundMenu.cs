using Capstone.DAL;
using Capstone.Models;
using System;
using System.Collections.Generic;

namespace Capstone.Views
{
    /// <summary>
    /// The top-level menu in our Market Application
    /// </summary>
    public class CampgroundMenu : CLIMenu
    {
        // Store any private variables, including DAOs here....
        
        protected IParkDAO parkDAO;
        protected ICampgroundDAO campgroundDAO;
        protected IReservationDAO reservationDAO;
        protected ISiteDAO siteDAO;
        private Park park;

        /// <summary>
        /// Constructor adds items to the top-level menu
        /// </summary>
        public CampgroundMenu(IParkDAO parkDAO, ICampgroundDAO campgroundDAO, IReservationDAO reservationDAO, ISiteDAO siteDAO, Park park) :
            base("CamgroundMenu")
        {
            this.parkDAO = parkDAO;
            this.campgroundDAO = campgroundDAO;
            this.reservationDAO = reservationDAO;
            this.siteDAO = siteDAO;
            this.park = park;
        }

        protected override void SetMenuOptions()
        {
            this.menuOptions.Add("1", "Search for Available Reservations");
            this.menuOptions.Add("B", "Return to Previous Screen");
            this.quitKey = "B";
            
        }

        /// <summary>
        /// The override of ExecuteSelection handles whatever selection was made by the user.
        /// This is where any business logic is executed.
        /// </summary>
        /// <param name="choice">"Key" of the user's menu selection</param>
        /// <returns></returns>
        protected override bool ExecuteSelection(string choice)
        {
            switch (choice)
            {
                case "1": // Do whatever option 1 is
                    Console.WriteLine("Which campground (enter 0 to cancel): ");
                    string response =  Console.ReadLine();
                    int campground = int.Parse(response);

                    if (campground == 0)
                    {
                        return true;
                    }

                    
                    Console.WriteLine("What is the arrival date? (MM/DD/YYYY): ");
                    string arrival = Console.ReadLine();

                    DateTime arrivalDate;
                    if (DateTime.TryParse(arrival, out DateTime date))
                    {
                        arrivalDate = DateTime.Parse(arrival);
                    }
                    else
                    {
                        Console.WriteLine($"Date invalid. Press 'Enter' to continue...");
                        Console.ReadLine();
                        return true;
                    }
                    

                    Console.WriteLine("What is the departure date? (MM/DD/YYYY): ");
                    string departure = Console.ReadLine();
                    DateTime departureDate;

                    if (DateTime.TryParse(departure, out DateTime date2))
                    {
                        departureDate = DateTime.Parse(departure);
                    }
                    else
                    {
                        Console.WriteLine($"Date invalid. Press 'Enter' to continue...");
                        Console.ReadLine();
                        return true;
                    }

                    ShowReservationResults(campground, arrivalDate, departureDate);
                    
                    return true;
                
                
            }
            return true;
        }

        //private bool GiveProperDate(string date)
        //{
            
        //    DateTime arrivalDate = DateTime.Parse(date);
        //    return false;
        //}
        private void ShowReservationResults(int campground, DateTime arrivalDate, DateTime departureDate)
        {
                List<Site> availableSites = siteDAO.GetTop5AvailableSites(campground, arrivalDate, departureDate);

                List<Campground> campgrounds = campgroundDAO.GetCampgrounds();
                Campground campground1 = new Campground();
            foreach (Campground camp in campgrounds)
            {
                if (campground == camp.Id)
                {
                    campground1 = camp;
                    break;
                }
                else
                {
                    continue;
                }
            }
                
                bool campgoundOpen = campground1.IsCampgroundOpen(arrivalDate, departureDate);

            if (!campgoundOpen)
            {
                Console.WriteLine($"Campground is open between {campground1.DisplayMonths(campground1.OpenMonths)} and {campground1.DisplayMonths(campground1.ClosedMonths)}. Please choose a date range within that timeframe. Thank you!");
                Console.WriteLine("Press enter to continue...");
                Console.ReadLine();
                return;
            }
            
            bool isAvailable = siteDAO.HasAvailableSites(campground, arrivalDate, departureDate);

            if (!isAvailable)
            {
                Console.WriteLine($"The date range preferred {arrivalDate}-{departureDate} is not available. Please make another selection.");

            }
            else
            {
                Console.WriteLine($"Results Matching your Search Criteria");
                Console.WriteLine($"Site No. \t\tMax Occup. \t\tAccessible? \t\tMax RV Length \t\tUtility \t\tCost");



                
                //TODO: Fix spacing on Site view CLI
                foreach (Site site in availableSites)
                {
                    Console.WriteLine($"{site.SiteId} \t{site.MaxOccupancy} \t{IsSiteAccessible(availableSites)} \t{MaxRVLength(availableSites)} \t{UtilitiesAvailable(availableSites)} \t{campground1.TotalStayCost(arrivalDate, departureDate):C}");
                }
                Console.WriteLine($"Which site whould be reserved (enter 0 to cancel)?");
                string response = Console.ReadLine();
                int campsite = int.Parse(response);
                if (campsite == 0)
                {
                    
                    return;
                }
                

                Console.WriteLine($"What name should the reservation be made under?");
                string reservationName = Console.ReadLine();

                int reservationId = reservationDAO.AddReservation(NewReservation(campsite, reservationName, arrivalDate, departureDate));

                Console.WriteLine($"The reservation has been made and the confirmation id is {reservationId}. Thank you for booking with us!");
                Console.WriteLine("Press ENTER to continue...");
                Console.ReadLine();

                
            }
        }

      

        private Reservation NewReservation(int siteId, string name, DateTime fromDate, DateTime toDate)
        {
            Reservation newReservation = new Reservation()
            {
                SiteId = siteId,
                Name = name,
                FromDate = fromDate,
                ToDate = toDate,
                CreateDate = DateTime.Today
            };
            return newReservation;
        }

        private string UtilitiesAvailable(List<Site> sites)
        {
            string utilitiesAvailable = null;
            foreach (Site site in sites)
            {
                if (site.HasUtilities)
                {
                    utilitiesAvailable = "Yes";
                }
                else
                {
                    utilitiesAvailable = "N/A";
                }
            }

            return utilitiesAvailable;
        }
        private string MaxRVLength(List<Site> sites)
        {
            string maxLength = null;
            foreach (Site site in sites)
            {
                if (site.MaxRVLength > 0)
                {
                    maxLength = Convert.ToString(site.MaxRVLength);
                }
                else
                {
                    maxLength = "N/A";
                }
            }
            return maxLength;
        }

        private string IsSiteAccessible(List<Site> sites)
        {
          string accessibility = null;
          foreach (Site site in sites)
            {
                if (site.IsAccessible)
                {
                    accessibility = "Yes";
                    
                }
                else
                {
                    accessibility = "No";
                    
                }
            }
            return accessibility;
        }

        protected override void BeforeDisplayMenu()
        {
            PrintHeader();
            
        }

        //protected override void AfterDisplayMenu()
        //{
        //    base.AfterDisplayMenu();
        //    SetColor(ConsoleColor.Cyan);
        //    Console.WriteLine("Display some data here, AFTER the sub-menu is shown....");
        //    ResetColor();
        //}

        public void PrintHeader()
        {
            List<Campground> camps = campgroundDAO.GetCampgrounds();

            //TODO: Fix spacing on camground CLI
            SetColor(ConsoleColor.Magenta);
            Console.WriteLine(Figgle.FiggleFonts.Standard.Render($""));
            Console.WriteLine($"Search for Campground Reservation");
            Console.WriteLine($"\tName \tOpen \tClose \tDaily Fee");
            foreach (Campground camp in camps)
            {
               
                Console.WriteLine($"#{camp.Id} \t{camp.Name} \t{camp.DisplayMonths(camp.OpenMonths)} \t{camp.DisplayMonths(camp.ClosedMonths)} \t{camp.DailyFee:C}");
            }
            ResetColor();
        }

      

    }
}
