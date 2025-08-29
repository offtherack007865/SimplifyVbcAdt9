using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifyVbcAdt9.Data.Models
{
   public class qy_GetEthinConfigOutput
    {
        public qy_GetEthinConfigOutput()
        {
            IsOk = true;
            ErrorMessage = string.Empty;
            qy_GetEthinConfigOutputColumnsList =
                new List<qy_GetEthinConfigOutputColumns>();
        }
        public bool IsOk { get; set; }
        public string ErrorMessage { get; set; }
        public List<qy_GetEthinConfigOutputColumns>
            qy_GetEthinConfigOutputColumnsList
            { get; set; }
    }
}
