CALL public.sp_get_details();

SELECT * FROM report.top_ten_rentals_summary;
SELECT * FROM report.top_ten_rentals_details;

-- delete data for testing
DELETE FROM report.top_ten_rentals_summary;
DELETE FROM report.top_ten_rentals_details;