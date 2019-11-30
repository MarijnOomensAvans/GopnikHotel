CREATE TABLE [dbo].[Kamer]
(
	[KamerId] INT NOT NULL PRIMARY KEY, 
    [Naam] VARCHAR(255) NOT NULL, 
    [Grootte] INT NOT NULL, 
    [Prijs] DECIMAL(18, 2) NOT NULL, 
    [Afbeelding] VARCHAR(255) NOT NULL
)
