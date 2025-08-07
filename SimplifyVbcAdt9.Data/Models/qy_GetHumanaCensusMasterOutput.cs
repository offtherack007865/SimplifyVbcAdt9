using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifyVbcAdt9.Data.Models
{
    public class qy_GetHumanaCensusMasterOutput
    {
        public qy_GetHumanaCensusMasterOutput()
        {
            IsOk = true;
            ErrorMessage = string.Empty;
            qy_GetHumanaCensusAdtMasterOutputColumnsList =
                new List<qy_GetHumanaCensusMasterOutputColumns>();
        }
        public bool IsOk { get; set; }
        public string ErrorMessage { get; set; }
        public List<qy_GetHumanaCensusMasterOutputColumns>
            qy_GetHumanaCensusAdtMasterOutputColumnsList
            { get; set; }
    }
}
