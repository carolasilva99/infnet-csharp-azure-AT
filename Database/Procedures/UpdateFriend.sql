CREATE PROCEDURE [dbo].[UpdateFriend]
	@Id int,
	@FirstName varchar(50),
	@LastName varchar(50),
	@PhotoId varchar(100),
	@Email varchar(50),
	@Cellphone varchar(50),
	@Birthdate date,
	@CountryId int,
	@StateId int
AS
	UPDATE Friend SET
		FirstName = @FirstName,
		LastName = @LastName,
		PhotoId = @PhotoId,
		Email = @Email,
		Cellphone = @Cellphone,
		BirthDate = @BirthDate,
		CountryId = @CountryId,
		StateId = @CountryId
	WHERE Id = @Id
RETURN 0
