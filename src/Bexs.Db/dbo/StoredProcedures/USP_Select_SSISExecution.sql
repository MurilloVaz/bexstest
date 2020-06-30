CREATE PROCEDURE USP_Select_SSISExecution
@package varchar(100)
AS
SELECT TOP 1 E.Execution_Id FROM SSISDB.catalog.executions E WHERE E.Status = 2 AND E.Package_Name = @package