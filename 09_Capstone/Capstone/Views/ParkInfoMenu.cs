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
        private string connectionString;

        /// <summary>
        /// Constructor adds items to the top-level menu
        /// </summary>
        public ParkInfoMenu(/** DAOs may be passed in... ***/) :
            base("Sub-Menu 1")
        {
            // Store any values or DAOs passed in....
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
                    WriteError("Not yet implemented");
                    Pause("");
                    return true;
                case "2": // Do whatever option 2 is
                    WriteError("Not yet implemented");
                    Pause("");
                    return false;
            }
            return true;
        }

        protected override void BeforeDisplayMenu()
        {
            PrintHeader(Selection);
        }

        protected override void AfterDisplayMenu()
        {
            base.AfterDisplayMenu();
            SetColor(ConsoleColor.Cyan);
            Console.WriteLine("Display some data here, AFTER the sub-menu is shown....");
            ResetColor();
        }

        public void PrintHeader(string Selection)
        {
            int parkId = int.Parse(Selection);
            ParkSqlDAO pk = new ParkSqlDAO(connectionString);
            List<Park> parks = pk.GetParks();
            SetColor(ConsoleColor.Magenta);
            Console.WriteLine(Figgle.FiggleFonts.Standard.Render($"{parks[parkId].Name}"));
            Console.WriteLine($"Location:       {parks[parkId].Location}");
            Console.WriteLine($"Established:    {parks[parkId].EstablishDate}");
            Console.WriteLine($"Area:           {parks[parkId].Area}");
            Console.WriteLine($"Annual Visitors:{parks[parkId].AnnualVisitors}");
            Console.WriteLine($"{parks[parkId].Description}");
            ResetColor();
        }

        //public void ListParks(string Selection)
        //{
        //    int parkId = int.Parse(Selection);
        //    List<Park> parks = ParkSqlDAO.GetParks();
        //}

    }
}
