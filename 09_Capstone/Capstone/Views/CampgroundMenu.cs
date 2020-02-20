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
        private string connectionString;
        private Park park;

        /// <summary>
        /// Constructor adds items to the top-level menu
        /// </summary>
        public CampgroundMenu(IParkDAO parkDAO, ICampgroundDAO campgroundDAO, Park park) :
            base("CamgroundMenu")
        {
            this.parkDAO = parkDAO;
            this.campgroundDAO = campgroundDAO;
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
                    
                    Console.WriteLine("What is the arrival date? (MM/DD/YYYY): ");
                    string arrival = Console.ReadLine();
                    DateTime arrivalDate = DateTime.Parse(arrival);

                    Console.WriteLine("What is the departure date? (MM/DD/YYYY): ");
                    string departure = Console.ReadLine();
                    DateTime departureDate = DateTime.Parse(departure);

                    ReservationDAO rv = new ReservationDAO(connectionString);
                    rv.IsReservationAvailable(campground, arrivalDate, departureDate);

                    return true;
                
            }
            return true;
        }

        //private void ViewCampgrounds()
        //{
        //    try
        //    {
        //        // get the cg
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("There was a problem gettng campgrounds. Please try again later and it will fail then also");
        //        Pause("Press enter to continue");
        //    }
        //}

        protected override void BeforeDisplayMenu()
        {
            PrintHeader(park);
            
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
            List<Campground> camps = new List<Campground>();
            CampgroundDAO cg = new CampgroundDAO(connectionString);
            cg.GetCampgrounds();

            SetColor(ConsoleColor.Magenta);
            Console.WriteLine(Figgle.FiggleFonts.Standard.Render($""));
            Console.WriteLine($"Search for Campground Reservation");
            foreach (Campground camp in camps)
            {
                Console.WriteLine($"{camp.Id} \t{camp.Name} \t{camp.OpenMonths} \t{camp.ClosedMonths}");
            }
            ResetColor();
        }

      

    }
}
