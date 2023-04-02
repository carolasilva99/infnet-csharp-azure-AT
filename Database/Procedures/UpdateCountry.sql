CREATE PROCEDURE [dbo].[UpdateCountry]
	@Id int,
	@Name varchar(255),
	@PhotoId varchar(255)
AS
	UPDATE Country SET
	Name = @Name,
	PhotoId = @PhotoId
	WHERE Id = @Id
RETURN 0
