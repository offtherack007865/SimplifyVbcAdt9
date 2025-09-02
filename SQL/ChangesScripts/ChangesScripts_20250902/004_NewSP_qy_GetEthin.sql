-- SQL Server Instance:  smg-sql01
IF (@@SERVERNAME <> 'smg-sql01')
BEGIN
PRINT 'Invalid SQL Server Connection'
RETURN
END

USE [Utilities];
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('adt.qy_GetEthin'))
   DROP PROC [adt].[qy_GetEthin]
GO
CREATE PROCEDURE [adt].[qy_GetEthin]
/* -----------------------------------------------------------------------------------------------------------
   Procedure Name  :  qy_GetEthin
   Business Analyis:
   Project/Process :   
   Description     :  Get all rows from Ethin table.
	  
   Author          :  Philip Morrison 
   Create Date     :  8/29/2025

   ***********************************************************************************************************
   **         Change History                                                                                **
   ***********************************************************************************************************

   Date       Version    Author             Description
   --------   --------   -----------        ------------
   8/29/2025  1.01.001   Philip Morrison    Created
*/ -----------------------------------------------------------------------------------------------------------                                   

AS
BEGIN

-- Instance Declarations
DECLARE @MyRecordCount [int] = 0
DECLARE @IsOk [bit] = 0


-- Template Declarations
DECLARE @Application            varchar(128) = 'SimplifyVbcAdt' 
DECLARE @Version                varchar(25)  = '1.01.001'

DECLARE @ProcessID              int          = 0
DECLARE @Process                varchar(128) = 'Ethin'

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

    SET @BatchDetailDescription = '010/010:  Get all rows from Ethin'
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailStart', @BatchDetailDescription
	
	  SELECT @AnticipatedRecordCount = COUNT(*)
	                                   FROM [SimplifyVbcAdt8].[dbo].[Ethin];
	  
    -- Get all rows from Ethin
    SELECT 
       [SummitMrn]
       ,[PatientClass]
       ,CONVERT([nvarchar] (50), [MessageTime], 121) AS [MessageTime]
       ,[LastName]
       ,[FirstName]
       ,[MiddleName]
       ,[Suffix]
       ,[Gender]
       ,CONVERT([nvarchar] (10), CONVERT([date],[DateOfBirth]), 101) AS [DateOfBirth]      
       ,CONVERT([nvarchar] (10), CONVERT([date],[DateOfDeath]), 101) AS [DateOfDeath]      
       ,[SendingFacility]
       ,CONVERT([nvarchar] (50), [AdmitTime], 121) AS [AdmitTime]
       ,CONVERT([nvarchar] (50), [DischargeTime], 121) AS [DischargeTime]
       ,[AttendingProvider]
       ,[PrimaryCareProvider]
       ,[AdmitSource]
       ,[AdmitReason]
       ,[DischargeStatus]
       ,[FinalDiagnosesList]
       ,[Insurance]
       ,[AdmitOrDischarge]
       ,[SourceFullFilename]
    FROM [SimplifyVbcAdt8].[dbo].[Ethin];
    
    SET @ActualRecordCount = @@ROWCOUNT
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailEnd', NULL, NULL, NULL, @AnticipatedRecordCount, @ActualRecordCount

----------------------------------------------------------------------------------------------------------------------------------------------------
--  Close Batch
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT, @BatchOutID, 'BatchEnd'

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