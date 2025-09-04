using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifyVbcAdt9.Data.Models
{
    public class ProcessSingleHumanaFileOutput
    {
        public ProcessSingleHumanaFileOutput()
        {
            IsOk = true;
            ErrorMessage = string.Empty;
            EmailBodyLine = string.Empty;
        }
        public bool IsOk { get; set; }
        public string ErrorMessage { get; set; }
        public string EmailBodyLine { get; set; }

    }
}
