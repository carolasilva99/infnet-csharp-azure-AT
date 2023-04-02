CREATE PROCEDURE [dbo].[GetFriends]
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
RETURN 0
