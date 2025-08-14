-- SQL Server Instance: smg-sql01
USE [BulkInsert8];

-- STEP 001 of 035
-- Delete all Column Definitions for PointClickCare
SELECT COUNT(*)
FROM [dbo].[BulkInsertDataTableAndColumnDefinition]
WHERE [BulkInsertDataTablePk] = 56;
-- 1 record 

-- BEGIN TRAN
DELETE [dbo].[BulkInsertDataTableAndColumnDefinition]
WHERE [BulkInsertDataTablePk] = 56;

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------

-- STEP 002 of 035
-- COLUMN 001 -- [LastName] [nvarchar] (50) NOT NULL
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
  56
  ,'nvarchar'
  ,50
  ,1
  ,'LastName'
  ,'adt.PointClickCare'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);


-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------

-- STEP 003 of 035
-- COLUMN 002 -- [FirstName] [nvarchar] (50) NOT NULL
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
  56
  ,'nvarchar'
  ,50
  ,2
  ,'FirstName'
  ,'adt.PointClickCare'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------

-- STEP 004 of 035
-- COLUMN 003 -- [SenderSourceCode] [nvarchar] (100) NOT NULL
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
  56
  ,'nvarchar'
  ,100
  ,3
  ,'SenderSourceCode'
  ,'adt.PointClickCare'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------

-- STEP 005 of 035
-- COLUMN 004 -- [SenderMrn] [nvarchar] (50) NOT NULL
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
  56
  ,'nvarchar'
  ,50
  ,4
  ,'SenderMrn'
  ,'adt.PointClickCare'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------

-- STEP 006 of 035
-- COLUMN 005 -- [SubscriberMrn] [nvarchar] (50) NOT NULL
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
  56
  ,'nvarchar'
  ,50
  ,5
  ,'SubscriberMrn'
  ,'adt.PointClickCare'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------

-- STEP 007 of 035
-- COLUMN 006 -- [FacilityName] [nvarchar] (300) NOT NULL
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
  56
  ,'nvarchar'
  ,300
  ,6
  ,'FacilityName'
  ,'adt.PointClickCare'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------

-- STEP 008 of 035
-- COLUMN 007 -- [Gender] [nvarchar] (50) NOT NULL
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
  56
  ,'nvarchar'
  ,50
  ,7
  ,'Gender'
  ,'adt.PointClickCare'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------

-- STEP 009 of 035
-- COLUMN 008 -- [DateOfBirth] [nvarchar] (50) NOT NULL
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
  56
  ,'nvarchar'
  ,50
  ,8
  ,'DateOfBirth'
  ,'adt.PointClickCare'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------

-- STEP 010 of 035
-- COLUMN 009 -- [EventTime] [nvarchar] (50) NOT NULL
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
  56
  ,'nvarchar'
  ,50
  ,9
  ,'EventTime'
  ,'adt.PointClickCare'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------

-- STEP 011 of 035
-- COLUMN 010 -- [AlertType] [nvarchar] (50) NOT NULL
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
  56
  ,'nvarchar'
  ,50
  ,10
  ,'AlertType'
  ,'adt.PointClickCare'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------

-- STEP 012 of 035
-- COLUMN 011 -- [HospitalService] [nvarchar] (50) NOT NULL
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
  56
  ,'nvarchar'
  ,50
  ,11
  ,'HospitalService'
  ,'adt.PointClickCare'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);


-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------

-- STEP 013 of 035
-- COLUMN 012 -- [AdmitSource] [nvarchar] (30) NOT NULL
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
  56
  ,'nvarchar'
  ,30
  ,12
  ,'AdmitSource'
  ,'adt.PointClickCare'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);


-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------

-- STEP 014 of 035
-- COLUMN 013 -- [AdmitDate] [nvarchar] (50) NOT NULL
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
  56
  ,'nvarchar'
  ,50
  ,13
  ,'AdmitDate'
  ,'adt.PointClickCare'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------

-- STEP 015 of 035
-- COLUMN 014 -- [PatientComplaints] [nvarchar] (250) NOT NULL
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
  56
  ,'nvarchar'
  ,250
  ,14
  ,'PatientComplaints'
  ,'adt.PointClickCare'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);


-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 016 of 035
-- COLUMN 015 -- [DiagnosisCode] [nvarchar] (150) NOT NULL
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
  56 -- [BulkInsertDataTablePk] [bigint] NOT NULL
  ,'nvarchar'
  ,150
  ,15
  ,'DiagnosisCode'
  ,'adt.PointClickCare'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);


-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 017 of 035
-- COLUMN 016 -- [DiagnosisDescription] [nvarchar] (250) NOT NULL
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
  56
  ,'nvarchar'
  ,250
  ,16
  ,'DiagnosisDescription'
  ,'adt.PointClickCare'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------

-- STEP 018 of 035
-- COLUMN 017 -- [DischargeDate] [nvarchar] (50) NOT NULL
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
  56
  ,'nvarchar'
  ,50
  ,17
  ,'DischargeDate'
  ,'adt.PointClickCare'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------

-- STEP 019 of 035
-- COLUMN 018 -- [DischargeLocation] [nvarchar] (250) NOT NULL
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
  56
  ,'nvarchar'
  ,250
  ,18
  ,'DischargeLocation'
  ,'adt.PointClickCare'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------

-- STEP 020 of 035
-- COLUMN 019 -- [DischargeDisposition] [nvarchar] (250) NOT NULL
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
  56
  ,'nvarchar'
  ,250
  ,19
  ,'DischargeDisposition'
  ,'adt.PointClickCare'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);


-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------

-- STEP 021 of 035
-- COLUMN 020 -- [DeathIndicator] [nvarchar] (30) NOT NULL
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
  56
  ,'nvarchar'
  ,30
  ,20
  ,'DeathIndicator'
  ,'adt.PointClickCare'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------

-- STEP 022 of 035
-- COLUMN 021 -- [PatientClass] [nvarchar] (50) NOT NULL
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
  56
  ,'nvarchar'
  ,50
  ,21
  ,'PatientClass'
  ,'adt.PointClickCare'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------

-- STEP 023 of 035
-- COLUMN 022 -- [PatientClassDescription] [nvarchar] (250) NOT NULL
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
  56
  ,'nvarchar'
  ,250
  ,22
  ,'PatientClassDescription'
  ,'adt.PointClickCare'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------

-- STEP 024 of 035
-- COLUMN 023 -- [PrimaryCareProvider] [nvarchar] (250) NOT NULL
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
  56
  ,'nvarchar'
  ,250
  ,23
  ,'PrimaryCareProvider'
  ,'adt.PointClickCare'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------

-- STEP 025 of 035
-- COLUMN 024 -- [Insurance] [nvarchar] (150) NOT NULL
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
  56
  ,'nvarchar'
  ,150
  ,24
  ,'Insurance'
  ,'adt.PointClickCare'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------

-- STEP 026 of 035
-- COLUMN 025 -- [Practice] [nvarchar] (250) NOT NULL
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
  56
  ,'nvarchar'
  ,250
  ,25
  ,'Practice'
  ,'adt.PointClickCare'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------

-- STEP 027 of 035
-- COLUMN 026 -- [Address1] [nvarchar] (150) NOT NULL
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
  56
  ,'nvarchar'
  ,150
  ,26
  ,'Address1'
  ,'adt.PointClickCare'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------

-- STEP 028 of 035
-- COLUMN 027 -- [Address2] [nvarchar] (150) NOT NULL
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
  56
  ,'nvarchar'
  ,150
  ,27
  ,'Address2'
  ,'adt.PointClickCare'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------

-- STEP 029 of 035
-- COLUMN 028 -- [State] [nvarchar] (50) NOT NULL
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
  56
  ,'nvarchar'
  ,50
  ,28
  ,'State'
  ,'adt.PointClickCare'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------

-- STEP 030 of 035
-- COLUMN 029 -- [Zipcode] [nvarchar] (50) NOT NULL
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
  56
  ,'nvarchar'
  ,50
  ,29
  ,'Zipcode'
  ,'adt.PointClickCare'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);


-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------

-- STEP 031 of 035
-- COLUMN 030 -- [NumberOfErVisits] [nvarchar] (50) NOT NULL
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
  56
  ,'nvarchar'
  ,50
  ,30
  ,'NumberOfErVisits'
  ,'adt.PointClickCare'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);


-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------

-- STEP 032 of 035
-- COLUMN 031 -- [NumberOfIpVisits] [nvarchar] (50) NOT NULL
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
  56
  ,'nvarchar'
  ,50
  ,31
  ,'NumberOfIpVisits'
  ,'adt.PointClickCare'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);


-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------

-- STEP 033 of 035
-- COLUMN 032 -- [HomePhone] [nvarchar] (50) NOT NULL
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
  56
  ,'nvarchar'
  ,50
  ,32
  ,'HomePhone'
  ,'adt.PointClickCare'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------

-- STEP 034 of 035
-- COLUMN 999 - [ImportFullFilename] [nvarchar] (300) NOT NULL
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
  56
  ,'nvarchar'
  ,300
  ,999
  ,'ImportFullFilename'
  ,'adt.PointClickCare'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------

-- STEP 035 of 035
-- COLUMN 1000 - [PointClickCareID] [int]
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
  56
  ,'int'
  ,1
  ,1000
  ,'PointClickCareID'
  ,'adt.PointClickCare'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------




