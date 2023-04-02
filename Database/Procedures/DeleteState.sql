CREATE PROCEDURE [dbo].[DeleteState]
	@Id int
AS
	DELETE Friend
	WHERE Id = @Id
RETURN 0
