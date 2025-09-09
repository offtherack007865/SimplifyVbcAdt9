-- SQL Server Instance:  smg-sql01
IF (@@SERVERNAME <> 'smg-sql01')
BEGIN
PRINT 'Invalid SQL Server Connection'
RETURN
END

USE [Utilities];
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('adt.qy_GetHumanaConfig'))
   DROP PROC [adt].[qy_GetHumanaConfig]
GO
CREATE PROCEDURE [adt].[qy_GetHumanaConfig]
/* -----------------------------------------------------------------------------------------------------------
   Procedure Name  :  qy_GetHumanaConfig
   Business Analyis:
   Project/Process :   
   Description     :  Get configuration settings for the 
                      'Humana ADT' application.
	  
   Author          :  Philip Morrison 
   Create Date     :  9/3/2025

   ***********************************************************************************************************
   **         Change History                                                                                **
   ***********************************************************************************************************

   Date       Version    Author             Description
   --------   --------   -----------        ------------
   9/3/2025   1.01.001   Philip Morrison    Created

*/ -----------------------------------------------------------------------------------------------------------                                   

AS
BEGIN

  -- KeyValueTable
  DECLARE @KeyValueTable Table
  (
    [Name] [nvarchar] (1000)
	,[Value] [nvarchar] (1000)
  );
  
  DECLARE @ReadDirectory [nvarchar] (1000) = ''
  DECLARE @InputFilenameContainsString [nvarchar] (1000) = ''
  DECLARE @LineToLookForSearchString [nvarchar] (1000) = ''
  DECLARE @SearchString [nvarchar] (1000) = ''
  DECLARE @InputFileArchiveDirectory [nvarchar] (1000) = ''
  DECLARE @OutputFileToSummitAdtProcessingReadDirectory [nvarchar] (1000) = ''
  DECLARE @OutputFileToAthenaReadDirectory [nvarchar] (1000) = ''
  DECLARE @OutputFileToLightbeamReadDirectory [nvarchar] (1000) = ''
  DECLARE @OutputFileToSummitAdtProcessingArchiveDirectory [nvarchar] (1000) = ''
  DECLARE @OutputFileToAthenaArchiveDirectory [nvarchar] (1000) = ''
  DECLARE @OutputFileToLightbeamArchiveDirectory [nvarchar] (1000) = ''
  DECLARE @SimplifyVbcAdtBaseWebApiUrl [nvarchar] (1000) = ''
  DECLARE @ExcelTemplateFullFilename [nvarchar] (1000) = ''
  DECLARE @ImportArchiveFolder [nvarchar] (1000) = ''
  DECLARE @BulkInsertConnectionString [nvarchar] (1000) = ''
  DECLARE @BulkInsertDbName [nvarchar] (1000) = ''
  DECLARE @BulkInsertDbTableName [nvarchar] (1000) = ''
  DECLARE @BulkInsertBaseWebApiUrl [nvarchar] (1000) = ''
  DECLARE @EmailBaseWebApiUrl [nvarchar] (1000) = ''
  DECLARE @EmailFromAddress [nvarchar] (1000) = ''
  DECLARE @Emailees [nvarchar] (1000) = ''

-- Template Declarations
DECLARE @Application            varchar(128) = 'SimplifyVbcAdt' 
DECLARE @Version                varchar(25)  = '1.00.001'

DECLARE @ProcessID              int          = 0
DECLARE @Process                varchar(128) = 'Humana'

DECLARE @BatchOutID             int
DECLARE @BatchDescription       varchar(1000) = @@ServerName + '  - ' + @Version
DECLARE @BatchDetailDescription varchar(1000)
DECLARE @BatchMessage           varchar(MAX)
DECLARE @User                   varchar(128) = SUSER_NAME()

DECLARE @AnticipatedRecordCount int 
DECLARE @ActualRecordCount      int

SET NOCOUNT ON

BEGIN TRY

--  Initialize Batch
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  NULL, 'BatchStart', @BatchDescription, @ProcessID, @Process
----------------------------------------------------------------------------------------------------------------------------------------------------

    SET @BatchDetailDescription = '010/230:  Populate KeyValueTable with call to [administration].[qy_GetApplicationSettings]'
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailStart', @BatchDetailDescription
	
	  SELECT @AnticipatedRecordCount = COUNT(*)
	                                   FROM @KeyValueTable;
	  
      -- Populate KeyValueTable with call to [administration].[qy_GetApplicationSettings]
      INSERT INTO @KeyValueTable
      (
        [Name]
	    ,[Value]
      )
      EXEC [Admin].[Utilities].[administration].[qy_GetApplicationSettings] 'SimplifyVbcAdt', 'Default', 'Humana', NULL, 'AppUser';
	
    SET @ActualRecordCount = @@ROWCOUNT
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailEnd', NULL, NULL, NULL, @AnticipatedRecordCount, @ActualRecordCount

----------------------------------------------------------------------------------------------------------------------------------------------------
/*	
    [ReadDirectory]
*/
    SET @BatchDetailDescription = '020/230:  Populate @ReadDirectory'
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailStart', @BatchDetailDescription
	
	  SELECT @AnticipatedRecordCount = COUNT(*)
      FROM @KeyValueTable
      WHERE [Name] = 'ReadDirectory';	
	  
      -- Populate @ReadDirectory
      SELECT @ReadDirectory = [Value]
      FROM @KeyValueTable
      WHERE [Name] = 'ReadDirectory';	
	
    SET @ActualRecordCount = @@ROWCOUNT
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailEnd', NULL, NULL, NULL, @AnticipatedRecordCount, @ActualRecordCount

----------------------------------------------------------------------------------------------------------------------------------------------------
/*	
    [InputFilenameContainsString]
*/
    SET @BatchDetailDescription = '030/230:  Populate @InputFilenameContainsString'
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailStart', @BatchDetailDescription
	
	  SELECT @AnticipatedRecordCount = COUNT(*)
      FROM @KeyValueTable
      WHERE [Name] = 'InputFilenameContainsString';	
	  
      -- Populate @InputFilenameContainsString
      SELECT @InputFilenameContainsString = [Value]
      FROM @KeyValueTable
      WHERE [Name] = 'InputFilenameContainsString';	
	
    SET @ActualRecordCount = @@ROWCOUNT
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailEnd', NULL, NULL, NULL, @AnticipatedRecordCount, @ActualRecordCount

----------------------------------------------------------------------------------------------------------------------------------------------------
/*	
    [LineToLookForSearchString]
*/
    SET @BatchDetailDescription = '040/230:  Populate @LineToLookForSearchString'
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailStart', @BatchDetailDescription
	
	  SELECT @AnticipatedRecordCount = COUNT(*)
      FROM @KeyValueTable
      WHERE [Name] = 'LineToLookForSearchString';	
	  
      -- Populate @LineToLookForSearchString
      SELECT @LineToLookForSearchString = [Value]
      FROM @KeyValueTable
      WHERE [Name] = 'LineToLookForSearchString';	
	
    SET @ActualRecordCount = @@ROWCOUNT
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailEnd', NULL, NULL, NULL, @AnticipatedRecordCount, @ActualRecordCount

----------------------------------------------------------------------------------------------------------------------------------------------------
/*	
    [SearchString]
*/
    SET @BatchDetailDescription = '050/230:  Populate @SearchString'
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailStart', @BatchDetailDescription
	
	  SELECT @AnticipatedRecordCount = COUNT(*)
      FROM @KeyValueTable
      WHERE [Name] = 'SearchString';	
	  
      -- Populate @SearchString
      SELECT @SearchString = [Value]
      FROM @KeyValueTable
      WHERE [Name] = 'SearchString';	
	
    SET @ActualRecordCount = @@ROWCOUNT
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailEnd', NULL, NULL, NULL, @AnticipatedRecordCount, @ActualRecordCount

----------------------------------------------------------------------------------------------------------------------------------------------------
/*	
    [InputFileArchiveDirectory]
*/
    SET @BatchDetailDescription = '060/230:  Populate @InputFileArchiveDirectory'
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailStart', @BatchDetailDescription
	
	  SELECT @AnticipatedRecordCount = COUNT(*)
      FROM @KeyValueTable
      WHERE [Name] = 'InputFileArchiveDirectory';	
	  
      -- Populate @InputFileArchiveDirectory
      SELECT @InputFileArchiveDirectory = [Value]
      FROM @KeyValueTable
      WHERE [Name] = 'InputFileArchiveDirectory';	
	
    SET @ActualRecordCount = @@ROWCOUNT
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailEnd', NULL, NULL, NULL, @AnticipatedRecordCount, @ActualRecordCount

----------------------------------------------------------------------------------------------------------------------------------------------------
/*	
    [OutputFileToSummitAdtProcessingReadDirectory]
*/
    SET @BatchDetailDescription = '070/230:  Populate @OutputFileToSummitAdtProcessingReadDirectory'
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailStart', @BatchDetailDescription
	
	  SELECT @AnticipatedRecordCount = COUNT(*)
      FROM @KeyValueTable
      WHERE [Name] = 'OutputFileToSummitAdtProcessingReadDirectory';	
	  
      -- Populate @OutputFileToSummitAdtProcessingReadDirectory
      SELECT @OutputFileToSummitAdtProcessingReadDirectory = [Value]
      FROM @KeyValueTable
      WHERE [Name] = 'OutputFileToSummitAdtProcessingReadDirectory';	
	
    SET @ActualRecordCount = @@ROWCOUNT
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailEnd', NULL, NULL, NULL, @AnticipatedRecordCount, @ActualRecordCount

----------------------------------------------------------------------------------------------------------------------------------------------------
/*	
    [OutputFileToAthenaReadDirectory]
*/
    SET @BatchDetailDescription = '080/230:  Populate @OutputFileToAthenaReadDirectory'
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailStart', @BatchDetailDescription
	
	  SELECT @AnticipatedRecordCount = COUNT(*)
      FROM @KeyValueTable
      WHERE [Name] = 'OutputFileToAthenaReadDirectory';	
	  
      -- Populate @OutputFileToAthenaReadDirectory
      SELECT @OutputFileToAthenaReadDirectory = [Value]
      FROM @KeyValueTable
      WHERE [Name] = 'OutputFileToAthenaReadDirectory';	
	
    SET @ActualRecordCount = @@ROWCOUNT
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailEnd', NULL, NULL, NULL, @AnticipatedRecordCount, @ActualRecordCount

----------------------------------------------------------------------------------------------------------------------------------------------------
/*	
    [OutputFileToLightbeamReadDirectory]
*/
    SET @BatchDetailDescription = '090/230:  Populate @OutputFileToLightbeamReadDirectory'
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailStart', @BatchDetailDescription
	
	  SELECT @AnticipatedRecordCount = COUNT(*)
      FROM @KeyValueTable
      WHERE [Name] = 'OutputFileToLightbeamReadDirectory';	
	  
      -- Populate @OutputFileToLightbeamReadDirectory
      SELECT @OutputFileToLightbeamReadDirectory = [Value]
      FROM @KeyValueTable
      WHERE [Name] = 'OutputFileToLightbeamReadDirectory';	
	
    SET @ActualRecordCount = @@ROWCOUNT
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailEnd', NULL, NULL, NULL, @AnticipatedRecordCount, @ActualRecordCount

----------------------------------------------------------------------------------------------------------------------------------------------------
/*	
    [OutputFileToSummitAdtProcessingArchiveDirectory]
*/
    SET @BatchDetailDescription = '100/230:  Populate @OutputFileToSummitAdtProcessingArchiveDirectory'
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailStart', @BatchDetailDescription
	
	  SELECT @AnticipatedRecordCount = COUNT(*)
      FROM @KeyValueTable
      WHERE [Name] = 'OutputFileToSummitAdtProcessingArchiveDirectory';	
	  
      -- Populate @OutputFileToSummitAdtProcessingArchiveDirectory
      SELECT @OutputFileToSummitAdtProcessingArchiveDirectory = [Value]
      FROM @KeyValueTable
      WHERE [Name] = 'OutputFileToSummitAdtProcessingArchiveDirectory';	
	
    SET @ActualRecordCount = @@ROWCOUNT
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailEnd', NULL, NULL, NULL, @AnticipatedRecordCount, @ActualRecordCount

----------------------------------------------------------------------------------------------------------------------------------------------------
/*	
    [OutputFileToAthenaArchiveDirectory]
*/
    SET @BatchDetailDescription = '110/230:  Populate @OutputFileToAthenaArchiveDirectory'
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailStart', @BatchDetailDescription
	
	  SELECT @AnticipatedRecordCount = COUNT(*)
      FROM @KeyValueTable
      WHERE [Name] = 'OutputFileToAthenaArchiveDirectory';	
	  
      -- Populate @OutputFileToAthenaArchiveDirectory
      SELECT @OutputFileToAthenaArchiveDirectory = [Value]
      FROM @KeyValueTable
      WHERE [Name] = 'OutputFileToAthenaArchiveDirectory';	
	
    SET @ActualRecordCount = @@ROWCOUNT
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailEnd', NULL, NULL, NULL, @AnticipatedRecordCount, @ActualRecordCount

----------------------------------------------------------------------------------------------------------------------------------------------------
/*	
    [OutputFileToLightbeamArchiveDirectory]
*/
    SET @BatchDetailDescription = '120/230:  Populate @OutputFileToLightbeamArchiveDirectory'
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailStart', @BatchDetailDescription
	
	  SELECT @AnticipatedRecordCount = COUNT(*)
      FROM @KeyValueTable
      WHERE [Name] = 'OutputFileToLightbeamArchiveDirectory';	
	  
      -- Populate @OutputFileToLightbeamArchiveDirectory
      SELECT @OutputFileToLightbeamArchiveDirectory = [Value]
      FROM @KeyValueTable
      WHERE [Name] = 'OutputFileToLightbeamArchiveDirectory';	
	
    SET @ActualRecordCount = @@ROWCOUNT
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailEnd', NULL, NULL, NULL, @AnticipatedRecordCount, @ActualRecordCount

----------------------------------------------------------------------------------------------------------------------------------------------------
/*	
    [SimplifyVbcAdtBaseWebApiUrl]
*/
    SET @BatchDetailDescription = '130/230:  Populate @SimplifyVbcAdtBaseWebApiUrl'
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailStart', @BatchDetailDescription
	
	  SELECT @AnticipatedRecordCount = COUNT(*)
      FROM @KeyValueTable
      WHERE [Name] = 'SimplifyVbcAdtBaseWebApiUrl';	
	  
      -- Populate @SimplifyVbcAdtBaseWebApiUrl
      SELECT @SimplifyVbcAdtBaseWebApiUrl = [Value]
      FROM @KeyValueTable
      WHERE [Name] = 'SimplifyVbcAdtBaseWebApiUrl';	
	
    SET @ActualRecordCount = @@ROWCOUNT
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailEnd', NULL, NULL, NULL, @AnticipatedRecordCount, @ActualRecordCount

----------------------------------------------------------------------------------------------------------------------------------------------------
/*	
    [ExcelTemplateFullFilename]
*/
    SET @BatchDetailDescription = '140/230:  Populate @ExcelTemplateFullFilename'
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailStart', @BatchDetailDescription
	
	  SELECT @AnticipatedRecordCount = COUNT(*)
      FROM @KeyValueTable
      WHERE [Name] = 'ExcelTemplateFullFilename';	
	  
      -- Populate @ExcelTemplateFullFilename
      SELECT @ExcelTemplateFullFilename = [Value]
      FROM @KeyValueTable
      WHERE [Name] = 'ExcelTemplateFullFilename';	
	
    SET @ActualRecordCount = @@ROWCOUNT
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailEnd', NULL, NULL, NULL, @AnticipatedRecordCount, @ActualRecordCount

----------------------------------------------------------------------------------------------------------------------------------------------------
/*	
    [ImportArchiveFolder]
*/
    SET @BatchDetailDescription = '150/230:  Populate @ImportArchiveFolder'
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailStart', @BatchDetailDescription
	
	  SELECT @AnticipatedRecordCount = COUNT(*)
      FROM @KeyValueTable
      WHERE [Name] = 'ImportArchiveFolder';	
	  
      -- Populate @ImportArchiveFolder
      SELECT @ImportArchiveFolder = [Value]
      FROM @KeyValueTable
      WHERE [Name] = 'ImportArchiveFolder';	
	
    SET @ActualRecordCount = @@ROWCOUNT
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailEnd', NULL, NULL, NULL, @AnticipatedRecordCount, @ActualRecordCount

----------------------------------------------------------------------------------------------------------------------------------------------------
/*	
    [BulkInsertConnectionString]
*/
    SET @BatchDetailDescription = '160/230:  Populate @BulkInsertConnectionString'
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailStart', @BatchDetailDescription
	
	  SELECT @AnticipatedRecordCount = COUNT(*)
      FROM @KeyValueTable
      WHERE [Name] = 'BulkInsertConnectionString';	
	  
      -- Populate @BulkInsertConnectionString
      SELECT @BulkInsertConnectionString = [Value]
      FROM @KeyValueTable
      WHERE [Name] = 'BulkInsertConnectionString';	
	
    SET @ActualRecordCount = @@ROWCOUNT
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailEnd', NULL, NULL, NULL, @AnticipatedRecordCount, @ActualRecordCount

----------------------------------------------------------------------------------------------------------------------------------------------------
/*	
    [BulkInsertDbName]
*/
    SET @BatchDetailDescription = '170/230:  Populate @BulkInsertDbName'
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailStart', @BatchDetailDescription
	
	  SELECT @AnticipatedRecordCount = COUNT(*)
      FROM @KeyValueTable
      WHERE [Name] = 'BulkInsertDbName';	
	  
      -- Populate @BulkInsertDbName
      SELECT @BulkInsertDbName = [Value]
      FROM @KeyValueTable
      WHERE [Name] = 'BulkInsertDbName';	
	
    SET @ActualRecordCount = @@ROWCOUNT
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailEnd', NULL, NULL, NULL, @AnticipatedRecordCount, @ActualRecordCount

----------------------------------------------------------------------------------------------------------------------------------------------------
/*	
    [BulkInsertDbTableName]
*/
    SET @BatchDetailDescription = '180/230:  Populate @BulkInsertDbTableName'
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailStart', @BatchDetailDescription
	
	  SELECT @AnticipatedRecordCount = COUNT(*)
      FROM @KeyValueTable
      WHERE [Name] = 'BulkInsertDbTableName';	
	  
      -- Populate @BulkInsertDbTableName
      SELECT @BulkInsertDbTableName = [Value]
      FROM @KeyValueTable
      WHERE [Name] = 'BulkInsertDbTableName';	
	
    SET @ActualRecordCount = @@ROWCOUNT
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailEnd', NULL, NULL, NULL, @AnticipatedRecordCount, @ActualRecordCount

----------------------------------------------------------------------------------------------------------------------------------------------------
/*	
    [BulkInsertBaseWebApiUrl]
*/
    SET @BatchDetailDescription = '190/230:  Populate @BulkInsertBaseWebApiUrl'
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailStart', @BatchDetailDescription
	
	  SELECT @AnticipatedRecordCount = COUNT(*)
      FROM @KeyValueTable
      WHERE [Name] = 'BulkInsertBaseWebApiUrl';	
	  
      -- Populate @BulkInsertBaseWebApiUrl
      SELECT @BulkInsertBaseWebApiUrl = [Value]
      FROM @KeyValueTable
      WHERE [Name] = 'BulkInsertBaseWebApiUrl';	
	
    SET @ActualRecordCount = @@ROWCOUNT
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailEnd', NULL, NULL, NULL, @AnticipatedRecordCount, @ActualRecordCount

----------------------------------------------------------------------------------------------------------------------------------------------------
/*	
    [EmailBaseWebApiUrl]
*/
    SET @BatchDetailDescription = '200/230:  Populate @EmailBaseWebApiUrl'
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailStart', @BatchDetailDescription
	
	  SELECT @AnticipatedRecordCount = COUNT(*)
      FROM @KeyValueTable
      WHERE [Name] = 'EmailBaseWebApiUrl';	
	  
      -- Populate @EmailBaseWebApiUrl
      SELECT @EmailBaseWebApiUrl = [Value]
      FROM @KeyValueTable
      WHERE [Name] = 'EmailBaseWebApiUrl';	
	
    SET @ActualRecordCount = @@ROWCOUNT
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailEnd', NULL, NULL, NULL, @AnticipatedRecordCount, @ActualRecordCount

----------------------------------------------------------------------------------------------------------------------------------------------------
/*	
    [EmailFromAddress]
*/
    SET @BatchDetailDescription = '210/230:  Populate @EmailFromAddress'
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailStart', @BatchDetailDescription
	
	  SELECT @AnticipatedRecordCount = COUNT(*)
      FROM @KeyValueTable
      WHERE [Name] = 'EmailFromAddress';	
	  
      -- Populate @EmailFromAddress
      SELECT @EmailFromAddress = [Value]
      FROM @KeyValueTable
      WHERE [Name] = 'EmailFromAddress';	
	
    SET @ActualRecordCount = @@ROWCOUNT
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailEnd', NULL, NULL, NULL, @AnticipatedRecordCount, @ActualRecordCount

----------------------------------------------------------------------------------------------------------------------------------------------------
/*	
    [Emailees]
*/
    SET @BatchDetailDescription = '220/230:  Populate @Emailees'
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailStart', @BatchDetailDescription
	
	  SELECT @AnticipatedRecordCount = COUNT(*)
      FROM @KeyValueTable
      WHERE [Name] = 'Emailees';	
	  
      -- Populate @Emailees
      SELECT @Emailees = [Value]
      FROM @KeyValueTable
      WHERE [Name] = 'Emailees';	
	
    SET @ActualRecordCount = @@ROWCOUNT
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailEnd', NULL, NULL, NULL, @AnticipatedRecordCount, @ActualRecordCount

----------------------------------------------------------------------------------------------------------------------------------------------------
/*	
  DECLARE @ReadDirectory [nvarchar] (1000) = ''
  DECLARE @InputFilenameContainsString [nvarchar] (1000) = ''
  DECLARE @LineToLookForSearchString [nvarchar] (1000) = ''
  DECLARE @SearchString [nvarchar] (1000) = ''
  DECLARE @InputFileArchiveDirectory [nvarchar] (1000) = ''
  DECLARE @OutputFileToSummitAdtProcessingReadDirectory [nvarchar] (1000) = ''
  DECLARE @OutputFileToAthenaReadDirectory [nvarchar] (1000) = ''
  DECLARE @OutputFileToLightbeamReadDirectory [nvarchar] (1000) = ''
  DECLARE @OutputFileToSummitAdtProcessingArchiveDirectory [nvarchar] (1000) = ''
  DECLARE @OutputFileToAthenaArchiveDirectory [nvarchar] (1000) = ''
  DECLARE @OutputFileToLightbeamArchiveDirectory [nvarchar] (1000) = ''
  DECLARE @SimplifyVbcAdtBaseWebApiUrl [nvarchar] (1000) = ''
  DECLARE @ExcelTemplateFullFilename [nvarchar] (1000) = ''
  DECLARE @ImportArchiveFolder [nvarchar] (1000) = ''
  DECLARE @BulkInsertConnectionString [nvarchar] (1000) = ''
  DECLARE @BulkInsertDbName [nvarchar] (1000) = ''
  DECLARE @BulkInsertDbTableName [nvarchar] (1000) = ''
  DECLARE @BulkInsertBaseWebApiUrl [nvarchar] (1000) = ''
  DECLARE @EmailBaseWebApiUrl [nvarchar] (1000) = ''
  DECLARE @EmailFromAddress [nvarchar] (1000) = ''
  DECLARE @Emailees [nvarchar] (1000) = ''
*/
    SET @BatchDetailDescription = '230/230:  Populate @ReadDirectory'
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailStart', @BatchDetailDescription
	
	  SET @AnticipatedRecordCount = 1;
	  
      -- Output 
      SELECT 
        @ReadDirectory AS [ReadDirectory]
        ,@InputFilenameContainsString AS [InputFilenameContainsString]
        ,@LineToLookForSearchString AS [LineToLookForSearchString]
        ,@SearchString AS [SearchString]
        ,@InputFileArchiveDirectory AS [InputFileArchiveDirectory]
        ,@OutputFileToSummitAdtProcessingReadDirectory AS [OutputFileToSummitAdtProcessingReadDirectory]
        ,@OutputFileToAthenaReadDirectory AS [OutputFileToAthenaReadDirectory]
        ,@OutputFileToLightbeamReadDirectory AS [OutputFileToLightbeamReadDirectory]
        ,@OutputFileToSummitAdtProcessingArchiveDirectory AS [OutputFileToSummitAdtProcessingArchiveDirectory]
        ,@OutputFileToAthenaArchiveDirectory AS [OutputFileToAthenaArchiveDirectory]
        ,@OutputFileToLightbeamArchiveDirectory AS [OutputFileToLightbeamArchiveDirectory]
        ,@SimplifyVbcAdtBaseWebApiUrl AS [SimplifyVbcAdtBaseWebApiUrl]
        ,@ExcelTemplateFullFilename AS [ExcelTemplateFullFilename]
        ,@ImportArchiveFolder AS [ImportArchiveFolder]
        ,@BulkInsertConnectionString AS [BulkInsertConnectionString]
        ,@BulkInsertDbName AS [BulkInsertDbName]
        ,@BulkInsertDbTableName AS [BulkInsertDbTableName]
        ,@BulkInsertBaseWebApiUrl AS [BulkInsertBaseWebApiUrl]
        ,@EmailBaseWebApiUrl AS [EmailBaseWebApiUrl]
        ,@EmailFromAddress AS [EmailFromAddress]
        ,@Emailees AS [Emailees];
	
    SET @ActualRecordCount = @@ROWCOUNT
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailEnd', NULL, NULL, NULL, @AnticipatedRecordCount, @ActualRecordCount

----------------------------------------------------------------------------------------------------------------------------------------------------

--  Close batch
    EXEC Admin.Utilities.logs.di_batch @BatchOutID OUTPUT, @BatchOutID, 'BatchEnd'

END TRY


BEGIN CATCH
DECLARE @Err              int
     ,  @ErrorMessage     varchar(Max)
     ,  @ErrorLine        varchar(128)
     ,  @Workstation      varchar(128) = @Application
     ,  @Procedure        VARCHAR(500)

    IF ERROR_NUMBER() IS NULL 
      SET @Err =0;
    ELSE
      SET @Err = ERROR_NUMBER();

    SET @ErrorMessage = ERROR_MESSAGE()
    SET @ErrorLine    = 'SP Line Number: ' + CAST(ERROR_LINE() as varchar(10)) 
    
	SET @Workstation  = HOST_NAME()
	
    SET @Procedure    = @@SERVERNAME + '.' + DB_NAME() + '.' + OBJECT_SCHEMA_NAME(@@ProcID) + '.' + OBJECT_NAME(@@ProcID) + ' - ' + @ErrorLine + ' - ' + LEFT(@BatchDetailDescription, 7)
    EXEC Admin.Utilities.administration.di_ErrorLog  @Application ,@Process, @Version ,0, @ErrorMessage, @Procedure,  @User , @Workstation

    SET @BatchMessage = 'Process Failed:  ' +  @ErrorMessage
    EXEC Admin.Utilities.logs.di_batch @BatchOutID OUTPUT, @BatchOutID, 'BatchEnd', @BatchMessage
	
    RAISERROR(@ErrorMessage, 16,1)

END CATCH


END