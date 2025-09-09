-- SQL Server Instance: smg-sql01
IF (@@SERVERNAME <> 'smg-sql01')
BEGIN
PRINT 'Invalid SQL Server Connection'
RETURN
END

USE [Staging];

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'HumanaObs' AND TABLE_SCHEMA = 'adt')
   DROP TABLE [adt].[HumanaObs];
GO
/* -----------------------------------------------------------------------------------------------------------
   Table Name  :  HumanaObs
   Business Analyis:
   Project/Process :   
   Description     :  The Staging table for the Humana Observation ADT Input file
                        
   Author          :   Philip Morrison
   Create Date     :   8/27/2025 

   ***********************************************************************************************************
   **         Change History                                                                                **
   ***********************************************************************************************************

   Date       Version    Author             Description
   --------   --------   -----------        ------------
   8/27/2025   1.01.001   Philip Morrison    Created
*/ -----------------------------------------------------------------------------------------------------------                                   

CREATE TABLE [adt].[HumanaObs](
	[HumanaObsID] [int] IDENTITY(1,1) NOT NULL
    ,[RptDt] [nvarchar] (MAX) NOT NULL
	,[Type] [nvarchar] (MAX) NOT NULL
	,[Group] [nvarchar] (MAX) NOT NULL 
	,[GrouperName] [nvarchar] (MAX) NOT NULL
	,[FacDbName] [nvarchar] (MAX) NOT NULL 
	,[PcpName] [nvarchar] (MAX) NOT NULL 
	,[CaseType] [nvarchar] (MAX) NOT NULL 
	,[AuthId] [nvarchar] (MAX) NOT NULL 
	,[FirstDay] [nvarchar] (MAX) NOT NULL
	,[SubscriberId] [nvarchar] (MAX) NOT NULL
	,[FirstName] [nvarchar] (MAX) NOT NULL 
	,[LastName] [nvarchar] (MAX) NOT NULL 
	,[DateOfBirth] [nvarchar] (MAX) NOT NULL
	,[GrouperId] [nvarchar] (MAX) NOT NULL 
	,[PcpId] [nvarchar] (MAX) NOT NULL 
	,[FaxTaxId] [nvarchar] (MAX) NOT NULL  
	,[AuthType] [nvarchar] (MAX) NOT NULL  
	,[RequestType] [nvarchar] (MAX) NOT NULL  
	,[NotifDate] [nvarchar] (MAX) NOT NULL  
	,[LastDay] [nvarchar] (MAX) NOT NULL
	,[DiagCode1] [nvarchar] (MAX) NOT NULL  
	,[DiagDesc1] [nvarchar] (MAX) NOT NULL  
	,[DiagCode2] [nvarchar] (MAX) NOT NULL  
	,[DiagDesc2] [nvarchar] (MAX) NOT NULL  
	,[DiagCode3] [nvarchar] (MAX) NOT NULL  
	,[DiagDesc3] [nvarchar] (MAX) NOT NULL  
	,[ProcCode] [nvarchar] (MAX) NOT NULL  
	,[ProcDesc] [nvarchar] (MAX) NOT NULL  
	,[Gender] [nvarchar] (MAX) NOT NULL
	,[SourceFullFilename] [nvarchar] (MAX) NOT NULL  
CONSTRAINT [pk_adtHumanaObsHumanaObsID] PRIMARY KEY CLUSTERED 
(
	[HumanaObsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
