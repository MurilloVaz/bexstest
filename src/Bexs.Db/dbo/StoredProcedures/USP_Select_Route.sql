CREATE PROCEDURE USP_Select_Route
AS
SELECT 
	R.Id, 
	R.IdRouteEntryFrom AS 'From', 
	RE_FROM.Code AS FromCode,
	R.IdRouteEntryTo AS 'To',
	RE_TO.Code AS ToCode,
	R.Price 
FROM Route R
	INNER JOIN RouteEntry RE_FROM 
		ON RE_FROM.Id = R.IdRouteEntryFrom
	INNER JOIN RouteEntry RE_TO 
		ON RE_TO.Id = R.IdRouteEntryTo
WHERE R.Active = 1
ORDER BY R.Price DESC