
using SimplifyVbcAdt9.CallWebApiLand;
using SimplifyVbcAdt9.Data.Models;

using EmailWebApiLand9.CallEmailWebApiLand;
using EmailWebApiLand9.Data.Models;

using log4net;

using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using SimplifyVbcAdt9.EthinConsoleApp;

namespace SimplifyVbcAdt9.EthinConsoleApp
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
            ReadInEthinConfigOptions
                myReadInEthinConfigOptions =
                    new ReadInEthinConfigOptions(config);
            EthinConfigOptions
                myConfigOptions =
                    myReadInEthinConfigOptions.ReadIn();

            // Get Config Options from the database.
            CallWebApiLandClass
                myCallForGetOptions =
                    new CallWebApiLandClass
                        (
                            myConfigOptions.ConfigOptionsBaseWebUrl
                        );

            qy_GetEthinConfigOutput
                myqy_GetEthinConfigOutput =
                    myCallForGetOptions.qy_GetEthinConfig();

            if (!myqy_GetEthinConfigOutput.IsOk ||
                myqy_GetEthinConfigOutput
                .qy_GetEthinConfigOutputColumnsList
                .Count != 1)
            {
                log.Error($"We had an error in trying to get the configuration file from the database:  {myqy_GetEthinConfigOutput.ErrorMessage}");
                return;
            }


            // Main Operations.
            EthinMainOps
                myEthinMainOps =
                    new EthinMainOps
                        (
                            myqy_GetEthinConfigOutput
                            .qy_GetEthinConfigOutputColumnsList[0]
                        );

            EthinMainOpsOutput
                myEthinMainOpsOutput =
                    myEthinMainOps.DoIt();
            if (!myEthinMainOpsOutput.IsOk)
            {
                log.Error(myEthinMainOpsOutput.ErrorMessage);
                return;
            }

            if (myEthinMainOpsOutput.NumberOfFilesProcessed > 0)
            {
                string mySubjectLine = "Successfully formatted and sent Ethin ADT Files For Import Processing.";
                string myBody = $"Successfully formatted and sent {myEthinMainOpsOutput.NumberOfFilesProcessed.ToString()} Ethin ADT Files For Import Processing.";

                string[] myEmailAddressArray =
                    "pwmorrison@summithealthcare.com"
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
                        myqy_GetEthinConfigOutput
                        .qy_GetEthinConfigOutputColumnsList [0] 
                        .EmailFromAddress;

                string myEmailWebApiDotNet7BaseUrl =
                        myqy_GetEthinConfigOutput
                        .qy_GetEthinConfigOutputColumnsList[0]
                        .EmailBaseWebApiUrl;

                // Email the notifyees.
                CallEmailWebApiLand
                    myCallEmailWebApiLand9 =
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
                        myCallEmailWebApiLand9.CallIHtmlStringBody();
                if (!myEmailSendWithHtmlStringOutput.IsOk)
                {
                    log.Error($"Error upon trying to invoke the Email Web Api with Url:  {myEmailWebApiDotNet7BaseUrl} and subject line of {mySubjectLine}");
                    return;
                }
            }
        }
    }
}

