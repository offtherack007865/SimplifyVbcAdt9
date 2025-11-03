using SimplifyVbcAdt9.Data.Models;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifyVbcAdt9.PointClickCareConsoleApp
{
    public class ExcelCellStringValue
    {
        public ExcelCellStringValue
        (
        Worksheet inputWorksheet
            , qy_GetPointClickCareConfigOutputColumns inputConfigOptions
            , string inputColumnName
            , int inputRowNumber
            , int inputColumnNumber
        )
        {
            MyWorksheet = inputWorksheet;
            MyConfigOptions = inputConfigOptions;
            MyColumnName = inputColumnName;
            MyRowNumber = inputRowNumber;
            MyColumnNumber = inputColumnNumber;
            if (MyColumnNumber == 14)
            {
                int i = 0;
                i++;
            }
        }
        public Worksheet MyWorksheet { get; set; }
        public qy_GetPointClickCareConfigOutputColumns MyConfigOptions { get; set; }
        public string MyColumnName { get; set; }
        public int MyRowNumber { get; set; }
        public int MyColumnNumber { get; set; }
        public string MyColumnDesignation
        {
            get
            {
                switch (MyColumnNumber)
                {
                    case 1:
                        return "A";
                    case 2:
                        return "B";
                    case 3:
                        return "C";
                    case 4:
                        return "D";
                    case 5:
                        return "E";
                    case 6:
                        return "F";
                    case 7:
                        return "G";
                    case 8:
                        return "H";
                    case 9:
                        return "I";
                    case 10:
                        return "J";
                    case 11:
                        return "K";
                    case 12:
                        return "L";
                    case 13:
                        return "M";
                    case 14:
                        return "N";
                    case 15:
                        return "O";
                    case 16:
                        return "P";
                    case 17:
                        return "Q";
                    case 18:
                        return "R";
                    case 19:
                        return "S";
                    case 20:
                        return "T";
                    case 21:
                        return "U";
                    case 22:
                        return "V";
                    case 23:
                        return "W";
                    case 24:
                        return "X";
                    case 25:
                        return "Y";
                    case 26:
                        return "Z";
                    case 27:
                        return "AA";
                    case 28:
                        return "AB";
                    case 29:
                        return "AC";
                    case 30:
                        return "AD";
                    case 31:
                        return "AE";
                    case 32:
                        return "AF";
                    case 33:
                        return "AG";
                    case 34:
                        return "AH";
                    case 35:
                        return "AI";
                    case 36:
                        return "AJ";
                    case 37:
                        return "AK";
                    case 38:
                        return "AL";
                    case 39:
                        return "AM";
                    case 40:
                        return "AN";
                    case 41:
                        return "AO";
                    case 42:
                        return "AP";
                    case 43:
                        return "AQ";
                    case 44:
                        return "AR";
                    case 45:
                        return "AS";
                    case 46:
                        return "AT";
                    case 47:
                        return "AU";
                    case 48:
                        return "AV";
                    case 49:
                        return "AW";
                    case 50:
                        return "AX";
                    case 51:
                        return "AY";
                    case 52:
                        return "AZ";
                    case 53:
                        return "BA";
                    case 54:
                        return "BB";
                    case 55:
                        return "BC";
                    case 56:
                        return "BD";
                    case 57:
                        return "BE";
                    case 58:
                        return "BF";
                    case 59:
                        return "BG";
                    case 60:
                        return "BH";
                    case 61:
                        return "BI";
                    case 62:
                        return "BJ";
                    case 63:
                        return "BK";
                    case 64:
                        return "BL";
                    case 65:
                        return "BM";
                    case 66:
                        return "BN";
                    case 67:
                        return "BO";

                    default:
                        return string.Empty;
                }
            }
        }
        public string MyCellDesignation
        {
            get
            {
                if (MyColumnDesignation.Length == 0)
                {
                    return string.Empty;
                }
                return $"{MyColumnDesignation}{MyRowNumber.ToString()}";
            }
        }


        public GetExcelCellStringValueOutput GetExcelCellStringValue()
        {
            GetExcelCellStringValueOutput returnOutput = new GetExcelCellStringValueOutput();
            string? cellValue = null;

            // If we have run out of columns to get return.
            if (MyCellDesignation.Length == 0)
            {
                return returnOutput;
            }

            cellValue = MyWorksheet.Range[MyCellDesignation].Text;

            if (cellValue == null)
            {
                cellValue = MyWorksheet.Range[MyCellDesignation].NumberValue.ToString();
            }

            if (cellValue == null)
            {
                cellValue = string.Empty;
            }
            if (MyCellDesignation.StartsWith("I") ||
                MyCellDesignation.StartsWith("M") ||
                MyCellDesignation.StartsWith("Q"))
            {
                cellValue =
                    ConvertMMSlashddSlashyyyyBlankTimeToCanonicalDateTimeFormat
                    (
                        cellValue
                    );
                returnOutput.OutputStringValue = cellValue;
            }
            else
            {
                returnOutput.OutputStringValue = cellValue.Replace("NaN", "").Replace(",", "^").Replace("\'", "").Replace("\"", "").Trim();
            }

            return returnOutput;
        }
        public string ConvertMMSlashddSlashyyyyBlankTimeToCanonicalDateTimeFormat(string inputDateInMMMSpaceddSpaceyyyy)
        {
            // Seems to come in in either format
            // 1.  MM/dd/yyyy Hh:mm
            // 2.  MM/dd/yyyy hh:mm AM|PM 

            string candidateFullDateTimeString = string.Empty;


            string returnOutput = string.Empty;
            inputDateInMMMSpaceddSpaceyyyy = inputDateInMMMSpaceddSpaceyyyy.Replace("NaN", "");
            string[] fullDateParts =
                inputDateInMMMSpaceddSpaceyyyy.Split(" ");

            List<string> fullDatePartsList = new List<string>();

            foreach (string part in fullDateParts)
            {
                if (part.Trim().Length != 0)
                {
                    fullDatePartsList.Add(part.Trim());
                }
            }
            int testInt = -1;
            int hhInt = -1;

            string[] dateParts;
            List<string> datePartsList = new List<string>();
            string[] timeParts;
            List<string> timePartsList = new List<string>();
            string MMString = string.Empty;
            string ddString = string.Empty;
            string yyyyString = string.Empty;
            string hhString = string.Empty;
            string mmString = string.Empty;
            DateTime testDateTime = DateTime.MinValue;


            // 1.  Parse and validate MM/dd/yyyy Hh:mm
            if (fullDatePartsList.Count == 2)
            {
                dateParts = fullDatePartsList[0].Split("/");
                foreach (string part in dateParts)
                {
                    if (part.Trim().Length != 0)
                    {
                        datePartsList.Add(part.Trim());
                    }
                }

                // Parse and validate date parts.
                if (datePartsList.Count != 3)
                {
                    return returnOutput;
                }
                MMString = $"{datePartsList[0].ToString().PadLeft(2, '0')}";
                ddString = $"{datePartsList[1].ToString().PadLeft(2, '0')}";
                yyyyString = datePartsList[2].ToString();

                testInt = -1;
                int.TryParse(MMString, out testInt);
                if (testInt < 1 || testInt > 12)
                {
                    return returnOutput;
                }

                testInt = -1;
                int.TryParse(ddString, out testInt);
                if (testInt < 1 || testInt > 31)
                {
                    return returnOutput;
                }

                // Parse and validate time parts.
                timeParts = fullDatePartsList[1].Split(":");
                if (timeParts.Length != 3 && timeParts.Length != 2)
                {
                    return returnOutput;
                }
                foreach (string part in timeParts)
                {
                    if (part.Trim().Length != 0)
                    {
                        timePartsList.Add(part.Trim());
                    }
                }

                if (timePartsList.Count != 3 && timeParts.Length != 2)
                {
                    return returnOutput;
                }
                hhString = $"{timePartsList[0].ToString().PadLeft(2, '0')}";
                mmString = $"{timePartsList[1].ToString().PadLeft(2, '0')}";

                testInt = -1;
                int.TryParse(hhString, out testInt);
                if (testInt == -1 || testInt > 23)
                {
                    return returnOutput;
                }

                testInt = -1;
                int.TryParse(mmString, out testInt);
                if (testInt == -1 || testInt > 59)
                {
                    return returnOutput;
                }

                candidateFullDateTimeString =
                    $"{yyyyString}-{MMString}-{ddString} {hhString}:{mmString}:00.000";

                testDateTime = DateTime.MinValue;
                DateTime.TryParse(candidateFullDateTimeString, out testDateTime);

                if (testDateTime == DateTime.MinValue)
                {
                    return returnOutput;
                }
                returnOutput = candidateFullDateTimeString;
                return returnOutput;
            }
            //  Parse and validate 2.  MM/dd/yyyy hh:mm AM|PM 
            else if (fullDatePartsList.Count == 3 && (fullDatePartsList[2].Trim().ToUpper().CompareTo("AM") == 0 || fullDatePartsList[2].Trim().ToUpper().CompareTo("PM") == 0))
            {
                dateParts = fullDatePartsList[0].Split("/");
                foreach (string part in dateParts)
                {
                    if (part.Trim().Length != 0)
                    {
                        datePartsList.Add(part.Trim());
                    }
                }

                // Parse and validate date parts.
                if (datePartsList.Count != 3)
                {
                    return returnOutput;
                }
                MMString = $"{datePartsList[0].ToString().PadLeft(2, '0')}";
                ddString = $"{datePartsList[1].ToString().PadLeft(2, '0')}";
                yyyyString = datePartsList[2].ToString();

                testInt = -1;
                int.TryParse(MMString, out testInt);
                if (testInt < 1 || testInt > 12)
                {
                    return returnOutput;
                }

                testInt = -1;
                int.TryParse(ddString, out testInt);
                if (testInt < 1 || testInt > 31)
                {
                    return returnOutput;
                }

                // Parse and validate time parts.
                timeParts = fullDatePartsList[1].Split(":");
                foreach (string part in timeParts)
                {
                    if (part.Trim().Length != 0)
                    {
                        timePartsList.Add(part.Trim());
                    }
                }

                if (timePartsList.Count != 2)
                {
                    return returnOutput;
                }
                hhString = $"{timePartsList[0].ToString().PadLeft(2, '0')}";
                mmString = $"{timePartsList[1].ToString().PadLeft(2, '0')}";

                testInt = -1;
                int.TryParse(hhString, out testInt);
                if (testInt == -1 || testInt > 12)
                {
                    return returnOutput;
                }

                // Adjust hours to PM time.
                hhInt = testInt;
                if (hhInt < 12 && fullDatePartsList[2].Trim().ToUpper().CompareTo("PM") == 0)
                {
                    hhInt = hhInt + 12;
                }
                hhString = hhInt.ToString().PadLeft(2, '0');

                testInt = -1;
                int.TryParse(mmString, out testInt);
                if (testInt == -1 || testInt > 59)
                {
                    return returnOutput;
                }
                candidateFullDateTimeString =
                    $"{yyyyString}-{MMString}-{ddString} {hhString}:{mmString}:00.000";

                testDateTime = DateTime.MinValue;
                DateTime.TryParse(candidateFullDateTimeString, out testDateTime);

                if (testDateTime == DateTime.MinValue)
                {
                    return returnOutput;
                }
                returnOutput = candidateFullDateTimeString;
                return returnOutput;
            }
            return returnOutput;
        }




        public string SplitNameIntoFirstAndLastName(string inputFullName)
        {
            string returnOutput = string.Empty;
            returnOutput = inputFullName.Replace("NaN", "");
            if (returnOutput.Contains(","))
            {
                string[] strPartsArray =
                    inputFullName.Split(",");
                if (strPartsArray.Length == 2)
                {
                    returnOutput = $"{returnOutput.Replace(",", "^")},{strPartsArray[0].Trim()},{strPartsArray[1].Trim()}";
                }
            }
            return returnOutput;
        }
    }

}
