using BulkInsert9.CallWebApiLand;
using BulkInsert9.Data.Models;
using log4net;
using SimplifyVbcAdt9.Data.Models;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SimplifyVbcAdt9.PointClickCareConsoleApp
{
    public class MainOps
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MainOps));
        public MainOps
                (
                    qy_GetPointClickCareConfigOutputColumns inputConfigOps
                    , List<string> inputFullFilenameList
                )
        {
            MyConfigOptions = inputConfigOps;
            MyFullFilenameList = inputFullFilenameList;
        }
        public qy_GetPointClickCareConfigOutputColumns MyConfigOptions { get; set; }
        public string MyFullArchiveFilename { get; set; }
        public List<string> MyFullFilenameList { get; set; }


        public MainOpsOutput MyMain()
        {
            MainOpsOutput returnOutput = new MainOpsOutput();

            foreach (string loopFullFilename in MyFullFilenameList)
            {
                ProcessSingleFileOutput
                    myProcessSingleFileOutput =
                        ProcessSingleFile(loopFullFilename);
                if (myProcessSingleFileOutput.EmailBodyLine.Length > 0)
                {
                    returnOutput.MailBodyLineList.Add(myProcessSingleFileOutput.EmailBodyLine);
                }
            }

            return returnOutput;
        }
        public ProcessSingleFileOutput ProcessSingleFile(string inputFilename)
        {
            ProcessSingleFileOutput returnOutput = new ProcessSingleFileOutput();

            // new PointClickCare filename:  PointClickCare Discharges MM.dd.yy-MM.dd.yy.xslx
            string newPointClickCareFilename =
                BuildNewPointClickCareFilename(inputFilename);

            Regex lastDateRegEx = new Regex(@"(?<MM>\d{2}).(?<dd>\d{2}).(?<yy>\d{2}).xlsx$");
            Match lastDateMatch = lastDateRegEx.Match(newPointClickCareFilename);

            string MMstring = string.Empty;
            string ddstring = string.Empty;
            string yystring = string.Empty;
            DateTime fileDate = DateTime.MinValue;
            if (lastDateMatch.Success)
            {
                MMstring = lastDateMatch.Groups["MM"].Value.Trim();
                ddstring = lastDateMatch.Groups["dd"].Value.Trim();
                yystring = lastDateMatch.Groups["yy"].Value.Trim();

                DateTime.TryParse($"{MMstring}/{ddstring}/20{yystring}", out fileDate);
            }
            if (fileDate == DateTime.MinValue)
            {
                if (File.Exists(inputFilename))
                {
                    File.Delete(inputFilename);
                }
                return returnOutput;
            }

            FileInfo inputFullFilenameFi = new FileInfo(newPointClickCareFilename);

            // Copy old PointClickCare discharges file to Input Archive Directory
            string inputArchiveFullFilename =
                Path.Combine(MyConfigOptions.InputFileArchiveDirectory, inputFullFilenameFi.Name);
            if (File.Exists(inputArchiveFullFilename))
            {
                if (File.Exists(inputArchiveFullFilename))
                {
                    File.Delete(inputArchiveFullFilename);
                }
            }
            File.Copy(inputFilename, inputArchiveFullFilename);

            // If the filename already exists in the "already imported archive", do nothing.
            string inputAlreadyImportedFullFilename =
                Path.Combine(MyConfigOptions.ImportArchiveFolder, inputFullFilenameFi.Name);
            if (File.Exists(inputAlreadyImportedFullFilename))
            {
                if (File.Exists(inputFilename))
                {
                    File.Delete(inputFilename);
                }
                return returnOutput;
            }

            // Copy the template to the output folder.
            FileInfo newPointClickCareFi = new FileInfo(newPointClickCareFilename);
            string outputArchiveFullFilename =
                Path.Combine
                (
                    MyConfigOptions.OutputFileArchiveDirectory
                    , newPointClickCareFi.Name
                );

            if (File.Exists(outputArchiveFullFilename))
            {
                File.Delete(outputArchiveFullFilename);
            }
            File.Copy
            (
                MyConfigOptions.ExcelTemplateFullFilename
                , outputArchiveFullFilename
            );

            // Extract Excel data
            ExtractExcelDataFromSingleFileOutput
                myExtractExcelDataFromSingleFileOutput =
                    ExtractExcelDataFromSingleFileSingleFile(inputArchiveFullFilename);
            if (!myExtractExcelDataFromSingleFileOutput.IsOk)
            {
                returnOutput.IsOk = false;
                returnOutput.ErrorMessage = myExtractExcelDataFromSingleFileOutput.ErrorMessage;
                return returnOutput;
            }

            // Truncate Raw table.
            dd_PointClickCareOutput
                mydd_TruncatePointClickCareOutput =
                    TruncatePointClickCareRaw();
            if (!mydd_TruncatePointClickCareOutput.IsOk)
            {
                returnOutput.IsOk = false;
                returnOutput.ErrorMessage =
                    mydd_TruncatePointClickCareOutput.ErrorMessage;
                return returnOutput;
            }

            // Bulk Insert 
            BulkInsertOutput
                myBulkInsertPointClickCareDataOutput =
                    BulkInsertPointClickCareRawData
                    (
                        myExtractExcelDataFromSingleFileOutput.MyCsvLineList
                        , inputArchiveFullFilename
                    );

            if (!myBulkInsertPointClickCareDataOutput.IsOk)
            {
                returnOutput.IsOk = false;
                returnOutput.ErrorMessage =
                    myBulkInsertPointClickCareDataOutput.ErrorMessage;
                return returnOutput;
            }

            // Finalize CollectionsRaw table.
            di_PointClickCareOutput
                mydi_FinalizePointClickCareOutput =
                    FinalizePointClickCareRaw(inputArchiveFullFilename);
            if (!mydi_FinalizePointClickCareOutput.IsOk)
            {
                returnOutput.IsOk = false;
                returnOutput.ErrorMessage =
                    mydi_FinalizePointClickCareOutput.ErrorMessage;
                return returnOutput;
            }

            // Get PointClickCare rows
            qy_GetPointClickCareOutput
                mySpGetPointClickCareOutput =
                    GetPointClickCare();
            if (!mySpGetPointClickCareOutput.IsOk)
            {
                returnOutput.IsOk = false;
                returnOutput.ErrorMessage =
                    mySpGetPointClickCareOutput.ErrorMessage;
                return returnOutput;
            }

            // Populate the cells of the target spreadsheet.
            PopulateExcelTemplateWithPointClickCareDataOutput
                myPopulateExcelTemplateWithPointClickCareDataOutput =
                    PopulateExcelTemplateWithPointClickCareMasterData
                    (
                        outputArchiveFullFilename
                        , mySpGetPointClickCareOutput.qy_GetPointClickCareOutputColumnsList
                    );
            if (!myPopulateExcelTemplateWithPointClickCareDataOutput.IsOk)
            {
                returnOutput.IsOk = false;
                returnOutput.ErrorMessage =
                    myPopulateExcelTemplateWithPointClickCareDataOutput.ErrorMessage;
                return returnOutput;
            }

            // Move new PointClickCare discharges file to Simple File Mover Read Directory
            string toSimpleFileMoverReadDirectoryFullFilename =
                Path.Combine(this.MyConfigOptions.ToSimpleFileMoverReadDirectory, newPointClickCareFi.Name);
            if (File.Exists(toSimpleFileMoverReadDirectoryFullFilename))
            {
                File.Delete(toSimpleFileMoverReadDirectoryFullFilename);
            }
            File.Copy(outputArchiveFullFilename, toSimpleFileMoverReadDirectoryFullFilename);

            //Delete old PointClickCare filename
            if (File.Exists(inputFilename))
            {
                File.Delete(inputFilename);
            }
            returnOutput.EmailBodyLine = $"Processed file {inputFullFilenameFi.Name}";
            return returnOutput;
        }

        public string BuildNewPointClickCareFilename(string inputFullFilename)
        {
            FileInfo myFi = new FileInfo(inputFullFilename);


            Regex dateRegEx = new Regex(@"(?<MM>\d{2}).(?<dd>\d{2}).(?<yy>\d{2}).xlsx");
            Match dateMatch = dateRegEx.Match(myFi.Name);

            string MMString = string.Empty;
            string ddString = string.Empty;
            string yyString = string.Empty;
            if (dateMatch.Success)
            {
                MMString = dateMatch.Groups["MM"].Value.Trim();
                ddString = dateMatch.Groups["dd"].Value.Trim();
                yyString = dateMatch.Groups["yy"].Value.Trim();
            }
            else
            {
                DateTime lastUpdateTimestamp = myFi.LastWriteTime;
                MMString = lastUpdateTimestamp.Month.ToString().PadLeft(2, '0');
                ddString = lastUpdateTimestamp.Day.ToString().PadLeft(2, '0');
                yyString = lastUpdateTimestamp.Year.ToString().Substring(2, 2);
            }

            DateTime currentFileDate = new DateTime(1900, 1, 1);
            DateTime.TryParse($"{MMString}/{ddString}/20{yyString}", out currentFileDate);
            if (currentFileDate == DateTime.MinValue || currentFileDate == new DateTime(1900, 1, 1))
            {
                DateTime currentDateTime = new DateTime();

                currentFileDate = new DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day);
            }
            DateTime oneDayBackFileDate = currentFileDate.AddDays(-1);
            string newFilename =
                Path.Combine
                (
                    this.MyConfigOptions.OutputFileArchiveDirectory
                    , $"PointClickCare Discharges {oneDayBackFileDate.Month.ToString().PadLeft(2, '0')}.{oneDayBackFileDate.Day.ToString().PadLeft(2, '0')}.{oneDayBackFileDate.Year.ToString().Substring(2, 2)}-{currentFileDate.Month.ToString().PadLeft(2, '0')}.{currentFileDate.Day.ToString().PadLeft(2, '0')}.{currentFileDate.Year.ToString().Substring(2, 2)}.xlsx"
                );

            return newFilename;
        }
        public Worksheet GetWorksheetByName(Workbook inputWorkbook, string inputWorksheetName)
        {
            Worksheet returnWorksheet = null;

            returnWorksheet = (Worksheet?)inputWorkbook.Worksheets.Where(w => w.Name.CompareTo(inputWorksheetName) == 0).FirstOrDefault();

            return returnWorksheet;
        }

        public ExtractExcelDataFromSingleFileOutput ExtractExcelDataFromSingleFileSingleFile(string inputFullFilename)
        {
            ExtractExcelDataFromSingleFileOutput returnOutput = new ExtractExcelDataFromSingleFileOutput();

            log.Info("B4 init of Workbook}");

            // Load workbook
            Workbook workbook = new Workbook();

            log.Info("B4 Load of workbook {inputFullFilename}");

            workbook.LoadFromFile(inputFullFilename);

            log.Info("b4 Load of worksheet");

            // Single "Discharges" worksheet.
            List<Worksheet> relevantWorksheetList = new List<Worksheet>();
            relevantWorksheetList.Add(workbook.Worksheets[0]);


            log.Info("after Load of worksheet");

            returnOutput.MyCsvLineList = new List<string>();
            foreach (Worksheet loopWorksheet in relevantWorksheetList)
            {
                List<string> PointClickCareFileColumnNameList =
                    GetPointClickCareFileColumnNameList();
                ExtractDataFromPointClickCareExcelRowOutput myExtractDataFromPointClickCareExcelRowOutput =
                    new ExtractDataFromPointClickCareExcelRowOutput();
                int rowCtr = 0;
                rowCtr = this.MyConfigOptions.StartLineNumber;
                while (true)
                {
                    if (rowCtr == 1198)
                    {
                        int i = 0;
                        i++;
                    }
                    myExtractDataFromPointClickCareExcelRowOutput =
                        new ExtractDataFromPointClickCareExcelRowOutput();
                    myExtractDataFromPointClickCareExcelRowOutput =
                        ExtractDataFromExcelRow
                        (
                            loopWorksheet
                            , rowCtr
                            , PointClickCareFileColumnNameList
                        );
                    if (!myExtractDataFromPointClickCareExcelRowOutput.IsOk)
                    {
                        returnOutput.IsOk = false;
                        returnOutput.ErrorMessage = myExtractDataFromPointClickCareExcelRowOutput.ErrorMessage;
                        returnOutput.MyCsvLineList = new List<string>();
                        return returnOutput;
                    }
                    if (myExtractDataFromPointClickCareExcelRowOutput.OutputCsvString.Replace("~", "").Replace("NaN", "").Length == 0)
                    {
                        break;
                    }

                    string finalRowValue = $"~,{myExtractDataFromPointClickCareExcelRowOutput.OutputCsvString}";
                    returnOutput.MyCsvLineList.Add(finalRowValue);
                    rowCtr++;
                }
            }
            return returnOutput;
        }
        public ExtractDataFromPointClickCareExcelRowOutput ExtractDataFromExcelRow(Worksheet inputWorksheet, int inputRowCtr, List<string> inputFileColumnList)
        {
            ExtractDataFromPointClickCareExcelRowOutput returnOutput = new ExtractDataFromPointClickCareExcelRowOutput();

            List<string> PointClickCareFileColumnNameList = new List<string>();

            List<ExcelCellStringValue> myExcelCellStringValueList =
                new List<ExcelCellStringValue>();


            ExcelCellStringValue myExcelCellStringValue = null;
            int oneBasedColCtr = 0;
            for (int colCtr = 0; colCtr < inputFileColumnList.Count; colCtr++)
            {
                oneBasedColCtr = colCtr + 1;
                myExcelCellStringValue =
                    new ExcelCellStringValue
                        (
                            inputWorksheet
                            , this.MyConfigOptions
                            , inputFileColumnList[colCtr]
                            , inputRowCtr
                            , oneBasedColCtr
                         );
                myExcelCellStringValueList.Add(myExcelCellStringValue);
            }

            StringBuilder outputCsvStringBuilder = new StringBuilder();
            foreach (ExcelCellStringValue loopCellValue in myExcelCellStringValueList)
            {
                GetExcelCellStringValueOutput
                    myCellOutput =
                        loopCellValue.GetExcelCellStringValue();
                if (!myCellOutput.IsOk)
                {
                    returnOutput.IsOk = false;
                    returnOutput.ErrorMessage = myCellOutput.ErrorMessage;
                    returnOutput.OutputCsvString = string.Empty;
                    return returnOutput;
                }

                if (outputCsvStringBuilder.Length > 0)
                {
                    outputCsvStringBuilder.Append(",");
                }
                if (myCellOutput.OutputStringValue.Length > 0)
                {
                    outputCsvStringBuilder.Append(myCellOutput.OutputStringValue);
                }
            }

            returnOutput.OutputCsvString =
                outputCsvStringBuilder.ToString();

            return returnOutput;
        }

        public List<string> GetPointClickCareFileColumnNameList()
        {
            List<string> returnOutput = new List<string>();

            returnOutput.Add("LastName");
            returnOutput.Add("FirstName");
            returnOutput.Add("SenderSourceCode");
            returnOutput.Add("SenderMrn");
            returnOutput.Add("SubscriberMrn");
            returnOutput.Add("FacilityName");
            returnOutput.Add("Gender");
            returnOutput.Add("DateOfBirth");
            returnOutput.Add("EventTime");
            returnOutput.Add("AlertType");
            returnOutput.Add("HospitalService");
            returnOutput.Add("AdmitSource");
            returnOutput.Add("AdmitDate");
            returnOutput.Add("PatientComplaints");
            returnOutput.Add("DiagnosisCode");
            returnOutput.Add("DiagnosisDescription");
            returnOutput.Add("DischargeDate");
            returnOutput.Add("DischargeLocation");
            returnOutput.Add("DischargeDisposition");
            returnOutput.Add("DeathIndicator");
            returnOutput.Add("PatientClass");
            returnOutput.Add("PatientClassDescription");
            returnOutput.Add("PrimaryCareProvider");
            returnOutput.Add("Insurance");
            returnOutput.Add("Practice");
            returnOutput.Add("Address1");
            returnOutput.Add("Address2");
            returnOutput.Add("State");
            returnOutput.Add("Zipcode");
            returnOutput.Add("NumberOfErVisits");
            returnOutput.Add("NumberOfIpVisits");
            returnOutput.Add("HomePhone");

            return returnOutput;
        }
        public dd_PointClickCareOutput TruncatePointClickCareRaw()
        {
            dd_PointClickCareOutput returnOutput =
                new dd_PointClickCareOutput();

            CallWebApiLand.CallWebApiLandClass myCallMyWebApiLandClass =
                new CallWebApiLand.CallWebApiLandClass(this.MyConfigOptions.SimplifyVbcAdtBaseWebApiUrl);
            dd_PointClickCareOutput
                mydd_TruncatePointClickCareOutput =
                    myCallMyWebApiLandClass.dd_PointClickCare();
            if (!mydd_TruncatePointClickCareOutput.IsOk)
            {
                returnOutput.IsOk = false;
                returnOutput.ErrorMessage =
                    mydd_TruncatePointClickCareOutput.ErrorMessage;
                return returnOutput;
            }
            return returnOutput;
        }
        public BulkInsertOutput BulkInsertPointClickCareRawData(List<string> inputCsvLineList, string inputSourceFullFilename)
        {
            BulkInsertOutput returnOutput =
                new BulkInsertOutput();

            // Make sure the Pk column is in the non-CSV column list.
            List<NonCsvFileColumnDefAndValue>
                myNonCsvFileColumnDefAndValueList =
                    new List<NonCsvFileColumnDefAndValue>();

            // SourceFullFilename
            spGetColumnDefsForGivenDbAndTableName_OutputColumns myPkColDef =
                new spGetColumnDefsForGivenDbAndTableName_OutputColumns
                {
                    MyDbColumnName = "ImportFullFilename",
                    MyDbColumnLength = 300,
                    MyDbColumnType = "nvarchar",
                    MyDbName = "Staging",
                    MyDbTableName = "adt.PointClickCare",
                    MyFilePosition = 999
                };
            NonCsvFileColumnDefAndValue myPkNonCsvFileColumnDefAndValue =
                            new NonCsvFileColumnDefAndValue
                            {
                                MyColDef = myPkColDef,
                                MyValueString = inputSourceFullFilename
                            };
            myNonCsvFileColumnDefAndValueList.Add(myPkNonCsvFileColumnDefAndValue);


            // Pk
            myPkColDef =
                new spGetColumnDefsForGivenDbAndTableName_OutputColumns
                {
                    MyDbColumnName = "PointClickCareID",
                    MyDbColumnLength = 1,
                    MyDbColumnType = "int",
                    MyDbName = "Staging",
                    MyDbTableName = "adt.PointClickCare",
                    MyFilePosition = 1000
                };
            myPkNonCsvFileColumnDefAndValue =
                new NonCsvFileColumnDefAndValue
                {
                    MyColDef = myPkColDef,
                    MyValueString = "0"
                };
            myNonCsvFileColumnDefAndValueList.Add(myPkNonCsvFileColumnDefAndValue);

            // Call Web API Endpoint to perform the Bulk Insert of Tcareb_Patient_Appt_Raw
            CallBulkInsertWebApiLand
                myCallBulkInsert =
                new CallBulkInsertWebApiLand
                (
                    MyConfigOptions.BulkInsertConnectionString //string inputDbConnectionString
                   , MyConfigOptions.BulkInsertDbName // string inputDbName
                   , MyConfigOptions.BulkInsertDbTableName // string inputDbTableName
                   , inputCsvLineList // List<string> inputCsvLineList
                   , myNonCsvFileColumnDefAndValueList // List<NonCsvFileColumnDefAndValue> inputNonCsvFileColumnDefAndValueList
                   , MyConfigOptions.BulkInsertBaseWebApiUrl // string inputBulkInsertWebApiBaseUrl
                );

            BulkInsertOutput myBulkInsertOutput =
                myCallBulkInsert.CallIt();

            if (!myBulkInsertOutput.IsOk)
            {
                returnOutput.IsOk = false;
                returnOutput.ErrorMessage = myBulkInsertOutput.ErrorMessage;
                return returnOutput;
            }
            return returnOutput;
        }
        public di_PointClickCareOutput
                    FinalizePointClickCareRaw
                    (
                        string inputFilename
                    )
        {
            di_PointClickCareOutput returnOutput =
                new di_PointClickCareOutput();
            CallWebApiLand.CallWebApiLandClass
                myCallMyWebApiLandClass =
                    new CallWebApiLand.CallWebApiLandClass(this.MyConfigOptions.SimplifyVbcAdtBaseWebApiUrl);

            returnOutput =
                    myCallMyWebApiLandClass.di_PointClickCare(inputFilename);
            return returnOutput;
        }

        public qy_GetPointClickCareOutput
                    GetPointClickCare
                    (
                    )
        {
            qy_GetPointClickCareOutput returnOutput =
                new qy_GetPointClickCareOutput();
            CallWebApiLand.CallWebApiLandClass
                myCallMyWebApiLandClass =
                    new CallWebApiLand.CallWebApiLandClass(this.MyConfigOptions.SimplifyVbcAdtBaseWebApiUrl);

            returnOutput =
                myCallMyWebApiLandClass.qy_GetPointClickCare();
            return returnOutput;
        }

        public
            PopulateExcelTemplateWithPointClickCareDataOutput
                PopulateExcelTemplateWithPointClickCareMasterData
                (
                    string inputTemplateExcelFullFilename
                    , List<qy_GetPointClickCareOutputColumns> qy_GetPointClickCareOutputColumnsList
                )
        {
            PopulateExcelTemplateWithPointClickCareDataOutput
                returnOutput =
                    new PopulateExcelTemplateWithPointClickCareDataOutput();

            log.Info("B4 init of Workbook}");

            // Load workbook
            Workbook workbook = new Workbook();

            log.Info("B4 Load of workbook {inputTemplateExcelFullFilename}");

            workbook.LoadFromFile(inputTemplateExcelFullFilename);

            log.Info("b4 Load of worksheet");

            // Get first worksheet
            Worksheet sheet = workbook.Worksheets[0];

            log.Info($"after Load of worksheet number of rows:  {sheet.Rows.Length}");
            int startingExcelRow = 1;
            int excelRowCtr = startingExcelRow;
            for (int rowCtr = 0; rowCtr < qy_GetPointClickCareOutputColumnsList.Count; rowCtr++)
            {
                excelRowCtr++;
                sheet.InsertRow(excelRowCtr);

                sheet.Range[$"A{excelRowCtr}"].Value = qy_GetPointClickCareOutputColumnsList[rowCtr].LastName;
                sheet.Range[$"B{excelRowCtr}"].Value = qy_GetPointClickCareOutputColumnsList[rowCtr].FirstName;
                sheet.Range[$"C{excelRowCtr}"].Value = qy_GetPointClickCareOutputColumnsList[rowCtr].SenderSourceCode;
                sheet.Range[$"D{excelRowCtr}"].Value = qy_GetPointClickCareOutputColumnsList[rowCtr].SenderMrn;
                sheet.Range[$"E{excelRowCtr}"].Value = qy_GetPointClickCareOutputColumnsList[rowCtr].SubscriberMrn;
                sheet.Range[$"F{excelRowCtr}"].Value = qy_GetPointClickCareOutputColumnsList[rowCtr].FacilityName;
                sheet.Range[$"G{excelRowCtr}"].Value = qy_GetPointClickCareOutputColumnsList[rowCtr].Gender;
                sheet.Range[$"H{excelRowCtr}"].Value = qy_GetPointClickCareOutputColumnsList[rowCtr].DateOfBirth;
                sheet.Range[$"I{excelRowCtr}"].Value = qy_GetPointClickCareOutputColumnsList[rowCtr].EventTime;
                sheet.Range[$"J{excelRowCtr}"].Value = qy_GetPointClickCareOutputColumnsList[rowCtr].AlertType;
                sheet.Range[$"K{excelRowCtr}"].Value = qy_GetPointClickCareOutputColumnsList[rowCtr].HospitalService;
                sheet.Range[$"L{excelRowCtr}"].Value = qy_GetPointClickCareOutputColumnsList[rowCtr].AdmitSource;
                sheet.Range[$"M{excelRowCtr}"].Value = qy_GetPointClickCareOutputColumnsList[rowCtr].AdmitDate;
                sheet.Range[$"N{excelRowCtr}"].Value = qy_GetPointClickCareOutputColumnsList[rowCtr].PatientComplaints;
                sheet.Range[$"O{excelRowCtr}"].Value = qy_GetPointClickCareOutputColumnsList[rowCtr].DiagnosisCode;
                sheet.Range[$"P{excelRowCtr}"].Value = qy_GetPointClickCareOutputColumnsList[rowCtr].DiagnosisDescription;
                sheet.Range[$"Q{excelRowCtr}"].Value = qy_GetPointClickCareOutputColumnsList[rowCtr].DischargeDate;
                sheet.Range[$"R{excelRowCtr}"].Value = qy_GetPointClickCareOutputColumnsList[rowCtr].DischargeLocation;
                sheet.Range[$"S{excelRowCtr}"].Value = qy_GetPointClickCareOutputColumnsList[rowCtr].DischargeDisposition;
                sheet.Range[$"T{excelRowCtr}"].Value = qy_GetPointClickCareOutputColumnsList[rowCtr].DeathIndicator;
                sheet.Range[$"U{excelRowCtr}"].Value = qy_GetPointClickCareOutputColumnsList[rowCtr].PatientClass;
                sheet.Range[$"V{excelRowCtr}"].Value = qy_GetPointClickCareOutputColumnsList[rowCtr].PatientClassDescription;
                sheet.Range[$"W{excelRowCtr}"].Value = qy_GetPointClickCareOutputColumnsList[rowCtr].PrimaryCareProvider;
                sheet.Range[$"X{excelRowCtr}"].Value = qy_GetPointClickCareOutputColumnsList[rowCtr].Insurance;
                sheet.Range[$"Y{excelRowCtr}"].Value = qy_GetPointClickCareOutputColumnsList[rowCtr].Practice;
                sheet.Range[$"Z{excelRowCtr}"].Value = qy_GetPointClickCareOutputColumnsList[rowCtr].Address1;
                sheet.Range[$"AA{excelRowCtr}"].Value = qy_GetPointClickCareOutputColumnsList[rowCtr].Address2;
                sheet.Range[$"AB{excelRowCtr}"].Value = qy_GetPointClickCareOutputColumnsList[rowCtr].State;
                sheet.Range[$"AC{excelRowCtr}"].Value = qy_GetPointClickCareOutputColumnsList[rowCtr].Zipcode;
                sheet.Range[$"AD{excelRowCtr}"].Value = qy_GetPointClickCareOutputColumnsList[rowCtr].NumberOfErVisits;
                sheet.Range[$"AE{excelRowCtr}"].Value = qy_GetPointClickCareOutputColumnsList[rowCtr].NumberOfIpVisits;
                sheet.Range[$"AF{excelRowCtr}"].Value = qy_GetPointClickCareOutputColumnsList[rowCtr].HomePhone;

            }

            workbook.Save();

            return returnOutput;
        }
    }
}