CREATE PROCEDURE [dbo].[CreateCountry]
	@Name varchar(255),
	@PhotoId varchar(255)
AS
	INSERT INTO Country (Name, PhotoId)
	VALUES(@Name, @PhotoId)
RETURN 0
