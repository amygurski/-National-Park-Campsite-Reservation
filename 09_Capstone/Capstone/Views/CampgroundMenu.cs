using Capstone.DAL;
using Capstone.Models;
using System;
using System.Collections.Generic;

namespace Capstone.Views
{
    /// <summary>
    /// Campground Menu is the list of campgrounds inside the selected park
    /// The user can search for available reservations or return to the previous screen
    /// </summary>
    public class CampgroundMenu : CLIMenu
    {
        // Variables including DAOs
        protected IParkDAO parkDAO;
        protected ICampgroundDAO campgroundDAO;
        protected IReservationDAO reservationDAO;
        protected ISiteDAO siteDAO;
        private Park park;

        /// <summary>
        /// Constructor takes constructors and selected park
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
                case "1": //Select campground
                    Console.WriteLine("Which campground (enter 0 to cancel): ");
                    string response = Console.ReadLine();
                    int campground = 0;

                    if (int.TryParse(response, out int result))
                    {
                        campground = int.Parse(response);

                        if (campground == 0)
                        {
                            return true;
                        }
                    }
                    else //Prompt if invalid selection
                    {
                        Console.WriteLine($"Entry invalid. Please retry reservation. Thank you!");
                        return true;
                    }


                    //Get arrival date
                    Console.WriteLine("What is the arrival date? (MM/DD/YYYY): ");
                    string arrival = Console.ReadLine();

                    DateTime arrivalDate;
                    if (!DateTime.TryParse(arrival, out DateTime date))
                    {
                        Console.WriteLine($"Date invalid. Press 'Enter' to continue...");
                        Console.ReadLine();
                        return true;
                    }
                    else
                    {
                        arrivalDate = DateTime.Parse(arrival);
                    }


                    //Get departure date
                    Console.WriteLine("What is the departure date? (MM/DD/YYYY): ");
                    string departure = Console.ReadLine();

                    DateTime departureDate;

                    //TODO: Move to separate method rather than repeating code.
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

        /// <summary>
        /// Shows 5 campsites available for the user to select and reserve.
        /// Checks if the campground is open and if the dates.
        /// Prompts if the site is fully reserved.
        /// </summary>
        /// <param name="campgroundId"></param>
        /// <param name="arrivalDate"></param>
        /// <param name="departureDate"></param>
        private void ShowReservationResults(int campgroundId, DateTime arrivalDate, DateTime departureDate)
        {
            List<Site> availableSites = siteDAO.GetTop5AvailableSites(campgroundId, arrivalDate, departureDate);

            Campground campground = campgroundDAO.GetCampground(campgroundId);
            
            //Check that the selected campground is open for the requested dates (eg. no winter closures)
            bool campgoundOpen = campground.IsCampgroundOpen(arrivalDate, departureDate);
            if (!campgoundOpen)
            {
                Console.WriteLine($"Campground is open between {campground.DisplayMonths(campground.OpenMonths)} and {campground.DisplayMonths(campground.ClosedMonths)}. Please choose a date range within that timeframe. Thank you!");
                Console.WriteLine("Press enter to continue...");
                Console.ReadLine();
                return;
            }

            // Check that sites are available in database
            bool isAvailable = siteDAO.HasAvailableSites(campgroundId, arrivalDate, departureDate);
            if (!isAvailable)
            {
                Console.WriteLine($"The date range preferred {arrivalDate}-{departureDate} is not available. Please make another selection.");

            }
            else
            {
                //Display 5 available sites with details
                Console.WriteLine($"Results Matching your Search Criteria");
                Console.WriteLine("{0, -10} {1,-10} {2,-15} {3,-15} {4,-10} {5,-5}", "Site No.", "Max Occup.", "Accessible?", "Max RV Length", "Utility", "Cost");


                foreach (Site site in availableSites)
                {
                    //TODO: The 3 methods IsSiteAccessible(site), MaxRVLength(site), UtilitiesAvailable(site) are just text formatting. I'd like to change these to simple ternary expressions
                    Console.WriteLine(" {0, -10} {1,-10} {2,-15} {3,-15} {4,-10} {5,-5:C}", site.SiteId, site.MaxOccupancy, IsSiteAccessible(site), MaxRVLength(site), UtilitiesAvailable(site), campground.TotalStayCost(arrivalDate, departureDate));
                }

                //User to choose a site to reserve
                Console.WriteLine($" Which site whould be reserved (enter 0 to cancel)?");
                string response = Console.ReadLine();
                int campsite = int.Parse(response);
                if (campsite == 0)
                {
                    return;
                }

                //Get reservation details
                Console.WriteLine($" What name should the reservation be made under?");
                string reservationName = Console.ReadLine();

                //Add reservation to database
                int reservationId = reservationDAO.AddReservation(NewReservation(campsite, reservationName, arrivalDate, departureDate));

                //Let user know their reservation is confirmed
                Console.WriteLine($" The reservation has been made and the confirmation id is {reservationId}. Thank you for booking with us!");
                Console.WriteLine(" Press ENTER to continue...");
                Console.ReadLine();
            }
        }

        /// <summary>
        /// Create a reservation for adding to database
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="name"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Change display for utilities to Yes or N/A
        /// </summary>
        /// <param name="sites"></param>
        /// <returns>string of whether the site has utilities</returns>
        private string UtilitiesAvailable(Site site)
        {
            string utilitiesAvailable = null;

                if (site.HasUtilities)
                {
                    utilitiesAvailable = "Yes";
                }
                else
                {
                    utilitiesAvailable = "N/A";
                }

            return utilitiesAvailable;
        }

        /// <summary>
        /// Change display for MaxRV Length to string or N/A
        /// </summary>
        /// <param name="sites"></param>
        /// <returns>string of the max RV length allowed</returns>
        private string MaxRVLength(Site site)
        {
            string maxLength = null;
                if (site.MaxRVLength > 0)
                {
                    maxLength = Convert.ToString(site.MaxRVLength);
                }
                else
                {
                    maxLength = "N/A";
                }
            return maxLength;
        }

        /// <summary>
        /// Change display for IsSiteAccessible to Yes or No
        /// </summary>
        /// <param name="site"></param>
        /// <returns>string indicating Yes or No for site accessibility</returns>
        private string IsSiteAccessible(Site site)
        {
            string accessibility = null;
                if (site.IsAccessible)
                {
                    accessibility = "Yes";
                }
                else
                {
                    accessibility = "No";
                }
            return accessibility;
        }

        protected override void BeforeDisplayMenu()
        {
            PrintHeader();
        }

        
        /// <summary>
        /// Display campground header
        /// </summary>
        public void PrintHeader()
        {
            List<Campground> camps = campgroundDAO.GetCampgrounds();

            SetColor(ConsoleColor.Magenta);
            Console.WriteLine(Figgle.FiggleFonts.Standard.Render($""));
            Console.WriteLine($"Search for Campground Reservation");
            Console.WriteLine(" {0, -5} {1,-35} {2,-10} {3,-10} {4,-10}", "", "Name", "Open", "Close", "Daily Fee");

            foreach (Campground camp in camps)
            {
                Console.WriteLine("#{0, -5} {1,-35} {2,-10} {3,-10} {4,-10:C}", camp.Id, camp.Name, camp.DisplayMonths(camp.OpenMonths), camp.DisplayMonths(camp.ClosedMonths), camp.DailyFee);
            }
            ResetColor();
        }



    }
}
