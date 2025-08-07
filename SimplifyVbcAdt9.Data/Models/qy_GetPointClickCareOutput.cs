using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifyVbcAdt9.Data.Models
{
    public class qy_GetPointClickCareOutput
    {
        public qy_GetPointClickCareOutput()
        {
            IsOk = true;
            ErrorMessage = string.Empty;
            qy_GetPointClickCareOutputColumnsList =
                new List<qy_GetPointClickCareOutputColumns>();
        }
        public bool IsOk { get; set; }
        public string ErrorMessage { get; set; }
        public List<qy_GetPointClickCareOutputColumns>
            qy_GetPointClickCareOutputColumnsList
            { get; set; }
    }
}
