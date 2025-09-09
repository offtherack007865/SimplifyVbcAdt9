-- SQL Server Instance: smg-sql01
IF (@@SERVERNAME <> 'smg-sql01')
BEGIN
PRINT 'Invalid SQL Server Connection'
RETURN
END

USE [Staging];

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Humana' AND TABLE_SCHEMA = 'adt')
   DROP TABLE [adt].[Humana];
GO
/* -----------------------------------------------------------------------------------------------------------
   Table Name  :  Ethin
   Business Analyis:
   Project/Process :   
   Description     :  The Staging table for the Ethin ADT Input file
                        
   Author          :   Philip Morrison
   Create Date     :   8/27/2025 

   ***********************************************************************************************************
   **         Change History                                                                                **
   ***********************************************************************************************************

   Date       Version    Author             Description
   --------   --------   -----------        ------------
   8/27/2025   1.01.001   Philip Morrison    Created
*/ -----------------------------------------------------------------------------------------------------------                                   

CREATE TABLE [adt].[Humana](
	[HumanaID] [int] IDENTITY(1,1) NOT NULL
    ,[RptDt] [nvarchar] (MAX) NOT NULL
	,[Type] [nvarchar] (MAX) NOT NULL  
	,[Group] [nvarchar] (MAX) NOT NULL
	,[GrouperName] [nvarchar] (MAX) NOT NULL 
	,[FacDbName] [nvarchar] (MAX) NOT NULL
	,[PcpName] [nvarchar] (MAX) NOT NULL 
	,[BedType] [nvarchar] (MAX) NOT NULL 
	,[AuthId] [nvarchar] (MAX) NOT NULL
	,[AdmitDt] [nvarchar] (MAX) NOT NULL
	,[DcDate] [nvarchar] (MAX) NOT NULL
	,[DcDisposition] [nvarchar] (MAX) NOT NULL
	,[SubscriberId] [nvarchar] (MAX) NOT NULL
	,[LastName] [nvarchar] (MAX) NOT NULL
	,[FirstName] [nvarchar] (MAX) NOT NULL 
	,[DateOfBirth] [nvarchar] (MAX) NOT NULL
	,[IsCaseReAdmit14dy] [nvarchar] (MAX) NOT NULL 
	,[IsCaseReAdmit30dy] [nvarchar] (MAX) NOT NULL
	,[ReadmitScore] [nvarchar] (MAX) NOT NULL
	,[RiskScoreGroup] [nvarchar] (MAX) NOT NULL
	,[ClinicalProgram] [nvarchar] (MAX) NOT NULL
	,[CaseName] [nvarchar] (MAX) NOT NULL 
	,[CaseStatus] [nvarchar] (MAX) NOT NULL  
    ,[GrouperId] [nvarchar] (MAX) NOT NULL  
	,[PcpId] [nvarchar] (MAX) NOT NULL  
	,[FaxTaxId] [nvarchar] (MAX) NOT NULL  
	,[AdmitType] [nvarchar] (MAX) NOT NULL  
	,[RequestType] [nvarchar] (MAX) NOT NULL  
	,[NotifDate] [nvarchar] (MAX) NOT NULL  
	,[AuthorizedDays] [nvarchar] (MAX) NOT NULL  
	,[ContactType] [nvarchar] (MAX) NOT NULL  
	,[ContactName] [nvarchar] (MAX) NOT NULL  
	,[ContactPhone] [nvarchar] (MAX) NOT NULL  
	,[ContactNbrExt] [nvarchar] (MAX) NOT NULL  
	,[ContactEmail] [nvarchar] (MAX) NOT NULL  
	,[DiagCode1] [nvarchar] (MAX) NOT NULL  
	,[DiagDesc1] [nvarchar] (MAX) NOT NULL  
	,[DiagCode2] [nvarchar] (MAX) NOT NULL  
	,[DiagDesc2] [nvarchar] (MAX) NOT NULL  
	,[ProcCode1] [nvarchar] (MAX) NOT NULL  
	,[ProcDesc1] [nvarchar] (MAX) NOT NULL  
	,[ProcCode2] [nvarchar] (MAX) NOT NULL  
	,[ProcDesc2] [nvarchar] (MAX) NOT NULL  
	,[AuthSource] [nvarchar] (MAX) NOT NULL  
	,[LobDesc] [nvarchar] (MAX) NOT NULL  
	,[Gender] [nvarchar] (MAX) NOT NULL
	
	,[AdmitOrDischarge] [nvarchar] (MAX) NOT NULL
	
	,[SourceFullFilename] [nvarchar] (MAX) NOT NULL
CONSTRAINT [pk_adtHumana] PRIMARY KEY CLUSTERED 
(
	[HumanaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
