CREATE PROCEDURE [dbo].[GetCountryById]
	@Id int
AS
	SELECT 
		Id,
		Name,
		PhotoId
	FROM Country
	WHERE Id = @Id
RETURN 0
