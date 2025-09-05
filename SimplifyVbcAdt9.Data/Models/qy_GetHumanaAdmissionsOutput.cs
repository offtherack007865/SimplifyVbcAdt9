using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifyVbcAdt9.Data.Models
{
    public class qy_GetHumanaAdmissionsOutput
    {
        public qy_GetHumanaAdmissionsOutput()
        {
            IsOk = true;
            ErrorMessage = string.Empty;
            qy_GetHumanaOutputColumnsList =
                new List<qy_GetHumanaAdmissionsOutputColumns>();
        }
        public bool IsOk { get; set; }
        public string ErrorMessage { get; set; }
        public List<qy_GetHumanaAdmissionsOutputColumns>
            qy_GetHumanaOutputColumnsList
            { get; set; }
    }
}
