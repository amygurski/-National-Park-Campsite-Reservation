SELECT * FROM park;
SELECT * FROM campground;
SELECT * FROM site;
SELECT * FROM reservation;


-- Get top 5 sites along with their daily fee
SELECT DISTINCT TOP 5 s.*, daily_fee * DATEDIFF(Day, '2020-02-24', '2020-03-01') as total_fee
FROM site s 
JOIN reservation r ON s.site_id = r.site_id 
JOIN campground c ON s.campground_id = c.campground_id 
WHERE c.campground_id = 2
AND (('2020-02-24' NOT BETWEEN from_date AND to_date) AND ('2020-03-01' NOT BETWEEN from_date AND to_date))


--Top 5 sites, site info only needed
SELECT DISTINCT TOP 5 s.*
FROM site s 
JOIN reservation r ON s.site_id = r.site_id 
JOIN campground c ON s.campground_id = c.campground_id 
WHERE c.campground_id = 2
AND (('2020-02-24' NOT BETWEEN from_date AND to_date) AND ('2020-03-01' NOT BETWEEN from_date AND to_date))

--TESTING

SELECT DISTINCT s.*
FROM site s 
JOIN reservation r ON s.site_id = r.site_id 
WHERE s.campground_id = 2
AND (('2020-02-20' NOT BETWEEN from_date AND to_date) AND ('2020-03-01' NOT BETWEEN from_date AND to_date))



DECLARE @campground_id int = 2;
DECLARE @startdate date = '2020-03-12'
DECLARE @enddate date = '2020-03-20'

SELECT * FROM SITE s
JOIN reservation r ON s.site_id = r.site_id 
WHERE campground_id = @campground_id

SELECT DISTINCT TOP 5 s.*
FROM site s
LEFT JOIN reservation r ON s.site_id = r.site_id 
WHERE s.campground_id = @campground_id 
AND s.site_id NOT IN(SELECT site_id FROM reservation WHERE(@startdate BETWEEN from_date AND to_date) OR from_date IS NULL) 
AND s.site_id NOT IN(SELECT site_id FROM reservation WHERE(@enddate BETWEEN from_date AND to_date) OR from_date IS NULL) 
AND s.site_id NOT IN(SELECT site_id FROM reservation WHERE(from_date BETWEEN @startdate AND @enddate) OR from_date IS NULL)


--To see all
SELECT * FROM SITE s
JOIN reservation r ON s.site_id = r.site_id 
WHERE campground_id = @campground_id

-- New query
DECLARE @campgroundId int = 2;
DECLARE @arrivalDate date = '2020-03-12'
DECLARE @departureDate date = '2020-03-20'

SELECT DISTINCT TOP 5 s.*
FROM site s
LEFT JOIN reservation r ON s.site_id = r.site_id
WHERE s.campground_id = @campgroundId
AND s.site_id NOT IN(SELECT site_id FROM reservation WHERE(@arrivalDate BETWEEN from_date AND to_date) OR from_date IS NULL)
AND s.site_id NOT IN(SELECT site_id FROM reservation WHERE(@departureDate BETWEEN from_date AND to_date) OR from_date IS NULL)
AND s.site_id NOT IN(SELECT site_id FROM reservation WHERE(from_date BETWEEN @arrivalDate AND @departureDate) OR from_date IS NULL)


