using BulkInsert9.CallWebApiLand;
using BulkInsert9.Data.Models;
using log4net;
using SimplifyVbcAdt9.Data.Models;
using Spire.Xls;
using Spire.Xls.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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

            // Clean all lines inside of file (commas inside of double-quotes)
            CleanInideOfDoubleQuotesInEntireFileReturningFileLineList
                myClean =
                    new CleanInideOfDoubleQuotesInEntireFileReturningFileLineList
                    (
                        inputFilename
                    );
            List<string> cleanedLines = 
                myClean.DoIt();

            // Delete file having un-cleaned lines so that we may re-write it.
            if (File.Exists(inputFilename))
            {
                File.Delete(inputFilename);
            }

            // Re-write file with cleaned file lines.
            System.IO.File.WriteAllLines(inputFilename, cleanedLines);

            // Copy old PointClickCare discharges CSV file to Input Archive Directory
            FileInfo originalCsvFi = new FileInfo(inputFilename);
            string inputArchiveCsvFullFilename =
                Path.Combine(MyConfigOptions.InputFileArchiveDirectory, originalCsvFi.Name);
            if (File.Exists(inputArchiveCsvFullFilename))
            {
                File.Delete(inputArchiveCsvFullFilename);
            }
            File.Copy(inputFilename, inputArchiveCsvFullFilename);


            // Convert input CSV file to XLSX
            inputFilename =
                ConvertCsvToXlsx(inputFilename);

            // If columns for Event Type and Event Type Description
            // exist, delete them.
            IfExistEventColumnsDeleteThem(inputFilename);

            // new PointClickCare filename:  PointClickCare Discharges MM.dd.yy-MM.dd.yy.xslx
            string newPointClickCareFilename =
                BuildNewPointClickCareFilename(inputFilename);

            Regex lastDateRegEx = new Regex(@"PCCADT(?<MM>\d{2})(?<dd>\d{2})(?<yy>\d{2}).xlsx$");
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
                    ExtractExcelDataFromSingleFileSingleFile(inputFilename);
            if (!myExtractExcelDataFromSingleFileOutput.IsOk)
            {
                returnOutput.IsOk = false;
                returnOutput.ErrorMessage = myExtractExcelDataFromSingleFileOutput.ErrorMessage;
                return returnOutput;
            }

            // Delete the input xlsx file from the input Archive folder, leaving the csv file.
            if (File.Exists(inputFilename))
            {
                File.Delete(inputFilename);
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
                        , inputFilename
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
                    FinalizePointClickCareRaw(inputFilename);
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

            // Convert the output xlsx file to a csv file.
            string newCsvOutputFullFilename =
                ConvertXlsxToCsv(outputArchiveFullFilename);

            // Delete the output xlsx file, leaving only the csv file.
            if (File.Exists(outputArchiveFullFilename))
            {
                File.Delete(outputArchiveFullFilename);
            }

            // Move new PointClickCare discharges csv file to Simple File Mover Read Directory
            FileInfo fiOutputCsvFi = new FileInfo(newCsvOutputFullFilename);


            string toSimpleFileMoverReadDirectoryFullFilename =
                Path.Combine(this.MyConfigOptions.ToSimpleFileMoverReadDirectory, fiOutputCsvFi.Name);
            if (File.Exists(toSimpleFileMoverReadDirectoryFullFilename))
            {
                File.Delete(toSimpleFileMoverReadDirectoryFullFilename);
            }
            File.Copy(newCsvOutputFullFilename, toSimpleFileMoverReadDirectoryFullFilename);

            //Delete old PointClickCare filename
            if (File.Exists(originalCsvFi.FullName))
            {
                File.Delete(originalCsvFi.FullName);
            }
            returnOutput.EmailBodyLine = $"Processed file {originalCsvFi.Name}";
            return returnOutput;
        }

        public string BuildNewPointClickCareFilename(string inputFullFilename)
        {
            FileInfo myFi = new FileInfo(inputFullFilename);

            string newFilename =
                Path.Combine
                (
                    this.MyConfigOptions.OutputFileArchiveDirectory
                    , myFi.Name
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
                    if (rowCtr == 5)
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

                    string finalRowValue = $"{myExtractDataFromPointClickCareExcelRowOutput.OutputCsvString}";

                    if (ValidateRowValue(finalRowValue))
                    {
                        returnOutput.MyCsvLineList.Add(finalRowValue);
                    }
                    rowCtr++;
                }
            }
            return returnOutput;
        }
        public bool ValidateRowValue(string inputRowValue)
        {
            List<string> stringPartsList = 
                inputRowValue.Split(',').ToList();

            List<string> myColumnList =
                GetPointClickCareFileColumnNameList();
            int listPositionOfEventTime =
                myColumnList.IndexOf("EventTime");
            int listPositionOfAdmitDate =
                    myColumnList.IndexOf("AdmitDate");
            int listPositionOfDischargeDate =
                myColumnList.IndexOf("DischargeDate");

            if (listPositionOfEventTime >= stringPartsList.Count ||
                listPositionOfAdmitDate >= stringPartsList.Count ||
                listPositionOfDischargeDate >= stringPartsList.Count)
            {
                return false;
            }

            // Validate EventTime datetime string
            if (!ValidateDateTimeStringFormat(stringPartsList[listPositionOfEventTime]))
            { 
                return false; 
            }

            // Validate AdmitDate datetime string
            if (!ValidateDateTimeStringFormat(stringPartsList[listPositionOfAdmitDate]))
            {
                return false;
            }

            // Validate DischargeDate datetime string
            if (!ValidateDateTimeStringFormat(stringPartsList[listPositionOfDischargeDate]))
            {
                return false;
            }

            return true;
        }
        public bool ValidateDateTimeStringFormat(string inputDateTimeString)
        {
            bool returnOutput = true;

            DateTime isDateTimeStringValid = DateTime.MinValue;
            DateTime.TryParse(inputDateTimeString, out isDateTimeStringValid);
            if (isDateTimeStringValid == DateTime.MinValue)
            {
                returnOutput = false;
            }

            return returnOutput;
        }
        public string ConvertXlsxToCsv(string inputFullOutputXlsxFilename)
        {
            string returnOutput = string.Empty;

            //Create an instance of Workbook class
            Workbook workbook = new Workbook();
            //Load an Excel file
            workbook.LoadFromFile(inputFullOutputXlsxFilename);

            //Get the first worksheet
            Worksheet sheet = workbook.Worksheets[0];

            FileInfo fi = new FileInfo(inputFullOutputXlsxFilename);
            returnOutput =
                Path.Combine(fi.DirectoryName, $"{fi.Name.Replace(".xlsx", ".csv")}");

            //Save the worksheet as CSV
            sheet.SaveToFile(returnOutput, ",", Encoding.UTF8);

            return returnOutput;
        }
        public string ConvertCsvToXlsx(string inputFullInputCsvFilename)
        {
            // Create an instance of Workbook class
            Workbook workbook = new Workbook();

            FileInfo fi = new FileInfo(inputFullInputCsvFilename);

            // Load a CSV file
            workbook.LoadFromFile(inputFullInputCsvFilename, ",", 1, 1);

            // Get the first worksheet and used range
            Worksheet sheet =
                workbook.Worksheets[0];
            CellRange usedRange = sheet.AllocatedRange;
            usedRange.IgnoreErrorOptions = IgnoreErrorType.NumberAsText;

            // Autofit columns and rows
            usedRange.AutoFitColumns();
            usedRange.AutoFitRows();

            // Save the result file
            string filenameSansExtension =
                fi.Name.Replace(".csv","");

            string newXlsxFullFilename =
                Path.Combine(fi.DirectoryName, $"{filenameSansExtension}.xlsx");

            if (File.Exists(newXlsxFullFilename))
            {
                File.Delete(newXlsxFullFilename);
            }
            workbook.SaveToFile(newXlsxFullFilename, ExcelVersion.Version2013);

            return newXlsxFullFilename;
        }
        public void IfExistEventColumnsDeleteThem(string inputFullInputXlsxFilename)
        {
            // Create an instance of Workbook class
            Workbook workbook = new Workbook();

            // Load a CSV file
            workbook.LoadFromFile(inputFullInputXlsxFilename);

            // Get the first worksheet and used range
            Worksheet sheet =
                workbook.Worksheets[0];
            string? cellValueForEventType = null;
            string? cellValueForEventTypeDescription = null;
            cellValueForEventType = sheet.Range["U1"].Text.Trim();
            cellValueForEventTypeDescription = sheet.Range["V1"].Text.Trim();

            if (cellValueForEventType != null && 
                cellValueForEventType.CompareTo("Event Type") == 0 &&
                cellValueForEventTypeDescription != null &&
                cellValueForEventTypeDescription.CompareTo("Event Type Description") == 0)
            {
                sheet.DeleteColumn(21);
                // With column EventType deleted, the EventTypeDescription becomes
                // column 21.
                sheet.DeleteColumn(21);
            }

            workbook.SaveToFile(inputFullInputXlsxFilename, ExcelVersion.Version2013);
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

                sheet.Range[$"A{excelRowCtr}"].Text = qy_GetPointClickCareOutputColumnsList[rowCtr].LastName;
                sheet.Range[$"B{excelRowCtr}"].Text = qy_GetPointClickCareOutputColumnsList[rowCtr].FirstName;
                sheet.Range[$"C{excelRowCtr}"].Text = qy_GetPointClickCareOutputColumnsList[rowCtr].SenderSourceCode;
                sheet.Range[$"D{excelRowCtr}"].Text = qy_GetPointClickCareOutputColumnsList[rowCtr].SenderMrn;
                sheet.Range[$"E{excelRowCtr}"].Text = qy_GetPointClickCareOutputColumnsList[rowCtr].SubscriberMrn;
                sheet.Range[$"F{excelRowCtr}"].Text = qy_GetPointClickCareOutputColumnsList[rowCtr].FacilityName;
                sheet.Range[$"G{excelRowCtr}"].Text = qy_GetPointClickCareOutputColumnsList[rowCtr].Gender;
                sheet.Range[$"H{excelRowCtr}"].Text = ConvertCanonicalDateTomSlashdSlashyyyy(qy_GetPointClickCareOutputColumnsList[rowCtr].DateOfBirth);
                sheet.Range[$"I{excelRowCtr}"].Text = ConvertCanonicalDateTomSlashdSlashyyyy(qy_GetPointClickCareOutputColumnsList[rowCtr].EventTime);
                
                sheet.Range[$"J{excelRowCtr}"].Text = qy_GetPointClickCareOutputColumnsList[rowCtr].AlertType;
                sheet.Range[$"K{excelRowCtr}"].Text = qy_GetPointClickCareOutputColumnsList[rowCtr].HospitalService;
                sheet.Range[$"L{excelRowCtr}"].Text = qy_GetPointClickCareOutputColumnsList[rowCtr].AdmitSource;

                sheet.Range[$"M{excelRowCtr}"].Text = ConvertCanonicalDateTomSlashdSlashyyyy(qy_GetPointClickCareOutputColumnsList[rowCtr].AdmitDate);

                sheet.Range[$"N{excelRowCtr}"].Text = qy_GetPointClickCareOutputColumnsList[rowCtr].PatientComplaints;
                sheet.Range[$"O{excelRowCtr}"].Text = qy_GetPointClickCareOutputColumnsList[rowCtr].DiagnosisCode;
                sheet.Range[$"P{excelRowCtr}"].Text = qy_GetPointClickCareOutputColumnsList[rowCtr].DiagnosisDescription;

                sheet.Range[$"Q{excelRowCtr}"].Text = ConvertCanonicalDateTomSlashdSlashyyyy(qy_GetPointClickCareOutputColumnsList[rowCtr].DischargeDate);
                sheet.Range[$"R{excelRowCtr}"].Text = qy_GetPointClickCareOutputColumnsList[rowCtr].DischargeLocation;
                sheet.Range[$"S{excelRowCtr}"].Text = qy_GetPointClickCareOutputColumnsList[rowCtr].DischargeDisposition;
                sheet.Range[$"T{excelRowCtr}"].Text = qy_GetPointClickCareOutputColumnsList[rowCtr].DeathIndicator;
                sheet.Range[$"U{excelRowCtr}"].Text = qy_GetPointClickCareOutputColumnsList[rowCtr].PatientClass;
                sheet.Range[$"V{excelRowCtr}"].Text = qy_GetPointClickCareOutputColumnsList[rowCtr].PatientClassDescription;
                sheet.Range[$"W{excelRowCtr}"].Text = qy_GetPointClickCareOutputColumnsList[rowCtr].PrimaryCareProvider;
                sheet.Range[$"X{excelRowCtr}"].Text = qy_GetPointClickCareOutputColumnsList[rowCtr].Insurance;
                sheet.Range[$"Y{excelRowCtr}"].Text = qy_GetPointClickCareOutputColumnsList[rowCtr].Practice;
                sheet.Range[$"Z{excelRowCtr}"].Text = qy_GetPointClickCareOutputColumnsList[rowCtr].Address1;
                sheet.Range[$"AA{excelRowCtr}"].Text = qy_GetPointClickCareOutputColumnsList[rowCtr].Address2;
                sheet.Range[$"AB{excelRowCtr}"].Text = qy_GetPointClickCareOutputColumnsList[rowCtr].State;
                sheet.Range[$"AC{excelRowCtr}"].Text = qy_GetPointClickCareOutputColumnsList[rowCtr].Zipcode;
                sheet.Range[$"AD{excelRowCtr}"].Text = qy_GetPointClickCareOutputColumnsList[rowCtr].NumberOfErVisits;
                sheet.Range[$"AE{excelRowCtr}"].Text = qy_GetPointClickCareOutputColumnsList[rowCtr].NumberOfIpVisits;
                sheet.Range[$"AF{excelRowCtr}"].Text = qy_GetPointClickCareOutputColumnsList[rowCtr].HomePhone;
            }

            workbook.Save();

            return returnOutput;
        }

        public string ConvertCanonicalDateTomSlashdSlashyyyy(string inputCanonicalDateTime)
        {
            string returnOutput = string.Empty;

            returnOutput = inputCanonicalDateTime;
            DateTime myDateTime = DateTime.MinValue;
            DateTime.TryParse(returnOutput, out myDateTime);
            if (myDateTime == DateTime.MinValue)
            {
                return returnOutput;
            }

            returnOutput = $"{myDateTime.Month.ToString()}/{myDateTime.Day.ToString()}/{myDateTime.Year}";
            return returnOutput;
        }

    }
}