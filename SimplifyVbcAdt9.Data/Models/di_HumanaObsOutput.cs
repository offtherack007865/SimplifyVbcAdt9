using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifyVbcAdt9.Data.Models
{
    public class di_HumanaObsOutput
    {
        public di_HumanaObsOutput()
        {
            IsOk = true;
            ErrorMessage = string.Empty;
            di_HumanaObsOutputColumnsList =
                new List<di_HumanaObsOutputColumns>();
        }
        public bool IsOk {  get; set; }
        public string ErrorMessage { get; set; }
        public List<di_HumanaObsOutputColumns>
            di_HumanaObsOutputColumnsList
            { get; set; }
    }
}
