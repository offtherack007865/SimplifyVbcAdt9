-- SQL Server Instance:  smg-sql01
IF (@@SERVERNAME <> 'smg-sql01')
BEGIN
PRINT 'Invalid SQL Server Connection'
RETURN
END

USE [BulkInsert8];

-----------------------------------------------------------------------------------------------
-- STEP 001 of 082
-- Delete all records for table.
SELECT COUNT(*)
FROM [dbo].[BulkInsertDataTableAndColumnDefinition]
WHERE [BulkInsertDataTablePk] = 58;
-- 1 record 

-- BEGIN TRAN
DELETE [dbo].[BulkInsertDataTableAndColumnDefinition]
WHERE [BulkInsertDataTablePk] = 58;

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------

-- STEP 002 of 082
-- COLUMN 001 - [RptDt] [datetime] NOT NULL
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
  58
  ,'nvarchar'
  ,50
  ,1
  ,'RptDt'
  ,'adt.Humana'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN


-----------------------------------------------------------------------------------------------
-- STEP 003 of 082
-- COLUMN 002 - [Type] [nvarchar] (255) NULL 
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
  58
  ,'nvarchar'
  ,255
  ,2
  ,'Type'
  ,'adt.Humana'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN


-----------------------------------------------------------------------------------------------
-- STEP 004 of 082
-- COLUMN 003 - [Group] [nvarchar] (255) NULL 
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
  58
  ,'nvarchar'
  ,255
  ,3
  ,'Group'
  ,'adt.Humana'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN


-----------------------------------------------------------------------------------------------

-- STEP 005 of 082
-- COLUMN 004 - [GrouperName] [nvarchar] (255) NULL 
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
  58
  ,'nvarchar'
  ,255
  ,4
  ,'GrouperName'
  ,'adt.Humana'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN


-----------------------------------------------------------------------------------------------
-- STEP 006 of 082
-- COLUMN 005 - [FacDbName] [nvarchar] (255) NULL 
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
  58
  ,'nvarchar'
  ,255
  ,5
  ,'FacDbName'
  ,'adt.Humana'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN


-----------------------------------------------------------------------------------------------
-- STEP 007 of 082
-- COLUMN 006 - [PcpName] [nvarchar] (255) NULL 
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
  58
  ,'nvarchar'
  ,255
  ,6
  ,'PcpName'
  ,'adt.Humana'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN


-----------------------------------------------------------------------------------------------
-- STEP 008 of 082
-- COLUMN 007 - [BedType] [nvarchar] (255) NULL 
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
  58
  ,'nvarchar'
  ,255
  ,7
  ,'BedType'
  ,'adt.Humana'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN


-----------------------------------------------------------------------------------------------
-- STEP 009 of 082
-- COLUMN 008 - [AuthId] [nvarchar] (255) NULL 
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
  58
  ,'nvarchar'
  ,255
  ,8
  ,'AuthId'
  ,'adt.Humana'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN


-----------------------------------------------------------------------------------------------
-- STEP 010 of 082
-- COLUMN 009 - [AdmitDt] [datetime] NOT NULL
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
  58
  ,'datetime MM/dd/yyyy'
  ,50
  ,9
  ,'AdmitDt'
  ,'adt.Humana'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN


-----------------------------------------------------------------------------------------------
-- STEP 011 of 082
-- COLUMN 010 - [DcDate] [datetime] NOT NULL
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
  58
  ,'datetime MM/dd/yyyy'
  ,50
  ,10
  ,'DcDate'
  ,'adt.Humana'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN


-----------------------------------------------------------------------------------------------
-- STEP 012 of 082
-- COLUMN 011 - [DcDisposition] [nvarchar] (255) NULL 
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
  58
  ,'nvarchar'
  ,255
  ,11
  ,'DcDisposition'
  ,'adt.Humana'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN


-----------------------------------------------------------------------------------------------
-- STEP 013 of 082
-- COLUMN 012 - [SubscriberId] [nvarchar] (255) NULL 
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
  58
  ,'nvarchar'
  ,255
  ,12
  ,'SubscriberId'
  ,'adt.Humana'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN


-----------------------------------------------------------------------------------------------
-- STEP 014 of 082
-- COLUMN 013 - [LastName] [nvarchar] (255) NULL 
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
  58
  ,'nvarchar'
  ,255
  ,13
  ,'LastName'
  ,'adt.Humana'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 015 of 082
-- COLUMN 014 - [FirstName] [nvarchar] (255) NULL 
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
  58
  ,'nvarchar'
  ,255
  ,14
  ,'FirstName'
  ,'adt.Humana'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 016 of 082
-- COLUMN 015 - [DateOfBirth] [datetime] NOT NULL
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
  58
  ,'datetime MM/dd/yyyy'
  ,50
  ,15
  ,'DateOfBirth'
  ,'adt.Humana'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 017 of 082
-- COLUMN 016 - [IsCaseReAdmit14dy] [nvarchar] (255) NULL 
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
  58
  ,'nvarchar'
  ,255
  ,16
  ,'IsCaseReAdmit14dy'
  ,'adt.Humana'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 018 of 082
-- COLUMN 017 - [IsCaseReAdmit30dy] [nvarchar] (255) NULL 
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
  58
  ,'nvarchar'
  ,255
  ,17
  ,'IsCaseReAdmit30dy'
  ,'adt.Humana'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 019 of 082
-- COLUMN 018 - [ReadmitScore] [int] NULL 
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
  58
  ,'int'
  ,10
  ,18
  ,'ReadmitScore'
  ,'adt.Humana'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 020 of 082
-- COLUMN 019 - [RiskScoreGroup] [nvarchar] (255) NULL  
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
  58
  ,'nvarchar'
  ,255
  ,19
  ,'RiskScoreGroup'
  ,'adt.Humana'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 021 of 082
-- COLUMN 020 - [ClinicalProgram] [nvarchar] (255) NULL  
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
  58
  ,'nvarchar'
  ,255
  ,20
  ,'ClinicalProgram'
  ,'adt.Humana'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 022 of 082
-- COLUMN 021 - [CaseName] [nvarchar] (255) NULL  
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
  58
  ,'nvarchar'
  ,255
  ,21
  ,'CaseName'
  ,'adt.Humana'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 023 of 082
-- COLUMN 022 - [CaseStatus] [nvarchar] (255) NULL  
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
  58
  ,'nvarchar'
  ,255
  ,22
  ,'CaseStatus'
  ,'adt.Humana'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 024 of 082
-- COLUMN 023 - [GrouperId] [nvarchar] (255) NULL  
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
  58
  ,'nvarchar'
  ,255
  ,23
  ,'GrouperId'
  ,'adt.Humana'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 025 of 082
-- COLUMN 024 - [PcpId] [nvarchar] (255) NULL  
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
  58
  ,'nvarchar'
  ,255
  ,24
  ,'PcpId'
  ,'adt.Humana'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 026 of 082
-- COLUMN 025 - [FaxTaxId] [nvarchar] (255) NULL  
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
  58
  ,'nvarchar'
  ,255
  ,25
  ,'FaxTaxId'
  ,'adt.Humana'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 027 of 082
-- COLUMN 026 - [AdmitType] [nvarchar] (255) NULL  
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
  58
  ,'nvarchar'
  ,255
  ,26
  ,'AdmitType'
  ,'adt.Humana'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 028 of 082
-- COLUMN 027 - [RequestType] [nvarchar] (255) NULL  
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
  58
  ,'nvarchar'
  ,255
  ,27
  ,'RequestType'
  ,'adt.Humana'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 029 of 082
-- COLUMN 028 - [NotifDate] [datetime] NULL  
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
  58
  ,'datetime MM/dd/yyyy'
  ,255
  ,28
  ,'NotifDate'
  ,'adt.Humana'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 030 of 082
-- COLUMN 029 - [AuthorizedDays] [int] NULL  
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
  58
  ,'int'
  ,10
  ,29
  ,'AuthorizedDays'
  ,'adt.Humana'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 031 of 082
-- COLUMN 030 - [ContactType] [nvarchar] (255) NULL  
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
  58
  ,'nvarchar'
  ,255
  ,30
  ,'ContactType'
  ,'adt.Humana'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 032 of 082
-- COLUMN 031 - [ContactName] [nvarchar] (255) NULL  
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
  58
  ,'nvarchar'
  ,255
  ,31
  ,'ContactName'
  ,'adt.Humana'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 033 of 082
-- COLUMN 032 - [ContactPhone] [nvarchar] (255) NULL  
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
  58
  ,'nvarchar'
  ,255
  ,32
  ,'ContactPhone'
  ,'adt.Humana'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 034 of 082
-- COLUMN 033 - [ContactNbrExt] [nvarchar] (255) NULL  
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
  58
  ,'nvarchar'
  ,255
  ,33
  ,'ContactNbrExt'
  ,'adt.Humana'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 035 of 082
-- COLUMN 034 - [ContactEmail] [nvarchar] (255) NULL  
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
  58
  ,'nvarchar'
  ,255
  ,34
  ,'ContactEmail'
  ,'adt.Humana'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 036 of 082
-- COLUMN 035 - [DiagCode1] [nvarchar] (255) NULL  
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
  58
  ,'nvarchar'
  ,255
  ,35
  ,'DiagCode1'
  ,'adt.Humana'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 037 of 082
-- COLUMN 036 - [DiagDesc1] [nvarchar] (255) NULL  
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
  58
  ,'nvarchar'
  ,255
  ,36
  ,'DiagDesc1'
  ,'adt.Humana'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 038 of 082
-- COLUMN 037 - [DiagCode2] [nvarchar] (255) NULL  
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
  58
  ,'nvarchar'
  ,255
  ,37
  ,'DiagCode2'
  ,'adt.Humana'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN

-----------------------------------------------------------------------------------------------

-- STEP 039 of 082
-- COLUMN 038 - [DiagDesc2] [nvarchar] (255) NULL  
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
  58
  ,'nvarchar'
  ,255
  ,38
  ,'DiagDesc2'
  ,'adt.Humana'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 040 of 082
-- COLUMN 039 - [ProcCode1] [int] NULL  
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
  58
  ,'int'
  ,10
  ,39
  ,'ProcCode1'
  ,'adt.Humana'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 041 of 082
-- COLUMN 040 - [ProcDesc1] [nvarchar] (255) NULL  
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
  58
  ,'nvarchar'
  ,255
  ,40
  ,'ProcDesc1'
  ,'adt.Humana'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 042 of 082
-- COLUMN 041 - [ProcCode2] [int] NULL  
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
  58
  ,'int'
  ,10
  ,41
  ,'ProcCode2'
  ,'adt.Humana'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 043 of 082
-- COLUMN 042 - [ProcDesc2] [nvarchar] (255) NULL  
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
  58
  ,'nvarchar'
  ,255
  ,42
  ,'ProcDesc2'
  ,'adt.Humana'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 044 of 082
-- COLUMN 043 - [AuthSource] [nvarchar] (255) NULL  
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
  58
  ,'nvarchar'
  ,255
  ,43
  ,'AuthSource'
  ,'adt.Humana'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 045 of 082
-- COLUMN 044 - [LobDesc] [nvarchar] (255) NULL  
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
  58
  ,'nvarchar'
  ,255
  ,44
  ,'LobDesc'
  ,'adt.Humana'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------
-- STEP 046 of 082
-- COLUMN 045 - [Gender] [nvarchar] (255) NULL	
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
  58
  ,'nvarchar'
  ,255
  ,45
  ,'Gender'
  ,'adt.Humana'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------

-- STEP 047 of 082
-- COLUMN 46 - [AdmitOrDischarge] [nvarchar] (255) NULL
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
  58
  ,'nvarchar'
  ,255
  ,46
  ,'AdmitOrDischarge'
  ,'adt.Humana'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN


-----------------------------------------------------------------------------------------------
-- STEP 048 of 082
-- COLUMN 999 - [SourceFullFilename] [nvarchar] (500) NULL  
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
  58
  ,'nvarchar'
  ,500
  ,999
  ,'SourceFullFilename'
  ,'adt.Humana'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN

-----------------------------------------------------------------------------------------------
-- STEP 050 of 082
-- COLUMN 1082 - [HumanaID] [int]
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
  58
  ,'int'
  ,1
  ,1082
  ,'HumanaID'
  ,'adt.Humana'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------





























-----------------------------------------------------------------------------------------------
/*
CREATE TABLE [adt].[HumanaObs](

001. [RptDt] [datetime] NOT NULL
002. [Type] [nvarchar] (255) NULL  
003. [Group] [nvarchar] (255) NULL 
004. [GrouperName] [nvarchar] (255) NULL 
005. [FacDbName] [nvarchar] (255) NULL 
006. [PcpName] [nvarchar] (255) NULL 
007. [CaseType] [nvarchar] (255) NULL 
008. [AuthId] [nvarchar] (255) NULL 
009. [FirstDay] [datetime] NOT NULL
010. [SubscriberId] [nvarchar] (255) NULL 
011. [FirstName] [nvarchar] (255) NULL 
012. [LastName] [nvarchar] (255) NULL 
013. [DateOfBirth] [datetime] NOT NULL
014. [GrouperId] [nvarchar] (255) NULL 
015. [PcpId] [nvarchar] (255) NULL  
016. [FaxTaxId] [nvarchar] (255) NULL  
017. [AuthType] [nvarchar] (255) NULL  
018. [RequestType] [nvarchar] (255) NULL  
019. [NotifDate] [datetime] NULL  
020. [LastDay] [datetime] NOT NULL
021. [DiagCode1] [nvarchar] (255) NULL  
022. [DiagDesc1] [nvarchar] (255) NULL  
023. [DiagCode2] [nvarchar] (255) NULL  
024. [DiagDesc2] [nvarchar] (255) NULL  
025. [DiagCode3] [nvarchar] (255) NULL  
026. [DiagDesc3] [nvarchar] (255) NULL  
027. [ProcCode] [int] NULL  
028. [ProcDesc] [nvarchar] (255) NULL  
029. [Gender] [nvarchar] (255) NULL
	
	,[SourceFullFilename] [nvarchar] (500) NULL  */
    
-- STEP 051 of 082
-- Delete all records for table.
SELECT COUNT(*)
FROM [dbo].[BulkInsertDataTableAndColumnDefinition]
WHERE [BulkInsertDataTablePk] = 59;
-- 1 record 

-- BEGIN TRAN
DELETE [dbo].[BulkInsertDataTableAndColumnDefinition]
WHERE [BulkInsertDataTablePk] = 59;

-- COMMIT TRAN
-- ROLLBACK TRAN
-----------------------------------------------------------------------------------------------

-- STEP 052 of 082
-- COLUMN 001 - [RptDt] [datetime] NOT NULL
-----------------------------------------------------------------------------------------------
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
  59
  ,'datetime MM/dd/yyyy'
  ,50
  ,1
  ,'RptDt'
  ,'adt.HumanaObs'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN


-----------------------------------------------------------------------------------------------

-- STEP 053 of 082
-- COLUMN 002 - [Type] [nvarchar] (255) NULL
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
  59
  ,'nvarchar'
  ,255
  ,2
  ,'Type'
  ,'adt.HumanaObs'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN


-----------------------------------------------------------------------------------------------

-- STEP 54 of 082
-- COLUMN 003 - [Group] [nvarchar] (255) NULL
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
  59
  ,'nvarchar'
  ,255
  ,3
  ,'Group'
  ,'adt.HumanaObs'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN


-----------------------------------------------------------------------------------------------

-- STEP 055 of 082
-- COLUMN 004 - [GrouperName] [nvarchar] (255) NULL
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
  59
  ,'nvarchar'
  ,255
  ,4
  ,'GrouperName'
  ,'adt.HumanaObs'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN


-----------------------------------------------------------------------------------------------

-- STEP 056 of 082
-- COLUMN 005 - [FacDbName] [nvarchar] (255) NULL
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
  59
  ,'nvarchar'
  ,255
  ,5
  ,'FacDbName'
  ,'adt.HumanaObs'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN


-----------------------------------------------------------------------------------------------

-- STEP 057 of 082
-- COLUMN 006 - [PcpName] [nvarchar] (255) NULL
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
  59
  ,'nvarchar'
  ,255
  ,6
  ,'PcpName'
  ,'adt.HumanaObs'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN


-----------------------------------------------------------------------------------------------

-- STEP 058 of 082
-- COLUMN 007 - [CaseType] [nvarchar] (255) NULL
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
  59
  ,'nvarchar'
  ,255
  ,7
  ,'CaseType'
  ,'adt.HumanaObs'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN


-----------------------------------------------------------------------------------------------

-- STEP 059 of 082
-- COLUMN 008 - [AuthId] [nvarchar] (255) NULL
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
  59
  ,'nvarchar'
  ,255
  ,8
  ,'AuthId'
  ,'adt.HumanaObs'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN


-----------------------------------------------------------------------------------------------

-- STEP 060 of 082
-- COLUMN 009 - [FirstDay] [datetime] NOT NULL
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
  59
  ,'datetime MM/dd/yyyy'
  ,50
  ,9
  ,'FirstDay'
  ,'adt.HumanaObs'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN


-----------------------------------------------------------------------------------------------

-- STEP 061 of 082
-- COLUMN 010 - [SubscriberId] [nvarchar] (255) NULL
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
  59
  ,'nvarchar'
  ,255
  ,10
  ,'SubscriberId'
  ,'adt.HumanaObs'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN


-----------------------------------------------------------------------------------------------

-- STEP 062 of 082
-- COLUMN 011 - [FirstName] [nvarchar] (255) NULL
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
  59
  ,'nvarchar'
  ,255
  ,11
  ,'FirstName'
  ,'adt.HumanaObs'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN


-----------------------------------------------------------------------------------------------

-- STEP 063 of 082
-- COLUMN 012 - [LastName] [nvarchar] (255) NULL
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
  59
  ,'nvarchar'
  ,255
  ,12
  ,'LastName'
  ,'adt.HumanaObs'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN


-----------------------------------------------------------------------------------------------

-- STEP 064 of 082
-- COLUMN 013 - [DateOfBirth] [datetime] NOT NULL
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
  59
  ,'datetime MM/dd/yyyy'
  ,50
  ,13
  ,'DateOfBirth'
  ,'adt.HumanaObs'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN


-----------------------------------------------------------------------------------------------

-- STEP 065 of 082
-- COLUMN 014 - [GrouperId] [nvarchar] (255) NULL
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
  59
  ,'nvarchar'
  ,255
  ,14
  ,'GrouperId'
  ,'adt.HumanaObs'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN


-----------------------------------------------------------------------------------------------

-- STEP 066 of 082
-- COLUMN 015 - [PcpId] [nvarchar] (255) NULL
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
  59
  ,'nvarchar'
  ,255
  ,15
  ,'PcpId'
  ,'adt.HumanaObs'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN


-----------------------------------------------------------------------------------------------

-- STEP 067 of 082
-- COLUMN 016 - [FaxTaxId] [nvarchar] (255) NULL  
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
  59
  ,'nvarchar'
  ,255
  ,16
  ,'FaxTaxId'
  ,'adt.HumanaObs'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN


-----------------------------------------------------------------------------------------------

-- STEP 068 of 082
-- COLUMN 0017 - [AuthType] [nvarchar] (255) NULL
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
  59
  ,'nvarchar'
  ,255
  ,17
  ,'AuthType'
  ,'adt.HumanaObs'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN


-----------------------------------------------------------------------------------------------

-- STEP 069 of 082
-- COLUMN 018 - [RequestType] [nvarchar] (255) NULL
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
  59
  ,'nvarchar'
  ,255
  ,18
  ,'RequestType'
  ,'adt.HumanaObs'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN


-----------------------------------------------------------------------------------------------

-- STEP 070 of 082
-- COLUMN 019 - [NotifDate] [datetime] NULL
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
  59
  ,'datetime MM/dd/yyyy'
  ,50
  ,19
  ,'NotifDate'
  ,'adt.HumanaObs'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN


-----------------------------------------------------------------------------------------------

-- STEP 071 of 082
-- COLUMN 020 - [LastDay] [datetime] NOT NULL
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
  59
  ,'datetime MM/dd/yyyy'
  ,50
  ,20
  ,'LastDay'
  ,'adt.HumanaObs'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN


-----------------------------------------------------------------------------------------------

-- STEP 072 of 082
-- COLUMN 021 - [DiagCode1] [nvarchar] (255) NULL
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
  59
  ,'nvarchar'
  ,255
  ,21
  ,'DiagCode1'
  ,'adt.HumanaObs'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN


-----------------------------------------------------------------------------------------------

-- STEP 073 of 082
-- COLUMN 022 - [DiagDesc1] [nvarchar] (255) NULL
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
  59
  ,'nvarchar'
  ,255
  ,22
  ,'DiagDesc1'
  ,'adt.HumanaObs'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN


-----------------------------------------------------------------------------------------------

-- STEP 074 of 082
-- COLUMN 023 - [DiagCode2] [nvarchar] (255) NULL
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
  59
  ,'nvarchar'
  ,255
  ,23
  ,'DiagCode2'
  ,'adt.HumanaObs'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN


-----------------------------------------------------------------------------------------------

-- STEP 075 of 082
-- COLUMN 024 - [DiagDesc2] [nvarchar] (255) NULL
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
  59
  ,'nvarchar'
  ,255
  ,24
  ,'DiagDesc2'
  ,'adt.HumanaObs'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN


-----------------------------------------------------------------------------------------------

-- STEP 076 of 082
-- COLUMN 025 - [DiagCode3] [nvarchar] (255) NULL  
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
  59
  ,'nvarchar'
  ,255
  ,25
  ,'DiagCode3'
  ,'adt.HumanaObs'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN


-----------------------------------------------------------------------------------------------

-- STEP 077 of 082
-- COLUMN 026 - [DiagDesc3] [nvarchar] (255) NULL
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
  59
  ,'nvarchar'
  ,255
  ,26
  ,'DiagDesc3'
  ,'adt.HumanaObs'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN


-----------------------------------------------------------------------------------------------

-- STEP 078 of 082
-- COLUMN 027 - [ProcCode] [int] NULL
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
  59
  ,'nvarchar'
  ,255
  ,27
  ,'ProcCode'
  ,'adt.HumanaObs'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN


-----------------------------------------------------------------------------------------------

-- STEP 079 of 082
-- COLUMN 028 - [ProcDesc] [nvarchar] (255) NULL
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
  59
  ,'nvarchar'
  ,255
  ,28
  ,'ProcDesc'
  ,'adt.HumanaObs'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN


-----------------------------------------------------------------------------------------------

-- STEP 080 of 082
-- COLUMN 029 - [Gender] [nvarchar] (MAX) NOT NULL
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
  59
  ,'nvarchar'
  ,255
  ,29
  ,'Gender'
  ,'adt.HumanaObs'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN


-----------------------------------------------------------------------------------------------

-- STEP 081 of 082
-- COLUMN 999 - [SourceFullFilename] [nvarchar] (500) NULL
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
  59
  ,'nvarchar'
  ,500
  ,999
  ,'SourceFullFilename'
  ,'adt.HumanaObs'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN


-----------------------------------------------------------------------------------------------

-- STEP 082 of 082
-- COLUMN 1082 - [HumanaObsID] [int] IDENTITY(1,1) NOT NULL
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
  59
  ,'int'
  ,10
  ,1082
  ,'HumanaObsID'
  ,'adt.HumanaObs'
  ,'Staging'
  ,'pwmorrison'
  ,getdate()
  ,'pwmorrison'
  ,getdate()
);

-- COMMIT TRAN
-- ROLLBACK TRAN


-----------------------------------------------------------------------------------------------



