CREATE PROCEDURE [dbo].[RemoveFromMyFriendsList]
	@Id int,
	@OldFriendId int
AS
	DELETE Friends
	WHERE FriendId = @Id AND MyFriendId = @OldFriendId
RETURN 0
