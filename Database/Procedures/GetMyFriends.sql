CREATE PROCEDURE [dbo].[GetMyFriends]
	@Id int
AS
	SELECT 
		Id,
		FirstName,
		LastName,
		PhotoId,
		Email,
		Cellphone,
		BirthDate,
		CountryId,
		StateId
	FROM Friend
	INNER JOIN Friends ON Friends.FriendId = @Id
RETURN 0
