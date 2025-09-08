-- SQL Service Instance:  smg-sql01
/*	
    [ReadDirectory]
	,[InputFilenameContainsString]
	,[LineToLookForSearchString]
	,[SearchString]
	,[InputFileArchiveDirectory]
    ,[OutputFileToSummitAdtProcessingReadDirectory]
	,[OutputFileToAthenaReadDirectory]
	,[OutputFileToLightbeamReadDirectory]
	,[OutputFileToSummitAdtProcessingArchiveDirectory]
	,[OutputFileToAthenaArchiveDirectory]
	,[OutputFileToLightbeamArchiveDirectory]
	,[SimplifyVbcAdtBaseWebApiUrl]
	,[ExcelTemplateFullFilename]
	,[ImportArchiveFolder]
	,[ps_odsConnectionString]
	,[BulkInsertConnectionString]
	,[BulkInsertDbName]
	,[BulkInsertDbTableName]
	,[BulkInsertBaseWebApiUrl]
	,[EmailBaseWebApiUrl]
	,[EmailFromAddress]
    ,STUFF((SELECT  '; ' + [EmailAddress]
         FROM    
         ( SELECT DISTINCT [EmailAddress]
             FROM  [dbo].[SimplifyVbcAdtHumanaConfigEmailAddress] eml
            where eml.[SimplifyVbcAdtHumanaConfigPk] = cfg.[Pk]
         ) x
         FOR
         XML PATH('')
         ), 1, 2, '') [Emailees]
         
        BulkInsertBaseWebApiUrl	"http://webservices:8082/api/Ops/PerformBulkInsert"	string
		BulkInsertConnectionString	"Data Source=smg-sql01;Initial Catalog=SimplifyVbcAdt8;TrustServerCertificate=True;Integrated Security=False;User ID=AppUser;Password=SoMm1t!@"	string
		BulkInsertDbName	"SimplifyVbcAdt8"	string
		BulkInsertDbTableName	"HumanaRaw"	string
		EmailBaseWebApiUrl	"http://webservices:8081/api/EmailWebApi/SendEmailWithHtmlStringInput"	string
		EmailFromAddress	"smgapplications@summithealthcare.com"	string
		Emailees	"pwmorrison@summithealthcare.com"	string
		ExcelTemplateFullFilename	"\\\\ps-nas\\nas\\SSS\\VBC\\1. Care Coordination\\ADMIN ASSISTANT\\ADT FEEDS\\Humana Simplifier (do not use)\\HumanaTemplate\\HUMANA DAILY DROP TEMPLATE.xlsx"	string
		ImportArchiveFolder	"\\\\ps-nas\\nas\\SSS\\IT\\Shared\\SSS ODS\\ADT Feeds\\Humana\\Archive"	string
		InputFileArchiveDirectory	"\\\\ps-nas\\nas\\SSS\\VBC\\1. Care Coordination\\ADMIN ASSISTANT\\ADT FEEDS\\Humana Simplifier (do not use)\\InputFileArchive"	string
		InputFilenameContainsString	"*.xlsx"	string
		LineToLookForSearchString	2	int
		OutputFileToAthenaArchiveDirectory	"\\\\ps-nas\\nas\\SSS\\VBC\\1. Care Coordination\\ADMIN ASSISTANT\\ADT FEEDS\\Humana Simplifier (do not use)\\OutputToAthenaArchive"	string
		OutputFileToAthenaReadDirectory	"\\\\smg-sql01\\AthenaFiles\\Humana"	string
		OutputFileToLightbeamArchiveDirectory	"\\\\ps-nas\\nas\\SSS\\VBC\\1. Care Coordination\\ADMIN ASSISTANT\\ADT FEEDS\\Humana Simplifier (do not use)\\OutputToLightbeam"	string
		OutputFileToLightbeamReadDirectory	"\\\\ps-nas\\nas\\SMG\\Sites\\STAR\\CovenantFileDrop\\Humana\\ImportForHl7Conversion"	string
		OutputFileToSummitAdtProcessingArchiveDirectory	"\\\\ps-nas\\nas\\SSS\\VBC\\1. Care Coordination\\ADMIN ASSISTANT\\ADT FEEDS\\Humana Simplifier (do not use)\\OutputToSummitADTProcessingArchive"	string
		OutputFileToSummitAdtProcessingReadDirectory	"\\\\ps-nas\\nas\\sss\\it\\shared\\SSS ODS\\ADT Feeds\\Humana\\Pending"	string
		ReadDirectory	"\\\\ps-nas\\nas\\SSS\\VBC\\1. Care Coordination\\ADMIN ASSISTANT\\ADT FEEDS\\Humana Simplifier (do not use)"	string
		SearchString	"Summit MRN"	string
		SimplifyVbcAdtBaseWebApiUrl	"http://webservices:8091"	string
		ps_odsConnectionString	"Data Source=smg-sql01;Initial Catalog=Utilities;TrustServerCertificate=True;Integrated Security=False;User ID=AppUser;Password=SoMm1t!@"	string
         
         
*/
USE [Administration];


DECLARE @MyApplicationID [int] = 0;

-- If Application ID exists, delete both the Process ID and its associated Settings.
SELECT @MyApplicationID = [ApplicationID]
  FROM [admin].[Application]
  WHERE [ApplicationName] = 'SimplifyVbcAdt';
  
IF (@MyApplicationID IS NULL OR @MyApplicationID = 0) BEGIN
  print ('Insert Application');
  INSERT INTO [admin].[Application]
           ([ApplicationName]
           ,[Description]
           ,[NotificationOnError]
           ,[NotificationGroupID]
           ,[Notes]
           ,[Visible]
           ,[ListOrder]
           ,[Active]
           ,[InsertedBy]
           ,[InsertedDate]
           ,[UpdatedBy]
           ,[UpdatedDate])
     VALUES
           ('SimplifyVbcAdt'
           ,'SimplifyVbcAdt'
           ,0
           ,null
           ,null
           ,1
           ,550
           ,1
           ,'pwmorrison'
           ,getdate()
           ,'pwmorrison'
           ,getdate());
		   
SELECT @MyApplicationID = [ApplicationID]
  FROM [admin].[Application]
  WHERE [ApplicationName] = 'SimplifyVbcAdt';
		   
END

DECLARE @MyProcessID [int] = 0;

-- If Process ID exists, delete both the Process ID and its associated Settings.
SELECT @MyProcessID = [ProcessID]
  FROM [support].[Process]
  WHERE [Name] = 'Humana'
    AND [ApplicationID] = @MyApplicationID;  
  
IF (@MyProcessID IS NOT NULL) BEGIN
  DELETE [support].[Setting]
   WHERE [ApplicationID] = @MyApplicationID
     AND [ProcessID] = @MyProcessID;
	 
  DELETE [support].[Process]
   WHERE [ApplicationID] = @MyApplicationID
     AND [ProcessID] = @MyProcessID;
	 
END
  
-- Insert Process 
INSERT INTO [support].[Process]
           ([ApplicationID]
           ,[Name]
           ,[Type]
           ,[Description]
           ,[NotificationOnError]
           ,[NotificationGroupID]
           ,[Visible]
           ,[ListOrder]
           ,[Active])
     VALUES
           (@MyApplicationID
           ,'Humana'
           ,'Reporting' 
           ,'Humana'
           ,0
           ,NULL
           ,1
           ,0 
           ,1);		   
  print 'Set process'

-- Find newly inserted process ID
SET @MyProcessID = NULL;
SELECT @MyProcessID = [ProcessID]
  FROM [support].[Process]
  WHERE [Name] = 'Humana'
    AND [ApplicationID] = @MyApplicationID;  
IF (@MyProcessID IS NULL) BEGIN
  print 'Process ID not there.'
  RETURN;
END 

print 'ApplicationId = ' + convert([nvarchar] (20), @MyApplicationID)
print 'ProcessId = ' + convert([nvarchar] (20), @MyProcessID)

/*	
    [ReadDirectory]
	,[InputFilenameContainsString]
	,[LineToLookForSearchString]
	,[SearchString]
	,[InputFileArchiveDirectory]
    ,[OutputFileToSummitAdtProcessingReadDirectory]
	,[OutputFileToAthenaReadDirectory]
	,[OutputFileToLightbeamReadDirectory]
	,[OutputFileToSummitAdtProcessingArchiveDirectory]
	,[OutputFileToAthenaArchiveDirectory]
	,[OutputFileToLightbeamArchiveDirectory]
	,[SimplifyVbcAdtBaseWebApiUrl]
	,[ExcelTemplateFullFilename]
	,[ImportArchiveFolder]
	,[BulkInsertConnectionString]
	,[BulkInsertDbName]
	,[BulkInsertDbTableName]
	,[BulkInsertBaseWebApiUrl]
	,[EmailBaseWebApiUrl]
	,[EmailFromAddress]
    ,STUFF((SELECT  '; ' + [EmailAddress]
         FROM    
         ( SELECT DISTINCT [EmailAddress]
             FROM  [dbo].[SimplifyVbcAdtHumanaConfigEmailAddress] eml
            where eml.[SimplifyVbcAdtHumanaConfigPk] = cfg.[Pk]
         ) x
         FOR
         XML PATH('')
         ), 1, 2, '') [Emailees]
         
        BulkInsertBaseWebApiUrl	"http://webservices:8082/api/Ops/PerformBulkInsert"	string
		BulkInsertConnectionString	"Data Source=smg-sql01;Initial Catalog=SimplifyVbcAdt8;TrustServerCertificate=True;Integrated Security=False;User ID=AppUser;Password=SoMm1t!@"	string
		BulkInsertDbName	"SimplifyVbcAdt8"	string
		BulkInsertDbTableName	"HumanaRaw"	string
		EmailBaseWebApiUrl	"http://webservices:8081/api/EmailWebApi/SendEmailWithHtmlStringInput"	string
		EmailFromAddress	"smgapplications@summithealthcare.com"	string
		Emailees	"pwmorrison@summithealthcare.com"	string
		ExcelTemplateFullFilename	"\\\\ps-nas\\nas\\SSS\\VBC\\1. Care Coordination\\ADMIN ASSISTANT\\ADT FEEDS\\Humana Simplifier (do not use)\\HumanaTemplate\\HUMANA DAILY DROP TEMPLATE.xlsx"	string
		ImportArchiveFolder	"\\\\ps-nas\\nas\\SSS\\IT\\Shared\\SSS ODS\\ADT Feeds\\Humana\\Archive"	string
		InputFileArchiveDirectory	"\\\\ps-nas\\nas\\SSS\\VBC\\1. Care Coordination\\ADMIN ASSISTANT\\ADT FEEDS\\Humana Simplifier (do not use)\\InputFileArchive"	string
		InputFilenameContainsString	"*.xlsx"	string
		LineToLookForSearchString	2	int
		OutputFileToAthenaArchiveDirectory	"\\\\ps-nas\\nas\\SSS\\VBC\\1. Care Coordination\\ADMIN ASSISTANT\\ADT FEEDS\\Humana Simplifier (do not use)\\OutputToAthenaArchive"	string
		OutputFileToAthenaReadDirectory	"\\\\smg-sql01\\AthenaFiles\\Humana"	string
		OutputFileToLightbeamArchiveDirectory	"\\\\ps-nas\\nas\\SSS\\VBC\\1. Care Coordination\\ADMIN ASSISTANT\\ADT FEEDS\\Humana Simplifier (do not use)\\OutputToLightbeam"	string
		OutputFileToLightbeamReadDirectory	"\\\\ps-nas\\nas\\SMG\\Sites\\STAR\\CovenantFileDrop\\Humana\\ImportForHl7Conversion"	string
		OutputFileToSummitAdtProcessingArchiveDirectory	"\\\\ps-nas\\nas\\SSS\\VBC\\1. Care Coordination\\ADMIN ASSISTANT\\ADT FEEDS\\Humana Simplifier (do not use)\\OutputToSummitADTProcessingArchive"	string
		OutputFileToSummitAdtProcessingReadDirectory	"\\\\ps-nas\\nas\\sss\\it\\shared\\SSS ODS\\ADT Feeds\\Humana\\Pending"	string
		ReadDirectory	"\\\\ps-nas\\nas\\SSS\\VBC\\1. Care Coordination\\ADMIN ASSISTANT\\ADT FEEDS\\Humana Simplifier (do not use)"	string
		SearchString	"Summit MRN"	string
		SimplifyVbcAdtBaseWebApiUrl	"http://webservices:8091"	string
*/
--BEGIN TRAN

INSERT INTO [support].[Setting]

           ([ApplicationID]
            ,[ProcessID]
            ,[Name]
            ,[Type]
            ,[Description]
            ,[Value]
            ,[Active])

VALUES (@MyApplicationID
           ,@MyProcessID
           ,'ReadDirectory'
           ,'Default'
           ,''
           ,'\\ps-nas\nas\SSS\VBC\1. Care Coordination\ADMIN ASSISTANT\ADT FEEDS\Humana Simplifier (do not use)'
           ,1)
      ,(@MyApplicationID
           ,@MyProcessID
           ,'InputFilenameContainsString'
           ,'Default'
           ,''
           ,'*.xlsx'
           ,1)
      ,(@MyApplicationID
           ,@MyProcessID
           ,'LineToLookForSearchString'
           ,'Default'
           ,''
           ,'2'
           ,1)
      ,(@MyApplicationID
           ,@MyProcessID
           ,'SearchString'
           ,'Default'
           ,''
           ,'Summit MRN'
           ,1)           
      ,(@MyApplicationID
           ,@MyProcessID
           ,'InputFileArchiveDirectory'
           ,'Default'
           ,''
           ,'\\ps-nas\nas\SSS\VBC\1. Care Coordination\ADMIN ASSISTANT\ADT FEEDS\Humana Simplifier (do not use)\InputFileArchive'
           ,1)
      ,(@MyApplicationID
           ,@MyProcessID
           ,'OutputFileToSummitAdtProcessingReadDirectory'
           ,'Default'
           ,''
           ,'\\ps-nas\nas\SSS\VBC\1. Care Coordination\ADMIN ASSISTANT\ADT FEEDS\Humana Simplifier (do not use)\OutputToSummitADTProcessingArchive'
           ,1)
      ,(@MyApplicationID
           ,@MyProcessID
           ,'OutputFileToAthenaReadDirectory'
           ,'Default'
           ,''
           ,'\\ps-nas\nas\SSS\VBC\1. Care Coordination\ADMIN ASSISTANT\ADT FEEDS\Humana Simplifier (do not use)\OutputToAthenaArchive'
           ,1)
      ,(@MyApplicationID
           ,@MyProcessID
           ,'OutputFileToLightbeamReadDirectory'
           ,'Default'
           ,''
           ,'\\ps-nas\nas\SSS\VBC\1. Care Coordination\ADMIN ASSISTANT\ADT FEEDS\Humana Simplifier (do not use)\OutputToLightbeam'
           ,1)           
      ,(@MyApplicationID
           ,@MyProcessID
           ,'OutputFileToSummitAdtProcessingArchiveDirectory'
           ,'Default'
           ,''
           ,'\\ps-nas\nas\SSS\VBC\1. Care Coordination\ADMIN ASSISTANT\ADT FEEDS\Humana Simplifier (do not use)\OutputToSummitADTProcessingArchive'
           ,1)
      ,(@MyApplicationID
           ,@MyProcessID
           ,'OutputFileToAthenaArchiveDirectory'
           ,'Default'
           ,''
           ,'\\ps-nas\nas\SSS\VBC\1. Care Coordination\ADMIN ASSISTANT\ADT FEEDS\Humana Simplifier (do not use)\OutputToAthenaArchive'
           ,1)
      ,(@MyApplicationID
           ,@MyProcessID
           ,'OutputFileToLightbeamArchiveDirectory'
           ,'Default'
           ,''
           ,'\\\\ps-nas\\nas\\SSS\\VBC\\1. Care Coordination\\ADMIN ASSISTANT\\ADT FEEDS\\Humana Simplifier (do not use)\\OutputToLightbeam'
           ,1)
      ,(@MyApplicationID
           ,@MyProcessID
           ,'SimplifyVbcAdtBaseWebApiUrl'
           ,'Default'
           ,''
           ,'http://webservices:8106'
           ,1)
      ,(@MyApplicationID
           ,@MyProcessID
           ,'ExcelTemplateFullFilename'
           ,'Default'
           ,''
           ,'\\ps-nas\nas\SSS\VBC\1. Care Coordination\ADMIN ASSISTANT\ADT FEEDS\Humana Simplifier (do not use)\HumanaTemplate\HUMANA DAILY DROP TEMPLATE.xlsx'
           ,1)
      ,(@MyApplicationID
           ,@MyProcessID
           ,'ImportArchiveFolder'
           ,'Default'
           ,''
           ,'\\ps-nas\nas\SSS\IT\Shared\SSS ODS\ADT Feeds\Humana\Archive'
           ,1)
      ,(@MyApplicationID
           ,@MyProcessID
           ,'BulkInsertConnectionString'
           ,'Default'
           ,''
           ,'Data Source=smg-dsdev05;Initial Catalog=Staging;TrustServerCertificate=True;Integrated Security=False;User ID=AppUser;Password=SoMm1t!@'
           ,1)
      ,(@MyApplicationID
           ,@MyProcessID
           ,'BulkInsertDbName'
           ,'Default'
           ,''
           ,'Staging'
           ,1)
      ,(@MyApplicationID
           ,@MyProcessID
           ,'BulkInsertDbTableName'
           ,'Default'
           ,''
           ,'adt.Humana'
           ,1)
      ,(@MyApplicationID
           ,@MyProcessID
           ,'BulkInsertBaseWebApiUrl'
           ,'Default'
           ,''
           ,'http://webservices:5559/api/Ops/PerformBulkInsert'
           ,1)
      ,(@MyApplicationID
           ,@MyProcessID
           ,'EmailBaseWebApiUrl'
           ,'Default'
           ,''
           ,'http://webservices:8081/api/EmailWebApi/SendEmailWithHtmlStringInput'
           ,1)
      ,(@MyApplicationID
           ,@MyProcessID
           ,'EmailFromAddress'
           ,'Default'
           ,''
           ,'smgapplications@summithealthcare.com'
           ,1)
      ,(@MyApplicationID
           ,@MyProcessID
           ,'Emailees'
           ,'Default'
           ,''
           ,'pwmorrison@summithealthcare.com'
           ,1);


-- COMMIT TRAN

-- ROLLBACK