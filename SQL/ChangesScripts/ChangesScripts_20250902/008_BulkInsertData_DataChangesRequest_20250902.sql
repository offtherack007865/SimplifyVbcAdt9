-- SQL Server Instance:  smg-sql01
IF (@@SERVERNAME <> 'smg-sql01')
BEGIN
PRINT 'Invalid SQL Server Connection'
RETURN
END

USE [BulkInsert8];


/*CREATE TABLE [dbo].[Ethin](
	[EthinID] [int] IDENTITY(1,1) NOT NULL
    ,[SummitMrn] [nvarchar] (255) NOT NULL
	,[PatientClass] [nvarchar] (255) NOT NULL
	,[MessageTime] [datetime] NOT NULL
	,[LastName] [nvarchar] (255) NOT NULL
	,[FirstName] [nvarchar] (255) NOT NULL
	,[MiddleName] [nvarchar] (255) NULL
	,[Suffix] [nvarchar] (255) NULL
	,[Gender] [nvarchar] (255) NOT NULL
	,[DateOfBirth] [datetime] NOT NULL
	,[DateOfDeath] [datetime] NULL
	,[SendingFacility] [nvarchar] (255) NULL
	,[AdmitTime] [datetime] NOT NULL
	,[DischargeTime] [datetime] NOT NULL
	,[AttendingProvider] [nvarchar] (255) NULL
	,[PrimaryCareProvider] [nvarchar] (255) NULL
	,[AdmitSource] [nvarchar] (255) NULL
	,[AdmitReason] [nvarchar] (255) NULL
	,[DischargeStatus] [nvarchar] (255) NULL
	,[FinalDiagnosesList] [nvarchar] (255) NULL
	,[Insurance] [nvarchar] (255) NULL
		
	,[AdmitOrDischarge] [nvarchar] (255) NULL  

	,[SourceFullFilename] [nvarchar] (500) NULL  
*/
-----------------------------------------------------------------------------------------------
-- STEP 001 of 023
-- COLUMN 001 - [SummitMrn] [nvarchar] (255) NOT NULL
SELECT 1;
-- 1 record 

-- BEGIN TRAN
Insert [dbo].[BulkInsertDataTableAndColumnDefinition]
(
  [BulkInsertDataTablePk] 
  ,[MyDbColumnType]
  ,[MyDbColumnLength]
  ,[MyFilePosition]
  ,[MyDbColumnName]
  ,[MyDbTableName]
  ,[MyDbName]
  ,[CreatedBy]
  ,[CreatedTimestamp]
  ,[UpdatedBy]
  ,[UpdatedTimestamp]
)
values 
(
  57
  ,'nvarchar'
  ,255
  ,1
  ,'SummitMrn'
  ,'adt.Ethin'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 002 of 023
-- COLUMN 002 - [PatientClass] [nvarchar] (255) NOT NULL
SELECT 1;
-- 1 record 

-- BEGIN TRAN
Insert [dbo].[BulkInsertDataTableAndColumnDefinition]
(
  [BulkInsertDataTablePk] 
  ,[MyDbColumnType]
  ,[MyDbColumnLength]
  ,[MyFilePosition]
  ,[MyDbColumnName]
  ,[MyDbTableName]
  ,[MyDbName]
  ,[CreatedBy]
  ,[CreatedTimestamp]
  ,[UpdatedBy]
  ,[UpdatedTimestamp]
)
values 
(
  57
  ,'nvarchar'
  ,255
  ,2
  ,'PatientClass'
  ,'adt.Ethin'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 003 of 023
-- COLUMN 003 - [MessageTime] [datetime] NOT NULL
SELECT 1;
-- 1 record 

-- BEGIN TRAN
Insert [dbo].[BulkInsertDataTableAndColumnDefinition]
(
  [BulkInsertDataTablePk] 
  ,[MyDbColumnType]
  ,[MyDbColumnLength]
  ,[MyFilePosition]
  ,[MyDbColumnName]
  ,[MyDbTableName]
  ,[MyDbName]
  ,[CreatedBy]
  ,[CreatedTimestamp]
  ,[UpdatedBy]
  ,[UpdatedTimestamp]
)
values 
(
  57
  ,'datetime'
  ,50
  ,3
  ,'MessageTime'
  ,'adt.Ethin'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 004 of 023
-- COLUMN 004 - [LastName] [nvarchar] (255) NOT NULL
SELECT 1;
-- 1 record 

-- BEGIN TRAN
Insert [dbo].[BulkInsertDataTableAndColumnDefinition]
(
  [BulkInsertDataTablePk] 
  ,[MyDbColumnType]
  ,[MyDbColumnLength]
  ,[MyFilePosition]
  ,[MyDbColumnName]
  ,[MyDbTableName]
  ,[MyDbName]
  ,[CreatedBy]
  ,[CreatedTimestamp]
  ,[UpdatedBy]
  ,[UpdatedTimestamp]
)
values 
(
  57
  ,'nvarchar'
  ,255
  ,4
  ,'LastName'
  ,'adt.Ethin'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 005 of 023
-- COLUMN 005 - [FirstName] [nvarchar] (255) NOT NULL
SELECT 1;
-- 1 record 

-- BEGIN TRAN
Insert [dbo].[BulkInsertDataTableAndColumnDefinition]
(
  [BulkInsertDataTablePk] 
  ,[MyDbColumnType]
  ,[MyDbColumnLength]
  ,[MyFilePosition]
  ,[MyDbColumnName]
  ,[MyDbTableName]
  ,[MyDbName]
  ,[CreatedBy]
  ,[CreatedTimestamp]
  ,[UpdatedBy]
  ,[UpdatedTimestamp]
)
values 
(
  57
  ,'nvarchar'
  ,255
  ,5
  ,'FirstName'
  ,'adt.Ethin'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 006 of 023
-- COLUMN 6 - [MiddleName] [nvarchar] (255) NULL
SELECT 1;
-- 1 record 

-- BEGIN TRAN
Insert [dbo].[BulkInsertDataTableAndColumnDefinition]
(
  [BulkInsertDataTablePk] 
  ,[MyDbColumnType]
  ,[MyDbColumnLength]
  ,[MyFilePosition]
  ,[MyDbColumnName]
  ,[MyDbTableName]
  ,[MyDbName]
  ,[CreatedBy]
  ,[CreatedTimestamp]
  ,[UpdatedBy]
  ,[UpdatedTimestamp]
)
values 
(
  57
  ,'nvarchar'
  ,255
  ,6
  ,'MiddleName'
  ,'adt.Ethin'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 007 of 023
-- COLUMN 007 - [Suffix] [nvarchar] (255) NULL
SELECT 1;
-- 1 record 

-- BEGIN TRAN
Insert [dbo].[BulkInsertDataTableAndColumnDefinition]
(
  [BulkInsertDataTablePk] 
  ,[MyDbColumnType]
  ,[MyDbColumnLength]
  ,[MyFilePosition]
  ,[MyDbColumnName]
  ,[MyDbTableName]
  ,[MyDbName]
  ,[CreatedBy]
  ,[CreatedTimestamp]
  ,[UpdatedBy]
  ,[UpdatedTimestamp]
)
values 
(
  57
  ,'nvarchar'
  ,255
  ,7
  ,'Suffix'
  ,'adt.Ethin'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 008 of 023
-- COLUMN 008 - [Gender] [nvarchar] (255) NOT NULL  
SELECT 1;
-- 1 record 

-- BEGIN TRAN
Insert [dbo].[BulkInsertDataTableAndColumnDefinition]
(
  [BulkInsertDataTablePk] 
  ,[MyDbColumnType]
  ,[MyDbColumnLength]
  ,[MyFilePosition]
  ,[MyDbColumnName]
  ,[MyDbTableName]
  ,[MyDbName]
  ,[CreatedBy]
  ,[CreatedTimestamp]
  ,[UpdatedBy]
  ,[UpdatedTimestamp]
)
values 
(
  57
  ,'nvarchar'
  ,255
  ,8
  ,'Gender'
  ,'adt.Ethin'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 009 of 023
-- COLUMN 009 - [DateOfBirth] [datetime] NOT NULL
SELECT 1;
-- 1 record 

-- BEGIN TRAN
Insert [dbo].[BulkInsertDataTableAndColumnDefinition]
(
  [BulkInsertDataTablePk] 
  ,[MyDbColumnType]
  ,[MyDbColumnLength]
  ,[MyFilePosition]
  ,[MyDbColumnName]
  ,[MyDbTableName]
  ,[MyDbName]
  ,[CreatedBy]
  ,[CreatedTimestamp]
  ,[UpdatedBy]
  ,[UpdatedTimestamp]
)
values 
(
  57
  ,'datetime MM/dd/yyyy'
  ,50
  ,9
  ,'DateOfBirth'
  ,'adt.Ethin'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 010 of 023
-- COLUMN 010 - [DateOfDeath] [datetime] NULL  
SELECT 1;
-- 1 record 

-- BEGIN TRAN
Insert [dbo].[BulkInsertDataTableAndColumnDefinition]
(
  [BulkInsertDataTablePk] 
  ,[MyDbColumnType]
  ,[MyDbColumnLength]
  ,[MyFilePosition]
  ,[MyDbColumnName]
  ,[MyDbTableName]
  ,[MyDbName]
  ,[CreatedBy]
  ,[CreatedTimestamp]
  ,[UpdatedBy]
  ,[UpdatedTimestamp]
)
values 
(
  57
  ,'datetime MM/dd/yyyy'
  ,50
  ,10
  ,'DateOfDeath'
  ,'adt.Ethin'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 011 of 023
-- COLUMN 011 - [SendingFacility] [nvarchar] (255) NULL
SELECT 1;
-- 1 record 

-- BEGIN TRAN
Insert [dbo].[BulkInsertDataTableAndColumnDefinition]
(
  [BulkInsertDataTablePk] 
  ,[MyDbColumnType]
  ,[MyDbColumnLength]
  ,[MyFilePosition]
  ,[MyDbColumnName]
  ,[MyDbTableName]
  ,[MyDbName]
  ,[CreatedBy]
  ,[CreatedTimestamp]
  ,[UpdatedBy]
  ,[UpdatedTimestamp]
)
values 
(
  57
  ,'nvarchar'
  ,255
  ,11
  ,'SendingFacility'
  ,'adt.Ethin'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 012 of 023
-- COLUMN 012 - [AdmitTime] [datetime] NOT NULL
SELECT 1;
-- 1 record 

-- BEGIN TRAN
Insert [dbo].[BulkInsertDataTableAndColumnDefinition]
(
  [BulkInsertDataTablePk] 
  ,[MyDbColumnType]
  ,[MyDbColumnLength]
  ,[MyFilePosition]
  ,[MyDbColumnName]
  ,[MyDbTableName]
  ,[MyDbName]
  ,[CreatedBy]
  ,[CreatedTimestamp]
  ,[UpdatedBy]
  ,[UpdatedTimestamp]
)
values 
(
  57
  ,'datetime'
  ,50
  ,12
  ,'AdmitTime'
  ,'adt.Ethin'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 013 of 023
-- COLUMN 013 - [DischargeTime] [datetime] NOT NULL
SELECT 1;
-- 1 record 

-- BEGIN TRAN
Insert [dbo].[BulkInsertDataTableAndColumnDefinition]
(
  [BulkInsertDataTablePk] 
  ,[MyDbColumnType]
  ,[MyDbColumnLength]
  ,[MyFilePosition]
  ,[MyDbColumnName]
  ,[MyDbTableName]
  ,[MyDbName]
  ,[CreatedBy]
  ,[CreatedTimestamp]
  ,[UpdatedBy]
  ,[UpdatedTimestamp]
)
values 
(
  57
  ,'datetime'
  ,50
  ,13
  ,'DischargeTime'
  ,'adt.Ethin'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 014 of 023
-- COLUMN 014 - [AttendingProvider] [nvarchar] (255) NULL
SELECT 1;
-- 1 record 

-- BEGIN TRAN
Insert [dbo].[BulkInsertDataTableAndColumnDefinition]
(
  [BulkInsertDataTablePk] 
  ,[MyDbColumnType]
  ,[MyDbColumnLength]
  ,[MyFilePosition]
  ,[MyDbColumnName]
  ,[MyDbTableName]
  ,[MyDbName]
  ,[CreatedBy]
  ,[CreatedTimestamp]
  ,[UpdatedBy]
  ,[UpdatedTimestamp]
)
values 
(
  57
  ,'nvarchar'
  ,255
  ,14
  ,'AttendingProvider'
  ,'adt.Ethin'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 015 of 023
-- COLUMN 015 - [PrimaryCareProvider] [nvarchar] (255) NULL
SELECT 1;
-- 1 record 

-- BEGIN TRAN
Insert [dbo].[BulkInsertDataTableAndColumnDefinition]
(
  [BulkInsertDataTablePk] 
  ,[MyDbColumnType]
  ,[MyDbColumnLength]
  ,[MyFilePosition]
  ,[MyDbColumnName]
  ,[MyDbTableName]
  ,[MyDbName]
  ,[CreatedBy]
  ,[CreatedTimestamp]
  ,[UpdatedBy]
  ,[UpdatedTimestamp]
)
values 
(
  57
  ,'nvarchar'
  ,255
  ,15
  ,'PrimaryCareProvider'
  ,'adt.Ethin'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 016 of 023
-- COLUMN 016 - [AdmitSource] [nvarchar] (255) NULL
SELECT 1;
-- 1 record 

-- BEGIN TRAN
Insert [dbo].[BulkInsertDataTableAndColumnDefinition]
(
  [BulkInsertDataTablePk] 
  ,[MyDbColumnType]
  ,[MyDbColumnLength]
  ,[MyFilePosition]
  ,[MyDbColumnName]
  ,[MyDbTableName]
  ,[MyDbName]
  ,[CreatedBy]
  ,[CreatedTimestamp]
  ,[UpdatedBy]
  ,[UpdatedTimestamp]
)
values 
(
  57
  ,'nvarchar'
  ,255
  ,16
  ,'AdmitSource'
  ,'adt.Ethin'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 017 of 023
-- COLUMN 017 - [AdmitReason] [nvarchar] (255) NULL
SELECT 1;
-- 1 record 

-- BEGIN TRAN
Insert [dbo].[BulkInsertDataTableAndColumnDefinition]
(
  [BulkInsertDataTablePk] 
  ,[MyDbColumnType]
  ,[MyDbColumnLength]
  ,[MyFilePosition]
  ,[MyDbColumnName]
  ,[MyDbTableName]
  ,[MyDbName]
  ,[CreatedBy]
  ,[CreatedTimestamp]
  ,[UpdatedBy]
  ,[UpdatedTimestamp]
)
values 
(
  57
  ,'nvarchar'
  ,255
  ,17
  ,'AdmitReason'
  ,'adt.Ethin'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 018 of 023
-- COLUMN 018 - [DischargeStatus] [nvarchar] (255) NULL
SELECT 1;
-- 1 record 

-- BEGIN TRAN
Insert [dbo].[BulkInsertDataTableAndColumnDefinition]
(
  [BulkInsertDataTablePk] 
  ,[MyDbColumnType]
  ,[MyDbColumnLength]
  ,[MyFilePosition]
  ,[MyDbColumnName]
  ,[MyDbTableName]
  ,[MyDbName]
  ,[CreatedBy]
  ,[CreatedTimestamp]
  ,[UpdatedBy]
  ,[UpdatedTimestamp]
)
values 
(
  57
  ,'nvarchar'
  ,255
  ,18
  ,'DischargeStatus'
  ,'adt.Ethin'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 019 of 023
-- COLUMN 019 - [FinalDiagnosesList] [nvarchar] (255) NULL
SELECT 1;
-- 1 record 

-- BEGIN TRAN
Insert [dbo].[BulkInsertDataTableAndColumnDefinition]
(
  [BulkInsertDataTablePk] 
  ,[MyDbColumnType]
  ,[MyDbColumnLength]
  ,[MyFilePosition]
  ,[MyDbColumnName]
  ,[MyDbTableName]
  ,[MyDbName]
  ,[CreatedBy]
  ,[CreatedTimestamp]
  ,[UpdatedBy]
  ,[UpdatedTimestamp]
)
values 
(
  57
  ,'nvarchar'
  ,255
  ,19
  ,'FinalDiagnosesList'
  ,'adt.Ethin'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 020 of 023
-- COLUMN 020 - [Insurance] [nvarchar] (255) NULL
SELECT 1;
-- 1 record 

-- BEGIN TRAN
Insert [dbo].[BulkInsertDataTableAndColumnDefinition]
(
  [BulkInsertDataTablePk] 
  ,[MyDbColumnType]
  ,[MyDbColumnLength]
  ,[MyFilePosition]
  ,[MyDbColumnName]
  ,[MyDbTableName]
  ,[MyDbName]
  ,[CreatedBy]
  ,[CreatedTimestamp]
  ,[UpdatedBy]
  ,[UpdatedTimestamp]
)
values 
(
  57
  ,'nvarchar'
  ,255
  ,20
  ,'Insurance'
  ,'adt.Ethin'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------

-- STEP 021 of 023
-- COLUMN 998 - [AdmitOrDischarge] [nvarchar] (255) NULL  
SELECT 1;
-- 1 record 

-- BEGIN TRAN
Insert [dbo].[BulkInsertDataTableAndColumnDefinition]
(
  [BulkInsertDataTablePk] 
  ,[MyDbColumnType]
  ,[MyDbColumnLength]
  ,[MyFilePosition]
  ,[MyDbColumnName]
  ,[MyDbTableName]
  ,[MyDbName]
  ,[CreatedBy]
  ,[CreatedTimestamp]
  ,[UpdatedBy]
  ,[UpdatedTimestamp]
)
values 
(
  57
  ,'nvarchar'
  ,255
  ,998
  ,'AdmitOrDischarge'
  ,'adt.Ethin'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 022 of 023
-- COLUMN 999 - [SourceFullFilename] [nvarchar] (500)
SELECT 1;
-- 1 record 

-- BEGIN TRAN
Insert [dbo].[BulkInsertDataTableAndColumnDefinition]
(
  [BulkInsertDataTablePk] 
  ,[MyDbColumnType]
  ,[MyDbColumnLength]
  ,[MyFilePosition]
  ,[MyDbColumnName]
  ,[MyDbTableName]
  ,[MyDbName]
  ,[CreatedBy]
  ,[CreatedTimestamp]
  ,[UpdatedBy]
  ,[UpdatedTimestamp]
)
values 
(
  57
  ,'nvarchar'
  ,500
  ,999
  ,'SourceFullFilename'
  ,'adt.Ethin'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 023 of 023
-- COLUMN 1000 - [EthinID] [int]
SELECT 1;
-- 1 record 

-- BEGIN TRAN
Insert [dbo].[BulkInsertDataTableAndColumnDefinition]
(
  [BulkInsertDataTablePk] 
  ,[MyDbColumnType]
  ,[MyDbColumnLength]
  ,[MyFilePosition]
  ,[MyDbColumnName]
  ,[MyDbTableName]
  ,[MyDbName]
  ,[CreatedBy]
  ,[CreatedTimestamp]
  ,[UpdatedBy]
  ,[UpdatedTimestamp]
)
values 
(
  57
  ,'int'
  ,1
  ,1000
  ,'EthinID'
  ,'adt.Ethin'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------





