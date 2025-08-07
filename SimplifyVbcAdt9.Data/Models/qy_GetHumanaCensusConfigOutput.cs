using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifyVbcAdt9.Data.Models
{
    public class qy_GetHumanaCensusConfigOutput
    {
        public qy_GetHumanaCensusConfigOutput()
        {
            IsOk = true;
            ErrorMessage = string.Empty;
            qy_GetHumanaCensusConfigOutputColumnsList =
                new List<qy_GetHumanaCensusConfigOutputColumns>();
        }
        public bool IsOk {  get; set; }
        public string ErrorMessage { get; set; }
        public List<qy_GetHumanaCensusConfigOutputColumns>
            qy_GetHumanaCensusConfigOutputColumnsList
            { get; set; }
    }
}
