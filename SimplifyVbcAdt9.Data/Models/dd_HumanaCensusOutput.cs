using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifyVbcAdt9.Data.Models
{
    public class dd_HumanaCensusOutput
    {
        public dd_HumanaCensusOutput()
        {
            IsOk = true;
            ErrorMessage = string.Empty;
            dd_TruncateHumanaCensusOutputColumnsList =
                new List<dd_HumanaCensusOutputColumns>();
        }
        public bool IsOk { get; set; }
        public string ErrorMessage { get; set; }
        public List<dd_HumanaCensusOutputColumns>
            dd_TruncateHumanaCensusOutputColumnsList
            { get; set; }
    }
}
