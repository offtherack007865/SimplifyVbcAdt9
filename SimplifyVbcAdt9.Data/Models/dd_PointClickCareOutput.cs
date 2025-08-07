using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifyVbcAdt9.Data.Models
{
    public class dd_PointClickCareOutput
    {
        public dd_PointClickCareOutput()
        {
            IsOk = true;
            ErrorMessage = string.Empty;
            dd_PointClickCareOutputColumnsList =
                new List<dd_PointClickCareOutputColumns>();
        }
        public bool IsOk { get; set; }
        public string ErrorMessage { get; set; }
        public List<dd_PointClickCareOutputColumns>
            dd_PointClickCareOutputColumnsList
            { get; set; }
    }
}
