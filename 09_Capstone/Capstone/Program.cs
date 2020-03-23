using Capstone.DAL;
using Capstone.Views;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get the connection string from the appsettings.json file
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
            string connectionString = configuration.GetConnectionString("Project");

            // DAOs to be passed to Main Menu
            IParkDAO parkDAO = new ParkDAO(connectionString);
            ICampgroundDAO campgroundDAO = new CampgroundDAO(connectionString);
            IReservationDAO reservationDAO = new ReservationDAO(connectionString);
            ISiteDAO siteDAO = new SiteDAO(connectionString);
            
            //Instantiate the main menu with the DAOs
            MainMenu mainMenu = new MainMenu(parkDAO, campgroundDAO, reservationDAO, siteDAO);

            // Run the menu
            mainMenu.Run();
        }
    }
}
