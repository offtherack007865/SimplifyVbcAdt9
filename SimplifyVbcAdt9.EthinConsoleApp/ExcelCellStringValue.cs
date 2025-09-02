using EmailWebApiLand9.Data.Models;
using SimplifyVbcAdt9.Data.Models;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifyVbcAdt9.EthinConsoleApp
{
    public class ExcelCellStringValue
    {
        public ExcelCellStringValue
        (
            Worksheet inputWorksheet
            , qy_GetEthinConfigOutputColumns inputConfigOptions
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
        public qy_GetEthinConfigOutputColumns MyConfigOptions { get; set; }
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

            if (MyCellDesignation.CompareTo("J253") == 0)
            {
                int i = 0;
                i++;
            }
            // For some reason the Ethin Deceased Date input field is filled with a date plus a "00" value.
            // This causes the cell value to be the same date but with a "2000" year.  Replace the 2000 with
            // the current year.  Also, the date may come in the MM/dd/yyyy hh:mm:ss format.  If it does,
            // make the cell value the date field only.  If that date contains the year 2000, replace it with 
            // the current year.
            if ((MyCellDesignation.StartsWith("C") && cellValue.Length > 0) ||
                (MyCellDesignation.StartsWith("L") && cellValue.Length > 0) ||
                (MyCellDesignation.StartsWith("M") && cellValue.Length > 0))
            {
                cellValue =
                    GetStringFormatForTimestamp
                    (
                        cellValue
                    );
            }


            // For some reason the Ethin Deceased Date input field is filled with a date plus a "00" value.
            // This causes the cell value to be the same date but with a "2000" year.  Replace the 2000 with
            // the current year.  Also, the date may come in the MM/dd/yyyy hh:mm:ss format.  If it does,
            // make the cell value the date field only.  If that date contains the year 2000, replace it with 
            // the current year.
            if (MyCellDesignation.StartsWith("J") && cellValue.Length > 0)
            {
                string[] dateTimeParts = cellValue.Split(" ");
                if (dateTimeParts.Length > 0)
                {
                    cellValue =
                        dateTimeParts[0];
                }
                if (cellValue.EndsWith("/2000"))
                {
                    DateTime myCurrentDate = DateTime.Now;
                    cellValue = cellValue.Replace("/2000", $"/{myCurrentDate.Year.ToString()}");
                }
            }

            returnOutput.OutputStringValue = cellValue.Replace("NaN", "").Replace(",", "^").Replace("\'", "").Replace("\"", "").Trim();

            return returnOutput;
        }

        public string GetStringFormatForTimestamp(string inputCellValue)
        {
            string returnOutput = string.Empty;

            string[] timestampParts =
                inputCellValue.Split(' ');
            if (timestampParts.Length != 3)
            {
                return returnOutput;
            }

            string[] dateParts =
               timestampParts[0].ToString().Split('/');
            if (dateParts.Length != 3)
            {
                return returnOutput;
            }


            DateTime myDateTime =
                new DateTime(1900, 1, 1);
            DateTime.TryParse(timestampParts[0], out myDateTime);
            if (myDateTime == new DateTime(1900, 1, 1) ||
                myDateTime == DateTime.MinValue)
            {
                return returnOutput;
            }

            string[] timeParts =
               timestampParts[1].ToString().Split(':');
            if (timeParts.Length != 2)
            {
                return returnOutput;
            }
            string hhString = timeParts[0].Trim();
            string mmString = timeParts[1].Trim();

            int hhInt = -1;
            int.TryParse(hhString, out hhInt);

            if (hhInt <= -1 || hhInt >= 13)
            {
                return returnOutput;
            }
            int mmInt = -1;
            int.TryParse(mmString, out mmInt);
            if (mmInt <= -1 || mmInt >= 60)
            {
                return returnOutput;
            }

            if (timestampParts[2].ToString().ToUpper().CompareTo("PM") == 0)
            {
                hhInt += 12;
            }

            myDateTime = myDateTime.AddHours(hhInt);
            myDateTime = myDateTime.AddMinutes(mmInt);

            returnOutput = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");

            return returnOutput;
        }
    }
}
