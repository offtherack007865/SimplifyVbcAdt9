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

namespace SimplifyVbcAdt9.HumanaConsoleApp
{
    public class HumanaMainOps
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(HumanaMainOps));
        public HumanaMainOps(qy_GetHumanaConfigOutputColumns inputqy_GetHumanaConfigOutputColumns)
        {
            Myqy_GetHumanaConfigOutputColumns =
                inputqy_GetHumanaConfigOutputColumns;
        }

        public qy_GetHumanaConfigOutputColumns Myqy_GetHumanaConfigOutputColumns { get; set; }


        public HumanaMainOpsOutput DoIt()
        {
            HumanaMainOpsOutput returnOutput =
                new HumanaMainOpsOutput();

            string wildcardedSearchString = Myqy_GetHumanaConfigOutputColumns.InputFilenameContainsString;
            if (wildcardedSearchString.StartsWith("."))
            {
                wildcardedSearchString =
                    $"*{wildcardedSearchString}";
            }
            List<string>
                fullFilenameList =
                    Directory.GetFiles
                    (
                        Myqy_GetHumanaConfigOutputColumns.ReadDirectory
                        , wildcardedSearchString
                        , SearchOption.TopDirectoryOnly
                    ).ToList();

            if (fullFilenameList.Count > 0)
            {
                foreach (string loopFullFilename in fullFilenameList)
                {
                    ProcessSingleHumanaFileOutput
                        myProcessSingleHumanaFileOutput =
                            ProcessSingleHumanaFile(loopFullFilename);
                    if (myProcessSingleHumanaFileOutput.EmailBodyLine.Length > 0)
                    {
                        returnOutput.NumberOfFilesProcessed++;
                    }
                }
            }

            return returnOutput;
        }
        public ProcessSingleHumanaFileOutput ProcessSingleHumanaFile(string inputFullFilename)
        {
            ProcessSingleHumanaFileOutput returnOutput = new ProcessSingleHumanaFileOutput();

            FileInfo inputFullFilenameFi = new FileInfo(inputFullFilename);


            // Copy old Humana discharges file to Input Archive Directory
            string inputArchiveFullFilename =
                Path.Combine(Myqy_GetHumanaConfigOutputColumns.InputFileArchiveDirectory, inputFullFilenameFi.Name);
            if (File.Exists(inputArchiveFullFilename))
            {
                if (File.Exists(inputArchiveFullFilename))
                {
                    File.Delete(inputArchiveFullFilename);
                }
            }
            File.Copy(inputFullFilename, inputArchiveFullFilename);

            // If the filename already exists in the "already imported archive", do nothing.
            //string inputAlreadyImportedFullFilename =
            //    Path.Combine(Myqy_GetHumanaConfigOutputColumns.ImportArchiveFolder, inputFullFilenameFi.Name);
            //if (File.Exists(inputAlreadyImportedFullFilename))
            //{
            //    if (File.Exists(inputFullFilename))
            //    {
            //        File.Delete(inputFullFilename);
            //    }
            //    return returnOutput;
            //}

            // new Humana filename:  Humana Discharges MM.dd.yy-MM.dd.yy.xslx

            string newHumanaFilename = inputFullFilenameFi.Name;
            Regex lastDateRegEx = new Regex(@"(?<yyyy>\d{4})-(?<MM>\d{2})-(?<dd>\d{2}).xlsx$");
            Match lastDateMatch = lastDateRegEx.Match(newHumanaFilename);

            string MMstring = string.Empty;
            string ddstring = string.Empty;
            string yyyystring = string.Empty;
            DateTime fileDate = DateTime.MinValue;
            if (lastDateMatch.Success)
            {
                MMstring = lastDateMatch.Groups["MM"].Value.Trim();
                ddstring = lastDateMatch.Groups["dd"].Value.Trim();
                yyyystring = lastDateMatch.Groups["yyyy"].Value.Trim();

                DateTime.TryParse($"{MMstring}/{ddstring}/{yyyystring}", out fileDate);
            }
            if (fileDate == DateTime.MinValue)
            {
                if (File.Exists(inputFullFilename))
                {
                    File.Delete(inputFullFilename);
                }
                return returnOutput;
            }

            // Copy input file to the "send to Athena" archive folder.
            string outputFileToAthenaToArchiveFullFilename =
                Path.Combine
                (
                    Myqy_GetHumanaConfigOutputColumns.OutputFileToAthenaArchiveDirectory
                    , inputFullFilenameFi.Name
                );

            if (File.Exists(outputFileToAthenaToArchiveFullFilename))
            {
                File.Delete(outputFileToAthenaToArchiveFullFilename);
            }
            File.Copy
            (
                inputFullFilename
                , outputFileToAthenaToArchiveFullFilename
            );

            // Extract Excel data
            ExtractExcelDataFromHumanaSingleFileOutput
                myExtractExcelDataFromHumanaSingleFileOutput =
                    ExtractExcelDataFromSingleFileSingleFile(inputFullFilename);
            if (!myExtractExcelDataFromHumanaSingleFileOutput.IsOk)
            {
                returnOutput.IsOk = false;
                returnOutput.ErrorMessage = myExtractExcelDataFromHumanaSingleFileOutput.ErrorMessage;
                return returnOutput;
            }

            // Truncate Raw table.
            dd_HumanaOutput
                mydd_HumanaOutput =
                    TruncateHumanaRaw();
            if (!mydd_HumanaOutput.IsOk)
            {
                returnOutput.IsOk = false;
                returnOutput.ErrorMessage =
                    mydd_HumanaOutput.ErrorMessage;
                return returnOutput;
            }

            // Truncate Obs Raw table.
            dd_HumanaObsOutput
                mydd_HumanaObsOutput =
                    TruncateHumanaObsRaw();
            if (!mydd_HumanaObsOutput.IsOk)
            {
                returnOutput.IsOk = false;
                returnOutput.ErrorMessage =
                    mydd_HumanaObsOutput.ErrorMessage;
                return returnOutput;
            }

            // Bulk Insert Humana Raw
            BulkInsertHumanaDataOutput
                myBulkInsertHumanaDataOutput =
                    BulkInsertHumanaRawData
                    (
                        myExtractExcelDataFromHumanaSingleFileOutput.MyCsvLineList
                        , inputFullFilename
                    );
            if (!myBulkInsertHumanaDataOutput.IsOk)
            {
                returnOutput.IsOk = false;
                returnOutput.ErrorMessage =
                    myBulkInsertHumanaDataOutput.ErrorMessage;
                return returnOutput;
            }


            // Bulk Insert Obs Humana Raw 
            BulkInsertHumanaObsDataOutput
                myBulkInsertHumanaObsDataOutput =
                    BulkInsertHumanaObsRawData
                    (
                        myExtractExcelDataFromHumanaSingleFileOutput.MyObsCsvLineList
                        , inputFullFilename
                    );
            if (!myBulkInsertHumanaObsDataOutput.IsOk)
            {
                returnOutput.IsOk = false;
                returnOutput.ErrorMessage =
                    myBulkInsertHumanaObsDataOutput.ErrorMessage;
                return returnOutput;
            }

            // Finalize HumanaRaw table.
            di_HumanaOutput
                mydi_HumanaOutput =
                    FinalizeHumanaRaw(this.Myqy_GetHumanaConfigOutputColumns.SimplifyVbcAdtBaseWebApiUrl);
            if (!mydi_HumanaOutput.IsOk)
            {
                returnOutput.IsOk = false;
                returnOutput.ErrorMessage =
                    mydi_HumanaOutput.ErrorMessage;
                return returnOutput;
            }

            // Finalize HumanaObsRaw table.
            di_HumanaObsOutput
                mydi_HumanaObsOutput =
                    FinalizeHumanaObsRaw(this.Myqy_GetHumanaConfigOutputColumns.SimplifyVbcAdtBaseWebApiUrl);
            if (!mydi_HumanaObsOutput.IsOk)
            {
                returnOutput.IsOk = false;
                returnOutput.ErrorMessage =
                    mydi_HumanaObsOutput.ErrorMessage;
                return returnOutput;
            }

            // Get HumanaMasterAdmits rows
            qy_GetHumanaAdmissionsOutput
                mySpGetHumanaMasterAdmitsOutput =
                    GetHumanaMasterAdmits();
            if (!mySpGetHumanaMasterAdmitsOutput.IsOk)
            {
                returnOutput.IsOk = false;
                returnOutput.ErrorMessage =
                    mySpGetHumanaMasterAdmitsOutput.ErrorMessage;
                return returnOutput;
            }

            // Get HumanaMasterDischarges rows
            qy_GetHumanaDischargesOutput
                mySpGetHumanaMasterDischargesOutput =
                    GetHumanaMasterDischarges();
            if (!mySpGetHumanaMasterDischargesOutput.IsOk)
            {
                returnOutput.IsOk = false;
                returnOutput.ErrorMessage =
                    mySpGetHumanaMasterDischargesOutput.ErrorMessage;
                return returnOutput;
            }

            // Get HumanaObsMaster rows
            qy_GetHumanaObsOutput
                myqy_GetHumanaObsOutput =
                    GetHumanaObsMaster();
            if (!mySpGetHumanaMasterDischargesOutput.IsOk)
            {
                returnOutput.IsOk = false;
                returnOutput.ErrorMessage =
                    mySpGetHumanaMasterDischargesOutput.ErrorMessage;
                return returnOutput;
            }

            // Copy the template to the output Summit ADT File Process folder.
            string outputFileToLightbeamSftpLocationFullFilename =
                Path.Combine
                (
                    Myqy_GetHumanaConfigOutputColumns.OutputFileToLightbeamArchiveDirectory
                    , inputFullFilenameFi.Name
                );

            if (File.Exists(outputFileToLightbeamSftpLocationFullFilename))
            {
                File.Delete(outputFileToLightbeamSftpLocationFullFilename);
            }
            File.Copy
            (
                Myqy_GetHumanaConfigOutputColumns.ExcelTemplateFullFilename
                , outputFileToLightbeamSftpLocationFullFilename
            );


            // Populate the cells of the target spreadsheet; output file to the Lightbeam Archive folder.
            PopulateExcelTemplateWithHumanaMasterDataOutput
                myPopulateExcelTemplateWithHumanaMasterDataOutput =
                    PopulateExcelTemplateWithHumanaMasterData
                    (
                        outputFileToLightbeamSftpLocationFullFilename
                        , mySpGetHumanaMasterAdmitsOutput.qy_GetHumanaOutputColumnsList
                        , mySpGetHumanaMasterDischargesOutput.qy_GetHumanaDischargesOutputColumnsList
                        , myqy_GetHumanaObsOutput.qy_GetHumanaObsOutputColumnsList
                    );
            if (!myPopulateExcelTemplateWithHumanaMasterDataOutput.IsOk)
            {
                returnOutput.IsOk = false;
                returnOutput.ErrorMessage =
                    myPopulateExcelTemplateWithHumanaMasterDataOutput.ErrorMessage;
                return returnOutput;
            }

            // Copy the formatted file to the Athena Archive Directory.
            string outputFileToAthenaArchiveFullFilename =
                Path.Combine
                (
                    Myqy_GetHumanaConfigOutputColumns.OutputFileToAthenaArchiveDirectory
                    , inputFullFilenameFi.Name
                );

            if (File.Exists(outputFileToAthenaArchiveFullFilename))
            {
                File.Delete(outputFileToAthenaArchiveFullFilename);
            }
            File.Copy
            (
                outputFileToLightbeamSftpLocationFullFilename
                , outputFileToAthenaArchiveFullFilename
            );

            // In the Athena Archive Directory, on the "Cover" tab replace the date
            // that of the input file date.

            OnTheCoverTabPlaceTheFileDateInYyyyDashMmDashDdFormat(outputFileToAthenaArchiveFullFilename);

            // In the Athena Archive Directory, delete the Gender column from the
            // "Admits", "Discharges", and "Obs" tabs of
            // the input file.  Athena cannot import records with a Gender column.

            DeleteGenderColumnFromAdmitsDischargesAndObsTabs(outputFileToAthenaArchiveFullFilename);

            // Copy the formatted file sans the "Gender" column to the Summit ADT file
            // Archive Directory.
            string outputFileToSummitAdtArchiveFolderFullFilename =
                Path.Combine
                (
                    Myqy_GetHumanaConfigOutputColumns.OutputFileToSummitAdtProcessingArchiveDirectory
                    , inputFullFilenameFi.Name
                );

            if (File.Exists(outputFileToSummitAdtArchiveFolderFullFilename))
            {
                File.Delete(outputFileToSummitAdtArchiveFolderFullFilename);
            }
            File.Copy
            (
                outputFileToAthenaArchiveFullFilename
                , outputFileToSummitAdtArchiveFolderFullFilename
            );

            //Delete old Humana filename
            if (File.Exists(inputFullFilename))
            {
                File.Delete(inputFullFilename);
            }
            returnOutput.EmailBodyLine = $"Processed file {inputFullFilenameFi.Name}";

            return returnOutput;
        }
        public void OnTheCoverTabPlaceTheFileDateInYyyyDashMmDashDdFormat(string inputFullFilename)
        {
            var pattern = @"_(?<filedate>\d{4}-\d{2}-\d{2}).xlsx";
            var match = Regex.Match(inputFullFilename, pattern);

            string myFileDate = string.Empty;
            if (!match.Success)
            {
                return;
            }
            myFileDate = match.Groups["filedate"].ToString();


            // Load workbook
            Workbook workbook = new Workbook();

            log.Info("B4 Load of workbook {inputFullFilename}");

            workbook.LoadFromFile(inputFullFilename);

            myFileDate = "2024-06-21";
            // get "Cover" worksheet and change the Date to that in the filename.
            Worksheet coverWorksheet = GetWorksheetByName(workbook, "Cover");
            coverWorksheet.Range[$"B6:O6"].Text = myFileDate;
            workbook.SaveToFile(inputFullFilename);

        }

        public void DeleteGenderColumnFromAdmitsDischargesAndObsTabs(string inputFullFilename)
        {
            log.Info("B4 init of Workbook}");

            // Load workbook
            Workbook workbook = new Workbook();

            log.Info("B4 Load of workbook {inputFullFilename}");

            workbook.LoadFromFile(inputFullFilename);

            log.Info("b4 Load of worksheet");

            // Get 'Admits' and 'Discharges' worksheets and delete the Gender column from them.
            List<Worksheet> relevantWorksheetList = new List<Worksheet>();

            relevantWorksheetList.Add(GetWorksheetByName(workbook, "Admits"));
            relevantWorksheetList.Add(GetWorksheetByName(workbook, "Discharges"));

            log.Info("after Load of worksheet");
            foreach (Worksheet loopWorksheet in relevantWorksheetList)
            {
                for (int colCtr = 1; colCtr < 46; colCtr++)
                {
                    loopWorksheet.ShowColumn(colCtr);
                }
                loopWorksheet.DeleteColumn(45);
            }

            // get "Obs" worksheet and delete "Gender" column from it.
            Worksheet obsWorksheet = GetWorksheetByName(workbook, "Obs");
            for (int colCtr = 1; colCtr < 30; colCtr++)
            {
                obsWorksheet.ShowColumn(colCtr);
            }
            obsWorksheet.DeleteColumn(29);

            workbook.SaveToFile(inputFullFilename);

        }


        public void DeleteGenderColumnFromAdmitsAndDischargesTabs(string inputFullFilename)
        {
            log.Info("B4 init of Workbook}");

            // Load workbook
            Workbook workbook = new Workbook();

            log.Info("B4 Load of workbook {inputFullFilename}");

            workbook.LoadFromFile(inputFullFilename);

            log.Info("b4 Load of worksheet");

            // Get 'Admits' and 'Discharges' worksheets and delete the Gender column from them.
            List<Worksheet> relevantWorksheetList = new List<Worksheet>();

            relevantWorksheetList.Add(GetWorksheetByName(workbook, "Admits"));
            relevantWorksheetList.Add(GetWorksheetByName(workbook, "Discharges"));

            log.Info("after Load of worksheet");
            foreach (Worksheet loopWorksheet in relevantWorksheetList)
            {
                for (int colCtr = 1; colCtr < 46; colCtr++)
                {
                    loopWorksheet.ShowColumn(colCtr);
                }
                loopWorksheet.DeleteColumn(45);
            }

            workbook.SaveToFile(inputFullFilename);

        }






















        public Worksheet GetWorksheetByName(Workbook inputWorkbook, string inputWorksheetName)
        {
            Worksheet returnWorksheet = null;

            returnWorksheet = (Worksheet?)inputWorkbook.Worksheets.Where(w => w.Name.CompareTo(inputWorksheetName) == 0).FirstOrDefault();

            return returnWorksheet;
        }

        public ExtractExcelDataFromHumanaSingleFileOutput ExtractExcelDataFromSingleFileSingleFile(string inputFullFilename)
        {
            ExtractExcelDataFromHumanaSingleFileOutput returnOutput = new ExtractExcelDataFromHumanaSingleFileOutput();

            log.Info("B4 init of Workbook}");

            // Load workbook
            Workbook workbook = new Workbook();

            log.Info("B4 Load of workbook {inputFullFilename}");

            workbook.LoadFromFile(inputFullFilename);

            log.Info("b4 Load of worksheet");

            // Get 'Admits' worksheet
            List<Worksheet> relevantWorksheetList = new List<Worksheet>();

            relevantWorksheetList.Add(GetWorksheetByName(workbook, "Admits"));
            relevantWorksheetList.Add(GetWorksheetByName(workbook, "Discharges"));


            log.Info("after Load of worksheet");
            List<string>
                    HumanaFileColumnNameList =
                        GetHumanaFileColumnNameList();

            ExtractDataFromHumanaExcelRowOutput
                myExtractDataFromHumanaExcelRowOutput =
                    new ExtractDataFromHumanaExcelRowOutput();

            returnOutput.MyCsvLineList = new List<string>();
            for (int worksheetCtr = 0; worksheetCtr < relevantWorksheetList.Count; worksheetCtr++)
            {
                HumanaFileColumnNameList =
                        GetHumanaFileColumnNameList();

                myExtractDataFromHumanaExcelRowOutput =
                    new ExtractDataFromHumanaExcelRowOutput();

                int rowCtr = -1;
                int.TryParse(Myqy_GetHumanaConfigOutputColumns.LineToLookForSearchString, out rowCtr);
                while (true)
                {
                    if (rowCtr == 362)
                    {
                        int i = 0;
                        i++;
                    }
                    myExtractDataFromHumanaExcelRowOutput =
                        new ExtractDataFromHumanaExcelRowOutput();
                    myExtractDataFromHumanaExcelRowOutput =
                        ExtractDataFromExcelRow
                        (
                            relevantWorksheetList[worksheetCtr]
                            , rowCtr
                            , HumanaFileColumnNameList
                        );
                    if (!myExtractDataFromHumanaExcelRowOutput.IsOk)
                    {
                        returnOutput.IsOk = false;
                        returnOutput.ErrorMessage = myExtractDataFromHumanaExcelRowOutput.ErrorMessage;
                        returnOutput.MyCsvLineList = new List<string>();
                        return returnOutput;
                    }
                    if (myExtractDataFromHumanaExcelRowOutput.OutputCsvString.Length == 0)
                    {
                        break;
                    }

                    switch (worksheetCtr)
                    {
                        case 0:
                            returnOutput.MyCsvLineList.Add($"{myExtractDataFromHumanaExcelRowOutput.OutputCsvString},Admit");
                            break;
                        case 1:
                            returnOutput.MyCsvLineList.Add($"{myExtractDataFromHumanaExcelRowOutput.OutputCsvString},Discharge");
                            break;
                    }

                    rowCtr++;
                }
            }

            // Get Obs Tab Data.

            List<string>
                HumanaFileObsColumnNameList =
                    GetHumanaFileObsColumnNameList();

            myExtractDataFromHumanaExcelRowOutput =
               new ExtractDataFromHumanaExcelRowOutput();

            Worksheet obsWorksheet = GetWorksheetByName(workbook, "Obs");
            int rowCtr2 = 0;
            int.TryParse(Myqy_GetHumanaConfigOutputColumns.LineToLookForSearchString, out rowCtr2);
            while (true)
            {
                if (rowCtr2 == 362)
                {
                    int i = 0;
                    i++;
                }
                myExtractDataFromHumanaExcelRowOutput =
                    new ExtractDataFromHumanaExcelRowOutput();
                myExtractDataFromHumanaExcelRowOutput =
                    ExtractDataFromExcelRow
                    (
                        obsWorksheet
                        , rowCtr2
                        , HumanaFileObsColumnNameList
                    );
                if (!myExtractDataFromHumanaExcelRowOutput.IsOk)
                {
                    returnOutput.IsOk = false;
                    returnOutput.ErrorMessage = myExtractDataFromHumanaExcelRowOutput.ErrorMessage;
                    returnOutput.MyObsCsvLineList = new List<string>();
                    return returnOutput;
                }
                if (myExtractDataFromHumanaExcelRowOutput.OutputCsvString.Length == 0)
                {
                    break;
                }

                returnOutput.MyObsCsvLineList.Add(myExtractDataFromHumanaExcelRowOutput.OutputCsvString);
                rowCtr2++;
            }











            return returnOutput;
        }
        public ExtractDataFromHumanaExcelRowOutput ExtractDataFromExcelRow(Worksheet inputWorksheet, int inputRowCtr, List<string> inputFileColumnList)
        {
            ExtractDataFromHumanaExcelRowOutput returnOutput = new ExtractDataFromHumanaExcelRowOutput();

            List<string> HumanaFileColumnNameList = new List<string>();

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
                            , Myqy_GetHumanaConfigOutputColumns
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

        public List<string> GetHumanaFileColumnNameList()
        {
            List<string> returnOutput = new List<string>();
            returnOutput.Add("RptDt");
            returnOutput.Add("Type");
            returnOutput.Add("Group");
            returnOutput.Add("GrouperName");
            returnOutput.Add("FacDbName");
            returnOutput.Add("PcpName");
            returnOutput.Add("BedType");
            returnOutput.Add("AuthId");
            returnOutput.Add("AdmitDt");
            returnOutput.Add("DcDate");
            returnOutput.Add("DcDisposition");
            returnOutput.Add("SubscriberId");
            returnOutput.Add("LastName");
            returnOutput.Add("FirstName");
            returnOutput.Add("DateOfBirth");
            returnOutput.Add("IsCaseReAdmit14dy");
            returnOutput.Add("IsCaseReAdmit30dy");
            returnOutput.Add("ReadmitScore");
            returnOutput.Add("RiskScoreGroup");
            returnOutput.Add("ClinicalProgram");
            returnOutput.Add("CaseName");
            returnOutput.Add("CaseStatus");
            returnOutput.Add("GrouperId");
            returnOutput.Add("PcpId");
            returnOutput.Add("FaxTaxId");
            returnOutput.Add("AdmitType");
            returnOutput.Add("RequestType");
            returnOutput.Add("NotifDate");
            returnOutput.Add("AuthorizedDays");
            returnOutput.Add("ContactType");
            returnOutput.Add("ContactName");
            returnOutput.Add("ContactPhone");
            returnOutput.Add("ContactNbrExt");
            returnOutput.Add("ContactEmail");
            returnOutput.Add("DiagCode1");
            returnOutput.Add("DiagDesc1");
            returnOutput.Add("DiagCode2");
            returnOutput.Add("DiagDesc2");
            returnOutput.Add("ProcCode1");
            returnOutput.Add("ProcDesc1");
            returnOutput.Add("ProcCode2");
            returnOutput.Add("ProcDesc2");
            returnOutput.Add("AuthSource");
            returnOutput.Add("LobDesc");
            returnOutput.Add("Gender");
            return returnOutput;
        }

        public List<string> GetHumanaFileObsColumnNameList()
        {
            List<string> returnOutput = new List<string>();
            returnOutput.Add("RptDt");
            returnOutput.Add("Type");
            returnOutput.Add("Group");
            returnOutput.Add("GrouperName");
            returnOutput.Add("FacDbName");
            returnOutput.Add("PcpName");
            returnOutput.Add("CaseType");
            returnOutput.Add("AuthId");
            returnOutput.Add("FirstDay");
            returnOutput.Add("SubscriberId");
            returnOutput.Add("FirstName");
            returnOutput.Add("LastName");
            returnOutput.Add("DateOfBirth");
            returnOutput.Add("GrouperId");
            returnOutput.Add("PcpId");
            returnOutput.Add("FaxTaxId");
            returnOutput.Add("AuthType");
            returnOutput.Add("RequestType");
            returnOutput.Add("NotifDate");
            returnOutput.Add("LastDay");
            returnOutput.Add("DiagCode1");
            returnOutput.Add("DiagDesc1");
            returnOutput.Add("DiagCode2");
            returnOutput.Add("DiagDesc2");
            returnOutput.Add("DiagCode3");
            returnOutput.Add("DiagDesc3");
            returnOutput.Add("ProcCode");
            returnOutput.Add("ProcDesc");
            returnOutput.Add("Gender");
            return returnOutput;
        }

        public dd_HumanaOutput TruncateHumanaRaw()
        {
            dd_HumanaOutput returnOutput =
                new dd_HumanaOutput();

            CallWebApiLand.CallWebApiLandClass myCallMyWebApiLandClass =
                new CallWebApiLand.CallWebApiLandClass(this.Myqy_GetHumanaConfigOutputColumns.SimplifyVbcAdtBaseWebApiUrl);
            returnOutput =
                    myCallMyWebApiLandClass.dd_Humana();
            return returnOutput;
        }
        public BulkInsertHumanaDataOutput BulkInsertHumanaRawData(List<string> inputCsvLineList, string inputSourceFullFilename)
        {
            BulkInsertHumanaDataOutput returnOutput =
                new BulkInsertHumanaDataOutput();

            // Make sure the Pk column is in the non-CSV column list.
            List<NonCsvFileColumnDefAndValue>
                myNonCsvFileColumnDefAndValueList =
                    new List<NonCsvFileColumnDefAndValue>();

            // SourceFullFilename
            spGetColumnDefsForGivenDbAndTableName_OutputColumns myPkColDef =
                new spGetColumnDefsForGivenDbAndTableName_OutputColumns
                {
                    MyDbColumnName = "SourceFullFilename",
                    MyDbColumnLength = 1000,
                    MyDbColumnType = "nvarchar",
                    MyDbName = "Staging",
                    MyDbTableName = "adt.Humana",
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
                    MyDbColumnName = "HumanaID",
                    MyDbColumnLength = 1,
                    MyDbColumnType = "int",
                    MyDbName = "Staging",
                    MyDbTableName = "adt.Humana",
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
                    Myqy_GetHumanaConfigOutputColumns.BulkInsertConnectionString //string inputDbConnectionString
                   , Myqy_GetHumanaConfigOutputColumns.BulkInsertDbName // string inputDbName
                   , Myqy_GetHumanaConfigOutputColumns.BulkInsertDbTableName // string inputDbTableName
                   , inputCsvLineList // List<string> inputCsvLineList
                   , myNonCsvFileColumnDefAndValueList // List<NonCsvFileColumnDefAndValue> inputNonCsvFileColumnDefAndValueList
                   , Myqy_GetHumanaConfigOutputColumns.BulkInsertBaseWebApiUrl // string inputBulkInsertWebApiBaseUrl
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
        public di_HumanaOutput
                    FinalizeHumanaRaw
                    (
                        string inputProcessorTennCareLogOnId
                    )
        {
            di_HumanaOutput returnOutput =
                new di_HumanaOutput();
            CallWebApiLand.CallWebApiLandClass
                myCallMyWebApiLandClass =
                    new CallWebApiLand.CallWebApiLandClass(this.Myqy_GetHumanaConfigOutputColumns.SimplifyVbcAdtBaseWebApiUrl);

            di_HumanaOutput
                mydi_HumanaOutput =
                    myCallMyWebApiLandClass.di_Humana();
            if (!mydi_HumanaOutput.IsOk)
            {
                returnOutput.IsOk = false;
                returnOutput.ErrorMessage =
                    mydi_HumanaOutput.ErrorMessage;
                return returnOutput;
            }
            return returnOutput;
        }

        public qy_GetHumanaAdmissionsOutput
                    GetHumanaMasterAdmits()
        {
            qy_GetHumanaAdmissionsOutput returnOutput =
                new qy_GetHumanaAdmissionsOutput();
            CallWebApiLand.CallWebApiLandClass
                myCallMyWebApiLandClass =
                    new CallWebApiLand.CallWebApiLandClass(this.Myqy_GetHumanaConfigOutputColumns.SimplifyVbcAdtBaseWebApiUrl);

            returnOutput =
                myCallMyWebApiLandClass.qy_GetHumanaAdmissions();
            if (!returnOutput.IsOk)
            {
                returnOutput.IsOk = false;
                returnOutput.ErrorMessage =
                    returnOutput.ErrorMessage;
                return returnOutput;
            }
            return returnOutput;
        }

        public qy_GetHumanaDischargesOutput
                    GetHumanaMasterDischarges()
        {
            qy_GetHumanaDischargesOutput returnOutput =
                new qy_GetHumanaDischargesOutput();
            CallWebApiLand.CallWebApiLandClass
                myCallMyWebApiLandClass =
                    new CallWebApiLand.CallWebApiLandClass(this.Myqy_GetHumanaConfigOutputColumns.SimplifyVbcAdtBaseWebApiUrl);

            returnOutput =
                myCallMyWebApiLandClass.qy_GetHumanaDischarges();
            if (!returnOutput.IsOk)
            {
                returnOutput.IsOk = false;
                returnOutput.ErrorMessage =
                    returnOutput.ErrorMessage;
                return returnOutput;
            }
            return returnOutput;
        }




        public dd_HumanaObsOutput TruncateHumanaObsRaw()
        {
            dd_HumanaObsOutput returnOutput =
                new dd_HumanaObsOutput();

            CallWebApiLand.CallWebApiLandClass myCallMyWebApiLandClass =
                new CallWebApiLand.CallWebApiLandClass(this.Myqy_GetHumanaConfigOutputColumns.SimplifyVbcAdtBaseWebApiUrl);
            returnOutput =
                    myCallMyWebApiLandClass.dd_HumanaObs();
            return returnOutput;
        }
        public BulkInsertHumanaObsDataOutput BulkInsertHumanaObsRawData(List<string> inputCsvLineList, string inputSourceFullFilename)
        {
            BulkInsertHumanaObsDataOutput returnOutput =
                new BulkInsertHumanaObsDataOutput();

            // Make sure the Pk column is in the non-CSV column list.
            List<NonCsvFileColumnDefAndValue>
                myNonCsvFileColumnDefAndValueList =
                    new List<NonCsvFileColumnDefAndValue>();

            // SourceFullFilename
            spGetColumnDefsForGivenDbAndTableName_OutputColumns myPkColDef =
                new spGetColumnDefsForGivenDbAndTableName_OutputColumns
                {
                    MyDbColumnName = "SourceFullFilename",
                    MyDbColumnLength = 300,
                    MyDbColumnType = "nvarchar",
                    MyDbName = "Staging",
                    MyDbTableName = "adt.HumanaObs",
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
                    MyDbColumnName = "HumanaObsID",
                    MyDbColumnLength = 1,
                    MyDbColumnType = "int",
                    MyDbName = "Staging",
                    MyDbTableName = "adt.HumanaObs",
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
                    Myqy_GetHumanaConfigOutputColumns.BulkInsertConnectionString //string inputDbConnectionString
                   , Myqy_GetHumanaConfigOutputColumns.BulkInsertDbName // string inputDbName
                   , "adt.HumanaObs" // string inputDbTableName
                   , inputCsvLineList // List<string> inputCsvLineList
                   , myNonCsvFileColumnDefAndValueList // List<NonCsvFileColumnDefAndValue> inputNonCsvFileColumnDefAndValueList
                   , Myqy_GetHumanaConfigOutputColumns.BulkInsertBaseWebApiUrl // string inputBulkInsertWebApiBaseUrl
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
        public di_HumanaObsOutput
                    FinalizeHumanaObsRaw
                    (
                        string inputProcessorTennCareLogOnId
                    )
        {
            di_HumanaObsOutput returnOutput =
                new di_HumanaObsOutput();
            CallWebApiLand.CallWebApiLandClass
                myCallMyWebApiLandClass =
                    new CallWebApiLand.CallWebApiLandClass(this.Myqy_GetHumanaConfigOutputColumns.SimplifyVbcAdtBaseWebApiUrl);

            di_HumanaObsOutput
                mydi_HumanaObsOutput =
                    myCallMyWebApiLandClass.di_HumanaObs();
            if (!mydi_HumanaObsOutput.IsOk)
            {
                returnOutput.IsOk = false;
                returnOutput.ErrorMessage =
                    mydi_HumanaObsOutput.ErrorMessage;
                return returnOutput;
            }
            return returnOutput;
        }

        public qy_GetHumanaObsOutput
                    GetHumanaObsMaster()
        {
            qy_GetHumanaObsOutput returnOutput =
                new qy_GetHumanaObsOutput();
            CallWebApiLand.CallWebApiLandClass
                myCallMyWebApiLandClass =
                    new CallWebApiLand.CallWebApiLandClass(this.Myqy_GetHumanaConfigOutputColumns.SimplifyVbcAdtBaseWebApiUrl);

            returnOutput =
                myCallMyWebApiLandClass.qy_GetHumanaObs();
            if (!returnOutput.IsOk)
            {
                returnOutput.IsOk = false;
                returnOutput.ErrorMessage =
                    returnOutput.ErrorMessage;
                return returnOutput;
            }
            return returnOutput;
        }





























        public
            PopulateExcelTemplateWithHumanaMasterDataOutput
                PopulateExcelTemplateWithHumanaMasterData
                (
                    string inputTemplateExcelFullFilename
                    , List<qy_GetHumanaAdmissionsOutputColumns> inputSpGetHumanaMasterAdmitsOutputColumnsList
                    , List<qy_GetHumanaDischargesOutputColumns> inputSpGetHumanaMasterDischargesOutputColumnsList
                    , List<qy_GetHumanaObsOutputColumns> inputqy_GetHumanaObsOutputColumnsList
                )
        {
            PopulateExcelTemplateWithHumanaMasterDataOutput
                returnOutput =
                    new PopulateExcelTemplateWithHumanaMasterDataOutput();

            log.Info("B4 init of Workbook}");

            // Load workbook
            Workbook workbook = new Workbook();

            log.Info("B4 Load of workbook {inputTemplateExcelFullFilename}");

            workbook.LoadFromFile(inputTemplateExcelFullFilename);

            log.Info("b4 Load of worksheet");

            // Get first worksheet
            Worksheet admitsSheet = workbook.Worksheets[1];
            Worksheet dischargesSheet = workbook.Worksheets[2];
            Worksheet obsSheet = workbook.Worksheets[3];

            log.Info($"after Load of Admits worksheet number of rows:  {admitsSheet.Rows.Length}");
            int startingExcelRow = 1;
            int excelRowCtr = startingExcelRow;
            for (int rowCtr = 0; rowCtr < inputSpGetHumanaMasterAdmitsOutputColumnsList.Count; rowCtr++)
            {
                excelRowCtr++;
                admitsSheet.InsertRow(excelRowCtr);
                admitsSheet.Range[$"A{excelRowCtr}"].Text = inputSpGetHumanaMasterAdmitsOutputColumnsList[rowCtr].RptDt;
                admitsSheet.Range[$"B{excelRowCtr}"].Text = inputSpGetHumanaMasterAdmitsOutputColumnsList[rowCtr].Type;
                admitsSheet.Range[$"C{excelRowCtr}"].Text = inputSpGetHumanaMasterAdmitsOutputColumnsList[rowCtr].Group;
                admitsSheet.Range[$"D{excelRowCtr}"].Text = inputSpGetHumanaMasterAdmitsOutputColumnsList[rowCtr].GrouperName;
                admitsSheet.Range[$"E{excelRowCtr}"].Text = inputSpGetHumanaMasterAdmitsOutputColumnsList[rowCtr].FacDbName;
                admitsSheet.Range[$"F{excelRowCtr}"].Text = inputSpGetHumanaMasterAdmitsOutputColumnsList[rowCtr].PcpName;
                admitsSheet.Range[$"G{excelRowCtr}"].Text = inputSpGetHumanaMasterAdmitsOutputColumnsList[rowCtr].BedType;
                admitsSheet.Range[$"H{excelRowCtr}"].Text = inputSpGetHumanaMasterAdmitsOutputColumnsList[rowCtr].AuthId;
                admitsSheet.Range[$"I{excelRowCtr}"].Text = inputSpGetHumanaMasterAdmitsOutputColumnsList[rowCtr].AdmitDt;
                admitsSheet.Range[$"J{excelRowCtr}"].Text = inputSpGetHumanaMasterAdmitsOutputColumnsList[rowCtr].DcDate;
                admitsSheet.Range[$"K{excelRowCtr}"].Text = inputSpGetHumanaMasterAdmitsOutputColumnsList[rowCtr].DcDisposition;
                admitsSheet.Range[$"L{excelRowCtr}"].Text = inputSpGetHumanaMasterAdmitsOutputColumnsList[rowCtr].SubscriberId;
                admitsSheet.Range[$"M{excelRowCtr}"].Text = inputSpGetHumanaMasterAdmitsOutputColumnsList[rowCtr].LastName;
                admitsSheet.Range[$"N{excelRowCtr}"].Text = inputSpGetHumanaMasterAdmitsOutputColumnsList[rowCtr].FirstName;
                admitsSheet.Range[$"O{excelRowCtr}"].Text = inputSpGetHumanaMasterAdmitsOutputColumnsList[rowCtr].DateOfBirth;
                admitsSheet.Range[$"P{excelRowCtr}"].Text = inputSpGetHumanaMasterAdmitsOutputColumnsList[rowCtr].IsCaseReAdmit14dy;
                admitsSheet.Range[$"Q{excelRowCtr}"].Text = inputSpGetHumanaMasterAdmitsOutputColumnsList[rowCtr].IsCaseReAdmit30dy;
                admitsSheet.Range[$"R{excelRowCtr}"].Text = inputSpGetHumanaMasterAdmitsOutputColumnsList[rowCtr].ReadmitScore.ToString();
                admitsSheet.Range[$"S{excelRowCtr}"].Text = inputSpGetHumanaMasterAdmitsOutputColumnsList[rowCtr].RiskScoreGroup;
                admitsSheet.Range[$"T{excelRowCtr}"].Text = inputSpGetHumanaMasterAdmitsOutputColumnsList[rowCtr].ClinicalProgram;
                admitsSheet.Range[$"U{excelRowCtr}"].Text = inputSpGetHumanaMasterAdmitsOutputColumnsList[rowCtr].CaseName;
                admitsSheet.Range[$"V{excelRowCtr}"].Text = inputSpGetHumanaMasterAdmitsOutputColumnsList[rowCtr].CaseStatus;
                admitsSheet.Range[$"W{excelRowCtr}"].Text = inputSpGetHumanaMasterAdmitsOutputColumnsList[rowCtr].GrouperId;
                admitsSheet.Range[$"X{excelRowCtr}"].Text = inputSpGetHumanaMasterAdmitsOutputColumnsList[rowCtr].PcpId;
                admitsSheet.Range[$"Y{excelRowCtr}"].Text = inputSpGetHumanaMasterAdmitsOutputColumnsList[rowCtr].FaxTaxId;
                admitsSheet.Range[$"Z{excelRowCtr}"].Text = inputSpGetHumanaMasterAdmitsOutputColumnsList[rowCtr].AdmitType;
                admitsSheet.Range[$"AA{excelRowCtr}"].Text = inputSpGetHumanaMasterAdmitsOutputColumnsList[rowCtr].RequestType;
                admitsSheet.Range[$"AB{excelRowCtr}"].Text = inputSpGetHumanaMasterAdmitsOutputColumnsList[rowCtr].NotifDate;
                admitsSheet.Range[$"AC{excelRowCtr}"].Text = inputSpGetHumanaMasterAdmitsOutputColumnsList[rowCtr].AuthorizedDays;
                admitsSheet.Range[$"AD{excelRowCtr}"].Text = inputSpGetHumanaMasterAdmitsOutputColumnsList[rowCtr].ContactType;
                admitsSheet.Range[$"AE{excelRowCtr}"].Text = inputSpGetHumanaMasterAdmitsOutputColumnsList[rowCtr].ContactName;
                admitsSheet.Range[$"AF{excelRowCtr}"].Text = inputSpGetHumanaMasterAdmitsOutputColumnsList[rowCtr].ContactPhone;
                admitsSheet.Range[$"AG{excelRowCtr}"].Text = inputSpGetHumanaMasterAdmitsOutputColumnsList[rowCtr].ContactNbrExt;
                admitsSheet.Range[$"AH{excelRowCtr}"].Text = inputSpGetHumanaMasterAdmitsOutputColumnsList[rowCtr].ContactEmail;
                admitsSheet.Range[$"AI{excelRowCtr}"].Text = inputSpGetHumanaMasterAdmitsOutputColumnsList[rowCtr].DiagCode1;
                admitsSheet.Range[$"AJ{excelRowCtr}"].Text = inputSpGetHumanaMasterAdmitsOutputColumnsList[rowCtr].DiagDesc1;
                admitsSheet.Range[$"AK{excelRowCtr}"].Text = inputSpGetHumanaMasterAdmitsOutputColumnsList[rowCtr].DiagCode2;
                admitsSheet.Range[$"AL{excelRowCtr}"].Text = inputSpGetHumanaMasterAdmitsOutputColumnsList[rowCtr].DiagDesc2;
                admitsSheet.Range[$"AM{excelRowCtr}"].Text = inputSpGetHumanaMasterAdmitsOutputColumnsList[rowCtr].ProcCode1;
                admitsSheet.Range[$"AN{excelRowCtr}"].Text = inputSpGetHumanaMasterAdmitsOutputColumnsList[rowCtr].ProcDesc1;
                admitsSheet.Range[$"AO{excelRowCtr}"].Text = inputSpGetHumanaMasterAdmitsOutputColumnsList[rowCtr].ProcCode2;
                admitsSheet.Range[$"AP{excelRowCtr}"].Text = inputSpGetHumanaMasterAdmitsOutputColumnsList[rowCtr].ProcDesc2;
                admitsSheet.Range[$"AQ{excelRowCtr}"].Text = inputSpGetHumanaMasterAdmitsOutputColumnsList[rowCtr].AuthSource;
                admitsSheet.Range[$"AR{excelRowCtr}"].Text = inputSpGetHumanaMasterAdmitsOutputColumnsList[rowCtr].LobDesc;
                admitsSheet.Range[$"AS{excelRowCtr}"].Text = inputSpGetHumanaMasterAdmitsOutputColumnsList[rowCtr].Gender;
            }



            log.Info($"after Load of Discharges worksheet number of rows:  {dischargesSheet.Rows.Length}");
            startingExcelRow = 1;
            excelRowCtr = startingExcelRow;
            for (int rowCtr = 0; rowCtr < inputSpGetHumanaMasterDischargesOutputColumnsList.Count; rowCtr++)
            {
                excelRowCtr++;
                dischargesSheet.InsertRow(excelRowCtr);
                dischargesSheet.Range[$"A{excelRowCtr}"].Text = inputSpGetHumanaMasterDischargesOutputColumnsList[rowCtr].RptDt;
                dischargesSheet.Range[$"B{excelRowCtr}"].Text = inputSpGetHumanaMasterDischargesOutputColumnsList[rowCtr].Type;
                dischargesSheet.Range[$"C{excelRowCtr}"].Text = inputSpGetHumanaMasterDischargesOutputColumnsList[rowCtr].Group;
                dischargesSheet.Range[$"D{excelRowCtr}"].Text = inputSpGetHumanaMasterDischargesOutputColumnsList[rowCtr].GrouperName;
                dischargesSheet.Range[$"E{excelRowCtr}"].Text = inputSpGetHumanaMasterDischargesOutputColumnsList[rowCtr].FacDbName;
                dischargesSheet.Range[$"F{excelRowCtr}"].Text = inputSpGetHumanaMasterDischargesOutputColumnsList[rowCtr].PcpName;
                dischargesSheet.Range[$"G{excelRowCtr}"].Text = inputSpGetHumanaMasterDischargesOutputColumnsList[rowCtr].BedType;
                dischargesSheet.Range[$"H{excelRowCtr}"].Text = inputSpGetHumanaMasterDischargesOutputColumnsList[rowCtr].AuthId;
                dischargesSheet.Range[$"I{excelRowCtr}"].Text = inputSpGetHumanaMasterDischargesOutputColumnsList[rowCtr].AdmitDt;
                dischargesSheet.Range[$"J{excelRowCtr}"].Text = inputSpGetHumanaMasterDischargesOutputColumnsList[rowCtr].DcDate;
                dischargesSheet.Range[$"K{excelRowCtr}"].Text = inputSpGetHumanaMasterDischargesOutputColumnsList[rowCtr].DcDisposition;
                dischargesSheet.Range[$"L{excelRowCtr}"].Text = inputSpGetHumanaMasterDischargesOutputColumnsList[rowCtr].SubscriberId;
                dischargesSheet.Range[$"M{excelRowCtr}"].Text = inputSpGetHumanaMasterDischargesOutputColumnsList[rowCtr].LastName;
                dischargesSheet.Range[$"N{excelRowCtr}"].Text = inputSpGetHumanaMasterDischargesOutputColumnsList[rowCtr].FirstName;
                dischargesSheet.Range[$"O{excelRowCtr}"].Text = inputSpGetHumanaMasterDischargesOutputColumnsList[rowCtr].DateOfBirth;
                dischargesSheet.Range[$"P{excelRowCtr}"].Text = inputSpGetHumanaMasterDischargesOutputColumnsList[rowCtr].IsCaseReAdmit14dy;
                dischargesSheet.Range[$"Q{excelRowCtr}"].Text = inputSpGetHumanaMasterDischargesOutputColumnsList[rowCtr].IsCaseReAdmit30dy;
                dischargesSheet.Range[$"R{excelRowCtr}"].Text = inputSpGetHumanaMasterDischargesOutputColumnsList[rowCtr].ReadmitScore;
                dischargesSheet.Range[$"S{excelRowCtr}"].Text = inputSpGetHumanaMasterDischargesOutputColumnsList[rowCtr].RiskScoreGroup;
                dischargesSheet.Range[$"T{excelRowCtr}"].Text = inputSpGetHumanaMasterDischargesOutputColumnsList[rowCtr].ClinicalProgram;
                dischargesSheet.Range[$"U{excelRowCtr}"].Text = inputSpGetHumanaMasterDischargesOutputColumnsList[rowCtr].CaseName;
                dischargesSheet.Range[$"V{excelRowCtr}"].Text = inputSpGetHumanaMasterDischargesOutputColumnsList[rowCtr].CaseStatus;
                dischargesSheet.Range[$"W{excelRowCtr}"].Text = inputSpGetHumanaMasterDischargesOutputColumnsList[rowCtr].GrouperId;
                dischargesSheet.Range[$"X{excelRowCtr}"].Text = inputSpGetHumanaMasterDischargesOutputColumnsList[rowCtr].PcpId;
                dischargesSheet.Range[$"Y{excelRowCtr}"].Text = inputSpGetHumanaMasterDischargesOutputColumnsList[rowCtr].FaxTaxId;
                dischargesSheet.Range[$"Z{excelRowCtr}"].Text = inputSpGetHumanaMasterDischargesOutputColumnsList[rowCtr].AdmitType;
                dischargesSheet.Range[$"AA{excelRowCtr}"].Text = inputSpGetHumanaMasterDischargesOutputColumnsList[rowCtr].RequestType;
                dischargesSheet.Range[$"AB{excelRowCtr}"].Text = inputSpGetHumanaMasterDischargesOutputColumnsList[rowCtr].NotifDate;
                dischargesSheet.Range[$"AC{excelRowCtr}"].Text = inputSpGetHumanaMasterDischargesOutputColumnsList[rowCtr].AuthorizedDays;
                dischargesSheet.Range[$"AD{excelRowCtr}"].Text = inputSpGetHumanaMasterDischargesOutputColumnsList[rowCtr].ContactType;
                dischargesSheet.Range[$"AE{excelRowCtr}"].Text = inputSpGetHumanaMasterDischargesOutputColumnsList[rowCtr].ContactName;
                dischargesSheet.Range[$"AF{excelRowCtr}"].Text = inputSpGetHumanaMasterDischargesOutputColumnsList[rowCtr].ContactPhone;
                dischargesSheet.Range[$"AG{excelRowCtr}"].Text = inputSpGetHumanaMasterDischargesOutputColumnsList[rowCtr].ContactNbrExt;
                dischargesSheet.Range[$"AH{excelRowCtr}"].Text = inputSpGetHumanaMasterDischargesOutputColumnsList[rowCtr].ContactEmail;
                dischargesSheet.Range[$"AI{excelRowCtr}"].Text = inputSpGetHumanaMasterDischargesOutputColumnsList[rowCtr].DiagCode1;
                dischargesSheet.Range[$"AJ{excelRowCtr}"].Text = inputSpGetHumanaMasterDischargesOutputColumnsList[rowCtr].DiagDesc1;
                dischargesSheet.Range[$"AK{excelRowCtr}"].Text = inputSpGetHumanaMasterDischargesOutputColumnsList[rowCtr].DiagCode2;
                dischargesSheet.Range[$"AL{excelRowCtr}"].Text = inputSpGetHumanaMasterDischargesOutputColumnsList[rowCtr].DiagDesc2;
                dischargesSheet.Range[$"AM{excelRowCtr}"].Text = inputSpGetHumanaMasterDischargesOutputColumnsList[rowCtr].ProcCode1.ToString();
                dischargesSheet.Range[$"AN{excelRowCtr}"].Text = inputSpGetHumanaMasterDischargesOutputColumnsList[rowCtr].ProcDesc1;
                dischargesSheet.Range[$"AO{excelRowCtr}"].Text = inputSpGetHumanaMasterDischargesOutputColumnsList[rowCtr].ProcCode2.ToString();
                dischargesSheet.Range[$"AP{excelRowCtr}"].Text = inputSpGetHumanaMasterDischargesOutputColumnsList[rowCtr].ProcDesc2;
                dischargesSheet.Range[$"AQ{excelRowCtr}"].Text = inputSpGetHumanaMasterDischargesOutputColumnsList[rowCtr].AuthSource;
                dischargesSheet.Range[$"AR{excelRowCtr}"].Text = inputSpGetHumanaMasterDischargesOutputColumnsList[rowCtr].LobDesc;
                dischargesSheet.Range[$"AS{excelRowCtr}"].Text = inputSpGetHumanaMasterDischargesOutputColumnsList[rowCtr].Gender;
            }

            log.Info($"after Load of obs worksheet number of rows:  {obsSheet.Rows.Length}");
            startingExcelRow = 1;
            excelRowCtr = startingExcelRow;
            for (int rowCtr = 0; rowCtr < inputqy_GetHumanaObsOutputColumnsList.Count; rowCtr++)
            {
                excelRowCtr++;
                obsSheet.InsertRow(excelRowCtr);
                obsSheet.Range[$"A{excelRowCtr}"].Text = inputqy_GetHumanaObsOutputColumnsList[rowCtr].RptDt;
                obsSheet.Range[$"B{excelRowCtr}"].Text = inputqy_GetHumanaObsOutputColumnsList[rowCtr].Type;
                obsSheet.Range[$"C{excelRowCtr}"].Text = inputqy_GetHumanaObsOutputColumnsList[rowCtr].Group;
                obsSheet.Range[$"D{excelRowCtr}"].Text = inputqy_GetHumanaObsOutputColumnsList[rowCtr].GrouperName;
                obsSheet.Range[$"E{excelRowCtr}"].Text = inputqy_GetHumanaObsOutputColumnsList[rowCtr].FacDbName;
                obsSheet.Range[$"F{excelRowCtr}"].Text = inputqy_GetHumanaObsOutputColumnsList[rowCtr].PcpName;
                obsSheet.Range[$"G{excelRowCtr}"].Text = inputqy_GetHumanaObsOutputColumnsList[rowCtr].CaseType;
                obsSheet.Range[$"H{excelRowCtr}"].Text = inputqy_GetHumanaObsOutputColumnsList[rowCtr].AuthId;
                obsSheet.Range[$"I{excelRowCtr}"].Text = inputqy_GetHumanaObsOutputColumnsList[rowCtr].FirstDay;
                obsSheet.Range[$"J{excelRowCtr}"].Text = inputqy_GetHumanaObsOutputColumnsList[rowCtr].SubscriberId;
                obsSheet.Range[$"K{excelRowCtr}"].Text = inputqy_GetHumanaObsOutputColumnsList[rowCtr].FirstName;
                obsSheet.Range[$"L{excelRowCtr}"].Text = inputqy_GetHumanaObsOutputColumnsList[rowCtr].LastName;
                obsSheet.Range[$"M{excelRowCtr}"].Text = inputqy_GetHumanaObsOutputColumnsList[rowCtr].DateOfBirth;
                obsSheet.Range[$"N{excelRowCtr}"].Text = inputqy_GetHumanaObsOutputColumnsList[rowCtr].GrouperId;
                obsSheet.Range[$"O{excelRowCtr}"].Text = inputqy_GetHumanaObsOutputColumnsList[rowCtr].PcpId;
                obsSheet.Range[$"P{excelRowCtr}"].Text = inputqy_GetHumanaObsOutputColumnsList[rowCtr].FaxTaxId;
                obsSheet.Range[$"Q{excelRowCtr}"].Text = inputqy_GetHumanaObsOutputColumnsList[rowCtr].AuthType;
                obsSheet.Range[$"R{excelRowCtr}"].Text = inputqy_GetHumanaObsOutputColumnsList[rowCtr].RequestType;
                obsSheet.Range[$"S{excelRowCtr}"].Text = inputqy_GetHumanaObsOutputColumnsList[rowCtr].NotifDate;
                obsSheet.Range[$"T{excelRowCtr}"].Text = inputqy_GetHumanaObsOutputColumnsList[rowCtr].LastDay;
                obsSheet.Range[$"U{excelRowCtr}"].Text = inputqy_GetHumanaObsOutputColumnsList[rowCtr].DiagCode1;
                obsSheet.Range[$"V{excelRowCtr}"].Text = inputqy_GetHumanaObsOutputColumnsList[rowCtr].DiagDesc1;
                obsSheet.Range[$"W{excelRowCtr}"].Text = inputqy_GetHumanaObsOutputColumnsList[rowCtr].DiagCode2;
                obsSheet.Range[$"X{excelRowCtr}"].Text = inputqy_GetHumanaObsOutputColumnsList[rowCtr].DiagDesc2;
                obsSheet.Range[$"Y{excelRowCtr}"].Text = inputqy_GetHumanaObsOutputColumnsList[rowCtr].DiagCode3;
                obsSheet.Range[$"Z{excelRowCtr}"].Text = inputqy_GetHumanaObsOutputColumnsList[rowCtr].DiagDesc3;
                obsSheet.Range[$"AA{excelRowCtr}"].Text = inputqy_GetHumanaObsOutputColumnsList[rowCtr].ProcCode;
                obsSheet.Range[$"AB{excelRowCtr}"].Text = inputqy_GetHumanaObsOutputColumnsList[rowCtr].ProcDesc;
                obsSheet.Range[$"AC{excelRowCtr}"].Text = inputqy_GetHumanaObsOutputColumnsList[rowCtr].Gender;
            }


            workbook.Save();

            return returnOutput;
        }

    }
}
