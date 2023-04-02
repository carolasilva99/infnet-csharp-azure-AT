CREATE PROCEDURE [dbo].[AddToMyFriendsList]
	@Id int,
	@NewFriendId int
AS
	INSERT INTO Friends (FriendId, MyFriendId)
	VALUES (@Id, @NewFriendId)
RETURN 0
