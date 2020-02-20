using Capstone.DAL;
using Capstone.Models;
using System;
using System.Collections.Generic;

namespace Capstone.Views
{
    /// <summary>
    /// The top-level menu in our Market Application
    /// </summary>
    public class MainMenu : CLIMenu
    {
        // DAOs - Interfaces to our data objects can be stored here...
        protected IParkSqlDAO parkSqlDAO;
        //protected ICountryDAO countryDAO;

        /// <summary>
        /// Constructor adds items to the top-level menu. YOu will likely have parameters for one or more DAO's here...
        /// </summary>
        public MainMenu(IParkSqlDAO parkSqlDAO) : base("Main Menu")
        {
            this.parkSqlDAO = parkSqlDAO;
            //this.countryDAO = countryDAO;
        }

        private string Selection = null;
        //private int ParkId = 0;

        protected override void SetMenuOptions()
        {
            List<Park> parks = parkSqlDAO.GetParks();

            Console.WriteLine($"Select a Park for Further Details: ");

            foreach (Park park in parks)
            {
                Console.WriteLine($" {park.Id}. {park.Name}");
            }
            Console.WriteLine($"Please enter selection here: ");
            Selection = Console.ReadLine();
            int parkId = int.Parse(Selection);

            if (parkId > parks.Count || parkId < 1)
            {
                Console.WriteLine($"Please enter valid selection:");
                Selection = Console.ReadLine();
                int parkId1 = int.Parse(Selection);
            }
            else
            {
            ExecuteSelection(Selection);

            }

            
        }

        /// <summary>
        /// The override of ExecuteSelection handles whatever selection was made by the user.
        /// This is where any business logic is executed.
        /// </summary>
        /// <param name="choice">"Key" of the user's menu selection</param>
        /// <returns></returns>
        protected override bool ExecuteSelection(string Selection)
        {
            List<Park> parks = parkSqlDAO.GetParks();
            int parkId = 0;

            if (Selection != "q" || Selection != "Q")
            {
                parkId = int.Parse(Selection);
                Console.Clear();
                Console.WriteLine();
                
            }
            

            switch (parkId)
            {
                
                case 1: // Do whatever option 1 is
                    Console.Clear();   
                    ParkInfoMenu sm = new ParkInfoMenu();                 
                    sm.PrintHeader(Selection);
                    Pause("Press enter to continue");
                    return true;    // Keep running the main menu
                case 2: // Do whatever option 2 is
                    WriteError("Not yet implemented");
                    Pause("");
                    return true;    // Keep running the main menu
                case 3: // Create and show the sub-menu
                    ParkInfoMenu pm = new ParkInfoMenu();
                    pm.Run();
                    return true;    // Keep running the main menu
            }
            return true;
        }

        protected override void BeforeDisplayMenu()
        {
            PrintHeader();
        }


        private void PrintHeader()
        {
            int parkId = int.Parse(Selection);
            List<Park> parks = parkSqlDAO.GetParks();
            SetColor(ConsoleColor.Magenta);
            Console.WriteLine(Figgle.FiggleFonts.Standard.Render($"National Parks"));
            
            ResetColor();
        }
    }
}
