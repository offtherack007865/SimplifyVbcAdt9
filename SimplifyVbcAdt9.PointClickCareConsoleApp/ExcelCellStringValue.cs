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
                    ConvertMMSlashddSlashyyyyCommaTimeToCanonicalDateTimeFormat
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
        public string ConvertMMSlashddSlashyyyyCommaTimeToCanonicalDateTimeFormat(string inputDateInMMMSpaceddSpaceyyyy)
        {
            string returnOutput = string.Empty;
            returnOutput = inputDateInMMMSpaceddSpaceyyyy.Replace("NaN", "");
            string[] fullDateParts =
                inputDateInMMMSpaceddSpaceyyyy.Split(" ");

            List<string> fullDatePartsList = new List<string>();

            foreach(string part in fullDateParts)
            {
                if (part.Trim().Length != 0)
                {
                    fullDatePartsList.Add(part);
                }
            }
            if (fullDatePartsList.Count != 3)
            {
                return returnOutput;
            }
            string MMSlashddSlashyyyy = fullDatePartsList[0].Trim();
            string myTime = fullDatePartsList[1].Trim();

            string myAmOrPm = fullDatePartsList[2].Trim();

            if (myAmOrPm.ToUpper().CompareTo("AM") != 0 &&
                myAmOrPm.ToUpper().CompareTo("PM") != 0)
            {
                return returnOutput;
            }


            string[] dateParts =
               MMSlashddSlashyyyy.Split("/");
            if (dateParts.Length != 3)
            {
                return returnOutput;
            }
            string MM = dateParts[0].Trim();
            string dd = dateParts[1].Trim();
            string yyyy = dateParts[2].Trim();

            string[] hhColonmmColonssParts =
                myTime.Split(":");
            if (hhColonmmColonssParts.Length != 2)
            {
                return returnOutput;
            }

            string hh = hhColonmmColonssParts[0].Trim();
            string mm = hhColonmmColonssParts[1].Trim();

            bool addTwelveHours = false;
            if (myAmOrPm.ToUpper().CompareTo("PM") == 0)
            {
                addTwelveHours = true;
            }
            int hhInt = -1;
            int.TryParse(hh, out hhInt);
            if (hhInt < 0 || hhInt > 11)
            {
                return returnOutput;
            }

            if (addTwelveHours == true)
            {
                hhInt = hhInt + 12;
                hh = hhInt.ToString();
            }

            DateTime myDateTime = DateTime.MinValue;
            returnOutput =
                $"{MM}/{dd}/{yyyy} {hh}:{mm}:00.000";
            DateTime.TryParse(returnOutput, out myDateTime);

            if (myDateTime == DateTime.MinValue ||
                myDateTime == new DateTime(1900, 1, 1))
            {
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
