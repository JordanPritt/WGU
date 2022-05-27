-- =============================================================
-- Author:      Jordan Pritt
-- Description: Proof that the data we are quering is accurate.
-- =============================================================

DO 
$$

-- declare variables
DECLARE 
    item_id INTEGER;

BEGIN
    -- show the report data
    SELECT inventory_id
    INTO item_id
    FROM report.top_ten_rentals_summary
    LIMIT 1;

    RAISE NOTICE 'id: %', item_id;

    -- show the first record from query that populated report
    DROP TABLE IF EXISTS temp_result;
    CREATE TEMP TABLE temp_result AS
    SELECT f.title AS title, 
           r.inventory_id AS inventory_id, 
           f.rating, 
           f.length, COUNT(*) AS times_rented, 
           SUM(p.amount) AS total
    FROM public.payment AS p
        JOIN public.rental AS r ON p.rental_id = r.rental_id
        JOIN public.inventory AS i ON r.inventory_id = i.inventory_id
        JOIN public.film AS f ON i.film_id = f.film_id
    GROUP BY r.inventory_id, f.title, f.rating, f.length
    ORDER BY total DESC
    LIMIT 1;

    -- prove first record is correct - as of first successful run
    DROP TABLE IF EXISTS temp_total;
    CREATE temporary TABLE temp_total AS
    SELECT SUM(pay.amount) AS total_amount
    FROM public.rental AS rental
        JOIN public.payment AS pay ON rental.rental_id = pay.rental_id
    WHERE rental.inventory_id = item_id;
END; 
$$;

-- display results
SELECT * FROM temp_result;
SELECT * FROM temp_total;