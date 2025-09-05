-- SQL Server Instance:  smg-sql01
IF (@@SERVERNAME <> 'smg-dsdev05')
BEGIN
PRINT 'Invalid SQL Server Connection'
RETURN
END

USE [Utilities];
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('adt.qy_GetHumanaAdmissions'))
   DROP PROC [adt].[qy_GetHumanaAdmissions]
GO
CREATE PROCEDURE [adt].[qy_GetHumanaAdmissions]
/* -----------------------------------------------------------------------------------------------------------
   Procedure Name  :  qy_GetHumanaAdmissions
   Business Analyis:
   Project/Process :   
   Description     :  Get all Admissions rows from Humana table.
	  
   Author          :  Philip Morrison 
   Create Date     :  9/5/2025

   ***********************************************************************************************************
   **         Change History                                                                                **
   ***********************************************************************************************************

   Date       Version    Author             Description
   --------   --------   -----------        ------------
   9/5/2025  1.01.001   Philip Morrison    Created
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

    SET @BatchDetailDescription = '010/010:  Get all rows from Humana'
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailStart', @BatchDetailDescription
	
	  SELECT @AnticipatedRecordCount = COUNT(*)
	                                   FROM [SimplifyVbcAdt8].[dbo].[Humana];
	  
    -- Get all rows from Humana
    SELECT 
       CONVERT([nvarchar] (50), [RptDt], 121) AS [RptDt]
	   ,[Type]
	   ,[Group]
	   ,[GrouperName]
	   ,[FacDbName]
	   ,[PcpName]
	   ,[BedType]
	   ,[AuthId]
       ,CONVERT([nvarchar] (50), [AdmitDt], 121) AS [AdmitDt]
       ,CONVERT([nvarchar] (50), [DcDate], 121) AS [DcDate]
	   ,[DcDisposition]
	   ,[SubscriberId]
	   ,[LastName]
	   ,[FirstName]
       ,CONVERT([nvarchar] (10), CONVERT([date],[DateOfBirth]), 101) AS [DateOfBirth]      
	   ,[IsCaseReAdmit14dy]
	   ,[IsCaseReAdmit30dy]
	   ,CONVERT([nvarchar] (10), [ReadmitScore]) AS [ReadmitScore]
	   ,[RiskScoreGroup]
	   ,[ClinicalProgram]
	   ,[CaseName]
	   ,[CaseStatus]
       ,[GrouperId]
	   ,[PcpId]
	   ,[FaxTaxId]
	   ,[AdmitType]
	   ,[RequestType]
       ,CONVERT([nvarchar] (50), [NotifDate], 121) AS [NotifDate]
	   ,CONVERT([nvarchar] (10), [AuthorizedDays]) AS [AuthorizedDays]
	   ,[ContactType]
	   ,[ContactName]
	   ,[ContactPhone]
	   ,[ContactNbrExt]
	   ,[ContactEmail]
	   ,[DiagCode1]
	   ,[DiagDesc1]
	   ,[DiagCode2]
	   ,[DiagDesc2]
	   ,CONVERT([nvarchar] (10), [ProcCode1]) AS [ProcCode1]
	   ,[ProcDesc1]
	   ,CONVERT([nvarchar] (10), [ProcCode2]) AS [ProcCode2]
	   ,[ProcDesc2]
	   ,[AuthSource]
	   ,[LobDesc]
	   ,[Gender]
       ,[AdmitOrDischarge]
       ,[SourceFullFilename] 
    FROM [SimplifyVbcAdt8].[dbo].[Humana]
    WHERE UPPER([AdmitOrDischarge]) = 'ADMIT';
    
    
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