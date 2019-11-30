MERGE INTO [GopnikHotelDB].[dbo].[Klant] AS Target
USING (values
	(1, 'Andrenov Amerov', 'amerov@yandexmail.ru', '636783','Strezhevoy','Ulitsa Vykhodtseva', 8, 'a'),
	(2, 'Petrov Polmarov', 'polmarov@yandexmail.ru', '665717','Bratsk','Ulitsa Kirova', 86, NULL),
	(3, 'Zareyev Zazov', 'zazov@yandexmail.ru', '677008','Yakutsk','Ulitsa Safronova', 50, NULL),
	(4, 'Marin Omensk', 'marin@yandexmail.ru', '5247XA','Rosmalen','Anne Frankstraat', 17, NULL)
) AS Source (KlantId, Naam, Mail, Postcode, Woonplaats, Straat, Huisnummer, Toevoeging)
ON TARGET.KlantId = Source.KlantId
WHEN NOT MATCHED BY TARGET THEN
	INSERT (KlantId, Naam, Mail, Postcode, Woonplaats, Straat, Huisnummer, Toevoeging)
	VALUES (KlantId, Naam, Mail, Postcode, Woonplaats, Straat, Huisnummer, Toevoeging)
WHEN MATCHED THEN
	UPDATE SET
		KlantId = Source.KlantId,
		Naam= Source.Naam,
		Mail = Source.Mail, 
		Postcode = Source.Postcode,
		Woonplaats = Source.Woonplaats,
		Straat = Source.Straat,
		Huisnummer = Source.Huisnummer,
		Toevoeging = Source.Toevoeging;