-- SQL Server Instance:  smg-sql01
USE [Utilities];
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('adt.di_PointClickCare'))
   DROP PROC [adt].[di_PointClickCare]
GO
CREATE PROCEDURE [adt].[di_PointClickCare]
(
  @inputFullFilename [nvarchar] (300)
)

/* -----------------------------------------------------------------------------------------------------------
   Procedure Name  :  di_PointClickCare
   Business Analyis:
   Project/Process :   
   Description     :  Finalize PointClickCare table.
	  
   Author          :  Philip Morrison 
   Create Date     :  8/5/2025

   ***********************************************************************************************************
   **         Change History                                                                                **
   ***********************************************************************************************************

   Date       Version    Author             Description
   --------   --------   -----------        ------------
   8/5/2025  1.01.001   Philip Morrison    Created
*/ -----------------------------------------------------------------------------------------------------------                                   

AS
BEGIN

-- Instance Declarations.
declare @IsOk [bit] 
declare @MyRecordCount [int] 

-- Template Declarations
DECLARE @Application            varchar(128) = 'SimplifyVbcAdt' 
DECLARE @Version                varchar(25)  = '1.01.002'

DECLARE @ProcessID              int          = 0
DECLARE @Process                varchar(128) = 'PointClickCare'

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

    SET @BatchDetailDescription = '010/030:  Truncate PointClickCare'
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailStart', @BatchDetailDescription
	
	  SELECT @AnticipatedRecordCount = COUNT(*)
	                                   FROM [SimplifyVbcAdt8].[dbo].[PointClickCare];
	  
    -- Truncate PointClickCareAdtMaster
    TRUNCATE TABLE [SimplifyVbcAdt8].[dbo].[PointClickCare];
	
    SET @ActualRecordCount = @@ROWCOUNT
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailEnd', NULL, NULL, NULL, @AnticipatedRecordCount, @ActualRecordCount
----------------------------------------------------------------------------------------------------------------------------------------------------

    SET @BatchDetailDescription = '020/030:  Copy staging.PointClickCare to SimplifyVbcAdt8.PointClickCareAdtMaster'
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailStart', @BatchDetailDescription
	
	  SELECT @AnticipatedRecordCount = COUNT(*)
	                                   FROM [Staging].[adt].[PointClickCare];
	  
    -- Copy staging.PointClickCare to SimplifyVbcAdt8.PointClickCare'
    INSERT INTO [SimplifyVbcAdt8].[dbo].[PointClickCare]
	(
      [LastName]
      ,[FirstName]
      ,[SenderSourceCode]
      ,[SenderMrn]
      ,[SubscriberMrn]
      ,[FacilityName]
      ,[Gender]
      ,[DateOfBirth]
      ,[EventTime]
      ,[AlertType]
      ,[HospitalService]
      ,[AdmitSource]
      ,[AdmitDate]
      ,[PatientComplaints]
      ,[DiagnosisCode]
      ,[DiagnosisDescription]
      ,[DischargeDate]
      ,[DischargeLocation]
      ,[DischargeDisposition]
      ,[DeathIndicator]
      ,[PatientClass]
      ,[PatientClassDescription]
      ,[PrimaryCareProvider]
      ,[Insurance]
      ,[Practice]
      ,[Address1]
      ,[Address2]
      ,[State]
      ,[Zipcode]
      ,[NumberOfErVisits]
      ,[NumberOfIpVisits]
      ,[HomePhone]
      ,[ImportFullFilename]
	)
    SELECT
      REPLACE([LastName],'^', ',')
      ,REPLACE([FirstName],'^', ',')
      ,[SenderSourceCode]
      ,[SenderMrn]
      ,[SubscriberMrn]
      ,REPLACE([FacilityName],'^', ',')
      ,[Gender]
      ,[DateOfBirth]
      ,[EventTime]
      ,[AlertType]
      ,REPLACE([HospitalService],'^', ',')
      ,REPLACE([AdmitSource],'^', ',')
      ,[AdmitDate]
      ,REPLACE([PatientComplaints],'^', ',')
      ,[DiagnosisCode]
      ,REPLACE([DiagnosisDescription],'^', ',')
      ,[DischargeDate]
      ,REPLACE([DischargeLocation],'^', ',')
      ,REPLACE([DischargeDisposition],'^', ',')
      ,[DeathIndicator]
      ,[PatientClass]
      ,REPLACE([PatientClassDescription],'^', ',')
      ,REPLACE([PrimaryCareProvider],'^', ',')
      ,REPLACE([Insurance],'^', ',')
      ,REPLACE([Practice],'^', ',')
      ,REPLACE([Address1],'^', ',')
      ,REPLACE([Address2],'^', ',')
      ,[State]
      ,[Zipcode]
      ,[NumberOfErVisits]
      ,[NumberOfIpVisits]
      ,[HomePhone]
      ,@inputFullFilename
	FROM [Staging].[adt].[PointClickCare];
	
    SET @ActualRecordCount = @@ROWCOUNT
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailEnd', NULL, NULL, NULL, @AnticipatedRecordCount, @ActualRecordCount

----------------------------------------------------------------------------------------------------------------------------------------------------

    SET @BatchDetailDescription = '030/030:  Send Output that Copy worked.'
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailStart', @BatchDetailDescription
	
	  SELECT @AnticipatedRecordCount = 1;
	
    -- Did Copy work
    SELECT @MyRecordCount = COUNT(*)
                            FROM [SimplifyVbcAdt8].[dbo].[PointClickCare];
    
    -- Send Output indication of whether Truncate worked.
    IF @MyRecordCount = 0 BEGIN
      SET @IsOk = 0;
    END
    ELSE BEGIN
      SET @IsOk = 1;
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