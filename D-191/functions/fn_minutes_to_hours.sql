-- Author:      Jordan Pritt
-- Description: Converts minutes to hours. This function assumes the integer input is 
--              a minutes represnetaion of time, and then converts that time to hours.

CREATE OR REPLACE FUNCTION fn_minutes_to_hours(lengthInMinutes INTEGER) RETURNS DECIMAL AS
$$
BEGIN
    RETURN ROUND(lengthInMinutes::DECIMAL / 60.0, 1);
END;
$$

LANGUAGE plpgsql;

-- Test call to indiciate success or failure 
-- SELECT fn_transform_length(120);

-- output when run like above:
-- *---*---------------------*
-- |   | fn_transform_length |
-- *---*---------------------*
-- | 1 | 2                   |
-- *---*---------------------*