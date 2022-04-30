-- Create the report schema
CREATE SCHEMA IF NOT EXISTS report;

-- Create the summary table to house our report data
CREATE TABLE IF NOT EXISTS report.top_ten_rentals_summary (
   id                SERIAL PRIMARY KEY  NOT NULL,
   title             TEXT NOT NULL,
   inventory_id      INT  NOT NULL,
   times_rented      INT  NOT NULL,
   total             INT  NOT NULL
);

-- Create the details table to house our report data
CREATE TABLE IF NOT EXISTS report.top_ten_rentals_details (
   id               SERIAL PRIMARY KEY NOT NULL,
   title            TEXT NOT NULL,
   inventory_id     INT NOT NULL,
   release_year     YEAR NOT NULL,
   rental_duration  INT,
   rental_rate      INT,
   length           INT,
   replacement_cost INT,
   rating           MPAA_RATING NOT NULL,
   times_rented     INT NOT NULL,
   total            INT NOT NULL
)
