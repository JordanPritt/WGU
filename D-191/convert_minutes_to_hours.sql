-- Author:      Jordan Pritt
-- Description: Converts minutes to hours. This function assumes the integer input is 
--              a minutes represnetaion of time, and then converts that time to hours.

CREATE OR REPLACE FUNCTION fn_transform_length(lengthInMinutes INTEGER) RETURNS INTEGER AS
$$
BEGIN
    RETURN lengthInMinutes / 60;
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