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
        private string Selection = null;


        /// <summary>
        /// Constructor adds items to the top-level menu. YOu will likely have parameters for one or more DAO's here...
        /// </summary>
        public MainMenu(IParkSqlDAO parkSqlDAO) : base("Main Menu")
        {
            this.parkSqlDAO = parkSqlDAO;

        }



        protected override void SetMenuOptions()
        {

            List<Park> parks = parkSqlDAO.GetParks();

            Console.WriteLine($"Select a Park for Further Details: ");

            foreach (Park park in parks)
            {
                this.menuOptions.Add(park.Id.ToString(), park.Name);

            }
            this.menuOptions.Add("Q", "Quit");

            //Console.WriteLine($"Please enter selection here: ");
            //Selection = Console.ReadLine();

            //int parkId = 0;

            //if (Selection != "q" || Selection != "Q")
            //{
            //    parkId = int.Parse(Selection);
            //} 
            //else
            //{
            //this.quitKey = "q";
            //}

            //if (parkId > parks.Count || parkId < 1)
            //{
            //    Console.WriteLine($"Please enter valid selection:");
            //    Selection = Console.ReadLine();
            //    parkId = int.Parse(Selection);
            //}
            //else
            //{
            //ExecuteSelection(Selection);

            //}



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
            parkId = int.Parse(Selection);

            Console.Clear();

            Park park = null;
            foreach(Park p in parks)
            {
                if (p.Id == parkId)
                {
                    park = p;
                    break;
                }
            }

            ParkInfoMenu sm = new ParkInfoMenu(parkSqlDAO, park);
            sm.Run();

            //switch (parkId)
            //{

            //    case 1: // Do whatever option 1 is
            //        Console.Clear();   
            //        ParkInfoMenu sm = new ParkInfoMenu(parkSqlDAO, parks[parkId - 1]);
            //        sm.Run();
            //        Pause("Press enter to continue");
            //        return true;    // Keep running the main menu
            //    case 2: // Do whatever option 2 is
            //        Console.Clear();
            //        ParkInfoMenu sm1 = new ParkInfoMenu(parkSqlDAO, parks[parkId - 1]);
            //        sm1.Run();
            //        Pause("Press enter to continue");
            //        return true;    // Keep running the main menu
            //    case 3: // Create and show the sub-menu
            //        Console.Clear();
            //        ParkInfoMenu sm2 = new ParkInfoMenu(parkSqlDAO, parks[parkId - 1]);
            //        sm2.Run();
            //        Pause("Press enter to continue");
            //        return true;    // Keep running the main menu
            //}
            return true;
        }

        protected override void BeforeDisplayMenu()
        {
            PrintHeader();
        }


        private void PrintHeader()
        {

            SetColor(ConsoleColor.Magenta);
            Console.WriteLine(Figgle.FiggleFonts.Standard.Render("National Parks"));

            ResetColor();
        }
    }
}
