using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifyVbcAdt9.Data.Models
{
    public class qy_GetHumanaConfigOutput
    {
        public qy_GetHumanaConfigOutput()
        {
            IsOk = true;
            ErrorMessage = string.Empty;
            qy_GetHumanaConfigOutputColumnsList =
                new List<qy_GetHumanaConfigOutputColumns>();
        }
        public bool IsOk { get; set; }
        public string ErrorMessage { get; set; }
        public List<qy_GetHumanaConfigOutputColumns>
            qy_GetHumanaConfigOutputColumnsList
            { get; set; }
    }
}
