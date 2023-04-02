CREATE PROCEDURE [dbo].[GetStateById]
	@Id int
AS
	SELECT Id,
		   Name,
		   PhotoId,
		   CountryId
	FROM State
	WHERE Id = @Id
RETURN 0
