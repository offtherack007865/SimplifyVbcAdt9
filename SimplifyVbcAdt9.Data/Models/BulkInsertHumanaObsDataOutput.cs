using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifyVbcAdt9.Data.Models
{
    public class BulkInsertHumanaObsDataOutput
    {
        public BulkInsertHumanaObsDataOutput()
        {
            IsOk = true;
            ErrorMessage = string.Empty;
        }
        public bool IsOk { get; set; }
        public string ErrorMessage { get; set; }
    }
}
