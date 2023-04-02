CREATE TABLE [dbo].[Friend] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [FirstName] VARCHAR (50)  NOT NULL,
    [LastName]  VARCHAR (50)  NOT NULL,
    [PhotoId]   VARCHAR (100) NULL,
    [Email]     VARCHAR (50)  NOT NULL,
    [Cellphone] VARCHAR (25)  NULL,
    [BirthDate] DATE          NULL,
    [CountryId] INT           NOT NULL,
    [StateId]   INT           NOT NULL
);


