# Module 2 Capstone - National Park Campsite Reservation
Build a command line driven application using C# and SQL that the National Park Service can use to book campsite reservations. The information about Parks, Campgrounds, Campsites, and Reservations come from a SQL database.

## Screenshots of the application
 ###  **Main Menu**
 Displays parks

![MainMenu](https://github.com/amygurski/National-Park-Campsite-Reservation/blob/master/img/MainMenu.PNG)

 ###  **Park Menu**
 Shows details about the selected park
 
![ParkDetails](https://github.com/amygurski/National-Park-Campsite-Reservation/blob/master/img/Park.PNG)

###  **Campgrounds**
Shows campgrounds within Park

![Campgrounds](https://github.com/amygurski/National-Park-Campsite-Reservation/blob/master/img/Campgrounds.PNG)

###  **Site Details**
Show Top 5 available sites in selected campground. Checks against the park's camping season and existing reservations in database.

![AvailableSites](https://github.com/amygurski/National-Park-Campsite-Reservation/blob/master/img/AvailableSites.PNG)

###  **Site Reserved**
Confirms the reservations. It's added to the Reservation table in the database. 

![BookSite](https://github.com/amygurski/National-Park-Campsite-Reservation/blob/master/img/BookSite.PNG)

### Requirements
1. As a user of the system, I need the ability to view a list of the available parks in the system, sorted
    alphabetically by name.
       a. A park includes an id, name, location, established date, area, annual visitor count, and
          description.
2. As a user of the system, I need the ability to select a park that my customer is visiting and see a list of
    all campgrounds for that available park.
       a. A campground includes an id, name, open month, closing month, and a daily fee.
3. As a user of the system, I need the ability to select a campground and search for date availability so
    that I can make a reservation.
       a. A reservation search only requires the desired campground, a start date, and an end date.
       b. A campsite is unavailable if any part of their preferred date range overlaps with an existing
          reservation.
       c. If no campsites are available, indicate to the user that there are no available sites and ask them
          if they would like to enter in an alternate date range.
       d. The TOP 5 available campsites should be displayed along with the cost for the total stay.

4. As a user of the system, once I find a campsite that is open during the time window I am looking for, I
    need the ability to book a reservation at a selected campsite.
       a. A reservation requires a name to reserve under, a start date, and an end date.
       b. A confirmation id is presented to the user once the reservation has been submitted.

### Data Sources 
The application had access to a Relational Database populated with data. 
