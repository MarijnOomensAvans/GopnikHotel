MERGE INTO [GopnikHotelDB].[dbo].[Boeking] AS Target
USING (values
	(1, '20190713', 2, 1)
) AS Source (BoekingId, Datum, IdKamer, IdKlant)
ON TARGET.BoekingId = Source.BoekingId
WHEN NOT MATCHED BY TARGET THEN
	INSERT (BoekingId, Datum, IdKamer, IdKlant)
	VALUES (BoekingId, Datum, IdKamer, IdKlant)
WHEN MATCHED THEN
	UPDATE SET
		BoekingId = Source.BoekingId,
		Datum = Source.Datum,
		IdKamer = Source.IdKamer, 
		IdKlant = Source.IdKlant;