-- SQL Server Instance: smg-sql01
IF (@@SERVERNAME <> 'smg-dsdev05')
BEGIN
PRINT 'Invalid SQL Server Connection'
RETURN
END

USE [SimplifyVbcAdt8];

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'HumanaObs' AND TABLE_SCHEMA = 'dbo')
   DROP TABLE [dbo].[HumanaObs];
GO
/* -----------------------------------------------------------------------------------------------------------
   Table Name  :  HumanaObs
   Business Analyis:
   Project/Process :   
   Description     :  The Master table for the Humana Observation ADT Input file
                        
   Author          :   Philip Morrison
   Create Date     :   8/27/2025 

   ***********************************************************************************************************
   **         Change History                                                                                **
   ***********************************************************************************************************

   Date       Version    Author             Description
   --------   --------   -----------        ------------
   8/27/2025   1.01.001   Philip Morrison    Created
*/ -----------------------------------------------------------------------------------------------------------                                   

CREATE TABLE [dbo].[HumanaObs](
	[HumanaObsID] [int] IDENTITY(1,1) NOT NULL
    ,[RptDt] [datetime] NOT NULL
	,[Type] [nvarchar] (255) NULL  
	,[Group] [nvarchar] (255) NULL 
	,[GrouperName] [nvarchar] (255) NULL 
	,[FacDbName] [nvarchar] (255) NULL 
	,[PcpName] [nvarchar] (255) NULL 
	,[CaseType] [nvarchar] (255) NULL 
	,[AuthId] [nvarchar] (255) NULL 
	,[FirstDay] [datetime] NOT NULL
	,[SubscriberId] [nvarchar] (255) NULL 
	,[FirstName] [nvarchar] (255) NULL 
	,[LastName] [nvarchar] (255) NULL 
	,[DateOfBirth] [datetime] NOT NULL
	,[GrouperId] [nvarchar] (255) NULL 
	,[PcpId] [nvarchar] (255) NULL  
	,[FaxTaxId] [nvarchar] (255) NULL  
	,[AuthType] [nvarchar] (255) NULL  
	,[RequestType] [nvarchar] (255) NULL  
	,[NotifDate] [datetime] NULL  
	,[LastDay] [datetime] NOT NULL
	,[DiagCode1] [nvarchar] (255) NULL  
	,[DiagDesc1] [nvarchar] (255) NULL  
	,[DiagCode2] [nvarchar] (255) NULL  
	,[DiagDesc2] [nvarchar] (255) NULL  
	,[DiagCode3] [nvarchar] (255) NULL  
	,[DiagDesc3] [nvarchar] (255) NULL  
	,[ProcCode] [int] NULL  
	,[ProcDesc] [nvarchar] (255) NULL  
	,[Gender] [nvarchar] (255) NULL
	
	,[SourceFullFilename] [nvarchar] (500) NULL  
CONSTRAINT [pk_dboHumanaObs] PRIMARY KEY CLUSTERED 
(
	[HumanaObsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
