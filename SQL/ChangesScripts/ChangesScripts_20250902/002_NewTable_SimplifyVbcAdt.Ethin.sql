-- SQL Server Instance: smg-sql01
IF (@@SERVERNAME <> 'smg-sql01')
BEGIN
PRINT 'Invalid SQL Server Connection'
RETURN
END

USE [SimplifyVbcAdt8];

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Ethin' AND TABLE_SCHEMA = 'dbo')
   DROP TABLE [dbo].[Ethin];
GO
/* -----------------------------------------------------------------------------------------------------------
   Table Name  :  Ethin
   Business Analyis:
   Project/Process :   
   Description     :  The Master table for the Ethin ADT Input file
                        
   Author          :   Philip Morrison
   Create Date     :   8/27/2025 

   ***********************************************************************************************************
   **         Change History                                                                                **
   ***********************************************************************************************************

   Date       Version    Author             Description
   --------   --------   -----------        ------------
   8/27/2025   1.01.001   Philip Morrison    Created
*/ -----------------------------------------------------------------------------------------------------------                                   


CREATE TABLE [dbo].[Ethin](
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
CONSTRAINT [PK_dboEthin] PRIMARY KEY CLUSTERED 
(
	[EthinID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
