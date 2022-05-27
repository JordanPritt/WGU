-- ========================================
-- Author:      Jordan Pritt
-- Description: Retrieves the detail data.
-- ========================================

SELECT f.title,
        r.inventory_id,
        f.release_year,
        f.rental_duration,
        f.rental_rate,
        fn_minutes_to_hours(f.length) AS hours_long,
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
