using BulkInsert9.CallWebApiLand;
using BulkInsert9.Data.Models;
using log4net;
using SimplifyVbcAdt9.Data.Models;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SimplifyVbcAdt9.HumanaCensusConsoleApp
{
    public class MainOps
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MainOps));
        public MainOps
                (
                    qy_GetHumanaCensusConfigOutputColumns inputConfigOps
                    , List<string> inputFullFilenameList
                )
        {
            MyConfigOptions = inputConfigOps;
            MyFullFilenameList = inputFullFilenameList;
        }
        public qy_GetHumanaCensusConfigOutputColumns MyConfigOptions { get; set; }
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

            // new HumanaCensus filename:  HumanaCensus Discharges MM.dd.yy-MM.dd.yy.xslx
            string newHumanaCensusFilename =
                BuildNewHumanaCensusFilename(inputFilename);

            Regex lastDateRegEx = new Regex(@"(?<MM>\d{2}).(?<dd>\d{2}).(?<yy>\d{2}).xlsx$");
            Match lastDateMatch = lastDateRegEx.Match(newHumanaCensusFilename);

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

            FileInfo inputFullFilenameFi = new FileInfo(newHumanaCensusFilename);

            // Copy old HumanaCensus discharges file to Input Archive Directory
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
            FileInfo newHumanaCensusFi = new FileInfo(newHumanaCensusFilename);
            string outputArchiveFullFilename =
                Path.Combine
                (
                    MyConfigOptions.OutputFileArchiveDirectory
                    , newHumanaCensusFi.Name
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
            dd_HumanaCensusOutput
                mydd_TruncateHumanaCensusOutput =
                    TruncateHumanaCensusRaw();
            if (!mydd_TruncateHumanaCensusOutput.IsOk)
            {
                returnOutput.IsOk = false;
                returnOutput.ErrorMessage =
                    mydd_TruncateHumanaCensusOutput.ErrorMessage;
                return returnOutput;
            }

            // Bulk Insert 
            BulkInsertHumanaCensusDataOutput
                myBulkInsertHumanaCensusDataOutput =
                    BulkInsertHumanaCensusRawData
                    (
                        myExtractExcelDataFromSingleFileOutput.MyCsvLineList
                        , inputArchiveFullFilename
                    );

            if (!myBulkInsertHumanaCensusDataOutput.IsOk)
            {
                returnOutput.IsOk = false;
                returnOutput.ErrorMessage =
                    myBulkInsertHumanaCensusDataOutput.ErrorMessage;
                return returnOutput;
            }

            // Finalize CollectionsRaw table.
            di_FinalizeHumanaCensusOutput
                mydi_FinalizeHumanaCensusOutput =
                    FinalizeHumanaCensusRaw(inputArchiveFullFilename);
            if (!mydi_FinalizeHumanaCensusOutput.IsOk)
            {
                returnOutput.IsOk = false;
                returnOutput.ErrorMessage =
                    mydi_FinalizeHumanaCensusOutput.ErrorMessage;
                return returnOutput;
            }

            // Get HumanaCensusMaster rows
            qy_GetHumanaCensusMasterOutput
                mySpGetHumanaCensusMasterOutput =
                    GetHumanaCensusMaster();
            if (!mySpGetHumanaCensusMasterOutput.IsOk)
            {
                returnOutput.IsOk = false;
                returnOutput.ErrorMessage =
                    mySpGetHumanaCensusMasterOutput.ErrorMessage;
                return returnOutput;
            }

            // Populate the cells of the target spreadsheet.
            PopulateExcelTemplateWithHumanaCensusMasterDataOutput
                myPopulateExcelTemplateWithHumanaCensusMasterDataOutput =
                    PopulateExcelTemplateWithHumanaCensusMasterData
                    (
                        outputArchiveFullFilename
                        , mySpGetHumanaCensusMasterOutput.qy_GetHumanaCensusAdtMasterOutputColumnsList
                    );
            if (!myPopulateExcelTemplateWithHumanaCensusMasterDataOutput.IsOk)
            {
                returnOutput.IsOk = false;
                returnOutput.ErrorMessage =
                    myPopulateExcelTemplateWithHumanaCensusMasterDataOutput.ErrorMessage;
                return returnOutput;
            }

            // Move new HumanaCensus discharges file to Simple File Mover Read Directory
            string toSimpleFileMoverReadDirectoryFullFilename =
                Path.Combine(this.MyConfigOptions.ToSimpleFileMoverReadDirectory, newHumanaCensusFi.Name);
            if (File.Exists(toSimpleFileMoverReadDirectoryFullFilename))
            {
                File.Delete(toSimpleFileMoverReadDirectoryFullFilename);
            }
            File.Copy(outputArchiveFullFilename, toSimpleFileMoverReadDirectoryFullFilename);

            //Delete old HumanaCensus filename
            if (File.Exists(inputFilename))
            {
                File.Delete(inputFilename);
            }
            returnOutput.EmailBodyLine = $"Processed file {inputFullFilenameFi.Name}";
            return returnOutput;
        }

        public string BuildNewHumanaCensusFilename(string inputFullFilename)
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
                    , $"HumanaCensus Discharges {oneDayBackFileDate.Month.ToString().PadLeft(2, '0')}.{oneDayBackFileDate.Day.ToString().PadLeft(2, '0')}.{oneDayBackFileDate.Year.ToString().Substring(2, 2)}-{currentFileDate.Month.ToString().PadLeft(2, '0')}.{currentFileDate.Day.ToString().PadLeft(2, '0')}.{currentFileDate.Year.ToString().Substring(2, 2)}.xlsx"
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
                List<string> HumanaCensusFileColumnNameList =
                    GetHumanaCensusFileColumnNameList();
                ExtractDataFromHumanaCensusExcelRowOutput myExtractDataFromHumanaCensusExcelRowOutput =
                    new ExtractDataFromHumanaCensusExcelRowOutput();
                int rowCtr = 0;
                rowCtr = this.MyConfigOptions.StartLineNumber;
                while (true)
                {
                    if (rowCtr == 1198)
                    {
                        int i = 0;
                        i++;
                    }
                    myExtractDataFromHumanaCensusExcelRowOutput =
                        new ExtractDataFromHumanaCensusExcelRowOutput();
                    myExtractDataFromHumanaCensusExcelRowOutput =
                        ExtractDataFromExcelRow
                        (
                            loopWorksheet
                            , rowCtr
                            , HumanaCensusFileColumnNameList
                        );
                    if (!myExtractDataFromHumanaCensusExcelRowOutput.IsOk)
                    {
                        returnOutput.IsOk = false;
                        returnOutput.ErrorMessage = myExtractDataFromHumanaCensusExcelRowOutput.ErrorMessage;
                        returnOutput.MyCsvLineList = new List<string>();
                        return returnOutput;
                    }
                    if (myExtractDataFromHumanaCensusExcelRowOutput.OutputCsvString.Replace("~","").Replace("NaN", "").Length == 0)
                    {
                        break;
                    }

                    string finalRowValue = $"~,{myExtractDataFromHumanaCensusExcelRowOutput.OutputCsvString}";
                    returnOutput.MyCsvLineList.Add(finalRowValue);
                    rowCtr++;
                }
            }
            return returnOutput;
        }
        public ExtractDataFromHumanaCensusExcelRowOutput ExtractDataFromExcelRow(Worksheet inputWorksheet, int inputRowCtr, List<string> inputFileColumnList)
        {
            ExtractDataFromHumanaCensusExcelRowOutput returnOutput = new ExtractDataFromHumanaCensusExcelRowOutput();

            List<string> HumanaCensusFileColumnNameList = new List<string>();

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

        public List<string> GetHumanaCensusFileColumnNameList()
        {
            List<string> returnOutput = new List<string>();

            returnOutput.Add("Dummy1");
            returnOutput.Add("PatientName");
            returnOutput.Add("DateOfBirth");
            returnOutput.Add("PaneledProvider");
            returnOutput.Add("SdohDetails");
            returnOutput.Add("SrfDetails");
            returnOutput.Add("SrfFlagYOrN");
            returnOutput.Add("StayType");
            returnOutput.Add("AdmitDate");
            returnOutput.Add("DischargeDate");
            returnOutput.Add("AuthStatus");
            returnOutput.Add("Disposition");
            returnOutput.Add("PatEligible");
            returnOutput.Add("PatEligibleThrough");
            returnOutput.Add("ReadmitRisk");
            returnOutput.Add("Readmit");
            returnOutput.Add("Category");
            returnOutput.Add("DxDescription");
            returnOutput.Add("FacilityOrPractitioner");
            returnOutput.Add("NewYOrN");

            return returnOutput;
        }
        public dd_HumanaCensusOutput TruncateHumanaCensusRaw()
        {
            dd_HumanaCensusOutput returnOutput =
                new dd_HumanaCensusOutput();

            CallWebApiLand.CallWebApiLandClass myCallMyWebApiLandClass =
                new CallWebApiLand.CallWebApiLandClass(this.MyConfigOptions.SimplifyVbcAdtBaseWebApiUrl);
            dd_HumanaCensusOutput
                mydd_TruncateHumanaCensusOutput =
                    myCallMyWebApiLandClass.dd_HumanaCensus();
            if (!mydd_TruncateHumanaCensusOutput.IsOk)
            {
                returnOutput.IsOk = false;
                returnOutput.ErrorMessage =
                    mydd_TruncateHumanaCensusOutput.ErrorMessage;
                return returnOutput;
            }
            return returnOutput;
        }
        public BulkInsertHumanaCensusDataOutput BulkInsertHumanaCensusRawData(List<string> inputCsvLineList, string inputSourceFullFilename)
        {
            BulkInsertHumanaCensusDataOutput returnOutput =
                new BulkInsertHumanaCensusDataOutput();

            // Make sure the Pk column is in the non-CSV column list.
            List<NonCsvFileColumnDefAndValue>
                myNonCsvFileColumnDefAndValueList =
                    new List<NonCsvFileColumnDefAndValue>();

            // SourceFullFilename
            spGetColumnDefsForGivenDbAndTableName_OutputColumns myPkColDef =
                new spGetColumnDefsForGivenDbAndTableName_OutputColumns
                {
                    MyDbColumnName = "FullFilename",
                    MyDbColumnLength = 300,
                    MyDbColumnType = "nvarchar",
                    MyDbName = "Staging",
                    MyDbTableName = "adt.HumanaCensus",
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
                    MyDbColumnName = "HumanaCensusID",
                    MyDbColumnLength = 1,
                    MyDbColumnType = "int",
                    MyDbName = "Staging",
                    MyDbTableName = "adt.HumanaCensus",
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
        public di_FinalizeHumanaCensusOutput
                    FinalizeHumanaCensusRaw
                    (
                        string inputFilename
                    )
        {
            di_FinalizeHumanaCensusOutput returnOutput =
                new di_FinalizeHumanaCensusOutput();
            CallWebApiLand.CallWebApiLandClass
                myCallMyWebApiLandClass =
                    new CallWebApiLand.CallWebApiLandClass(this.MyConfigOptions.SimplifyVbcAdtBaseWebApiUrl);

            returnOutput =
                    myCallMyWebApiLandClass.di_FinalizeHumanaCensus(inputFilename);
            return returnOutput;
        }

        public qy_GetHumanaCensusMasterOutput
                    GetHumanaCensusMaster
                    (
                    )
        {
            qy_GetHumanaCensusMasterOutput returnOutput =
                new qy_GetHumanaCensusMasterOutput();
            CallWebApiLand.CallWebApiLandClass
                myCallMyWebApiLandClass =
                    new CallWebApiLand.CallWebApiLandClass(this.MyConfigOptions.SimplifyVbcAdtBaseWebApiUrl);

            returnOutput =
                myCallMyWebApiLandClass.qy_GetHumanaCensusMaster();
            return returnOutput;
        }

        public
            PopulateExcelTemplateWithHumanaCensusMasterDataOutput
                PopulateExcelTemplateWithHumanaCensusMasterData
                (
                    string inputTemplateExcelFullFilename
                    , List<qy_GetHumanaCensusMasterOutputColumns> inputSpGetHumanaCensusMasterOutputColumnsList
                )
        {
            PopulateExcelTemplateWithHumanaCensusMasterDataOutput
                returnOutput =
                    new PopulateExcelTemplateWithHumanaCensusMasterDataOutput();

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
            for (int rowCtr = 0; rowCtr < inputSpGetHumanaCensusMasterOutputColumnsList.Count; rowCtr++)
            {
                excelRowCtr++;
                sheet.InsertRow(excelRowCtr);

                sheet.Range[$"A{excelRowCtr}"].Value = inputSpGetHumanaCensusMasterOutputColumnsList[rowCtr].PatientName;
                sheet.Range[$"B{excelRowCtr}"].Value = inputSpGetHumanaCensusMasterOutputColumnsList[rowCtr].PatientLastName;
                sheet.Range[$"C{excelRowCtr}"].Value = inputSpGetHumanaCensusMasterOutputColumnsList[rowCtr].PatienFirstName;
                sheet.Range[$"D{excelRowCtr}"].Value = inputSpGetHumanaCensusMasterOutputColumnsList[rowCtr].DateOfBirth;
                sheet.Range[$"E{excelRowCtr}"].Value = inputSpGetHumanaCensusMasterOutputColumnsList[rowCtr].PaneledProvider;
                sheet.Range[$"F{excelRowCtr}"].Value = inputSpGetHumanaCensusMasterOutputColumnsList[rowCtr].SdohDetails;
                sheet.Range[$"G{excelRowCtr}"].Value = inputSpGetHumanaCensusMasterOutputColumnsList[rowCtr].SrfDetails;
                sheet.Range[$"H{excelRowCtr}"].Value = inputSpGetHumanaCensusMasterOutputColumnsList[rowCtr].SrfFlagYOrN;
                sheet.Range[$"I{excelRowCtr}"].Value = inputSpGetHumanaCensusMasterOutputColumnsList[rowCtr].StayType;
                sheet.Range[$"J{excelRowCtr}"].Value = inputSpGetHumanaCensusMasterOutputColumnsList[rowCtr].AdmitDate;
                sheet.Range[$"K{excelRowCtr}"].Value = inputSpGetHumanaCensusMasterOutputColumnsList[rowCtr].DischargeDate;
                sheet.Range[$"L{excelRowCtr}"].Value = inputSpGetHumanaCensusMasterOutputColumnsList[rowCtr].AuthStatus;
                sheet.Range[$"M{excelRowCtr}"].Value = inputSpGetHumanaCensusMasterOutputColumnsList[rowCtr].Disposition;
                sheet.Range[$"N{excelRowCtr}"].Value = inputSpGetHumanaCensusMasterOutputColumnsList[rowCtr].PatEligible;
                sheet.Range[$"O{excelRowCtr}"].Value = inputSpGetHumanaCensusMasterOutputColumnsList[rowCtr].PatEligibleThrough;
                sheet.Range[$"P{excelRowCtr}"].Value = inputSpGetHumanaCensusMasterOutputColumnsList[rowCtr].ReadmitRisk;
                sheet.Range[$"Q{excelRowCtr}"].Value = inputSpGetHumanaCensusMasterOutputColumnsList[rowCtr].Readmit;
                sheet.Range[$"R{excelRowCtr}"].Value = inputSpGetHumanaCensusMasterOutputColumnsList[rowCtr].Category;
                sheet.Range[$"S{excelRowCtr}"].Value = inputSpGetHumanaCensusMasterOutputColumnsList[rowCtr].DxDescription;
                sheet.Range[$"T{excelRowCtr}"].Value = inputSpGetHumanaCensusMasterOutputColumnsList[rowCtr].FacilityOrPractitioner;
                sheet.Range[$"U{excelRowCtr}"].Value = inputSpGetHumanaCensusMasterOutputColumnsList[rowCtr].NewYOrN;
            }

            workbook.Save();

            return returnOutput;
        }
    }
}