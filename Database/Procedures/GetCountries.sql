CREATE PROCEDURE [dbo].[GetCountries]
AS
	SELECT 
		Id,
		Name,
		PhotoId
	FROM Country
RETURN 0
