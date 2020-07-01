CREATE PROCEDURE USP_Start_SSISExecution
@package varchar(100)
AS
DECLARE @execution_id BIGINT
EXEC [SSISDB].[catalog].[create_execution]  @execution_id=@execution_id OUTPUT, @folder_name=N'SSIS Packages', @project_name=N'Bexs.ETL', @package_name=@package
SELECT @execution_id