using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifyVbcAdt9.Data.Models
{
    public class qy_GetEthinOutput
    {
        public qy_GetEthinOutput()
        {
            IsOk = true;
            ErrorMessage = string.Empty;
            qy_GetEthinOutputColumnsList =
                new List<qy_GetEthinOutputColumns>();
        }
        public bool IsOk { get; set; }
        public string ErrorMessage { get; set; }
        public List<qy_GetEthinOutputColumns>
            qy_GetEthinOutputColumnsList
            { get; set; }
    }
}
