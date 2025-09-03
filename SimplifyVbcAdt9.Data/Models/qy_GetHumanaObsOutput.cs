using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifyVbcAdt9.Data.Models
{
    public class qy_GetHumanaObsOutput
    {
        public qy_GetHumanaObsOutput()
        {
            IsOk = true;
            ErrorMessage = string.Empty;
            qy_GetHumanaObsOutputColumnsList =
                new List<qy_GetHumanaObsOutputColumns>();
        }
        public bool IsOk { get; set; }
        public string ErrorMessage { get; set; }
        public List<qy_GetHumanaObsOutputColumns>
            qy_GetHumanaObsOutputColumnsList
            { get; set; }
    }
}
