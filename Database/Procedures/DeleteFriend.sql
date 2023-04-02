CREATE PROCEDURE [dbo].[DeleteFriend]
	@Id int
AS
	DELETE Friend
	WHERE Id = @Id
RETURN 0
