-- ========================================================================================
-- Author:      Jordan Pritt
-- Description: Creates a trigger to update the top 10 DVD report on DVD rental additions.
--              This will keep our report accurate and up-to-date as newer rentals occur.
--              Also adds in a trigger to update the summary when the details are updated.
-- ========================================================================================

-- function to run when the trigger is hit
CREATE OR REPLACE FUNCTION fn_summary_trigger() RETURNS TRIGGER AS $$
   BEGIN
      CALL sp_get_summary();
      RETURN NULL;
   END;$$
LANGUAGE plpgsql;

-- Run below on updates, we want to drop the trigger if it exists already
-- DROP TRIGGER tr_update_summary ON report.top_ten_rentals_details;

CREATE TRIGGER tr_update_summary
   AFTER INSERT ON report.top_ten_rentals_details
   FOR EACH ROW
      EXECUTE FUNCTION public.fn_summary_trigger();
