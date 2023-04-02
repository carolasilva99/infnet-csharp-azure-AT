CREATE PROCEDURE [dbo].[CreateFriend]
	@FirstName varchar(50),
	@LastName varchar(50),
	@PhotoId varchar(100),
	@Email varchar(50),
	@Cellphone varchar(50),
	@Birthdate date,
	@CountryId int,
	@StateId int
AS
	INSERT INTO Friend(
						FirstName,
						LastName,
						PhotoId,
						Email,
						Cellphone,
						BirthDate,
						CountryId,
						StateId
						)
	VALUES (
		@FirstName,
		@LastName,
		@PhotoId,
		@Email,
		@Cellphone,
		@Birthdate,
		@CountryId,
		@StateId
		)
RETURN 0
