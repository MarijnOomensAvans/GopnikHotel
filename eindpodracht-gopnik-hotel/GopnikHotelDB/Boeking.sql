CREATE TABLE [dbo].[Boeking]
(
	[BoekingId] INT NOT NULL, 
    [Datum] DATE NOT NULL, 
    [IdKamer] INT NOT NULL, 
    [IdKlant] INT NOT NULL,
	CONSTRAINT [PK_Boeking] PRIMARY KEY CLUSTERED ([IdKlant] ASC, [IdKamer] ASC, [BoekingId] ASC),
	CONSTRAINT [FK_Boeking_ToTable] FOREIGN KEY ([IdKamer]) REFERENCES [dbo].[Kamer] ([KamerId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Boeking_ToTable_1] FOREIGN KEY ([IdKlant]) REFERENCES [dbo].[Klant] ([KlantId]) ON DELETE CASCADE
)
