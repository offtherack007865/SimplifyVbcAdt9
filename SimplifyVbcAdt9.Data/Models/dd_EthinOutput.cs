using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifyVbcAdt9.Data.Models
{
    public class dd_EthinOutput
    {
        public dd_EthinOutput()
        {
            IsOk = true;
            ErrorMessage = string.Empty;
            dd_EthinOutputColumnsList =
                new List<dd_EthinOutputColumns>();
        }
        public bool IsOk { get; set; }
        public string ErrorMessage { get; set; }
        public List<dd_EthinOutputColumns>
            dd_EthinOutputColumnsList
            { get; set; }
    }
}
