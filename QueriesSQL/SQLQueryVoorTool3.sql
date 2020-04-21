Use LaboProjectDB;
--Must haves :
--•Als   gebruiker   wil   ik een   lijst   van   straatIDs kunnen   opvragen voor   een   opgegeven gemeentenaam.
SELECT straatId
FROM dbo.Gemeente_straat gs
JOIN dbo.Gemeente g ON g.gemeenteId = gs.gemeenteId
WHERE gemeenteNaam ='Aalst';

--•Als gebruiker wil ik een straat kunnen opvragen op basis van een meegegeven straatID.


--•Als   gebruiker   wil   ik   een   straat   kunnen   opvragen   op   basis   van   de   straatnaam   en   de gemeentenaam.


--•Als  gebruiker  wil  ik  alle  straatnamen  van  een  gemeente  kunnen  opvragen  (alfabetisch gesorteerd).
SELECT straatNaam
FROM Straat s
JOIN dbo.Gemeente_straat gs ON s.straatId= gs.straatId
JOIN dbo.Gemeente g ON g.gemeenteId = gs.gemeenteId
WHERE gemeenteNaam ='Aalst';