using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifyVbcAdt9.Data.Models
{
    public class di_FinalizeHumanaCensusOutput
    {
        public di_FinalizeHumanaCensusOutput()
        {
            IsOk = true;
            ErrorMessage = string.Empty;
            di_FinalizeHumanaCensusOutputColumnsList =
                new List<di_FinalizeHumanaCensusOutputColumns>();
        }
        public bool IsOk { get; set; }
        public string ErrorMessage { get; set; }
        public List<di_FinalizeHumanaCensusOutputColumns>
            di_FinalizeHumanaCensusOutputColumnsList
            { get; set; }
    }
}
