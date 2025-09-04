using BulkInsert9.CallWebApiLand;
using BulkInsert9.Data.Models;

using log4net;

using SimplifyVbcAdt9.Data.Models;

using Spire.Xls;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SimplifyVbcAdt9.EthinConsoleApp
{
    public class EthinMainOps
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(EthinMainOps));
        public EthinMainOps(qy_GetEthinConfigOutputColumns inputqy_GetEthinConfigOutputColumns)
        {
            Myqy_GetEthinConfigOutputColumns =
                inputqy_GetEthinConfigOutputColumns;
        }

        public qy_GetEthinConfigOutputColumns Myqy_GetEthinConfigOutputColumns { get; set; }

        public EthinMainOpsOutput DoIt()
        {
            EthinMainOpsOutput returnOutput =
                new EthinMainOpsOutput();

            string wildcardedSearchString = Myqy_GetEthinConfigOutputColumns.InputFilenameContainsString;
            wildcardedSearchString =
                $"*{wildcardedSearchString}*";
            List<string>
                fullFilenameList =
                    Directory.GetFiles
                    (
                        Myqy_GetEthinConfigOutputColumns.ReadDirectory
                        //@"\\ps-nas\nas\SSS\VBC\1. Care Coordination\ADMIN ASSISTANT\ADT FEEDS\etHIN Simplifer (do not use)"
                        //@"\\ps-nas\nas\SSS\VBC\1. Care Coordination\ADMIN ASSISTANT\ADT FEEDS\etHIN Simplifier (do not use)"
                        , wildcardedSearchString
                        , SearchOption.TopDirectoryOnly
                    ).ToList();

            if (fullFilenameList.Count > 0)
            {
                foreach (string loopFullFilename in fullFilenameList)
                {
                    ProcessSingleEthinFileOutput
                        myProcessSingleEthinFileOutput =
                            ProcessSingleEthinFile(loopFullFilename);
                    if (myProcessSingleEthinFileOutput.EmailBodyLine.CompareTo("") != 0)
                    {
                        returnOutput.NumberOfFilesProcessed++;
                    }
                }
            }


            return returnOutput;
        }
        public ProcessSingleEthinFileOutput ProcessSingleEthinFile(string inputFullFilename)
        {
            ProcessSingleEthinFileOutput returnOutput = new ProcessSingleEthinFileOutput();

            FileInfo inputFullFilenameFi = new FileInfo(inputFullFilename);


            // Copy old etHIN discharges file to Input Archive Directory
            string inputArchiveFullFilename =
                Path.Combine(Myqy_GetEthinConfigOutputColumns.InputFileArchiveDirectory, inputFullFilenameFi.Name);
            if (File.Exists(inputArchiveFullFilename))
            {
                File.Delete(inputArchiveFullFilename);
            }
            File.Copy(inputFullFilename, inputArchiveFullFilename);

            // If the filename already exists in the "already imported archive", do nothing.
            string inputAlreadyImportedFullFilename =
                Path.Combine(Myqy_GetEthinConfigOutputColumns.ImportArchiveFolder, inputFullFilenameFi.Name);
            if (File.Exists(inputAlreadyImportedFullFilename))
            {
                if (File.Exists(inputFullFilename))
                {
                    File.Delete(inputFullFilename);
                }
                return returnOutput;
            }

            // new etHIN filename:  etHIN Discharges MM.dd.yy-MM.dd.yy.xslx
            string newEthinFilename =
                BuildNewEthinFilename(inputFullFilename);

            Regex lastDateRegEx = new Regex(@"(?<MM>\d{2}).(?<dd>\d{2}).(?<yy>\d{2}).xlsx$");
            Match lastDateMatch = lastDateRegEx.Match(newEthinFilename);

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
                if (File.Exists(inputFullFilename))
                {
                    File.Delete(inputFullFilename);
                }
                return returnOutput;
            }

            // Copy the template to the output folder.
            FileInfo newEthinFi = new FileInfo(newEthinFilename);
            string outputArchiveFullFilename =
                Path.Combine
                (
                    Myqy_GetEthinConfigOutputColumns.OutputFileArchiveDirectory
                    , newEthinFi.Name
                );

            if (File.Exists(outputArchiveFullFilename))
            {
                File.Delete(outputArchiveFullFilename);
            }
            File.Copy
            (
                Myqy_GetEthinConfigOutputColumns.ExcelTemplateFullFilename
                , outputArchiveFullFilename
            );

            // Extract Excel data
            ExtractExcelDataFromEthinSingleFileOutput
                myExtractExcelDataFromEthinSingleFileOutput =
                    ExtractExcelDataFromSingleFileSingleFile(inputFullFilename);
            if (!myExtractExcelDataFromEthinSingleFileOutput.IsOk)
            {
                returnOutput.IsOk = false;
                returnOutput.ErrorMessage = myExtractExcelDataFromEthinSingleFileOutput.ErrorMessage;
                return returnOutput;
            }

            // Truncate Raw table.
            dd_EthinOutput
                mydd_EthinOutput =
                    TruncateEthinRaw();
            if (!mydd_EthinOutput.IsOk)
            {
                returnOutput.IsOk = false;
                returnOutput.ErrorMessage =
                    mydd_EthinOutput.ErrorMessage;
                return returnOutput;
            }

            // Bulk Insert 
            BulkInsertEthinDataOutput
                myBulkInsertEthinDataOutput =
                    BulkInsertEthinRawData
                    (
                        myExtractExcelDataFromEthinSingleFileOutput.MyCsvLineList
                        , inputFullFilename
                    );

            if (!myBulkInsertEthinDataOutput.IsOk)
            {
                returnOutput.IsOk = false;
                returnOutput.ErrorMessage =
                    myBulkInsertEthinDataOutput.ErrorMessage;
                return returnOutput;
            }

            // Finalize CollectionsRaw table.
            di_EthinOutput
                mydi_EthinOutput =
                    FinalizeEthinRaw(this.Myqy_GetEthinConfigOutputColumns.SimplifyVbcAdtBaseWebApiUrl);
            if (!mydi_EthinOutput.IsOk)
            {
                returnOutput.IsOk = false;
                returnOutput.ErrorMessage =
                    mydi_EthinOutput.ErrorMessage;
                return returnOutput;
            }

            // Get EthinMaster rows
            qy_GetEthinOutput
                myqy_GetEthinOutput =
                    GetEthinMaster();
            if (!myqy_GetEthinOutput.IsOk)
            {
                returnOutput.IsOk = false;
                returnOutput.ErrorMessage =
                    myqy_GetEthinOutput.ErrorMessage;
                return returnOutput;
            }

            // Populate the cells of the target spreadsheet.
            PopulateExcelTemplateWithEthinMasterDataOutput
                myPopulateExcelTemplateWithEthinMasterDataOutput =
                    PopulateExcelTemplateWithEthinMasterData
                    (
                        outputArchiveFullFilename
                        , myqy_GetEthinOutput.qy_GetEthinOutputColumnsList
                    );
            if (!myPopulateExcelTemplateWithEthinMasterDataOutput.IsOk)
            {
                returnOutput.IsOk = false;
                returnOutput.ErrorMessage =
                    myPopulateExcelTemplateWithEthinMasterDataOutput.ErrorMessage;
                return returnOutput;
            }

            // Move new etHIN discharges file to Simple File Mover Read Directory
            string toSimpleFileMoverReadDirectoryFullFilename =
                Path.Combine(Myqy_GetEthinConfigOutputColumns.ToSimpleFileMoverReadDirectory, newEthinFi.Name);
            if (File.Exists(toSimpleFileMoverReadDirectoryFullFilename))
            {
                File.Delete(toSimpleFileMoverReadDirectoryFullFilename);
            }
            File.Copy(outputArchiveFullFilename, toSimpleFileMoverReadDirectoryFullFilename);

            //Delete old etHIN filename
            if (File.Exists(inputFullFilename))
            {
                File.Delete(inputFullFilename);
            }
            returnOutput.EmailBodyLine = $"Processed file {inputFullFilenameFi.Name}";
            return returnOutput;
        }

        public string BuildNewEthinFilename(string inputFullFilename)
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
                    Myqy_GetEthinConfigOutputColumns.OutputFileArchiveDirectory
                    , $"etHIN Discharges {oneDayBackFileDate.Month.ToString().PadLeft(2, '0')}.{oneDayBackFileDate.Day.ToString().PadLeft(2, '0')}.{oneDayBackFileDate.Year.ToString().Substring(2, 2)}-{currentFileDate.Month.ToString().PadLeft(2, '0')}.{currentFileDate.Day.ToString().PadLeft(2, '0')}.{currentFileDate.Year.ToString().Substring(2, 2)}.xlsx"
                );

            return newFilename;
        }
        public Worksheet GetWorksheetByName(Workbook inputWorkbook, string inputWorksheetName)
        {
            Worksheet returnWorksheet = null;

            returnWorksheet = (Worksheet?)inputWorkbook.Worksheets.Where(w => w.Name.CompareTo(inputWorksheetName) == 0).FirstOrDefault();

            return returnWorksheet;
        }

        public ExtractExcelDataFromEthinSingleFileOutput ExtractExcelDataFromSingleFileSingleFile(string inputFullFilename)
        {
            ExtractExcelDataFromEthinSingleFileOutput returnOutput = new ExtractExcelDataFromEthinSingleFileOutput();

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
                List<string> EthinFileColumnNameList =
                    GetEthinFileColumnNameList();
                ExtractDataFromEthinExcelRowOutput myExtractDataFromEthinExcelRowOutput =
                    new ExtractDataFromEthinExcelRowOutput();
                int rowCtr = 0;
                rowCtr = this.Myqy_GetEthinConfigOutputColumns.StartLineNumber;
                while (true)
                {
                    if (rowCtr == 362)
                    {
                        int i = 0;
                        i++;
                    }
                    myExtractDataFromEthinExcelRowOutput =
                        new ExtractDataFromEthinExcelRowOutput();
                    myExtractDataFromEthinExcelRowOutput =
                        ExtractDataFromExcelRow
                        (
                            loopWorksheet
                            , rowCtr
                            , EthinFileColumnNameList
                        );
                    if (!myExtractDataFromEthinExcelRowOutput.IsOk)
                    {
                        returnOutput.IsOk = false;
                        returnOutput.ErrorMessage = myExtractDataFromEthinExcelRowOutput.ErrorMessage;
                        returnOutput.MyCsvLineList = new List<string>();
                        return returnOutput;
                    }
                    if (myExtractDataFromEthinExcelRowOutput.OutputCsvString.Length == 0)
                    {
                        break;
                    }
                    returnOutput.MyCsvLineList.Add(myExtractDataFromEthinExcelRowOutput.OutputCsvString);
                    rowCtr++;
                }
            }
            return returnOutput;
        }
        public ExtractDataFromEthinExcelRowOutput ExtractDataFromExcelRow(Worksheet inputWorksheet, int inputRowCtr, List<string> inputFileColumnList)
        {
            ExtractDataFromEthinExcelRowOutput returnOutput = new ExtractDataFromEthinExcelRowOutput();

            List<string> EthinFileColumnNameList = new List<string>();

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
                            , Myqy_GetEthinConfigOutputColumns
                            , inputFileColumnList[colCtr]
                            , inputRowCtr
                            , oneBasedColCtr
                         );
                myExcelCellStringValueList.Add(myExcelCellStringValue);
            }

            StringBuilder outputCsvStringBuilder = new StringBuilder();
            foreach (ExcelCellStringValue loopCellValue in myExcelCellStringValueList)
            {
                if (loopCellValue.MyColumnName.Contains("RxPast3Months"))
                {
                    int i = 0;
                    i++;
                }
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

        public List<string> GetEthinFileColumnNameList()
        {
            List<string> returnOutput = new List<string>();
            returnOutput.Add("SummitMrn");
            returnOutput.Add("PatientClass");
            returnOutput.Add("MessageTime");
            returnOutput.Add("LastName");
            returnOutput.Add("FirstName");
            returnOutput.Add("MiddleName");
            returnOutput.Add("Suffix");
            returnOutput.Add("Gender");
            returnOutput.Add("DateOfBirth");
            returnOutput.Add("DateOfDeath");
            returnOutput.Add("SendingFacility");
            returnOutput.Add("AdmitTime");
            returnOutput.Add("DischargeTime");
            returnOutput.Add("AttendingProvider");
            returnOutput.Add("PrimaryCareProvider");
            returnOutput.Add("AdmitSource");
            returnOutput.Add("AdmitReason");
            returnOutput.Add("DischargeStatus");
            returnOutput.Add("FinalDiagnosesList");
            returnOutput.Add("Insurance");
            return returnOutput;
        }
        public dd_EthinOutput TruncateEthinRaw()
        {
            dd_EthinOutput returnOutput =
                new dd_EthinOutput();

            CallWebApiLand.CallWebApiLandClass myCallMyWebApiLandClass =
                new CallWebApiLand.CallWebApiLandClass(this.Myqy_GetEthinConfigOutputColumns.SimplifyVbcAdtBaseWebApiUrl);
            dd_EthinOutput
                mydd_EthinOutput =
                    myCallMyWebApiLandClass.dd_Ethin();
            if (!mydd_EthinOutput.IsOk)
            {
                returnOutput.IsOk = false;
                returnOutput.ErrorMessage =
                    mydd_EthinOutput.ErrorMessage;
                return returnOutput;
            }
            return returnOutput;
        }
        public BulkInsertEthinDataOutput BulkInsertEthinRawData(List<string> inputCsvLineList, string inputSourceFullFilename)
        {
            BulkInsertEthinDataOutput returnOutput =
                new BulkInsertEthinDataOutput();

            // Make sure the Pk column is in the non-CSV column list.
            List<NonCsvFileColumnDefAndValue>
                myNonCsvFileColumnDefAndValueList =
                    new List<NonCsvFileColumnDefAndValue>();

            // AdmitOrDischarge
            spGetColumnDefsForGivenDbAndTableName_OutputColumns myPkColDef =
                new spGetColumnDefsForGivenDbAndTableName_OutputColumns
                {
                    MyDbColumnName = "AdmitOrDischarge",
                    MyDbColumnLength = 300,
                    MyDbColumnType = "nvarchar",
                    MyDbName = "Staging",
                    MyDbTableName = "adt.Ethin",
                    MyFilePosition = 998
                };
            NonCsvFileColumnDefAndValue myPkNonCsvFileColumnDefAndValue =
                            new NonCsvFileColumnDefAndValue
                            {
                                MyColDef = myPkColDef,
                                MyValueString = "Discharge"
                            };
            myNonCsvFileColumnDefAndValueList.Add(myPkNonCsvFileColumnDefAndValue);

            // SourceFullFilename
            myPkColDef =
                new spGetColumnDefsForGivenDbAndTableName_OutputColumns
                {
                    MyDbColumnName = "SourceFullFilename",
                    MyDbColumnLength = 500,
                    MyDbColumnType = "nvarchar",
                    MyDbName = "Staging",
                    MyDbTableName = "adt.Ethin",
                    MyFilePosition = 999
                };
            myPkNonCsvFileColumnDefAndValue =
                            new NonCsvFileColumnDefAndValue
                            {
                                MyColDef = myPkColDef,
                                MyValueString = inputSourceFullFilename
                            };
            myNonCsvFileColumnDefAndValueList.Add(myPkNonCsvFileColumnDefAndValue);


            // EthinID
            myPkColDef =
                new spGetColumnDefsForGivenDbAndTableName_OutputColumns
                {
                    MyDbColumnName = "EthinID",
                    MyDbColumnLength = 1,
                    MyDbColumnType = "int",
                    MyDbName = "Staging",
                    MyDbTableName = "adt.Ethin",
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
                    Myqy_GetEthinConfigOutputColumns.BulkInsertConnectionString //string inputDbConnectionString
                   , Myqy_GetEthinConfigOutputColumns.BulkInsertDbName // string inputDbName
                   , Myqy_GetEthinConfigOutputColumns.BulkInsertDbTableName // string inputDbTableName
                   , inputCsvLineList // List<string> inputCsvLineList
                   , myNonCsvFileColumnDefAndValueList // List<NonCsvFileColumnDefAndValue> inputNonCsvFileColumnDefAndValueList
                   , Myqy_GetEthinConfigOutputColumns.BulkInsertBaseWebApiUrl // string inputBulkInsertWebApiBaseUrl
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
        public di_EthinOutput
                    FinalizeEthinRaw
                    (
                        string inputProcessorTennCareLogOnId
                    )
        {
            di_EthinOutput returnOutput =
                new di_EthinOutput();
            CallWebApiLand.CallWebApiLandClass
                myCallMyWebApiLandClass =
                    new CallWebApiLand.CallWebApiLandClass(this.Myqy_GetEthinConfigOutputColumns.SimplifyVbcAdtBaseWebApiUrl);

            di_EthinOutput
                mydi_EthinOutput =
                    myCallMyWebApiLandClass.di_Ethin();
            if (!mydi_EthinOutput.IsOk)
            {
                returnOutput.IsOk = false;
                returnOutput.ErrorMessage =
                    mydi_EthinOutput.ErrorMessage;
                return returnOutput;
            }
            return returnOutput;
        }

        public qy_GetEthinOutput
                    GetEthinMaster
                    (
                    )
        {
            qy_GetEthinOutput returnOutput =
                new qy_GetEthinOutput();
            CallWebApiLand.CallWebApiLandClass
                myCallMyWebApiLandClass =
                    new CallWebApiLand.CallWebApiLandClass(this.Myqy_GetEthinConfigOutputColumns.SimplifyVbcAdtBaseWebApiUrl);

            returnOutput =
                    myCallMyWebApiLandClass.qy_GetEthin();
            return returnOutput;
        }

        public
            PopulateExcelTemplateWithEthinMasterDataOutput
                PopulateExcelTemplateWithEthinMasterData
                (
                    string inputTemplateExcelFullFilename
                    , List<qy_GetEthinOutputColumns> inputqy_GetEthinOutputColumnsList
                )
        {
            PopulateExcelTemplateWithEthinMasterDataOutput
                returnOutput =
                    new PopulateExcelTemplateWithEthinMasterDataOutput();

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
            for (int rowCtr = 0; rowCtr < inputqy_GetEthinOutputColumnsList.Count; rowCtr++)
            {
                excelRowCtr++;
                sheet.InsertRow(excelRowCtr);
                sheet.Range[$"A{excelRowCtr}"].Value = inputqy_GetEthinOutputColumnsList[rowCtr].SummitMrn;
                sheet.Range[$"B{excelRowCtr}"].Value = inputqy_GetEthinOutputColumnsList[rowCtr].PatientClass;
                sheet.Range[$"C{excelRowCtr}"].Value = inputqy_GetEthinOutputColumnsList[rowCtr].MessageTime;
                sheet.Range[$"D{excelRowCtr}"].Value = inputqy_GetEthinOutputColumnsList[rowCtr].LastName;
                sheet.Range[$"E{excelRowCtr}"].Value = inputqy_GetEthinOutputColumnsList[rowCtr].FirstName;
                sheet.Range[$"F{excelRowCtr}"].Value = inputqy_GetEthinOutputColumnsList[rowCtr].MiddleName;
                sheet.Range[$"G{excelRowCtr}"].Value = inputqy_GetEthinOutputColumnsList[rowCtr].Suffix;
                sheet.Range[$"H{excelRowCtr}"].Value = inputqy_GetEthinOutputColumnsList[rowCtr].Gender;
                sheet.Range[$"I{excelRowCtr}"].Value = inputqy_GetEthinOutputColumnsList[rowCtr].DateOfBirth;
                sheet.Range[$"J{excelRowCtr}"].Value = (inputqy_GetEthinOutputColumnsList[rowCtr].DateOfDeath.CompareTo("1/1/1900") == 0 ? null : inputqy_GetEthinOutputColumnsList[rowCtr].DateOfDeath);
                sheet.Range[$"K{excelRowCtr}"].Value = inputqy_GetEthinOutputColumnsList[rowCtr].SendingFacility;
                sheet.Range[$"L{excelRowCtr}"].Value = inputqy_GetEthinOutputColumnsList[rowCtr].AdmitTime;
                sheet.Range[$"M{excelRowCtr}"].Value = inputqy_GetEthinOutputColumnsList[rowCtr].DischargeTime;
                sheet.Range[$"N{excelRowCtr}"].Value = inputqy_GetEthinOutputColumnsList[rowCtr].AttendingProvider;
                sheet.Range[$"O{excelRowCtr}"].Value = inputqy_GetEthinOutputColumnsList[rowCtr].PrimaryCareProvider;
                sheet.Range[$"P{excelRowCtr}"].Value = inputqy_GetEthinOutputColumnsList[rowCtr].AdmitSource;
                sheet.Range[$"Q{excelRowCtr}"].Value = inputqy_GetEthinOutputColumnsList[rowCtr].AdmitReason;
                sheet.Range[$"R{excelRowCtr}"].Value = inputqy_GetEthinOutputColumnsList[rowCtr].DischargeStatus;
                sheet.Range[$"S{excelRowCtr}"].Value = inputqy_GetEthinOutputColumnsList[rowCtr].FinalDiagnosesList;
                sheet.Range[$"T{excelRowCtr}"].Value = inputqy_GetEthinOutputColumnsList[rowCtr].Insurance;
            }

            workbook.Save();

            return returnOutput;
        }
    }
}


