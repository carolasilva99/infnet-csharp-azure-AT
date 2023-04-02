CREATE PROCEDURE [dbo].[CreateState]
	@Name varchar(100),
	@PhotoId varchar(255),
	@CountryId int
AS
	INSERT INTO State(Name, PhotoId, CountryId)
	VALUES (@Name, @PhotoId, @CountryId)
RETURN 0
