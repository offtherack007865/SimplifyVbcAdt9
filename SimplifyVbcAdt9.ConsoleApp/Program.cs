﻿using BulkInsert9.Data.Models;
using EmailWebApiLand9.CallEmailWebApiLand;
using EmailWebApiLand9.Data;
using EmailWebApiLand9.Data.Models;

using log4net;

using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SimplifyVbcAdt9.CallWebApiLand;
using SimplifyVbcAdt9.Data.Models;

using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace SimplifyVbcAdt9.ConsoleApp
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));

        // Limit Program to run one instance only.
        public static Process PriorProcess()
        // Returns a System.Diagnostics.Process pointing to
        // a pre-existing process with the same name as the
        // current one, if any; or null if the current process
        // is unique.
        {
            Process curr = Process.GetCurrentProcess();
            Process[] procs = Process.GetProcessesByName(curr.ProcessName);
            foreach (Process p in procs)
            {
                if ((p.Id != curr.Id) &&
                    (p.MainModule.FileName == curr.MainModule.FileName))
                    return p;
            }
            return null;
        }
        public static void Main(string[] args)
        {
            if (PriorProcess() != null)
            {

                log.Error("Another instance of the app is already running.");
                return;
            }

            // configure logging via log4net
            string log4netConfigFullFilename =
                Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "log4net.config");
            var fileInfo = new FileInfo(log4netConfigFullFilename);
            if (fileInfo.Exists)
                log4net.Config.XmlConfigurator.Configure(fileInfo);
            else
                throw new InvalidOperationException("No log config file found");


            // Build a config object, using env vars and JSON providers.
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile(SimplifyVbcAdt9.Data.MyConstants.AppSettingsFile)
                .Build();

            // Read in Configuration Options for this Console Application
            ReadInConfigOptions myReadInConfigOptions = new ReadInConfigOptions(config);
            SimplifyVbcAdt9.Data.Models.ConfigOptions
                myConfigOptions =
                    myReadInConfigOptions.ReadIn();

            // Get Config Options from the database.
            CallWebApiLandClass
                myCallForGetOptions =
                    new CallWebApiLandClass
                        (
                            myConfigOptions.BaseWebUrl
                        );

            qy_GetHumanaCensusConfigOutput
                myqy_GetHumanaCensusConfigOutput =
                    myCallForGetOptions
                    .qy_GetHumanaCensusConfig
                    (
                        myConfigOptions.DbConfigSettingsApplication
                        , myConfigOptions.DbConfigSettingsType
                        , myConfigOptions.DbConfigSettingsProcess
                        , myConfigOptions.DbConfigSettingsNameFilter
                        , myConfigOptions.DbConfigSettingsUser
                    );
            if (!myqy_GetHumanaCensusConfigOutput.IsOk ||
                myqy_GetHumanaCensusConfigOutput.qy_GetHumanaCensusConfigOutputColumnsList.Count != 1)
            {
                log.Error($"We had an error in trying to get the configuration file from the database:  {myqy_GetHumanaCensusConfigOutput.ErrorMessage}");
                return;
            }

            GetSourceFileList
                myCallToGetSourceFileList =
                    new GetSourceFileList
                    (
                        myqy_GetHumanaCensusConfigOutput.qy_GetHumanaCensusConfigOutputColumnsList[0]
                    );
            myCallToGetSourceFileList.DoIt();

            if (myCallToGetSourceFileList.MyOutputListOfFullFilenames.Count == 0)
            {
                log.Info($"No files to process.");
                return;
            }

            // Main Operations.
            MainOps
                myMainOps =
                    new MainOps
                    (
                        myqy_GetHumanaCensusConfigOutput.qy_GetHumanaCensusConfigOutputColumnsList[0]
                        , myCallToGetSourceFileList.MyOutputListOfFullFilenames
                    );

            MainOpsOutput
                myMainOutput =
                    myMainOps.MyMain();
            if (!myMainOutput.IsOk)
            {
                log.Error(myMainOutput.ErrorMessage);
                return;
            }

            if (myMainOutput.MailBodyLineList.Count > 0)
            {
                string mySubjectLine = "Humana Census file(s) were successfully imported and looked up.";
                string myBody = string.Join("<br>", myMainOutput.MailBodyLineList);   //myMainOutput.EmailBody;
                string emailAddressListString = "pwmorrison@summithealthcare.com";
                string[] myEmailAddressArray =
                    emailAddressListString
                    .Split(";");

                List<string> myEmailAddressList =
                    new List<string>();
                foreach (string loopEmailAddress in myEmailAddressArray)
                {
                    string trimmedEmailAddress = loopEmailAddress.Trim();
                    if (trimmedEmailAddress.Length > 0)
                    {
                        myEmailAddressList.Add(trimmedEmailAddress);
                    }
                }

                string myFromEmailAddress =
                    myqy_GetHumanaCensusConfigOutput
                    .qy_GetHumanaCensusConfigOutputColumnsList[0]
                    .EmailFromAddress;

                string myEmailWebApiDotNet7BaseUrl =
                    myqy_GetHumanaCensusConfigOutput
                    .qy_GetHumanaCensusConfigOutputColumnsList[0]
                    .EmailBaseWebApiUrl;

                // Email the notifyees.
                CallEmailWebApiLand myCallEmailWebApi =
                    new CallEmailWebApiLand
                    (
                        mySubjectLine // string inputEemailSubject
                        , myBody // List<string> inputEmailBodyLineList
                        , myEmailAddressList // List<string> inputEmailAddressList
                        , myFromEmailAddress // string inputFromEmailAddress
                        , myEmailWebApiDotNet7BaseUrl // string inputEmailWebApiBaseUrl
                        , new List<string>() //List<string> inputAttachmentList
                    );
                EmailSendWithHtmlStringOutput
                    myEmailSendWithHtmlStringOutput =
                        myCallEmailWebApi.CallIHtmlStringBody();
                if (!myEmailSendWithHtmlStringOutput.IsOk)
                {
                    log.Error($"Error upon trying to invoke the Email Web Api.  Error Message:  {myEmailSendWithHtmlStringOutput}");
                    return;
                }
            }
        }
    }
}