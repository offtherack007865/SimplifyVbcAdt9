using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifyVbcAdt9.Data.Models
{
    public class ExtractExcelDataFromHumanaSingleFileOutput
    {
        public ExtractExcelDataFromHumanaSingleFileOutput()
        {
            IsOk = true;
            ErrorMessage = string.Empty;
            MyCsvLineList = new List<string>();
            MyObsCsvLineList = new List<string>();
        }
        public bool IsOk { get; set; }
        public string ErrorMessage { get; set; }
        public List<string> MyCsvLineList { get; set; }
        public List<string> MyObsCsvLineList { get; set; }

    }
}
