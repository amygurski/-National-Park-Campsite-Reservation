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