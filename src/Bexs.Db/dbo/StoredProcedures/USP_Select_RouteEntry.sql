CREATE PROCEDURE USP_Select_RouteEntry
AS
SELECT 
	RE.Id,
	RE.Code
FROM RouteEntry RE
WHERE RE.Active = 1