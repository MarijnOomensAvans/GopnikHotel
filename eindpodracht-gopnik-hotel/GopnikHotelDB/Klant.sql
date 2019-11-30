CREATE TABLE [dbo].[Klant]
(
	[KlantId] INT NOT NULL PRIMARY KEY, 
    [Naam] VARCHAR(255) NOT NULL, 
    [Mail] VARCHAR(255) NULL, 
    [Postcode] VARCHAR(6) NOT NULL, 
    [Woonplaats] VARCHAR(255) NOT NULL, 
    [Straat] VARCHAR(255) NOT NULL, 
    [Huisnummer] INT NOT NULL, 
    [Toevoeging] VARCHAR(1) NULL
)
