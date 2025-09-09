-- SQL Server Instance:  smg-sql01
IF (@@SERVERNAME <> 'smg-sql01')
BEGIN
PRINT 'Invalid SQL Server Connection'
RETURN
END

USE [Utilities];
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('adt.di_Humana'))
   DROP PROC [adt].[di_Humana]
GO
CREATE PROCEDURE [adt].[di_Humana]
/* -----------------------------------------------------------------------------------------------------------
   Procedure Name  :  di_Humana
   Business Analyis:
   Project/Process :   
   Description     :  Finalize Staging Humana table.
	  
   Author          :  Philip Morrison 
   Create Date     :  8/27/2025

   ***********************************************************************************************************
   **         Change History                                                                                **
   ***********************************************************************************************************

   Date       Version    Author             Description
   --------   --------   -----------        ------------
   8/27/2025  1.01.001   Philip Morrison    Created
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

    SET @BatchDetailDescription = '010/030:  Truncate Humana'
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailStart', @BatchDetailDescription
	
	  SELECT @AnticipatedRecordCount = COUNT(*)
	                                   FROM [SimplifyVbcAdt8].[dbo].[Humana];
	  
    -- Truncate Humana
    TRUNCATE TABLE [SimplifyVbcAdt8].[dbo].[Humana];
    
    SET @ActualRecordCount = @@ROWCOUNT
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailEnd', NULL, NULL, NULL, @AnticipatedRecordCount, @ActualRecordCount
----------------------------------------------------------------------------------------------------------------------------------------------------

    SET @BatchDetailDescription = '020/030:  Copy Staging Humana to SimplifyVbcAdt Humana'
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailStart', @BatchDetailDescription
	
	  SELECT @AnticipatedRecordCount = COUNT(*)
	                                   FROM [Staging].[adt].[Humana];
	  
     INSERT INTO [SimplifyVbcAdt8].[dbo].[Humana]
     (
        [RptDt]
        ,[Type]
        ,[Group]
        ,[GrouperName]
        ,[FacDbName]
        ,[PcpName]
        ,[BedType]
        ,[AuthId]	
        ,[AdmitDt]
        ,[DcDate]
        ,[DcDisposition]
        ,[SubscriberId]
        ,[LastName]
        ,[FirstName]
        ,[DateOfBirth]
        ,[IsCaseReAdmit14dy]
        ,[IsCaseReAdmit30dy]
        ,[ReadmitScore]
        ,[RiskScoreGroup]
        ,[ClinicalProgram]
        ,[CaseName]
        ,[CaseStatus]
        ,[GrouperId]
        ,[PcpId]
        ,[FaxTaxId]
        ,[AdmitType]
        ,[RequestType]
        ,[NotifDate]
        ,[AuthorizedDays]
        ,[ContactType]
        ,[ContactName]
        ,[ContactPhone]
        ,[ContactNbrExt]
        ,[ContactEmail]
        ,[DiagCode1]
        ,[DiagDesc1]
        ,[DiagCode2]
        ,[DiagDesc2]
        ,[ProcCode1]
        ,[ProcDesc1]
        ,[ProcCode2]
        ,[ProcDesc2]
        ,[AuthSource]
        ,[LobDesc]
        ,[Gender]
        ,[AdmitOrDischarge]
        ,[SourceFullFilename]
    )   
    SELECT
       CONVERT([datetime], [RptDt], 121)
       ,[Type]
       ,[Group]
       ,[GrouperName]
       ,[FacDbName]
       ,[PcpName]
       ,[BedType]
       ,[AuthId]
       ,CONVERT([datetime], [AdmitDt], 121)
       ,CONVERT([datetime], [DcDate], 121)
       ,[DcDisposition]
       ,[SubscriberId]
       ,[LastName]
       ,[FirstName]
       ,CONVERT([datetime], [DateOfBirth], 121)
       ,[IsCaseReAdmit14dy]
       ,[IsCaseReAdmit30dy]
       ,CONVERT([int], [ReadmitScore])
       ,[RiskScoreGroup]
       ,[ClinicalProgram]
       ,[CaseName]
       ,[CaseStatus]
       ,[GrouperId]
       ,[PcpId]
       ,[FaxTaxId]
       ,[AdmitType]
       ,[RequestType]
       ,CONVERT([datetime], [NotifDate], 121)
       ,CONVERT([int], [AuthorizedDays])
       ,[ContactType]
       ,[ContactName]
       ,[ContactPhone]
       ,[ContactNbrExt]
       ,[ContactEmail]
       ,[DiagCode1]
       ,[DiagDesc1]
       ,[DiagCode2]
       ,[DiagDesc2]
       ,CONVERT([int], [ProcCode1])
       ,[ProcDesc1]
       ,CONVERT([int], [ProcCode2])
       ,[ProcDesc2]
       ,[AuthSource]
       ,[LobDesc]
       ,[Gender]
       ,[AdmitOrDischarge]
       ,[SourceFullFilename]
    FROM [Staging].[adt].[Humana]
    
    SET @ActualRecordCount = @@ROWCOUNT
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailEnd', NULL, NULL, NULL, @AnticipatedRecordCount, @ActualRecordCount
----------------------------------------------------------------------------------------------------------------------------------------------------

    SET @BatchDetailDescription = '030/030:  Send Output that Finalize worked.'
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailStart', @BatchDetailDescription
	
	  SELECT @AnticipatedRecordCount = 1;
	
    -- Did Finalize work
    SELECT @MyRecordCount = COUNT(*)
                            FROM [SimplifyVbcAdt8].[dbo].[Humana];
    
    -- Send Output indication of whether Truncate worked.
    IF @MyRecordCount > 0 BEGIN
      SET @IsOk = 1;
    END
    ELSE BEGIN
      SET @IsOk = 0;
    END
    
    SELECT @IsOk as [IsOk];
    
    
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