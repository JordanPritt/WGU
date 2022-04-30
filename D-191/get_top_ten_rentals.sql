-- Author:      Jordan Pritt
-- Description: This procedure gathers the top ten rentals based on times rented and amount spent on the given DVD.
--              Once the data is queried it is then added to the report table after transforming the minutes to hours.
-- Remarks:     Can, or should, be run whenever the rental table is updated. Since this is based on rentals.

CREATE OR REPLACE PROCEDURE get_top_ten_rentals_summary()

AS
$$

-- Start a tansaction to get the data
BEGIN

    -- clear out existing data to refresh list
    DELETE FROM report.top_ten_rentals_summary;

    INSERT INTO report.top_ten_rentals_summary (title, inventory_id, times_rented, total)
    SELECT f.title AS title,
           r.inventory_id AS inventory_id,
           COUNT(*) AS times_rented,
           SUM(p.amount) AS total
    FROM public.payment AS p
        JOIN public.rental AS r ON p.rental_id = r.rental_id
        JOIN public.inventory AS i ON r.inventory_id = i.inventory_id
        JOIN public.film AS f ON i.film_id = f.film_id
    GROUP BY r.inventory_id, f.title, f.rating, f.length
    ORDER BY total DESC
    LIMIT 10;

    -- Rollback when there is an exception to perserve data integrity
    EXCEPTION
        WHEN OTHERS THEN
            ROLLBACK;

-- Commit the data
END;
$$

LANGUAGE plpgsql;