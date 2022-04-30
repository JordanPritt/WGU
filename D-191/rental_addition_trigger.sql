-- Author:      Jordan Pritt
-- Description: Creates a trigger to update the top 10 DVD report on DVD rental additions.
--              This will keep our report accurate and up-to-date as newer rentals occur.
--              Also adds in a trigger to updatethe summary when the details are updated.

CREATE OR REPLACE FUNCTION fn_details_trigger() RETURNS TRIGGER AS $$
   BEGIN
    CALL get_top_ten_rentals_details();
    RETURN NULL;
   END; $$
LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION fn_summary_trigger() RETURNS TRIGGER AS $$
   BEGIN
      CALL get_top_ten_rentals_summary();
      RETURN NULL;
   END;$$
LANGUAGE plpgsql;

-- we need to drop the triggers because IF EXISTS or REPLACE do not exist
-- for triggers in PostgreSQL, like they do for tables/functions/etc...

DROP TRIGGER tr_new_rentals ON public.rental;
DROP TRIGGER tr_update_summary ON report.top_ten_rentals_details;

CREATE TRIGGER tr_new_rentals
    AFTER INSERT ON public.rental
    FOR EACH ROW
      EXECUTE FUNCTION public.fn_details_trigger();

CREATE TRIGGER tr_update_summary
   AFTER INSERT ON report.top_ten_rentals_details
   FOR EACH ROW
      EXECUTE FUNCTION public.fn_summary_trigger();