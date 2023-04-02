CREATE PROCEDURE [dbo].[GetStatesByCountry]
	@CountryId int
AS
	SELECT Id,
		   Name,
		   PhotoId,
		   CountryId
	FROM State
	WHERE CountryId = @CountryId
RETURN 0
