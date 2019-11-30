MERGE INTO [GopnikHotelDB].[dbo].[Kamer] AS Target
USING (values
	(1, 'Sachalin Suite', 2, 1000.00,'https://i.imgur.com/TLA8Dme.jpg'),
	(2, 'Trotsky Trove', 3, 2000.00,'https://i.imgur.com/YpuMWkr.jpg'),
	(3, 'Kamtsjatka Kamer', 5, 2500.00,'https://i.imgur.com/l0uiusK.jpg'),
	(4, 'Stalin Surprise', 4, 3500.00,'https://i.imgur.com/Fmtm3Lg.jpg')
) AS Source (KamerId, Naam, Grootte, Prijs, Afbeelding)
ON TARGET.KamerId = Source.KamerId
WHEN NOT MATCHED BY TARGET THEN
	INSERT (KamerId, Naam, Grootte, Prijs, Afbeelding)
	VALUES (KamerId, Naam, Grootte, Prijs, Afbeelding)
WHEN MATCHED THEN
	UPDATE SET
		KamerId = Source.KamerId,
		Naam= Source.Naam,
		Grootte = Source.Grootte, 
		Prijs = Source.Prijs,
		Afbeelding = Source.Afbeelding;