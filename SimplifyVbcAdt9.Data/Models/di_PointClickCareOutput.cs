using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifyVbcAdt9.Data.Models
{
    public class di_PointClickCareOutput
    {
        public di_PointClickCareOutput()
        {
            IsOk = true;
            ErrorMessage = string.Empty;
            di_PointClickCareOutputColumnsList =
                new List<di_PointClickCareOutputColumns>();
        }
        public bool IsOk { get; set; }
        public string ErrorMessage { get; set; }
        public List<di_PointClickCareOutputColumns> di_PointClickCareOutputColumnsList 
            { get; set; }
    }
}
