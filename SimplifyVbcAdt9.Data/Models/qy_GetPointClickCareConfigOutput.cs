using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifyVbcAdt9.Data.Models
{
    public class qy_GetPointClickCareConfigOutput
    {
        public qy_GetPointClickCareConfigOutput()
        {
            IsOk = true;
            ErrorMessage = string.Empty;
            qy_GetPointClickCareConfigOutputColumnsList =
                new List<qy_GetPointClickCareConfigOutputColumns>();
        }
        public bool IsOk { get; set; }
        public string ErrorMessage { get; set; }
        public List<qy_GetPointClickCareConfigOutputColumns>
            qy_GetPointClickCareConfigOutputColumnsList
            { get; set; }
    }
}
