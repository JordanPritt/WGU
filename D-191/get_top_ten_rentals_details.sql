-- Author:      Jordan Pritt
-- Description: This procedure gathers the top ten rentals film aggregated information, for insights into the 
--              films that are the most popular amongst the customers.

CREATE OR REPLACE PROCEDURE get_top_ten_rentals_details()

AS
$$

-- Start a tansaction to get the data
BEGIN

    -- clear out existing data to refresh list
    DELETE FROM report.top_ten_rentals_details;

    INSERT INTO report.top_ten_rentals_details (title,
                                                inventory_id,
                                                release_year,
                                                rental_duration,
                                                rental_rate,
                                                length,
                                                replacement_cost,
                                                rating,
                                                times_rented,
                                                total)
    SELECT f.title,
           r.inventory_id,
           f.release_year,
           f.rental_duration,
           f.rental_rate,
           fn_transform_length(f.length) AS hours_long,
           f.replacement_cost,
           f.rating,
           COUNT(*) AS times_rented,
           SUM(p.amount) AS total
    FROM public.payment AS p
        JOIN public.rental AS r ON p.rental_id = r.rental_id
        JOIN public.inventory AS i ON r.inventory_id = i.inventory_id
        JOIN public.film AS f ON i.film_id = f.film_id
    GROUP BY f.title,
             r.inventory_id,
             f.release_year,
             f.rental_duration,
             f.rental_rate,
             f.length,
             f.replacement_cost,
             f.rating
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