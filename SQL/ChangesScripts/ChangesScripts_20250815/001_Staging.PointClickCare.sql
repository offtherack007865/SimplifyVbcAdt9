-- SQL Instance Name:  smg-sql01
USE [Staging];
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PointClickCare' AND TABLE_SCHEMA = 'adt')
   DROP TABLE [adt].[PointClickCare];
GO
/* -----------------------------------------------------------------------------------------------------------
   Table Name  :  PointClickCare
   Business Analyis:
   Project/Process :   
   Description     :  The Staging table for the  PointClickCare ADT Input file
                        
   Author          :   Philip Morrison
   Create Date     :   8/5/2025 

   ***********************************************************************************************************
   **         Change History                                                                                **
   ***********************************************************************************************************

   Date       Version    Author             Description
   --------   --------   -----------        ------------
   8/5/2025   1.01.001   Philip Morrison    Created
   8/14/2025  1.01.002   Philip Morrison    Eliminate Columns EventType and EventTypeDescription.
*/ -----------------------------------------------------------------------------------------------------------                                   


CREATE TABLE [adt].[PointClickCare]
(
  [PointClickCareID] [int] IDENTITY(1,1) NOT NULL
  ,[LastName] [nvarchar] (MAX) NOT NULL
  ,[FirstName] [nvarchar] (MAX) NOT NULL
  ,[SenderSourceCode] [nvarchar] (MAX) NOT NULL
  ,[SenderMrn] [nvarchar] (MAX) NOT NULL
  ,[SubscriberMrn] [nvarchar] (MAX) NOT NULL
  ,[FacilityName] [nvarchar] (MAX) NOT NULL
  ,[Gender] [nvarchar] (MAX) NOT NULL
  ,[DateOfBirth] [nvarchar] (MAX) NOT NULL
  ,[EventTime] [nvarchar] (MAX) NOT NULL
  ,[AlertType] [nvarchar] (MAX) NOT NULL
  ,[HospitalService] [nvarchar] (MAX) NOT NULL
  ,[AdmitSource] [nvarchar] (MAX) NOT NULL
  ,[AdmitDate] [nvarchar] (MAX) NOT NULL
  ,[PatientComplaints] [nvarchar] (MAX) NOT NULL
  ,[DiagnosisCode] [nvarchar] (MAX) NOT NULL
  ,[DiagnosisDescription] [nvarchar] (MAX) NOT NULL
  ,[DischargeDate] [nvarchar] (MAX) NOT NULL
  ,[DischargeLocation] [nvarchar] (MAX) NOT NULL
  ,[DischargeDisposition] [nvarchar] (MAX) NOT NULL
  ,[DeathIndicator] [nvarchar] (MAX) NOT NULL
  ,[PatientClass] [nvarchar] (MAX) NOT NULL
  ,[PatientClassDescription] [nvarchar] (MAX) NOT NULL
  ,[PrimaryCareProvider] [nvarchar] (MAX) NOT NULL
  ,[Insurance] [nvarchar] (MAX) NOT NULL
  ,[Practice] [nvarchar] (MAX) NOT NULL
  ,[Address1] [nvarchar] (MAX) NOT NULL
  ,[Address2] [nvarchar] (MAX) NOT NULL
  ,[State] [nvarchar] (MAX) NOT NULL
  ,[Zipcode] [nvarchar] (MAX) NOT NULL
  ,[NumberOfErVisits] [nvarchar] (MAX) NOT NULL
  ,[NumberOfIpVisits] [nvarchar] (MAX) NOT NULL
  ,[HomePhone] [nvarchar] (MAX) NOT NULL
  ,[ImportFullFilename] [nvarchar] (MAX) NOT NULL
CONSTRAINT [pk_adtPointClickCare] PRIMARY KEY CLUSTERED 
(
	[PointClickCareID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
