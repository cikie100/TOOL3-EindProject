Use LaboProjectDB;
--Must haves :
--•Als   gebruiker   wil   ik een   lijst   van   straatIDs kunnen   opvragen voor   een   opgegeven gemeentenaam.
		SELECT straatId
		FROM dbo.Gemeente_straat gs
		JOIN dbo.Gemeente g ON g.gemeenteId = gs.gemeenteId
		WHERE gemeenteNaam ='Aalst';

--•Als  gebruiker  wil  ik  alle  straatnamen  van  een  gemeente  kunnen  opvragen  (alfabetisch gesorteerd).
		SELECT straatNaam
		FROM Straat s
		JOIN dbo.Gemeente_straat gs ON s.straatId= gs.straatId
		JOIN dbo.Gemeente g ON g.gemeenteId = gs.gemeenteId
		WHERE gemeenteNaam ='Aalst';

--•Als gebruiker wil ik een straat kunnen opvragen op basis van een meegegeven straatID.
--wat ik niet makkelijk kon doen in 1 keer, heb ik maar in 3 delen gedaan
-- geeft s.straatId, straatNaam, gemeenteNaam, provincienaam, s.GraafId
		SELECT DISTINCT  s.straatId, straatNaam, gemeenteNaam, provincienaam, s.GraafId

		FROM Straat s
		 JOIN Gemeente_straat gs ON s.straatId = gs.straatId
		 JOIN Gemeente g ON g.gemeenteId = gs.gemeenteId
		 JOIN Provincie_Gemeente pg ON pg.provincieID = g.gemeenteId
		 JOIN Provincie p ON p.provincieID = pg.provincieID

		  JOIN Graaf_Knoop gk ON s.GraafId = gk.GraafId
		  JOIN Knoop k ON k.knoopId = gk.knoopId
		  JOIN Knoop_Segment ks ON ks.knoopId = k.knoopId
		  JOIN Segment se ON se.SegmentId = ks.SegmentId
		  JOIN Punt pu ON pu.SegmId = se.SegmentId
		Where s.straatId = '15';

		-- geeft alle k.knoopId, k.puntX, k.puntY
		SELECT DISTINCT k.knoopId, k.puntX, k.puntY
		
		FROM Straat 
		JOIN graaf g ON g.GraafId = Straat.graafID
		JOIN Graaf_Knoop gk ON g.GraafId = gk.GraafId
		JOIN Knoop k ON k.knoopId = gk.knoopId
		JOIN Knoop_Segment ks ON ks.knoopId = k.knoopId
		JOIN Segment se ON se.SegmentId = ks.SegmentId
		JOIN Punt p ON p.SegmId = se.SegmentId
		Where Straat.straatId = '15';

		-- geeft k.knoopId,se.SegmentId, se.BeginKnoopId, se.EindKnoopId, p.puntX, p.puntY
		SELECT DISTINCT k.knoopId,se.SegmentId, se.BeginKnoopId, se.EindKnoopId, p.puntX, p.puntY
		
		FROM Straat s
		JOIN graaf g ON g.GraafId = s.graafID
		JOIN Graaf_Knoop gk ON g.GraafId = gk.GraafId
		JOIN Knoop k ON k.knoopId = gk.knoopId
		JOIN Knoop_Segment ks ON ks.knoopId = k.knoopId
		JOIN Segment se ON se.SegmentId = ks.SegmentId
		JOIN Punt p ON p.SegmId = se.SegmentId
		Where s.straatId = '15';



--•Als   gebruiker   wil   ik   een   straat   kunnen   opvragen   op   basis   van   de   straatnaam   en   de gemeentenaam.
--wat ik niet makkelijk kon doen in 1 keer, heb ik maar in 3 delen gedaa
SELECT DISTINCT  s.straatId, straatNaam, gemeenteNaam, provincienaam, s.GraafId

		FROM Straat s
		 JOIN Gemeente_straat gs ON s.straatId = gs.straatId
		 JOIN Gemeente g ON g.gemeenteId = gs.gemeenteId
		 JOIN Provincie_Gemeente pg ON pg.provincieID = g.gemeenteId
		 JOIN Provincie p ON p.provincieID = pg.provincieID

		  JOIN Graaf_Knoop gk ON s.GraafId = gk.GraafId
		  JOIN Knoop k ON k.knoopId = gk.knoopId
		  JOIN Knoop_Segment ks ON ks.knoopId = k.knoopId
		  JOIN Segment se ON se.SegmentId = ks.SegmentId
		  JOIN Punt pu ON pu.SegmId = se.SegmentId
		Where s.straatNaam = 'Lageweg' AND  g.gemeenteNaam = 'Antwerpen';

		-- geeft alle k.knoopId, k.puntX, k.puntY
		SELECT DISTINCT k.knoopId, k.puntX, k.puntY
		 
		FROM Straat s
		JOIN Gemeente_straat gs ON s.straatId = gs.straatId
		JOIN Gemeente g ON g.gemeenteId = gs.gemeenteId
		JOIN graaf gr ON gr.GraafId = s.graafID
		JOIN Graaf_Knoop gk ON gr.GraafId = gk.GraafId
		JOIN Knoop k ON k.knoopId = gk.knoopId
		JOIN Knoop_Segment ks ON ks.knoopId = k.knoopId
		JOIN Segment se ON se.SegmentId = ks.SegmentId
		JOIN Punt p ON p.SegmId = se.SegmentId
		Where s.straatNaam = 'Lageweg' AND  g.gemeenteNaam = 'Antwerpen';


		-- geeft k.knoopId,se.SegmentId, se.BeginKnoopId, se.EindKnoopId, p.puntX, p.puntY
		SELECT DISTINCT k.knoopId,se.SegmentId, se.BeginKnoopId, se.EindKnoopId, p.puntX, p.puntY
		
		FROM Straat s
		JOIN Gemeente_straat gs ON s.straatId = gs.straatId
		JOIN Gemeente g ON g.gemeenteId = gs.gemeenteId
		JOIN graaf gr ON gr.GraafId = s.graafID
		JOIN Graaf_Knoop gk ON gr.GraafId = gk.GraafId
		JOIN Knoop k ON k.knoopId = gk.knoopId
		JOIN Knoop_Segment ks ON ks.knoopId = k.knoopId
		JOIN Segment se ON se.SegmentId = ks.SegmentId
		JOIN Punt p ON p.SegmId = se.SegmentId
		Where s.straatNaam = 'Lageweg' AND  g.gemeenteNaam = 'Antwerpen';


--•Als  gebruiker  wil  ik  alle  straten  kunnen  opvragen  die  grenzen  aan  een  opgegeven  straat (straatID).
SELECT DISTINCT  s.straatId, straatNaam, gemeenteNaam, provincienaam, s.GraafId

		FROM Straat s
		 JOIN Gemeente_straat gs ON s.straatId = gs.straatId
		 JOIN Gemeente g ON g.gemeenteId = gs.gemeenteId
		 JOIN Provincie_Gemeente pg ON pg.provincieID = g.gemeenteId
		 JOIN Provincie p ON p.provincieID = pg.provincieID

		  JOIN Graaf_Knoop gk ON s.GraafId = gk.GraafId
		  JOIN Knoop k ON k.knoopId = gk.knoopId
		  JOIN Knoop_Segment ks ON ks.knoopId = k.knoopId
		  JOIN Segment se ON se.SegmentId = ks.SegmentId
		  JOIN Punt pu ON pu.SegmId = se.SegmentId
		Where se.linksStraatnaamID = 3 OR  se.rechtsStraatnaamID = 3;

	--voor de knopen en segmenten te vullen gebruik ik een for(elke straat gevonden hierboven) en herbruik id de query die op straatId een straat opvroeg
