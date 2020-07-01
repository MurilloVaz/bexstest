CREATE PROCEDURE USP_Insert_Route
@fromCode VARCHAR(10),
@toCode VARCHAR(10),
@price decimal(19,4)
AS

DECLARE @fromId INT = (SELECT TOP 1 Id FROM RouteEntry WHERE Code = @fromCode)
DECLARE @toId INT = (SELECT TOP 1 Id FROM RouteEntry WHERE Code = @toCode)

IF(@fromId IS NULL) BEGIN
	INSERT INTO RouteEntry (Code) VALUES (@fromCode) SET @fromId = @@IDENTITY
END

IF(@toId IS NULL) BEGIN
	INSERT INTO RouteEntry (Code) VALUES (@toCode) SET @toId = @@IDENTITY
END

IF((SELECT TOP 1 Id FROM Route R WHERE R.IdRouteEntryFrom = @fromId AND R.IdRouteEntryTo = @toId) IS NOT NULL) BEGIN
	SELECT 0
END ELSE BEGIN
	INSERT INTO Route (IdRouteEntryFrom, IdRouteEntryTo, Price) VALUES (@fromId, @toId, @price) SELECT @@IDENTITY
END