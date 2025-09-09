-- SQL Server Instance: smg-sql01
IF (@@SERVERNAME <> 'smg-sql01')
BEGIN
PRINT 'Invalid SQL Server Connection'
RETURN
END

USE [SimplifyVbcAdt8];

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Humana' AND TABLE_SCHEMA = 'dbo')
   DROP TABLE [dbo].[Humana];
GO
/* -----------------------------------------------------------------------------------------------------------
   Table Name  :  Humana
   Business Analyis:
   Project/Process :   
   Description     :  The Master table for the Humana ADT Input file
                        
   Author          :   Philip Morrison
   Create Date     :   8/27/2025 

   ***********************************************************************************************************
   **         Change History                                                                                **
   ***********************************************************************************************************

   Date       Version    Author             Description
   --------   --------   -----------        ------------
   8/27/2025   1.01.001   Philip Morrison    Created
*/ -----------------------------------------------------------------------------------------------------------                                   


CREATE TABLE [dbo].[Humana](
	[HumanaID] [int] IDENTITY(1,1) NOT NULL
    ,[RptDt] [datetime] NOT NULL
	,[Type] [nvarchar] (255) NULL  
	,[Group] [nvarchar] (255) NULL 
	,[GrouperName] [nvarchar] (255) NULL 
	,[FacDbName] [nvarchar] (255) NULL 
	,[PcpName] [nvarchar] (255) NULL 
	,[BedType] [nvarchar] (255) NULL 
	,[AuthId] [nvarchar] (255) NULL 
	,[AdmitDt] [datetime] NOT NULL
	,[DcDate] [datetime] NOT NULL
	,[DcDisposition] [nvarchar] (255) NULL 
	,[SubscriberId] [nvarchar] (255) NULL 
	,[LastName] [nvarchar] (255) NULL 
	,[FirstName] [nvarchar] (255) NULL 
	,[DateOfBirth] [datetime] NOT NULL
	,[IsCaseReAdmit14dy] [nvarchar] (255) NULL 
	,[IsCaseReAdmit30dy] [nvarchar] (255) NULL 
	,[ReadmitScore] [int] NULL 
	,[RiskScoreGroup] [nvarchar] (255) NULL  
	,[ClinicalProgram] [nvarchar] (255) NULL  
	,[CaseName] [nvarchar] (255) NULL  
	,[CaseStatus] [nvarchar] (255) NULL  
    ,[GrouperId] [nvarchar] (255) NULL  
	,[PcpId] [nvarchar] (255) NULL  
	,[FaxTaxId] [nvarchar] (255) NULL  
	,[AdmitType] [nvarchar] (255) NULL  
	,[RequestType] [nvarchar] (255) NULL  
	,[NotifDate] [datetime] NULL  
	,[AuthorizedDays] [int] NULL  
	,[ContactType] [nvarchar] (255) NULL  
	,[ContactName] [nvarchar] (255) NULL  
	,[ContactPhone] [nvarchar] (255) NULL  
	,[ContactNbrExt] [nvarchar] (255) NULL  
	,[ContactEmail] [nvarchar] (255) NULL  
	,[DiagCode1] [nvarchar] (255) NULL  
	,[DiagDesc1] [nvarchar] (255) NULL  
	,[DiagCode2] [nvarchar] (255) NULL  
	,[DiagDesc2] [nvarchar] (255) NULL  
	,[ProcCode1] [int] NULL  
	,[ProcDesc1] [nvarchar] (255) NULL  
	,[ProcCode2] [int] NULL  
	,[ProcDesc2] [nvarchar] (255) NULL  
	,[AuthSource] [nvarchar] (255) NULL  
	,[LobDesc] [nvarchar] (255) NULL  
	,[Gender] [nvarchar] (255) NULL	
	
	,[AdmitOrDischarge] [nvarchar] (255) NULL  
	
	,[SourceFullFilename] [nvarchar] (500) NULL  
CONSTRAINT [pk_dboHumana] PRIMARY KEY CLUSTERED 
(
	[HumanaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
