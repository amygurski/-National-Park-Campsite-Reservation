SELECT * FROM park;
SELECT * FROM campground;
SELECT * FROM site;
SELECT * FROM reservation;

--Check if any sites available
SELECT *
from site s
JOIN  reservation r ON s.site_id = r.site_id
WHERE campground_id = 2
AND (('2020-02-20' NOT BETWEEN from_date AND to_date) AND ('2020-03-01' NOT BETWEEN from_date AND to_date))


-- Get top 5 sites along with their daily fee
SELECT DISTINCT TOP 5 s.*, daily_fee * DATEDIFF(Day, '2020-02-24', '2020-03-01') as total_fee
FROM site s 
LEFT JOIN reservation r ON s.site_id = r.site_id 
JOIN campground c ON s.campground_id = c.campground_id 
WHERE c.campground_id = 2
AND (('2020-02-24' NOT BETWEEN from_date AND to_date) AND ('2020-03-01' NOT BETWEEN from_date AND to_date))

-- Just checking all sites again a different way
SELECT s.*, daily_fee , from_date, to_date 
FROM site s 
LEFT JOIN reservation r ON s.site_id = r.site_id 
JOIN campground c ON s.campground_id = c.campground_id 
WHERE c.campground_id = 2
AND (('2020-02-20' NOT BETWEEN from_date AND to_date) AND ('2020-03-01' NOT BETWEEN from_date AND to_date))