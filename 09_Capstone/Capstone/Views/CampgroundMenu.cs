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
            this.menuOptions.Add("2", "Return to Previous Screen");
            this.quitKey = "2";
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
                        CampgroundMenu cm = new CampgroundMenu(parkDAO, campgroundDAO, reservationDAO, siteDAO, park);
                        cm.Run();
                    }

                    Console.WriteLine("What is the arrival date? (MM/DD/YYYY): ");
                    string arrival = Console.ReadLine();
                    DateTime arrivalDate = DateTime.Parse(arrival);

                    Console.WriteLine("What is the departure date? (MM/DD/YYYY): ");
                    string departure = Console.ReadLine();
                    DateTime departureDate = DateTime.Parse(departure);

                    ShowReservationResults(campground, arrivalDate, departureDate);
                    
                    return true;
                
            }
            return true;
        }

        private void ShowReservationResults(int campground, DateTime arrivalDate, DateTime departureDate)
        {
            bool isAvailable = siteDAO.HasAvailableSites(campground, arrivalDate, departureDate);
            if (!isAvailable)
            {
                Console.WriteLine($"The date range preferred {arrivalDate}-{departureDate} is not available. Please make another selection.");

            }
            else
            {
                Console.WriteLine($"Results Matching your Search Criteria");
                Console.WriteLine($"Site No. \tMax Occup. \tAccessible? \tMax RV Length \tUtility \tCost");

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
                

                foreach (Site site in availableSites)
                {
                    Console.WriteLine($"{site.SiteId} \t{site.MaxOccupancy} \t{IsSiteAccessible(availableSites)} \t{MaxRVLength(availableSites)} \t{UtilitiesAvailable(availableSites)} \t{campground1.DailyFee:C}");
                }
                Console.WriteLine($"Which site whould be reserved (enter 0 to cancel)?");
                string response = Console.ReadLine();
                int campsite = int.Parse(response);
                if (campsite == 0)
                {
                    CampgroundMenu cm = new CampgroundMenu(parkDAO, campgroundDAO, reservationDAO, siteDAO, park);
                    cm.Run();
                }
                

                Console.WriteLine($"What name should the reservation be made under?");
                string reservationName = Console.ReadLine();


                Console.WriteLine($"The reservation has been made and the confirmation id is .");

            }
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

        protected override void AfterDisplayMenu()
        {
            base.AfterDisplayMenu();
            SetColor(ConsoleColor.Cyan);
            Console.WriteLine("Display some data here, AFTER the sub-menu is shown....");
            ResetColor();
        }

        public void PrintHeader()
        {
            List<Campground> camps = campgroundDAO.GetCampgrounds();
            

            SetColor(ConsoleColor.Magenta);
            Console.WriteLine(Figgle.FiggleFonts.Standard.Render($""));
            Console.WriteLine($"Search for Campground Reservation");
            Console.WriteLine($"\tName \tOpen \tClose \tDaily Fee");
            foreach (Campground camp in camps)
            {
                Console.WriteLine($"#{camp.Id} \t{camp.Name} \t{camp.OpenMonths} \t{camp.ClosedMonths} \t{camp.DailyFee:C}");
            }
            ResetColor();
        }

      

    }
}
