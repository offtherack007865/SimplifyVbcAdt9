using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifyVbcAdt9.Data.Models
{
    public class dd_HumanaOutput
    {
        public dd_HumanaOutput()
        {
            IsOk = true;
            ErrorMessage = string.Empty;
            dd_HumanaOutputColumnsList =
                new List<dd_HumanaOutputColumns>();
        }
        public bool IsOk { get; set; }
        public string ErrorMessage { get; set; }
        public List<dd_HumanaOutputColumns>
            dd_HumanaOutputColumnsList
            { get; set; }
    }
}
