using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifyVbcAdt9.Data.Models
{
    public class qy_GetHumanaDischargesOutput
    {
        public qy_GetHumanaDischargesOutput()
        {
            IsOk = true;
            ErrorMessage = string.Empty;
            qy_GetHumanaDischargesOutputColumnsList =
                new List<qy_GetHumanaDischargesOutputColumns>();
        }
        public bool IsOk { get; set; }
        public string ErrorMessage { get; set; }
        public List<qy_GetHumanaDischargesOutputColumns>
            qy_GetHumanaDischargesOutputColumnsList
        { get; set; }
    }
}