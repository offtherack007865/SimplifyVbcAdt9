-- SQL Instance Name:  smg-sql01
USE [SimplifyVbcAdt8];
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PointClickCare' AND TABLE_SCHEMA = 'dbo')
   DROP TABLE [dbo].[PointClickCare];
GO
/* -----------------------------------------------------------------------------------------------------------
   Table Name  :  PointClickCare
   Business Analyis:
   Project/Process :   
   Description     :  The Master table for the PointClickCare ADT Input file
                        
   Author          :   Philip Morrison
   Create Date     :   8/5/2025 

   ***********************************************************************************************************
   **         Change History                                                                                **
   ***********************************************************************************************************

   Date       Version    Author             Description
   --------   --------   -----------        ------------
   8/5/2025   1.01.001   Philip Morrison    Created
*/ -----------------------------------------------------------------------------------------------------------                                   


CREATE TABLE [dbo].[PointClickCare]
(
  [PointClickCareID] [int] IDENTITY(1,1) NOT NULL
  ,[LastName] [nvarchar] (50) NOT NULL
  ,[FirstName] [nvarchar] (50) NOT NULL
  ,[SenderSourceCode] [nvarchar] (100) NOT NULL
  ,[SenderMrn] [nvarchar] (50) NOT NULL
  ,[SubscriberMrn] [nvarchar] (50) NOT NULL
  ,[FacilityName] [nvarchar] (300) NOT NULL
  ,[Gender] [nvarchar] (50) NOT NULL
  ,[DateOfBirth] [datetime] NOT NULL
  ,[EventTime] [datetime] NOT NULL
  ,[AlertType] [nvarchar] (50) NOT NULL
  ,[HospitalService] [nvarchar] (50) NOT NULL
  ,[AdmitSource] [nvarchar] (30) NOT NULL
  ,[AdmitDate] [datetime] NOT NULL
  ,[PatientComplaints] [nvarchar] (250) NOT NULL
  ,[DiagnosisCode] [nvarchar] (150) NOT NULL
  ,[DiagnosisDescription] [nvarchar] (250) NOT NULL
  ,[DischargeDate] [datetime] NOT NULL
  ,[DischargeLocation] [nvarchar] (250) NOT NULL
  ,[DischargeDisposition] [nvarchar] (250) NOT NULL
  ,[DeathIndicator] [nvarchar] (30) NOT NULL
  ,[PatientClass] [nvarchar] (50) NOT NULL
  ,[PatientClassDescription] [nvarchar] (250) NOT NULL
  ,[PrimaryCareProvider] [nvarchar] (250) NOT NULL
  ,[Insurance] [nvarchar] (150) NOT NULL
  ,[Practice] [nvarchar] (250) NOT NULL
  ,[Address1] [nvarchar] (150) NOT NULL
  ,[Address2] [nvarchar] (150) NOT NULL
  ,[State] [nvarchar] (50) NOT NULL
  ,[Zipcode] [nvarchar] (50) NOT NULL
  ,[NumberOfErVisits] [nvarchar] (50) NOT NULL
  ,[NumberOfIpVisits] [nvarchar] (50) NOT NULL
  ,[HomePhone] [nvarchar] (50) NOT NULL
  ,[ImportFullFilename] [nvarchar] (300) NOT NULL
CONSTRAINT [pk_dboPointClickCare] PRIMARY KEY CLUSTERED 
(
	[PointClickCareID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
