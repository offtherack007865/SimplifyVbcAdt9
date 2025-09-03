using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifyVbcAdt9.Data.Models
{
    public class dd_HumanaObsOutput
    {
        public dd_HumanaObsOutput()
        {
            IsOk = true;
            ErrorMessage = string.Empty;
            dd_HumanaObsOutputColumnsList =
                new List<dd_HumanaObsOutputColumns>();
        }
        public bool IsOk { get; set; }
        public string ErrorMessage { get; set; }
        public List<dd_HumanaObsOutputColumns>
            dd_HumanaObsOutputColumnsList
            { get; set; }
    }
}
