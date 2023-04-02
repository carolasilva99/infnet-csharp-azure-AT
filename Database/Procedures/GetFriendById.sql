CREATE PROCEDURE [dbo].[GetFriendById]
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
	WHERE Id = @Id
RETURN 0
