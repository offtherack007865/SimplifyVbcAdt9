-- SQL Server Instance:  smg-sql01
USE [Utilities];
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('adt.qy_GetPointClickCare'))
   DROP PROC [adt].[qy_GetPointClickCare]
GO
CREATE PROCEDURE [adt].[qy_GetPointClickCare]
/* -----------------------------------------------------------------------------------------------------------
   Procedure Name  :  qy_GetPointClickCare
   Business Analyis:
   Project/Process :   
   Description     :  Get all rows from PointClickCare table.
	  
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

-- Instance Declarations
DECLARE @MyRecordCount [int] = 0
DECLARE @IsOk [bit] = 0


-- Template Declarations
DECLARE @Application            varchar(128) = 'SimplifyVbcAdt' 
DECLARE @Version                varchar(25)  = '1.01.001'

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

    SET @BatchDetailDescription = '010/010:  Get all rows from PointClickCare'
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailStart', @BatchDetailDescription
	
	  SELECT @AnticipatedRecordCount = COUNT(*)
	                                   FROM [SimplifyVbcAdt8].[dbo].[PointClickCare];
	  
    -- Get all rows from PointClickCare
    SELECT 
      [LastName]
      ,[FirstName]
      ,[SenderSourceCode]
      ,[SenderMrn]
      ,[SubscriberMrn]
      ,[FacilityName]
      ,[Gender]
      ,CONVERT([nvarchar] (10), CONVERT([date],[DateOfBirth]), 101) AS [DateOfBirth]      
      ,CONVERT([nvarchar] (50), [EventTime], 121) AS [EventTime]
      ,[AlertType]
      ,[HospitalService]
      ,[AdmitSource]
      ,CONVERT([nvarchar] (50), [AdmitDate], 121) AS [AdmitDate]      
      ,[PatientComplaints]
      ,[DiagnosisCode]
      ,[DiagnosisDescription]
      ,CONVERT([nvarchar] (50), [DischargeDate], 121) AS [DischargeDate]      
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
    FROM [SimplifyVbcAdt8].[dbo].[PointClickCare];
    
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