CREATE PROCEDURE [dbo].[DeleteCountry]
	@Id int
AS
	DELETE Country
	WHERE Id = @Id
RETURN 0
