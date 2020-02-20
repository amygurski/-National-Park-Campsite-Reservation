using Capstone.DAL;
using Capstone.Models;
using System;
using System.Collections.Generic;

namespace Capstone.Views
{
    /// <summary>
    /// The top-level menu in our Market Application
    /// </summary>
    public class ParkInfoMenu : CLIMenu
    {
        // Store any private variables, including DAOs here....
        private string Selection = null;
        protected IParkDAO parkSqlDAO;
        private string connectionString;
        private Park park;

        /// <summary>
        /// Constructor adds items to the top-level menu
        /// </summary>
        public ParkInfoMenu(IParkDAO parkSqlDAO, Park park) :
            base("ParkInfoMenu")
        {
            this.parkSqlDAO = parkSqlDAO;
            this.park = park;
        }

        protected override void SetMenuOptions()
        {
            this.menuOptions.Add("1", "View Campgrounds");
            this.menuOptions.Add("2", "Search for Reservation");
            this.menuOptions.Add("B", "Back to Main Menu");
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
                     
                    Pause("");
                    return true;
                case "2": // Do whatever option 2 is
                    
                    Pause("");
                    return false;
            }
            return true;
        }

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

        public void PrintHeader(Park park)
        {
            //int parkId = int.Parse(Selection);
            //ParkSqlDAO pk = new ParkSqlDAO(connectionString);
            // List<Park> parks = .GetParks();
            SetColor(ConsoleColor.Magenta);
            Console.WriteLine(Figgle.FiggleFonts.Standard.Render($"{park.Name}"));
            Console.WriteLine($"Location:       {park.Location}");
            Console.WriteLine($"Established:    {park.EstablishDate}");
            Console.WriteLine($"Area:           {park.Area}");
            Console.WriteLine($"Annual Visitors:{park.AnnualVisitors}");
            Console.WriteLine($"{park.Description}");
            ResetColor();
        }

      

    }
}
