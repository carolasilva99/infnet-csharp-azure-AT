CREATE PROCEDURE [dbo].[UpdateState]
	@Id int,
	@Name varchar(100),
	@PhotoId varchar(255),
	@CountryId int
AS
	UPDATE State SET
	Name = @Name,
	PhotoId = @PhotoId,
	CountryId = @CountryId
	WHERE Id = @Id
RETURN 0
